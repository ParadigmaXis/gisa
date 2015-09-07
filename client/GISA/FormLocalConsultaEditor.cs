using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;
using GISA.Model;

namespace GISA
{
    public partial class FormLocalConsultaEditor : GISA.FormDomainValueEditor
    {
        public FormLocalConsultaEditor()
        {
            InitializeComponent();
        }

        // Antes de adicionar um novo elemento é verificado se foi eliminado um com uma designação igual. Se for esse o caso, a linha apagada é 
        // reaproveitada evitando possíveis futuros conflitos aquando da gravação de dados
        protected override DataRow GetNewDataRow(string Value)
        {
            GISADataset.LocalConsultaRow row = null;
            GISADataset.LocalConsultaRow[] deletedRows = (GISADataset.LocalConsultaRow[])(GisaDataSetHelper.GetInstance().LocalConsulta.Select(string.Format("Designacao='{0}'", Value), "", DataViewRowState.Deleted));
            if (deletedRows.Length > 0)
            {
                deletedRows[0].RejectChanges();
                row = deletedRows[0];
            }
            else
            {
                row = GisaDataSetHelper.GetInstance().LocalConsulta.NewLocalConsultaRow();
                row.Designacao = Value;
                GisaDataSetHelper.GetInstance().LocalConsulta.AddLocalConsultaRow(row);
            }
            return row;
        }

        protected override DataRow GetUpdatedDataRow(DataRow row, string NewValue)
        {
            GISADataset.LocalConsultaRow lcRow = (GISADataset.LocalConsultaRow)row;
            lcRow.Designacao = NewValue;
            return lcRow;
        }

        protected override DataRow[] GetRowDependencies(DataRow row)
        {
            var lcRow = (GISADataset.LocalConsultaRow)row;
            var dep = new List<string>();

            IDbConnection conn = GisaDataSetHelper.GetConnection();
            try
            {
                conn.Open();
                dep = RelatorioRule.Current.GetLocalConsultaAssociations(lcRow.ID, conn);
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
                report = "Não é possível remover o local de consulta \"" + lstValores.GetItemText(lstValores.SelectedItem) + "\" porque tem conteúdos associados. Terá que remover essas associações para que possa proceder à sua remoção. " + "Foram encontrados os seguintes items associados:" + System.Environment.NewLine + System.Environment.NewLine + report;
            }
            return report;
        }
    }
}
