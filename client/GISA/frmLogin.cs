using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using System.Threading;
using System.IO;
//using ParadigmaXis.Utils.Licensing;
using DBAbstractDataLayer;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA
{
	public class frmLogin : System.Windows.Forms.Form
	{

		[System.Runtime.InteropServices.DllImport("user32", EntryPoint="FindWindowA", ExactSpelling=true, CharSet=System.Runtime.InteropServices.CharSet.Ansi, SetLastError=true)]
		public static extern int FindWindow(string lpClassName, string lpWindowName);
		[System.Runtime.InteropServices.DllImport("user32", EntryPoint="SetForegroundWindow", ExactSpelling=true, CharSet=System.Runtime.InteropServices.CharSet.Ansi, SetLastError=true)]
		public static extern void SetForegroundWindow(long hwnd);
        
	#region  Windows Form Designer generated code 

		public frmLogin() : base()
		{

			try
			{
				Trace.WriteLine(string.Format("Starting Gisa @ {0}", DateTime.Now.ToString("u")));
			}
			catch (System.Configuration.ConfigurationException)
			{
				MessageBox.Show("O utilizador não possui as permissões de escrita necessárias " + Environment.NewLine + "ao correcto funcionamento da aplicação. " + Environment.NewLine + "Por favor contacte o administrador de sistema.", "Permissões insuficientes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				Environment.Exit(0);
			}

			CheckStartupConstraints();

			//This call is required by the Windows Form Designer.
			InitializeComponent();

            btnAccept.Click += btnAccept_Click;
            btnCancel.Click += btnCancel_Click;
            btnInfo.Click += btnInfo_Click;
            Label1.Click += Label1_Click;
            base.Activated += frmLogin_Activated;
            txtUser.KeyPress += txt_KeyPress;
            txtPassword.KeyPress += txt_KeyPress;
            base.Load += frmLogin_Load;
            base.MouseDown += frmLogin_MouseDown;
            base.MouseUp += frmLogin_MouseUp;
            base.MouseMove += frmLogin_MouseMove;

			Application.ThreadException += Application_ThreadException;

			//AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf Me.CurrentAppDomain_UnhandledException
			TextWriterTraceListener StdOutListener = new TextWriterTraceListener(Console.Out);
			TextWriterTraceListener StdErrListener = new TextWriterTraceListener(Console.Error);
			Trace.Listeners.Add(StdOutListener);
			Trace.Listeners.Add(StdErrListener);
			Trace.AutoFlush = true;

			Startup();

            Trace.WriteLine(string.Format("Gisa Version: {0}", lblVersion.Text));
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
		internal System.Windows.Forms.Button btnAccept;
		internal System.Windows.Forms.Button btnCancel;
		internal System.Windows.Forms.GroupBox grpRegistado;
		internal System.Windows.Forms.Label lblPassword;
		internal System.Windows.Forms.Label lblUser;
		internal System.Windows.Forms.TextBox txtPassword;
		internal System.Windows.Forms.TextBox txtUser;
		internal System.Windows.Forms.Panel pnlLogin;
		internal System.Windows.Forms.Button btnInfo;
		internal System.Windows.Forms.Label lblVersion;
		internal System.Windows.Forms.Label Label1;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpRegistado = new System.Windows.Forms.GroupBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.btnInfo = new System.Windows.Forms.Button();
            this.grpRegistado.SuspendLayout();
            this.pnlLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAccept.BackColor = System.Drawing.SystemColors.Control;
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccept.Location = new System.Drawing.Point(70, 67);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(80, 23);
            this.btnAccept.TabIndex = 6;
            this.btnAccept.Text = "Entrar";
            this.btnAccept.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(262, 67);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // grpRegistado
            // 
            this.grpRegistado.BackColor = System.Drawing.Color.Transparent;
            this.grpRegistado.Controls.Add(this.Label1);
            this.grpRegistado.Controls.Add(this.lblVersion);
            this.grpRegistado.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpRegistado.ForeColor = System.Drawing.Color.White;
            this.grpRegistado.Location = new System.Drawing.Point(64, 245);
            this.grpRegistado.Name = "grpRegistado";
            this.grpRegistado.Size = new System.Drawing.Size(352, 81);
            this.grpRegistado.TabIndex = 9;
            this.grpRegistado.TabStop = false;
            this.grpRegistado.Text = "Informação de registo ";
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(333, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(13, 13);
            this.Label1.TabIndex = 5;
            this.Label1.Text = "x";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblVersion
            // 
            this.lblVersion.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(24, 20);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(288, 16);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "GISA BASIC - Versão: 0.0";
            // 
            // pnlLogin
            // 
            this.pnlLogin.BackColor = System.Drawing.Color.Transparent;
            this.pnlLogin.Controls.Add(this.lblPassword);
            this.pnlLogin.Controls.Add(this.lblUser);
            this.pnlLogin.Controls.Add(this.txtPassword);
            this.pnlLogin.Controls.Add(this.txtUser);
            this.pnlLogin.Controls.Add(this.btnAccept);
            this.pnlLogin.Controls.Add(this.btnCancel);
            this.pnlLogin.Controls.Add(this.btnInfo);
            this.pnlLogin.Location = new System.Drawing.Point(40, 238);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(400, 96);
            this.pnlLogin.TabIndex = 10;
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.White;
            this.lblPassword.Location = new System.Drawing.Point(54, 36);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(88, 16);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "&Palavra-chave:";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUser
            // 
            this.lblUser.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUser.BackColor = System.Drawing.Color.Transparent;
            this.lblUser.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.Color.White;
            this.lblUser.Location = new System.Drawing.Point(54, 9);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(88, 16);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "&Utilizador:";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(150, 36);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(192, 20);
            this.txtPassword.TabIndex = 4;
            // 
            // txtUser
            // 
            this.txtUser.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtUser.Enabled = false;
            this.txtUser.Location = new System.Drawing.Point(150, 9);
            this.txtUser.MaxLength = 50;
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(192, 20);
            this.txtUser.TabIndex = 3;
            // 
            // btnInfo
            // 
            this.btnInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInfo.BackColor = System.Drawing.SystemColors.Control;
            this.btnInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInfo.Location = new System.Drawing.Point(166, 67);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(80, 23);
            this.btnInfo.TabIndex = 8;
            this.btnInfo.Text = "Informações";
            this.btnInfo.UseVisualStyleBackColor = false;
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(480, 350);
            this.Controls.Add(this.pnlLogin);
            this.Controls.Add(this.grpRegistado);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Iniciar sessão Gisa";
            this.grpRegistado.ResumeLayout(false);
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.ResumeLayout(false);

		}

	#endregion

		private class RegistrationException : Exception
		{
		}

		private class LoginException : Exception
		{
		}

		private void CheckStartupConstraints()
		{
			DBAbstractDataLayer.DataAccessRules.DALRule.ConnectionBuilder = new DBAbstractDataLayer.ConnectionBuilders.SqlClientBuilder();
	
			if (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().isMonoposto())
			{
				DBAbstractDataLayer.ConnectionBuilders.Builder.Server = null;
			}
			else
			{
                DBAbstractDataLayer.ConnectionBuilders.Builder.Server = ConfigurationManager.AppSettings["GISA.ServerLocation"];
			}

			// Verificar que a licença foi carregada e que contém informação quanto ao tipo de cliente
            if (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().Modules == null)
            {
                // causas possíveis (pedir log):
                //  - problema de acesso à base de dados
                //  - erro na licença
                MessageBox.Show("Não foi encontrada toda a informação de registo necessária, por favor contacte o " + Environment.NewLine + "fornecedor desta aplicação para obter uma licença válida.", GUIHelper.GUIHelper.getApplicationVersionHeader(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
		}

		private void Startup()
		{
			try
			{
				if (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().isMonoposto())
				{
					txtUser.Enabled = false;
					txtPassword.Enabled = false;
	#if TESTING
					txtUser.Enabled = true;
	#endif
				}
				else // cliente-servidor
				{
					txtUser.Enabled = true;
					txtPassword.Enabled = true;
				}

				txtUser.TextChanged += txt_TextChanged;
				txtPassword.TextChanged += txt_TextChanged;
				txt_TextChanged(txtUser, new EventArgs());

				FormGISAAbout.PopulateInformacoes(this.lblVersion);
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				MessageBox.Show("Não existe ou foi perdida a conetividade com o servidor " + Environment.NewLine + "configurado, a aplicação não poderá por isso ser utilizada.", "Servidor indisponível", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				Environment.Exit(0);
			}
		}

		private string GetRegistryValue(string valueName)
		{
			Microsoft.Win32.RegistryKey key = null;
			key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\ParadigmaXis\\GISA");

			string result = string.Empty;
			if (key != null)
			{
				result = key.GetValue(valueName, "").ToString();
				key.Close();
			}
			return result;
		}

		private void btnAccept_Click(object sender, System.EventArgs e)
		{

			// A 1ª instancia demora sempre mais tempo, é necessário carregar uma série de dados
			this.Cursor = Cursors.WaitCursor;
			GISADataset ds = GisaDataSetHelper.GetInstance();
			this.Cursor = Cursors.Arrow;

			if (ds == null)
			{
				MessageBox.Show("A base de dados não se encontra disponível, por favor " + Environment.NewLine + "contacte o administrador de sistema.", "Acesso à base de dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			frmMain main = null;
			try
			{
				string username = txtUser.Text.Replace("'", "''");
				string assemblyVersion = null;

                string modules = string.Empty;
                foreach(GISADataset.ModulesRow mRow in SessionHelper.AppConfiguration.GetCurrentAppconfiguration().Modules)
                {
                    modules += mRow.ID + ",";
                }
                modules = modules.TrimEnd(',');

                assemblyVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().FullName;
                
                // Validar utilizador
                TrusteeRule.IndexErrorMessages messageIndex = TrusteeRule.IndexErrorMessages.InvalidUser;

                // Carregar o utilizador 
				GISADataset.TrusteeUserRow tuRow = null;
				tuRow = LoadCurrentOperator(username);

                // LDAP Authentication
                string ldapServerName = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().LDAPServerName;
                string ldapSettings = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().LDAPServerSettings;

                if (ldapServerName != null && tuRow != null && tuRow.IsLDAPUser)
                {
                    LDAPHandler ldapH = new LDAPHandler(ldapServerName, ldapSettings, username, txtPassword.Text);
                    ldapH.ConnectToLDAP();

                    if (ldapH.IsLoggedIn)
                    {
                        messageIndex = 0;                        
                    }
                    else
                    {
                        messageIndex = TrusteeRule.IndexErrorMessages.InvalidUser;
                    }
                 
                }
                else
                {
                    messageIndex = Trustee.validateUser(username, CryptographyHelper.GetMD5(txtPassword.Text));
                }
				
				//verificar se ocorreu algum erro no processo de login
				if (messageIndex != 0)
				{
                    GUIHelper.GUIHelper.MessageBoxLoginErrorMessages(Convert.ToInt32(messageIndex));
					throw new LoginException();
				}
				
                // Adicionar o GisaPrincipal
                System.Threading.Thread.CurrentPrincipal = new GisaPrincipal(tuRow);
                                 
                if (SessionHelper.GetGisaPrincipal().TrusteePrivileges.Rows.Count == 0)
                {
                    GUIHelper.GUIHelper.MessageBoxLoginErrorMessages(Convert.ToInt32(DBAbstractDataLayer.DataAccessRules.TrusteeRule.IndexErrorMessages.UserWithoutPermissions));
                    throw new LoginException();
                }
                

				main = new frmMain(username);
			}
			catch (LoginException)
			{
				return;
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				MessageBox.Show("Ocorreu um erro durante o processo de início da sessão, " + Environment.NewLine + "por favor contacte o administrador de sistema.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			this.Hide();
			main.ShowDialog();
			this.Close();
		}

		private GISADataset.TrusteeUserRow LoadCurrentOperator(string username)
		{
			IDbConnection conn = GisaDataSetHelper.GetConnection();
			GISADataset.TrusteeRow[] tRowOperators = null;
			try
			{
				conn.Open();
				tRowOperators = (GISADataset.TrusteeRow[])(DBAbstractDataLayer.DataAccessRules.TrusteeRule.Current.LoadCurrentOperatorData(GisaDataSetHelper.GetInstance(), username, conn));
			}
			finally
			{
				conn.Close();
			}

			GISADataset.TrusteeUserRow tuRowOperator = null;

			if (tRowOperators.Length > 0)
			{
				tuRowOperator = tRowOperators[0].GetTrusteeUserRows()[0];
			}

			return tuRowOperator;
		}


		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			Trace.WriteLine(new String('*', 70));
			Trace.WriteLine("Unhandled exception follows:");
			Trace.WriteLine(e.Exception.Message);
			Trace.WriteLine(e.Exception.Source);
			Trace.WriteLine(e.Exception);
			if (e.Exception.InnerException != null)
			{
				Trace.WriteLine(e.Exception.InnerException);
			}
			Trace.WriteLine(new String('*', 70));
			Trace.Flush();
            if (e.Exception.GetType() == typeof(Search.SearchWebException))
            {
                MessageBox.Show("Ocorreu um erro de comunicação com o servidor de pesquisa, a" + Environment.NewLine + "aplicação terá de ser fechada. Se o problema persistir" + Environment.NewLine + "por favor contacte o administrador de sistema.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {            
                MessageBox.Show("Ocorreu um erro. A aplicação terá de ser fechada." + Environment.NewLine + "Por favor contacte o administrador de sistema.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);            
            }

			Environment.Exit(0);
		}

		private void btnInfo_Click(object sender, System.EventArgs e)
		{
			pnlLogin.Visible = false;
			btnAccept.Visible = false;
			btnCancel.Visible = false;
			this.CancelButton = btnInfo;
			grpRegistado.Visible = true;
		}

		private void Label1_Click(object sender, System.EventArgs e)
		{
			grpRegistado.Visible = false;
			pnlLogin.Visible = true;
			btnAccept.Visible = true;
			btnCancel.Visible = true;
			this.CancelButton = btnCancel;
		}

		private void frmLogin_Activated(object sender, System.EventArgs e)
		{
			if (txtUser.Enabled)
			{
				txtUser.Focus();
			}
			else
			{
				btnAccept.Focus();
			}
		}

		private void txt_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Enter))
			{
				btnAccept.PerformClick();
			}
		}

		private void txt_TextChanged(object sender, System.EventArgs e)
		{
			if (! (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().isMonoposto())) // cliente-servidor
			{
				if (txtUser.Text.Length > 0 && txtPassword.Text.Length > 0)
				{
					btnAccept.Enabled = true;
				}
				else
				{
	#if TESTING
					btnAccept.Enabled = true;
	#else
					btnAccept.Enabled = false;
	#endif
				}
			}
		}

		// mutex tem de ser declarado neste ambito por forma a não ser garbage collected
		private Mutex mutex;
		private void frmLogin_Load(object sender, System.EventArgs e)
		{
			try
			{
				// impedir existencia de multiplas instancias da aplicacao simultaneas
				mutex = new Mutex(false, "Global\\GISA_Client_Single_Instance");
			}
			catch (ApplicationException)
			{
				// não foi possível obter o mutex visto que já existe uma instância em execução na mesma máquina
				// e não temos permissões para lhe aceder (ex: existe outro user)
				mutex = null;
			}


	#if ! TESTING
			if (mutex == null || ! (mutex.WaitOne(1, true)))
			{
				this.Text = "Closing..";
				int hWndMain = 0;
				int hWndLogin = 0;
				hWndMain = FindWindow(null, "Gestão Integrada de Sistemas de Arquivo");
				hWndLogin = FindWindow(null, "Iniciar sessão Gisa");
				if (hWndMain != 0)
				{
					SetForegroundWindow(hWndMain);
				}
				else if (hWndLogin != 0 && hWndLogin != this.Handle.ToInt32())
				{
					SetForegroundWindow(hWndLogin);
				}
				else
				{
					MessageBox.Show("O Gisa já está a ser executado neste posto de trabalho.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				Trace.WriteLine("Já existe uma instancia da aplicação a correr.");
				Environment.Exit(0);
			}
	#endif
		}


	#region  Drag da janela de login 
		private bool IsMoving = false;
		private int MouseDownX;
		private int MouseDownY;
		private void frmLogin_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				IsMoving = true;
				MouseDownX = e.X;
				MouseDownY = e.Y;
			}
		}

		private void frmLogin_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				IsMoving = false;
			}
		}

		private void frmLogin_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (IsMoving)
			{
				Point temp = new Point();

				temp.X = this.Location.X + (e.X - MouseDownX);
				temp.Y = this.Location.Y + (e.Y - MouseDownY);
				this.Location = temp;
			}
		}
	#endregion
		

		[STAThread]
		static void Main()
		{
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            DirectoryInfo di = new DirectoryInfo(appData + @"\ParadigmaXis\GISA");
            if(!di.Exists)
            {
                di.Create();
            }
            System.Diagnostics.Trace.AutoFlush = true;
            System.Diagnostics.TextWriterTraceListener tl = new TextWriterTraceListener(di.FullName + @"\GISA.log", "GisaFileLogListener");
            System.Diagnostics.Trace.AutoFlush = true;
            System.Diagnostics.Trace.Listeners.Add(tl);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new frmLogin());
		}

	}

} //end of root namespace