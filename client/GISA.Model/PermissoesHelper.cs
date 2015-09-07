using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;
using GISA.Model;

namespace GISA
{
	public class PermissoesHelper
	{
        public static Font fontRegular = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
        public static Font fontItalic = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
        public static Font fontBold = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
        public static Font fontBoldItalic = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold | FontStyle.Italic, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
		
		public enum PermissionType: int
		{
			ImplicitDeny = 0,
            ImplicitGrant = 1,
			ExplicitGrant = 2,
			ExplicitDeny = 3
		}

		public enum PermissionTarget: int
		{
			Modulos = 0,
			Niveis = 1
		}

        private static GISADataset.TrusteeRow mGrpAcessoCompleto = null;
        public static GISADataset.TrusteeRow GrpAcessoCompleto
        {
            get 
            {
                if (mGrpAcessoCompleto == null)
                    mGrpAcessoCompleto =
                        (GISADataset.TrusteeRow)GisaDataSetHelper.GetInstance().Trustee.Select("Name='ACESSO_COMPLETO'")[0];

                return mGrpAcessoCompleto;
            }
        }

        private static GISADataset.TrusteeRow mGrpAcessoPublicados = null;
        public static GISADataset.TrusteeRow GrpAcessoPublicados
        {
            get
            {
                if (mGrpAcessoPublicados == null)
                    mGrpAcessoPublicados =
                        (GISADataset.TrusteeRow)GisaDataSetHelper.GetInstance().Trustee.Select("Name='ACESSO_PUBLICADOS'")[0];

                return mGrpAcessoPublicados;
            }
        }

        public static PermissionType CalculateEffectivePermissions(GISADataset.TrusteeRow tRow, GISADataset.TipoOperationRow opRow, GISADataset.DepositoRow depRow)
        {
            var tdpRow = GisaDataSetHelper.GetInstance().TrusteeDepositoPrivilege.Cast<GISADataset.TrusteeDepositoPrivilegeRow>()
                .SingleOrDefault(r => r.RowState != DataRowState.Deleted && r.IDTrustee == tRow.ID && r.IDTipoOperation == opRow.ID && r.IDDeposito == depRow.ID);

            if (tdpRow != null)
                return tdpRow.IsGrant ? PermissionType.ExplicitGrant : PermissionType.ExplicitDeny;

            if (tRow.CatCode.Equals("USR"))
            {
                if (tRow.GetTrusteeUserRows().Length > 0)
                {
                     var ugRows = tRow.GetTrusteeUserRows()[0].GetUserGroupsRows();
                     return CalculateGroupPermissionsDepositos(ugRows, opRow, depRow);
                }
            }

            return PermissionType.ImplicitDeny;
        }

		public static PermissoesHelper.PermissionType CalculateEffectivePermissions(GISADataset.TrusteeRow tRow, GISADataset.FunctionOperationRow foRow)
		{
			GISADataset.UserGroupsRow[] ugRows = null;
			GISADataset.TrusteePrivilegeRow[] tpuRows = null;
			tpuRows = (GISADataset.TrusteePrivilegeRow[])(GisaDataSetHelper.GetInstance().TrusteePrivilege.Select(string.Format("IDTrustee={0} AND IDTipoFunctionGroup={1} AND IdxTipoFunction={2} AND IDTipoOperation={3}", tRow.ID, foRow.IDTipoFunctionGroup, foRow.IdxTipoFunction, foRow.IDTipoOperation)));

			if (tpuRows.Length > 0)
			{
				if (tpuRows[0].IsGrant)
				{
					return PermissionType.ExplicitGrant;
				}
				else
				{
					return PermissionType.ExplicitDeny;
				}
			}

			if (tRow.CatCode.Equals("USR"))
			{
                if (tRow.GetTrusteeUserRows().Length > 0)
                {
                    ugRows = tRow.GetTrusteeUserRows()[0].GetUserGroupsRows();
                    return CalculateGroupPermissionsModulos(ugRows, foRow);
                }
			}
			
			return PermissionType.ImplicitDeny;
		}

        private static PermissionType CalculateGroupPermissionsDepositos(GISADataset.UserGroupsRow[] ugRows, GISADataset.TipoOperationRow opRow, GISADataset.DepositoRow depRow)
        {
            PermissionType GroupPermission = PermissionType.ImplicitDeny;

            if (ugRows == null) return GroupPermission;

            ugRows.ToList().ForEach(ugRow =>
                {
                    var pgRows = GisaDataSetHelper.GetInstance().TrusteeDepositoPrivilege.Cast<GISADataset.TrusteeDepositoPrivilegeRow>()
                        .Where(r => r.RowState != DataRowState.Deleted && r.IDDeposito == depRow.ID && r.IDTipoOperation == opRow.ID && r.IDTrustee == ugRow.TrusteeGroupRow.TrusteeRow.ID).Cast<DataRow>().ToArray();

                    GroupPermission = CalculateGroupPermissions(GroupPermission, pgRows);
                });

            return GroupPermission;
        }

