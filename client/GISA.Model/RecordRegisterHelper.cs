using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace GISA.Model
{
    public class RecordRegisterHelper
    {
        public static void RegisterRecordModificationFRD(GISADataset.FRDBaseRow CurrentFRDBase, bool existsModifiedDataFromRels, GISADataset.TrusteeUserRow tuOperator, GISADataset.TrusteeUserRow tuAuthor, DateTime data)
        {
            // só é registada uma nova entrada no controlo de descrição se alguma informação relativa à FRD 
            // tiver sido modificada; é também possível que já tenha sido registado uma nova entrada no controlo mas
            // nesse caso não se adiciona outra
            if (CurrentFRDBase == null || CurrentFRDBase.RowState == DataRowState.Detached ||
                GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao.Select("IDFRDBase=" + CurrentFRDBase.ID.ToString(), "", DataViewRowState.Added).Length > 0)

                return;

            GISADataset.NivelUnidadeFisicaRow[] NivelUnidadeFisicaRows = new GISADataset.NivelUnidadeFisicaRow[] {};
            if (CurrentFRDBase.NivelRow.IDTipoNivel == TipoNivel.OUTRO) 
                NivelUnidadeFisicaRows = CurrentFRDBase.NivelRow.GetNivelDesignadoRows()[0].GetNivelUnidadeFisicaRows();

            if (existsModifiedDataFromRels ||
                CurrentFRDBase.RowState != DataRowState.Unchanged ||
                Concorrencia.WasRecordModified(CurrentFRDBase) ||
                Concorrencia.WasRecordModified(CurrentFRDBase.NivelRow) ||
                (NivelUnidadeFisicaRows.Length > 0 && Concorrencia.isModifiedRow(NivelUnidadeFisicaRows[0])))
            {
                var dddRow = CreateFRDBaseDataDeDescricaoRow(CurrentFRDBase, tuOperator, tuAuthor, data);
                GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao.AddFRDBaseDataDeDescricaoRow(dddRow);
            }
        }

        public static GISADataset.FRDBaseDataDeDescricaoRow CreateFRDBaseDataDeDescricaoRow(GISADataset.FRDBaseRow CurrentFRDBase, GISADataset.TrusteeUserRow tuOperator, GISADataset.TrusteeUserRow tuAuthor, DateTime data)
        {
            return CreateFRDBaseDataDeDescricaoRow(CurrentFRDBase, tuOperator, tuAuthor, data, -1, false);
        }

        public static GISADataset.FRDBaseDataDeDescricaoRow CreateFRDBaseDataDeDescricaoRow(GISADataset.FRDBaseRow CurrentFRDBase, GISADataset.TrusteeUserRow tuOperator, GISADataset.TrusteeUserRow tuAuthor, DateTime data, long IDTipoNivelRelacionado)
        {
            return CreateFRDBaseDataDeDescricaoRow(CurrentFRDBase, tuOperator, tuAuthor, data, IDTipoNivelRelacionado, false);
        }

        public static GISADataset.FRDBaseDataDeDescricaoRow CreateFRDBaseDataDeDescricaoRow(GISADataset.FRDBaseRow CurrentFRDBase, GISADataset.TrusteeUserRow tuOperator, GISADataset.TrusteeUserRow tuAuthor, DateTime data, bool isImportacao)
        {
            return CreateFRDBaseDataDeDescricaoRow(CurrentFRDBase, tuOperator, tuAuthor, data, -1, isImportacao);
        }

        public static GISADataset.FRDBaseDataDeDescricaoRow CreateFRDBaseDataDeDescricaoRow(GISADataset.FRDBaseRow CurrentFRDBase, GISADataset.TrusteeUserRow tuOperator, GISADataset.TrusteeUserRow tuAuthor, DateTime data, long IDTipoNivelRelacionado, bool isImportacao)
        {
            GISADataset.FRDBaseDataDeDescricaoRow dddRow = null;
            dddRow = GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao.NewFRDBaseDataDeDescricaoRow();
            if (CurrentFRDBase.ID <= 0)
                throw new Exception(string.Format("Identificador negativo ({0}) no registo de frds!!", CurrentFRDBase.ID));
            dddRow.IDFRDBase = CurrentFRDBase.ID;
            dddRow.TrusteeUserRowByTrusteeUserFRDBaseDataDeDescricao = tuOperator;
            dddRow.TrusteeUserRowByTrusteeUserFRDBaseDataDeDescricaoAuthority = tuAuthor;
            dddRow.DataEdicao = GISA.Utils.GUIHelper.getTruncatedCurrentDate();
            if (data == DateTime.MinValue)
                dddRow["DataAutoria"] = DBNull.Value;
            else
                dddRow.DataAutoria = data;
            dddRow.IDTipoNivelRelacionado = IDTipoNivelRelacionado > 0 ? IDTipoNivelRelacionado : CurrentFRDBase.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica()[0].IDTipoNivelRelacionado;
            dddRow.Importacao = isImportacao;
            dddRow.Versao = new byte[] { };
            dddRow.isDeleted = 0;
            
            return dddRow;
        }

        public static void RegisterRecordModificationCA(GISADataset.ControloAutRow CurrentControloAut, bool existsModifiedDataFromRels, GISADataset.TrusteeUserRow tuOperator, GISADataset.TrusteeUserRow tuAuthor, DateTime data)
        {
            // só é registada uma nova entrada no controlo de descrição se alguma informação relativa à FRD 
            // tiver sido modificada; é também possível que já tenha sido registado uma nova entrada no controlo mas
            // nesse caso não se adiciona outra
            if (CurrentControloAut == null || CurrentControloAut.RowState == DataRowState.Detached ||
                GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao.Select("IDControloAut=" + CurrentControloAut.ID.ToString(), "", DataViewRowState.Added).Length > 0)

                return;

            if (existsModifiedDataFromRels || Concorrencia.WasRecordModified(CurrentControloAut))
            {
                var cadddRow = CreateControlAutDataDeDescricaoRow(CurrentControloAut, tuOperator, tuAuthor, data);
                GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao.AddControloAutDataDeDescricaoRow(cadddRow);
            }
        }

        public static GISADataset.ControloAutDataDeDescricaoRow CreateControlAutDataDeDescricaoRow(GISADataset.ControloAutRow CurrentControloAut, GISADataset.TrusteeUserRow tuOperator, GISADataset.TrusteeUserRow tuAuthor, DateTime data)
        {
            return CreateControlAutDataDeDescricaoRow(CurrentControloAut, tuOperator, tuAuthor, data, false);
        }

        public static GISADataset.ControloAutDataDeDescricaoRow CreateControlAutDataDeDescricaoRow(GISADataset.ControloAutRow CurrentControloAut, GISADataset.TrusteeUserRow tuOperator, GISADataset.TrusteeUserRow tuAuthor, DateTime data, bool isImportacao)
        {
            GISADataset.ControloAutDataDeDescricaoRow cadddRow = null;
            cadddRow = GisaDataSetHelper.GetInstance().ControloAutDataDeDescricao.NewControloAutDataDeDescricaoRow();
            cadddRow.IDControloAut = CurrentControloAut.ID;
            if (CurrentControloAut.ID <= 0)
                throw new Exception(string.Format("Identificador negativo ({0}) no registo de cas!!", CurrentControloAut.ID));
            cadddRow.TrusteeUserRowByTrusteeUserControloAutDataDeDescricao = tuOperator;
            cadddRow.TrusteeUserRowByTrusteeUserControloAutDataDeDescricaoAuthority = tuAuthor;
            cadddRow.DataEdicao = GISA.Utils.GUIHelper.getTruncatedCurrentDate();
            if (data == DateTime.MinValue)
                cadddRow["DataAutoria"] = DBNull.Value;
            else
                cadddRow.DataAutoria = data;
            cadddRow.IDTipoNoticiaAut = CurrentControloAut.IDTipoNoticiaAut;
            cadddRow.Importacao = isImportacao;
            cadddRow.Versao = new byte[] { };
            cadddRow.isDeleted = 0;
            return cadddRow;
        }
    }
}
