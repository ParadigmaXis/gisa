using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GISA.Model;

namespace GISA.IntGestDoc.Model
{
    public enum TipoEntidadeExterna
    {
        Ideografico = 1,
        Onomastico = 2,
        Geografico = 3,
        Produtor = 4,
        TipologiaInformacional = 5,
        Documento = 6,
        DocumentoComposto = 7,
        DocumentoAnexo = 8
    }

    public enum TipoEntidadeInterna : int
    {
        DocumentoSimples = 0,
        DocumentoComposto = 1,
        Serie = 2,
        SubSerie = 3,
        EntidadeProdutora = 4,
        Onomastico = 5,
        Ideografico = 6,
        Geografico = 7,
        Tipologia = 8
    }

    public static class TipoEntidade
    {
        public static TipoEntidadeInterna GetTipoEntidadeInterna(long tipoRelacionado)
        {
            if (tipoRelacionado == (long)TipoNivelRelacionado.SD)
                return TipoEntidadeInterna.DocumentoSimples;
            else if (tipoRelacionado == (long)TipoNivelRelacionado.D)
                return TipoEntidadeInterna.DocumentoComposto;
            else if (tipoRelacionado == (long)TipoNivelRelacionado.SR)
                return TipoEntidadeInterna.Serie;
            else if (tipoRelacionado == (long)TipoNivelRelacionado.SSR)
                return TipoEntidadeInterna.SubSerie;
            else
                throw new Exception("Tipo desconhecido");
        }

        public static TipoEntidadeInterna GetTipoEntidadeInterna(TipoEntidadeExterna tipoExterno)
        {
            switch (tipoExterno)
            {
                case TipoEntidadeExterna.Documento :
                case TipoEntidadeExterna.DocumentoAnexo:
                    return TipoEntidadeInterna.DocumentoSimples;
                case TipoEntidadeExterna.DocumentoComposto:
                    return TipoEntidadeInterna.DocumentoComposto;
                case TipoEntidadeExterna.Geografico:
                    return TipoEntidadeInterna.Geografico;
                case TipoEntidadeExterna.Ideografico:
                    return TipoEntidadeInterna.Ideografico;
                case TipoEntidadeExterna.Onomastico:
                    return TipoEntidadeInterna.Onomastico;
                case TipoEntidadeExterna.Produtor:
                    return TipoEntidadeInterna.EntidadeProdutora;
                case TipoEntidadeExterna.TipologiaInformacional:
                    return TipoEntidadeInterna.Tipologia;
                default:
                    throw new Exception("Tipo desconhecido");
            }
        }

        public static TipoNoticiaAut GetTipoNoticiaAut(TipoEntidadeExterna tipoExterno)
        {
            switch (tipoExterno)
            {
                case TipoEntidadeExterna.Geografico:
                    return TipoNoticiaAut.ToponimicoGeografico;
                case TipoEntidadeExterna.Ideografico:
                    return TipoNoticiaAut.Ideografico;
                case TipoEntidadeExterna.Onomastico:
                    return TipoNoticiaAut.Onomastico;
                case TipoEntidadeExterna.Produtor:
                    return TipoNoticiaAut.EntidadeProdutora;
                case TipoEntidadeExterna.TipologiaInformacional:
                    return TipoNoticiaAut.TipologiaInformacional;
                default:
                    throw new Exception("Tipo desconhecido");
            }
        }
    }
}