        private static PermissionType CalculateGroupPermissionsObjDigitais(GISADataset.UserGroupsRow[] ugRows, GISADataset.TipoOperationRow opRow, GISADataset.ObjetoDigitalRow odRow)
        {
            PermissionType GroupPermission = PermissionType.ImplicitDeny;

            if (ugRows == null) return GroupPermission;

            ugRows.ToList().ForEach(ugRow =>
            {
                var pgRows = GisaDataSetHelper.GetInstance().TrusteeObjetoDigitalPrivilege.Cast<GISADataset.TrusteeObjetoDigitalPrivilegeRow>()
                    .Where(r => r.RowState != DataRowState.Deleted && r.IDObjetoDigital == odRow.ID && r.IDTipoOperation == opRow.ID && r.IDTrustee == ugRow.TrusteeGroupRow.TrusteeRow.ID).Cast<DataRow>().ToArray();

                GroupPermission = CalculateGroupPermissions(GroupPermission, pgRows);
            });

            return GroupPermission;
        }

		public static PermissionType CalculateGroupPermissionsModulos(GISADataset.UserGroupsRow[] ugRows, GISADataset.FunctionOperationRow foRow)
		{
			string QueryGroup = string.Empty;
			DataRow[] pgRows = null;
			PermissionType GroupPermission = PermissionType.ImplicitDeny;

			if (ugRows == null)
			{
				return GroupPermission;
			}

			foreach (GISADataset.UserGroupsRow ugRow in ugRows)
			{
				QueryGroup = "IDTrustee={0} AND IDTipoFunctionGroup={1} AND IdxTipoFunction={2} AND IDTipoOperation={3}";
				QueryGroup = string.Format(QueryGroup, ugRow.TrusteeGroupRow.TrusteeRow.ID, foRow.IDTipoFunctionGroup, foRow.IdxTipoFunction, foRow.IDTipoOperation);
				pgRows = GisaDataSetHelper.GetInstance().TrusteePrivilege.Select(QueryGroup);

				GroupPermission = CalculateGroupPermissions(GroupPermission, pgRows);
			}

			return GroupPermission;
		}

		public static PermissionType CalculateGroupPermissions(PermissionType GroupPermission, DataRow[] privilegeRows)
		{
			if (privilegeRows.Length == 1)
			{
				if ((bool)(privilegeRows[0]["IsGrant"]))
				{
					if (GroupPermission == PermissionType.ImplicitDeny)
					{
						GroupPermission = PermissionType.ExplicitGrant;
					}
					else if (GroupPermission == PermissionType.ExplicitDeny)
					{
						// mentem-se o explicitdeny
					}
					else if (GroupPermission == PermissionType.ExplicitGrant)
					{
						// mantem-se o explictgrant
					}
				}
				else
				{
					if (GroupPermission == PermissionType.ImplicitDeny)
					{
						// muda-se para explicitdeny
						GroupPermission = PermissionType.ExplicitDeny;
					}
					else if (GroupPermission == PermissionType.ExplicitDeny)
					{
						// mentem-se o explicitdeny
					}
					else if (GroupPermission == PermissionType.ExplicitGrant)
					{
						// o deny tem prioridade sobre o grant quando definidos ao mesmo nível
						GroupPermission = PermissionType.ExplicitDeny;
					}
				}
			}
			return GroupPermission;
		}

        #region Permissões Nivel

        public static void ChangeToPermission(GISADataset.TrusteeRow user, GISADataset.NivelRow nivel, ListViewItem item, int colIndex, string permValue)
        {
            string TipoOperationName = item.ListView.Columns[colIndex].Text;
            var tnpRow = GetTrusteeNivelPrivilegeRow(user, nivel);

            if (permValue == null || permValue == string.Empty)
                tnpRow[TipoOperationName] = DBNull.Value;
            else if (permValue.Equals("Sim"))
                tnpRow[TipoOperationName] = 1;
            else if (permValue.Equals("Não"))
                tnpRow[TipoOperationName] = 0;

            var permissaoEfectiva = GetEffectivePermission(item, colIndex, TipoOperationName, tnpRow, user.ID);
            PopulatePermission(item, colIndex, tnpRow, permissaoEfectiva);
        }

        // as permissões alternam entre: atribuída -> não atribuída -> não definida (é o mesmo que não atribuído mas necessário 
        // para definir heranças de grupos de utilizadores e/ou níveis hierarquicamente superiores)
        public static void ChangePermission(GISADataset.TrusteeRow user, GISADataset.NivelRow nivel, ListViewItem item, int colIndex)
        {
            string TipoOperationName = item.ListView.Columns[colIndex].Text;
            var tnpRow = GetTrusteeNivelPrivilegeRow(user, nivel);

            if (tnpRow[TipoOperationName] == DBNull.Value)
                tnpRow[TipoOperationName] = 1;
            else if ((byte)tnpRow[TipoOperationName] == 1)
                tnpRow[TipoOperationName] = 0;
            else
            {
                // a permissão explícita é apagada e a permissão efectiva passa a ser herdada pelo grupo e/ou nível 
                // hierarquicamente superior (se for qualquer um dos casos)
                tnpRow[TipoOperationName] = DBNull.Value;
            }

            var permissaoEfectiva = GetEffectivePermission(item, colIndex, TipoOperationName, tnpRow, user.ID);
            PopulatePermission(item, colIndex, tnpRow, permissaoEfectiva);
        }

