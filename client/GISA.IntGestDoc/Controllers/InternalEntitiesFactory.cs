using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GISA.IntGestDoc.Model;
using GISA.IntGestDoc.Model.EntidadesExternas;
using GISA.IntGestDoc.Model.EntidadesInternas;

namespace GISA.IntGestDoc.Controllers
{
    public static class InternalEntitiesFactory
    {
        public static Dictionary<DocumentoExterno, DocumentoInterno> CreateInternalEntities(List<DocumentoExterno> docsSemCorrespNovas)
        {
            Dictionary<DocumentoInterno, DocumentoInterno> des = new Dictionary<DocumentoInterno, DocumentoInterno>();

            var result = new Dictionary<DocumentoExterno, DocumentoInterno>();
            DocumentoInterno docGisa = null;
            foreach (var docExt in docsSemCorrespNovas)
            {
                try
                {
                    if (docExt.GetType() == typeof(Model.EntidadesExternas.DocumentoSimples))
                        docGisa = AddEntidade(des, CreateInternalEntity(docExt as DocumentoSimples));
                    else if (docExt.GetType() == typeof(DocumentoComposto))
                        docGisa = AddEntidade(des, CreateInternalEntity(docExt as DocumentoComposto));
                    else
                        docGisa = AddEntidade(des, CreateInternalEntity(docExt as DocumentoAnexo));
                }
                catch (Exception) { throw; }

                result.Add(docExt, docGisa);
            }
            return result;
        }

        public static Dictionary<RegistoAutoridadeExterno, RegistoAutoridadeInterno> CreateInternalEntities(List<RegistoAutoridadeExterno> rasSemCorrespNovasEncontradas)
        {
            Dictionary<RegistoAutoridadeInterno, RegistoAutoridadeInterno> raes = new Dictionary<RegistoAutoridadeInterno, RegistoAutoridadeInterno>();

            var result = new Dictionary<RegistoAutoridadeExterno, RegistoAutoridadeInterno>();
            foreach (var rae in rasSemCorrespNovasEncontradas.OfType<Model.EntidadesExternas.Onomastico>())
                result.Add(rae, AddEntidade(raes, CreateInternalEntity(rae)));
            foreach (var rae in rasSemCorrespNovasEncontradas.OfType<Model.EntidadesExternas.Ideografico>())
                result.Add(rae, AddEntidade(raes, CreateInternalEntity<Model.EntidadesInternas.Ideografico, Model.EntidadesExternas.Ideografico>(rae)));
            foreach (var rae in rasSemCorrespNovasEncontradas.OfType<Model.EntidadesExternas.Geografico>())
                result.Add(rae, AddEntidade(raes, CreateInternalEntity<Model.EntidadesInternas.Geografico, Model.EntidadesExternas.Geografico>(rae)));
            foreach (var rae in rasSemCorrespNovasEncontradas.OfType<Model.EntidadesExternas.Tipologia>())
                result.Add(rae, AddEntidade(raes, CreateInternalEntity<Model.EntidadesInternas.Tipologia, Model.EntidadesExternas.Tipologia>(rae)));
            foreach (var rae in rasSemCorrespNovasEncontradas.OfType<Model.EntidadesExternas.Produtor>())
                result.Add(rae, AddEntidade(raes, CreateInternalEntity(rae)));
            return result;
        }

        private static DocumentoGisa CreateInternalEntity(DocumentoSimples de)
        {
            DocumentoGisa di = new DocumentoGisa();
            di.Tipo = TipoEntidadeInterna.DocumentoSimples;
            di.Estado = TipoEstado.Novo;
            di.Id = -1;
            di.Codigo = de.NUD;
            Database.Database.FillDocumentoGisa(de, di, TipoOpcao.Sugerida);

            return di;
        }

        private static DocumentoGisa CreateInternalEntity(DocumentoComposto documentoComposto)
        {
            DocumentoGisa di = new DocumentoGisa();
            di.Tipo = TipoEntidadeInterna.DocumentoComposto;
            di.Estado = TipoEstado.Novo;
            di.Id = -1;
            di.Codigo = documentoComposto.NUP;
            di.Titulo = documentoComposto.NUP;
            Database.Database.FillDocumentoGisa(documentoComposto, di, TipoOpcao.Sugerida);
            return di;
        }

        private static DocumentoGisa CreateInternalEntity(DocumentoAnexo documentoAnexo)
        {
            DocumentoGisa di = new DocumentoGisa();
            di.Tipo = TipoEntidadeInterna.DocumentoSimples;
            di.Estado = TipoEstado.Novo;
            di.Id = -1;
            di.Codigo = documentoAnexo.NUD;
            Database.Database.FillDocumentoGisa(documentoAnexo, di, TipoOpcao.Sugerida);
            return di;
        }

        private static Model.EntidadesInternas.Onomastico CreateInternalEntity(Model.EntidadesExternas.Onomastico regAutExt)
        {
            Model.EntidadesInternas.Onomastico result = new Model.EntidadesInternas.Onomastico();
            result.Estado = TipoEstado.Novo;
            result.Titulo = regAutExt.Titulo.Trim();
            if (!String.IsNullOrEmpty(regAutExt.NIF)) result.Codigo = regAutExt.NIF;
            return result;
        }

        private static Model.EntidadesInternas.Produtor CreateInternalEntity(Model.EntidadesExternas.Produtor regAutExt)
        {
            Model.EntidadesInternas.Produtor result = new Model.EntidadesInternas.Produtor();
            result.Estado = TipoEstado.Novo;
            result.Titulo = regAutExt.Codigo.Trim();
            result.Codigo = regAutExt.Codigo.Trim().Replace('/', '.');
            return result;
        }

        private static Xi CreateInternalEntity<Xi, Xe>(Xe regAutExt)
            where Xi : RegistoAutoridadeInterno, new()
            where Xe : RegistoAutoridadeExterno
        {
            Xi result = new Xi();
            result.Estado = TipoEstado.Novo;
            result.Titulo = regAutExt.Titulo.Trim();
            return result;
        }

        internal static X AddEntidade<X>(Dictionary<X, X> lstEntidades, X novaEntidade) where X : Entidade
        {
            if (!lstEntidades.ContainsKey(novaEntidade))
            {
                lstEntidades.Add(novaEntidade, novaEntidade);
                return novaEntidade;
            }
            return lstEntidades[novaEntidade];
        }
    }

    // serve para garantir que não existem entidades repetidas entre aquelas que são criadas aquando da edição das sugestões
    public class InternalEntities
    {
        private Dictionary<EntidadeInterna, EntidadeInterna> lstEntidadesInternas;

        public InternalEntities() { this.lstEntidadesInternas = new Dictionary<EntidadeInterna,EntidadeInterna>(); }

        public EntidadeInterna AddInternalEntity<X>(X InternalEntity) where X : EntidadeInterna
        {
            return InternalEntitiesFactory.AddEntidade(lstEntidadesInternas, InternalEntity);
        }
    }
}
