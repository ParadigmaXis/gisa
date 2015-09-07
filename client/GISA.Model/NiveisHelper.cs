using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;

namespace GISA.Model
{
    public class NiveisHelper
    {
        public static GISADataset.NivelRow GetNivelED(GISATreeNode produtor)
        {
            while (produtor.Parent != null)
                produtor = (GISATreeNode)produtor.Parent;

            return produtor.NivelRow;
        }

        public static string getNextSeriesCodigo(bool incrementIt)
        {
            GISADataset.TipoNivelRelacionadoCodigoRow tnrcRow = null;
            tnrcRow = (GISADataset.TipoNivelRelacionadoCodigoRow)(GisaDataSetHelper.GetInstance().TipoNivelRelacionadoCodigo.Select()[0]);
            if (incrementIt)
            {
                tnrcRow.Contador += 1;
                return tnrcRow.Contador.ToString("0");
            }
            else
                return (tnrcRow.Contador + 1).ToString("0");
        }

        // Permitir apenas a eliminação das folhas e dos níveis cuja 
        // funcionalidade de eliminação não elimina o nível propriamente 
        // dito mas sim a sua relação com o nível superior
        public static bool isRemovable(GISADataset.NivelRow NivelRow, GISADataset.NivelRow NivelUpperRow)
        {
            return isRemovable(NivelRow, NivelUpperRow, true);
        }

        public static bool isRemovable(GISADataset.NivelRow NivelRow, GISADataset.NivelRow NivelUpperRow, bool countUFs)
        {
            string filter = string.Empty;
            if (!countUFs)
                filter = string.Format("rh.IDTipoNivelRelacionado != {0:d}", TipoNivelRelacionado.UF);

            int parentCount = 0;
            int directChildCount = 0;
            bool moreThenOneParent = false;
            bool notExistsDirectChild = false;
            bool connectionClose = false;
            if (GisaDataSetHelper.GetConnection().State == ConnectionState.Closed)
            {
                connectionClose = true;
                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetTempConnection());
                try
                {
                    parentCount = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.getParentCount(NivelRow.ID.ToString(), ho.Connection);
                    directChildCount = DBAbstractDataLayer.DataAccessRules.NivelRule.Current.getDirectChildCount(NivelRow.ID.ToString(), filter, ho.Connection);
                    moreThenOneParent = parentCount > 1;
                    notExistsDirectChild = directChildCount == 0;
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
            }

            return !(TipoNivel.isNivelOrganico(NivelRow) && TipoNivel.isNivelOrganico(NivelUpperRow)) &&
                   (TipoNivel.isNivelOrganico(NivelRow) ||
                        (NivelRow != null && NivelRow.IDTipoNivel == TipoNivel.LOGICO && notExistsDirectChild) ||
                        (TipoNivel.isNivelOrganico(NivelUpperRow) && connectionClose && !moreThenOneParent && notExistsDirectChild) ||
                        ((NivelUpperRow == null || NivelUpperRow.IDTipoNivel == TipoNivel.DOCUMENTAL) && connectionClose && notExistsDirectChild) ||
                        (NivelUpperRow != null && NivelUpperRow.IDTipoNivel == TipoNivel.ESTRUTURAL && NivelRow.IDTipoNivel == TipoNivel.DOCUMENTAL && connectionClose && notExistsDirectChild && !moreThenOneParent) //permitir apagar séries/documentos soltos só com um produtor e sem niveis descendentes
                   ); // o estado da ligação tem de se ser fechado para que não ocorram situações de deadlock na BD
        }

        public static bool NivelFoiMovimentado(long IDNivel)
        {
            bool foiMov = false;
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                foiMov = MovimentoRule.Current.foiMovimentado(IDNivel, ho.Connection);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                throw;
            }
            finally
            {
                ho.Dispose();
            }

            return foiMov;
        }

        public static PersistencyHelper.SetNewNivelOrderPreSaveArguments AddNivelDocumentoSimplesWithDelegateArgs(GISADataset.NivelDesignadoRow ndRow, long IDUpper, long IDTipoNivelRelacionado)
        {
            if (IDTipoNivelRelacionado != TipoNivelRelacionado.SD) return null;
            var psArgsNivelDocSimples = new PersistencyHelper.SetNewNivelOrderPreSaveArguments();
            GisaDataSetHelper.GetInstance().NivelDocumentoSimples.AddNivelDocumentoSimplesRow(ndRow, -1, new byte[] { }, 0);
            psArgsNivelDocSimples.nRowID = ndRow.ID;
            psArgsNivelDocSimples.nRowIDUpper = IDUpper;
            return psArgsNivelDocSimples;
        }
    }
}
