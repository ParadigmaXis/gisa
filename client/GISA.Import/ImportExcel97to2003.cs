using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ExcelLibrary.SpreadSheet;
using System.Text.RegularExpressions;

namespace GISA.Import
{
    public class ImportExcel97to2003 : ImportExcel
    {
        private static int HEADER_ROW_INDEX = 0;

        public ImportExcel97to2003(string fName) : base(fName) { }

        protected override System.Data.DataSet ReadFile()
        {
            DataSet ds = null;

            try
            {
                var book = ExcelLibrary.SpreadSheet.Workbook.Open(this.fileName); //Open the Excel Document

                try
                {
                    ds = new DataSet();
                    book.Worksheets.ForEach(ws =>
                    {
                        DataTable dt = new DataTable();
                        dt.TableName = ws.Name;
                        ds.Tables.Add(dt);

                        // get column names
                        var headerRow = ws.Cells.GetRow(HEADER_ROW_INDEX);
                        for (int i = 0; i < headerRow.LastColIndex + 1; i++)
                            dt.Columns.Add(new DataColumn(headerRow.GetCell(i).StringValue, System.Type.GetType("System.String")));

                        //Work through each of the Rows in the Excel Worksheet
                        for (int row = HEADER_ROW_INDEX + 1; row < ws.Cells.LastRowIndex + 1; row++)
                        {
                            //Create a new DataRow with the info from the SQL Table.
                            var dr = dt.NewRow();

                            //Work through each of the Columns for Each Row in the Excel Worksheet
                            for (int col = 0; col < dt.Columns.Count; col++)
                                dr[col] = ws.Cells[row, col].Value == null ? "" : (dt.Columns[col].DataType == typeof(DateTime) ? ws.Cells[row, col].DateTimeValue.ToString() : Regex.Replace(ws.Cells[row, col].Value.ToString(), "(?<!\r)\n", "\r\n", RegexOptions.Compiled));

                            dt.Rows.Add(dr);
                        }
                    });
                }
                catch (Exception e)
                {
                    Trace.WriteLine("Erro ao obter informação do ficheiro excel: " + e);
                    MessageBox.Show("Ocorreu um erro a ler os dados do ficheiro excel.", "Importação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("Erro ao abrir o ficheiro excel: " + e);
                MessageBox.Show("Ocorreu um erro a abrir o ficheiro excel.", "Importação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

            return ds;
        }
    }
}
