using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA
{
	public class PanelOrganizacaoOrdenacao : GISA.GISAPanel
	{

	#region  Windows Form Designer generated code 

		public PanelOrganizacaoOrdenacao() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call

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
		internal System.Windows.Forms.GroupBox grpTradicaoDocumental;
		internal System.Windows.Forms.GroupBox grpOrdenacao;
		internal System.Windows.Forms.CheckedListBox chkLstTradicaoDocumental;
		internal System.Windows.Forms.CheckedListBox chklstOrdenacao;
		internal System.Windows.Forms.GroupBox GroupBox1;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.grpTradicaoDocumental = new System.Windows.Forms.GroupBox();
            this.chkLstTradicaoDocumental = new System.Windows.Forms.CheckedListBox();
            this.grpOrdenacao = new System.Windows.Forms.GroupBox();
            this.chklstOrdenacao = new System.Windows.Forms.CheckedListBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.grpTradicaoDocumental.SuspendLayout();
            this.grpOrdenacao.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpTradicaoDocumental
            // 
            this.grpTradicaoDocumental.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTradicaoDocumental.Controls.Add(this.chkLstTradicaoDocumental);
            this.grpTradicaoDocumental.Location = new System.Drawing.Point(8, 16);
            this.grpTradicaoDocumental.Name = "grpTradicaoDocumental";
            this.grpTradicaoDocumental.Size = new System.Drawing.Size(778, 112);
            this.grpTradicaoDocumental.TabIndex = 0;
            this.grpTradicaoDocumental.TabStop = false;
            this.grpTradicaoDocumental.Text = "Tradição documental";
            // 
            // chkLstTradicaoDocumental
            // 
            this.chkLstTradicaoDocumental.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLstTradicaoDocumental.IntegralHeight = false;
            this.chkLstTradicaoDocumental.Location = new System.Drawing.Point(8, 16);
            this.chkLstTradicaoDocumental.Name = "chkLstTradicaoDocumental";
            this.chkLstTradicaoDocumental.Size = new System.Drawing.Size(762, 89);
            this.chkLstTradicaoDocumental.TabIndex = 0;
            // 
            // grpOrdenacao
            // 
            this.grpOrdenacao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOrdenacao.Controls.Add(this.chklstOrdenacao);
            this.grpOrdenacao.Location = new System.Drawing.Point(8, 130);
            this.grpOrdenacao.Name = "grpOrdenacao";
            this.grpOrdenacao.Size = new System.Drawing.Size(778, 456);
            this.grpOrdenacao.TabIndex = 1;
            this.grpOrdenacao.TabStop = false;
            this.grpOrdenacao.Text = "Ordenação";
            // 
            // chklstOrdenacao
            // 
            this.chklstOrdenacao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chklstOrdenacao.IntegralHeight = false;
            this.chklstOrdenacao.Location = new System.Drawing.Point(8, 16);
            this.chklstOrdenacao.Name = "chklstOrdenacao";
            this.chklstOrdenacao.Size = new System.Drawing.Size(762, 432);
            this.chklstOrdenacao.TabIndex = 1;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.Controls.Add(this.grpOrdenacao);
            this.GroupBox1.Controls.Add(this.grpTradicaoDocumental);
            this.GroupBox1.Location = new System.Drawing.Point(3, 3);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(794, 594);
            this.GroupBox1.TabIndex = 3;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "3.4. Organização e ordenação";
            // 
            // PanelOrganizacaoOrdenacao
            // 
            this.Controls.Add(this.GroupBox1);
            this.Name = "PanelOrganizacaoOrdenacao";
            this.grpTradicaoDocumental.ResumeLayout(false);
            this.grpOrdenacao.ResumeLayout(false);
            this.GroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

        private GISADataset.FRDBaseRow CurrentFRDBase;
		private DomainValueListBoxController TradicaoDocumentalController;
		private DomainValueListBoxController OrdenacaoController;

		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			IsLoaded = false;
			CurrentFRDBase = (GISADataset.FRDBaseRow)CurrentDataRow;

			FRDRule.Current.LoadOrganizacaoOrdenacaoData(GisaDataSetHelper.GetInstance(), CurrentFRDBase.ID, conn);

			IsLoaded = true;
		}

		public override void ModelToView()
		{
			IsPopulated = false;
			TradicaoDocumentalController = new DomainValueListBoxController(CurrentFRDBase, GisaDataSetHelper.GetInstance().TipoTradicaoDocumental, GisaDataSetHelper.GetInstance().SFRDTradicaoDocumental, "IDTipoTradicaoDocumental", chkLstTradicaoDocumental);
			TradicaoDocumentalController.ModelToView();

			OrdenacaoController = new DomainValueListBoxController(CurrentFRDBase, GisaDataSetHelper.GetInstance().TipoOrdenacao, GisaDataSetHelper.GetInstance().SFRDOrdenacao, "IDTipoOrdenacao", chklstOrdenacao);
			OrdenacaoController.ModelToView();
			IsPopulated = true;
		}

		public override void ViewToModel()
		{
			if (CurrentFRDBase == null || CurrentFRDBase.RowState == DataRowState.Detached || ! IsLoaded)
			{
				return;
			}

			//Actualizar as modificações no modelo
			TradicaoDocumentalController.ViewToModel(CurrentFRDBase);
			OrdenacaoController.ViewToModel(CurrentFRDBase);
		}

		public override void Deactivate()
		{
			TradicaoDocumentalController.Deactivate();
			OrdenacaoController.Deactivate();
		}
	}

} //end of root namespace