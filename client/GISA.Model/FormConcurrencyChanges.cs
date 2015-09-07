//INSTANT C# NOTE: Formerly VB.NET project-level imports:
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;

using System.Windows.Forms;

namespace GISA.Model
{
	public class FormConcurrencyChanges : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public FormConcurrencyChanges() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            btnExportar.Click += new System.EventHandler(btnExportar_Click);
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
		internal System.Windows.Forms.Button btnFechar;
		internal System.Windows.Forms.Button btnExportar;
		internal System.Windows.Forms.RichTextBox rtbAlteracoes;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormConcurrencyChanges));
			this.rtbAlteracoes = new System.Windows.Forms.RichTextBox();
			this.btnFechar = new System.Windows.Forms.Button();
			this.btnExportar = new System.Windows.Forms.Button();
			this.SuspendLayout();
			//
			//rtbAlteracoes
			//
			this.rtbAlteracoes.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.rtbAlteracoes.Location = new System.Drawing.Point(4, 4);
			this.rtbAlteracoes.Name = "rtbAlteracoes";
			this.rtbAlteracoes.Size = new System.Drawing.Size(624, 284);
			this.rtbAlteracoes.TabIndex = 0;
			this.rtbAlteracoes.Text = "";
			//
			//btnFechar
			//
			this.btnFechar.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnFechar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnFechar.Location = new System.Drawing.Point(528, 300);
			this.btnFechar.Name = "btnFechar";
			this.btnFechar.TabIndex = 1;
			this.btnFechar.Text = "Fechar";
			//
			//btnExportar
			//
			this.btnExportar.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnExportar.Location = new System.Drawing.Point(436, 300);
			this.btnExportar.Name = "btnExportar";
			this.btnExportar.TabIndex = 2;
			this.btnExportar.Text = "Exportar";
			//
			//FormConcurrencyChanges
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnFechar;
			this.ClientSize = new System.Drawing.Size(632, 333);
			this.Controls.Add(this.btnExportar);
			this.Controls.Add(this.btnFechar);
			this.Controls.Add(this.rtbAlteracoes);
			this.Icon = (System.Drawing.Icon)(resources.GetObject("$this.Icon"));
			this.Name = "FormConcurrencyChanges";
			this.Text = "Alterações";
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

		private void btnExportar_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog mSaveDialog = new SaveFileDialog();
			mSaveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			mSaveDialog.AddExtension = true;
			mSaveDialog.DefaultExt = "rtf";
			mSaveDialog.Filter = "Rich Text Format (*.rtf)|*.rtf";
			mSaveDialog.OverwritePrompt = true;
			mSaveDialog.ValidateNames = true;
			mSaveDialog.FileName = "Alteracoes" + DateTime.Now.ToString("yyyyMMdd") + ".rtf";
			if (mSaveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				mSaveDialog.InitialDirectory = new System.IO.FileInfo(mSaveDialog.FileName).Directory.ToString();
				rtbAlteracoes.SaveFile(mSaveDialog.FileName);
			}
		}
	}

} //end of root namespace