using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GISA.IntGestDoc.Model;
using GISA.IntGestDoc.Model.EntidadesInternas;

namespace GISA.IntGestDoc.Tests.MockObjects
{
    /*
    public class MockDocInPortoMapper : DocInPortoMapper
    {
        private List<DocumentoGisa> docs = new List<DocumentoGisa>();
        private List<RegistoAutoridadeInterno> rais = new List<RegistoAutoridadeInterno>();

        public MockDocInPortoMapper() { }

        public MockDocInPortoMapper(List<DocumentoGisa> doc)
        {
            this.docs = doc;
        }

        public MockDocInPortoMapper(List<RegistoAutoridadeInterno> rai)
        {
            this.rais = rai;
        }

        protected override void FindNivelByName(DocumentoGisa doc, ref long IDNivel, ref string DesignacaoGisa, ref string CodigoGisa)
        {
            // valores que indicam que não foi encontrado nada
            IDNivel = -1;
            DesignacaoGisa = string.Empty;
            CodigoGisa = string.Empty;

            foreach (DocumentoGisa d in this.docs)
            {
                if (doc.Designacao.Equals(d.Designacao))
                {
                    IDNivel = d.Id;
                    DesignacaoGisa = d.Designacao;
                    CodigoGisa = d.Codigo;

                    return;
                }
            }
        }

        protected override void FindCaByName(ControloAut ca, ref long IDControloAut, ref string DesignacaoGisa, ref bool termoExists)
        {
            // valores que indicam que não foi encontrado nada
            IDControloAut = -1;
            DesignacaoGisa = string.Empty;
            termoExists = false;

            foreach (EntidadeGisaControloAut entGisaCA in this.rais)
            {
                if (ca.Designacao.Equals(entGisaCA.Designacao))
                {
                    IDControloAut = entGisaCA.Id;
                    DesignacaoGisa = entGisaCA.Designacao;
                    termoExists = entGisaCA.reuseTermo;

                    return;
                }
            }
        }

        public override void CheckCAsRelated(DocumentoGisa entGisaN)
        {
            foreach (EntidadeGisaControloAut entGisaCA in entGisaN.ControloAuts)
                entGisaCA.Documentos[entGisaN] = false;
        }

        protected override EntidadeGisaControloAut GetEntGisaExistente(ControloAut ca)
        {
            return this.rais.FirstOrDefault(o => o.Id == ca.Id);
        }
    }
     * */
}