        private static PermissoesHelper.PermissionType GetEffectivePermission(ListViewItem item, int colIndex, string TipoOperationName, GISADataset.TrusteeNivelPrivilegeRow tnpRow, long IDTrustee)
        {
            PermissoesHelper.PermissionType permissaoEfectiva = PermissionType.ImplicitDeny;
            if ((tnpRow != null) && (tnpRow[TipoOperationName] != DBNull.Value))
                permissaoEfectiva =
                    (byte)tnpRow[TipoOperationName] == 1 ? PermissionType.ExplicitGrant : PermissionType.ExplicitDeny;
            else
            {
                var nRow = (GISADataset.NivelRow)item.Tag;
                var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                try
                {
                    var perms = PermissoesRule.Current.CalculateImplicitPermissions(nRow.ID, IDTrustee, ho.Connection);
                    if (perms.ContainsKey(nRow.ID))
                        permissaoEfectiva = GetPermissionValue(perms[nRow.ID], GisaDataSetHelper.GetInstance().NivelTipoOperation.Cast<GISADataset.NivelTipoOperationRow>().Single(r => r.TipoOperationRow.Name.Equals(TipoOperationName))) == 1 ? PermissionType.ImplicitGrant : PermissionType.ImplicitDeny;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                    throw;
                }
                finally
                {
                    ho.Dispose();
                }
            }

            return permissaoEfectiva;
        }

        #endregion

        #region Permissões Objeto Digital Fedora
        public static void ChangeODPermission(GISADataset.TrusteeRow user, GISADataset.NivelRow nivel, ListViewItem item, int colIndex)
        {
            var odRow = item.Tag as GISADataset.ObjetoDigitalRow;
            var opRow = item.SubItems[colIndex].Tag as GISADataset.ObjetoDigitalTipoOperationRow;
            var todpRow = GisaDataSetHelper.GetInstance().TrusteeObjetoDigitalPrivilege.Cast<GISADataset.TrusteeObjetoDigitalPrivilegeRow>()
                .SingleOrDefault(r => r.RowState != DataRowState.Deleted && r.IDObjetoDigital == odRow.ID && r.IDTipoOperation == opRow.IDTipoOperation && r.IDTrustee == user.ID);

            if (todpRow != null)
            {
                if (todpRow.IsGrant)
                    todpRow.IsGrant = false;
                else
                    todpRow.Delete();
            }
            else
            {
                todpRow = GisaDataSetHelper.GetInstance().TrusteeObjetoDigitalPrivilege.Cast<GISADataset.TrusteeObjetoDigitalPrivilegeRow>()
                    .SingleOrDefault(r => r.RowState == DataRowState.Deleted && (long)r["IDObjetoDigital", DataRowVersion.Original] == odRow.ID && (byte)r["IDTipoOperation", DataRowVersion.Original] == opRow.IDTipoOperation && (long)r["IDTrustee", DataRowVersion.Original] == user.ID);

                if (todpRow != null)
                {
                    todpRow.RejectChanges();
                    todpRow.IsGrant = true;
                }
                else
                    todpRow = GisaDataSetHelper.GetInstance().TrusteeObjetoDigitalPrivilege.AddTrusteeObjetoDigitalPrivilegeRow(user, odRow, opRow, true, new byte[] { }, 0);
            }

            // popular as alterações
            var permissaoEfectiva = PermissoesHelper.CalculateEffectivePermissions(odRow, user, nivel, opRow.TipoOperationRow);
            PopulatePermission(item, colIndex, todpRow, permissaoEfectiva);
        }

        public static PermissionType CalculateEffectivePermissions(GISADataset.ObjetoDigitalRow odRow, GISADataset.TrusteeRow tRow, GISADataset.NivelRow nRow, GISADataset.TipoOperationRow opRow)
        {
            var tdpRow = GisaDataSetHelper.GetInstance().TrusteeObjetoDigitalPrivilege.Cast<GISADataset.TrusteeObjetoDigitalPrivilegeRow>()
                .SingleOrDefault(r => r.RowState != DataRowState.Deleted && r.IDTrustee == tRow.ID && r.IDTipoOperation == opRow.ID && r.IDObjetoDigital == odRow.ID);

            if (tdpRow != null)
                return tdpRow.IsGrant ? PermissionType.ExplicitGrant : PermissionType.ExplicitDeny;
            else
            {
                var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                try
                {
                    var perm = PermissoesRule.Current.CalculateODGroupPermissions(tRow.ID, odRow.ID, opRow.ID, ho.Connection);
                    if (perm < 0)
                    {
                        var nivelPerms = PermissoesRule.Current.CalculateEffectivePermissions(new List<long>() { nRow.ID }, tRow.ID, ho.Connection);
                        return GetPermissionValue(nivelPerms[nRow.ID], GisaDataSetHelper.GetInstance().NivelTipoOperation.Cast<GISADataset.NivelTipoOperationRow>().Single(r => r.TipoOperationRow.Name.Equals(opRow.Name))) == 1 ? PermissionType.ImplicitGrant : PermissionType.ImplicitDeny;
                    }
                    else
                        return perm == 0 ? PermissionType.ImplicitDeny : PermissionType.ImplicitGrant;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                    throw;
                }
                finally
                {
                    ho.Dispose();
                }
            }
        }
        #endregion

