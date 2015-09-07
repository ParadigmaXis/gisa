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
	public class ListTermos : System.Windows.Forms.UserControl
	{

		public delegate void TermoChangedEventHandler();
		public event TermoChangedEventHandler TermoChanged;
		public delegate void IncrementalSearchTextChangedEventHandler(string text);
		public event IncrementalSearchTextChangedEventHandler IncrementalSearchTextChanged;

	#region  Windows Form Designer generated code 

		public ListTermos() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

            rbCriar.CheckedChanged += rb_CheckedChanged;
            rbEscolher.CheckedChanged += rb_CheckedChanged;
            lstEscolher.KeyPress += lstEscolher_KeyPress;
            lstEscolher.KeyUp += lstEscolher_KeyUp;
            txtCriar.TextChanged += txtCriar_TextChanged;
            lstEscolher.Click += lstEscolher_Click;
            lstEscolher.SelectedIndexChanged += lstEscolher_SelectedIndexChanged;
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
		private System.ComponentModel.IContainer components = null;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		internal System.Windows.Forms.RadioButton rbCriar;
		internal System.Windows.Forms.GroupBox gbCriar;
		internal System.Windows.Forms.TextBox txtCriar;
        public System.Windows.Forms.RadioButton rbEscolher;
		internal System.Windows.Forms.GroupBox gbEscolher;
		internal System.Windows.Forms.ListBox lstEscolher;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.rbCriar = new System.Windows.Forms.RadioButton();
			this.gbCriar = new System.Windows.Forms.GroupBox();
			this.txtCriar = new System.Windows.Forms.TextBox();
			this.rbEscolher = new System.Windows.Forms.RadioButton();
			this.gbEscolher = new System.Windows.Forms.GroupBox();
			this.lstEscolher = new System.Windows.Forms.ListBox();
			this.gbCriar.SuspendLayout();
			this.gbEscolher.SuspendLayout();
			this.SuspendLayout();
			//
			//rbCriar
			//
			this.rbCriar.Checked = true;
			this.rbCriar.Location = new System.Drawing.Point(12, 3);
			this.rbCriar.Name = "rbCriar";
			this.rbCriar.Size = new System.Drawing.Size(145, 17);
			this.rbCriar.TabIndex = 1;
			this.rbCriar.TabStop = true;
			this.rbCriar.Text = "Escolher um novo termo";
			//
			//gbCriar
			//
			this.gbCriar.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.gbCriar.Controls.Add(this.txtCriar);
			this.gbCriar.Location = new System.Drawing.Point(0, 4);
			this.gbCriar.Name = "gbCriar";
			this.gbCriar.Size = new System.Drawing.Size(392, 49);
			this.gbCriar.TabIndex = 1;
			this.gbCriar.TabStop = false;
			//
			//txtCriar
			//
			this.txtCriar.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.txtCriar.Location = new System.Drawing.Point(9, 20);
			this.txtCriar.Name = "txtCriar";
			this.txtCriar.Size = new System.Drawing.Size(375, 20);
			this.txtCriar.TabIndex = 2;
			this.txtCriar.Text = "";
			//
			//rbEscolher
			//
			this.rbEscolher.Location = new System.Drawing.Point(12, 55);
			this.rbEscolher.Name = "rbEscolher";
			this.rbEscolher.Size = new System.Drawing.Size(177, 24);
			this.rbEscolher.TabIndex = 1;
			this.rbEscolher.Text = "Escolher um termo já existente";
			//
			//gbEscolher
			//
			this.gbEscolher.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.gbEscolher.Controls.Add(this.lstEscolher);
			this.gbEscolher.Enabled = false;
			this.gbEscolher.Location = new System.Drawing.Point(0, 60);
			this.gbEscolher.Name = "gbEscolher";
			this.gbEscolher.Size = new System.Drawing.Size(392, 172);
			this.gbEscolher.TabIndex = 2;
			this.gbEscolher.TabStop = false;
			//
			//lstEscolher
			//
			this.lstEscolher.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.lstEscolher.IntegralHeight = false;
			this.lstEscolher.Location = new System.Drawing.Point(9, 22);
			this.lstEscolher.Name = "lstEscolher";
			this.lstEscolher.Size = new System.Drawing.Size(375, 141);
			this.lstEscolher.TabIndex = 2;
			//
			//ListTermos
			//
			this.Controls.Add(this.rbCriar);
			this.Controls.Add(this.gbCriar);
			this.Controls.Add(this.rbEscolher);
			this.Controls.Add(this.gbEscolher);
			this.Name = "ListTermos";
			this.Size = new System.Drawing.Size(392, 236);
			this.gbCriar.ResumeLayout(false);
			this.gbEscolher.ResumeLayout(false);
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

		private string cachedNewlyCreatedDicionarioRow = string.Empty;
		public string ValidAuthorizedForm
		{
			get
			{
				if (rbEscolher.Checked)
				{
					// if none is selected "nothing" is returned
					if (lstEscolher.SelectedItem == null)
					{
						return null;
					}
					else
					{
						return lstEscolher.SelectedItem.ToString();
					}
				}
				else if (rbCriar.Checked)
				{
					// verificar se o termo escrito já existe ou ainda não foi preenchido. em tais casos não existe um valor válido e é por isso retornado nothing
					if (txtCriar.Text.Trim().Length == 0)
					{
						return null;
					}
					if (CheckAlreadyExist(txtCriar.Text.Trim()))
					{
						return null;
					}
                    return txtCriar.Text.Trim();
				}
				return null;
			}
			set
			{
				ClearIncrementalText();
				lstEscolher.SelectedItem = value;
			}
		}

		private bool CheckAlreadyExist(string termo)
		{
			foreach (GISADataset.DicionarioRow row in GisaDataSetHelper.GetInstance().Dicionario.Select("CatCode LIKE 'CA'"))
			{
				if (string.Compare(termo, row.Termo, false) == 0)
				{
					return true;
				}
			}
			return false;
		}

		public void LoadData()
		{
			// carrega todos
			LoadTermos(false, long.MinValue, null, long.MinValue);
			if (TermoChanged != null)
				TermoChanged();
		}

		public void LoadData(bool excludeAutorizados, long excludeAutorizadosTipoNoticiaAut)
		{
			// todos excepto as formas autorizadas do tipo de noticia de autoridade indicado
			LoadTermos(excludeAutorizados, excludeAutorizadosTipoNoticiaAut, null, long.MinValue);
			if (TermoChanged != null)
				TermoChanged();
		}

		public void LoadData(bool excludeAutorizados, long excludeAutorizadosTipoNoticiaAut, ArrayList includeOthers)
		{
			// todos excepto as formas autorizadas mas incluindo as que forem explicitamente especificads
			LoadTermos(excludeAutorizados, excludeAutorizadosTipoNoticiaAut, includeOthers, long.MinValue);
			if (TermoChanged != null)
				TermoChanged();
		}

		public void LoadData(long caID)
		{
			// todos excepto todas as formas do registo de autoridade actual (caID)
			LoadTermos(false, long.MinValue, null, caID);
			if (TermoChanged != null)
				TermoChanged();
		}

		private void LoadTermos(bool excludeAutorizados, long excludeAutorizadosTipoNoticiaAut, ArrayList includeOthers, long caID)
		{
			long start = 0;
			start = DateTime.Now.Ticks;

			ArrayList termosID = new ArrayList();
			// carregar para memória o conjunto de termos pretendidos
			IDbConnection conn = GisaDataSetHelper.GetConnection();
			try
			{
				conn.Open();
				bool constraints = GisaDataSetHelper.GetInstance().EnforceConstraints;
				GisaDataSetHelper.ManageDatasetConstraints(false);
				termosID = ControloAutRule.Current.LoadTermosData(GisaDataSetHelper.GetInstance(), excludeAutorizados, excludeAutorizadosTipoNoticiaAut, includeOthers, caID, conn);
				GisaDataSetHelper.ManageDatasetConstraints(constraints);
			}
			catch (System.Data.ConstraintException ex)
			{
				Trace.WriteLine(ex);
				Debug.Assert(false, ex.ToString());
				GisaDataSetHelper.FixDataSet(GisaDataSetHelper.GetInstance(), conn);
			}
			finally
			{
				conn.Close();
			}

			Debug.WriteLine("<<Load Termos>>: " + new TimeSpan(DateTime.Now.Ticks - start).ToString());

			start = DateTime.Now.Ticks;

			// popular a lista
			lstEscolher.BeginUpdate();
			lstEscolher.Items.Clear();
			lstEscolher.Items.AddRange(termosID.ToArray());
			lstEscolher.EndUpdate();

			Debug.WriteLine("<<Populate Termos>>: " + new TimeSpan(DateTime.Now.Ticks - start).ToString());

			if (lstEscolher.Items.Count > 0)
				lstEscolher.SelectedIndex = 0;
			else
			{
				gbEscolher.Enabled = false;
				rbEscolher.Enabled = false;
			}
		}

		private void rb_CheckedChanged(object sender, System.EventArgs e)
		{
			ClearIncrementalText();
			if (rbCriar.Checked)
			{
				gbEscolher.Enabled = false;
				gbCriar.Enabled = true;
				txtCriar.Focus();
				if (lstEscolher.SelectedItem != null)
				{
					txtCriar.Text = lstEscolher.SelectedItem.ToString();
					txtCriar.SelectAll();
				}
			}
			else if (rbEscolher.Checked)
			{
				txtCriar.Clear();
				gbCriar.Enabled = false;
				gbEscolher.Enabled = true;
				lstEscolher.Focus();
			}
			if (TermoChanged != null)
				TermoChanged();
		}

	#region  Pesquisa de texto na lista 
		private class DicionarioRowComparer : IComparer
		{

			public int Compare(object x, object y)
			{
				Debug.Assert(x is string);
				Debug.Assert(y is string);
				string xx = null;
				string yy = null;
				xx = (string)x;
				yy = (string)y;
				return string.Compare(xx, yy, true);
			}
		}

		private string incrementalText = string.Empty;
		private char incrementalCandidate;
		private void lstEscolher_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			incrementalCandidate = e.KeyChar;
			e.Handled = true;
		}

		private void lstEscolher_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Back:
					if (incrementalText.Length > 0)
					{
						incrementalText = incrementalText.Substring(0, incrementalText.Length - 1);
					}
					incrementalCandidate = char.MinValue;
					if (IncrementalSearchTextChanged != null)
						IncrementalSearchTextChanged(incrementalText);
					e.Handled = true;
					break;
				case Keys.Up:
				case Keys.Down:
				case Keys.Left:
				case Keys.Right:
					ClearIncrementalText();
					break;
				default:
					if (incrementalCandidate != char.MinValue)
					{
						incrementalText = incrementalText + incrementalCandidate;
						incrementalCandidate = char.MinValue;
						if (IncrementalSearchTextChanged != null)
							IncrementalSearchTextChanged(incrementalText);
						//StatusBarIncrementalText.Text = incrementalText
						e.Handled = true;
					}
					break;
			}

			if (e.Handled)
			{
				string[] Items = null;
				Items = new string[lstEscolher.Items.Count];
				lstEscolher.Items.CopyTo(Items, 0);

				int Position = System.Array.BinarySearch(Items, incrementalText, new DicionarioRowComparer());
				if (Position < 0) // Not found; hinting insertion point
				{
					lstEscolher.ClearSelected();
					if ((~ Position) < lstEscolher.Items.Count)
					{
						lstEscolher.TopIndex = (~ Position);
						lstEscolher.SelectedIndex = (~ Position);
					}
					else
					{
						lstEscolher.TopIndex = lstEscolher.Items.Count - 1;
						lstEscolher.SelectedIndex = lstEscolher.Items.Count - 1;
					}
				}
				else
				{
					lstEscolher.SelectedIndex = Position;
				}
			}
		}
	#endregion

		private void txtCriar_TextChanged(object sender, System.EventArgs e)
		{
			if (TermoChanged != null)
				TermoChanged();
		}

		private void ClearIncrementalText()
		{
			incrementalText = string.Empty;
            incrementalCandidate = char.MinValue;
			if (IncrementalSearchTextChanged != null)
				IncrementalSearchTextChanged(incrementalText);
		}

		private void lstEscolher_Click(object sender, System.EventArgs e)
		{
			ClearIncrementalText();
		}

		private void lstEscolher_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (TermoChanged != null)
				TermoChanged();
		}
	}
}