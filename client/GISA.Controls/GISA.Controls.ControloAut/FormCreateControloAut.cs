using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA.Controls.ControloAut
{
	public class FormCreateControloAut : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public FormCreateControloAut() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            ListTermos.IncrementalSearchTextChanged += listTermos_IncrementalSearchTextChanged;
            ListTermos.TermoChanged += ListTermos_TermoChanged;
            cbNoticiaAut.SelectedIndexChanged += cbNoticiaAut_SelectedIndexChanged;
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
		public System.Windows.Forms.Label lblNoticiaAut;
        public System.Windows.Forms.ComboBox cbNoticiaAut;
        public ListTermos ListTermos;
        public System.Windows.Forms.StatusBar StatusBar1;
        public System.Windows.Forms.StatusBarPanel StatusBarIncrementalText;
        public System.Windows.Forms.GroupBox grpTermo;
        public System.Windows.Forms.Button btnOk;
        private Label lblNIF;
        private TextBox txtNIF;
        public System.Windows.Forms.Button btnCancel;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.lblNoticiaAut = new System.Windows.Forms.Label();
            this.cbNoticiaAut = new System.Windows.Forms.ComboBox();
            this.grpTermo = new System.Windows.Forms.GroupBox();
            this.ListTermos = new GISA.Controls.ControloAut.ListTermos();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.StatusBar1 = new System.Windows.Forms.StatusBar();
            this.StatusBarIncrementalText = new System.Windows.Forms.StatusBarPanel();
            this.lblNIF = new System.Windows.Forms.Label();
            this.txtNIF = new System.Windows.Forms.TextBox();
            this.grpTermo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StatusBarIncrementalText)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNoticiaAut
            // 
            this.lblNoticiaAut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNoticiaAut.Location = new System.Drawing.Point(8, 8);
            this.lblNoticiaAut.Name = "lblNoticiaAut";
            this.lblNoticiaAut.Size = new System.Drawing.Size(388, 16);
            this.lblNoticiaAut.TabIndex = 5;
            this.lblNoticiaAut.Text = "Notícia de autoridade";
            // 
            // cbNoticiaAut
            // 
            this.cbNoticiaAut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbNoticiaAut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNoticiaAut.Location = new System.Drawing.Point(8, 24);
            this.cbNoticiaAut.Name = "cbNoticiaAut";
            this.cbNoticiaAut.Size = new System.Drawing.Size(388, 21);
            this.cbNoticiaAut.TabIndex = 1;
            // 
            // grpTermo
            // 
            this.grpTermo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTermo.Controls.Add(this.ListTermos);
            this.grpTermo.Location = new System.Drawing.Point(4, 56);
            this.grpTermo.Name = "grpTermo";
            this.grpTermo.Size = new System.Drawing.Size(400, 236);
            this.grpTermo.TabIndex = 2;
            this.grpTermo.TabStop = false;
            this.grpTermo.Text = "Forma autorizada";
            // 
            // ListTermos
            // 
            this.ListTermos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ListTermos.Location = new System.Drawing.Point(8, 16);
            this.ListTermos.Name = "ListTermos";
            this.ListTermos.ValidAuthorizedForm = null;
            this.ListTermos.Size = new System.Drawing.Size(384, 212);
            this.ListTermos.TabIndex = 14;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(204, 333);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(88, 24);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Aceitar";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(300, 333);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 24);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancelar";
            // 
            // StatusBar1
            // 
            this.StatusBar1.Location = new System.Drawing.Point(0, 364);
            this.StatusBar1.Name = "StatusBar1";
            this.StatusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.StatusBarIncrementalText});
            this.StatusBar1.ShowPanels = true;
            this.StatusBar1.Size = new System.Drawing.Size(408, 22);
            this.StatusBar1.SizingGrip = false;
            this.StatusBar1.TabIndex = 14;
            // 
            // StatusBarIncrementalText
            // 
            this.StatusBarIncrementalText.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.StatusBarIncrementalText.Name = "StatusBarIncrementalText";
            this.StatusBarIncrementalText.Width = 408;
            // 
            // lblNIF
            // 
            this.lblNIF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNIF.AutoSize = true;
            this.lblNIF.Location = new System.Drawing.Point(8, 307);
            this.lblNIF.Name = "lblNIF";
            this.lblNIF.Size = new System.Drawing.Size(24, 13);
            this.lblNIF.TabIndex = 15;
            this.lblNIF.Text = "NIF";
            this.lblNIF.Visible = false;
            // 
            // txtNIF
            // 
            this.txtNIF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNIF.Location = new System.Drawing.Point(38, 304);
            this.txtNIF.Name = "txtNIF";
            this.txtNIF.Size = new System.Drawing.Size(358, 20);
            this.txtNIF.TabIndex = 16;
            this.txtNIF.Visible = false;
            // 
            // FormCreateControloAut
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(408, 386);
            this.ControlBox = false;
            this.Controls.Add(this.txtNIF);
            this.Controls.Add(this.lblNIF);
            this.Controls.Add(this.StatusBar1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpTermo);
            this.Controls.Add(this.cbNoticiaAut);
            this.Controls.Add(this.lblNoticiaAut);
            this.Name = "FormCreateControloAut";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Criar notícia de autoridade";
            this.grpTermo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StatusBarIncrementalText)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

	#endregion

		private GISADataset.ControloAutDicionarioRow mOriginalCADRow = null;
		internal GISADataset.ControloAutDicionarioRow OriginalCADRow
		{
			get { return mOriginalCADRow; }
		}

		public GISADataset.TipoNoticiaAutRow SelectedTipoNoticiaAut
		{
			get { return (GISADataset.TipoNoticiaAutRow)cbNoticiaAut.SelectedItem; }
		}

        public void SetOptionalControlsVisible(bool visible)
        {
            lblNIF.Visible = visible;
            txtNIF.Visible = visible;
        }

        public string NIF
        {
            get { return txtNIF.Text; }
            set { txtNIF.Text = value; }
        }

		public void SetControloAutDicionarioRow(GISADataset.ControloAutDicionarioRow cadRow)
		{
			ListTermos.ValidAuthorizedForm = cadRow.DicionarioRow.Termo;
			mOriginalCADRow = cadRow;
			UpdateButtonState();
		}

		private void listTermos_IncrementalSearchTextChanged(string text)
		{
			StatusBarIncrementalText.Text = text;
		}

		private void ListTermos_TermoChanged()
		{
			UpdateButtonState();
		}

		private void cbNoticiaAut_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UpdateButtonState();
		}

		protected virtual void UpdateButtonState()
		{
			if (ExistsFormaAutorizada() || cbNoticiaAut.SelectedIndex < 1)
				btnOk.Enabled = false;
			else
				btnOk.Enabled = true;
		}

		internal bool ExistsFormaAutorizada()
		{
			GISADataset.DicionarioRow dRow = null;
			string selectedTermo = string.Empty;
			if (ListTermos.ValidAuthorizedForm != null)
				selectedTermo = ListTermos.ValidAuthorizedForm.Replace("'", "''");

            // é necessário prever a possibilidade de haverem termos que são strings vazias (resultantes de conversões) no dataset
			if (selectedTermo.Length > 0 && GisaDataSetHelper.GetInstance().Dicionario.Select(string.Format("Termo = '{0}' ", selectedTermo)).Length > 0)
				dRow = (GISADataset.DicionarioRow)(GisaDataSetHelper.GetInstance().Dicionario.Select(string.Format("Termo = '{0}'", selectedTermo))[0]);
			else if (selectedTermo.Length > 0)
			{
				dRow = GisaDataSetHelper.GetInstance().Dicionario.NewDicionarioRow();
				dRow.Termo = ListTermos.ValidAuthorizedForm;
				dRow.CatCode = "CA";
				dRow.Versao = new byte[]{};
			}

			if (dRow == null) // dRow é nothing se o valor selecionado já existir/for inválido
				return true;

			// As rows detached serão as acabadas de criar e que, por essa mesma razão, não 
			// foram ainda adicionadas ao dataset. Se usarmos um termo já existente é 
			// necessário verificar que não existam outros CAs deste tipo de notícia de autoridade 
			// que façam já uso deste termo como forma autorizada.
			bool alreadyExistsFormaAutorizada = false;
			if (dRow != null && dRow.RowState != DataRowState.Detached)
			{
				int tcaf = (int)TipoControloAutForma.FormaAutorizada;
				IDbConnection conn = GisaDataSetHelper.GetConnection();
				try
				{
					conn.Open();
					Trace.WriteLine("<ExistsControloAutDicionario>");
                    //TODO: apagar?
					//alreadyExistsFormaAutorizada = CBool(GisaDataSetHelper.GetDBLayer().CallScalarProcedure("sp_ExistsControloAutDicionario", param, types, values))
					alreadyExistsFormaAutorizada = ControloAutRule.Current.ExistsControloAutDicionario(dRow.ID, tcaf, ((GISADataset.TipoNoticiaAutRow)cbNoticiaAut.SelectedValue).ID, conn);
					Trace.WriteLine("</ExistsControloAutDicionario>");
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
			return alreadyExistsFormaAutorizada;
		}

		public void LoadData()
		{
			LoadData(false);
		}

		public void LoadData(bool excludeAutorizados)
		{

			bool constraintsEnforced = GisaDataSetHelper.GetInstance().EnforceConstraints;
			GisaDataSetHelper.ManageDatasetConstraints(false);

			// existirá um mOriginalCADRow sempre que se trate de uma edição. nesse 
			// caso queremos que sejam carregados apenas os termos que ainda não sejam 
			// formas autorizadas incluindo, no entanto, o próprio nível e edição (apesar
			// de se tratar na altura de uma forma autorizada)
			if (excludeAutorizados && mOriginalCADRow != null)
			{
				ArrayList includeOthers = new ArrayList();
				includeOthers.Add(mOriginalCADRow.DicionarioRow.ID);
				ListTermos.LoadData(true, SelectedTipoNoticiaAut.ID, includeOthers);
			}
			else
			{
				if (excludeAutorizados && SelectedTipoNoticiaAut != null)
					ListTermos.LoadData(excludeAutorizados, SelectedTipoNoticiaAut.ID);
				else
					ListTermos.LoadData();
			}

			GisaDataSetHelper.ManageDatasetConstraints(constraintsEnforced);

			// havendo uma selecção é necessário refrescar o termo selecionado agora que 
			// temos garantidamente os dados carregados
			if (mOriginalCADRow != null)
				SetControloAutDicionarioRow(mOriginalCADRow);
		}
	}
}