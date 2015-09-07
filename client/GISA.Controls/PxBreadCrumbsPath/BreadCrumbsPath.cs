using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GISA.Controls
{
	/// <summary>
	/// Summary description for BreadCrumbsPath.
	/// </summary>
	public class BreadCrumbsPath : System.Windows.Forms.UserControl
	{
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolTip toolTip;
		public static ImageList mImgList;
		public BreadCrumbsPath()
		{
			InitializeComponent();
			GetExtraResources();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BreadCrumbsPath));
            this.end = new System.Windows.Forms.Button();
            this.begin = new System.Windows.Forms.Button();
            this.upLevel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.breadCrumbsPanel = new System.Windows.Forms.Panel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // end
            // 
            this.end.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.end.BackColor = System.Drawing.SystemColors.Control;
            this.end.Enabled = false;
            this.end.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.end.ForeColor = System.Drawing.Color.Black;
            this.end.Location = new System.Drawing.Point(572, 3);
            this.end.Name = "end";
            this.end.Size = new System.Drawing.Size(16, 24);
            this.end.TabIndex = 12;
            this.end.Text = ">";
            this.end.UseVisualStyleBackColor = false;
            this.end.Visible = false;
            this.end.Click += new System.EventHandler(this.end_Click);
            // 
            // begin
            // 
            this.begin.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.begin.BackColor = System.Drawing.SystemColors.Control;
            this.begin.Enabled = false;
            this.begin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.begin.ForeColor = System.Drawing.Color.Black;
            this.begin.Location = new System.Drawing.Point(554, 3);
            this.begin.Name = "begin";
            this.begin.Size = new System.Drawing.Size(16, 24);
            this.begin.TabIndex = 11;
            this.begin.Text = "<";
            this.begin.UseVisualStyleBackColor = false;
            this.begin.Visible = false;
            this.begin.Click += new System.EventHandler(this.begin_Click);
            // 
            // upLevel
            // 
            this.upLevel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.upLevel.BackColor = System.Drawing.SystemColors.Control;
            this.upLevel.Enabled = false;
            this.upLevel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.upLevel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.upLevel.Image = ((System.Drawing.Image)(resources.GetObject("upLevel.Image")));
            this.upLevel.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.upLevel.Location = new System.Drawing.Point(590, 3);
            this.upLevel.Name = "upLevel";
            this.upLevel.Size = new System.Drawing.Size(24, 24);
            this.upLevel.TabIndex = 10;
            this.upLevel.UseVisualStyleBackColor = false;
            this.upLevel.Click += new System.EventHandler(this.upLevel_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.breadCrumbsPanel);
            this.panel2.ForeColor = System.Drawing.Color.Silver;
            this.panel2.Location = new System.Drawing.Point(5, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(543, 24);
            this.panel2.TabIndex = 13;
            this.panel2.Resize += new System.EventHandler(this.panel2_Resize);
            // 
            // breadCrumbsPanel
            // 
            this.breadCrumbsPanel.BackColor = System.Drawing.Color.White;
            this.breadCrumbsPanel.Location = new System.Drawing.Point(0, 0);
            this.breadCrumbsPanel.Name = "breadCrumbsPanel";
            this.breadCrumbsPanel.Padding = new System.Windows.Forms.Padding(5);
            this.breadCrumbsPanel.Size = new System.Drawing.Size(543, 24);
            this.breadCrumbsPanel.TabIndex = 4;
            // 
            // toolTip
            // 
            this.toolTip.ShowAlways = true;
            // 
            // BreadCrumbsPath
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.end);
            this.Controls.Add(this.begin);
            this.Controls.Add(this.upLevel);
            this.Controls.Add(this.panel2);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "BreadCrumbsPath";
            this.Size = new System.Drawing.Size(620, 30);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void GetExtraResources()
		{
			toolTip.SetToolTip(this.upLevel, "Nível superior");
			toolTip.SetToolTip(this.begin, "Mostrar caminho mais à esquerda");
			toolTip.SetToolTip(this.end, "Mostrar caminho mais à direita");
		}

		#region Members
		private int mLastPosition = 0;
		private int mSpacing = 3;
		private int mPaddingTop = 6;
		private System.Windows.Forms.Button end;
		private System.Windows.Forms.Button begin;
		private System.Windows.Forms.Button upLevel;
		private System.Windows.Forms.Panel breadCrumbsPanel;
		private System.Windows.Forms.Panel panel2;
        private List<BreadCrumb> mPath = new List<BreadCrumb>();
		#endregion

		public long getBreadCrumbsPathContextID
		{
			get
			{
                if (mPath != null && mPath.Count > 0)
                    return mPath[mPath.Count - 1].idNivel;
                else
                    return long.MinValue;
			}
		}

		public long getBreadCrumbsPathContextIDUpper
		{
			get
			{
                if (mPath != null && mPath.Count > 0)
                    return mPath[mPath.Count - 1].idUpperNivel;
                else
                    return long.MinValue;
			}
		}
		
		public List<BreadCrumb> Path 
		{
			get {return mPath;}
		}		
		
		#region Add / Remove Bread Crumbs
		public void AddBreadCrumb(string designacao, long idNivel, long idNivelUpper, int imageIndex) 
		{	
			// adicionar o bread crumb ao arraylist
			BreadCrumbsPath.BreadCrumb bc = new BreadCrumbsPath.BreadCrumb(designacao, idNivel, idNivelUpper);
			mPath.Add(bc);

            CreateBreadCrumbControl(designacao, imageIndex, bc);
		}

        private void CreateBreadCrumbControl(string designacao, int imageIndex, BreadCrumbsPath.BreadCrumb bc)
        {
            // componente gráfico correspondente ao novo elemento do caminho
            LinkLabel lLbl;
            lLbl = new LinkLabel();
            lLbl.Text = designacao;
            lLbl.Tag = bc;
            lLbl.AutoSize = true;
            lLbl.Top = mPaddingTop;
            lLbl.Click += new EventHandler(this.lLbl_Click);

            // icone que ilustra o tipo de elemento a adicionar ao caminho
            Label iconLbl;
            iconLbl = new Label();
            iconLbl.Top = 2;
            iconLbl.ImageList = mImgList;
            iconLbl.ImageIndex = imageIndex;

            iconLbl.Width = 20;

            Label lbl;
            lbl = new Label();
            lbl.Text = ">";
            lbl.Top = mPaddingTop;
            lbl.AutoSize = true;

            //  testar se o novo elemento do caminho não ultrapassa o espaço disponivel
            if (mLastPosition == 0)
            {
                // o controlo gráfico ainda não contem nenhum elemento
                iconLbl.Left = mLastPosition + mSpacing;
                breadCrumbsPanel.Controls.Add(iconLbl);
                mLastPosition += mSpacing + iconLbl.Width;

                lLbl.Left = mLastPosition;
            }
            else if ((breadCrumbsPanel.Width - mLastPosition) > (mSpacing + lbl.Width + mSpacing + lLbl.Width + mSpacing))
            {
                //existe espaço para adicionar o novo elemento ao controlo gráfico

                lbl.Left = mLastPosition;
                breadCrumbsPanel.Controls.Add(lbl);
                mLastPosition += mSpacing + lbl.Width;

                iconLbl.Left = mLastPosition;
                breadCrumbsPanel.Controls.Add(iconLbl);
                mLastPosition += iconLbl.Width;

                lLbl.Left = mLastPosition;
            }
            else
            {
                // não existe espaço para adicionar o novo elemento do caminho no controlo gráfico
                // pelo que é necessário reorganizar o seu conteúdo (é garantido que o último elemento
                // inserido no controlo fica visível)

                int acerto = ((mSpacing + lbl.Width + mSpacing + iconLbl.Width + lLbl.Width) - (breadCrumbsPanel.Width - mLastPosition));
                breadCrumbsPanel.Width += acerto;
                breadCrumbsPanel.Left = panel2.Width - breadCrumbsPanel.Width;
                begin.Enabled = true;
                begin.Visible = true;
                end.Visible = true;

                lbl.Left = mLastPosition;
                breadCrumbsPanel.Controls.Add(lbl);
                mLastPosition += mSpacing + lbl.Width;

                iconLbl.Left = mLastPosition;
                breadCrumbsPanel.Controls.Add(iconLbl);
                mLastPosition += iconLbl.Width;

                lLbl.Left = mLastPosition;
            }

            breadCrumbsPanel.Controls.Add(lLbl);
            mLastPosition += mSpacing + lLbl.Width + mSpacing;

            if (mPath.Count > 1)
                upLevel.Enabled = true;
        }

		public void ResetBreadCrumbsPath()
		{
			Control c;
			mPath.Clear();
			upLevel.Enabled = false;
			mLastPosition = 0;
			for (int i = 0; i < breadCrumbsPanel.Controls.Count; i++) 
			{
				c = breadCrumbsPanel.Controls[i];
				if (c.GetType() == typeof(LinkLabel)) 
				{
					((LinkLabel) c).Click -= new EventHandler(this.lLbl_Click);
				}
			}
			breadCrumbsPanel.Controls.Clear();
		}

		private void DeleteLastBreadCrumb() 
		{
			// remover o objeto do array que mantem os elementos que compoem o caminho
			mPath.RemoveAt(mPath.Count - 1);
				
			// remover o último elemento gráfico do controlo
			int bcWidth = breadCrumbsPanel.Controls[breadCrumbsPanel.Controls.Count - 1].Width;
			LinkLabel lLbl = (LinkLabel) breadCrumbsPanel.Controls[breadCrumbsPanel.Controls.Count - 1];
			lLbl.Click -= new EventHandler(this.lLbl_Click);
			breadCrumbsPanel.Controls.RemoveAt(breadCrumbsPanel.Controls.Count - 1);

			// remover o icon do último elemento gráfico do controlo
			int iconWidth = breadCrumbsPanel.Controls[breadCrumbsPanel.Controls.Count - 1].Width;
			breadCrumbsPanel.Controls.RemoveAt(breadCrumbsPanel.Controls.Count - 1);
				
			// remover o último separador entre elementos do caminho do controlo
			int sepWidth = breadCrumbsPanel.Controls[breadCrumbsPanel.Controls.Count - 1].Width;
			breadCrumbsPanel.Controls.RemoveAt(breadCrumbsPanel.Controls.Count - 1);

			//actualizar variavel mLastPosition
			mLastPosition -= (mSpacing + bcWidth + iconWidth + mSpacing + sepWidth + mSpacing);

			// actualizar o tamanho do painel que mantem os elementos graficos do caminho
			// no caso de estes serem visiveis no seu conjunto e nessa situação garantir que
			// último elemento adicionado está visível
			if (breadCrumbsPanel.Width > panel2.Width) 
			{
				if ( (breadCrumbsPanel.Width - panel2.Width) > (mSpacing + bcWidth + iconWidth + mSpacing + sepWidth + mSpacing)) 
				{
					breadCrumbsPanel.Width -= (mSpacing + bcWidth + iconWidth + mSpacing + sepWidth + mSpacing);
					breadCrumbsPanel.Left = panel2.Width - breadCrumbsPanel.Width;
					end.Enabled = false;
					begin.Enabled = true;
					begin.Visible = true;
					end.Visible = true;
				}
				else
				{
					breadCrumbsPanel.Width = panel2.Width;
					breadCrumbsPanel.Left = 0;
					begin.Enabled = false;
					end.Enabled = false;
					begin.Visible = false;
					end.Visible = false;
				}

			}
		}
		#endregion
		
		#region Events
		private void upLevel_Click(object sender, System.EventArgs e) 
		{
			if (mPath.Count > 1)
			{
				long oldNivelID = ((BreadCrumb)mPath[mPath.Count-1]).idNivel;
				DeleteLastBreadCrumb();
				
				// selecionar o agora último elemento e carregar os seus dados
				BreadCrumb bc = ((BreadCrumb)mPath[mPath.Count-1]);
				NewBreadCrumbSelectedEventArgs args = new NewBreadCrumbSelectedEventArgs(bc.idNivel, oldNivelID);
				this.OnNewBreadCrumbSelected(args);
			}
			// garantir que no controlo existe sempre no mínimo um elemento no controlo
			if (mPath.Count == 1) 
				upLevel.Enabled = false;
		}        

		// ajustar elementos do caminho às novas dimensões do controlo
		private void panel2_Resize(object sender, System.EventArgs e)
		{
			if (mLastPosition > panel2.Width) 
			{
				breadCrumbsPanel.Width = mLastPosition;
				breadCrumbsPanel.Left = panel2.Width - mLastPosition;
				begin.Enabled = true;
				end.Enabled = false;
				begin.Visible = true;
				end.Visible = true;
			}
			else
			{
				breadCrumbsPanel.Width = panel2.Width;
				breadCrumbsPanel.Left = 0;
				begin.Enabled = false;
				end.Enabled = false;
				begin.Visible = false;
				end.Visible = false;
			}
		}

		private void begin_Click(object sender, System.EventArgs e)
		{
			end.Enabled = true;
			if (breadCrumbsPanel.Left < -10) 
				breadCrumbsPanel.Left += 10;
			else
			{
				breadCrumbsPanel.Left = 0;
				begin.Enabled = false;
			}
		}

		private void end_Click(object sender, System.EventArgs e)
		{
			begin.Enabled = true;
			if (breadCrumbsPanel.Left > (panel2.Width - breadCrumbsPanel.Width + 10) ) 
				breadCrumbsPanel.Left -= 10;
			else
			{
				breadCrumbsPanel.Left = (panel2.Width - breadCrumbsPanel.Width);
				end.Enabled = false;
			} 
		}
	
		public void lLbl_Click(object sender, System.EventArgs e) 
		{
            BreadCrumb bc = ((BreadCrumb)((LinkLabel) sender).Tag);

			// apagar todos os elementos do controlo que sucedem o elemento selecionado
			long oldNivelID = long.MinValue;
			int bcToDelete = mPath.Count - (mPath.IndexOf(bc) + 1);
			while (bcToDelete > 0) 
			{
				oldNivelID = ((BreadCrumb)mPath[mPath.Count-1]).idNivel;
				DeleteLastBreadCrumb();
				bcToDelete--;
			}
			
			if (mPath.Count == 1)
				upLevel.Enabled = false;

			NewBreadCrumbSelectedEventArgs args = new NewBreadCrumbSelectedEventArgs(bc.idNivel, oldNivelID);
			this.OnNewBreadCrumbSelected(args);
		}

		public event NewBreadCrumbSelectedEventHandler NewBreadCrumbSelected;
		public delegate void NewBreadCrumbSelectedEventHandler(object sender, NewBreadCrumbSelectedEventArgs e);

		protected virtual void OnNewBreadCrumbSelected(NewBreadCrumbSelectedEventArgs e)
		{
			if (this.NewBreadCrumbSelected != null)
				NewBreadCrumbSelected(this, e);
		}

		// argumentos do evento de selecção de um elemento do caminho
		public class NewBreadCrumbSelectedEventArgs: EventArgs 
		{
			public long mIdNivelBCContext;
			public long mIdNivelLVContext;
			public NewBreadCrumbSelectedEventArgs(long idNivelBCContext, long idNivelLVContext)
			{
				this.mIdNivelBCContext = idNivelBCContext;
				this.mIdNivelLVContext = idNivelLVContext;
			}
		}
		#endregion

		public class BreadCrumb 
		{
			public string name;
			public long idNivel;
			public long idUpperNivel;

            // quando o bread crumb é referente a um nível orgânico
            public BreadCrumb(string name, long idNivel)
            {
                this.name = name;
                this.idNivel = idNivel;
            }

			public BreadCrumb(string name, long idNivel, long idUpperNivel){
				this.name = name;
				this.idNivel = idNivel;
				this.idUpperNivel = idUpperNivel;
			}
		}
	}
}
