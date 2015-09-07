using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
	public class RelacaoNivel : GISA.Relacao
	{

	#region  Windows Form Designer generated code 

		public RelacaoNivel() : base()
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
			this.lblTipoNivel.Location = new System.Drawing.Point(10, 17);
			this.lblTipoNivel.Name = "lblTipoNivel";
			this.lblTipoNivel.Size = new System.Drawing.Size(410, 16);
			//
			//cbTipoNivel
			//
			this.cbTipoNivel.Location = new System.Drawing.Point(10, 33);
			this.cbTipoNivel.Name = "cbTipoNivel";
			this.cbTipoNivel.Size = new System.Drawing.Size(414, 21);
			this.cbTipoNivel.TabIndex = 1;
			//
			//cbTipoControloAutRel
			//
			this.cbTipoControloAutRel.Name = "cbTipoControloAutRel";
			this.cbTipoControloAutRel.Size = new System.Drawing.Size(158, 21);
			this.cbTipoControloAutRel.Visible = false;
			//
			//lblTipocontroloAutRel
			//
			this.lblTipocontroloAutRel.Name = "lblTipocontroloAutRel";
			this.lblTipocontroloAutRel.Visible = false;
			//
			//RelacaoNivel
			//
			this.Name = "RelacaoNivel";

		}

	#endregion


		protected override void PopulateControls()
		{
			base.PopulateControls();

			// Ao contrário de em RelacaoControloAut o tipo das 
			// relações será sempre hierárquica, logo, esconde-se 
			// o controlo na GUI de forma a impedir o utilizador 
			// de alterar o seu valor e selecciona-se por omissão 
			// o tipo de relação hierárquica.
			foreach (GISADataset.TipoControloAutRelRow tcarRow in cbTipoControloAutRel.Items)
			{
				if (tcarRow.ID == Convert.ToInt64(TipoControloAutRel.Hierarquica))
				{
					cbTipoControloAutRel.SelectedItem = tcarRow;
					break;
				}
			}
		}

		private GISADataset.TipoNivelRelacionadoRow mTipoNivelRelacionadoRow = null;
		public GISADataset.TipoNivelRelacionadoRow TipoNivelRelacionadoRow
		{
			get
			{
				return mTipoNivelRelacionadoRow;
			}
			set
			{
				mTipoNivelRelacionadoRow = value;
			}
		}

		// Neste caso adiciona apenas um 
		protected override ArrayList GetPossibleSubItems(GISADataset.NivelRow nRow)
		{
			ArrayList subItems = new ArrayList();
			TipoNivelRelacionado.PossibleSubNivel subItem = new TipoNivelRelacionado.PossibleSubNivel();
			subItem.SubIDTipoNivelRelacionado = TipoNivelRelacionadoRow.ID;
			subItem.Designacao = TipoNivelRelacionadoRow.Designacao;
			subItems.Add(subItem);
			return subItems;
		}
	}

} //end of root namespace