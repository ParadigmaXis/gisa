/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2008-04-16
 * Time: 12:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace GISA.Search
{
	/// <summary>
	/// Description of PesquisaAvancada.
	/// </summary>
	public class NivelDocumentalSearch {
        
        #region Field defs.: Standard

        string id;
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        string textoLivre;

        public string TextoLivre
        {
            get { return textoLivre; }
            set { textoLivre = value; }
        }

		int modulo;
		
		public int Modulo {
			get { return modulo; }
			set { modulo = value; }
		}
		
		string codigoParcial;
		
		public string CodigoParcial {
			get { return codigoParcial; }
			set { codigoParcial = value; }
		}
		
		string designacao;
		
		public string Designacao {
			get { return designacao; }
			set { designacao = value; }
		}

        string autor;

        public string Autor
        {
            get { return autor; }
            set { autor = value; }
        }
		
		string entidadeProdutora;
		
		public string EntidadeProdutora {
			get { return entidadeProdutora; }
			set { entidadeProdutora = value; }
		}

        string[] niveisDocumentais = new string[] { };

        public string[] NiveisDocumentais
        {
            get { return niveisDocumentais; }
            set { niveisDocumentais = value; }
        }

        int niveisDocumentaisOP = 0;

        public int NiveisDocumentaisOP
        {
            get { return niveisDocumentaisOP; }
            set { niveisDocumentaisOP = value; }
        }
		
		string dataProducaoInicio;		
		public string DataProducaoInicio {
			get { return dataProducaoInicio; }
			set { dataProducaoInicio = value; }
		}
		
		string dataProducaoFim;
		
		public string DataProducaoFim {
			get { return dataProducaoFim; }
			set { dataProducaoFim = value; }
		}

        string dataProducaoInicioDoFim;
        public string DataProducaoInicioDoFim
        {
            get { return dataProducaoInicioDoFim; }
            set { dataProducaoInicioDoFim = value; }
        }

        string dataProducaoFimDoFim;
        public string DataProducaoFimDoFim
        {
            get { return dataProducaoFimDoFim; }
            set { dataProducaoFimDoFim = value; }
        }

		string tipologiaInformacional;
		
		public string TipologiaInformacional {
			get { return tipologiaInformacional; }
			set { tipologiaInformacional = value; }
		}
				
		string termosIndexacao;
		
		public string TermosIndexacao {
			get { return termosIndexacao; }
			set { termosIndexacao = value; }
		}
		
		string conteudoInformacional;
		
		public string ConteudoInformacional {
			get { return conteudoInformacional; }
			set { conteudoInformacional = value; }
		}
		
		string notas;
		
		public string Notas {
			get { return notas; }
			set { notas = value; }
		}
		
		string cota;
		
		public string Cota {
			get { return cota; }
			set { cota = value; }
		}

        string agrupador;
        public string Agrupador
        {
            get { return agrupador; }
            set { agrupador = value; }
        }

        string soComODs;
        public string SoComODs
        {
            get { return soComODs; }
            set { soComODs = value; }
        }

        string soComODsPub;
        public string SoComODsPub
        {
            get { return soComODsPub; }
            set { soComODsPub = value; }
        }

        string soComODsNaoPub;
        public string SoComODsNaoPub
        {
            get { return soComODsNaoPub; }
            set { soComODsNaoPub = value; }
        }

		string[] suporteEAcondicionamento = new string[]{};
		
		public string[] SuporteEAcondicionamento {
			get { return suporteEAcondicionamento; }
			set { suporteEAcondicionamento = value; }
		}
		
		int suporteEAcondicionamentoOP = 0;
		
		public int SuporteEAcondicionamentoOP {
			get { return suporteEAcondicionamentoOP; }
			set { suporteEAcondicionamentoOP = value; }
		}
		
		string[] materialDeSuporte = new string[]{};
		
		public string[] MaterialDeSuporte {
			get { return materialDeSuporte; }
			set { materialDeSuporte = value; }
		}
		
		int materialDeSuporteOP = 0;
		
		public int MaterialDeSuporteOP {
			get { return materialDeSuporteOP; }
			set { materialDeSuporteOP = value; }
		}
		
		string[] tecnicaRegisto = new string[]{};
		
		public string[] TecnicaRegisto {
			get { return tecnicaRegisto; }
			set { tecnicaRegisto = value; }
		}
		
		int tecnicaRegistoOP = 0;
		
		public int TecnicaRegistoOP {
			get { return tecnicaRegistoOP; }
			set { tecnicaRegistoOP = value; }
		}
		
		string[] estadoConservacao = new string[]{};
		
		public string[] EstadoConservacao {
			get { return estadoConservacao; }
			set { estadoConservacao = value; }
		}
		
		int estadoConservacaoOP = 0;
		
		public int EstadoConservacaoOP {
			get { return estadoConservacaoOP; }
			set { estadoConservacaoOP = value; }
        }
        #endregion

        #region Field defs.: Licencas de obra

        private string nome_LicencaObraRequerentes = string.Empty;
        public string Nome_LicencaObraRequerentes {
            get { return nome_LicencaObraRequerentes; }
            set { nome_LicencaObraRequerentes = value; }
        }

        // Termo_LicencaObraLocalizacaoObraActual,
        private string localizacaoObra_Actual = string.Empty;
        public string LocalizacaoObra_Actual {
            get { return localizacaoObra_Actual; }
            set { localizacaoObra_Actual = value; }
        }

        // NomeLocal_LicencaObraLocalizacaoObraAntiga:
        private string localizacaoObra_Antiga = string.Empty;
        public string LocalizacaoObra_Antiga {
            get { return localizacaoObra_Antiga; }
            set { localizacaoObra_Antiga = value; }
        }

        private string numPolicia_Actual = string.Empty;
        public string NumPolicia_Actual {
            get { return numPolicia_Actual; }
            set { numPolicia_Actual = value; }
        }

        private string numPolicia_Antigo = string.Empty;
        public string NumPolicia_Antigo {
            get { return numPolicia_Antigo; }
            set { numPolicia_Antigo = value; }
        }

        private string licencaObra_TipoObra = string.Empty;
        public string LicencaObra_TipoObra {
            get { return licencaObra_TipoObra; }
            set { licencaObra_TipoObra = value; }
        }
        
        private string termo_LicencaObraTecnicoObra = string.Empty;
        public string Termo_LicencaObraTecnicoObra {
            get { return termo_LicencaObraTecnicoObra; }
            set { termo_LicencaObraTecnicoObra = value; }
        }

        private string codigosAtestadoHabitabilidade = string.Empty;
        public string CodigosAtestadoHabitabilidade {
            get { return codigosAtestadoHabitabilidade; }
            set { codigosAtestadoHabitabilidade = value; }
        }

        private string datas_LicencaObraDataLicencaConstrucao_Inicio = string.Empty;
        public string Datas_LicencaObraDataLicencaConstrucao_Inicio {
            get { return datas_LicencaObraDataLicencaConstrucao_Inicio; }
            set { datas_LicencaObraDataLicencaConstrucao_Inicio = value; }
        }

        private string datas_LicencaObraDataLicencaConstrucao_Fim = string.Empty;
        public string Datas_LicencaObraDataLicencaConstrucao_Fim {
            get { return datas_LicencaObraDataLicencaConstrucao_Fim; }
            set { datas_LicencaObraDataLicencaConstrucao_Fim = value; }
        }

        private bool licencaObra_PHSimNao = false;
        public bool LicencaObra_PHSimNao {
            get { return licencaObra_PHSimNao; }
            set { licencaObra_PHSimNao = value; }
        }

        #endregion

        public NivelDocumentalSearch()
		{
			
		}
		
		public override string ToString(){

            StringBuilder str = new StringBuilder();

            #region Field defs.: Standard

            if (this.modulo == 1) str.Append("(publicar: sim)");
            else if (this.modulo == 2) str.Append("(publicar: nao)");
            else str.Append("(existe: sim)");

            if (this.Id != null && this.id.Trim().Length > 0) str.Append(" AND ").Append(string.Format("(id:{0})", this.id));
            if (this.textoLivre.Trim().Length > 0) str.Append(" AND ").Append(string.Format("({0})",this.textoLivre));
			if (this.codigoParcial.Trim().Length > 0) str.Append(" AND ").Append(string.Format("(codigo:{0})",this.codigoParcial.ToLower()));
            if (this.designacao.Length > 0) str.Append(" AND ").Append(string.Format("(titulo: ({0}))",this.designacao));
            if (this.autor.Length > 0) str.Append(" AND ").Append(string.Format("(autor: ({0}))", this.autor));
            if (this.entidadeProdutora.Length > 0) str.Append(" AND ").Append(string.Format("(entidadeProdutora: ({0}))",this.entidadeProdutora));
            if (this.niveisDocumentais.Length > 0) str.Append(" AND ")
                                                        .Append("(designacaoTipoNivelRelacionado:(\"")
                                                        .Append(string.Join("\" OR \"", this.niveisDocumentais))
                                                        .Append("\"))");
            if (this.dataProducaoInicio != null && this.dataProducaoInicio.Trim().Length > 0)
            {
                str.Append(" AND ");
                if (this.dataProducaoFim != null && this.dataProducaoFim.Trim().Length > 0)
                    str.AppendFormat("(inicioProducao: [{0} TO {1}])", this.dataProducaoInicio, this.dataProducaoFim);
                else 
                    str.AppendFormat("(inicioProducao: [{0} TO 99999999])", this.dataProducaoInicio);
            } 
            else if (this.dataProducaoFim != null && this.dataProducaoFim.Trim().Length > 0)
                str.Append(" AND ").AppendFormat("(inicioProducao: [00000000 TO {0}])", this.dataProducaoFim);

            if (this.dataProducaoInicioDoFim!= null && this.dataProducaoInicioDoFim.Trim().Length > 0)
            {
                str.Append(" AND ");
                if (this.dataProducaoFimDoFim != null && this.dataProducaoFimDoFim.Trim().Length > 0)
                    str.AppendFormat("(fimProducao: [{0} TO {1}])", this.dataProducaoInicioDoFim, this.dataProducaoFimDoFim);
                else
                    str.AppendFormat("(fimProducao: [{0} TO 99999999])", this.dataProducaoInicioDoFim);
            }
            else if (this.dataProducaoFimDoFim != null && this.dataProducaoFimDoFim.Trim().Length > 0)
                str.Append(" AND ").AppendFormat("(fimProducao: [00000000 TO {0}])", this.dataProducaoFimDoFim);
			
			if (this.tipologiaInformacional.Length > 0) str.Append(" AND ").Append(string.Format("(tipologiaInformacional: ({0}))",this.tipologiaInformacional));
            if (this.termosIndexacao.Length > 0) str.Append(" AND ").Append(string.Format("(assunto: ({0}))", this.termosIndexacao));
            if (this.conteudoInformacional.Length > 0) str.Append(" AND ").Append(string.Format("(conteudo: ({0}))", this.conteudoInformacional));
            if (this.notas.Length > 0) str.Append(" AND ").Append(string.Format("(notaGeral: ({0}))", this.notas));
            if (this.cota.Length > 0) str.Append(" AND ").Append(string.Format("(cota: ({0}))", this.cota));
            if (this.agrupador.Length > 0) str.Append(" AND ").Append(string.Format("(agrupador: ({0}))", this.agrupador));
            if (this.soComODs.Length > 0) str.Append(" AND ").Append(string.Format("(objetos: ({0}))", this.soComODs));
            if (this.soComODsPub.Length > 0) str.Append(" AND ").Append(string.Format("(objetosPublicados: ({0}))", this.soComODsPub));
            if (this.soComODsNaoPub.Length > 0) str.Append(" AND ").Append(string.Format("(objetosNaoPublicados: ({0}))", this.soComODsNaoPub));
            if (this.suporteEAcondicionamento.Length > 0)
            {
                str.Append(" AND ");

	      		string op = "OR";
				if (this.suporteEAcondicionamentoOP == 0)
					op = "AND";

                str.Append("(designacoesTipoFormaSuporteAcond:");
                str.Append(string.Join(" " + op + " designacoesTipoFormaSuporteAcond:", this.suporteEAcondicionamento));
                str.Append(")");												
	      	}
	      	
	      	if (this.materialDeSuporte.Length > 0)
            {
                str.Append(" AND ");
                
                string op = "OR";
				if(this.materialDeSuporteOP == 0)
					op = "AND";

                str.Append("(designacoesTipoMaterialDeSuporte:");
                str.Append(string.Join(" " + op + " designacoesTipoMaterialDeSuporte:", this.materialDeSuporte));
                str.Append(")");												
	      	}

	      	if(this.tecnicaRegisto.Length > 0)
            {
                str.Append(" AND ");
                
                string op = "OR";
				if(this.tecnicaRegistoOP == 0)
					op = "AND";

                str.Append("(designacoesTipoTecnicasDeRegisto:");
                str.Append(string.Join(" " + op + " designacoesTipoTecnicasDeRegisto:", this.tecnicaRegisto));
                str.Append(")");												
	      	}
	      	
	      	if(this.estadoConservacao.Length > 0){
                str.Append(" AND ");                

	      		string op = "OR";
				if(this.estadoConservacaoOP == 0)
					op = "AND";

                str.Append("(designacoesTipoEstadoDeConservacao:");
                str.Append(string.Join(" " + op + " designacoesTipoEstadoDeConservacao:", this.estadoConservacao));
                str.Append(")");
            }

            #endregion

            #region Field defs.: Licencas de obra

            if (this.Nome_LicencaObraRequerentes.Length > 0) {
                if (str.Length > 0)
                    str.Append(" AND ");
                str.Append(string.Format("(requerente: ({0}))", this.Nome_LicencaObraRequerentes));
            }

            if (this.LocalizacaoObra_Actual.Length > 0) {
                if (str.Length > 0)
                    str.Append(" AND ");
                //str.Append(string.Format("(Termo_LicencaObraLocalizacaoObraActual: ({0}) OR NomeLocal_LicencaObraLocalizacaoObraAntiga: ({0}) )",
                str.Append(string.Format("(localAtual: ({0}))", this.LocalizacaoObra_Actual));
            }

            if (this.LocalizacaoObra_Antiga.Length > 0) {
                if (str.Length > 0)
                    str.Append(" AND ");
                str.Append(string.Format("(localAntigo: ({0}))", this.LocalizacaoObra_Antiga));
            }

            if (this.NumPolicia_Actual.Length > 0) {
                if (str.Length > 0)
                    str.Append(" AND ");
                //str.Append(string.Format("(NumPolicia_LicencaObraLocalizacaoObraActual: ({0}) OR NumPolicia_LicencaObraLocalizacaoObraAntiga: ({0}) )",
                str.Append(string.Format("(numPoliciaAtual: ({0}))", this.NumPolicia_Actual));
            }

            if (this.NumPolicia_Antigo.Length > 0) {
                if (str.Length > 0)
                    str.Append(" AND ");
                //str.Append(string.Format("(NumPolicia_LicencaObraLocalizacaoObraActual: ({0}) OR NumPolicia_LicencaObraLocalizacaoObraAntiga: ({0}) )",
                str.Append(string.Format("(numPoliciaAntigo: ({0}))", this.NumPolicia_Antigo));
            }

            if (this.LicencaObra_TipoObra.Length > 0) {
                if (str.Length > 0)
                    str.Append(" AND ");
                str.Append(string.Format("(tipoObra: ({0}))", this.LicencaObra_TipoObra));
            }

            if (this.Termo_LicencaObraTecnicoObra.Length > 0) {
                if (str.Length > 0)
                    str.Append(" AND ");
                str.Append(string.Format("(tecnico: ({0}))", this.Termo_LicencaObraTecnicoObra));
            }

            if (this.CodigosAtestadoHabitabilidade.Length > 0) {
                if (str.Length > 0)
                    str.Append(" AND ");
                str.Append(string.Format("(atestado: ({0}))", this.CodigosAtestadoHabitabilidade));
            }

            // Datas:
            if (this.Datas_LicencaObraDataLicencaConstrucao_Inicio.Trim().Length > 0) {
                str.Append(" AND ");
                if (this.Datas_LicencaObraDataLicencaConstrucao_Fim.Trim().Length > 0) {
                    str.AppendFormat("(Datas_LicencaObraDataLicencaConstrucao: [{0} TO {1}])",
                        this.Datas_LicencaObraDataLicencaConstrucao_Inicio, this.Datas_LicencaObraDataLicencaConstrucao_Fim);
                }
                else 
                    str.AppendFormat("(Datas_LicencaObraDataLicencaConstrucao: [{0} TO 99999999])", this.Datas_LicencaObraDataLicencaConstrucao_Inicio);
            }
            else if (this.Datas_LicencaObraDataLicencaConstrucao_Fim.Trim().Length > 0) {
                str.Append(" AND ");
                str.AppendFormat("(Datas_LicencaObraDataLicencaConstrucao: [00000000 TO {0}])", this.Datas_LicencaObraDataLicencaConstrucao_Fim);
            }


            if (this.licencaObra_PHSimNao) {
                if (str.Length > 0)
                    str.Append(" AND ");
                str.Append("(LicencaObra_PHSimNao: sim)");
            }


            #endregion

            return str.ToString();
		}

        public bool IsCriteriaEmpty()
        {
            return this.ToString().Equals("(existe: sim)");
        }
    }
}
