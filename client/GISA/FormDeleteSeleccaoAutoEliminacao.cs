using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
	public class FormDeleteSeleccaoAutoEliminacao : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public FormDeleteSeleccaoAutoEliminacao() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
			//UpdateButtons()
			lvwAutosEliminacao.ListViewItemSorter = new SecondColumnSorter();
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
		internal System.Windows.Forms.ColumnHeader chCheck;
		internal System.Windows.Forms.ColumnHeader chDesignacao;
		internal System.Windows.Forms.Button btnOk;
		internal System.Windows.Forms.Button btnCancel;
		internal System.Windows.Forms.ListView lvwAutosEliminacao;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lvwAutosEliminacao = new System.Windows.Forms.ListView();
			this.chCheck = new System.Windows.Forms.ColumnHeader();
			this.chDesignacao = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			//
			//btnOk
			//
			this.btnOk.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(148, 236);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "Ok";
			//
			//btnCancel
			//
			this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(232, 236);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			//
			//lvwAutosEliminacao
			//
			this.lvwAutosEliminacao.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.lvwAutosEliminacao.CheckBoxes = true;
			this.lvwAutosEliminacao.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {this.chCheck, this.chDesignacao});
			this.lvwAutosEliminacao.FullRowSelect = true;
			this.lvwAutosEliminacao.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvwAutosEliminacao.Location = new System.Drawing.Point(4, 4);
			this.lvwAutosEliminacao.Name = "lvwAutosEliminacao";
			this.lvwAutosEliminacao.Size = new System.Drawing.Size(312, 224);
			this.lvwAutosEliminacao.TabIndex = 4;
			this.lvwAutosEliminacao.View = System.Windows.Forms.View.Details;
			//
			//chCheck
			//
			this.chCheck.Text = "";
			this.chCheck.Width = 19;
			//
			//chDesignacao
			//
			this.chDesignacao.Text = "Designacao";
			this.chDesignacao.Width = 280;
			//
			//FormDeleteSeleccaoAutoEliminacao
			//
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(324, 267);
			this.Controls.Add(this.lvwAutosEliminacao);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDeleteSeleccaoAutoEliminacao";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Remoção de autos de eliminação";
			this.ResumeLayout(false);

		}

	#endregion

		private class SecondColumnSorter : IComparer
		{

			public int Compare(object x, object y)
			{
				long xID = ((GISADataset.AutoEliminacaoRow)(((ListViewItem)x).Tag)).ID;
				long yID = ((GISADataset.AutoEliminacaoRow)(((ListViewItem)y).Tag)).ID;
				return System.Convert.ToInt32(xID - yID);
			}
		}

		public void LoadData(GISADataset.AutoEliminacaoRow[] autoEliminacaoRows)
		{
			lvwAutosEliminacao.Items.Clear();
			lvwAutosEliminacao.BeginUpdate();
			foreach (GISADataset.AutoEliminacaoRow aeRow in autoEliminacaoRows)
			{
				ListViewItem item = new ListViewItem(new string[] {string.Empty, string.Empty});
				item.SubItems[chDesignacao.Index].Text = aeRow.Designacao;
				item.Tag = aeRow;
				lvwAutosEliminacao.Items.Add(item);
			}
			lvwAutosEliminacao.Sort();
			lvwAutosEliminacao.EndUpdate();
		}

		public GISADataset.AutoEliminacaoRow[] GetChosenAutosEliminacao()
		{
			ArrayList removableAutosEliminacaoRows = new ArrayList();
			foreach (ListViewItem item in lvwAutosEliminacao.CheckedItems)
			{
				removableAutosEliminacaoRows.Add(item.Tag);
			}
			return (GISADataset.AutoEliminacaoRow[])(removableAutosEliminacaoRows.ToArray(typeof(GISADataset.AutoEliminacaoRow)));
		}
	}
} //end of root namespace