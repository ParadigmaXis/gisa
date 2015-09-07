using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

using GISA.Controls;
using GISA.Controls.Localizacao;


namespace GISA
{
	public class SlavePanelPermissoesPlanoClassificacao : GISA.SinglePanel
	{
	    #region  Windows Form Designer generated code 

		public SlavePanelPermissoesPlanoClassificacao() : base()
		{
			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            PermissoesNivelList1.BeforeNewListSelection += PermissoesNivelList1_BeforeNewListSelection;

            PermissoesNivelList1.PopulateTipoFiltro();
            PermissoesNivelList1.PopulateFiltro();
            PermissoesNivelList1.FilterVisible = true;

            this.ToolBar.Visible = false;

            ControloLocalizacao1.grpLocalizacao.Text = "Localização na estrutura arquivística";

            this.PermissoesNivelList1.TheReload = Save;
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
        internal PermissoesNivelList PermissoesNivelList1;
        internal ControloLocalizacao ControloLocalizacao1;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
        internal GISA.PanelMensagem PanelMensagem1;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.PanelMensagem1 = new GISA.PanelMensagem();
            this.PermissoesNivelList1 = new GISA.PermissoesNivelList();
            this.ControloLocalizacao1 = new GISA.Controls.Localizacao.ControloLocalizacao();
            this.pnlToolbarPadding.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFuncao
            // 
            this.lblFuncao.Location = new System.Drawing.Point(0, 0);
            this.lblFuncao.Text = "Definir Permissões";
            // 
            // pnlToolbarPadding
            // 
            this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 24);
            this.pnlToolbarPadding.Visible = false;
            // 
            // PanelMensagem1
            // 
            this.PanelMensagem1.BackColor = System.Drawing.SystemColors.Control;
            this.PanelMensagem1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMensagem1.IsLoaded = false;
            this.PanelMensagem1.IsPopulated = false;
            this.PanelMensagem1.Location = new System.Drawing.Point(0, 0);
            this.PanelMensagem1.Name = "PanelMensagem1";
            this.PanelMensagem1.Size = new System.Drawing.Size(600, 364);
            this.PanelMensagem1.TabIndex = 24;
            this.PanelMensagem1.TbBAuxListEventAssigned = false;
            this.PanelMensagem1.TheGenericDelegate = null;
            this.PanelMensagem1.Visible = false;
            // 
            // PermissoesNivelList1
            // 
            this.PermissoesNivelList1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PermissoesNivelList1.FilterVisible = true;
            this.PermissoesNivelList1.Location = new System.Drawing.Point(0, 52);
            this.PermissoesNivelList1.MultiSelectListView = true;
            this.PermissoesNivelList1.Name = "PermissoesNivelList1";
            this.PermissoesNivelList1.Padding = new System.Windows.Forms.Padding(6);
            this.PermissoesNivelList1.Size = new System.Drawing.Size(600, 145);
            this.PermissoesNivelList1.TabIndex = 25;
            this.PermissoesNivelList1.TheReload = null;
            // 
            // ControloLocalizacao1
            // 
            this.ControloLocalizacao1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ControloLocalizacao1.AutoScroll = true;
            this.ControloLocalizacao1.Location = new System.Drawing.Point(0, 168);
            this.ControloLocalizacao1.Name = "ControloLocalizacao1";
            this.ControloLocalizacao1.Size = new System.Drawing.Size(600, 168);
            this.ControloLocalizacao1.TabIndex = 26;
            // 
            // SlavePanelPermissoesPlanoClassificacao
            // 
            this.Controls.Add(this.ControloLocalizacao1);
            this.Controls.Add(this.PermissoesNivelList1);
            this.Controls.Add(this.PanelMensagem1);
            this.Name = "SlavePanelPermissoesPlanoClassificacao";
            this.Size = new System.Drawing.Size(600, 336);
            this.Controls.SetChildIndex(this.PanelMensagem1, 0);
            this.Controls.SetChildIndex(this.lblFuncao, 0);
            this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
            this.Controls.SetChildIndex(this.PermissoesNivelList1, 0);
            this.Controls.SetChildIndex(this.ControloLocalizacao1, 0);
            this.pnlToolbarPadding.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

		public static Bitmap FunctionImage
		{
			get {return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "PermissoesPorClassificacao_enabled_32x32.png");}
		}

