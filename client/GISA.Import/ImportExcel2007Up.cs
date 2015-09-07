using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace GISA.Import
{
    public class ImportExcel2007Up : ImportExcel
    {
        public ImportExcel2007Up(string fName) : base(fName) { }

        protected override DataSet ReadFile()
        {
            DataSet ds = null;

            try
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Open(this.fileName, true))
                {
                    ds = new DataSet();
                    var workBook = document.WorkbookPart.Workbook;
                    var sharedStrings = document.WorkbookPart.SharedStringTablePart.SharedStringTable;

                    workBook.Descendants<Sheet>().ToList().ForEach(s =>
                    {
                        var ws = (WorksheetPart)document.WorkbookPart.GetPartById(s.Id);
                        DataTable dt = new DataTable();
                        dt.TableName = s.Name;
                        ds.Tables.Add(dt);

                        var row1 = ws.Worksheet.Descendants<DocumentFormat.OpenXml.Spreadsheet.Row>().Where(r => r.RowIndex == 1).Single();
                        var columns = row1.Descendants<DocumentFormat.OpenXml.Spreadsheet.Cell>().Where(c => c.CellValue != null).
                                        ToDictionary(c => getColumn(c.CellReference), c => new DataColumn(GetCellValue(sharedStrings, c), System.Type.GetType("System.String")) { DefaultValue = "" });

                        dt.Columns.AddRange(columns.Values.Cast<DataColumn>().ToArray());

                        ws.Worksheet.Descendants<DocumentFormat.OpenXml.Spreadsheet.Row>().Where(r => r.RowIndex > 1).ToList().ForEach(r =>
                        {
                            //Create a new DataRow with the info from the SQL Table.
                            var dr = dt.NewRow();
                            r.Descendants<DocumentFormat.OpenXml.Spreadsheet.Cell>()
                                .Where(c => c.CellValue != null).ToList()
                                .ForEach(c => dr[(DataColumn)columns[getColumn(c.CellReference)]] = GetCellValue(sharedStrings, c));
                            dt.Rows.Add(dr);
                        });
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }

            return ds;
        }

        private static string getColumn(string cellPosition)
        {
            for (int index = 0; index < cellPosition.Length; index++)
                if (char.IsNumber(cellPosition[index]))
                    return cellPosition.Substring(0, index);

            return "";
        }

        private static string GetCellValue(SharedStringTable sharedStrings, DocumentFormat.OpenXml.Spreadsheet.Cell cell)
        {
            return cell.DataType != null
                        && cell.DataType.HasValue
                        && cell.DataType == CellValues.SharedString
                      ? sharedStrings.ChildElements[
                        int.Parse(cell.CellValue.InnerText)].InnerText
                      : cell.CellValue.InnerText;
        }
    }
}
