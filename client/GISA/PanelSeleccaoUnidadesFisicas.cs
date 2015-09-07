using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
	public class PanelSeleccaoUnidadesFisicas : GISA.GISAPanel
	{

	#region  Windows Form Designer generated code 

		public PanelSeleccaoUnidadesFisicas() : base()
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


		private GISADataset.FRDBaseRow CurrentFRDBase;

		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			IsLoaded = false;
			CurrentFRDBase = (GISADataset.FRDBaseRow)CurrentDataRow;
			
			IsLoaded = true;
		}

		public override void ViewToModel()
		{
			IsPopulated = true;
		}

		public override void Deactivate()
		{
		}

		public override void Save(IDbTransaction Trans)
		{
		}

	}

} //end of root namespace