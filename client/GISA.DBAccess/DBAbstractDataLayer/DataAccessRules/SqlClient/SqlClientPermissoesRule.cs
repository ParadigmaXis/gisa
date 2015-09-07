using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
	public sealed class SqlClientPermissoesRule : PermissoesRule
	{
		#region " Permissões por Módulo "
		public override void LoadDataModuloPermissoes(DataSet CurrentDataSet, Int16 IDTipoFunctionGroup, Int16 IdxTipoFunction, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
                command.Parameters.AddWithValue("@IDTipoFunctionGroup", IDTipoFunctionGroup);
                command.Parameters.AddWithValue("@IdxTipoFunction", IdxTipoFunction);

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["Trustee"]);
				da.Fill(CurrentDataSet, "Trustee");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TrusteeUser"]);
				da.Fill(CurrentDataSet, "TrusteeUser");

				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TrusteeGroup"]);
				da.Fill(CurrentDataSet, "TrusteeGroup");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["UserGroups"]);
                da.Fill(CurrentDataSet, "UserGroups");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TrusteePrivilege"],
                    " WHERE TrusteePrivilege.IDTipoFunctionGroup=@IDTipoFunctionGroup AND TrusteePrivilege.IdxTipoFunction=@IdxTipoFunction");
				da.Fill(CurrentDataSet, "TrusteePrivilege");
			}
		}
		#endregion

		#region Permissões por Classificação de Informação
        public override void LoadDataCIPermissoes(DataSet CurrentDataSet, List<long> lstIDNivel, IDbConnection conn)
        {
            GisaDataSetHelperRule.ImportIDs(lstIDNivel.ToArray(), conn);

            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["Trustee"]);
                da.Fill(CurrentDataSet, "Trustee");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TrusteeUser"]);
                da.Fill(CurrentDataSet, "TrusteeUser");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TrusteeGroup"]);
                da.Fill(CurrentDataSet, "TrusteeGroup");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TrusteeNivelPrivilege"],
                    "INNER JOIN #temp T ON T.ID = TrusteeNivelPrivilege.IDNivel ");
                da.Fill(CurrentDataSet, "TrusteeNivelPrivilege");
            }
        }

        public override void LoadDataCIPermissoes(DataSet CurrentDataSet, long IDNivel, IDbTransaction tran)
        {
            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)tran.Connection, (SqlTransaction) tran))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.Parameters.AddWithValue("@IDNivel", IDNivel);

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["Trustee"]);
                da.Fill(CurrentDataSet, "Trustee");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TrusteeUser"]);
                da.Fill(CurrentDataSet, "TrusteeUser");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TrusteeGroup"]);
                da.Fill(CurrentDataSet, "TrusteeGroup");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TrusteeNivelPrivilege"],
                    "WHERE TrusteeNivelPrivilege.IDNivel = @IDNivel");
                da.Fill(CurrentDataSet, "TrusteeNivelPrivilege");
            }
        }

        public override void LoadDataCIPermissoes(DataSet CurrentDataSet, long IDNivel, IDbConnection conn)
        {
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@IDNivel", IDNivel);
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["Trustee"]);
                da.Fill(CurrentDataSet, "Trustee");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TrusteeUser"]);
                da.Fill(CurrentDataSet, "TrusteeUser");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TrusteeGroup"]);
                da.Fill(CurrentDataSet, "TrusteeGroup");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TrusteeNivelPrivilege"],
                    "WHERE TrusteeNivelPrivilege.IDNivel = @IDNivel");
                da.Fill(CurrentDataSet, "TrusteeNivelPrivilege");
            }
        }

        public override void GetEffectivePermissions(string query, long IDTrustee, IDbConnection conn)
        {
            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #effective (IDNivel BIGINT PRIMARY KEY, IDUpper BIGINT, Criar TINYINT, Ler TINYINT, Escrever TINYINT, Apagar TINYINT, Expandir TINYINT)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO #effective SELECT DISTINCT ID, ID, NULL, NULL, NULL, NULL, NULL " + query;
                command.ExecuteNonQuery();

                command.CommandText = "sp_getEffectivePermissions";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@IDTrustee", SqlDbType.BigInt);
                command.Parameters[0].Value = IDTrustee;
                command.ExecuteNonQuery();
            }
        }

        public override void GetEffectiveReadPermissions(string query, long IDTrustee, IDbConnection conn)
        {
            long start = DateTime.Now.Ticks;
            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #effective (IDNivel BIGINT PRIMARY KEY, IDUpper BIGINT, Ler TINYINT)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO #effective SELECT DISTINCT ID, ID, null " + query;
                command.ExecuteNonQuery();

                command.CommandText = "sp_getEffectiveReadPermissions";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@IDTrustee", SqlDbType.BigInt);
                command.Parameters[0].Value = IDTrustee;
                command.ExecuteNonQuery();
            }
            Debug.WriteLine("<<GetEffectiveReadPermissions>> " + new TimeSpan(DateTime.Now.Ticks - start).ToString());
        }

        /* 
         * Obter lista de permissões efectivadas por utilizador/grupo. No caso dos utilizadores não são consideradas as permissões dos grupos aos quais pertencem 
         * para o cálculo das permissões efectivas. A permissão "real" efectiva do utilizador será a combinação entre a sua permissão "efectiva" com o resultado
         * da combinação das permissões efectivas dos seus grupos
         */
        public override Dictionary<long, Dictionary<string, bool>> GetEffectiveReadWritePermissions(long IDNivel, IDbConnection conn)
        {
            long start = DateTime.Now.Ticks;
            Dictionary<long, Dictionary<string, bool>> nivelPermissoes;
            using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn))
            {
                command.CommandText = "CREATE TABLE #effective (IDNivel BIGINT, IDUpper BIGINT, IDTrustee BIGINT, Ler TINYINT, Escrever TINYINT)";
                command.ExecuteNonQuery();

                command.CommandText =
@"
DECLARE @grp_pub BIGINT
SELECT @grp_pub = ID FROM Trustee where Name = 'ACESSO_PUBLICADOS'
INSERT INTO #effective
SELECT @IDNivel, @IDNivel, ID, null, null
from Trustee
where IsActive = @IsActive AND isDeleted = @IsDeleted AND ID <> @grp_pub";
                command.Parameters.AddWithValue("@IDNivel", IDNivel);
                command.Parameters.AddWithValue("@IsActive", 1);
                command.Parameters.AddWithValue("@IsDeleted", 0);
                command.ExecuteNonQuery();
                command.Parameters.Clear();

                command.CommandText = "sp_getEffectiveReadWritePermissions";
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();

                command.CommandText = "SELECT IDTrustee, Ler, Escrever FROM #effective";
                command.CommandType = CommandType.Text;
                var reader = command.ExecuteReader();

                nivelPermissoes = PermissoesRule.GetEffectiveReadWritePermissions(reader);

                PermissoesRule.Current.DropEffectivePermissionsTempTable(conn);
            }
            Debug.WriteLine("<<GetEffectiveReadWritePermissions>> " + new TimeSpan(DateTime.Now.Ticks - start).ToString());

            return nivelPermissoes;
        }

        public override void DropEffectivePermissionsTempTable(IDbConnection conn)
        {
            using (SqlCommand command = new SqlCommand("DROP TABLE #effective", (SqlConnection)conn))
            {
                command.ExecuteNonQuery();
            }
        }
		#endregion

		#region FormAdUtilizadores
		public override void LoadUtilizadores(DataSet CurrentDataSet, IDbConnection conn)
		{
            using (SqlCommand command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
			{
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["Trustee"]);
				da.Fill(CurrentDataSet, "Trustee");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TrusteeGroup"]);
				da.Fill(CurrentDataSet, "TrusteeGroup");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TrusteeUser"]);
				da.Fill(CurrentDataSet, "TrusteeUser");
				da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["UserGroups"]);
				da.Fill(CurrentDataSet, "UserGroups");
			}
		}
		#endregion

		#region ControloNivelList
        public override Dictionary<long, Dictionary<string, byte>> CalculateEffectivePermissions(List<long> IDNiveis, long IDTrustee, IDbConnection conn)
        {
            GisaDataSetHelperRule.ImportIDs(IDNiveis.ToArray(), conn);

            var query = "FROM #temp";

            PermissoesRule.Current.GetEffectivePermissions(query, IDTrustee, conn);

            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = "SELECT * FROM #effective";            
            var reader = command.ExecuteReader();

            var nivelPermissoes = GetEffectivePermissions(reader);

            PermissoesRule.Current.DropEffectivePermissionsTempTable(conn);

            return nivelPermissoes;
        }

        public override Dictionary<long, Dictionary<string, byte>> CalculateImplicitPermissions(long IDNivel, long IDTrustee, IDbConnection conn)
        {
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = "CREATE TABLE #effective (IDNivel BIGINT PRIMARY KEY, IDUpper BIGINT, Criar TINYINT, Ler TINYINT, Escrever TINYINT, Apagar TINYINT, Expandir TINYINT)";
            command.ExecuteNonQuery();

            command.CommandText = string.Format("INSERT INTO #effective SELECT {0}, {0}, NULL, NULL, NULL, NULL, NULL ", IDNivel);
            command.ExecuteNonQuery();

            command.CommandText = "sp_getImplicitPermissions";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@IDTrustee", SqlDbType.BigInt);
            command.Parameters[0].Value = IDTrustee;
            command.ExecuteNonQuery();

            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM #effective";
            var reader = command.ExecuteReader();

            var nivelPermissoes = GetEffectivePermissions(reader);

            PermissoesRule.Current.DropEffectivePermissionsTempTable(conn);
            return nivelPermissoes;
        }
		#endregion

        #region SlavePanelPermissoesOBjDigital
        public override List<PermissoesRule.ObjDig> LoadDataObjDigital(DataSet CurrentDataSet, long IDNivel, long IDTrustee, long IDLoginTrustee, out Dictionary<long, Dictionary<long, byte>> permsImpl, IDbConnection conn)
        {
            var res = new List<PermissoesRule.ObjDig>();
            permsImpl = new Dictionary<long, Dictionary<long, byte>>();

            using (var command = new SqlCommand("", (SqlConnection)conn))
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                command.CommandText = "CREATE TABLE #temp(ID BIGINT PRIMARY KEY); CREATE TABLE #odsTemp(ID BIGINT PRIMARY KEY, pid NVARCHAR(20), titulo NVARCHAR(768));";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO #temp VALUES (@IDNivel)";
                command.Parameters.AddWithValue("@IDNivel", IDNivel);
                command.ExecuteNonQuery();

                command.CommandText = @"
WITH Temp (ID, IDUpper)
AS (
    SELECT rh.ID, rh.IDUpper
    FROM RelacaoHierarquica rh
    WHERE rh.IDUpper = @IDNivel AND rh.isDeleted = @isDeleted
    
    UNION ALL
	
    SELECT rh.ID, rh.IDUpper
    FROM RelacaoHierarquica rh
		INNER JOIN Temp ON Temp.ID = rh.IDUpper
    WHERE rh.isDeleted = @isDeleted
)
INSERT INTO #temp
SELECT Temp.ID 
FROM Temp";
                command.Parameters.AddWithValue("@isDeleted", 0);
                command.ExecuteNonQuery();

                PermissoesRule.Current.GetEffectiveReadPermissions(" FROM #temp ", IDLoginTrustee, conn);

                command.CommandText = "DELETE FROM #temp WHERE ID IN (SELECT IDNivel FROM #effective WHERE Ler = @Ler OR Ler IS NULL)";
                command.Parameters.AddWithValue("@Ler", 0);
                command.ExecuteNonQuery();

                PermissoesRule.Current.DropEffectivePermissionsTempTable(conn);

                command.CommandText = @"
INSERT INTO #odsTemp
SELECT ID, pid, Titulo
FROM (
SELECT od.ID, od.pid, od.Titulo 
FROM #temp T
INNER JOIN FRDBase frd ON frd.IDNivel = T.ID AND frd.IDTipoFRDBase = @IDTipoFRDBase AND frd.isDeleted = @isDeleted
INNER JOIN SFRDImagem img ON img.IDFRDBase = frd.ID AND img.Tipo = @imgTipo AND img.isDeleted = @isDeleted
INNER JOIN SFRDImagemObjetoDigital imgOD ON imgOD.IDFRDBase = img.IDFRDBase AND imgOD.idx = img.idx AND imgOD.isDeleted = @isDeleted
INNER JOIN ObjetoDigital od ON od.ID = imgOD.IDObjetoDigital AND od.isDeleted = @isDeleted
UNION ALL
SELECT odSimples.ID, odSimples.pid, odSimples.Titulo
FROM #temp T
INNER JOIN FRDBase frd ON frd.IDNivel = T.ID AND frd.IDTipoFRDBase = @IDTipoFRDBase AND frd.isDeleted = @isDeleted
INNER JOIN SFRDImagem img ON img.IDFRDBase = frd.ID AND img.Tipo = @imgTipo AND img.isDeleted = @isDeleted
INNER JOIN SFRDImagemObjetoDigital imgOD ON imgOD.IDFRDBase = img.IDFRDBase AND imgOD.idx = img.idx AND imgOD.isDeleted = @isDeleted
INNER JOIN ObjetoDigital od ON od.ID = imgOD.IDObjetoDigital AND od.isDeleted = @isDeleted
INNER JOIN ObjetoDigitalRelacaoHierarquica odrh ON odrh.IDUpper = od.ID AND odrh.isDeleted = @isDeleted
INNER JOIN ObjetoDigital odSimples ON odSimples.ID = odrh.ID AND odSimples.isDeleted = @isDeleted
) ods";
                command.Parameters.AddWithValue("@IDTipoFRDBase", 1);
                command.Parameters.AddWithValue("@imgTipo", "Fedora");
                command.ExecuteNonQuery();

                PermissoesRule.Current.GetODEffectivePermissions(" FROM #odsTemp ", IDLoginTrustee, conn);

                command.CommandText = "DELETE FROM #odsTemp WHERE ID IN (SELECT DISTINCT ID FROM #effective WHERE IsGrant = @IsGrant OR IsGrant IS NULL)";
                command.Parameters.AddWithValue("@IsGrant", 0);
                command.ExecuteNonQuery();

                PermissoesRule.Current.DropEffectivePermissionsTempTable(conn);

                command.CommandText = "SELECT * FROM #odsTemp";
                var reader = command.ExecuteReader();

                var od = new PermissoesRule.ObjDig();
                while (reader.Read())
                {
                    od = new PermissoesRule.ObjDig();
                    od.ID = reader.GetInt64(0);
                    od.pid = reader.GetString(1);
                    od.titulo = reader.GetString(2);
                    res.Add(od);
                }
                reader.Close();

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["ObjetoDigital"],
                    "INNER JOIN #odsTemp ON #odsTemp.ID = ObjetoDigital.ID ");
                da.Fill(CurrentDataSet, "ObjetoDigital");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TrusteeUser"]);
                da.Fill(CurrentDataSet, "TrusteeUser");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TrusteeGroup"]);
                da.Fill(CurrentDataSet, "TrusteeGroup");

                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["UserGroups"]);
                da.Fill(CurrentDataSet, "UserGroups");

                // carregar permissões
                da.SelectCommand.CommandText = SqlSyntax.CreateSelectCommandText(CurrentDataSet.Tables["TrusteeObjetoDigitalPrivilege"],
                    "INNER JOIN #odsTemp ON #odsTemp.ID = TrusteeObjetoDigitalPrivilege.IDObjetoDigital " +
                    "WHERE TrusteeObjetoDigitalPrivilege.IDTrustee = " + IDTrustee);
                da.Fill(CurrentDataSet, "TrusteeObjetoDigitalPrivilege");

                command.CommandText = "DROP TABLE #temp; DROP TABLE #odsTemp; ";
                command.ExecuteNonQuery();
            }
            return res;
        }

        public override void GetODEffectivePermissions(string query, long IDTrustee, IDbConnection conn)
        {
            var command = new SqlCommand(string.Empty, (SqlConnection)conn);
            command.CommandText = string.Format(@"CREATE TABLE #effective (ID BIGINT, IDTipoOperation BIGINT, IsGrant TINYINT, PRIMARY KEY (ID, IDTipoOperation));
INSERT INTO #effective SELECT DISTINCT ID, 2, null {0}
INSERT INTO #effective SELECT DISTINCT ID, 3, null {0}", query);
            command.ExecuteNonQuery();

            command.CommandText = "sp_getODEffectivePermissions";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@IDTrustee", SqlDbType.BigInt);
            command.Parameters[0].Value = IDTrustee;
            command.ExecuteNonQuery();
        }

        public override int CalculateODGroupPermissions(long IDTrustee, long IDObjetoDigital, long IDOperation, IDbConnection conn)
        {
            using (var command = SqlSyntax.CreateSelectCommandWithNoDeletedRowsParam((SqlConnection)conn))
            {
                command.Parameters.AddWithValue("@IDTrustee", IDTrustee);
                command.Parameters.AddWithValue("@IDObjetoDigital", IDObjetoDigital);
                command.Parameters.AddWithValue("@IDOperation", IDOperation);
                command.CommandText = @"
select MIN(CONVERT(tinyint, todp.IsGrant))
from TrusteeObjetoDigitalPrivilege todp
	inner join ObjetoDigitalTipoOperation odtp on odtp.IDTipoOperation = todp.IDTipoOperation and odtp.isDeleted = @isDeleted
	inner join UserGroups ug on ug.IDGroup = todp.IDTrustee and ug.isDeleted = @isDeleted
where todp.IDObjetoDigital = @IDObjetoDigital and ug.IDUser = @IDTrustee and todp.IDTipoOperation = @IDOperation and todp.isDeleted = @isDeleted";
                var res = command.ExecuteScalar();
                return res == DBNull.Value ? -1 : System.Convert.ToInt32(res);
            }
        }
        #endregion
    }
}
