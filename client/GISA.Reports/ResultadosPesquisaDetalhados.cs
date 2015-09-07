using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA.Reports
{
    /// <summary>
    /// Summary description for CatalogoCompleto.
    /// </summary>
    public class ResultadosPesquisaDetalhados : CatalogoDetalhado
    {
        public ResultadosPesquisaDetalhados(string FileName, bool isTopDown, long idTrustee) : base(FileName, isTopDown, idTrustee) { }

        public ResultadosPesquisaDetalhados(string FileName, ArrayList parameters, long idTrustee) : base(FileName, parameters, false, idTrustee) { }

        public ResultadosPesquisaDetalhados(string FileName, ArrayList parameters, bool isTopDown, long idTrustee) : base(FileName, parameters, isTopDown, idTrustee) { }

        public ResultadosPesquisaDetalhados(string FileName, ArrayList parameters, List<ReportParameter> fields, bool isTopDown, long idTrustee) : base(FileName, parameters, fields, isTopDown, idTrustee) { }

       	protected override bool IsCatalogo(){
			return true;
		}

        protected override List<ReportParameter> Fields()
        {
            return mFields;
        }

		protected override string GetTitle(){
            return "Resultados da Pesquisa Detalhados";
		}

        protected override void InitializeReport(IDbConnection connection)
        {
            if (mParameters != null && mParameters.Count == 0)
            {
                return;
            }
            DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.InitializeReportResPesquisaDet(connection);
        }

        protected override void FinalizeReport(IDbConnection connection)
        {
            DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.FinalizeReportResPesquisaDet(connection);
        }

        protected override void LoadData(IDbConnection connection, ref IDataReader reader)
        {
            // Código para testar todas as combinações possíveis do relatório detalhado
            //List<ReportParameter> allParams = new List<ReportParameter>();
            //allParams = DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.BuildParamListInventCat();

            //List<ReportParameter> params1 = new List<ReportParameter>();
            //List<ReportParameter> params2 = new List<ReportParameter>();
            //List<ReportParameter> queryParams = new List<ReportParameter>();
            //ReportParameter rParam = null;
            //params2.AddRange(allParams.ToArray());

            //while (params1.Count != allParams.Count)
            //{
            //    queryParams.AddRange(params1.ToArray());
            //    foreach (ReportParameter param in params2)
            //    {
            //        try
            //        {
            //            queryParams.Add(param);
            //            IDbConnection conn = GISA.Model.GisaDataSetHelper.GetTempConnection();
            //            conn.Open();
            //            DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.ReportResPesquisaDetalhado(queryParams, IDTrustee(), conn);
            //            conn.Close();
            //            conn.Dispose();
            //            queryParams.Remove(param);
            //        }
            //        catch (Exception e)
            //        {
            //            System.Diagnostics.Debug.WriteLine(e);
            //        }
            //    }
            //    queryParams.Clear();
            //    rParam = params2[0];
            //    params2.Add(rParam);
            //    params2.RemoveAt(0);
            //}




            try
            {
                reader = DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.ReportResPesquisaDetalhado(this.mFields, IDTrustee(), connection);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                throw;
            }
        }

        // nos relatórios sobre os resultados de pesquisa os niveis de topo são sempre documentais
        protected override void LoadTopNivel(ref IDataReader reader)
        {
            
        }

        protected override void DefineTopNiveis(Nivel nvl, long IDUpperNivel)
        {
            if (!nvl.isContext && !topNiveis.Contains(nvl))
                topNiveis.Add(nvl);

            nvl.AddUpper(CreateNivelIfNonExistent(IDUpperNivel));
            nvl.IDNivelUpper = IDUpperNivel;
        }

        protected override void FillContents()
        {
            foreach (Nivel nvl in topNiveis)
            {
                float indentation = 0;
                Nivel n = nvl;
                List<Nivel> niveisContexto = new List<Nivel>();
                while (n.Uppers != null && n.Uppers.Count > 0 && ((Nivel)n.Uppers.ToArray()[0]).IDTipoNivel == 3)
                {
                    n = (Nivel) n.Uppers.ToArray()[0];
                    niveisContexto.Insert(0, n);
                }

                //foreach (Nivel nContexto in niveisContexto)
                //{
                //    AddNivelEstrutural(doc, nContexto, indentation, true);
                //    indentation = (float)(indentation + 0.5);
                //}

                AddNivelEstrutural(base.mDoc, nvl, indentation, niveisContexto);



                //if (nvl.Uppers != null && nvl.Uppers.Count > 0 && ((Nivel)nvl.Uppers(0)).IDTipoNivel == 3)
                //    AddNivelEstrutural(doc, nvl, 0.0F);
                //else
                //    AddNivelEstrutural(doc, nvl, 0.0F);
            }
        }

        protected override void ImprimeSubNiveis(Nivel nvl, iTextSharp.text.Document doc, float indentation)
        {
            
        }
    }
}
