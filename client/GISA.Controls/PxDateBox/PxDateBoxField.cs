using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using System.ComponentModel;

namespace GISA.Controls
{
	
		internal enum PxDateBoxFieldType: int
		{
			Year = 0,
			Month = 1,
			Day = 2
		}

		internal class PxDateBoxField : TextBox
		{

			private const int WM_PASTE = 770;
			private const int WM_CONTEXTMENU = 123;


			public PxDateBoxField() : base()
			{
				this.BorderStyle = System.Windows.Forms.BorderStyle.None;
				this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
				this.Size = new System.Drawing.Size(28, 13);
				this.Text = "";
				this.TextAlign = HorizontalAlignment.Right;

				AddHandlers();
			}

			public void AddHandlers()
			{
                base.GotFocus += MyBase_GotFocus;
                base.Leave += MyBase_Leave;

				base.Validating += MyBase_Validating;
				base.KeyPress += MyBase_KeyPress;
				base.KeyDown += MyBase_KeyDown;
				base.TextChanged += MyBase_TextChanged;
			}

			protected override void WndProc(ref Message msg)
			{
				switch (msg.Msg)
				{
					case WM_PASTE: // to disable pasting
					break;
					case WM_CONTEXTMENU: //to disable the right clicking contect menu
					break;
					default:
						base.WndProc(ref msg);
						break;
				}
			}

	#region  Propriedades adicionadas ao evento 
			[Browsable(false)]
			public new int MaxLength
			{
				get
				{
					return base.MaxLength;
				}
				set
				{
					base.MaxLength = value;
				}
			}

			private PxDateBoxFieldType mType = PxDateBoxFieldType.Day;
			[Browsable(true), Description("TextBox type (year, month or day)."), Category("Behavior")]
			public PxDateBoxFieldType Type
			{
				get
				{
					return mType;
				}
				set
				{
					mType = value;
					switch (mType)
					{
						case PxDateBoxFieldType.Year:
							this.MaxLength = 4;
							break;
						case PxDateBoxFieldType.Month:
						case PxDateBoxFieldType.Day:
							this.MaxLength = 2;
							break;
					}
				}
			}
	#endregion

			private void MyBase_GotFocus(object sender, System.EventArgs e)
			{
				this.SelectAll();
			}

			public void MyBase_KeyDown(object sender, KeyEventArgs e)
			{
				if (e.KeyCode == Keys.Right && this.SelectionStart + this.SelectionLength == this.Text.Length)
				{
					if (NextFocus != null)
						NextFocus(this.SelectionStart);
				}
				if (e.KeyCode == Keys.Left && this.SelectionStart == 0)
				{
					if (PreviousFocus != null)
						PreviousFocus(this.SelectionStart);
				}
			}

	#region  Eventos que despoletam validações 
			public void MyBase_Validating(object sender, System.ComponentModel.CancelEventArgs e)
			{

				if (! (currentDatePartValidator(this.Text)))
				{
					e.Cancel = true;
				}
				else
				{
					e.Cancel = false;
				}
			}

			// este evento resolve a maioria dos problemas com a introdução 
			// de valores inválidos nas datas, no entanto, ficam por tratar 
			// outros modos de introdução dos dados (por exemplo fazendo "paste").
			// embora geralmente não seja um problema pode se-lo nos casos em
			// que se torne necessário forçar um endedit.
			public void MyBase_KeyPress(object sender, KeyPressEventArgs e)
			{
				if (! (char.IsDigit(e.KeyChar)) && ! (e.KeyChar == '?') && ! (char.IsControl(e.KeyChar)))
				{
					e.Handled = true;
				}
			}

			public delegate void PreviousFocusEventHandler(int position);
			public event PreviousFocusEventHandler PreviousFocus;
			public delegate void NextFocusEventHandler(int position);
			public event NextFocusEventHandler NextFocus;
			private void MyBase_TextChanged(object sender, System.EventArgs e)
			{
				if (this.Text.Length != this.Text.Trim().Length)
				{
					base.TextChanged -= MyBase_TextChanged;
					this.Text = this.Text.Trim();
					base.TextChanged += MyBase_TextChanged;
				}
				// se a parcela da data for inválida desfaz-se a acção do utilizador e limpa-se o undo para que o utilizador não possa recuperar o valor errado.
				while (! (currentDatePartValidator(this.Text)) && this.CanUndo)
				{
					this.Undo();
				}
				this.ClearUndo();
				// Se preenchermos o campo até ao limite passamos automaticamente ao próximo.
				// No entanto, só o fazemos se o campo actual tiver o foco. Desta forma 
				// conseguimos que a mudança de foco não seja despoletada quando os valores 
				// do controlo são definidos programaticamente.
				
                /*
                if (this.Text.Length == this.MaxLength && this.Focused)
				{
					if (NextFocus != null)
						NextFocus(this.Text.Length);
				}
                */
			}
	#endregion

			private void MyBase_Leave(object sender, System.EventArgs e)
			{
				switch (Type)
				{
					case PxDateBoxFieldType.Year:
						if (this.IsValid() && (this.Text.IndexOf("?") == -1) & this.Text.Length > 0)
						{

							int dif = 4 - this.Text.Length;
							if (dif > 0)
							{
								string a = new String('0', dif);
								this.Text = a + this.Text;
							}
						}
						break;
					case PxDateBoxFieldType.Month:
						if (this.IsValid() && (this.Text.IndexOf("?") == -1))
						{
							if (this.Text.Length == 1)
							{
								this.Text = "0" + this.Text;
							}
						}
						break;
					case PxDateBoxFieldType.Day:
						if (this.IsValid() && (this.Text.IndexOf("?") == -1))
						{
							if (this.Text.Length == 1)
							{
								this.Text = "0" + this.Text;
							}
						}
						break;
				}

			}

			private delegate bool DatePartValidator(string part);
			private DatePartValidator currentDatePartValidator
			{
				get
				{
					switch (Type)
					{
						case PxDateBoxFieldType.Year:
							return DateHelper.IsValidYear;
						case PxDateBoxFieldType.Month:
                            return DateHelper.IsValidMonth;
						case PxDateBoxFieldType.Day:
                            return DateHelper.IsValidDay;
						default:
							//nunca deverá acontecer
							throw new Exception("Data não reconhecida");
					}
				}
			}

			public bool IsValid()
			{
				return currentDatePartValidator(this.Text);
			}
		}
} //end of root namespace