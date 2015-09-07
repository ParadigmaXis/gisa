using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using System.Collections.Generic;
using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

namespace GISA
{
	public class FRDControloAutoridade : GISA.MultiPanelControl
	{

		// default value. shadowed in derived classes
		public static Bitmap FunctionImage
		{
			get
			{
				return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "Conteudo_enabled_32x32.png");
			}
		}

	#region  Windows Form Designer generated code 

		public FRDControloAutoridade() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            base.ParentChanged += FRDControloAutoridade_ParentChanged;

//Nitro TODO: INSTANT C# TODO TASK: The return type of the tempWith1 variable must be corrected.
//ORIGINAL LINE: With DropDownTreeView1.Nodes.Add("1. Identificação")
			TreeNode tempWith1 = DropDownTreeView1.Nodes.Add("1. Identificação");
			tempWith1.Tag = PanelCAIdentificacao1;

////Nitro TODO: INSTANT C# TODO TASK: The return type of the tempWith2 variable must be corrected.
//ORIGINAL LINE: With DropDownTreeView1.Nodes.Add("2. Descrição")
            TreeNode tempWith2 = DropDownTreeView1.Nodes.Add("2. Descrição");
			tempWith2.Tag = PanelCADescricao1;

//Nitro TODO: INSTANT C# TODO TASK: The return type of the tempWith3 variable must be corrected.
//ORIGINAL LINE: With DropDownTreeView1.Nodes.Add("3. Relações")
            TreeNode tempWith3 = DropDownTreeView1.Nodes.Add("3. Relações");
			tempWith3.Tag = PanelCARelacoes1;

//Nitro TODO: INSTANT C# TODO TASK: The return type of the tempWith4 variable must be corrected.
//ORIGINAL LINE: With DropDownTreeView1.Nodes.Add("4. Controlo de descrição")
            TreeNode tempWith4 = DropDownTreeView1.Nodes.Add("4. Controlo de descrição");
			tempWith4.Tag = PanelCAControlo1;
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
		internal GISA.PanelCAControlo PanelCAControlo1;
		internal GISA.PanelCADescricao PanelCADescricao1;
		internal GISA.PanelCARelacoes PanelCARelacoes1;
		internal GISA.PanelCAIdentificacao PanelCAIdentificacao1;
		internal GISA.PanelMensagem PanelMensagem1;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.PanelCAControlo1 = new GISA.PanelCAControlo();
			this.PanelCADescricao1 = new GISA.PanelCADescricao();
			this.PanelCARelacoes1 = new GISA.PanelCARelacoes();
			this.PanelCAIdentificacao1 = new GISA.PanelCAIdentificacao();
			this.PanelMensagem1 = new PanelMensagem();
			this.SuspendLayout();
			//
			//DropDownTreeView1
			//
			this.DropDownTreeView1.GISAFunction = "(Controlo de Autoridade)";
			this.DropDownTreeView1.Name = "DropDownTreeView1";
			//
			//ToolBar
			//
			this.ToolBar.Name = "ToolBar";
			//
			//PanelCAControlo1
			//
			this.PanelCAControlo1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelCAControlo1.Location = new System.Drawing.Point(0, 49);
			this.PanelCAControlo1.Name = "PanelCAControlo1";
			this.PanelCAControlo1.Size = new System.Drawing.Size(688, 366);
			this.PanelCAControlo1.TabIndex = 11;
			this.PanelCAControlo1.Visible = false;
			//
			//PanelCADescricao1
			//
			this.PanelCADescricao1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelCADescricao1.Location = new System.Drawing.Point(0, 49);
			this.PanelCADescricao1.Name = "PanelCADescricao1";
			this.PanelCADescricao1.Size = new System.Drawing.Size(688, 366);
			this.PanelCADescricao1.TabIndex = 10;
			this.PanelCADescricao1.Visible = false;
			//
			//PanelCARelacoes1
			//
			this.PanelCARelacoes1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelCARelacoes1.Location = new System.Drawing.Point(0, 49);
			this.PanelCARelacoes1.Name = "PanelCARelacoes1";
			this.PanelCARelacoes1.Size = new System.Drawing.Size(688, 366);
			this.PanelCARelacoes1.TabIndex = 9;
			this.PanelCARelacoes1.Visible = false;
			//
			//PanelCAIdentificacao1
			//
			this.PanelCAIdentificacao1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelCAIdentificacao1.Location = new System.Drawing.Point(0, 49);
			this.PanelCAIdentificacao1.Name = "PanelCAIdentificacao1";
			this.PanelCAIdentificacao1.Size = new System.Drawing.Size(688, 366);
			this.PanelCAIdentificacao1.TabIndex = 8;
            //
            //PanelMensagem1
            //
            this.PanelMensagem1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMensagem1.Location = new System.Drawing.Point(0, 49);
            this.PanelMensagem1.Name = "PanelMensagem1";
            this.PanelMensagem1.Size = new System.Drawing.Size(688, 366);
            this.PanelMensagem1.TabIndex = 20;
			//
			//FRDControloAutoridade
			//
			this.GisaPanelScroller.Controls.Add(this.PanelCAIdentificacao1);
			this.GisaPanelScroller.Controls.Add(this.PanelCARelacoes1);
			this.GisaPanelScroller.Controls.Add(this.PanelCADescricao1);
			this.GisaPanelScroller.Controls.Add(this.PanelCAControlo1);
			this.GisaPanelScroller.Controls.Add(this.PanelMensagem1);
			this.Name = "FRDControloAutoridade";
			this.Size = new System.Drawing.Size(688, 415);
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

