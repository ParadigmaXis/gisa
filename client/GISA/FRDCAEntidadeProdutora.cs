using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.SharedResources;

namespace GISA
{
	public class FRDCAEntidadeProdutora : GISA.FRDControloAutoridade
	{

	#region  Windows Form Designer generated code 

		public FRDCAEntidadeProdutora() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
			DropDownTreeView1.GISAFunction = "Entidade Produtora";
			DropDownTreeView1.SelectedNode = DropDownTreeView1.Nodes[0];
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

		public static new Bitmap FunctionImage
		{
			get
			{
				return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "EntidadeProdutora_enabled_32x32.png");
			}
		}
	}

} //end of root namespace