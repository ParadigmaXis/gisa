using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

using GISA.Model;
using GISA.Reports.XLSX;
using iTextSharp.text;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA.Reports
{
	/// <summary>
	/// Summary description for ResultadosPesquisa.
	/// </summary>
	public class ResultadosPesquisa : Relatorio
	{
        private bool _inclLicObras = false;
        private bool _inclReq = false;
        List<string> headers = new List<string>() { "Identificador", "Código Referência", "Nível de Descrição", "Título", "Datas de Produção" }; 
        public ResultadosPesquisa(string FileName, ArrayList parameters, long IDTrustee, bool licObr, bool req)
            : base(FileName, parameters, IDTrustee)
        {
            _inclLicObras = licObr;
            _inclReq = req;

            if (_inclReq)
                headers.Add("Requisitado");

            headers.Add("Agrupador");

            if (_inclLicObras)
            {
                headers.Add("Requerentes iniciais");
                headers.Add("Localização da obra (atual)");
                headers.Add("Num. polícia (atual)");
                headers.Add("Localização da obra (antiga)");
                headers.Add("Num. polícia (antigo)");
                headers.Add("Tipo de obra");
            }
        }

		private List<PesquisaRule.NivelDocumental> niveis;


		protected override string GetTitle()
		{
			return "Resultados da Pesquisa Resumidos";
		}

		protected override void InitializeReport(IDbConnection connection)
		{
            DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.InitializeReportResPesquisa(connection);
		}

		protected override void FinalizeReport(IDbConnection connection)
		{
            DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.FinalizeReportResPesquisa(connection);
		}	

		protected override void LoadContents(IDbConnection connection, ref IDataReader reader) 
		{
            ArrayList results = new ArrayList();
			try 
			{
                results = DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.GetPesquisaResultsDetails("#temp", true, connection);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				throw;
			}

            niveis = new List<PesquisaRule.NivelDocumental>();
            var tbl = GisaDataSetHelper.GetInstance().TipoNivelRelacionado.Cast<GISADataset.TipoNivelRelacionadoRow>();
            foreach (PesquisaRule.NivelDocumental res in results)
            {
                res.TipoNivelRelacionado = tbl.Single(r => r.ID == res.IDTipoNivelRelacionado).Designacao;
                niveis.Add(res);
                DoAddedEntries(1);
            }
		}
        
        protected override void FillContents()
        {
            Table detailsTable = new Table(5, 1);

            //este offset deve passar novamente para 0 caso se volte a usar Tables em vez de PdfTables
            detailsTable.Offset = 3;
            detailsTable.Width = 100;
            detailsTable.BorderColor = iTextSharp.text.Color.LIGHT_GRAY;
            detailsTable.DefaultCellBorderColor = iTextSharp.text.Color.LIGHT_GRAY;
            detailsTable.Padding = 3;
            detailsTable.CellsFitPage = true;

            float indentPercent = (0.0f + 5.0f) * 100f / 21.6f;
            // uma página A4 tem 21.6 cm
            detailsTable.Widths = new float[] { 10, indentPercent, 19, 100 - 19 - 19 - 10 - indentPercent, 19 };

            headers.ForEach(h => AddNewCell(detailsTable, h, this.BodyFont));

            foreach (var nvl in niveis)
            {
                AddNivel(detailsTable, nvl);
                DoRemovedEntries(1);
            }

            AddTable(base.mDoc, detailsTable);            
        }        

		private void AddNivel(Table detailsTable, PesquisaRule.NivelDocumental nvl)
		{
            AddNewCell(detailsTable, nvl.IDNivel.ToString(), this.ContentFont);
            AddNewCell(detailsTable, nvl.CodigoCompleto, this.ContentFont);
            AddNewCell(detailsTable, nvl.TipoNivelRelacionado, this.ContentFont);
            AddNewCell(detailsTable, nvl.Designacao, this.ContentFont);
            AddNewCell(detailsTable, GISA.Utils.GUIHelper.FormatDateInterval(
                GISA.Utils.GUIHelper.FormatDate(nvl.InicioAno, nvl.InicioMes, nvl.InicioDia, nvl.InicioAtribuida), 
                GISA.Utils.GUIHelper.FormatDate(nvl.FimAno, nvl.FimMes, nvl.FimDia, nvl.FimAtribuida)), 
                this.ContentFont);
            if (_inclReq)
                AddNewCell(detailsTable, nvl.Requisitado ? "Sim" : "Não", this.ContentFont);
            AddNewCell(detailsTable, nvl.Agrupador, this.ContentFont);
            if (_inclLicObras)
            {
                AddNewCell(detailsTable, nvl.RequerentesIniciais, this.ContentFont);
                AddNewCell(detailsTable, nvl.LocObraDesignacaoAct, this.ContentFont);
                AddNewCell(detailsTable, nvl.LocObraNumPoliciaAct, this.ContentFont);
                AddNewCell(detailsTable, nvl.LocObraDesignacaoAnt, this.ContentFont);
                AddNewCell(detailsTable, nvl.LocObraNumPoliciaAnt, this.ContentFont);
                AddNewCell(detailsTable, nvl.TipoObra, this.ContentFont);
            }
		}

        protected override void FillContentsXLSX()
        {
            var doc = XLSXExportHelper.CreateDocument(this.GetFileName, this.GetTitle());
            var worksheetPart = doc.WorkbookPart.WorksheetParts.First();

            AddTableHeaders(worksheetPart);

            uint rowIdx = 2;
            foreach (var nvl in niveis)
            {
                var values = BuildRow(nvl);
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
            
            XLSXExportHelper.InsertValuesInWorksheet(worksheetPart, 1, headers);
        }

        private List<string> BuildRow(PesquisaRule.NivelDocumental nvl)
        {
            var res = new List<string>();
            res.Add(nvl.IDNivel.ToString());
            res.Add(nvl.CodigoCompleto);
            res.Add(nvl.TipoNivelRelacionado);
            res.Add(nvl.Designacao);
            res.Add(GISA.Utils.GUIHelper.FormatDateInterval(
                    GISA.Utils.GUIHelper.FormatDate(nvl.InicioAno, nvl.InicioMes, nvl.InicioDia, nvl.InicioAtribuida),
                    GISA.Utils.GUIHelper.FormatDate(nvl.FimAno, nvl.FimMes, nvl.FimDia, nvl.FimAtribuida)));
            if (_inclReq)
                res.Add(nvl.Requisitado ? "Sim" : "Não");
            res.Add(nvl.Agrupador);
            if (_inclLicObras)
            {
                res.Add(nvl.RequerentesIniciais);
                res.Add(nvl.LocObraDesignacaoAct);
                res.Add(nvl.LocObraNumPoliciaAct);
                res.Add(nvl.LocObraDesignacaoAnt);
                res.Add(nvl.LocObraNumPoliciaAnt);
                res.Add(nvl.TipoObra);
            }
            return res;
        }
	}	
}