		private GISADataset.ControloAutRow CurrentControloAut;
		private bool isLoaded = false;
		public override void LoadData()
		{
			try
			{
				((frmMain)TopLevelControl).EnterWaitMode();
				GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
				try
				{
					if (! isLoaded)
					{
						if (CurrentContext.ControloAutDicionario == null)
						{
							CurrentControloAut = null;
							return;
						}

						CurrentControloAut = CurrentContext.ControloAutDicionario.ControloAutRow;

						GisaDataSetHelper.ManageDatasetConstraints(false);

						// Recarregar o próprio registo de autoridade para despistar 
						// o caso em que alguem o possa entretanto já ter eliminado
						ControloAutRule.Current.LoadControloAut(GisaDataSetHelper.GetInstance(), CurrentContext.ControloAutDicionario.IDControloAut, ho.Connection);

						if (CurrentControloAut == null || CurrentControloAut.RowState == DataRowState.Detached)
						{
							return;
						}

						ControloAutRule.Current.LoadControloAutData(GisaDataSetHelper.GetInstance(), CurrentContext.ControloAutDicionario.IDControloAut, ho.Connection);

						GisaDataSetHelper.ManageDatasetConstraints(true);
						isLoaded = true;
					}

					GISAPanel selectedPanel = (GISAPanel)this.DropDownTreeView1.SelectedNode.Tag;
					if (! selectedPanel.IsLoaded)
					{
						GisaDataSetHelper.ManageDatasetConstraints(false);
						long startTicks = 0;
						startTicks = DateTime.Now.Ticks;
						selectedPanel.LoadData(CurrentContext.ControloAutDicionario.ControloAutRow, ho.Connection);
						Debug.WriteLine("Time dispend loading " + selectedPanel.ToString() + ": " + new TimeSpan(DateTime.Now.Ticks - startTicks).ToString());
						GisaDataSetHelper.ManageDatasetConstraints(true);
					}
				}
				catch (System.Data.ConstraintException Ex)
				{
					Trace.WriteLine(Ex);
					GisaDataSetHelper.FixDataSet(GisaDataSetHelper.GetInstance(), ho.Connection);
				}
				finally
				{
					ho.Dispose();
				}
			}
			finally
			{
				((frmMain)TopLevelControl).LeaveWaitMode();
			}
		}

		public override void ModelToView()
		{
			try
			{
				((frmMain)TopLevelControl).EnterWaitMode();

				// se nao existir um contexto definido os slavepanels não devem ser apresentados
				if (CurrentControloAut == null || CurrentControloAut.RowState == DataRowState.Detached)
				{

					this.Visible = false;
					return;
				}

				this.Visible = true;
				GISAPanel selectedPanel = (GISAPanel)this.DropDownTreeView1.SelectedNode.Tag;
				if (selectedPanel.IsLoaded && ! selectedPanel.IsPopulated)
				{
					long startTicks = 0;
					startTicks = DateTime.Now.Ticks;
					selectedPanel.ModelToView();
					Debug.WriteLine("Time dispend model to view " + selectedPanel.ToString() + ": " + new TimeSpan(DateTime.Now.Ticks - startTicks).ToString());
				}

				try
				{
					GisaPrincipalPermission tempWith1 = new GisaPrincipalPermission(SessionHelper.GetGisaPrincipal(), this.GetType().FullName, GisaPrincipalPermission.WRITE);
					tempWith1.Demand();
				}
				catch (System.Security.SecurityException)
				{
                    GUIHelper.GUIHelper.makeReadOnly(this.GisaPanelScroller);
				}
			}
			finally
			{
				((frmMain)TopLevelControl).LeaveWaitMode();
			}
		}

