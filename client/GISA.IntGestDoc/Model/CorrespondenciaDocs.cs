using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GISA.IntGestDoc.Model.EntidadesExternas;
using GISA.IntGestDoc.Model.EntidadesInternas;

namespace GISA.IntGestDoc.Model
{
    public class CorrespondenciaDocs: Correspondencia
    {
        private List<CorrespondenciaRAs> mCorrespondenciasRAs =
            new List<CorrespondenciaRAs>();

        public bool Edited = false;

        internal CorrespondenciaDocs(DocumentoSimples de, DocumentoGisa di, TipoSugestao tipoSugestao)
            :base(de, di, tipoSugestao) { }
        internal CorrespondenciaDocs(DocumentoAnexo de, DocumentoGisa di, TipoSugestao tipoSugestao)
            : base(de, di, tipoSugestao) { }
        internal CorrespondenciaDocs(DocumentoComposto de, DocumentoGisa di, TipoSugestao tipoSugestao)
            : base(de, di, tipoSugestao) { }

        public List<CorrespondenciaRAs> CorrespondenciasRAs { get { return mCorrespondenciasRAs.Where(c => c.TipoOpcao != TipoOpcao.Ignorar).ToList(); } }
        public List<CorrespondenciaRAs> AllCorrespondenciasRAs { get { return mCorrespondenciasRAs; } }

        public void AddCorrespondenciaRA(CorrespondenciaRAs CRa)
        {
            mCorrespondenciasRAs.Add(CRa);
        }
    }
}
