using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;

namespace GISA.Model
{
	public class ControloAutHelper
	{
		// Devolve a forma autorizada do CA passado
		public static GISADataset.ControloAutDicionarioRow getFormaAutorizada(GISADataset.ControloAutRow caRow)
		{
			foreach (GISADataset.ControloAutDicionarioRow cadRow in caRow.GetControloAutDicionarioRows())
			{
				if (cadRow.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada)
					return cadRow;
			}
			return null;
		}

        public static List<CAAssociado> GetRelatedControloAut(List<GISADataset.ControloAutDicionarioRow> cadRows)
        {
            var res = new List<CAAssociado>();
            var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                var start = DateTime.Now.Ticks;
                res = DiplomaModeloRule.Current.GetCANiveisAssociados(cadRows.Select(r => r.IDControloAut).ToList(), ho.Connection);
                Trace.WriteLine("<<LoadNivelDesignadoOfSelfAndParent>>: " + new TimeSpan(DateTime.Now.Ticks - start).ToString());
            }
            catch (Exception ex) { Debug.WriteLine(ex); throw ex; }
            finally { ho.Dispose(); }

            return res;
        }

        public static string GetControloAutUsage(List<GISADataset.ControloAutDicionarioRow> cadRows, ref bool HasDocumentData, ref List<string> nivelIDsAssoc)
        {
            HasDocumentData = false;
            nivelIDsAssoc = new List<string>();

            if (cadRows.Count == 0) return "";

            System.Text.StringBuilder Result = new System.Text.StringBuilder();
            long tipoNoticia = cadRows.First().ControloAutRow.IDTipoNoticiaAut;

            List<CAAssociado> NivelList = GetRelatedControloAut(cadRows);

            bool HasNiveisDocAssoc = false;
            foreach (CAAssociado caAssociado in NivelList)
            {
                Result.Append(string.Format("Associado a {0}: {1}", caAssociado.TipoNivelDesignado, caAssociado.NivelDesignacao));
                Result.Append(Environment.NewLine);
                if (caAssociado.AllowDelete)
                    HasNiveisDocAssoc = true; // basta haver um nível documental associado para não perminir que o CA seja eliminado

                if (caAssociado.IDNivel > 0)
                    nivelIDsAssoc.Add(caAssociado.IDNivel.ToString());
            }

            HasDocumentData = Result.Length > 0 && tipoNoticia != (long)TipoNoticiaAut.EntidadeProdutora ? false : HasNiveisDocAssoc;
            
            return Result.ToString();
        }
	}
}