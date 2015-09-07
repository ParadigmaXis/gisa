using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
	public class SinglePanel : GISA.GISAControl
	{

	#region  Windows Form Designer generated code 

		public SinglePanel() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call

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
		protected internal System.Windows.Forms.Label lblFuncao;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.lblFuncao = new System.Windows.Forms.Label();
			this.pnlToolbarPadding.SuspendLayout();
			this.SuspendLayout();
			//
			//ToolBar
			//
			this.ToolBar.Name = "ToolBar";
			//
			//pnlToolbarPadding
			//
			this.pnlToolbarPadding.DockPadding.Left = 5;
			this.pnlToolbarPadding.DockPadding.Right = 5;
			this.pnlToolbarPadding.Name = "pnlToolbarPadding";
			//
			//lblFuncao
			//
			this.lblFuncao.BackColor = System.Drawing.Color.Gray;
			this.lblFuncao.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblFuncao.Font = new System.Drawing.Font("Arial", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.lblFuncao.ForeColor = System.Drawing.Color.White;
			this.lblFuncao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblFuncao.Location = new System.Drawing.Point(0, 27);
			this.lblFuncao.Name = "lblFuncao";
			this.lblFuncao.Size = new System.Drawing.Size(600, 24);
			this.lblFuncao.TabIndex = 1;
			this.lblFuncao.Text = "Título da função";
			this.lblFuncao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			//
			//SinglePanel
			//
			this.Controls.Add(this.lblFuncao);
			this.Name = "SinglePanel";
			this.Controls.SetChildIndex(this.lblFuncao, 0);
			this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
			this.pnlToolbarPadding.ResumeLayout(false);
			this.ResumeLayout(false);

		}

	#endregion


		public virtual bool UpdateContext()
		{
			return UpdateContext(null);
		}

		public virtual bool UpdateContext(ListViewItem item)
		{
    		return false;
		}

		public override void ActivateMessagePanel(GISAPanel messagePanel)
		{
			messagePanel.Visible = true;
            messagePanel.BringToFront();
		}

		public override void DeactivateMessagePanel(GISAPanel messagePanel)
		{
			if (messagePanel != null) // este caso acontece se estivermos num local em que simplesmente não seja necessário um messagepanel (e por isso não exista um)
			{
				messagePanel.Visible = false;
                messagePanel.SendToBack();
			}
			
			this.pnlToolbarPadding.Visible = true;
		}

        public override PersistencyHelper.SaveResult Save()
		{
			return Save(false);
		}

        public override PersistencyHelper.SaveResult Save(bool activateOpcaoCancelar)
		{
			return PersistencyHelper.save(activateOpcaoCancelar);
		}
	}
}