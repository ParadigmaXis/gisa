using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBAbstractDataLayer.DataAccessRules
{
    public abstract class IntGestDocRule : DALRule
    {
        private static IntGestDocRule current = null;
        public static void ClearCurrent()
        {
            current = null;
        }
        public static IntGestDocRule Current
        {
            get
            {
                if (Object.ReferenceEquals(null, current))
                {
                    current = (IntGestDocRule)Create(typeof(IntGestDocRule));
                }
                return current;
            }
        }

        public abstract long GetSerie(DataSet currentDataSet, long ID, IDbConnection conn);

        public abstract List<DocGisaInfo> LoadDocsCorrespondenciasAnteriores(DataSet currentDataSet, List<EntidadeExterna> docsExternos, int IDTipoEntidade, IDbConnection conn);
        public abstract Dictionary<string, DocGisaInfo> LoadDocsCorrespondenciasNovas(DataSet currentDataSet, List<string> idsExternos, long IDTipoNivelRelacionado, IDbConnection conn);

        public abstract void LoadRAsCorrespondenciasAnteriores(DataSet currentDataSet, List<EntidadeExterna> rasExternos, IDbConnection conn);
        public abstract Dictionary<long, long> LoadRAsCorrespondenciasNovas(DataSet currentDataSet, Dictionary<long, EntidadeExterna> dalraes, IDbConnection conn);

        public abstract List<DocInPortoRecord> FilterPreviousIncorporations(List<DocInPortoRecord> diprecords, IDbConnection conn);
        public abstract void LoadInteg_Config(DataSet currentDataSet, IDbConnection conn);

        public abstract List<long> GetProdutoresRelDirect(long IDNivel, IDbConnection conn);
        public abstract void LoadDocumentDetails(DataSet currentDataSet, long IDNivelDoc, IDbConnection conn);

        public class EntidadeExterna
        {
            public string IDExterno;
            public string Titulo;
            public int Sistema;
            public int TipoEntidade;

            /*public override int GetHashCode()
            {
                return this.IDExterno.GetHashCode() ^ this.Sistema.GetHashCode() ^ this.TipoEntidade.GetHashCode() ^ this.Titulo.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                bool isEqual = false;
                if (obj is EntidadeExterna)
                {
                    EntidadeExterna other = (EntidadeExterna)obj;
                    isEqual = this.IDExterno == other.IDExterno && this.Sistema == other.Sistema && this.TipoEntidade == other.TipoEntidade && this.Titulo == other.Titulo;
                }
                return isEqual;
            }*/
        }

        public struct DocGisaInfo
        {
            public long IDNivel;
            public List<long> IDNivelProdutores;
        }

        public struct DocInPortoRecord
        {
            public string IDExterno;
            public DateTime DataArquivo;
            public int IDTipoEntidade;
            public int IDSistema;
        }
    }
}
