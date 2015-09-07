using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace DBAbstractDataLayer.DataAccessRules {

    public abstract class EADGeneratorRule : DALRule {

        private static EADGeneratorRule current = null;
        public static void ClearCurrent() {
            current = null;
        }
        public static EADGeneratorRule Current {
            get {
                if (Object.ReferenceEquals(null, current)) {
                    current = (EADGeneratorRule)Create(typeof(EADGeneratorRule));
                }
                return current;
            }
        }

        public abstract string get_eadheader_audience(long IDNivel, IDbConnection conn);
        public abstract string get_eadid_countrycode(long IDNivel, IDbConnection conn);

        public abstract string get_CodigoTipoNivelRelacionado(long IDNivel_PAI, long IDNivel, IDbConnection conn);

        public abstract string get_NvlDesg_Designacao_Dict_Termo(long IDNivel, IDbConnection conn);
        public abstract string get_Author(long IDNivel, IDbConnection conn);

        public struct EAD_profiledesc {
            public string creation;
            public string date;
        }
        public abstract EAD_profiledesc get_profiledesc(long IDNivel, IDbConnection conn);

        public struct EAD_physdesc {
            public Dictionary<string, long> extent;
            public string dimension;
            public string unit;
            public string notes;
        }

        public abstract EAD_physdesc get_physdesc(long IDNivel, IDbConnection conn);

        public abstract string get_EntidadeDetentoraForNivel(long IDNivel, IDbConnection conn);

        public abstract string get_DatasDeProducao(long IDNivel, IDbConnection conn);
        public abstract string get_Extremos_DatasDeProducao_UnidadeFisica(long IDNivel, IDbConnection conn);

        public abstract string get_physdescCota(long IDNivel, IDbConnection conn);

        public struct Origination_EntProdutora_Tipo {
            public string termo;
            public long IDTipoEntidadeProdutora;
        }
        public abstract List<Origination_EntProdutora_Tipo> get_EntidadesProdutoras_Origination(long IDNivel, IDbConnection conn);
        public abstract List<Origination_EntProdutora_Tipo> get_Autores_Origination(long IDNivel, IDbConnection conn);

        public struct ArchDesc_Acqinfo_Custodhist {
            public string acqinfo;
            public string custodhist;
        }
        public abstract ArchDesc_Acqinfo_Custodhist get_ArchDesc_Acqinfo_Custodhist(long IDNivel, IDbConnection conn);

        public abstract string get_bioghist(long IDNivel, IDbConnection conn);

        public struct ScopeContent {
            public string dict_Termo;
            public int IndexFRDCA_Selector;
            public long IDTipoNoticiaAut;
            public string ConteudoInformacional;
        }
        public abstract List<ScopeContent> get_scopecontent(long IDNivel, IDbConnection conn);

        public struct Termo_Outras_Formas {
            public string Termo;
            public string Outras_Formas;
        }
        public struct ScopeContent_PROCESSO_DE_OBRAS {
            public List<ScopeContent> scopeContent;
            public List<string> requerentes;
            public List<string> averbamentos;
            public List<string> loc_actual;
            public List<Termo_Outras_Formas> loc_actual_OutrasFormas;
            public List<string> loc_antiga;
            public string tipo_obra;
            public string strPH;
            public List<string> tecnicoObra;
            public List<Termo_Outras_Formas> tecnicoObra_OutrasFormas;
            public List<string> atestado;
            public List<string> datasLicenca;
        }
        public abstract bool isProcessoDeObras(long IDNivel, IDbConnection conn);
        public abstract ScopeContent_PROCESSO_DE_OBRAS get_scopecontent_PROCESSO_DE_OBRAS(long IDNivel, IDbConnection conn);

        public struct Appraisal_InfoRelacionada {
            public string densidade_InfoRel_Titulo;
            public string densidade_InfoRel_Tipo;
            public string densidade_InfoRel_Grau;
            public string densidade_InfoRel_Ponderacao;
        }

        public struct Appraisal {
            public string pertinencia_Nivel;
            public string pertinencia_Ponderacao;
            public string pertinencia_FreqUso;
            
            public string densidade_Tipo;
            public string densidade_Grau;

            public List<Appraisal_InfoRelacionada> densidade_InfoRel;

            public string enqdr_Legal_Diploma;
            public string enqdr_Legal_RefTblSeleccao;

            public string destino_Final;
            public string prazo_Conservacao;
            public string auto_eliminacao;
            public string observacoes;
            public bool isEmpty() { 
                return densidade_InfoRel.Count == 0 &&
                    pertinencia_Nivel.Equals(string.Empty) &&
                    pertinencia_Ponderacao.Equals(string.Empty) &&
                    pertinencia_FreqUso.Equals(string.Empty) &&

                    densidade_Tipo.Equals(string.Empty) &&
                    densidade_Grau.Equals(string.Empty) &&
                    
                    enqdr_Legal_Diploma.Equals(string.Empty) &&
                    enqdr_Legal_RefTblSeleccao.Equals(string.Empty) &&

                    destino_Final.Equals(string.Empty) &&
                    prazo_Conservacao.Equals(string.Empty) &&
                    auto_eliminacao.Equals(string.Empty) &&
                    observacoes.Equals(string.Empty) ;
            }
        }
        public abstract Appraisal get_Appraisal(long IDNivel, IDbConnection conn);

        public abstract string get_Accruals(long IDNivel, IDbConnection conn);

        public struct Arrangement {
            public List<string> tradicaoDocumental;
            public List<string> ordenacao;
            public bool isEmpty() { return (tradicaoDocumental.Count == 0 && ordenacao.Count == 0); }
        }
        public abstract Arrangement get_Arrangement(long IDNivel, IDbConnection conn);

        public struct CondicoesDeAcesso {
            public string CondicaoDeAcesso;
            public string CondicaoDeReproducao;
            public string AuxiliarDePesquisa;
        }
        public abstract CondicoesDeAcesso get_CondicoesDeAcesso(long IDNivel, IDbConnection conn);

        public struct LangMaterial {
            public string BibliographicCodeAlpha3;
            public string LanguageNameEnglish;
            public bool isEmpty() { return (BibliographicCodeAlpha3.Equals(string.Empty) && LanguageNameEnglish.Equals(string.Empty)); }
        }
        public abstract List<LangMaterial> get_LangMaterial(long IDNivel, IDbConnection conn);

        public struct PhysTech {
            public List<string> suporte_acondicionamento;
            public List<string> material_suporte;
            public List<string> tecnicas_registo;
            public List<string> estado_conservacao;
            public bool isEmpty() { return (suporte_acondicionamento.Count==0 && material_suporte.Count==0 && tecnicas_registo.Count==0 && estado_conservacao.Count==0); }
        }
        public abstract PhysTech get_PhysTech(long IDNivel, IDbConnection conn);

        public struct DocumentacaoAssociada {
            public string originalsloc;
            public string altformavail;
            public string relatedmaterial;
            public string bibliography;
            public bool isEmpty() { return (originalsloc.Equals(string.Empty) && altformavail.Equals(string.Empty) && relatedmaterial.Equals(string.Empty) && bibliography.Equals(string.Empty)); }
        }
        public abstract DocumentacaoAssociada get_DocumentacaoAssociada(long IDNivel, IDbConnection conn);

        public abstract string get_NotaGeral(long IDNivel, IDbConnection conn);

        public struct Processinfo_Date {
            public string data_descricao;
            public string data_registo;
            public string operador;
            public string autoridade;
        }
        public struct Processinfo {
            public string nota_arquivista;
            public List<Processinfo_Date> datas_autores;
            public bool isEmpty() {
                return ((nota_arquivista == null || nota_arquivista.Equals(string.Empty)) &&
                    datas_autores.Count == 0);
            }
        }
        public abstract Processinfo get_Processinfo(long IDNivel, IDbConnection conn);

        public abstract string get_descrules(long IDNivel, IDbConnection conn);

        public struct NiveisDescendentes {
            public long IDNivelPai;
            public long IDNivel;
            public long TipoNivel;
            public string Termo_Estrutural_Filho;
            public long geracao;

            public override bool Equals(Object obj) {
                bool eq = obj is NiveisDescendentes && this.IDNivel == ((NiveisDescendentes)obj).IDNivel;
                return eq;
            }
            public override int GetHashCode() {
                return (IDNivelPai.ToString() + IDNivel.ToString()).GetHashCode();
            }
        }
        public abstract List<NiveisDescendentes> get_All_NiveisDescendentes(long IDNivelTopo, long IDTrustee, IDbConnection conn);
        public abstract int get_Count_All_NiveisDescendentes(long IDNivelTopo, long IDTrustee, IDbConnection conn);
        public abstract List<NiveisDescendentes> get_NiveisDescendentes(long IDNivel, long IDTrustee, IDbConnection conn);

        public abstract string get_dao_href(long IDNivel, IDbConnection conn);
    }
}
