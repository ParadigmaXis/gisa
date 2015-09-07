using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.IntGestDoc.Model.EntidadesInternas
{
    public class PropriedadeDocumentoGisaTemplate<T> : PropriedadeDocumentoGisa
    {
        public Dictionary<TipoOpcao, TipoEstado> EstadoRelacaoPorOpcao = new Dictionary<TipoOpcao, TipoEstado>();
        public Dictionary<TipoOpcao, T> Escolhas = new Dictionary<TipoOpcao, T>();
        public T Valor
        {
            get { return this.Escolhas.ContainsKey(this.TipoOpcao) ? this.Escolhas[this.TipoOpcao] : default(T); }
            set { this.Escolhas[this.TipoOpcao] = value; }
        }

        public PropriedadeDocumentoGisaTemplate()
        {
            this.TipoOpcao = TipoOpcao.Sugerida;
            this.EstadoRelacaoPorOpcao[this.TipoOpcao] = TipoEstado.Novo;
        }

        public void AdicionaValorOriginal(T valor)
        {
            this.Escolhas[TipoOpcao.Original] = valor;
            this.EstadoRelacaoPorOpcao[TipoOpcao.Original] = TipoEstado.SemAlteracoes;
        }

        public void RemoveValorOriginal()
        {
            this.Escolhas.Remove(TipoOpcao.Original);
            this.EstadoRelacaoPorOpcao.Remove(TipoOpcao.Original);
        }

        public void RemoveValor()
        {
            if (this.TipoOpcao == TipoOpcao.Sugerida) return;
            this.Escolhas.Remove(this.TipoOpcao);
            this.EstadoRelacaoPorOpcao.Remove(this.TipoOpcao);
            this.TipoOpcao = TipoOpcao.Sugerida;
        }

        public T GetValor(TipoOpcao opcao)
        {
            if (this.Escolhas.ContainsKey(opcao))
                return this.Escolhas[opcao];
            else
                return default(T);
        }

        public override void Revert()
        {
            var val = this.GetValor(TipoOpcao.Sugerida);
            this.TipoOpcao = val == null ? TipoOpcao.Ignorar : TipoOpcao.Sugerida;
        }
    }
}
