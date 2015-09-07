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
	public class FormAdUtilizadores : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public FormAdUtilizadores() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            lstvwUtilizadores.SelectedIndexChanged += lstvwUtilizadores_SelectedIndexChanged;
			LoadData();
			PopulateUsers();
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
		internal System.Windows.Forms.ColumnHeader colNome;
		internal System.Windows.Forms.ColumnHeader colTipo;
		internal System.Windows.Forms.Button btnAdicionar;
		internal System.Windows.Forms.Button btnCancelar;
		internal System.Windows.Forms.ListView lstvwUtilizadores;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.lstvwUtilizadores = new System.Windows.Forms.ListView();
			this.colNome = new System.Windows.Forms.ColumnHeader();
			this.colTipo = new System.Windows.Forms.ColumnHeader();
			this.btnAdicionar = new System.Windows.Forms.Button();
			this.btnCancelar = new System.Windows.Forms.Button();
			this.SuspendLayout();
			//
			//lstvwUtilizadores
			//
			this.lstvwUtilizadores.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.lstvwUtilizadores.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {this.colNome, this.colTipo});
			this.lstvwUtilizadores.FullRowSelect = true;
			this.lstvwUtilizadores.Location = new System.Drawing.Point(1, 1);
			this.lstvwUtilizadores.Name = "lstvwUtilizadores";
			this.lstvwUtilizadores.Size = new System.Drawing.Size(518, 190);
			this.lstvwUtilizadores.TabIndex = 0;
			this.lstvwUtilizadores.View = System.Windows.Forms.View.Details;
			//
			//colNome
			//
			this.colNome.Text = "Nome";
			this.colNome.Width = 300;
			//
			//colTipo
			//
			this.colTipo.Text = "Tipo";
			this.colTipo.Width = 150;
			//
			//btnAdicionar
			//
			this.btnAdicionar.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnAdicionar.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnAdicionar.Enabled = false;
			this.btnAdicionar.Location = new System.Drawing.Point(360, 208);
			this.btnAdicionar.Name = "btnAdicionar";
			this.btnAdicionar.TabIndex = 1;
			this.btnAdicionar.Text = "Adicionar";
			//
			//btnCancelar
			//
			this.btnCancelar.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancelar.Location = new System.Drawing.Point(440, 208);
			this.btnCancelar.Name = "btnCancelar";
			this.btnCancelar.TabIndex = 2;
			this.btnCancelar.Text = "Cancelar";
			//
			//FormAdUtilizadores
			//
			this.AcceptButton = this.btnAdicionar;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancelar;
			this.ClientSize = new System.Drawing.Size(520, 238);
			this.ControlBox = false;
			this.Controls.Add(this.btnCancelar);
			this.Controls.Add(this.btnAdicionar);
			this.Controls.Add(this.lstvwUtilizadores);
			this.Name = "FormAdUtilizadores";
			this.ShowInTaskbar = false;
			this.Text = "Utilizadores";
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

		private void LoadData()
		{
			IDbConnection conn = GisaDataSetHelper.GetConnection();
			try
			{
				conn.Open();
				PermissoesRule.Current.LoadUtilizadores(GisaDataSetHelper.GetInstance(), conn);
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
		}

		private void PopulateUsers()
		{
			this.lstvwUtilizadores.BeginUpdate();
			string filter = null;
	#if TESTING
			filter = string.Empty;
	#else
			filter = "BuiltInTrustee=0";
	#endif
			foreach (GISADataset.TrusteeRow tRow in GisaDataSetHelper.GetInstance().Trustee.Select(filter, "CatCode Asc, Name Asc"))
			{
				ListViewItem item = new ListViewItem();
				item.SubItems.AddRange(new string[] {string.Empty, string.Empty});
				item.SubItems[0].Text = tRow.Name;
				if (tRow.CatCode.Equals("USR"))
				{
					item.SubItems[1].Text = "Utilizador";
				}
				else
				{
					item.SubItems[1].Text = "Grupo de Utilizadores";
				}
				item.Tag = tRow;
				this.lstvwUtilizadores.Items.Add(item);
			}
			this.lstvwUtilizadores.EndUpdate();
		}

		public ListView.SelectedListViewItemCollection SelectedItems
		{
			get
			{
				return lstvwUtilizadores.SelectedItems;
			}
		}

		private void lstvwUtilizadores_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lstvwUtilizadores.SelectedItems.Count > 0)
			{
				btnAdicionar.Enabled = true;
			}
			else
			{
				btnAdicionar.Enabled = false;
			}
		}
	}

} //end of root namespace