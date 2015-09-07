using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;

using GISA.Controls;
using GISA.Controls.ControloAut;

namespace GISA
{
	public class FormControloAutRel : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

        Control parent = null;
		public FormControloAutRel(GISADataset.ControloAutRow ContextCA, Control parent) : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

            //Add any initialization after the InitializeComponent() callcaList.BeforeNewListSelection += caList_BeforeNewListSelection;
            btnAccept.Click += btnAccept_Click;
			mContextControloAut = ContextCA;

			this.relacaoCA.ContextNivelRow = ((GISADataset.NivelControloAutRow)(GisaDataSetHelper.GetInstance().NivelControloAut.Select(string.Format("IDControloAut={0}", ContextCA.ID))[0])).NivelRow;            
            caList.BeforeNewListSelection += caList_BeforeNewListSelection;
            this.caList.FilterVisible = true;

            this.parent = parent;
		}

		//Form overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components = null;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		internal System.Windows.Forms.Button btnAccept;
		internal System.Windows.Forms.Button btnCancel;
		protected internal ControloAutList caList;
		protected internal System.Windows.Forms.GroupBox GroupBox1;
		internal GISA.RelacaoControloAut relacaoCA;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.btnAccept = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.GroupBox1 = new System.Windows.Forms.GroupBox();
			this.caList = new ControloAutList();
			this.relacaoCA = new GISA.RelacaoControloAut();
			this.GroupBox1.SuspendLayout();
			this.SuspendLayout();
			//
			//btnAccept
			//
			this.btnAccept.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnAccept.Enabled = false;
			this.btnAccept.Location = new System.Drawing.Point(506, 436);
			this.btnAccept.Name = "btnAccept";
			this.btnAccept.TabIndex = 6;
			this.btnAccept.Text = "Aceitar";
			//
			//btnCancel
			//
			this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(586, 436);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Cancelar";
			//
			//GroupBox1
			//
			this.GroupBox1.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.GroupBox1.Controls.Add(this.caList);
			this.GroupBox1.Location = new System.Drawing.Point(7, 4);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new System.Drawing.Size(674, 258);
			this.GroupBox1.TabIndex = 1;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "Notícia de autoridade relacionada";
			//
			//caList
			//
			this.caList.AllowedNoticiaAutLocked = false;
			this.caList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.caList.DockPadding.Bottom = 6;
			this.caList.DockPadding.Left = 6;
			this.caList.DockPadding.Right = 6;
			this.caList.DockPadding.Top = 6;
			//this.caList.ListHandler = null;
			this.caList.Location = new System.Drawing.Point(3, 16);
			this.caList.Name = "caList";
			//this.caList.originalLabel = "Notícias de autoridade encontradas";
			this.caList.Size = new System.Drawing.Size(668, 239);
			this.caList.TabIndex = 1;
			//
			//relacaoCA
			//
			this.relacaoCA.ContextNivelRow = null;
			this.relacaoCA.Location = new System.Drawing.Point(5, 266);
			this.relacaoCA.Name = "relacaoCA";
			this.relacaoCA.Size = new System.Drawing.Size(678, 160);
			this.relacaoCA.TabIndex = 2;
			//
			//FormControloAutRel
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(690, 468);
			this.Controls.Add(this.relacaoCA);
			this.Controls.Add(this.GroupBox1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnAccept);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormControloAutRel";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Relação entre entidades produtoras";
			this.GroupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

		private GISADataset.ControloAutRow mContextControloAut = null;
		private GISADataset.ControloAutRow ContextControloAut
		{
			get
			{
				return mContextControloAut;
			}
		}

		public GISADataset.ControloAutRow ControloAutselecionado
		{
			get
			{
				return ControloAutDicionarioselecionado.ControloAutRow;
			}
		}

		public GISADataset.ControloAutDicionarioRow ControloAutDicionarioselecionado
		{
			get
			{
				ListViewItem li = caList.SelectedItems[0];
				Debug.Assert(li.Tag is GISADataset.ControloAutDicionarioRow);
				return (GISADataset.ControloAutDicionarioRow)li.Tag;
			}
		}

		public GISADataset.ControloAutDicionarioRow ControloAutDicionarioAutorizado
		{
			get
			{
				DataRow[] cadRows = GisaDataSetHelper.GetInstance(). ControloAutDicionario. Select("IDControloAut=" + ControloAutselecionado.ID.ToString() + " AND IDTipoControloAutForma=" + System.Enum.Format(typeof(TipoControloAutForma), TipoControloAutForma.FormaAutorizada, "D"));

				if (cadRows.Length > 0)
				{
					return (GISADataset.ControloAutDicionarioRow)(cadRows[0]);
				}
				else
				{
					return null;
				}
			}
		}

		private DataRow mContextRel;
		public DataRow ContextRel
		{
			get
			{
				return mContextRel;
			}
			set
			{
				mContextRel = value;
			}
		}


		private bool mInEditMode = false;
		public void SetEditMode()
		{
			caList.Enabled = false;
			relacaoCA.cbTipoControloAutRel.Enabled = false; // edição de relações hierarquicas para nao hierarquicas e vice-versa causaria uns quantos problemas, seria necessário remover o registo de uma tabela e adiciona-lo em outra.
			mInEditMode = true;
		}

		private void RefreshButtonState(ListViewItem item)
		{
			if (item != null && item.ListView != null)
			{
				btnAccept.Enabled = true;
			}
			else
			{
				btnAccept.Enabled = false;
			}
		}

	#region  Paginated list events code 

		private void caList_BeforeNewListSelection(object sender, BeforeNewSelectionEventArgs e)
		{
			if (e.ItemToBeSelected.ListView == null || (e.ItemToBeSelected != null && e.ItemToBeSelected.ListView == null))
			{
				relacaoCA.ContextNivelRow = null;
			}
			else
			{
				GISADataset.ControloAutRow caRow = null;
				caRow = ((GISADataset.ControloAutDicionarioRow)e.ItemToBeSelected.Tag).ControloAutRow;

				IDbConnection conn = GisaDataSetHelper.GetConnection();
				try
				{
					conn.Open();

					DBAbstractDataLayer.DataAccessRules.NivelRule.Current.LoadNivelByControloAut(caRow.ID, GisaDataSetHelper.GetInstance(), conn);
				}
				catch (Exception ex)
				{
					Trace.WriteLine(ex);
					throw;
				}
				finally
				{
					conn.Close();
				}
				relacaoCA.ContextNivelRow = caRow.GetNivelControloAutRows()[0].NivelRow;
			}

			RefreshButtonState(e.ItemToBeSelected);
		}

	#endregion

		private void btnAccept_Click(object sender, System.EventArgs e)
		{
			if (validateData())
			{
				DialogResult = System.Windows.Forms.DialogResult.OK;
				Close();
			}
		}

		private bool validateData()
		{
			GISADataset.ControloAutDicionarioRow cadRow = ControloAutDicionarioAutorizado;
			GISADataset.TipoControloAutRelRow tcarRow = null;
			tcarRow = (GISADataset.TipoControloAutRelRow)relacaoCA.cbTipoControloAutRel.SelectedItem;

			PersistencyHelper.VerifyRelExistencePreConcArguments pcArgs = new PersistencyHelper.VerifyRelExistencePreConcArguments();

			IDbConnection conn = GisaDataSetHelper.GetConnection();
			try
			{
				conn.Open();
				DBAbstractDataLayer.DataAccessRules.NivelRule.Current.LoadNivelByControloAut(cadRow.IDControloAut, GisaDataSetHelper.GetInstance(), conn);
            }
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				throw ex;
			}
			finally
			{
				conn.Close();
			}

			string errorTitle = "Erro ao estabelecer relação";

			if (! mInEditMode)
			{

				// Garantir que uma EP não é relacionada com ela própria. 
				if (cadRow.ControloAutRow.ID == ContextControloAut.ID)
				{
					MessageBox.Show("Não é permitido relacionar uma notícia de autoridade consigo própria.", errorTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}

				// validar que a relação que pretendemos criar ainda não existe
				if (GisaDataSetHelper.GetInstance().ControloAutRel.Select(string.Format("((IDControloAut={0} AND IDControloAutAlias={1}) OR (IDControloAut={1} AND IDControloAutAlias={0})) AND IDTipoRel = {2:d}", cadRow.ControloAutRow.ID, ContextControloAut.ID, tcarRow.ID)).Length > 0 || (tcarRow.ID == Convert.ToInt64(TipoControloAutRel.Hierarquica) && GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1} OR ID={1} AND IDUpper={0}", cadRow.ControloAutRow.GetNivelControloAutRows()[0].NivelRow.ID, ContextControloAut.GetNivelControloAutRows()[0].NivelRow.ID)).Length > 0))
				{

					MessageBox.Show("Não é permitida mais que uma relação do mesmo tipo " + Environment.NewLine + "entre as mesmas duas entidades produtoras.", errorTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}

				//Agir de forma diferente conforme seja uma relação hierarquica ou seja uma relação de outro qualquer tipo
				if (tcarRow.ID == Convert.ToInt64(TipoControloAutRel.Hierarquica))
				{
					// Carregar informação do Nível do CA com que se pretende estabelecer esta relação
					GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
					try
					{
						DBAbstractDataLayer.DataAccessRules.FRDRule.Current.LoadRetrieveSelectionData(GisaDataSetHelper.GetInstance(), cadRow.IDControloAut, ho.Connection);
                        DBAbstractDataLayer.DataAccessRules.FRDRule.Current.LoadFRD(GisaDataSetHelper.GetInstance(), ContextControloAut.GetNivelControloAutRows()[0].NivelRow.ID, ho.Connection);
					}
					finally
					{
						ho.Dispose();
					}

					long tnrID = 0;
					tnrID = ((TipoNivelRelacionado.PossibleSubNivel)relacaoCA.cbTipoNivel.SelectedItem).SubIDTipoNivelRelacionado;
					GISADataset.RelacaoHierarquicaRow rhRow = null;
					rhRow = GisaDataSetHelper.GetInstance().RelacaoHierarquica.NewRelacaoHierarquicaRow();
					rhRow.IDUpper = cadRow.ControloAutRow.GetNivelControloAutRows()[0].NivelRow.ID;
					rhRow.ID = ContextControloAut.GetNivelControloAutRows()[0].NivelRow.ID;
					rhRow.IDTipoNivelRelacionado = tnrID;
					// Guardar datas
					rhRow.InicioAno = GISA.Utils.GUIHelper.ReadYear(relacaoCA.dtRelacaoInicio.ValueYear);
					rhRow.InicioMes = GISA.Utils.GUIHelper.ReadMonth(relacaoCA.dtRelacaoInicio.ValueMonth);
					rhRow.InicioDia = GISA.Utils.GUIHelper.ReadDay(relacaoCA.dtRelacaoInicio.ValueDay);
					rhRow.FimAno = GISA.Utils.GUIHelper.ReadYear(relacaoCA.dtRelacaoFim.ValueYear);
					rhRow.FimMes = GISA.Utils.GUIHelper.ReadMonth(relacaoCA.dtRelacaoFim.ValueMonth);
					rhRow.FimDia = GISA.Utils.GUIHelper.ReadDay(relacaoCA.dtRelacaoFim.ValueDay);

					rhRow.Descricao = relacaoCA.txtDescricao.Text;
                    //rhRow.isDeleted = 0;
					GisaDataSetHelper.GetInstance().RelacaoHierarquica.AddRelacaoHierarquicaRow(rhRow);

					pcArgs.ID = rhRow.ID;
					pcArgs.IDUpper = rhRow.IDUpper;
					pcArgs.IDTipoRel = rhRow.IDTipoNivelRelacionado;
					pcArgs.isCARRow = false;

                    // actualizar permissões implícitas
                    var postSaveAction = new PostSaveAction();
                    PersistencyHelper.UpdatePermissionsPostSaveArguments args = new PersistencyHelper.UpdatePermissionsPostSaveArguments();
                    postSaveAction.args = args;

                    postSaveAction.postSaveDelegate = delegate(PersistencyHelper.PostSaveArguments postSaveArgs)
                    {
                        if (!postSaveArgs.cancelAction && ContextControloAut.RowState != DataRowState.Detached && pcArgs.CreateEditResult == PersistencyHelper.VerifyRelExistencePreConcArguments.CreateEditRelationErrors.NoError)
                        {
                            if (this.parent.GetType() == typeof(MasterPanelControloAut))
                            {
                                ((MasterPanelControloAut)this.parent).CurrentContext.RaiseRegisterModificationEvent(this.ContextControloAut);

                                var caRegRow = GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao.Cast<GISADataset.ControloAutDataDeDescricaoRow>().Single(r => r.RowState == DataRowState.Added);

                                PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao,
                                    new GISADataset.ControloAutDataDeDescricaoRow[] { caRegRow }, postSaveArgs.tran);

                                var frdRow = rhRow.NivelRowByNivelRelacaoHierarquica.GetFRDBaseRows().SingleOrDefault();

                                if (frdRow == null)
                                {
                                    var tipoFRD = (GISADataset.TipoFRDBaseRow)(GisaDataSetHelper.GetInstance().TipoFRDBase.Select("ID=" + DomainValuesHelper.stringifyEnumValue(TipoFRDBase.FRDOIRecolha))[0]);
                                    frdRow = GisaDataSetHelper.GetInstance().FRDBase.AddFRDBaseRow(rhRow.NivelRowByNivelRelacaoHierarquica, tipoFRD, "", "", new byte[] { }, 0);
                                    var dp = GisaDataSetHelper.GetInstance().SFRDDatasProducao.AddSFRDDatasProducaoRow(frdRow, "", "", "", "", false, "", "", "", "", false, new byte[] { }, 0);
                                    var ce = GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura.AddSFRDConteudoEEstruturaRow(frdRow, "", "", new byte[] { }, 0);
                                    var c = GisaDataSetHelper.GetInstance().SFRDContexto.AddSFRDContextoRow(frdRow, "", "", "", false, new byte[] { }, 0);
                                    var da = GisaDataSetHelper.GetInstance().SFRDDocumentacaoAssociada.AddSFRDDocumentacaoAssociadaRow(frdRow, "", "", "", "", new byte[] { }, 0);
                                    var ds = GisaDataSetHelper.GetInstance().SFRDDimensaoSuporte.AddSFRDDimensaoSuporteRow(frdRow, "", new byte[] { }, 0);
                                    var ng = GisaDataSetHelper.GetInstance().SFRDNotaGeral.AddSFRDNotaGeralRow(frdRow, "", new byte[] { }, 0);
                                    var CurrentSFRDAvaliacao = GisaDataSetHelper.GetInstance().SFRDAvaliacao.NewSFRDAvaliacaoRow();
                                    CurrentSFRDAvaliacao.FRDBaseRow = frdRow;
                                    CurrentSFRDAvaliacao.IDPertinencia = 1;
                                    CurrentSFRDAvaliacao.IDDensidade = 1;
                                    CurrentSFRDAvaliacao.IDSubdensidade = 1;
                                    CurrentSFRDAvaliacao.Publicar = false;
                                    CurrentSFRDAvaliacao.Observacoes = "";
                                    CurrentSFRDAvaliacao.AvaliacaoTabela = false;
                                    GisaDataSetHelper.GetInstance().SFRDAvaliacao.AddSFRDAvaliacaoRow(CurrentSFRDAvaliacao);
                                    var condA = GisaDataSetHelper.GetInstance().SFRDCondicaoDeAcesso.AddSFRDCondicaoDeAcessoRow(frdRow, "", "", "", "", new byte[] { }, 0);

                                    PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().FRDBase, new DataRow[] { frdRow }, postSaveArgs.tran);
                                    PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().SFRDDatasProducao, new DataRow[] { dp }, postSaveArgs.tran);
                                    PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().SFRDConteudoEEstrutura, new DataRow[] { ce }, postSaveArgs.tran);
                                    PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().SFRDContexto, new DataRow[] { c }, postSaveArgs.tran);
                                    PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().SFRDDocumentacaoAssociada, new DataRow[] { da }, postSaveArgs.tran);
                                    PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().SFRDDimensaoSuporte, new DataRow[] { ds }, postSaveArgs.tran);
                                    PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().SFRDNotaGeral, new DataRow[] { ng }, postSaveArgs.tran);
                                    PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().SFRDAvaliacao, new DataRow[] { CurrentSFRDAvaliacao }, postSaveArgs.tran);
                                    PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().SFRDCondicaoDeAcesso, new DataRow[] { condA }, postSaveArgs.tran);
                                }

                                var nvlRegRow = RecordRegisterHelper
                                        .CreateFRDBaseDataDeDescricaoRow(frdRow,
                                            caRegRow.TrusteeUserRowByTrusteeUserControloAutDataDeDescricao,
                                            caRegRow.TrusteeUserRowByTrusteeUserControloAutDataDeDescricaoAuthority,
                                            caRegRow.DataAutoria);
                                nvlRegRow.DataEdicao = caRegRow.DataEdicao;
                                nvlRegRow.IDTipoNivelRelacionado = rhRow.IDTipoNivelRelacionado;

                                GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao.AddFRDBaseDataDeDescricaoRow(nvlRegRow);

                                PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao,
                                    GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao.Cast<GISADataset.FRDBaseDataDeDescricaoRow>().Where(r => r.RowState == DataRowState.Added).ToArray(), postSaveArgs.tran);
                            }
                        }
                    };

                    PersistencyHelper.save(ValidateCreateRel, pcArgs, postSaveAction);
					PersistencyHelper.cleanDeletedData();

					if (ContextControloAut.RowState == DataRowState.Detached)
					{
						MessageBox.Show("Não é possível estabelecer a relação uma vez que a notícia de autoridade " + System.Environment.NewLine + "selecionada no painel superior foi apagada por outro utilizador.", "Erro ao estabelecer relação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return false;
					}
					else
					{
						if (pcArgs.CreateEditResult == PersistencyHelper.VerifyRelExistencePreConcArguments.CreateEditRelationErrors.RelationAlreadyExists)
						{
							MessageBox.Show("Não é permitida mais que uma relação do mesmo tipo entre as mesmas duas entidades produtoras.", "Erro ao estabelecer relação", MessageBoxButtons.OK, MessageBoxIcon.Warning);

							return false;
						}
						else if (pcArgs.CreateEditResult == PersistencyHelper.VerifyRelExistencePreConcArguments.CreateEditRelationErrors.CyclicRelation)
						{
							MessageBox.Show("Prestes a criar um conjunto de relações cíclicas.", "Erro ao estabelecer relação", MessageBoxButtons.OK, MessageBoxIcon.Warning);

							return false;
						}
                        else if (pcArgs.CreateEditResult == PersistencyHelper.VerifyRelExistencePreConcArguments.CreateEditRelationErrors.CADeleted)
                        {
                            MessageBox.Show("Não é possível estabelecer a relação uma vez que uma das notícias de autoridade " + System.Environment.NewLine + "foi apagada por outro utilizador.", "Erro ao estabelecer relação", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            return false;
                        }
                        else
                        {
                            GISA.Search.Updater.updateProdutor(new List<string>() { 
                                cadRow.ControloAutRow.ID.ToString(), ContextControloAut.ID.ToString()});
                            GISA.Search.Updater.updateNivelDocumentalComProdutores(cadRow.ControloAutRow.GetNivelControloAutRows()[0].ID);
                        }
					}
				}
				else
				{
					GISADataset.ControloAutRelRow carRow = null;
					carRow = GisaDataSetHelper.GetInstance().ControloAutRel.NewControloAutRelRow();
					carRow.IDControloAut = cadRow.ControloAutRow.ID;
					carRow.IDControloAutAlias = ContextControloAut.ID;
					carRow.IDTipoRel = tcarRow.ID;
					// Guardar datas
					carRow.InicioAno = GISA.Utils.GUIHelper.ReadYear(relacaoCA.dtRelacaoInicio.ValueYear);
					carRow.InicioMes = GISA.Utils.GUIHelper.ReadMonth(relacaoCA.dtRelacaoInicio.ValueMonth);
					carRow.InicioDia = GISA.Utils.GUIHelper.ReadDay(relacaoCA.dtRelacaoInicio.ValueDay);
					carRow.FimAno = GISA.Utils.GUIHelper.ReadYear(relacaoCA.dtRelacaoFim.ValueYear);
					carRow.FimMes = GISA.Utils.GUIHelper.ReadMonth(relacaoCA.dtRelacaoFim.ValueMonth);
					carRow.FimDia = GISA.Utils.GUIHelper.ReadDay(relacaoCA.dtRelacaoFim.ValueDay);

					carRow.Descricao = relacaoCA.txtDescricao.Text;
					GisaDataSetHelper.GetInstance().ControloAutRel.AddControloAutRelRow(carRow);

					pcArgs.ID = carRow.IDControloAut;
					pcArgs.IDUpper = carRow.IDControloAutAlias;
					pcArgs.IDTipoRel = carRow.IDTipoRel;
					pcArgs.isCARRow = true;

                    var postSaveAction = new PostSaveAction();
                    PersistencyHelper.UpdatePermissionsPostSaveArguments args = new PersistencyHelper.UpdatePermissionsPostSaveArguments();
                    postSaveAction.args = args;

                    postSaveAction.postSaveDelegate = delegate(PersistencyHelper.PostSaveArguments postSaveArgs)
                    {
                        if (!postSaveArgs.cancelAction && ContextControloAut.RowState != DataRowState.Detached && pcArgs.CreateEditResult == PersistencyHelper.VerifyRelExistencePreConcArguments.CreateEditRelationErrors.NoError)
                        {
                            if (this.parent.GetType() == typeof(MasterPanelControloAut))
                            {
                                ((MasterPanelControloAut)this.parent).CurrentContext.RaiseRegisterModificationEvent(this.ContextControloAut);

                                PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao,
                                        GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao.Cast<GISADataset.ControloAutDataDeDescricaoRow>().Where(r => r.RowState == DataRowState.Added).ToArray(), postSaveArgs.tran);
                            }
                        }
                    };

					PersistencyHelper.save(ValidateCreateRel, pcArgs, postSaveAction);
					PersistencyHelper.cleanDeletedData();

					if (ContextControloAut.RowState == DataRowState.Detached)
					{
						MessageBox.Show("Não é possível estabelecer a relação uma vez que a notícia de autoridade " + System.Environment.NewLine + "selecionada no painel superior foi apagada por outro utilizador.", "Erro ao estabelecer relação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return false;
					}
					else
					{
                        if (pcArgs.CreateEditResult == PersistencyHelper.VerifyRelExistencePreConcArguments.CreateEditRelationErrors.RelationAlreadyExists)
                        {
                            MessageBox.Show("Não é permitida mais que uma relação do mesmo tipo entre as mesmas duas entidades produtoras.", "Erro ao estabelecer relação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                        else
                        {
                            GISA.Search.Updater.updateProdutor(new List<string>() { 
                                carRow.IDControloAut.ToString(), carRow.IDControloAutAlias.ToString() });
                        }
					}
				}
			}
			else // edição
			{
				//Agir de forma diferente conforme seja uma relação hierarquica ou seja uma relação de outro qualquer tipo
				if (tcarRow.ID == Convert.ToInt64(TipoControloAutRel.Hierarquica))
				{
					long tnrID = 0;
					tnrID = ((TipoNivelRelacionado.PossibleSubNivel)relacaoCA.cbTipoNivel.SelectedItem).SubIDTipoNivelRelacionado;
					GISADataset.RelacaoHierarquicaRow rhRow = null;
					rhRow = (GISADataset.RelacaoHierarquicaRow)ContextRel;
					// Guardar tipo de nivel
					rhRow.IDTipoNivelRelacionado = tnrID;
					// Guardar datas
					rhRow.InicioAno = GISA.Utils.GUIHelper.ReadYear(relacaoCA.dtRelacaoInicio.ValueYear);
					rhRow.InicioMes = GISA.Utils.GUIHelper.ReadMonth(relacaoCA.dtRelacaoInicio.ValueMonth);
					rhRow.InicioDia = GISA.Utils.GUIHelper.ReadDay(relacaoCA.dtRelacaoInicio.ValueDay);
					rhRow.FimAno = GISA.Utils.GUIHelper.ReadYear(relacaoCA.dtRelacaoFim.ValueYear);
					rhRow.FimMes = GISA.Utils.GUIHelper.ReadMonth(relacaoCA.dtRelacaoFim.ValueMonth);
					rhRow.FimDia = GISA.Utils.GUIHelper.ReadDay(relacaoCA.dtRelacaoFim.ValueDay);
					// Guardar descrição
					rhRow.Descricao = relacaoCA.txtDescricao.Text;

                    var postSaveAction = new PostSaveAction();
                    PersistencyHelper.UpdatePermissionsPostSaveArguments args = new PersistencyHelper.UpdatePermissionsPostSaveArguments();
                    postSaveAction.args = args;

                    postSaveAction.postSaveDelegate = delegate(PersistencyHelper.PostSaveArguments postSaveArgs)
                    {
                        if (!postSaveArgs.cancelAction && ContextControloAut.RowState != DataRowState.Detached && pcArgs.CreateEditResult == PersistencyHelper.VerifyRelExistencePreConcArguments.CreateEditRelationErrors.NoError)
                        {
                            if (this.parent.GetType() == typeof(MasterPanelControloAut))
                            {
                                ((MasterPanelControloAut)this.parent).CurrentContext.RaiseRegisterModificationEvent(this.ContextControloAut);

                                PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao,
                                        GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao.Cast<GISADataset.ControloAutDataDeDescricaoRow>().Where(r => r.RowState == DataRowState.Added).ToArray(), postSaveArgs.tran);
                            }
                        }
                    };

                    PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save(postSaveAction);
				    PersistencyHelper.cleanDeletedData();

                    if (successfulSave == PersistencyHelper.SaveResult.successful)
                        UpdateCA(rhRow);
				}
				else
				{
					GISADataset.ControloAutRelRow carRow = null;
					carRow = (GISADataset.ControloAutRelRow)ContextRel;

					// Actualização de uma relação previamente existente
					carRow["IDTipoRel"] = ((GISADataset.TipoControloAutRelRow)relacaoCA.cbTipoControloAutRel.SelectedItem).ID;
					// guardar datas
					carRow.InicioAno = GISA.Utils.GUIHelper.ReadYear(relacaoCA.dtRelacaoInicio.ValueYear);
					carRow.InicioMes = GISA.Utils.GUIHelper.ReadMonth(relacaoCA.dtRelacaoInicio.ValueMonth);
					carRow.InicioDia = GISA.Utils.GUIHelper.ReadDay(relacaoCA.dtRelacaoInicio.ValueDay);
					carRow.FimAno = GISA.Utils.GUIHelper.ReadYear(relacaoCA.dtRelacaoFim.ValueYear);
					carRow.FimMes = GISA.Utils.GUIHelper.ReadMonth(relacaoCA.dtRelacaoFim.ValueMonth);
					carRow.FimDia = GISA.Utils.GUIHelper.ReadDay(relacaoCA.dtRelacaoFim.ValueDay);

					carRow.Descricao = relacaoCA.txtDescricao.Text;

                    var postSaveAction = new PostSaveAction();
                    PersistencyHelper.UpdatePermissionsPostSaveArguments args = new PersistencyHelper.UpdatePermissionsPostSaveArguments();
                    postSaveAction.args = args;

                    postSaveAction.postSaveDelegate = delegate(PersistencyHelper.PostSaveArguments postSaveArgs)
                    {
                        if (!postSaveArgs.cancelAction && ContextControloAut.RowState != DataRowState.Detached && pcArgs.CreateEditResult == PersistencyHelper.VerifyRelExistencePreConcArguments.CreateEditRelationErrors.NoError)
                        {
                            if (this.parent.GetType() == typeof(MasterPanelControloAut))
                            {
                                ((MasterPanelControloAut)this.parent).CurrentContext.RaiseRegisterModificationEvent(this.ContextControloAut);

                                PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao,
                                        GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao.Cast<GISADataset.ControloAutDataDeDescricaoRow>().Where(r => r.RowState == DataRowState.Added).ToArray(), postSaveArgs.tran);
                            }
                        }
                    };

                    PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save(postSaveAction);
				    PersistencyHelper.cleanDeletedData();

                    if (successfulSave == PersistencyHelper.SaveResult.successful)
                        UpdateCA(carRow);
				}

			}

			//TODO: PARA ALGUMAS DAS SEGUINTES VALIDACOES SERÁ NECESSÁRIO TRAZER PARA DENTRO DESTE FORM UMA GRANDE PARTE DA LOGICA EXISTENTE FORA DELE, NOMEADAMENTE A NOÇÃO DE SE ESTE FORM ESTÁ A SER UTILIZADO NA CRIAÇÃO OU NA EDIÇÃO DE UMA DADA RELAÇÃO
			//ToDo: validar que as datas da relação não vão para alem das datas de existencia de ambos os níveis (superior e inferior)
			//ToDo: validar que o tipo de nivel escolhido está de acordo com os tipos de nivel subordinados
			return true;
		}

        private void UpdateCA(object rel)
        {
            GISADataset.ControloAutRelRow carRow = null;
            GISADataset.RelacaoHierarquicaRow rhRow = null;

            if (rel.GetType() == typeof(GISADataset.ControloAutRelRow))
            {
                carRow = (GISADataset.ControloAutRelRow)rel;
                GISA.Search.Updater.updateProdutor(new List<string>() { 
                    carRow.IDControloAut.ToString(), carRow.IDControloAutAlias.ToString() });
            }
            else
            {
                rhRow = (GISADataset.RelacaoHierarquicaRow)rel;
                GISA.Search.Updater.updateProdutor(rhRow.NivelRowByNivelRelacaoHierarquica.GetNivelControloAutRows()[0].IDControloAut);
                GISA.Search.Updater.updateProdutor(rhRow.NivelRowByNivelRelacaoHierarquicaUpper.GetNivelControloAutRows()[0].IDControloAut);
            }
        }

		private void ValidateCreateRel(PersistencyHelper.PreConcArguments args)
		{
			PersistencyHelper.VerifyRelExistencePreConcArguments vrePsa = null;
			vrePsa = (PersistencyHelper.VerifyRelExistencePreConcArguments)args;

			DataRow[] newRelRow = null;

			if (vrePsa.isCARRow)
			{
				newRelRow = GisaDataSetHelper.GetInstance().ControloAutRel.Select(string.Format("((IDControloAut={0} AND IDControloAutAlias={1}) OR (IDControloAut={1} AND IDControloAutAlias={0})) AND IDTipoRel = {2}", vrePsa.ID, vrePsa.IDUpper, vrePsa.IDTipoRel));
			}
			else
			{
				newRelRow = GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1} AND IDTipoNivelRelacionado = {2}", vrePsa.ID, vrePsa.IDUpper, vrePsa.IDTipoRel));
			}

			int result = ControloAutRule.Current.ExistsRel(vrePsa.ID, vrePsa.IDUpper, vrePsa.IDTipoRel, vrePsa.isCARRow, vrePsa.tran);
			switch (result)
			{
				case 0:
					vrePsa.CreateEditResult = PersistencyHelper.VerifyRelExistencePreConcArguments.CreateEditRelationErrors.NoError;
					break;
				case 1:
					vrePsa.CreateEditResult = PersistencyHelper.VerifyRelExistencePreConcArguments.CreateEditRelationErrors.RelationAlreadyExists;
					break;
				case 2:
					vrePsa.CreateEditResult = PersistencyHelper.VerifyRelExistencePreConcArguments.CreateEditRelationErrors.CyclicRelation;
					break;
				case 3:
					vrePsa.CreateEditResult = PersistencyHelper.VerifyRelExistencePreConcArguments.CreateEditRelationErrors.CADeleted;
					break;
			}

            if (!(result == Convert.ToInt32(PersistencyHelper.VerifyRelExistencePreConcArguments.CreateEditRelationErrors.NoError)) && newRelRow.Length > 0)
            {
                newRelRow[0].RejectChanges();
            }
            else if (newRelRow.Length > 0)
            {
                var rhRow = newRelRow[0] as GISADataset.RelacaoHierarquicaRow;
                if (rhRow == null) return;

                PermissoesHelper.AddNivelGrantPermissions(rhRow.NivelRowByNivelRelacaoHierarquica, rhRow.NivelRowByNivelRelacaoHierarquicaUpper, vrePsa.tran);
            }
		}		
	}
} //end of root namespace