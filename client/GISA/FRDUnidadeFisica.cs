using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;
using System.Collections.Generic;

namespace GISA
{
	public class FRDUnidadeFisica : GISA.MultiPanelControl
	{

		public static Bitmap FunctionImage
		{
			get
			{
				return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "UnidadeFisica_enabled_32x32.png");
			}
		}

	#region  Windows Form Designer generated code 

		public FRDUnidadeFisica() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call

//Nitro TODO: INSTANT C# TODO TASK: The return type of the tempWith1 variable must be corrected.
//ORIGINAL LINE: With DropDownTreeView1.Nodes.Add("Identificação")
			TreeNode tempWith1 = DropDownTreeView1.Nodes.Add("Identificação");
			tempWith1.Tag = PanelUFIdentificacao1;
//Nitro TODO: INSTANT C# TODO TASK: The return type of the tempWith2 variable must be corrected.
//ORIGINAL LINE: With DropDownTreeView1.Nodes.Add("Unidades de Descrição")
            TreeNode tempWith2 = DropDownTreeView1.Nodes.Add("Unidades de Descrição");
			tempWith2.Tag = PanelUFUnidadesDescricao1;
//Nitro TODO: INSTANT C# TODO TASK: The return type of the tempWith3 variable must be corrected.
//ORIGINAL LINE: With DropDownTreeView1.Nodes.Add("Controlo de descrição")
            TreeNode tempWith3 = DropDownTreeView1.Nodes.Add("Controlo de descrição");
			tempWith3.Tag = PanelUFControloDescricao1;


