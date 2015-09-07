using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

namespace GISA
{
	public class ControlAutoEliminacao : System.Windows.Forms.UserControl
	{

	#region  Windows Form Designer generated code 

		public ControlAutoEliminacao() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            btnAutoEliminacaoManager.Click += btnAutoEliminacaoManager_Click;
			GetExtraResources();
		}

		//UserControl overrides dispose to clean up the component list.
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
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		internal System.Windows.Forms.GroupBox grpAutoEliminacao;
		internal System.Windows.Forms.Button btnAutoEliminacaoManager;
		internal System.Windows.Forms.ComboBox cbAutoEliminacao;
		internal System.Windows.Forms.ToolTip ToolTip1;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.grpAutoEliminacao = new System.Windows.Forms.GroupBox();
            this.btnAutoEliminacaoManager = new System.Windows.Forms.Button();
            this.cbAutoEliminacao = new System.Windows.Forms.ComboBox();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grpAutoEliminacao.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpAutoEliminacao
            // 
            this.grpAutoEliminacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAutoEliminacao.Controls.Add(this.btnAutoEliminacaoManager);
            this.grpAutoEliminacao.Controls.Add(this.cbAutoEliminacao);
            this.grpAutoEliminacao.Location = new System.Drawing.Point(0, 0);
            this.grpAutoEliminacao.Name = "grpAutoEliminacao";
            this.grpAutoEliminacao.Size = new System.Drawing.Size(137, 48);
            this.grpAutoEliminacao.TabIndex = 1;
            this.grpAutoEliminacao.TabStop = false;
            this.grpAutoEliminacao.Text = "Nº Auto de eliminação";
            // 
            // btnAutoEliminacaoManager
            // 
            this.btnAutoEliminacaoManager.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAutoEliminacaoManager.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAutoEliminacaoManager.Location = new System.Drawing.Point(107, 20);
            this.btnAutoEliminacaoManager.Name = "btnAutoEliminacaoManager";
            this.btnAutoEliminacaoManager.Size = new System.Drawing.Size(24, 23);
            this.btnAutoEliminacaoManager.TabIndex = 2;
            // 
            // cbAutoEliminacao
            // 
            this.cbAutoEliminacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAutoEliminacao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAutoEliminacao.DropDownWidth = 200;
            this.cbAutoEliminacao.Location = new System.Drawing.Point(8, 20);
            this.cbAutoEliminacao.Name = "cbAutoEliminacao";
            this.cbAutoEliminacao.Size = new System.Drawing.Size(93, 21);
            this.cbAutoEliminacao.TabIndex = 1;
            // 
            // ControlAutoEliminacao
            // 
            this.Controls.Add(this.grpAutoEliminacao);
            this.Name = "ControlAutoEliminacao";
            this.Size = new System.Drawing.Size(140, 48);
            this.grpAutoEliminacao.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

		private void GetExtraResources()
		{
			this.btnAutoEliminacaoManager.Image = SharedResourcesOld.CurrentSharedResources.Editar;
			ToolTip1.SetToolTip(this.btnAutoEliminacaoManager, SharedResourcesOld.CurrentSharedResources.EditarString);
		}

		// Todos os autos de eliminação terão já de ter sido carregados para memória 
		// antes de ser pedido a população da combobox
		public void rebindToData()
		{
			cbAutoEliminacao.DataSource = null;
			cbAutoEliminacao.DataSource = getAutoEliminacaoDataSource();
			cbAutoEliminacao.DisplayMember = "Designacao";
			cbAutoEliminacao.ValueMember = "ID";
		}

		private DataTable getAutoEliminacaoDataSource()
		{
			DataTable dtAutoEliminacao = new DataTable();
			dtAutoEliminacao.Columns.Add("ID", typeof(long));
			dtAutoEliminacao.Columns.Add("Designacao", typeof(string));
			dtAutoEliminacao.Rows.Add(new object[] {long.MinValue, string.Empty}); // é utilizado o long.minvalue e não, por exemplo, -1 porque os novos autos de eliminação terão um id -1
			
            foreach (GISADataset.AutoEliminacaoRow row in GisaDataSetHelper.GetInstance().AutoEliminacao.Select(string.Empty, "Designacao"))
				dtAutoEliminacao.Rows.Add(new object[] {row["ID"], row["Designacao"]});

			return dtAutoEliminacao;
		}

		private void btnAutoEliminacaoManager_Click(object sender, System.EventArgs e)
		{
			FormAutoEliminacaoEditor formAutoEditor = new FormAutoEliminacaoEditor();
			IDbConnection conn = GisaDataSetHelper.GetConnection();
			try
			{
				conn.Open();
				RelatorioRule.Current.LoadAutosEliminacao(GisaDataSetHelper.GetInstance(), conn);
			}
			finally
			{
				conn.Close();
			}

			formAutoEditor.LoadData(GisaDataSetHelper.GetInstance().AutoEliminacao.Select(string.Empty, "Designacao"), "Designacao");
			formAutoEditor.ShowDialog();

            PersistencyHelper.save();
            PersistencyHelper.cleanDeletedData();

			long selectedAutoEliminacaoID = 0;
			if (cbAutoEliminacao.SelectedValue == null)
				selectedAutoEliminacaoID = long.MinValue;
			else
				selectedAutoEliminacaoID = (long)cbAutoEliminacao.SelectedValue;

			rebindToData();
			cbAutoEliminacao.SelectedValue = selectedAutoEliminacaoID;
		}

		public bool ContentsEnabled
		{
			get {return cbAutoEliminacao.Enabled;}
			set
			{
				cbAutoEliminacao.Enabled = value;
				btnAutoEliminacaoManager.Enabled = true;
			}
		}
	}
}