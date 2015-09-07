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
	public class PesquisaAvancada
	{
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
		
		string entidadeProdutora;
		
		public string EntidadeProdutora {
			get { return entidadeProdutora; }
			set { entidadeProdutora = value; }
		}		
		
		DateTime dataProducaoInicio = new DateTime(DateTime.MinValue.Ticks);
		
		public DateTime DataProducaoInicio {
			get { return dataProducaoInicio; }
			set { dataProducaoInicio = value; }
		}
		
		DateTime dataProducaoFim = new DateTime(DateTime.MaxValue.Ticks);
		
		public DateTime DataProducaoFim {
			get { return dataProducaoFim; }
			set { dataProducaoFim = value; }
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


		public PesquisaAvancada()
		{
			
		}
		
		public override string ToString(){

            StringBuilder str = new StringBuilder();

            if (this.modulo == 1)
            {
                str.Append("(publicar: sim)");
            }
            else
            {
                str.Append("(existe: sim)");
            }

            if (this.textoLivre.Trim().Length > 0)
            {
                if (str.Length > 0)
                {
                    str.Append(" AND ");
                }

                str.Append("(");
                str.Append(this.textoLivre);
                str.Append(")");
            }

			if(this.codigoParcial.Trim().Length > 0){
                if (str.Length > 0)
                {
                    str.Append(" AND ");
                }

                str.Append("(codigo:");
                str.Append(this.codigoParcial);
                str.Append(")");				
			}
			
			if(this.designacao.Length > 0){
                if(str.Length>0)
                {
                    str.Append(" AND ");
                }

                str.Append(string.Format("(titulo: ({0}))",this.designacao));			 	
			}
		
            
			if(this.entidadeProdutora.Length > 0){
                if(str.Length>0)
                {
                    str.Append(" AND ");
                }				

                str.Append(string.Format("(entidadeProdutora: ({0}))",this.entidadeProdutora));							
			}	      	
	      	
			
	      	if(!this.dataProducaoInicio.Equals(new DateTime(DateTime.MinValue.Ticks))  || !this.dataProducaoFim.Equals(new DateTime(DateTime.MaxValue.Ticks))){
                if (str.Length > 0)
                {
                    str.Append(" AND ");
                }				
				string inicioIntervalo  = this.dataProducaoInicio.ToString("yyyyMMdd");
                string fimIntervalo     = this.dataProducaoFim.ToString("yyyyMMdd");

                str.Append("(");
				str.Append("inicioProducao:[");
                str.Append(inicioIntervalo);
				str.Append(" TO ");
								
                str.Append(fimIntervalo);
				str.Append("]");

                str.Append(" AND ");

                str.Append("fimProducao:[");
                str.Append(inicioIntervalo);
                str.Append(" TO ");

                str.Append(fimIntervalo);
                str.Append("]");
                str.Append(")");
	      	}
			
			if(this.tipologiaInformacional.Length > 0){
				if(str.Length>0)
                {
                    str.Append(" AND ");
                }               
				
                str.Append(string.Format("(tipologiaInformacional: ({0}))",this.tipologiaInformacional));				
			}

			if(this.termosIndexacao.Length > 0){
                if(str.Length>0)
                {
                    str.Append(" AND ");
                }
								
                str.Append(string.Format("(assunto: ({0}))",this.termosIndexacao));				
			}
			
			if(this.conteudoInformacional.Length > 0){
                if (str.Length > 0)
                {
                    str.Append(" AND ");
                }

                str.Append(string.Format("(conteudo: ({0}))", this.conteudoInformacional));				
			}
			
			if(this.notas.Length > 0){
                if (str.Length > 0)
                {
                    str.Append(" AND ");
                }

                str.Append(string.Format("(notaGeral: ({0}))", this.notas));				                
			}
			
			if(this.cota.Length > 0){
                if (str.Length > 0)
                {
                    str.Append(" AND ");
                }

                str.Append(string.Format("(cota: ({0}))",this.cota));                
			}				                  
	      	
	      	if(this.suporteEAcondicionamento.Length > 0){
                if (str.Length > 0)
                {
                    str.Append(" AND ");
                }

	      		string op = "OR";
				if(this.suporteEAcondicionamentoOP == 0){
					op = "AND";
				}

                str.Append("(designacoesTipoFormaSuporteAcond:");
                str.Append(string.Join(" " + op + " designacoesTipoFormaSuporteAcond:", this.suporteEAcondicionamento));
                str.Append(")");												
	      	}
	      	
	      	if(this.materialDeSuporte.Length > 0){
                if (str.Length > 0)
                {
                    str.Append(" AND ");
                }

	      		string op = "OR";
				if(this.materialDeSuporteOP == 0){
					op = "AND";
				}

                str.Append("(designacoesTipoMaterialDeSuporte:");
                str.Append(string.Join(" " + op + " designacoesTipoMaterialDeSuporte:", this.materialDeSuporte));
                str.Append(")");												
	      	}

	      	if(this.tecnicaRegisto.Length > 0){
                if (str.Length > 0)
                {
                    str.Append(" AND ");
                }

	      		string op = "OR";
				if(this.tecnicaRegistoOP == 0){
					op = "AND";
				}

                str.Append("(designacoesTipoTecnicasDeRegisto:");
                str.Append(string.Join(" " + op + " designacoesTipoTecnicasDeRegisto:", this.tecnicaRegisto));
                str.Append(")");												
	      	}
	      	
	      	if(this.estadoConservacao.Length > 0){
                if (str.Length > 0)
                {
                    str.Append(" AND ");
                }

	      		string op = "OR";
				if(this.estadoConservacaoOP == 0){
					op = "AND";
				}

                str.Append("(designacoesTipoEstadoDeConservacao:");
                str.Append(string.Join(" " + op + " designacoesTipoEstadoDeConservacao:", this.estadoConservacao));
                str.Append(")");												
	      	}
	      	
			return str.ToString();
		}
	}
}
