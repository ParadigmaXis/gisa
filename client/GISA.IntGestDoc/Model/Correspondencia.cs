using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;

using GISA;
using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;

using GISA.IntGestDoc.Model.EntidadesExternas;
using GISA.IntGestDoc.Model.EntidadesInternas;

namespace GISA.IntGestDoc.Model
{
    public enum TipoSugestao
    {
        Historico, // baseada em correspondencia anteriores
        Heuristica, // baseada no confronto com os dados já existentes no gisa
        Criacao, // correspondencia com uma entidade nova
        NaoSugerido
    }

    public abstract class Correspondencia
    {
        private Dictionary<TipoOpcao, EntidadeInterna> mEscolhasEntidadesInternas = 
            new Dictionary<TipoOpcao, EntidadeInterna>();
        public List<EntidadeInterna> EscolhasEntidadesInternas
        {
            get
            {
                var entLst = new List<EntidadeInterna>();
                foreach (TipoOpcao op in Enum.GetValues(typeof(TipoOpcao)))
                {
                    var ei = this.GetEntidadeInterna(op);
                    if (ei == null) continue;

                    entLst.Add(ei);
                }
                return entLst;
            }
        }
        public EntidadeExterna EntidadeExterna { get; set; }
        public EntidadeInterna EntidadeInterna
        {
            get 
            {
                EntidadeInterna ei = null;
                if (this.mEscolhasEntidadesInternas.ContainsKey(this.TipoOpcao))
                    ei = this.mEscolhasEntidadesInternas[this.TipoOpcao];
                return ei;
            }
            set {
                this.TipoOpcao = TipoOpcao.Trocada;
                this.mEscolhasEntidadesInternas[this.TipoOpcao] = value;
            }
        }

        public void AddEntidadeInternaOriginal(EntidadeInterna ei)
        {
            this.mEscolhasEntidadesInternas[TipoOpcao.Original] = ei;
            this.EstadoRelacaoPorOpcao[TipoOpcao.Original] = TipoEstado.SemAlteracoes;
        }

        public void RemoveEntidadeInternaOriginal()
        {
            this.mEscolhasEntidadesInternas.Remove(TipoOpcao.Original);
            this.EstadoRelacaoPorOpcao.Remove(TipoOpcao.Original);
        }

        public EntidadeInterna GetEntidadeInterna(TipoOpcao opcao)
        {
            if (this.mEscolhasEntidadesInternas.ContainsKey(opcao))
                return this.mEscolhasEntidadesInternas[opcao];
            else
                return null;
        }

        public TipoOpcao TipoOpcao { get; set; }

        private TipoSugestao tipoSugestao;
        public TipoSugestao TipoSugestao {
            get {return tipoSugestao;}
            set 
            {
                this.tipoSugestao = value;
                if (value == TipoSugestao.NaoSugerido)
                    this.TipoOpcao = TipoOpcao.Trocada;
            }
        }

        public Dictionary<TipoOpcao, TipoEstado> EstadoRelacaoPorOpcao = new Dictionary<TipoOpcao, TipoEstado>();

        internal Correspondencia(EntidadeExterna ee, EntidadeInterna ei, TipoSugestao tipoSugestao) 
        {
            this.TipoOpcao = TipoOpcao.Sugerida; //assume-se correspondencias instanciadas são sempre sugestões
            this.TipoSugestao = tipoSugestao;
            this.EntidadeExterna = ee;
            this.mEscolhasEntidadesInternas[TipoOpcao.Sugerida] = ei;
        }
    }
}