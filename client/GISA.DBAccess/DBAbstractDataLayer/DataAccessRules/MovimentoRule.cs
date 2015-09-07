using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DBAbstractDataLayer.DataAccessRules
{
    public abstract class MovimentoRule : DALRule
    {
        private static MovimentoRule current = null;
        public static void ClearCurrent()
        {
            current = null;
        }

        public static MovimentoRule Current
        {
            get
            {
                if (Object.ReferenceEquals(null, current))
                {
                    current = (MovimentoRule)Create(typeof(MovimentoRule));
                }
                return current;
            }
        }

        public abstract void LoadMovimento(long movID, DataSet currentDataSet, IDbConnection conn);

        public abstract void CalculateOrderedItems(string catCode, string FiltroNroMovimento, string FiltroDataInicio, string FiltroDataFim, string FiltroEntidade, string FiltroCodigo, IDbConnection conn);
        public abstract ArrayList GetItems(DataSet currentDataSet, int pageNr, int itemsPerPage, IDbConnection conn);
        public abstract void DeleteTemporaryResults(IDbConnection conn);

        public abstract List<DocumentoMovimentado> GetDocumentos(long IDMov, DataSet currentDataSet, IDbConnection conn);
        public abstract List<DocumentoMovimentado> GetDocumentos(long IDMov, string filter, DataSet currentDataSet, IDbConnection conn);

        public abstract bool IsDeletable(long IDMov, IDbConnection conn);
        public abstract bool CanDeleteEntity(long IDEntidade, IDbConnection conn);

        #region MasterPanelSeries
        public abstract bool foiMovimentado(long IDNivel, IDbConnection conn);
        #endregion

        public class DocumentoMovimentado
        {
            private long idNivel;
            private string codigoCompleto;
            private string designacao;                                 
            private string nivelDescricao;

            private string anoInicio;
            private string mesInicio;
            private string diaInicio;
            private string anoFim;
            private string mesFim;
            private string diaFim;

            public long IDNivel
            {
                get { return this.idNivel; }
                set { this.idNivel = value; }
            }

            public string CodigoCompleto
            {
                get { return this.codigoCompleto; }
                set { this.codigoCompleto = value; }
            }

            public string Designacao
            {
                get { return this.designacao; }
                set { this.designacao = value; }
            }                        
            
            public string NivelDescricao
            {
                get { return this.nivelDescricao; }
                set { this.nivelDescricao = value; }
            }

            public string AnoInicio
            {
                get { return this.anoInicio; }
                set { this.anoInicio = value; }
            }

            public string MesInicio
            {
                get { return this.mesInicio; }
                set { this.mesInicio = value; }
            }

            public string DiaInicio
            {
                get { return this.diaInicio; }
                set { this.diaInicio = value; }
            }

            public string AnoFim
            {
                get { return this.anoFim; }
                set { this.anoFim = value; }
            }

            public string MesFim
            {
                get { return this.mesFim; }
                set { this.mesFim = value; }
            }

            public string DiaFim
            {
                get { return this.diaFim; }
                set { this.diaFim = value; }
            }

            public DocumentoMovimentado()
            {
                this.idNivel = -1;
                this.codigoCompleto = string.Empty;
                this.designacao = string.Empty;                
                this.nivelDescricao = string.Empty;

                this.anoInicio = string.Empty;
                this.mesInicio = string.Empty;
                this.diaInicio = string.Empty;

                this.anoFim = string.Empty;
                this.mesFim = string.Empty;
                this.diaFim = string.Empty;
            }
        }

        public abstract bool estaRequisitado(long idNivel, IDbConnection conn);
        public abstract bool estaRequisitado(long idNivel, IDbTransaction tran);
        public abstract bool temMovimentosPosteriores(long IDNivel, long IDMovimento, string MovimentoCatCode, IDbTransaction tran);
        public abstract bool CanDeleteMovimento(long IDMovimento, string MovimentoCatCode, IDbTransaction tran);

        public class RequisicaoInfo {
            public long idNivel;
            public long idMovimento;
            public string entidade;
            public DateTime data;
            public string notas;
        }
        public abstract RequisicaoInfo getRequisicaoInfo(long idNivel, IDbConnection conn);

        public class DocumentoRequisicaoInfo {
            public long idNivel;
            public string ND_Designacao;
            public string Codigo_Completo;

            public long idMovimento;
            public string entidade = string.Empty;
            public DateTime data;
            public string notas = string.Empty;
        }
        public abstract List<DocumentoRequisicaoInfo> getDocumentosNaoDevolvidos(IDbConnection conn);
        public abstract long getCountDocumentosNaoDevolvidos(IDbConnection conn);

        #region EntidadeList

        public abstract void Entidade_CalculateOrderedItems(string activo, string FiltroTermoLike, string CodigoEntidadeLike, IDbConnection conn);
        public abstract ArrayList Entidade_GetItems(DataSet currentDataSet, int pageNr, int itemsPerPage, string FiltroTermoLike, IDbConnection conn);
        public abstract void Entidade_DeleteTemporaryResults(IDbConnection conn);
        #endregion

        public abstract IDataReader GetAllMovimentos(DateTime data_inicio, DateTime data_fim, IDbConnection conn);
    }
}