		private GISADataset.NivelRow mCurrentNivel;
		public GISADataset.NivelRow currentNivel
		{
			get {return mCurrentNivel;}
			set {mCurrentNivel = value;}
		}

        private GISADataset.TrusteeRow mCurrentUser;
        public GISADataset.TrusteeRow currentUser
        {
            get { return mCurrentUser; }
            set { mCurrentUser = value; }
        }

        public override void LoadData()
		{
            if (CurrentContext.PermissoesNivel == null || CurrentContext.PermissoesTrustee == null)
			{
				currentNivel = null;
                currentUser = null;
				return;
			}

			currentNivel = CurrentContext.PermissoesNivel;
            currentUser = CurrentContext.PermissoesTrustee;

            PermissoesNivelList1.CurrentTrusteeRow = currentUser;
            PermissoesNivelList1.CurrentNivelRow = currentNivel;
            PermissoesNivelList1.ReloadList();
		}

		public override void ModelToView()
		{
    	}

		public override bool ViewToModel()
		{
		    return false;
		}

        private void Save()
        {
            Save(false);
        }

        public override PersistencyHelper.SaveResult Save(bool activateOpcaoCancelar)
        {
            List<long> niveisIDs = new List<long>();
            foreach (GISADataset.TrusteeNivelPrivilegeRow tnpRow in GisaDataSetHelper.GetInstance().TrusteeNivelPrivilege.Select("", "", DataViewRowState.Added | DataViewRowState.ModifiedOriginal))
                niveisIDs.Add(tnpRow.IDNivel);

            PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save(activateOpcaoCancelar);

            return successfulSave;
        }

		public override void Deactivate()
		{
            ControloLocalizacao1.ClearTree();
            nRowSelected = null;
		}

		protected override bool isInnerContextValid()
		{
			return currentNivel != null;
		}

		protected override bool isOuterContextValid()
		{
			return CurrentContext.PermissoesNivel != null;
		}

		protected override bool isOuterContextDeleted()
		{
			Debug.Assert(CurrentContext.PermissoesNivel != null, "CurrentContext.PermissoesNivelEstrututalDocumental Is Nothing");
			return CurrentContext.PermissoesNivel.RowState == DataRowState.Detached;
		}

		protected override bool hasReadPermission()
		{
			return PermissoesHelper.AllowRead;
		}

		protected override void addContextChangeHandlers()
		{
			CurrentContext.PermissoesChanged += this.Recontextualize;
		}

		protected override void removeContextChangeHandlers()
		{
			CurrentContext.PermissoesChanged -= this.Recontextualize;
		}

		protected override PanelMensagem GetDeletedContextMessage()
		{
			PanelMensagem1.LblMensagem.Text = "Esta relação hierárquica foi eliminada não sendo, por isso, possível apresentar o nível em causa.";
			return PanelMensagem1;
		}

		protected override PanelMensagem GetNoContextMessage()
		{
			PanelMensagem1.LblMensagem.Text = "Para visualizar as permissões deverá selecionar uma unidade de descrição no painel superior.";
			return PanelMensagem1;
		}

		protected override PanelMensagem GetNoReadPermissionMessage()
		{
			PanelMensagem1.LblMensagem.Text = "Não tem permissão para visualizar os detalhes do nível de descrição selecionado no painel superior.";
			return PanelMensagem1;
		}
        
        private GISADataset.NivelRow nRowSelected = null;
        private void PermissoesNivelList1_BeforeNewListSelection(object sender, BeforeNewSelectionEventArgs e)
        {
            ((frmMain)TopLevelControl).EnterWaitMode();

            GISADataset.NivelRow nivelRow = null;
            nivelRow = (GISADataset.NivelRow)e.ItemToBeSelected.Tag;
            //verificar se o nivel selecciona está apagado

            if (nivelRow == null)
            {
                ControloLocalizacao1.ClearTree();
            }
            else if (nRowSelected == nivelRow)
            {
                // não actualizar a árvore
            }
            else
            {
                //popular árvore
                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                try
                {
                    ControloLocalizacao1.BuildTree(nivelRow.ID, ho.Connection, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    throw ex;
                }
                finally
                {
                    ho.Dispose();
                }
            }

            nRowSelected = nivelRow;
            ((frmMain)TopLevelControl).LeaveWaitMode();
        }
	}
}