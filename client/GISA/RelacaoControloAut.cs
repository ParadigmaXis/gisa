using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
	public class RelacaoControloAut : GISA.Relacao
	{

	#region  Windows Form Designer generated code 

		public RelacaoControloAut() : base()
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
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			//
			//lblTipoNivel
			//
			this.lblTipoNivel.Name = "lblTipoNivel";
			//
			//cbTipoNivel
			//
			this.cbTipoNivel.Name = "cbTipoNivel";
			this.cbTipoNivel.Size = new System.Drawing.Size(234, 21);
			this.cbTipoNivel.TabIndex = 2;
			//
			//cbTipoControloAutRel
			//
			this.cbTipoControloAutRel.Name = "cbTipoControloAutRel";
			this.cbTipoControloAutRel.Size = new System.Drawing.Size(172, 21);
			this.cbTipoControloAutRel.TabIndex = 1;
			//
			//lblTipocontroloAutRel
			//
			this.lblTipocontroloAutRel.Name = "lblTipocontroloAutRel";
			//
			//RelacaoControloAut
			//
			this.Name = "RelacaoControloAut";

		}

	#endregion

	}

} //end of root namespace