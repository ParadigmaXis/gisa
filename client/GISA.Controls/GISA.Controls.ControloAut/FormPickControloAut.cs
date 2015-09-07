using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA.Controls.ControloAut
{
	public class FormPickControloAut : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 


		public FormPickControloAut(): this(null)
		{
		}

		public FormPickControloAut(GISADataset.ControloAutRow ContextControloAut) : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() Call
            caList.BeforeNewListSelection += caList_BeforeNewListSelection;
            caList.DoubleClick += caList_DoubleClick;
            btnAdicionar.Click += btnAdicionar_Click;

			mContextControloAut = ContextControloAut;
            caList.FilterVisible = true;
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
		public ControloAutList caList;
		internal System.Windows.Forms.Button btnAdicionar;
		internal System.Windows.Forms.Button btnCancelar;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.caList = new ControloAutList();
			this.btnAdicionar = new System.Windows.Forms.Button();
			this.btnCancelar = new System.Windows.Forms.Button();
			this.SuspendLayout();
			//
			//caList
			//
			this.caList.AllowedNoticiaAutLocked = false;
			this.caList.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.caList.DockPadding.Bottom = 6;
			this.caList.DockPadding.Left = 6;
			this.caList.DockPadding.Right = 6;
			this.caList.DockPadding.Top = 6;
            //this.caList.ListHandler = null;
			this.caList.Location = new System.Drawing.Point(2, 2);
			this.caList.Name = "caList";
			this.caList.Size = new System.Drawing.Size(630, 339);
			this.caList.TabIndex = 1;
			//
			//btnAdicionar
			//
			this.btnAdicionar.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnAdicionar.Enabled = false;
			this.btnAdicionar.Location = new System.Drawing.Point(464, 346);
			this.btnAdicionar.Name = "btnAdicionar";
			this.btnAdicionar.Size = new System.Drawing.Size(72, 24);
			this.btnAdicionar.TabIndex = 2;
			this.btnAdicionar.Text = "A&dicionar";
			//
			//btnCancelar
			//
			this.btnCancelar.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancelar.Location = new System.Drawing.Point(544, 346);
			this.btnCancelar.Name = "btnCancelar";
			this.btnCancelar.Size = new System.Drawing.Size(72, 24);
			this.btnCancelar.TabIndex = 3;
			this.btnCancelar.Text = "&Cancelar";
			//
			//FormPickControloAut
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancelar;
			this.ClientSize = new System.Drawing.Size(634, 383);
			this.Controls.Add(this.caList);
			this.Controls.Add(this.btnAdicionar);
			this.Controls.Add(this.btnCancelar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPickControloAut";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

		private GISADataset.ControloAutRow mContextControloAut = null;
		private void caList_BeforeNewListSelection(object sender, BeforeNewSelectionEventArgs e)
		{
            btnAdicionar.Enabled = e.ItemToBeSelected != null && e.ItemToBeSelected.ListView != null;
		}

		private void caList_DoubleClick(object sender, System.EventArgs e)
		{
			btnAdicionar.PerformClick();
		}

		private void btnAdicionar_Click(object sender, System.EventArgs e)
		{
			if (validateData())
			{
				DialogResult = System.Windows.Forms.DialogResult.OK;
				Close();
			}
		}

		private bool validateData()
		{
			if (mContextControloAut == null) return true;

			GISADataset.ControloAutDicionarioRow cadRow = null;
			foreach (ListViewItem item in caList.SelectedItems)
			{
				cadRow = (GISADataset.ControloAutDicionarioRow)item.Tag;
				if (cadRow.ControloAutRow == mContextControloAut)
				{
					MessageBox.Show("Não é permitido relacionar uma notícia de autoridade consigo própria.", "Erro ao estabelecer relação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}
			}
			return true;
		}
	}
} //end of root namespace