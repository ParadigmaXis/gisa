using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA
{
	public class FormAutoEliminacaoPicker : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public FormAutoEliminacaoPicker() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            lvwAutosEliminacao.SelectedIndexChanged += lvwAutosEliminacao_SelectedIndexChanged;
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
		internal System.Windows.Forms.Button btnOk;
		internal System.Windows.Forms.Button btnCancel;
		internal System.Windows.Forms.ColumnHeader ColumnHeaderDesignacao;
		internal System.Windows.Forms.ColumnHeader ColumnHeaderDocumentosCount;
		internal System.Windows.Forms.ColumnHeader ColumnHeaderUnidadesFisicasCount;
		internal System.Windows.Forms.ListView lvwAutosEliminacao;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lvwAutosEliminacao = new System.Windows.Forms.ListView();
			this.ColumnHeaderDesignacao = new System.Windows.Forms.ColumnHeader();
			this.ColumnHeaderDocumentosCount = new System.Windows.Forms.ColumnHeader();
			this.ColumnHeaderUnidadesFisicasCount = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			//
			//btnOk
			//
			this.btnOk.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(204, 236);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "Ok";
			//
			//btnCancel
			//
			this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(288, 236);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			//
			//lvwAutosEliminacao
			//
			this.lvwAutosEliminacao.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.lvwAutosEliminacao.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {this.ColumnHeaderDesignacao, this.ColumnHeaderDocumentosCount, this.ColumnHeaderUnidadesFisicasCount});
			this.lvwAutosEliminacao.FullRowSelect = true;
			this.lvwAutosEliminacao.HideSelection = false;
			this.lvwAutosEliminacao.Location = new System.Drawing.Point(4, 4);
			this.lvwAutosEliminacao.Name = "lvwAutosEliminacao";
			this.lvwAutosEliminacao.Size = new System.Drawing.Size(376, 224);
			this.lvwAutosEliminacao.TabIndex = 2;
			this.lvwAutosEliminacao.View = System.Windows.Forms.View.Details;
			//
			//ColumnHeaderDesignacao
			//
			this.ColumnHeaderDesignacao.Text = "Título";
			this.ColumnHeaderDesignacao.Width = 167;
			//
			//ColumnHeaderDocumentosCount
			//
			this.ColumnHeaderDocumentosCount.Text = "Nº. unidades documentais";
			this.ColumnHeaderDocumentosCount.Width = 108;
			//
			//ColumnHeaderUnidadesFisicasCount
			//
			this.ColumnHeaderUnidadesFisicasCount.Text = "Nº. unidades físicas";
			this.ColumnHeaderUnidadesFisicasCount.Width = 97;
			//
			//FormAutoEliminacaoPicker
			//
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(384, 267);
			this.ControlBox = false;
			this.Controls.Add(this.lvwAutosEliminacao);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Name = "FormAutoEliminacaoPicker";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Seleção de auto de eliminação";
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion


		public void LoadData()
		{
			LoadData(false);
		}

		public void LoadData(bool excludeEmptyAutos)
		{
			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			ArrayList aeList = null;
			try
			{
				Trace.WriteLine("<getAutosEliminacao>");
                //TODO: apagar?
				//dataReader = GisaDataSetHelper.GetDBLayer().CallStoredProcedure("sp_getAutosEliminacao", param, types, values)
				aeList = RelatorioRule.Current.LoadDataFormAutoEliminacaoPicker(excludeEmptyAutos, ho.Connection);
				Trace.WriteLine("</getAutosEliminacao>");

				foreach (object[] ae in aeList)
				{
					addAutoToListView(ae);
				}

				// obter também os autos de eliminação que possam já ter sido criados mas ainda não existam na BD
				foreach (GISADataset.AutoEliminacaoRow aeRow in GisaDataSetHelper.GetInstance().AutoEliminacao.Select("ID < 0"))
				{
					addAutoToListView(new object[] {aeRow.ID, aeRow.Designacao, 0, 0}); // FIXME: estes zeros nao deviam estar hardcoded
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				throw;
			}
			finally
			{
				ho.Dispose();
			}
			UpdateButtonState();
		}

		private void addAutoToListView(object[] ae)
		{
			ListViewItem item = null;
			item = lvwAutosEliminacao.Items.Add(ae[1].ToString());
			item.SubItems.Add(ae[2].ToString());
			item.SubItems.Add(ae[3].ToString());
			item.Tag = System.Convert.ToInt64(ae[0]);
		}

		public GISADataset.AutoEliminacaoRow SelectedAutoEliminacao
		{
			get
			{
				GISADataset.AutoEliminacaoRow aeRow = null;
				if (lvwAutosEliminacao.SelectedItems.Count > 0)
				{
					GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
					try
					{
						RelatorioRule.Current.LoadAutoEliminacao(GisaDataSetHelper.GetInstance(), (long)(lvwAutosEliminacao.SelectedItems[0].Tag), ho.Connection);
					}
					catch (Exception ex)
					{
						Trace.WriteLine(ex);
						throw;
					}
					finally
					{
						ho.Dispose();
					}

					GISADataset.AutoEliminacaoRow[] aeRows = null;

                    //TODO: apagar?
					//GisaDataSetHelper.GetAutoEliminacaoDataAdapter(String.Format("WHERE ID={0}", lvwAutosEliminacao.SelectedItems(0).Tag)).Fill(GisaDataSetHelper.GetInstance().AutoEliminacao)
					//PersistencyHelper.cleanDeletedRows()
					aeRows = (GISADataset.AutoEliminacaoRow[])(GisaDataSetHelper.GetInstance().AutoEliminacao.Select(string.Format("ID={0}", lvwAutosEliminacao.SelectedItems[0].Tag)));
					if (aeRows.Length > 0)
					{
						aeRow = aeRows[0];
					}
				}
				return aeRow;
			}
		}

		// Propriedade utilizada para os casos em que possam ser vários os autos de eliminação selecionados
		public GISADataset.AutoEliminacaoRow[] SelectedAutosEliminacao
		{
			get
			{
				GISADataset.AutoEliminacaoRow[] aeRows = null;
				if (lvwAutosEliminacao.SelectedItems.Count > 0)
				{
					System.Text.StringBuilder queryFilter = null;
					ArrayList aeIDs = new ArrayList();
					foreach (ListViewItem item in lvwAutosEliminacao.SelectedItems)
					{
						aeIDs.Add(item.Tag);
						if (queryFilter == null)
						{
							queryFilter = new System.Text.StringBuilder();
						}
						else
						{
							queryFilter.Append(" OR ");
						}
						queryFilter.AppendFormat("ID={0}", item.Tag);
					}

					GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
					try
					{
						RelatorioRule.Current.LoadAutosEliminacao(GisaDataSetHelper.GetInstance(), aeIDs, ho.Connection);
					}
					catch (Exception ex)
					{
						Trace.WriteLine(ex);
						throw;
					}
					finally
					{
						ho.Dispose();
					}
					aeRows = (GISADataset.AutoEliminacaoRow[])(GisaDataSetHelper.GetInstance().AutoEliminacao.Select(queryFilter.ToString()));
				}
				return aeRows;
			}
			set
			{
				lvwAutosEliminacao.BeginUpdate();
				foreach (ListViewItem item in lvwAutosEliminacao.Items)
				{
					if (Array.IndexOf(value, item.Tag) >= 0)
					{
						item.Selected = true;
					}
				}
				lvwAutosEliminacao.EndUpdate();
			}
		}

		private void lvwAutosEliminacao_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UpdateButtonState();
		}

		private void UpdateButtonState()
		{
			btnOk.Enabled = lvwAutosEliminacao.SelectedItems.Count > 0;
		}
	}

} //end of root namespace