        private GISADataset.TrusteeUserRow GetSelectedAuthor()
        {
            GISADataset.TrusteeUserRow tuAuthorRow = null;
            // pode não existir selecção, nesse caso a datarow encontrada foi uma "extra" adicionada e estará detached

            if (PanelCAControlo1 != null &&
                PanelCAControlo1.IsLoaded &&
                PanelCAControlo1.ControloRevisoes1.ControloAutores1.SelectedAutor != null &&
                !(((DataRow)PanelCAControlo1.ControloRevisoes1.ControloAutores1.SelectedAutor).RowState == DataRowState.Detached) &&
                ((GISADataset.TrusteeRow)(PanelCAControlo1.ControloRevisoes1.ControloAutores1.SelectedAutor)).GetTrusteeUserRows().Length > 0)

                tuAuthorRow = ((GISADataset.TrusteeRow)PanelCAControlo1.ControloRevisoes1.ControloAutores1.SelectedAutor).GetTrusteeUserRows()[0];
            else if (SessionHelper.GetGisaPrincipal().TrusteeUserAuthor != null && !(SessionHelper.GetGisaPrincipal().TrusteeUserAuthor.RowState == DataRowState.Detached))
                tuAuthorRow = SessionHelper.GetGisaPrincipal().TrusteeUserAuthor;

            return tuAuthorRow;
        }

        private void AddRegistration(GISADataset.ControloAutRow caRow, bool existsModifiedData)
        {
            GISADataset.TrusteeUserRow tuAuthor = GetSelectedAuthor();
            GISADataset.TrusteeUserRow tuOperator = SessionHelper.GetGisaPrincipal().TrusteeUserOperator;
            DateTime data;
            if (PanelCAControlo1 != null)
                data = PanelCAControlo1.ControloRevisoes1.dtpRecolha.Value;
            else
                data = DateTime.Now;

            GISA.Model.RecordRegisterHelper.RegisterRecordModificationCA(caRow, existsModifiedData, tuOperator, tuAuthor, data);
        }

		public override bool ViewToModel()
		{
			base.ViewToModel();

            AddRegistration(CurrentControloAut, existsModifiedData);
			return true;
		}

		public override void Deactivate()
		{
			DeactivatePanels();
			isLoaded = false;
			CurrentControloAut = null;
			existsModifiedData = false;
		}


        public override PersistencyHelper.SaveResult Save()
		{
			return Save(false);
		}