			DropDownTreeView1.SelectedNode = DropDownTreeView1.Nodes[0];
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
		internal GISA.PanelUFIdentificacao2 PanelUFIdentificacao1;
		internal GISA.PanelUFControloDescricao PanelUFControloDescricao1;
		internal GISA.PanelUFUnidadesDescricao PanelUFUnidadesDescricao1;
		internal GISA.PanelMensagem PanelMensagem1;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.PanelUFIdentificacao1 = new GISA.PanelUFIdentificacao2();
			this.PanelUFControloDescricao1 = new GISA.PanelUFControloDescricao();
			this.PanelUFUnidadesDescricao1 = new GISA.PanelUFUnidadesDescricao();
			this.PanelMensagem1 = new PanelMensagem();
			this.SuspendLayout();
			//
			//DropDownTreeView1
			//
			this.DropDownTreeView1.GISAFunction = "Descrição ";
			this.DropDownTreeView1.Name = "DropDownTreeView1";
			//
			//ToolBar
			//
			this.ToolBar.Name = "ToolBar";
			//
			//PanelUFIdentificacao1
			//
			this.PanelUFIdentificacao1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelUFIdentificacao1.Location = new System.Drawing.Point(0, 49);
			this.PanelUFIdentificacao1.Name = "PanelUFIdentificacao1";
			this.PanelUFIdentificacao1.Size = new System.Drawing.Size(700, 366);
			this.PanelUFIdentificacao1.TabIndex = 0;
			//
			//PanelUFControloDescricao1
			//
			this.PanelUFControloDescricao1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelUFControloDescricao1.Location = new System.Drawing.Point(0, 49);
			this.PanelUFControloDescricao1.Name = "PanelUFControloDescricao1";
			this.PanelUFControloDescricao1.Size = new System.Drawing.Size(700, 366);
			this.PanelUFControloDescricao1.TabIndex = 1;
			this.PanelUFControloDescricao1.Visible = false;
			//
			//PanelUFUnidadesDescricao
			//
			this.PanelUFUnidadesDescricao1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelUFUnidadesDescricao1.Location = new System.Drawing.Point(0, 49);
			this.PanelUFUnidadesDescricao1.Name = "PanelUFUnidadesDescricao1";
			this.PanelUFUnidadesDescricao1.Size = new System.Drawing.Size(700, 366);
			this.PanelUFUnidadesDescricao1.TabIndex = 2;
			this.PanelUFUnidadesDescricao1.Visible = false;
            //
            //PanelMensagem1
            //
            this.PanelMensagem1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMensagem1.Location = new System.Drawing.Point(0, 49);
            this.PanelMensagem1.Name = "PanelMensagem1";
            this.PanelMensagem1.Size = new System.Drawing.Size(700, 366);
            this.PanelMensagem1.TabIndex = 20;
			//
			//FRDUnidadeFisica
			//
			this.GisaPanelScroller.Controls.Add(this.PanelUFIdentificacao1);
			this.GisaPanelScroller.Controls.Add(this.PanelUFControloDescricao1);
			this.GisaPanelScroller.Controls.Add(this.PanelUFUnidadesDescricao1);
			this.GisaPanelScroller.Controls.Add(this.PanelMensagem1);
			this.Name = "FRDUnidadeFisica";
			this.Size = new System.Drawing.Size(700, 415);
			this.ResumeLayout(false);

		}

	#endregion

		private GISADataset.FRDBaseRow CurrentFRDBase;
		private bool isLoaded = false;
		public override void LoadData()
		{
			try
			{
				((frmMain)TopLevelControl).EnterWaitMode();
				GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
				try
				{
                    GisaDataSetHelper.ManageDatasetConstraints(false);

					if (! isLoaded)
					{
						if (CurrentContext.NivelUnidadeFisica == null)
						{
							CurrentFRDBase = null;
							return;
						}						

						// Recarregar a uf actual e guardar um contexto localmente
						FRDRule.Current.LoadFRDUnidadeFisicaData(GisaDataSetHelper.GetInstance(), CurrentContext.NivelUnidadeFisica.ID, System.Enum.Format(typeof(TipoFRDBase), TipoFRDBase.FRDUnidadeFisica, "D"), ho.Connection);


						string QueryFilter = "IDNivel=" + CurrentContext.NivelUnidadeFisica.ID.ToString() + " AND IDTipoFRDBase=" + System.Enum.Format(typeof(TipoFRDBase), TipoFRDBase.FRDUnidadeFisica, "D");


                        //TODO: INSTANT C# TODO TASK: The return type of the tempWith1 variable must be corrected.

						GISADataset tempWith1 = GisaDataSetHelper.GetInstance();
						try
						{
							CurrentFRDBase = (GISADataset.FRDBaseRow)(tempWith1.FRDBase.Select(QueryFilter)[0]);
						}
						catch (IndexOutOfRangeException)
						{
							CurrentFRDBase = tempWith1.FRDBase.AddFRDBaseRow(CurrentContext.NivelUnidadeFisica, (GISADataset.TipoFRDBaseRow)(tempWith1.TipoFRDBase. Select("ID=" + System.Enum.Format(typeof(TipoFRDBase), TipoFRDBase.FRDUnidadeFisica, "D"))[0]), "", "", new byte[]{}, 0);
						}

						//---

						if (CurrentFRDBase == null || CurrentFRDBase.RowState == DataRowState.Detached || CurrentContext.NivelUnidadeFisica == null || CurrentContext.NivelUnidadeFisica.RowState == DataRowState.Detached)
						{

							return;
						}
						isLoaded = true;
					}
					GisaDataSetHelper.ManageDatasetConstraints(false);
					GISAPanel selectedPanel = (GISAPanel)this.DropDownTreeView1.SelectedNode.Tag;
					if (! selectedPanel.IsLoaded)
					{
						long startTicks = 0;
						startTicks = DateTime.Now.Ticks;
						selectedPanel.LoadData(CurrentFRDBase, ho.Connection);
						Debug.WriteLine("Time dispend loading " + selectedPanel.ToString() + ": " + new TimeSpan(DateTime.Now.Ticks - startTicks).ToString());
					}
					GisaDataSetHelper.ManageDatasetConstraints(true);
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
				if (CurrentFRDBase == null || CurrentFRDBase.RowState == DataRowState.Detached)
				{

					this.Visible = false;
					return;
				}

				try
				{
                //TODO: INSTANT C# TODO TASK: C# does not have an equivalent to VB.NET's 'MyClass' keyword
                //ORIGINAL LINE: With New GisaPrincipalPermission(SessionHelper.GetGisaPrincipal(), this.GetType().FullName, GisaPrincipalPermission.WRITE)
					GisaPrincipalPermission tempWith1 = new GisaPrincipalPermission(SessionHelper.GetGisaPrincipal(), this.GetType().FullName, GisaPrincipalPermission.WRITE);
					tempWith1.Demand();
				}
				catch (System.Security.SecurityException)
				{
					//Me.GisaPanelScroller.Enabled = False
                    GUIHelper.GUIHelper.makeReadOnly(this.GisaPanelScroller);
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

            if (PanelUFControloDescricao1 != null &&
                PanelUFControloDescricao1.IsLoaded &&
                PanelUFControloDescricao1.ControloRevisoes1.ControloAutores1.SelectedAutor != null &&
                !(((DataRow)PanelUFControloDescricao1.ControloRevisoes1.ControloAutores1.SelectedAutor).RowState == DataRowState.Detached) &&
                ((GISADataset.TrusteeRow)(PanelUFControloDescricao1.ControloRevisoes1.ControloAutores1.SelectedAutor)).GetTrusteeUserRows().Length > 0)

                tuAuthorRow = ((GISADataset.TrusteeRow)PanelUFControloDescricao1.ControloRevisoes1.ControloAutores1.SelectedAutor).GetTrusteeUserRows()[0];
            else if (SessionHelper.GetGisaPrincipal().TrusteeUserAuthor != null && !(SessionHelper.GetGisaPrincipal().TrusteeUserAuthor.RowState == DataRowState.Detached))
                tuAuthorRow = SessionHelper.GetGisaPrincipal().TrusteeUserAuthor;

            return tuAuthorRow;
        }

        private void AddRegistration(GISADataset.FRDBaseRow frdBase, bool existsModifiedData)
        {
            GISADataset.TrusteeUserRow tuAuthor = GetSelectedAuthor();
            GISADataset.TrusteeUserRow tuOperator = SessionHelper.GetGisaPrincipal().TrusteeUserOperator;
            DateTime data;
            if (PanelUFControloDescricao1 != null)
                data = PanelUFControloDescricao1.ControloRevisoes1.dtpRecolha.Value;
            else
                data = DateTime.Now;

            GISA.Model.RecordRegisterHelper.RegisterRecordModificationFRD(frdBase, existsModifiedData, tuOperator, tuAuthor, data);
        }

		public override bool ViewToModel()
		{
			base.ViewToModel();

            AddRegistration(CurrentFRDBase, existsModifiedData);

			//' só é registada uma nova entrada no controlo de descrição se alguma informação relativa à FRD 
			//' tiver sido modificada; no entanto só será criada uma entrada neste método caso não tenha sido 
			//' criado outra no PanelUFControloDescricao (que só acontece se o utilizador o tiver visualizado)

			return true;
		}

		public override void Deactivate()
		{
			DeactivatePanels();
			isLoaded = false;
			CurrentFRDBase = null;
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
                    List<string> IDNiveis = DBAbstractDataLayer.DataAccessRules.UFRule.Current.GetNiveisDocAssociados(CurrentFRDBase.NivelRow.ID, ho.Connection);
                    GISA.Search.Updater.updateNivelDocumental(IDNiveis);
                    GISA.Search.Updater.updateUnidadeFisica(CurrentFRDBase.NivelRow.ID);
                }
                catch (GISA.Search.UpdateServerException)
                {
                }
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
            return CurrentFRDBase != null && !(CurrentFRDBase.RowState == DataRowState.Detached) && CurrentFRDBase.isDeleted == 0;
		}

		protected override bool isOuterContextValid()
		{
			return CurrentContext.NivelUnidadeFisica != null;
		}

		protected override bool isOuterContextDeleted()
		{
			Debug.Assert(CurrentContext.NivelUnidadeFisica != null, "CurrentContext.NivelUnidadeFisica Is Nothing");
			return CurrentContext.NivelUnidadeFisica.RowState == DataRowState.Detached;
		}

		protected override void addContextChangeHandlers()
		{
			CurrentContext.NivelUnidadeFisicaChanged += this.Recontextualize;
			CurrentContext.AddRevisionEvent += RegisterModification;
		}

		protected override void removeContextChangeHandlers()
		{
			CurrentContext.NivelUnidadeFisicaChanged -= this.Recontextualize;
			CurrentContext.AddRevisionEvent -= RegisterModification;
		}

        protected override void RegisterModification(object o, RegisterModificationEventArgs e)
		{
            AddRegistration((GISADataset.FRDBaseRow)e.Context, true);
		}

		protected override PanelMensagem GetDeletedContextMessage()
		{
			PanelMensagem1.LblMensagem.Text = "Esta unidade física foi eliminada não sendo por isso possível apresentá-la.";
			return PanelMensagem1;
		}

		protected override PanelMensagem GetNoContextMessage()
		{
			PanelMensagem1.LblMensagem.Text = "Para visualizar os detalhes deverá selecionar uma unidade física no painel superior.";
			return PanelMensagem1;
		}
	}
}