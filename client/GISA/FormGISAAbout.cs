//INSTANT C# NOTE: Formerly VB.NET project-level imports:
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
	public class FormGISAAbout : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public FormGISAAbout() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
			LoadListViewAssemblies();
			FormGISAAbout.PopulateInformacoes(this.lblVersion);
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
		internal System.Windows.Forms.ColumnHeader ColumnHeader1;
		internal System.Windows.Forms.ColumnHeader ColumnHeader2;
		internal System.Windows.Forms.ListView ListViewAssemblies;
		internal System.Windows.Forms.ColumnHeader ColumnHeader4;
		internal System.Windows.Forms.ColumnHeader ColumnHeader3;
		internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.Label lblVersion;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label6;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGISAAbout));
            this.btnClose = new System.Windows.Forms.Button();
            this.ListViewAssemblies = new System.Windows.Forms.ListView();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Label3 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(551, 398);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(72, 24);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "&Fechar";
            // 
            // ListViewAssemblies
            // 
            this.ListViewAssemblies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListViewAssemblies.AutoArrange = false;
            this.ListViewAssemblies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ListViewAssemblies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.ColumnHeader2,
            this.ColumnHeader3,
            this.ColumnHeader4});
            this.ListViewAssemblies.FullRowSelect = true;
            this.ListViewAssemblies.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ListViewAssemblies.Location = new System.Drawing.Point(182, 306);
            this.ListViewAssemblies.MultiSelect = false;
            this.ListViewAssemblies.Name = "ListViewAssemblies";
            this.ListViewAssemblies.Size = new System.Drawing.Size(440, 71);
            this.ListViewAssemblies.TabIndex = 0;
            this.ListViewAssemblies.UseCompatibleStateImageBehavior = false;
            this.ListViewAssemblies.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "Biblioteca";
            this.ColumnHeader1.Width = 100;
            // 
            // ColumnHeader2
            // 
            this.ColumnHeader2.Text = "Versão";
            this.ColumnHeader2.Width = 64;
            // 
            // ColumnHeader3
            // 
            this.ColumnHeader3.Text = "GAC";
            this.ColumnHeader3.Width = 46;
            // 
            // ColumnHeader4
            // 
            this.ColumnHeader4.Text = "Localização";
            this.ColumnHeader4.Width = 210;
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.Color.White;
            this.Label3.Location = new System.Drawing.Point(14, 393);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(528, 48);
            this.Label3.TabIndex = 5;
            this.Label3.Text = resources.GetString("Label3.Text");
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.White;
            this.lblVersion.Location = new System.Drawing.Point(184, 122);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(440, 16);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "GISA - Versão: 0.0";
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.White;
            this.Label1.Location = new System.Drawing.Point(184, 104);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(440, 16);
            this.Label1.TabIndex = 11;
            this.Label1.Text = "GISA Open Source";
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.Color.White;
            this.Label2.Location = new System.Drawing.Point(184, 218);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(440, 23);
            this.Label2.TabIndex = 12;
            this.Label2.Text = "Desenvolvimento: ParadigmaXis - Arquitectura e Engenharia de Software S.A.";
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.ForeColor = System.Drawing.Color.White;
            this.Label4.Location = new System.Drawing.Point(184, 234);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(440, 32);
            this.Label4.TabIndex = 13;
            this.Label4.Text = "Conceção: Câmaras Municipais do Porto, Espinho, Vila do Conde, V. N. Gaia e Unive" +
    "rsidade do Porto";
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.Color.White;
            this.Label5.Location = new System.Drawing.Point(184, 200);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(440, 16);
            this.Label5.TabIndex = 14;
            this.Label5.Text = "Entidades";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.Transparent;
            this.Label6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.ForeColor = System.Drawing.Color.White;
            this.Label6.Location = new System.Drawing.Point(184, 290);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(432, 16);
            this.Label6.TabIndex = 15;
            this.Label6.Text = "Bibliotecas utilizadas";
            // 
            // FormGISAAbout
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(641, 437);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.ListViewAssemblies);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGISAAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Acerca de ...";
            this.ResumeLayout(false);

		}

	#endregion

		private void LoadListViewAssemblies()
		{
			foreach (System.Reflection.Assembly Asm in AppDomain.CurrentDomain.GetAssemblies())
			{
				ListViewItem lvi = new ListViewItem(Asm.GetName().Name);
				lvi.SubItems.Add(Asm.GetName().Version.ToString());
				if (Asm.GlobalAssemblyCache)
				{
					lvi.SubItems.Add("Sim");
				}
				else
				{
					lvi.SubItems.Add("Não");
				}

				string location = "";
				try
				{
					location = Asm.Location;
				}
				catch (NotSupportedException)
				{
					// Ignore - this error is sometimes thrown by asm.Location for certain dynamic assemblies
				}
				lvi.SubItems.Add(location);
				ListViewAssemblies.Items.Add(lvi);
			}
		}

		public static void PopulateInformacoes(Label lblVersion)
		{
			lblVersion.Text = getVersion();
	    }

		private static string getVersion()
		{
			string version = string.Empty;

			Microsoft.Win32.RegistryKey key = null;
			key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\ParadigmaXis\\GISA");
			if (key != null)
			{
				string versionRegistry = null;
				string versionAssembly = null;
				string versionSvn = null;
				// TODO: passar a ler esta informação do ficheiro de licença em vez do registry
				versionRegistry = key.GetValue("Version", string.Empty).ToString().Trim();
				versionAssembly = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Trim();
				versionSvn = Application.ProductVersion;
				if (versionRegistry.Equals(versionAssembly) || versionRegistry.Length == 0)
				{
					version = string.Format("{0} {1}", versionAssembly, versionSvn);
				}
				else
				{
					version = string.Format("{0} {1}  ({2})", versionAssembly, versionSvn, versionRegistry);
				}
				string tipoServerName = null;
				if (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().isMonoposto())
				{
					tipoServerName = "Monoposto";
				}
				else // cliente-servidor
				{
					tipoServerName = "Cliente-Servidor";
				}
				version = string.Format("{0} - Versão: {1}", tipoServerName, version);
				key.Close();
			}
			else
			{
				string.Format("{0} - Versão: {1}", SessionHelper.AppConfiguration.GetCurrentAppconfiguration().TipoServer.Name, "0.0");
			}
			return version;
		}
	}

} //end of root namespace