        public override PersistencyHelper.SaveResult Save(bool activateOpcaoCancelar)
		{
            PersistencyHelper.SaveResult successfulSave = base.Save(activateOpcaoCancelar);
            if (successfulSave == PersistencyHelper.SaveResult.successful)
			{
				GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                try
                {
                    List<string> IDNiveis = ControloAutRule.Current.GetNiveisDocAssociados(CurrentControloAut.ID, ho.Connection);
                    GISA.Search.Updater.updateNivelDocumental(IDNiveis);
                    switch (CurrentControloAut.IDTipoNoticiaAut)
                    {
                        case (long)TipoNoticiaAut.EntidadeProdutora:
                            GISA.Search.Updater.updateProdutor(CurrentControloAut.ID);
                            GISA.Search.Updater.updateNivelDocumentalComProdutores(CurrentControloAut.GetNivelControloAutRows()[0].ID);
                            break;
                        case (long)TipoNoticiaAut.Onomastico:
                        case (long)TipoNoticiaAut.Ideografico:
                        case (long)TipoNoticiaAut.ToponimicoGeografico:
                            GISA.Search.Updater.updateAssunto(CurrentControloAut.ID);
                            break;
                        case (long)TipoNoticiaAut.TipologiaInformacional:
                            GISA.Search.Updater.updateTipologia(CurrentControloAut.ID);
                            break;
                    }
                }
                catch (GISA.Search.UpdateServerException)
                { }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.ToString());
                    throw;
                }
				finally
				{
					ho.Dispose();
				}
			}
            return successfulSave;
		}

		protected override bool isInnerContextValid()
		{
			return CurrentControloAut != null && ! (CurrentControloAut.RowState == DataRowState.Detached) && CurrentControloAut.isDeleted == 0;
		}

		protected override bool isOuterContextValid()
		{
			return CurrentContext.ControloAutDicionario != null;
		}

		protected override bool isOuterContextDeleted()
		{
			Debug.Assert(CurrentContext.ControloAutDicionario != null, "CurrentContext.ControloAutDicionario Is Nothing");
			return CurrentContext.ControloAutDicionario.RowState == DataRowState.Detached;
		}

		protected override void addContextChangeHandlers()
		{
			CurrentContext.ControloAutChanged += this.Recontextualize;
            CurrentContext.AddRevisionEvent += RegisterModification;
		}

		protected override void removeContextChangeHandlers()
		{
			CurrentContext.ControloAutChanged -= this.Recontextualize;
            CurrentContext.AddRevisionEvent -= RegisterModification;
		}

		protected override void RegisterModification(object o, RegisterModificationEventArgs e)
		{
            // apesar de de ter ocorrido um save, só se indica que houve alteraçõs nos dados para
            // ser adicionado o resgisto somente quando o contexto deixar de ser o selecionado
            AddRegistration((GISADataset.ControloAutRow)e.Context, true);
		}

		private void FRDControloAutoridade_ParentChanged(object sender, System.EventArgs e)
		{
			if (! (TopLevelControl is frmMain))
			{
				return;
			}
			frmMain main = (frmMain)TopLevelControl;
			LumiSoft.UI.Controls.WOutlookBar.WOutlookBar ob = (LumiSoft.UI.Controls.WOutlookBar.WOutlookBar)(main.PanelOutlookBar.Controls[0]);

            var tfRow = (GISADataset.TipoFunctionRow)ob.StuckenItem.Tag;			
			switch (tfRow.idx)
			{
				case 1: // Entidade detentora
						// Do nothing
					PanelCAIdentificacao1.VisibleEPExtensions = true;
					break;
				case 2:
                    // Drop area 2, area 3
                    DropUnusedAreas();

                    PanelCAIdentificacao1.VisibleOnomasticoExtensions = true;

                    break;
				case 3: // Conteúdos e Tipologia Informacional
					// Drop area 2, area 3
                    DropUnusedAreas();

                    if (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsLicObrEnable())
                        PanelCAIdentificacao1.VisibleTipExtensions = true;

                    break;						
			}
		}

        // Drop area 2, area 3
        private void DropUnusedAreas()
        {
            DropDownTreeView1.Nodes.RemoveAt(1);
            this.Controls.Remove(this.PanelCADescricao1);
            DropDownTreeView1.Nodes.RemoveAt(1);
            this.Controls.Remove(this.PanelCARelacoes1);

            // Remover os campos 4.1, 4.2 e "esticar" o campo 4.3 por forma a ocupar o espaço livre
            int freeHeight = 0;
            freeHeight = this.PanelCAControlo1.grpIdentificadorRegisto.Height;
            this.PanelCAControlo1.Controls.Remove(this.PanelCAControlo1.grpIdentificadorRegisto);
            this.PanelCAControlo1.Controls.Remove(this.PanelCAControlo1.grpIdentidadeInstituicoes);
            this.PanelCAControlo1.grpRegrasConvencoes.Left = this.PanelCAControlo1.grpIdentidadeInstituicoes.Left;
            this.PanelCAControlo1.grpRegrasConvencoes.Width = this.PanelCAControlo1.grpIdentificadorRegisto.Width;
            foreach (Control ctrl in this.PanelCAControlo1.Controls)
            {
                ctrl.Top -= freeHeight;
                if (ctrl == this.PanelCAControlo1.grpObservacoes || ctrl == this.PanelCAControlo1.grpDataCriacaoRevisao)
                    ctrl.Height += freeHeight;
            }
        }

		protected override PanelMensagem GetDeletedContextMessage()
		{
			PanelMensagem1.LblMensagem.Text = "Esta notícia de autoridade foi eliminada não sendo por isso possível apresentá-la.";
			return PanelMensagem1;
		}

		protected override PanelMensagem GetNoContextMessage()
		{
			PanelMensagem1.LblMensagem.Text = "Para visualizar os detalhes deverá selecionar uma notícia de autoridade no painel superior.";
			return PanelMensagem1;
		}
	}

} //end of root namespace