using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

using GISA.Model;
using GISA.Reports.XLSX;
using iTextSharp.text;

namespace GISA.Reports
{
	/// <summary>
	/// Summary description for UnidadesFisicasResumido.
	/// </summary>
	public class UnidadesFisicasResumido : UnidadesFisicas
	{
		public UnidadesFisicasResumido(string FileName, long IDTrustee) : base(FileName, IDTrustee) {}

		public UnidadesFisicasResumido(string FileName, ArrayList parameters, long IDTrustee) : base(FileName, parameters, null, IDTrustee) {}

        private List<UnidadeFisica> ufs;

        protected override string GetTitle()
        {
            return "Resultados";
        }

        protected override void InitializeReport(IDbConnection connection)
        {
            DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.InitializeListaUnidadesFisicas(null, connection);
        }

        protected override void FinalizeReport(IDbConnection connection)
        {
            DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.FinalizeListaUnidadesFisicas(connection);
        }

        protected override void LoadContents(IDbConnection connection, ref IDataReader reader) 
		{ 
			try 
			{
				reader = DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.ReportResPesquisaResumidoUnidadesFisicas(connection);				
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				throw;
			}

            UnidadeFisica uf = new UnidadeFisica();
			long ufID;
            ufs = new List<UnidadeFisica>();
            Dictionary<long, string> codCompletos = new Dictionary<long,string>();

            ufID = 0;
			while (reader.Read())
			{
                uf = new UnidadeFisica();
				ufID = System.Convert.ToInt64(reader.GetValue(0));
				uf.ID = ufID.ToString();
                uf.Codigo = reader.GetValue(1).ToString();
                uf.Titulo = reader.GetValue(2).ToString();
				uf.InicioAno = reader.GetValue(3).ToString();
				uf.InicioMes = reader.GetValue(4).ToString();
				uf.InicioDia = reader.GetValue(5).ToString();
                uf.InicioAtribuida = reader.GetValue(6) == DBNull.Value ? false : System.Convert.ToBoolean(reader.GetValue(6));
				uf.FimAno = reader.GetValue(7).ToString();
				uf.FimMes = reader.GetValue(8).ToString();
				uf.FimDia = reader.GetValue(9).ToString();
                uf.FimAtribuida = reader.GetValue(10) == DBNull.Value ? false : System.Convert.ToBoolean(reader.GetValue(10));
                uf.Cota = reader.GetValue(11).ToString();
                uf.GuiaIncorporacao = reader.GetValue(12) == DBNull.Value ? "" : reader.GetValue(12).ToString();
                uf.Eliminada = reader.GetValue(13) == DBNull.Value ? false : System.Convert.ToBoolean(reader.GetValue(13));
                uf.CodBarras = reader.GetValue(14) == DBNull.Value ? "" : reader.GetValue(14).ToString();

				ufs.Add(uf);
                DoAddedEntries(1);
			}
		}
        
        protected override void FillContents()
        {
            var tbl = CreateTable();

            AddNewCell(tbl, "Código", this.BodyFont);
            AddNewCell(tbl, "Título", this.BodyFont);
            AddNewCell(tbl, "Datas de Produção", this.BodyFont);
            AddNewCell(tbl, "Cota", this.BodyFont);
            AddNewCell(tbl, "Guia de Incorporação", this.BodyFont);
            AddNewCell(tbl, "Código de Barras", this.BodyFont);

            foreach (UnidadeFisica uf in ufs)
            {
                AddUF(uf, tbl);
                DoRemovedEntries(1);
            }

            AddTable(base.mDoc, tbl);
        }

        private Table CreateTable()
        {
            var tbl = new Table(6, 1);

            tbl.Offset = 0;
            tbl.Width = 100;
            tbl.BorderColor = iTextSharp.text.Color.LIGHT_GRAY;
            tbl.DefaultCellBorderColor = iTextSharp.text.Color.LIGHT_GRAY;
            tbl.Padding = 3;
            tbl.CellsFitPage = true;

            // largura das colunas em percentagem
            tbl.Widths = new float[] { 15, 23, 20, 12, 18, 12 };

            return tbl;
        }

        private void AddUF(UnidadeFisica uf, iTextSharp.text.Table tbl)
        {
            var font = uf.Eliminada ? this.ContentStrikeThroughFont : this.ContentFont;

            AddNewCell(tbl, uf.Codigo, font);
            AddNewCell(tbl, uf.Titulo, font);
            AddNewCell(tbl, GISA.Utils.GUIHelper.FormatDateInterval(
                GISA.Utils.GUIHelper.FormatDate(uf.InicioAno, uf.InicioMes, uf.InicioDia, uf.InicioAtribuida),
                GISA.Utils.GUIHelper.FormatDate(uf.FimAno, uf.FimMes, uf.FimDia, uf.FimAtribuida)),
                font);
            AddNewCell(tbl, uf.Cota, font);
            AddNewCell(tbl, uf.GuiaIncorporacao, font);
            AddNewCell(tbl, uf.CodBarras, font);
        }

        protected override void FillContentsXLSX()
        {
            var doc = XLSXExportHelper.CreateDocument(this.GetFileName, this.GetTitle());
            var worksheetPart = doc.WorkbookPart.WorksheetParts.First();

            AddTableHeaders(worksheetPart);

            uint rowIdx = 2;
            foreach (var uf in ufs)
            {
                var values = BuildRow(uf);
                XLSXExportHelper.InsertValuesInWorksheet(worksheetPart, rowIdx++, values);

                DoRemovedEntries(1);
            }

            // Save the new worksheet.
            worksheetPart.Worksheet.Save();

            // Close the document.
            doc.Close();
        }

        private void AddTableHeaders(WorksheetPart worksheetPart)
        {
            var headers = new List<string>() { "Código", "Título", "Datas de Produção", "Cota", "Guia de Incorporação", "Código de Barras" };
            XLSXExportHelper.InsertValuesInWorksheet(worksheetPart, 1, headers);
        }

        private List<string> BuildRow(UnidadeFisica uf)
        {
            return new List<string>{ 
                uf.Codigo,
                uf.Titulo,
                GISA.Utils.GUIHelper.FormatDateInterval(
                    GISA.Utils.GUIHelper.FormatDate(uf.InicioAno, uf.InicioMes, uf.InicioDia, uf.InicioAtribuida),
                    GISA.Utils.GUIHelper.FormatDate(uf.FimAno, uf.FimMes, uf.FimDia, uf.FimAtribuida)),
                uf.Cota,
                uf.GuiaIncorporacao,
                uf.CodBarras};
        }

		private class UnidadeFisica 
		{
            // ArrayList de Unidade Física
			public string ID;
			public string Codigo;
			public string Titulo;
			public string InicioAno;
			public string InicioMes;
			public string InicioDia;
            public bool InicioAtribuida;
			public string FimAno;
			public string FimMes;
			public string FimDia;
            public bool FimAtribuida;
            public string Cota;
            public string GuiaIncorporacao;
            public bool Eliminada;
            public string CodBarras;
		}
	}
}