        private static GISADataset.TrusteeNivelPrivilegeRow GetTrusteeNivelPrivilegeRow(GISADataset.TrusteeRow user, GISADataset.NivelRow nivel)
        {
            var tnpRow = GisaDataSetHelper.GetInstance().TrusteeNivelPrivilege.Rows.Cast<GISADataset.TrusteeNivelPrivilegeRow>()
                .SingleOrDefault(r => r.IDNivel == nivel.ID && r.IDTrustee == user.ID && r.RowState != DataRowState.Deleted);

            if (tnpRow == null)
            {
                tnpRow = GisaDataSetHelper.GetInstance().TrusteeNivelPrivilege.Rows.Cast<GISADataset.TrusteeNivelPrivilegeRow>()
                 .SingleOrDefault(r => r.IDNivel == nivel.ID && r.IDTrustee == user.ID && r.RowState == DataRowState.Deleted);

                if (tnpRow != null)
                    tnpRow.RejectChanges();
                else
                {
                    tnpRow = GisaDataSetHelper.GetInstance().TrusteeNivelPrivilege.NewTrusteeNivelPrivilegeRow();
                    tnpRow.IDTrustee = user.ID;
                    tnpRow.IDNivel = nivel.ID;
                    tnpRow["Criar"] = DBNull.Value;
                    tnpRow["Ler"] = DBNull.Value;
                    tnpRow["Escrever"] = DBNull.Value;
                    tnpRow["Apagar"] = DBNull.Value;
                    tnpRow["Expandir"] = DBNull.Value;
                    tnpRow.Versao = new byte[] { };
                    tnpRow.isDeleted = 0;
                    GisaDataSetHelper.GetInstance().TrusteeNivelPrivilege.AddTrusteeNivelPrivilegeRow(tnpRow);
                }
            }
            return tnpRow;
        }

        public static void PopulatePermission(ListViewItem item, int colIndex, DataRow privilegeRow, PermissoesHelper.PermissionType permissaoEfectiva)
        {
            string grant = string.Empty;
            if (permissaoEfectiva == PermissoesHelper.PermissionType.ExplicitGrant ||
                permissaoEfectiva == PermissoesHelper.PermissionType.ImplicitGrant)

                grant = "Sim";
            else if (permissaoEfectiva == PermissoesHelper.PermissionType.ExplicitDeny ||
                permissaoEfectiva == PermissoesHelper.PermissionType.ImplicitDeny)

                grant = "Não";

            item.SubItems[colIndex].Text = grant;

            if (permissaoEfectiva == PermissoesHelper.PermissionType.ImplicitGrant ||
                permissaoEfectiva == PermissoesHelper.PermissionType.ImplicitDeny)
            {
                if (privilegeRow == null || privilegeRow.RowState == DataRowState.Unchanged || privilegeRow.RowState == DataRowState.Added || privilegeRow.RowState == DataRowState.Detached)
                    item.SubItems[colIndex].Font = PermissoesHelper.fontItalic;
                else
                    item.SubItems[colIndex].Font = PermissoesHelper.fontBoldItalic;
            }
            else
            {
                if (privilegeRow.RowState == DataRowState.Unchanged || !Concorrencia.isModifiedRow(privilegeRow))
                    item.SubItems[colIndex].Font = PermissoesHelper.fontRegular;
                else
                    item.SubItems[colIndex].Font = PermissoesHelper.fontBold;
            }
        }

	#region  Permissoes por Classificação 
		private static bool mAllowCreate;
		private static bool mAllowDelete;
		private static bool mAllowEdit;
		private static bool mAllowRead;
		private static bool mAllowExpand;

		public static bool AllowCreate
		{
			get {return mAllowCreate;}
		}

		public static bool AllowDelete
		{
			get {return mAllowDelete;}
		}

		public static bool AllowEdit
		{
			get {return mAllowEdit;}
		}

		public static bool AllowRead
		{
			get {return mAllowRead;}
		}

		public static bool AllowExpand
		{
			get {return mAllowExpand;}
		}

        public static bool CanChangePermission(ListViewItem item, int colIndex)
        {
            return !(GisaDataSetHelper.GetInstance().TipoOperation.Select(
                string.Format("Name='{0}'", item.ListView.Columns[colIndex].Text)).Length == 0);
        }

