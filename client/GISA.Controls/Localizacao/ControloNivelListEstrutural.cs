using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA.Controls.Localizacao
{
	public class ControloNivelListEstrutural : ControloNivelList
	{

	#region  Windows Form Designer generated code 

		public ControloNivelListEstrutural() : base()
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
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}

	#endregion

        public override bool isShowable(GISADataset.NivelRow nivel)
		{
			return isShowable(nivel, null);
		}

		public override bool isShowable(GISADataset.NivelRow nivel, GISADataset.NivelRow nivelUpper)
		{
			// nivelUpper não é utilizado neste método propositadamente
			return nivel.TipoNivelRow.ID == TipoNivel.LOGICO || nivel.TipoNivelRow.IsStructure;
		}

        public override long TipoNivelRelLimitExcl()
        {
            return TipoNivelRelacionado.SR;
        }
	}
}