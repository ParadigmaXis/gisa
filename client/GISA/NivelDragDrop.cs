using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
	public class NivelDragDrop : GenericDragDrop
	{

		public delegate void AcceptNivelRowEventHandler(GISADataset.NivelRow NivelRow);
		public event AcceptNivelRowEventHandler AcceptNivelRow;
		public delegate void AcceptNodeEventHandler(GISATreeNode Node);
		public event AcceptNodeEventHandler AcceptNode;
		public delegate void AcceptItemEventHandler(ListViewItem Item);
		public event AcceptItemEventHandler AcceptItem;

		private long[] IDTipoNivel;
		public NivelDragDrop(ListView ListView, params long[] IDTipoNivel) : base(ListView, typeof(GISADataset.NivelRow), typeof(GISADataset.NivelRow[]), typeof(GISATreeNode), typeof(ListViewItem), typeof(ListViewItem[]))
		{
			this.IDTipoNivel = IDTipoNivel;
		}
        public NivelDragDrop(DataGridView dataGridView, params long[] IDTipoNivel)
            : base(dataGridView, typeof(GISADataset.NivelRow), typeof(GISADataset.NivelRow[]), typeof(GISATreeNode), typeof(ListViewItem), typeof(ListViewItem[]))
        {
            this.IDTipoNivel = IDTipoNivel;
        }

		protected override void VerifyContents(object Value, ref bool Cancel)
		{
			if (Value is GISADataset.NivelRow)
				Cancel = (Array.IndexOf(IDTipoNivel, ((GISADataset.NivelRow)Value).IDTipoNivel) == IDTipoNivel.GetLowerBound(0) - 1);
			else if (Value is GISATreeNode)
				Cancel = (Array.IndexOf(IDTipoNivel, ((GISATreeNode)Value).NivelRow.IDTipoNivel) == IDTipoNivel.GetLowerBound(0) - 1);
			else if (Value is ListViewItem)
				Cancel = (Array.IndexOf(IDTipoNivel, ((GISADataset.NivelRow)(((ListViewItem)Value).Tag)).IDTipoNivel) == IDTipoNivel.GetLowerBound(0) - 1);
			else
				Cancel = true;
		}

		protected override void AcceptContents(object Value)
		{
			GISADataset.NivelRow nivelRow = null;
			GISATreeNode node = null;
			ListViewItem item = null;

			if (Value is GISADataset.NivelRow)
			{
				nivelRow = (GISADataset.NivelRow)Value;
				if (AcceptNivelRow != null)
					AcceptNivelRow(nivelRow);
			}
			else if (Value is GISATreeNode)
			{
				node = (GISATreeNode)Value;
				if (AcceptNode != null)
					AcceptNode(node);
			}
			else if (Value is ListViewItem)
			{
				item = (ListViewItem)Value;
				if (AcceptItem != null)
					AcceptItem(item);
			}
		}
	}
}