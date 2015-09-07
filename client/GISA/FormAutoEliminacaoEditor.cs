using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;
using GISA.Model;

namespace GISA
{
	public class FormAutoEliminacaoEditor : GISA.FormDomainValueEditor
	{

	#region  Windows Form Designer generated code 

		public FormAutoEliminacaoEditor() : base()
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

		// Antes de adicionar um novo elemento é verificado se foi eliminado um com uma designação igual. Se for esse o caso, a linha apagada é 
		// reaproveitada evitando possíveis futuros conflitos aquando da gravação de dados
		protected override DataRow GetNewDataRow(string Value)
		{
			GISADataset.AutoEliminacaoRow row = null;
			GISADataset.AutoEliminacaoRow[] deletedRows = (GISADataset.AutoEliminacaoRow[])(GisaDataSetHelper.GetInstance().AutoEliminacao.Select(string.Format("Designacao='{0}'", Value), "", DataViewRowState.Deleted));
			if (deletedRows.Length > 0)
			{
				deletedRows[0].RejectChanges();
				row = deletedRows[0];
			}
			else
			{
				row = GisaDataSetHelper.GetInstance().AutoEliminacao.NewAutoEliminacaoRow();
				row.Designacao = Value;
				GisaDataSetHelper.GetInstance().AutoEliminacao.AddAutoEliminacaoRow(row);
			}
			return row;
		}

		protected override DataRow GetUpdatedDataRow(DataRow row, string NewValue)
		{
			GISADataset.AutoEliminacaoRow aeRow = (GISADataset.AutoEliminacaoRow)row;
			aeRow.Designacao = NewValue;
			return aeRow;
		}

		protected override DataRow[] GetRowDependencies(DataRow row)
		{
			var aeRow = (GISADataset.AutoEliminacaoRow)row;
            var dep = new List<string>();

            IDbConnection conn = GisaDataSetHelper.GetConnection();
            try
            {
                conn.Open();
                dep = RelatorioRule.Current.GetAutoEliminacaoAssociations (aeRow.ID, conn);
            }
            finally
            {
                conn.Close();
            }

            DataRow[] relatedRows = new DataRow[dep.Count];
            DataTable t = new DataTable();
            DataColumn c = new DataColumn("Value", typeof(string));
            t.Columns.Add(c);
            int cnt = 0;
            foreach (string v in dep)
            {
                DataRow dr = t.NewRow();
                dr[0] = v;
                t.Rows.Add(dr);
                relatedRows[cnt++] = dr;
            }

			//GISADataset.SFRDAvaliacaoRow[] relatedRows = (GISADataset.SFRDAvaliacaoRow[])(GisaDataSetHelper.GetInstance().SFRDAvaliacao. Select("IDAutoEliminacao = " + aeRow.ID.ToString()));

			return relatedRows;
		}

		protected override string GetDependenciesReport(DataRow[] rows)
		{
			string report = null;
			foreach (var dependentRow in rows)
			{
				if (report == null)
				{
					report = "";
				}
                report += "  " + dependentRow[0] + System.Environment.NewLine;
			}
			if (report != null)
			{
                report = "Não é possível remover o auto de eliminação \"" + lstValores.GetItemText(lstValores.SelectedItem) + "\" porque tem conteúdos associados. Terá que remover essas associações para que possa proceder à sua remoção. " + "Foram encontrados os seguintes items associados:" + System.Environment.NewLine + System.Environment.NewLine + report;
			}
			return report;
		}
	}

} //end of root namespace