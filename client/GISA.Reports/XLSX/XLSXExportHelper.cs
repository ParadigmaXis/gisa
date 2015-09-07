using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.Reports.XLSX
{
    public static class XLSXExportHelper
    {
        public static SpreadsheetDocument CreateDocument(string fileName, string sheetName)
        {
            // Create a spreadsheet document by supplying the file name.
            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook);
            CreateParts(spreadsheetDocument);

            return spreadsheetDocument;
        }

        // Adds child parts and generates content of the specified part
        private static void CreateParts(SpreadsheetDocument document)
        {
            WorkbookPart workbookPart1 = document.AddWorkbookPart();
            GenerateWorkbookPart1Content(workbookPart1);

            WorksheetPart worksheetPart1 = workbookPart1.AddNewPart<WorksheetPart>("rId1");
            Worksheet worksheet1 = new Worksheet();
            SheetData sheetData1 = new SheetData();
            worksheet1.Append(sheetData1);
            worksheetPart1.Worksheet = worksheet1;
            //GenerateWorksheetPart1Content(worksheetPart1);
        }

        // Generates content of workbookPart1. 
        private static void GenerateWorkbookPart1Content(WorkbookPart workbookPart1)
        {
            Workbook workbook1 = new Workbook();
            workbook1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");

            Sheets sheets1 = new Sheets();
            Sheet sheet1 = new Sheet() { Name = "Sheet1", SheetId = (UInt32Value)1U, Id = "rId1" };
            sheets1.Append(sheet1);

            workbook1.Append(sheets1);
            workbookPart1.Workbook = workbook1;
        }

        private static Dictionary<int, string> cols = new Dictionary<int, string>() { { 0, "A" }, { 1, "B" }, { 2, "C" }, { 3, "D" }, { 4, "E" }, { 5, "F" } };
        public static void InsertValuesInWorksheet(WorksheetPart worksheetPart, uint rowIdx, List<string> values)
        {
            var worksheet = worksheetPart.Worksheet;
            var sheetData = worksheet.GetFirstChild<SheetData>();

            Row row = new Row();
            values.ForEach(v =>
            {
                Cell cell = new Cell() { DataType = CellValues.InlineString };
                InlineString inlineString = new InlineString();
                inlineString.Append(new Text() { Text = v });
                cell.Append(inlineString);
                row.Append(cell);
            });
            sheetData.Append(row);
        }
    }
}