		public static void UpdateNivelPermissions(GISADataset.NivelRow NivelRow, GISADataset.NivelRow NivelUpperRow, long trusteeUserOperatorID)
		{
			ClearPermissons();
			byte[] permissoes = null;
			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetTempConnection());
			try
			{
                if (NivelRow != null)
                {
                    permissoes = GetPermissons(NivelRow.ID, NivelUpperRow.ID, trusteeUserOperatorID, ho.Connection);
                    mAllowCreate = permissoes[0] == 1;
                    mAllowRead = permissoes[1] == 1;
                    mAllowEdit = permissoes[2] == 1;
                    mAllowDelete = permissoes[3] == 1;
                    mAllowExpand = permissoes[4] == 1;
                }
                else
                {
                    permissoes = GetPermissons(NivelUpperRow.ID, trusteeUserOperatorID, ho.Connection);
                    mAllowCreate = permissoes[0] == 1;
                }
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.ToString());
				throw ex;
			}
			finally
			{
				ho.Dispose();
			}
		}

		public static void UpdateNivelPermissions(GISADataset.NivelRow NivelRow, long trusteeUserOperatorID)
		{
			ClearPermissons();
			if (NivelRow == null)
				return;

			if (NivelRow.RowState == DataRowState.Detached)
				return;

			byte[] permissoes = null;
			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetTempConnection());
			try
			{
				permissoes = GetPermissons(NivelRow.ID, trusteeUserOperatorID, ho.Connection);
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.ToString());
				throw ex;
			}
			finally
			{
				ho.Dispose();
			}
			mAllowCreate = permissoes[0] == 1;
			mAllowRead = permissoes[1] == 1;
			mAllowEdit = permissoes[2] == 1;
			mAllowDelete = permissoes[3] == 1;
			mAllowExpand = permissoes[4] == 1;
		}

        private static byte GetPermissionValue(Dictionary<string, byte> permissaoOperacao, GISADataset.NivelTipoOperationRow nto)
        {
            return !permissaoOperacao.ContainsKey(nto.TipoOperationRow.Name) ? (byte)0 : permissaoOperacao[nto.TipoOperationRow.Name];
        }

		private static byte[] GetPermissons(long NivelID, long TrusteeID, IDbConnection conn)
		{
			byte Create = 0;
            byte Delete = 0;
            byte Edit = 0;
            byte Read = 0;
            byte Expand = 0;
            var permissoes = new Dictionary<long, Dictionary<string, byte>>();

			try
			{
				permissoes = DBAbstractDataLayer.DataAccessRules.PermissoesRule.Current.CalculateEffectivePermissions(new List<long>() { NivelID }, TrusteeID, conn);
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.ToString());
				throw ex;
			}

            var permissaoOperacao = new Dictionary<string, byte>();
            if (!permissoes.ContainsKey(NivelID))
                return new byte[] { Create, Read, Edit, Delete, Expand };
            else
                permissaoOperacao = permissoes[NivelID];

            foreach (GISADataset.NivelTipoOperationRow nto in GisaDataSetHelper.GetInstance().NivelTipoOperation)
            {
                switch (nto.IDTipoOperation)
                {
                    case 1:
                        Create = GetPermissionValue(permissaoOperacao, nto);
                        break;
                    case 2:
                        Read = GetPermissionValue(permissaoOperacao, nto);
                        break;
                    case 3:
                        Edit = GetPermissionValue(permissaoOperacao, nto);
                        break;
                    case 4:
                        Delete = GetPermissionValue(permissaoOperacao, nto);
                        break;
                    case 7:
                        Expand = GetPermissionValue(permissaoOperacao, nto);
                        break;
                }

            }
			return new byte[] {Create, Read, Edit, Delete, Expand};
		}

        private static byte[] GetPermissons(long NivelID, long NivelIDUpper, long TrusteeID, IDbConnection conn)
		{
            byte Create = 0;
            byte Delete = 0;
            byte Edit = 0;
            byte Read = 0;
            byte Expand = 0;
            var permissoes = new Dictionary<string, byte>();
            var niveispermissoes = new Dictionary<long, Dictionary<string, byte>>();
            List<long> NivelIDs = new List<long>();
            NivelIDs.Add(NivelID);
            NivelIDs.Add(NivelIDUpper);

			try
			{
				niveispermissoes = DBAbstractDataLayer.DataAccessRules.PermissoesRule.Current.CalculateEffectivePermissions(NivelIDs, TrusteeID, conn);
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.ToString());
				throw ex;
			}

			permissoes = niveispermissoes[NivelID];
			foreach (GISADataset.NivelTipoOperationRow nto in GisaDataSetHelper.GetInstance().NivelTipoOperation)
			{
				if (permissoes.ContainsKey(nto.TipoOperationRow.Name))
				{
					switch (nto.IDTipoOperation)
					{
						case 1:
							Create = GetPermissionValue(permissoes, nto);
							break;
						case 2:
                            Read = GetPermissionValue(permissoes, nto);
							break;
						case 3:
                            Edit = GetPermissionValue(permissoes, nto);
							break;
						case 4:
                            Delete = GetPermissionValue(permissoes, nto);
							break;
						case 7:
                            Expand = GetPermissionValue(permissoes, nto);
							break;
					}
				}
			}
            Create = GetPermissionValue(niveispermissoes[NivelIDUpper], GisaDataSetHelper.GetInstance().NivelTipoOperation.Cast<GISADataset.NivelTipoOperationRow>().Single(r => r.IDTipoOperation == 1));
			return new byte[] {Create, Read, Edit, Delete, Expand};
		}

		private static void ClearPermissons()
		{
			mAllowCreate = false;
			mAllowDelete = false;
			mAllowEdit = false;
			mAllowRead = false;
			mAllowExpand = false;
		}
	#endregion

        public static void AddNewNivelGrantPermissions(GISADataset.NivelRow NivelRow)
        {
            var trusteeRow = SessionHelper.GetGisaPrincipal().TrusteeUserOperator.TrusteeRow;
            GisaDataSetHelper.GetInstance().TrusteeNivelPrivilege.AddTrusteeNivelPrivilegeRow(trusteeRow, NivelRow, 1, 1, 1, 1, 1, new byte[] { }, 0);
            GisaDataSetHelper.GetInstance().TrusteeNivelPrivilege.AddTrusteeNivelPrivilegeRow(GrpAcessoCompleto, NivelRow, 1, 1, 1, 1, 1, new byte[] { }, 0);
        }

        public static void AddNewNivelGrantPermissions(GISADataset.NivelRow NivelRow, GISADataset.NivelRow NivelUpperRow)
        {
            AddNewNivelGrantPermissions(NivelRow);

            if (NivelUpperRow == null) return; // trata-se de uma entidade detentora
            
            // aos casos restantes deve-se atribuir permissões ao utilizador que criou o nivel e a todos os outros (utilizadores
            // ou grupos) que tenham permissões de criação no nivel superior)
            var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                DBAbstractDataLayer.DataAccessRules.PermissoesRule.Current.LoadDataCIPermissoes(GisaDataSetHelper.GetInstance(), NivelRow.ID, ho.Connection);
                if (NivelUpperRow != null)
                    DBAbstractDataLayer.DataAccessRules.PermissoesRule.Current.LoadDataCIPermissoes(GisaDataSetHelper.GetInstance(), NivelUpperRow.ID, ho.Connection);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
            finally
            {
                ho.Dispose();
            }

            var tuOperator = SessionHelper.GetGisaPrincipal().TrusteeUserOperator.TrusteeRow;

            GisaDataSetHelper.GetInstance().TrusteeGroup.Cast<GISADataset.TrusteeGroupRow>()
                .Select(tg => tg.TrusteeRow).Where(r => r.IsVisibleObject && r.ID != tuOperator.ID && r.ID != GrpAcessoCompleto.ID).ToList()
                .ForEach(tRow =>
                {
                    var tnpUpperRow = GisaDataSetHelper.GetInstance().TrusteeNivelPrivilege.Cast<GISADataset.TrusteeNivelPrivilegeRow>().SingleOrDefault(r => r.IDTrustee == tRow.ID && r.IDNivel == NivelUpperRow.ID);

                    if (tnpUpperRow != null && !tnpUpperRow.IsCriarNull() && tnpUpperRow.Criar == 1 && !tnpUpperRow.IsExpandirNull() && tnpUpperRow.Expandir == 1)
                    {
                        GisaDataSetHelper.GetInstance().TrusteeNivelPrivilege.AddTrusteeNivelPrivilegeRow(
                            tRow, NivelRow, 1, 1, 1, 1, 1, new byte[] { }, 0);
                    }
                    else if (tnpUpperRow != null && ((!tnpUpperRow.IsCriarNull() && tnpUpperRow.Criar == 0) || (!tnpUpperRow.IsExpandirNull() && tnpUpperRow.Expandir == 0)))
                    {
                        GisaDataSetHelper.GetInstance().TrusteeNivelPrivilege.AddTrusteeNivelPrivilegeRow(
                            tRow, NivelRow, 0, 0, 0, 0, 0, new byte[] { }, 0);
                    }
                });
        }

        public static void AddNivelGrantPermissions(GISADataset.NivelRow NivelRow, GISADataset.NivelRow NivelUpperRow, IDbTransaction iDbTransaction)
        {
            // atribuir permissões ao utilizador que criou o nivel e a todos os outros (utilizadores
            // ou grupos) que tenham permissões de criação no nivel superior)
            
            var tuOperator = SessionHelper.GetGisaPrincipal().TrusteeUserOperator.TrusteeRow;

            GisaDataSetHelper.GetInstance().TrusteeGroup.Cast<GISADataset.TrusteeGroupRow>()
                .Select(tg => tg.TrusteeRow).Where(r => r.IsVisibleObject).ToList().ForEach(tRow =>
                {
                    var tnpRow = GisaDataSetHelper.GetInstance().TrusteeNivelPrivilege.Cast<GISADataset.TrusteeNivelPrivilegeRow>().SingleOrDefault(r => r.IDTrustee == tRow.ID && r.IDNivel == NivelRow.ID);
                    var tnpUpperRow = GisaDataSetHelper.GetInstance().TrusteeNivelPrivilege.Cast<GISADataset.TrusteeNivelPrivilegeRow>().SingleOrDefault(r => r.IDTrustee == tRow.ID && r.IDNivel == NivelUpperRow.ID );

                    if (tnpRow == null && tnpUpperRow != null && !tnpUpperRow.IsCriarNull() && tnpUpperRow.Criar == 1)
                    {
                        GisaDataSetHelper.GetInstance().TrusteeNivelPrivilege.AddTrusteeNivelPrivilegeRow(
                            tRow, NivelRow, tnpUpperRow.Criar, tnpUpperRow.Ler, tnpUpperRow.Escrever,
                            tnpUpperRow.Apagar, tnpUpperRow.Expandir, new byte[] { }, 0);
                    }
                });
        }

        public static void UndoAddNivelGrantPermissions(GISADataset.NivelRow NivelRow)
        {
            Trace.WriteLine("Removing privileges from nivel with ID " + NivelRow.ID.ToString());

            GisaDataSetHelper.GetInstance().TrusteeNivelPrivilege.Cast<GISADataset.TrusteeNivelPrivilegeRow>().
                Where(tnp => tnp.RowState != DataRowState.Deleted && tnp.IDNivel == NivelRow.ID).ToList().ForEach(r => r.Delete());
        }

        public static void UndoAddNivelGrantPermissions(GISADataset.NivelRow NivelRow, GISADataset.TrusteeRow trusteeRow)
        {
            Trace.WriteLine("Removing privileges from nivel with ID " + NivelRow.ID.ToString());

            GisaDataSetHelper.GetInstance().TrusteeNivelPrivilege.Cast<GISADataset.TrusteeNivelPrivilegeRow>().
                Where(tnp => tnp.RowState != DataRowState.Deleted && tnp.IDNivel == NivelRow.ID && tnp.IDTrustee == trusteeRow.ID).ToList().ForEach(r => r.Delete());
        }

        public static void ChangeDocPermissionPublicados(long docID, bool permissao)
        {
            GISADataset.TrusteeNivelPrivilegeRow tnpRow = null;
            GISADataset.TrusteeNivelPrivilegeRow[] tnpRows = (GISADataset.TrusteeNivelPrivilegeRow[])
                GisaDataSetHelper.GetInstance().TrusteeNivelPrivilege.Select(string.Format("IDNivel={0} AND IDTrustee={1}", docID, PermissoesHelper.GrpAcessoPublicados.ID));

            if (tnpRows.Length > 0)
                tnpRow = tnpRows[0];
            else
            {
                tnpRow = GisaDataSetHelper.GetInstance().TrusteeNivelPrivilege.NewTrusteeNivelPrivilegeRow();
                tnpRow.IDNivel = docID;
                tnpRow.IDTrustee = PermissoesHelper.GrpAcessoPublicados.ID;
                tnpRow.Versao = new byte[] { };
                GisaDataSetHelper.GetInstance().TrusteeNivelPrivilege.AddTrusteeNivelPrivilegeRow(tnpRow);
            }

            if (permissao)
                tnpRow.Ler = 1;
            else
                tnpRow["Ler"] = DBNull.Value;
        }

        #region Fedora
        private static bool mObjDigAllowWrite = false;
        private static bool mObjDigAllowRead = false;

        public static bool ObjDigAllowWrite
        {
            get { return mObjDigAllowWrite; }
        }

        public static bool ObjDigAllowRead
        {
            get { return mObjDigAllowRead; }
        }

        private static GISADataset.ObjetoDigitalTipoOperationRow mObjDigOpWRITE;
        public static GISADataset.ObjetoDigitalTipoOperationRow ObjDigOpWRITE
        {
            get
            {
                if (mObjDigOpWRITE == null)
                    mObjDigOpWRITE = GisaDataSetHelper.GetInstance().ObjetoDigitalTipoOperation.Cast<GISADataset.ObjetoDigitalTipoOperationRow>().Single(r => r.IDTipoOperation == (byte)TipoOperation.WRITE);
                return mObjDigOpWRITE;
            }
        }

        private static GISADataset.ObjetoDigitalTipoOperationRow mObjDigOpREAD;
        public static GISADataset.ObjetoDigitalTipoOperationRow ObjDigOpREAD
        {
            get
            {
                if (mObjDigOpREAD == null)
                    mObjDigOpREAD = GisaDataSetHelper.GetInstance().ObjetoDigitalTipoOperation.Cast<GISADataset.ObjetoDigitalTipoOperationRow>().Single(r => r.IDTipoOperation == (byte)TipoOperation.READ);
                return mObjDigOpREAD;
            }
        }

        public static void AddNewObjDigGrantPermissions(List<GISADataset.ObjetoDigitalRow> odRows, GISADataset.NivelRow nRow)
        {
            var trusteeRow = SessionHelper.GetGisaPrincipal().TrusteeUserOperator.TrusteeRow;
            Dictionary<long, Dictionary<string, bool>> perms;
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                // carregar utilizadores e grupos
                PermissoesRule.Current.LoadUtilizadores(GisaDataSetHelper.GetInstance(), ho.Connection);
                // carregar permissões efectivas
                perms = PermissoesRule.Current.GetEffectiveReadWritePermissions(nRow.ID, ho.Connection);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw;
            }
            finally
            {
                ho.Dispose();
            }

            // atribuir permissões efectivas a todos os utilizadores / grupos com base nas permissões efectivas de cada um ao nivel documental
            var ops = GisaDataSetHelper.GetInstance().ObjetoDigitalTipoOperation.Cast<GISADataset.ObjetoDigitalTipoOperationRow>().ToList();
            var trustees = GisaDataSetHelper.GetInstance().Trustee.Cast<GISADataset.TrusteeRow>().ToList();
            Dictionary<string, bool> perm;

            odRows.ForEach(odRow =>
                {
                    trustees.ForEach(tRow =>
                        {
                            if (perms.ContainsKey(tRow.ID) && tRow.ID != trusteeRow.ID)
                            {
                                perm = perms[tRow.ID];
                                ops.ForEach(opRow =>
                                    {
                                        if (perm.ContainsKey(opRow.TipoOperationRow.Name))
                                            GisaDataSetHelper.GetInstance().TrusteeObjetoDigitalPrivilege
                                                .AddTrusteeObjetoDigitalPrivilegeRow(tRow, odRow, opRow, perm[opRow.TipoOperationRow.Name], new byte[] { }, 0);
                                    });
                            }
                        });
                });

            // atribuir permissões totais ao utilizador que está a criar o OD            
            odRows.ForEach(odRow =>
                {
                    ops.ForEach(opRow =>
                                    {
                                        GisaDataSetHelper.GetInstance().TrusteeObjetoDigitalPrivilege
                                            .AddTrusteeObjetoDigitalPrivilegeRow(trusteeRow, odRow, opRow, true, new byte[] { }, 0);
                                    });
                });
        }

        public static void ChangeObjDigPermissionPublicados(GISADataset.ObjetoDigitalRow odRow)
        {
            var todpRow = GisaDataSetHelper.GetInstance().TrusteeObjetoDigitalPrivilege.Cast<GISADataset.TrusteeObjetoDigitalPrivilegeRow>()
                .SingleOrDefault(r => r.RowState != DataRowState.Deleted && r.IDObjetoDigital == odRow.ID && r.IDTrustee == PermissoesHelper.GrpAcessoPublicados.ID);

            if (todpRow == null && odRow.Publicado)
            {
                todpRow = GisaDataSetHelper.GetInstance().TrusteeObjetoDigitalPrivilege.NewTrusteeObjetoDigitalPrivilegeRow();
                todpRow.ObjetoDigitalRow = odRow;
                todpRow.IDTrustee = PermissoesHelper.GrpAcessoPublicados.ID;
                todpRow.IDTipoOperation = (byte)TipoOperation.READ;
                todpRow.IsGrant = odRow.Publicado;
                todpRow.Versao = new byte[] { };
                GisaDataSetHelper.GetInstance().TrusteeObjetoDigitalPrivilege.AddTrusteeObjetoDigitalPrivilegeRow(todpRow);
            }
            else if (todpRow != null)
            {
                if (odRow.Publicado)
                    todpRow.IsGrant = odRow.Publicado;
                else
                    todpRow.Delete();
            }
        }

        public static void LoadObjDigitalPermissions(GISADataset.NivelRow nRow, GISADataset.TrusteeRow tRow)
        {
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                // carregar a informação independentemente do contexto selecionado
                DBAbstractDataLayer.DataAccessRules.PermissoesRule.Current.LoadDataCIPermissoes(GisaDataSetHelper.GetInstance(), nRow.ID, ho.Connection);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw;
            }
            finally
            {
                ho.Dispose();
            }

            var imgRowsObjDig = nRow.GetFRDBaseRows().Single().GetSFRDImagemRows().Where(r => r.Tipo.Equals(FedoraHelper.typeFedora));
            if (imgRowsObjDig.Count() == 1)
            {
                var odRow = imgRowsObjDig.Single().GetSFRDImagemObjetoDigitalRows().Single().ObjetoDigitalRow;
                GisaDataSetHelper.GetInstance().ObjetoDigitalTipoOperation.Cast<GISADataset.ObjetoDigitalTipoOperationRow>().ToList().ForEach(op =>
                {
                    var perm = CalculateEffectivePermissions(odRow, tRow, nRow, op.TipoOperationRow);
                    var isGrant = perm == PermissionType.ExplicitGrant || perm == PermissionType.ImplicitGrant;
                    if (op.IDTipoOperation == (byte)TipoOperation.READ)
                        mObjDigAllowRead = isGrant;
                    else if (op.IDTipoOperation == (byte)TipoOperation.WRITE)
                        mObjDigAllowWrite = isGrant;
                });
            }
            else
            {
                mObjDigAllowRead = true;
                mObjDigAllowWrite = true;
            }

        }
        #endregion

        
    }
}
