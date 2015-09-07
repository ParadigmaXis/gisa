using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GISA.GUIHelper;
using GISA.IntGestDoc.Model.EntidadesExternas;

namespace GISA.IntGestDoc.Model.EntidadesInternas
{

    public class DocumentoGisa : DocumentoInterno
    {
        public struct ObjectosDigitais
        {
            public string NUD;
            public int Tipo;
            public string NomeFicheiro;
            public string TipoDescricao;
        }

        public CorrespondenciaDocs Processo;
        public PropriedadeDocumentoGisaTemplate<DocumentoGisa> Serie;
        public PropriedadeDocumentoGisaTemplate<string> NumeroEspecifico;
        public PropriedadeDocumentoGisaTemplate<string> TituloDoc;
        public PropriedadeDocumentoGisaTemplate<DataIncompleta> DataCriacao;
        public PropriedadeDocumentoGisaTemplate<string> Notas;
        public PropriedadeDocumentoGisaTemplate<string> Confidencialidade;
        public PropriedadeDocumentoGisaTemplate<string> NumLocalRefPred;
        public PropriedadeDocumentoGisaTemplate<string> CodPostalLoc;
        public PropriedadeDocumentoGisaTemplate<string> Assunto;
        public List<PropriedadeDocumentoGisaTemplate<string>> Requerentes;
        public List<PropriedadeDocumentoGisaTemplate<string>> Averbamentos;
        public PropriedadeDocumentoGisaTemplate<string> Agrupador;

        public List<ObjectosDigitais> ObjDigitais { get; set; }

        public override string Titulo
        {
            get { return this.TituloDoc.Valor; }
            set { this.TituloDoc.Valor = value; }
        }

        public List<string> Produtores { get; set; }
        public List<string> Ideograficos { get; set; }
        public List<string> Onomasticos { get; set; }
        public List<string> Toponimias { get; set; }
        public string Tipologia { get; set; }

        public DocumentoGisa()
        {
            this.Produtores = new List<string>();
            this.Ideograficos = new List<string>();
            this.Onomasticos = new List<string>();
            this.Toponimias = new List<string>();

            this.Processo = null;
            this.Serie = new PropriedadeDocumentoGisaTemplate<DocumentoGisa>();
            this.NumeroEspecifico = new PropriedadeDocumentoGisaTemplate<string>();
            this.TituloDoc = new PropriedadeDocumentoGisaTemplate<string>();
            this.DataCriacao = new PropriedadeDocumentoGisaTemplate<DataIncompleta>();
            this.Notas = new PropriedadeDocumentoGisaTemplate<string>();
            this.Confidencialidade = new PropriedadeDocumentoGisaTemplate<string>();
            this.Requerentes = new List<PropriedadeDocumentoGisaTemplate<string>>();
            this.Averbamentos = new List<PropriedadeDocumentoGisaTemplate<string>>();
            this.NumLocalRefPred = new PropriedadeDocumentoGisaTemplate<string>();
            this.CodPostalLoc = new PropriedadeDocumentoGisaTemplate<string>();
            this.Assunto = new PropriedadeDocumentoGisaTemplate<string>();
            this.Agrupador = new PropriedadeDocumentoGisaTemplate<string>();

            this.ObjDigitais = new List<ObjectosDigitais>();
        }

        public void RevertProperties()
        {
            this.Serie.Revert();
            this.NumeroEspecifico.Revert(); 
            this.TituloDoc.Revert();
            this.DataCriacao.Revert();
            this.Notas.Revert();
            this.Confidencialidade.Revert();
            this.NumLocalRefPred.Revert();
            this.CodPostalLoc.Revert();
            this.Agrupador.Revert();
            this.Requerentes.Cast<PropriedadeDocumentoGisa>().ToList().ForEach(p => p.Revert());
            this.Averbamentos.Cast<PropriedadeDocumentoGisa>().ToList().ForEach(p => p.Revert());
        }

        internal List<string> Assuntos
        { 
            get
            {
                List<string> ret = new List<string>();
                ret.AddRange(this.Ideograficos);
                ret.AddRange(this.Onomasticos);
                ret.AddRange(this.Toponimias);               

                return ret;
            }
        }

        public void CopyProperties(CorrespondenciaDocs cDoc)
        {
            var fromDocument = cDoc.TipoOpcao != TipoOpcao.Ignorar ? cDoc.EntidadeInterna as DocumentoGisa : cDoc.GetEntidadeInterna(TipoOpcao.Sugerida) as DocumentoGisa;
            this.Processo = fromDocument.Processo;
            this.NumeroEspecifico = fromDocument.NumeroEspecifico;
            this.Codigo = fromDocument.Codigo;
            this.DataCriacao = fromDocument.DataCriacao;
            this.Notas = fromDocument.Notas;
            this.Confidencialidade = fromDocument.Confidencialidade;
            this.Requerentes = fromDocument.Requerentes;
            this.Averbamentos = fromDocument.Averbamentos;
            this.NumLocalRefPred = fromDocument.NumLocalRefPred;
            this.CodPostalLoc = fromDocument.CodPostalLoc;
            this.Assunto = fromDocument.Assunto;
            this.Agrupador = fromDocument.Agrupador;

            this.ObjDigitais = fromDocument.ObjDigitais;
        }

        public void CommitStateProperties()
        {
            if (this.Serie.Valor != null) this.Serie.Valor.Estado = TipoEstado.SemAlteracoes;
            this.Serie.EstadoRelacaoPorOpcao[this.Serie.TipoOpcao] = TipoEstado.SemAlteracoes;
            this.NumeroEspecifico.EstadoRelacaoPorOpcao[this.NumeroEspecifico.TipoOpcao] = TipoEstado.SemAlteracoes;
            this.DataCriacao.EstadoRelacaoPorOpcao[this.DataCriacao.TipoOpcao] = TipoEstado.SemAlteracoes;
            this.Notas.EstadoRelacaoPorOpcao[this.Notas.TipoOpcao] = TipoEstado.SemAlteracoes;
            this.Confidencialidade.EstadoRelacaoPorOpcao[this.Confidencialidade.TipoOpcao] = TipoEstado.SemAlteracoes;
            this.NumLocalRefPred.EstadoRelacaoPorOpcao[this.NumLocalRefPred.TipoOpcao] = TipoEstado.SemAlteracoes;
            this.CodPostalLoc.EstadoRelacaoPorOpcao[this.CodPostalLoc.TipoOpcao] = TipoEstado.SemAlteracoes;
            this.Assunto.EstadoRelacaoPorOpcao[this.Assunto.TipoOpcao] = TipoEstado.SemAlteracoes;
            this.Agrupador.EstadoRelacaoPorOpcao[this.Agrupador.TipoOpcao] = TipoEstado.SemAlteracoes;

            this.Requerentes.ForEach(r => r.EstadoRelacaoPorOpcao[r.TipoOpcao] = TipoEstado.SemAlteracoes);
            this.Averbamentos.ForEach(a => a.EstadoRelacaoPorOpcao[a.TipoOpcao] = TipoEstado.SemAlteracoes);
        }
    }    
}