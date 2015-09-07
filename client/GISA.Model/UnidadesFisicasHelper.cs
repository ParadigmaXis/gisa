using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GISA.Model
{
    public class UnidadesFisicasHelper
    {
        public struct UaInfo
        {
            public long frdID;
            public long IDTipoNivelRelacionado;
        }

        public static string GenerateNewCodigoString(GISADataset.NivelRow nivelEDRow, int ano)
        {
            DataRow[] DataRows = GisaDataSetHelper.GetInstance().NivelUnidadeFisicaCodigo.Select("ID=" + nivelEDRow.ID.ToString() + " AND Ano=" + System.DateTime.Now.Year.ToString());
            if (DataRows.Length == 0)
                return "UF" + DateTime.Now.Year.ToString() + "-" + 1.ToString();
            else
                return "UF" + DateTime.Now.Year.ToString() + "-" + (((GISADataset.NivelUnidadeFisicaCodigoRow)(DataRows[0])).Contador + 1).ToString();
        }

        // returns the updated NivelUnidadeFisicaCodigoRow considering a new Codigo
        public static GISADataset.NivelUnidadeFisicaCodigoRow GetNewCodigoRow(GISADataset.NivelRow nivelRow, int ano)
        {
            GISADataset.NivelRow ParentEDRow = nivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].NivelRowByNivelRelacaoHierarquicaUpper;
            DataRow[] DataRows = GisaDataSetHelper.GetInstance().NivelUnidadeFisicaCodigo.Select("ID=" + ParentEDRow.ID.ToString() + " AND Ano=" + System.DateTime.Now.Year.ToString());
            GISADataset.NivelUnidadeFisicaCodigoRow codigoRow = null;
            if (DataRows.Length == 0)
            {
                codigoRow = GisaDataSetHelper.GetInstance().NivelUnidadeFisicaCodigo.AddNivelUnidadeFisicaCodigoRow(ParentEDRow, System.DateTime.Now.Year, 1M, new byte[] { }, 0);
            }
            else
            {
                codigoRow = (GISADataset.NivelUnidadeFisicaCodigoRow)(DataRows[0]);
                codigoRow.Contador = codigoRow.Contador + 1;
            }

            return codigoRow;
        }

        public static GISADataset.NivelRow CreateUF(GISADataset.NivelRow nivelED, string designacaoUF)
        {
            return CreateUF(nivelED, designacaoUF, "");
        }

        public static GISADataset.NivelRow CreateUF(GISADataset.NivelRow nivelED, string designacaoUF, string guiaUF)
        {
            GISADataset.TipoNivelRelacionadoRow tnrRow = (GISADataset.TipoNivelRelacionadoRow)(GisaDataSetHelper.GetInstance().TipoNivelRelacionado.Select(string.Format("ID={0}", TipoNivelRelacionado.UF))[0]);

            GISADataset.NivelRow nufRow = null;
            GISADataset.NivelDesignadoRow ndufRow = null;
            GISADataset.RelacaoHierarquicaRow rhufRow = null;
            GISADataset.NivelUnidadeFisicaRow nufufRow = null;
            GISADataset.FRDBaseRow frdufRow = null;

            // nivel
            nufRow = GisaDataSetHelper.GetInstance().Nivel.NewNivelRow();
            // nivelDesignado
            ndufRow = GisaDataSetHelper.GetInstance().NivelDesignado.NewNivelDesignadoRow();
            // RelacaoHierarquica
            rhufRow = GisaDataSetHelper.GetInstance().RelacaoHierarquica.NewRelacaoHierarquicaRow();
            // NivelUnidadeFisicaRow
            nufufRow = GisaDataSetHelper.GetInstance().NivelUnidadeFisica.NewNivelUnidadeFisicaRow();
            // FRDBaseRow
            frdufRow = GisaDataSetHelper.GetInstance().FRDBase.NewFRDBaseRow();

            GISADataset tempWith1 = GisaDataSetHelper.GetInstance();
            Trace.WriteLine("A criar unidade física...");
            nufRow.TipoNivelRow = tnrRow.TipoNivelRow;
            nufRow.Codigo = UnidadesFisicasHelper.GenerateNewCodigoString(nivelED, System.DateTime.Now.Year);
            nufRow.CatCode = "NVL";

            ndufRow.NivelRow = nufRow;
            ndufRow.Designacao = designacaoUF;
            //CreateUF_edID = nivelED.ID;
            //CreateUF_designacao = designacaoUF;
            //CreateUF_guia = guiaUF;

            rhufRow.NivelRowByNivelRelacaoHierarquica = nufRow;
            rhufRow.TipoNivelRelacionadoRow = (GISADataset.TipoNivelRelacionadoRow)(GisaDataSetHelper.GetInstance().TipoNivelRelacionado.Select(string.Format("ID={0}", TipoNivelRelacionado.UF))[0]);
            rhufRow["InicioAno"] = DBNull.Value;
            rhufRow["InicioMes"] = DBNull.Value;
            rhufRow["InicioDia"] = DBNull.Value;
            rhufRow["FimAno"] = DBNull.Value;
            rhufRow["FimMes"] = DBNull.Value;
            rhufRow["FimDia"] = DBNull.Value;
            rhufRow.NivelRowByNivelRelacaoHierarquicaUpper = nivelED;

            nufufRow.GuiaIncorporacao = guiaUF;
            nufufRow.NivelDesignadoRow = ndufRow;

            frdufRow.NivelRow = nufRow;
            frdufRow.NotaDoArquivista = string.Empty;
            frdufRow.TipoFRDBaseRow = (GISADataset.TipoFRDBaseRow)(GisaDataSetHelper.GetInstance().TipoFRDBase.Select(string.Format("ID={0}", System.Enum.Format(typeof(TipoFRDBase), TipoFRDBase.FRDUnidadeFisica, "D")))[0]);
            frdufRow.RegrasOuConvencoes = string.Empty;

            tempWith1.Nivel.AddNivelRow(nufRow);
            tempWith1.NivelDesignado.AddNivelDesignadoRow(ndufRow);
            tempWith1.RelacaoHierarquica.AddRelacaoHierarquicaRow(rhufRow);
            tempWith1.NivelUnidadeFisica.AddNivelUnidadeFisicaRow(nufufRow);
            tempWith1.FRDBase.AddFRDBaseRow(frdufRow);

            var sfrdDatasProducaoRow = GisaDataSetHelper.GetInstance().SFRDDatasProducao.Cast<GISADataset.SFRDDatasProducaoRow>().Where(r => r.IDFRDBase == frdufRow.ID).SingleOrDefault();
            if (sfrdDatasProducaoRow == null)
                GisaDataSetHelper.GetInstance().SFRDDatasProducao.AddSFRDDatasProducaoRow(frdufRow, "", "", "", "", false, "", "", "", "", false, new byte[] { }, 0);

            return nufRow;
        }

        public static string GetConteudoInformacional(GISADataset ds, GISADataset.NivelRow nivelUF)
        {
            var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                DBAbstractDataLayer.DataAccessRules.UFRule.Current.LoadUFConteudoEstruturaData(ds, nivelUF.GetFRDBaseRows()[0].ID, ho.Connection);
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

            var ce = nivelUF.GetFRDBaseRows()[0].GetSFRDConteudoEEstruturaRows().FirstOrDefault();
            return ce != null ? ce.ConteudoInformacional : "";
        }
    }
}
