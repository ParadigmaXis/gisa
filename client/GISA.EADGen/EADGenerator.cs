using System;

using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

using System.Xml;

using DBAbstractDataLayer.DataAccessRules;

using GISA.Model;


namespace GISA.EADGen {

    // Para a pirotecnia da progressBar:
    public delegate void AddedEntriesEventHandler(int Count);
    public delegate void RemovedEntriesEventHandler(int Count);

    public class EADGenerator {

        private const string GISAID_PREFIX = "GISA_IDNVL_";

        private XmlTextWriter xw;
        private IDbConnection conn;
        private string fileName = "";

        private string titlestmt_titleproper = string.Empty;

        private EADGenerator_Cache tree_todos_descendentes;

        private HashSet<EADGeneratorRule.NiveisDescendentes> explorados = new HashSet<EADGeneratorRule.NiveisDescendentes>();

        public event AddedEntriesEventHandler AddedEntries;
        public event RemovedEntriesEventHandler RemovedEntries;
        protected void DoAddedEntries(int Count) {
            if (AddedEntries != null) {
                Trace.WriteLine("DoAddedEntries");
                AddedEntries(Count);
            }
        }
        protected void DoRemovedEntries(int Count) {
            if (RemovedEntries != null) {
                RemovedEntries(Count);
            }
        }

        public EADGenerator(string pathFileName, IDbConnection connection) {
            this.fileName = pathFileName;
            this.conn = connection;
            this.xw = new XmlTextWriter(this.fileName, System.Text.Encoding.UTF8);
            
            this.xw.WriteStartDocument();
            this.set_formatting_opts();
        }

        private void set_formatting_opts() {
            this.xw.Formatting = Formatting.Indented;
            this.xw.Indentation = 1;
            this.xw.IndentChar = '\t';
        }

        public bool generate(long IDNivel_PAI, long IDNivel) {
            this.tree_todos_descendentes = new EADGenerator_Cache(IDNivel, this.conn);

            bool ret = false;

            // <!DOCTYPE EAD PUBLIC "+//ISBN 1-931666-00-8//DTD ead.dtd (Encoded Archival Description (EAD) Version 2002)//EN">
            //this.xw.WriteDocType("ead", "+//ISBN 1-931666-00-8//DTD ead.dtd (Encoded Archival Description (EAD) Version 2002)//EN", null, null);
            this.xw.WriteDocType("ead", null, "ead.dtd", null);

            gen_open_EAD();

            gen_eadheader(IDNivel);

            gen_ARCHDESC(IDNivel_PAI, IDNivel);

            gen_close_EAD();

            // flush & close:
            xw.Flush();
            xw.Close();

            return ret;
        }

        #region <ead>
        /*
         * <!ELEMENT ead
         *      (eadheader, frontmatter?, archdesc)
         */
        private void gen_open_EAD() { xw.WriteStartElement("ead"); }
        private void gen_close_EAD() { xw.WriteEndElement(); }
        #endregion

        #region <eadheader>

        /*
         * <!ELEMENT eadheader
         *  (eadid, filedesc, profiledesc?, revisiondesc?)
         */
        public void gen_eadheader(long IDNivel) {
            // <eadheader ...>
            xw.WriteStartElement("eadheader");
            xw.WriteAttributeString("countryencoding", "iso3166-1");
            xw.WriteAttributeString("repositoryencoding", "iso15511");
            xw.WriteAttributeString("langencoding", "iso639-2b");

            // Attrib: audience:
            string audience = EADGeneratorRule.Current.get_eadheader_audience(IDNivel, this.conn);
            xw.WriteAttributeString("audience", audience);

            // Codigo de referencia (para cada)
            gen_EADID(IDNivel);

            // File description: informacao bibliografica:
            gen_FILEDESC(IDNivel);

            // profiledesc:
            gen_PROFILEDESC(IDNivel);

            // </eadheader>
            xw.WriteEndElement();
        }

        private void gen_EADID(long IDNivel) {
            // <eadid>
            xw.WriteStartElement("eadid");

            // Attrib: countrycode
            string countrycode = EADGeneratorRule.Current.get_eadid_countrycode(IDNivel, this.conn);
            xw.WriteAttributeString("countrycode", countrycode);

            string codigoCompletoNivel = NivelRule.Current.GetCodigoCompletoNivel(IDNivel, this.conn);

            // Attrib: mainagencycode devera´ ser o codigo da entidade detentora;
            // usar a string até ao primeiro '/' em codigoCompletoNivel:
            string mainagencycode = "_";
            string[] agencyCodeArray = codigoCompletoNivel.Split('/');
            if (agencyCodeArray.Length > 0)
                mainagencycode = agencyCodeArray[0];
            xw.WriteAttributeString("mainagencycode", mainagencycode);
            
            // Identificador: codigo de referencia do painel 1.1
            xw.WriteString(codigoCompletoNivel);

            // </eadid>
            xw.WriteEndElement();
        }


        /*
         * <!ELEMENT filedesc
         *   (titlestmt, editionstmt?, publicationstmt?, seriesstmt?, notestmt?)
         */
        private void gen_FILEDESC(long IDNivel) {
            // <filedesc>
            xw.WriteStartElement("filedesc");

            // <!ELEMENT titlestmt
            //      (titleproper+, subtitle*, author?, sponsor?)

            // <titlestmt>:
            xw.WriteStartElement("titlestmt");

            // <titleproper>
            xw.WriteStartElement("titleproper");
            this.titlestmt_titleproper = EADGeneratorRule.Current.get_NvlDesg_Designacao_Dict_Termo(IDNivel, this.conn);
            xw.WriteString(this.titlestmt_titleproper);
            // </titleproper>
            xw.WriteEndElement();

            // <subtitle> ?

            // <author>
            string author = EADGeneratorRule.Current.get_Author(IDNivel, this.conn);
            if (!author.Equals(string.Empty)) {
                xw.WriteStartElement("author");
                xw.WriteString(author);
                // </author>
                xw.WriteEndElement();
            }

            // <titlestmt>:
            xw.WriteEndElement();

            // </filedesc>
            xw.WriteEndElement();
        }

        /*
         * <!ELEMENT profiledesc
         *  (creation?, langusage?, descrules?)
         *  Se os dados obtidos da BD forem a null, nao escrever este elemento
         */
        private void gen_PROFILEDESC(long IDNivel) {
            //EADGeneratorRule.EAD_profiledesc profiledesc = EADGeneratorRule.Current.get_profiledesc(IDNivel, this.conn);
            string creation_str = "Descrição EAD exportada automaticamente via GISA - Gestão Integrada de Sistemas de Arquivo. Sistema desenvolvido pela ParadigmaXis.";

            // <profiledesc>
            xw.WriteStartElement("profiledesc");
            // <creation>
            xw.WriteStartElement("creation");
            xw.WriteString(creation_str);
                // <date>
                xw.WriteStartElement("date");
                xw.WriteString(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss").Replace("-", "/"));
                xw.WriteEndElement();
            // </creation>
            xw.WriteEndElement();

            // <descrules>:
            string descrules = EADGeneratorRule.Current.get_descrules(IDNivel, this.conn);
            if (!descrules.Equals(string.Empty)) {
                // <descrules>
                xw.WriteStartElement("descrules");
                xw.WriteString(descrules);
                // </descrules>
                xw.WriteEndElement();
            }

            // </profiledesc>
            xw.WriteEndElement();
        }

        #endregion


        #region <archdesc>
        /*
         * <!ELEMENT archdesc
         *   (runner*, did, (%m.desc.full;)*)
         *   
         * May contain:
         * USADOS (dentro do <c>):
         * did;
         * acqinfo;
         * custodhist;
         * bioghist;
         * scopecontent;
         * appraisal;
         * accruals;
         * arrangement;
         * accessrestrict;
         * userestrict;
         * otherfindaid;
         * processinfo;
         * 
         * 
         * NAO USADOS:
         * , , , altformavail, , , bibliography, 
         * , controlaccess, , dao, daogrp, descgrp, , dsc, fileplan, index, 
         * note, odd, originalsloc, , phystech, prefercite, , relatedmaterial, 
         * runner, , separatedmaterial, 
         */
        private void gen_ARCHDESC(long IDNivel_PAI, long IDNivel) {
            // <archdesc>
            xw.WriteStartElement("archdesc");
            // Attrib. 'level'
            gen_attrib_LEVEL(IDNivel_PAI, IDNivel);

            // <did>
            gen_DID(IDNivel);

            // <acqinfo> & <custodhist>:
            gen_acqinfo_custodhist(IDNivel);

            // <bioghist>:
            gen_bioghist_ARCHDESC(IDNivel);

            // <scopecontent>
            gen_scopecontent_index(IDNivel);

            // <appraisal>
            gen_appraisal(IDNivel);

            // <accruals>
            gen_accruals(IDNivel);

            // <arrangement>
            gen_arrangement(IDNivel);

            // <accessrestrict>, <userestrict>, <otherfindaid>
            gen_accessrestrict_userestrict_otherfindaid(IDNivel);

            // <phystech>
            gen_phystech(IDNivel);

            // <originalsloc>, <altformavail>, <relatedmaterial>, <bibliography>
            gen_DocumentacaoAssociada(IDNivel);

            // <note>
            gen_note(IDNivel);

            // <processinfo>
            gen_processinfo(IDNivel);

            // <dsc> com <c> se existirem niveis descendentes deste:
            List<EADGeneratorRule.NiveisDescendentes> IDNiveisDescs = 
                this.tree_todos_descendentes.get_NiveisDescendentes(IDNivel);

            if (IDNiveisDescs.Count > 0)
                gen_list_dsc_c(IDNivel_PAI, IDNiveisDescs);

            // </archdesc>
            xw.WriteEndElement();
        }

        private void gen_attrib_LEVEL(long IDNivel_PAI, long IDNivel) {
            string codigo = EADGeneratorRule.Current.get_CodigoTipoNivelRelacionado(IDNivel_PAI, IDNivel, this.conn);
            string level = "otherlevel";

            switch (codigo) {
                case "A":
                    level = "fonds";
                    break;
                case "SA":
                    level = "subfonds";
                    break;

                case "SEC":
                case "SSEC":
                    level = "recordgrp";
                    break;

                case "SR":
                    level = "series";
                    break;
                case "SSR":
                    level = "subseries";
                    break;

                case "SD":
                    level = "item";
                    break;
                case "D":
                    int childCount = NivelRule.Current.getDirectChildCount(IDNivel.ToString(), string.Empty, this.conn);
                    if (childCount == 0)
                        level = "item";
                    else
                        level = "file";
                    break;

                default:
                    level = "otherlevel";
                    break;
            }
            xw.WriteAttributeString("level", level);
        }

        #region <dsc> (dentro do <archdesc>)
        /*
         * <!ELEMENT dsc
         * ((head?, (%m.blocks;)*),
         * (((thead?, ((c, thead?)+ | (c01, thead?)+)) | dsc*)))
         */
        private void gen_list_dsc_c(long IDNivel_PAI, List<EADGeneratorRule.NiveisDescendentes> NiveisDesc) {
            // NOTA: pode gerar um <dsc> vazio:
            // <dsc>
            xw.WriteStartElement("dsc");
            xw.WriteAttributeString("type", "in-depth");
            
            foreach (EADGeneratorRule.NiveisDescendentes Nivel in NiveisDesc) {
                if (this.explorados.Add(Nivel))
                    // <c> 
                    gen_C(IDNivel_PAI, Nivel);
                // else, ja foi explorado
                else {
                    Trace.WriteLine("EAD::gen_list_dsc_c(): o IDNivel " + Nivel.IDNivel + " ja foi explorado");
                }
            }
            
            // </dsc>
            xw.WriteEndElement();
        }

        #endregion

        #region <acqinfo> e <custodhist>
        private void gen_acqinfo_custodhist(long IDNivel) {
            // <acqinfo>:
            EADGeneratorRule.ArchDesc_Acqinfo_Custodhist archDesc_Acqinfo_Custodhist = EADGeneratorRule.Current.get_ArchDesc_Acqinfo_Custodhist(IDNivel, this.conn);
            if (!archDesc_Acqinfo_Custodhist.acqinfo.Equals(string.Empty)) {
                xw.WriteStartElement("acqinfo");
                xw.WriteStartElement("p");
                xw.WriteString(archDesc_Acqinfo_Custodhist.acqinfo);
                xw.WriteEndElement();
                //  </acqinfo>
                xw.WriteEndElement();
            }
            // <custodhist>:
            if (!archDesc_Acqinfo_Custodhist.custodhist.Equals(string.Empty)) {
                xw.WriteStartElement("custodhist");
                xw.WriteStartElement("p");
                xw.WriteString(archDesc_Acqinfo_Custodhist.custodhist);
                xw.WriteEndElement();
                // </custodhist>
                xw.WriteEndElement();
            }
        }

        #endregion

        #region <bioghist>

        private void gen_bioghist_ARCHDESC(long IDNivel) {
            string bioghist = EADGeneratorRule.Current.get_bioghist(IDNivel, this.conn);
            // TODO
        }

        private void gen_bioghist(long IDNivel) {
            // <bioghist>:
            string bioghist = EADGeneratorRule.Current.get_bioghist(IDNivel, this.conn);
            if (!bioghist.Equals(string.Empty)) {
                xw.WriteStartElement("bioghist");
                string[] bioghist_prgs = bioghist.Split(new char[] { '\n', '\r' });
                generate_P_elems(bioghist_prgs, xw);
                // </bioghist>:
                xw.WriteEndElement();
            }
        }

        #endregion

        #region <C>
        /*
         * <!ELEMENT c
         * (head?, did, (%m.desc.full;)*, (thead?, c+)*)
         * 
         * May contain:
         * 
         * USADOS:
         * did;
         * acqinfo;
         * custodhist;
         * scopecontent;
         * appraisal;
         * accruals;
         * arrangement;
         * 
         * accessrestrict;
         * userestrict;
         * otherfindaid;
         * phystech;
         * 
         * originalsloc;
         * altformavail;
         * relatedmaterial;
         * bibliography;
         * 
         * note;
         * processinfo;
         * 
         * NAO USADOS:
         * 
         * , , , , , , 
         * , bioghist, c, controlaccess, , dao, daogrp, descgrp, , 
         * dsc, fileplan, head, index, , odd, , , , 
         * prefercite, , , , separatedmaterial, 
         * thead, 
         */
        private void gen_C(long IDNivel_PAI, EADGeneratorRule.NiveisDescendentes NivelActual) {
            // Pirotecnia:
            DoAddedEntries(1);
            //DoRemovedEntries(1);

            // Gerar o <c> somente se estivermos perante um nivel de tipo 3 (DOCUMENTAL):
            //if (NivelActual.TipoNivel == 3) {
            // <c>
            xw.WriteStartElement("c");
            // Attrib. 'level'
            gen_attrib_LEVEL(IDNivel_PAI, NivelActual.IDNivel);

            // <did>
            gen_DID(NivelActual.IDNivel);

            // <acqinfo> & <custodhist>:
            gen_acqinfo_custodhist(NivelActual.IDNivel);

            // <bioghist>:
            gen_bioghist(NivelActual.IDNivel);

            // <scopecontent>
            gen_scopecontent_index(NivelActual.IDNivel);

            // <appraisal>
            gen_appraisal(NivelActual.IDNivel);

            // <accruals>
            gen_accruals(NivelActual.IDNivel);

            // <arrangement>
            gen_arrangement(NivelActual.IDNivel);

            // <accessrestrict>, <userestrict>, <otherfindaid>
            gen_accessrestrict_userestrict_otherfindaid(NivelActual.IDNivel);

            // <phystech>
            gen_phystech(NivelActual.IDNivel);

            // <originalsloc>, <altformavail>, <relatedmaterial>, <bibliography>
            gen_DocumentacaoAssociada(NivelActual.IDNivel);

            // <note>
            gen_note(NivelActual.IDNivel);

            // <processinfo>
            gen_processinfo(NivelActual.IDNivel);

            // Procurar os niveis descendentes deste e gerar o elemento <c> recursivamente:
            //List<EADGeneratorRule.NiveisDescendentes> NiveisDescs = EADGeneratorRule.Current.get_NiveisDescendentes(NivelActual.IDNivel, this.conn);
            List<EADGeneratorRule.NiveisDescendentes> NiveisDescs =
                this.tree_todos_descendentes.get_NiveisDescendentes(NivelActual.IDNivel);
                
            foreach (EADGeneratorRule.NiveisDescendentes newNivel in NiveisDescs) {
                if (this.explorados.Add(newNivel))
                    gen_C(NivelActual.IDNivel, newNivel);
                else {
                    gen_reference(NivelActual.IDNivel, newNivel.IDNivel);
                    Trace.WriteLine("EAD::gen_C() (dentro de um nivel DOCUMENTAL): o IDNivel " + newNivel.IDNivel + " ja foi explorado");
                }
            }
            // </c>
            xw.WriteEndElement();
            //}

            //else {
            //    // Procurar os niveis descendentes deste e gerar o elemento <c> recursivamente:
            //    List<EADGeneratorRule.NiveisDescendentes> NiveisDescs = EADGeneratorRule.Current.get_NiveisDescendentes(NivelActual.IDNivel, this.conn);
            //    foreach (EADGeneratorRule.NiveisDescendentes newNivel in NiveisDescs) {
            //        if (this.explorados.Add(newNivel))
            //            gen_C(newNivel);
            //        else {
            //            Trace.WriteLine("EAD::gen_C() (NAO DOCUMENTAL): o IDNivel " + newNivel.IDNivel + " ja foi explorado");
            //        }
            //    }
            //}
        }

        private void gen_reference(long IDNivel_PAI, long IDNivel) {
            // <c>
            xw.WriteStartElement("c");
            // Attrib. 'level':
            gen_attrib_LEVEL(IDNivel_PAI, IDNivel);

            // <did>
            xw.WriteStartElement("did");
            // <unitid>
            xw.WriteStartElement("unitid");
            // <archref>
            xw.WriteStartElement("archref");

            // <ref linktype="simple" target="GISA_IDNVL_1234567890" show="embed" actuate="onload">
            xw.WriteStartElement("ref");
            xw.WriteAttributeString("linktype", "simple");
            xw.WriteAttributeString("target", GISAID_PREFIX + IDNivel.ToString());
            xw.WriteAttributeString("show", "embed");
            xw.WriteAttributeString("actuate", "onload");

            // </ref>
            xw.WriteEndElement();

            // </archref>
            xw.WriteEndElement();
            // </unitid>
            xw.WriteEndElement();
            // <did>
            xw.WriteEndElement();
            // </c>
            xw.WriteEndElement();
        }

        #endregion

        #endregion

        #region <scopecontent> (dentro do <archdesc> & <c>)

        private void gen_scopecontent_index(long IDNivel) {
            if (EADGeneratorRule.Current.isProcessoDeObras(IDNivel, this.conn))
                gen_scopecontent_PROCESSO_OBRAS(IDNivel);
            else {
                List<EADGeneratorRule.ScopeContent> list_scope = EADGeneratorRule.Current.get_scopecontent(IDNivel, this.conn);
                gen_scopecontent_CONTEUDO_INFORM(list_scope, true);
                gen_index(list_scope);
            }
        }

        private void gen_scopecontent_CONTEUDO_INFORM(List<EADGeneratorRule.ScopeContent> list_scope, bool gen_xml_scopecontent_elem) {
            if (list_scope.Count > 0) {
                StringBuilder conteudoInformacional = new StringBuilder().Append("Conteúdo informacional: ");
                int _i_conteudoInformacional = 0;
                StringBuilder tipoInformacional = new StringBuilder().Append("Tipologia informacional: ");
                int _i_tipoInformacional = 0;
                StringBuilder Sub_tipoInformacional = new StringBuilder().Append("Subtipologia informacional: ");
                int _i_Sub_tipoInformacional = 0;
                StringBuilder diplomaLegal = new StringBuilder().Append("Diploma legal: ");
                int _i_diplomaLegal = 0;
                StringBuilder modelo = new StringBuilder().Append("Modelo: ");
                int _i_modelo = 0;

                foreach (EADGeneratorRule.ScopeContent scope in list_scope) {
                    if (!scope.ConteudoInformacional.Equals(string.Empty)) {
                        conteudoInformacional.Append(_i_conteudoInformacional++ == 0 ? "" : "; ").Append(scope.ConteudoInformacional);
                    }
                    switch (scope.IDTipoNoticiaAut) {
                        case (int)TipoNoticiaAut.TipologiaInformacional:
                            if (scope.IndexFRDCA_Selector > 0)  // sub-tipologia
                                Sub_tipoInformacional.Append(_i_Sub_tipoInformacional++ == 0 ? "" : "; ").Append(scope.dict_Termo);
                            else
                                tipoInformacional.Append(_i_tipoInformacional++ == 0 ? "" : "; ").Append(scope.dict_Termo);
                            break;

                        case (int)TipoNoticiaAut.Diploma:
                            if (scope.IndexFRDCA_Selector < 0)
                                diplomaLegal.Append(_i_diplomaLegal++ == 0 ? "" : "; ").Append(scope.dict_Termo);
                            break;

                        case (int)TipoNoticiaAut.Modelo:
                            modelo.Append(_i_modelo++ == 0 ? "" : "; ").Append(scope.dict_Termo);
                            break;

                        default:
                            break;
                    }
                }

                // Escrever os varios textos:
                if (_i_conteudoInformacional > 0 || _i_tipoInformacional > 0 || _i_diplomaLegal > 0 || _i_modelo > 0 ) {
                    // <scopecontent>
                    if (gen_xml_scopecontent_elem)
                        xw.WriteStartElement("scopecontent");

                    if (_i_conteudoInformacional > 0) {
                        xw.WriteStartElement("p");
                        xw.WriteString(conteudoInformacional.ToString());
                        xw.WriteEndElement();
                    }
                    if (_i_tipoInformacional > 0) {
                        xw.WriteStartElement("p");
                        xw.WriteString(tipoInformacional.ToString());
                        xw.WriteEndElement();
                    }
                    if (_i_Sub_tipoInformacional > 0) {
                        xw.WriteStartElement("p");
                        xw.WriteString(Sub_tipoInformacional.ToString());
                        xw.WriteEndElement();
                    }

                    if (_i_diplomaLegal > 0) {
                        xw.WriteStartElement("p");
                        xw.WriteString(diplomaLegal.ToString());
                        xw.WriteEndElement();
                    }
                    if (_i_modelo > 0) {
                        xw.WriteStartElement("p");
                        xw.WriteString(modelo.ToString());
                        xw.WriteEndElement();
                    }
                    if (gen_xml_scopecontent_elem)
                        xw.WriteEndElement();
                }
            }
        }

        /*
         * So para elementos de indexacao do tipo TipoNoticiaAut.Ideografico, TipoNoticiaAut.Onomastico, TipoNoticiaAut.ToponimicoGeografico
         */
        private void gen_index(List<EADGeneratorRule.ScopeContent> list_scope) {
            if (exists_indexEntry(list_scope)) {
                // <index>
                xw.WriteStartElement("index");
                foreach (EADGeneratorRule.ScopeContent scope in list_scope) {
                    switch (scope.IDTipoNoticiaAut) {
                        case (int)TipoNoticiaAut.Ideografico:       // subject
                            xw.WriteStartElement("indexentry");
                            xw.WriteStartElement("subject");
                            xw.WriteString(scope.dict_Termo);
                            xw.WriteEndElement();
                            xw.WriteEndElement();
                            break;

                        case (int)TipoNoticiaAut.Onomastico:        // name
                            xw.WriteStartElement("indexentry");
                            xw.WriteStartElement("name");
                            xw.WriteString(scope.dict_Termo);
                            xw.WriteEndElement();
                            xw.WriteEndElement();
                            break;

                        case (int)TipoNoticiaAut.ToponimicoGeografico:  // geogname
                            xw.WriteStartElement("indexentry");
                            xw.WriteStartElement("geogname");
                            xw.WriteString(scope.dict_Termo);
                            xw.WriteEndElement();
                            xw.WriteEndElement();
                            break;
                    }
                }
                // </index>
                xw.WriteEndElement();
            }
        }

        private bool exists_indexEntry(List<EADGeneratorRule.ScopeContent> list_scope) {
            foreach (EADGeneratorRule.ScopeContent scope in list_scope)
                if (scope.IDTipoNoticiaAut == (int)TipoNoticiaAut.Ideografico ||
                    scope.IDTipoNoticiaAut == (int)TipoNoticiaAut.Onomastico ||
                    scope.IDTipoNoticiaAut == (int)TipoNoticiaAut.ToponimicoGeografico)
                    return true;

            return false;
        }

        /*
         * Assume que o IDNivel e´ realmente um processo de obras
         */
        private void gen_scopecontent_PROCESSO_OBRAS(long IDNivel) {
            EADGeneratorRule.ScopeContent_PROCESSO_DE_OBRAS scpCon_PROC_OBRAS = EADGeneratorRule.Current.get_scopecontent_PROCESSO_DE_OBRAS(IDNivel, this.conn);

            // <scopecontent>
            xw.WriteStartElement("scopecontent");

            gen_scopecontent_CONTEUDO_INFORM(scpCon_PROC_OBRAS.scopeContent, false);

            if (scpCon_PROC_OBRAS.requerentes.Count > 0)
                gen_P_header_and_list("Requerentes/proprietários (iniciais)", scpCon_PROC_OBRAS.requerentes);

            if (scpCon_PROC_OBRAS.averbamentos.Count > 0)
                gen_P_header_and_list("Requerentes/proprietários (averbamento)", scpCon_PROC_OBRAS.averbamentos);
            
            if (scpCon_PROC_OBRAS.loc_actual.Count > 0)
                gen_P_header_and_list("Localização da obra (designação actual)", scpCon_PROC_OBRAS.loc_actual);

            if (scpCon_PROC_OBRAS.loc_antiga.Count > 0)
                gen_P_header_and_list("Localização da obra (designação antiga)", scpCon_PROC_OBRAS.loc_antiga);

            if (!scpCon_PROC_OBRAS.tipo_obra.Equals(string.Empty))
                gen_P_header_and_list("Tipo de obra", new List<string>{scpCon_PROC_OBRAS.tipo_obra});

            if (!scpCon_PROC_OBRAS.strPH.Equals(string.Empty))
                gen_P_header_and_list("Propriedade horizontal", new List<string>{scpCon_PROC_OBRAS.strPH});

            if (scpCon_PROC_OBRAS.tecnicoObra.Count > 0)
                gen_P_header_and_list("Técnico de obra", scpCon_PROC_OBRAS.tecnicoObra);

            if (scpCon_PROC_OBRAS.atestado.Count > 0)
                gen_P_header_and_list("Atestado de habitabilidade", scpCon_PROC_OBRAS.atestado);

            if (scpCon_PROC_OBRAS.datasLicenca.Count > 0)
                gen_P_header_and_list("Data da licença de construção", scpCon_PROC_OBRAS.datasLicenca);

            // </scopecontent>
            xw.WriteEndElement();

            gen_index(scpCon_PROC_OBRAS.scopeContent);

        }


        #endregion

        #region <did> (dentro do <archdesc> & <c>)

        /* Descriptive Identification:
         * <!ELEMENT did (head?, (%m.did;)+) >
         * <!ATTLIST did
         *      %a.common;
         *      %am.did.encodinganalog;
         *      
         * May contain:
         * unitdate;
         * unittitle;
         * physdesc (com <date>);
         * physloc;
         * repository;
         * origination;
         * dao;
         * 
         * NAO USADOS:
         * abstract, container, , daogrp, head, langmaterial, materialspec, note,  
         * unitid, 
         */
        private void gen_DID(long IDNivel) {
            // <did>
            xw.WriteStartElement("did");

            // ID:
            xw.WriteAttributeString("id", GISAID_PREFIX + IDNivel.ToString());

            // <unitdate>
            string unitdate = EADGeneratorRule.Current.get_DatasDeProducao(IDNivel, this.conn);
            if (!unitdate.Equals(string.Empty)) {
                xw.WriteStartElement("unitdate");
                xw.WriteAttributeString("type", "inclusive");
                xw.WriteString(unitdate);
                // </unitdate>
                xw.WriteEndElement();
            }

            // <unittitle>
            string unittitle = EADGeneratorRule.Current.get_NvlDesg_Designacao_Dict_Termo(IDNivel, this.conn);
            xw.WriteStartElement("unittitle");
            xw.WriteString(unittitle);
            // </unittitle>
            xw.WriteEndElement();

            // <physdesc> (com <date>)
            EADGeneratorRule.EAD_physdesc the_physdesc = EADGeneratorRule.Current.get_physdesc(IDNivel, this.conn);
            string the_date = EADGeneratorRule.Current.get_Extremos_DatasDeProducao_UnidadeFisica(IDNivel, this.conn).Trim();

            if (!the_physdesc.dimension.Equals(String.Empty) || 
                !the_date.Equals(String.Empty)) {

                // <physdesc>
                xw.WriteStartElement("physdesc");

                the_physdesc.extent.Keys.ToList().ForEach(k =>
                    {
                        xw.WriteStartElement("extent");
                        var quant = the_physdesc.extent[k];
                        var extent = string.Format("{0} {1}{2}", quant, k, quant > 1 ? "(s)" : "");
                        xw.WriteString(extent);
                        xw.WriteEndElement();
                    });
                

                // <dimensions>
                if (!the_physdesc.dimension.Equals(String.Empty)) {
                    xw.WriteStartElement("dimensions");
                    xw.WriteAttributeString("UNIT", the_physdesc.unit);
                    xw.WriteAttributeString("TYPE", "Altura x Largura x Profundidade");
                    xw.WriteString(the_physdesc.dimension);
                    // </dimensions>
                    xw.WriteEndElement();
                }

                if (!the_date.Equals(String.Empty)) {
                    // <date>
                    xw.WriteStartElement("date");
                    xw.WriteString(the_date);
                    // </date>
                    xw.WriteEndElement();
                }

                xw.WriteString(the_physdesc.notes);
                // </physdesc>
                xw.WriteEndElement();
            }

            // <physloc>:
            string physdesc_cota = EADGeneratorRule.Current.get_physdescCota(IDNivel, this.conn);
            if (!physdesc_cota.Equals(string.Empty)) {
                xw.WriteStartElement("physloc");
                xw.WriteAttributeString("label", "Cota(s):");
                xw.WriteString(physdesc_cota);
                // </physloc>:
                xw.WriteEndElement();
            }

            // <repository> (com <corpname>)
            xw.WriteStartElement("repository");
            // <corpname>
            xw.WriteStartElement("corpname");
            string corpName = EADGeneratorRule.Current.get_EntidadeDetentoraForNivel(IDNivel, this.conn);
            xw.WriteString(corpName);
            // </corpname>
            xw.WriteEndElement();
            // </repository>
            xw.WriteEndElement();

            // <origination>
            // produtores
            List<EADGeneratorRule.Origination_EntProdutora_Tipo> originations = EADGeneratorRule.Current.get_EntidadesProdutoras_Origination(IDNivel, this.conn);
            if (originations.Count > 0) {
                xw.WriteStartElement("origination");
                xw.WriteAttributeString("label", "Creator");
                foreach (EADGeneratorRule.Origination_EntProdutora_Tipo orig in originations)
                    if (!orig.termo.Equals(string.Empty)) {
                        switch (orig.IDTipoEntidadeProdutora) {
                            case 1:     // Colectividade
                                xw.WriteStartElement("corpname");
                                xw.WriteAttributeString("role", "produtor");
                                xw.WriteString(orig.termo);
                                xw.WriteEndElement();
                                break;
                            case 2:     // Familia
                                xw.WriteStartElement("famname");
                                xw.WriteAttributeString("role", "produtor");
                                xw.WriteString(orig.termo);
                                xw.WriteEndElement();
                                break;
                            case 3:     // Pessoa
                                xw.WriteStartElement("persname");
                                xw.WriteAttributeString("role", "produtor");
                                xw.WriteString(orig.termo);
                                xw.WriteEndElement();
                                break;
                            default:
                                xw.WriteString(orig.termo);
                                break;
                        }
                    }

                // Autores
                originations = EADGeneratorRule.Current.get_Autores_Origination(IDNivel, this.conn);
                if (originations.Count > 0)
                {
                    foreach (EADGeneratorRule.Origination_EntProdutora_Tipo orig in originations)
                        if (!orig.termo.Equals(string.Empty))
                        {
                            switch (orig.IDTipoEntidadeProdutora)
                            {
                                case 1:     // Colectividade
                                    xw.WriteStartElement("corpname");
                                    xw.WriteAttributeString("role", "autor");
                                    xw.WriteString(orig.termo);
                                    xw.WriteEndElement();
                                    break;
                                case 2:     // Familia
                                    xw.WriteStartElement("famname");
                                    xw.WriteAttributeString("role", "autor");
                                    xw.WriteString(orig.termo);
                                    xw.WriteEndElement();
                                    break;
                                case 3:     // Pessoa
                                    xw.WriteStartElement("persname");
                                    xw.WriteAttributeString("role", "autor");
                                    xw.WriteString(orig.termo);
                                    xw.WriteEndElement();
                                    break;
                                default:
                                    xw.WriteString(orig.termo);
                                    break;
                            }
                        }
                }

                // </origination>
                xw.WriteEndElement();
            }
            // <langmaterial>
            gen_langmaterial(IDNivel);

            // <dao>
            gen_dao(IDNivel);

            // <did>
            xw.WriteEndElement();
        }

        #region <langmaterial> (dentro do <did>)
        private void gen_langmaterial(long IDNivel) {
            List<EADGeneratorRule.LangMaterial> lang = EADGeneratorRule.Current.get_LangMaterial(IDNivel, this.conn);
            if (lang.Count > 0) {
                // <langmaterial>
                xw.WriteStartElement("langmaterial");
                foreach (EADGeneratorRule.LangMaterial l in lang) {
                    // <language>
                    xw.WriteStartElement("language");
                    xw.WriteAttributeString("langcode", l.BibliographicCodeAlpha3);
                    xw.WriteString(l.LanguageNameEnglish);
                    // </language>
                    xw.WriteEndElement();
                }
                // </langmaterial>
                xw.WriteEndElement();
            }
        }

        #endregion

        #endregion

        #region <appraisal> (dentro do <archdesc> & <c>)
        private void gen_appraisal(long IDNivel) {
            EADGeneratorRule.Appraisal theAppraisal = EADGeneratorRule.Current.get_Appraisal(IDNivel, this.conn);

            if (!theAppraisal.isEmpty()) {
                // <appraisal>
                xw.WriteStartElement("appraisal");

                if (!theAppraisal.pertinencia_Nivel.Equals(string.Empty))
                    gen_P_header_and_list("Nível", theAppraisal.pertinencia_Nivel);
                if (!theAppraisal.pertinencia_Ponderacao.Equals(string.Empty))
                    gen_P_header_and_list("Ponderação", theAppraisal.pertinencia_Ponderacao);
                if (!theAppraisal.pertinencia_FreqUso.Equals(string.Empty))
                    gen_P_header_and_list("Frequência de uso", theAppraisal.pertinencia_FreqUso);
                if (!theAppraisal.densidade_Tipo.Equals(string.Empty))
                    gen_P_header_and_list("Tipo de produção", theAppraisal.densidade_Tipo);
                if (!theAppraisal.densidade_Grau.Equals(string.Empty))
                    gen_P_header_and_list("Grau de densidade", theAppraisal.densidade_Grau);

                foreach (EADGeneratorRule.Appraisal_InfoRelacionada infoRel in theAppraisal.densidade_InfoRel) {
                    if (!infoRel.densidade_InfoRel_Titulo.Equals(string.Empty))
                        gen_P_header_and_list("Informação relacionada - Título", infoRel.densidade_InfoRel_Titulo);
                    if (!infoRel.densidade_InfoRel_Tipo.Equals(string.Empty))
                        gen_P_header_and_list("Informação relacionada - Tipo de produção", infoRel.densidade_InfoRel_Tipo);
                    if (!infoRel.densidade_InfoRel_Grau.Equals(string.Empty))
                        gen_P_header_and_list("Informação relacionada - Grau de densidade", infoRel.densidade_InfoRel_Grau);
                    if (!infoRel.densidade_InfoRel_Ponderacao.Equals(string.Empty))
                        gen_P_header_and_list("Informação relacionada - Ponderação", infoRel.densidade_InfoRel_Ponderacao);
                }

                // Enquadramento legal:
                if (!theAppraisal.enqdr_Legal_Diploma.Equals(string.Empty))
                    gen_P_header_and_list("Diploma", theAppraisal.enqdr_Legal_Diploma);
                if (!theAppraisal.enqdr_Legal_RefTblSeleccao.Equals(string.Empty))
                    gen_P_header_and_list("Referência na tabela de seleção", theAppraisal.enqdr_Legal_RefTblSeleccao);

                // Destino final:
                if (!theAppraisal.destino_Final.Equals(string.Empty))
                    gen_P_header_and_list("Destino final", theAppraisal.destino_Final);

                // Prazo de conservacao:
                if (!theAppraisal.prazo_Conservacao.Equals(string.Empty))
                    gen_P_header_and_list("Prazo de conservação (anos)", theAppraisal.prazo_Conservacao);

                // Auto de eliminacao:
                if (!theAppraisal.auto_eliminacao.Equals(string.Empty))
                    gen_P_header_and_list("Nº de auto de eliminação", theAppraisal.auto_eliminacao);

                // Observacoes:
                if (!theAppraisal.observacoes.Equals(string.Empty))
                    gen_P_header_and_list("Observações", theAppraisal.observacoes);

                // </appraisal>
                xw.WriteEndElement();
            }
        }

        #endregion

        #region <accruals> (dentro do <archdesc> e <c>)

        private void gen_accruals(long IDNivel) {
            string accruals = EADGeneratorRule.Current.get_Accruals(IDNivel, this.conn);
            if (!accruals.Equals(string.Empty)) {
                // <accruals>:
                xw.WriteStartElement("accruals");
                xw.WriteStartElement("p");
                xw.WriteString(accruals);
                // </p>
                xw.WriteEndElement();
                // </accruals>
                xw.WriteEndElement();
            }
        }
        #endregion

        #region <arrangement> (dentro do <archdesc> e <c>)

        private void gen_arrangement(long IDNivel) {
            EADGeneratorRule.Arrangement arrangement = EADGeneratorRule.Current.get_Arrangement(IDNivel, conn);
            if (!arrangement.isEmpty()) {
                // <arrangement>
                xw.WriteStartElement("arrangement");
                if (arrangement.tradicaoDocumental.Count > 0)
                    gen_P_header_and_list("Tradição documental", arrangement.tradicaoDocumental);
                if (arrangement.ordenacao.Count > 0)
                    gen_P_header_and_list("Ordenação", arrangement.ordenacao);
                // </arrangement>
                xw.WriteEndElement();
            }
        }

        #endregion

        #region <accessrestrict>, <userestrict>, <otherfindaid> (dentro do <archdesc> e <c>)

        private void gen_accessrestrict_userestrict_otherfindaid(long IDNivel) {
            EADGeneratorRule.CondicoesDeAcesso condicoes = EADGeneratorRule.Current.get_CondicoesDeAcesso(IDNivel, this.conn);

            if (!condicoes.CondicaoDeAcesso.Equals(string.Empty)) {
                // <accessrestrict>
                xw.WriteStartElement("accessrestrict");
                xw.WriteStartElement("p");
                xw.WriteString(condicoes.CondicaoDeAcesso);
                xw.WriteEndElement();
                // </accessrestrict>
                xw.WriteEndElement();
            }
            if (!condicoes.CondicaoDeReproducao.Equals(string.Empty)) {
                // <userestrict>
                xw.WriteStartElement("userestrict");
                xw.WriteStartElement("p");
                xw.WriteString(condicoes.CondicaoDeReproducao);
                xw.WriteEndElement();
                // </userestrict>
                xw.WriteEndElement();
            }
            if (!condicoes.AuxiliarDePesquisa.Equals(string.Empty)) {
                // <otherfindaid>
                xw.WriteStartElement("otherfindaid");
                xw.WriteStartElement("p");
                xw.WriteString(condicoes.AuxiliarDePesquisa);
                xw.WriteEndElement();
                // </otherfindaid>
                xw.WriteEndElement();
            }
        }

        #endregion

        #region <phystech> (dentro do <archdesc> e <c>)

        private void gen_phystech(long IDNivel) {
            EADGeneratorRule.PhysTech phystech = EADGeneratorRule.Current.get_PhysTech(IDNivel, this.conn);
            if (!phystech.isEmpty()) {
                // <phystech>
                xw.WriteStartElement("phystech");
                if (phystech.suporte_acondicionamento.Count > 0)
                    gen_P_header_and_list("Forma de suporte e/ou acondicionamento", phystech.suporte_acondicionamento);
                if (phystech.material_suporte.Count > 0)
                    gen_P_header_and_list("Material de suporte", phystech.material_suporte);
                if (phystech.tecnicas_registo.Count > 0)
                    gen_P_header_and_list("Técnica de registo", phystech.tecnicas_registo);
                if (phystech.estado_conservacao.Count > 0)
                    gen_P_header_and_list("Estado de conservação", phystech.estado_conservacao);
                // </phystech>
                xw.WriteEndElement();
            }
        }
        #endregion

        #region <originalsloc>, <altformavail>, <relatedmaterial>, <bibliography> (dentro do <archdesc> & <c>)
        private void gen_DocumentacaoAssociada(long IDNivel) {
            EADGeneratorRule.DocumentacaoAssociada docAssoc = EADGeneratorRule.Current.get_DocumentacaoAssociada(IDNivel, this.conn);

            if (!docAssoc.originalsloc.Equals(string.Empty)) {
                xw.WriteStartElement("originalsloc");
                xw.WriteStartElement("p");
                xw.WriteString(docAssoc.originalsloc);
                xw.WriteEndElement();
                // </originalsloc>
                xw.WriteEndElement();
            }

            if (!docAssoc.altformavail.Equals(string.Empty)) {
                xw.WriteStartElement("altformavail");
                xw.WriteStartElement("p");
                xw.WriteString(docAssoc.altformavail);
                xw.WriteEndElement();
                // </altformavail>
                xw.WriteEndElement();
            }
            if (!docAssoc.relatedmaterial.Equals(string.Empty)) {
                xw.WriteStartElement("relatedmaterial");
                xw.WriteStartElement("p");
                xw.WriteString(docAssoc.relatedmaterial);
                xw.WriteEndElement();
                // </relatedmaterial>
                xw.WriteEndElement();
            }
            if (!docAssoc.bibliography.Equals(string.Empty)) {
                xw.WriteStartElement("bibliography");
                xw.WriteStartElement("p");
                xw.WriteString(docAssoc.bibliography);
                xw.WriteEndElement();
                // </bibliography>
                xw.WriteEndElement();
            }
        }

        #endregion

        #region <processinfo> (dentro do <archdesc> & <c>)

        private void gen_processinfo(long IDNivel) {
            EADGeneratorRule.Processinfo pi = EADGeneratorRule.Current.get_Processinfo(IDNivel, this.conn);
            if (!pi.isEmpty()) {
                // <processinfo>
                xw.WriteStartElement("processinfo");
                if (!pi.nota_arquivista.Equals(string.Empty)) {
                    xw.WriteStartElement("p");
                    xw.WriteString(pi.nota_arquivista);
                    // </p>
                    xw.WriteEndElement();
                }
                foreach (EADGeneratorRule.Processinfo_Date registo in pi.datas_autores) 
                    gen_p_date_type(registo);

                // </processinfo>
                xw.WriteEndElement();
            }
        }

        private void gen_p_date_type(EADGeneratorRule.Processinfo_Date the_Processinfo_Date) {
            xw.WriteStartElement("p");
            bool author = false;
            // Autor e/ou operador:
            if (!the_Processinfo_Date.autoridade.Equals(string.Empty)) {
                xw.WriteString("Autor: " + the_Processinfo_Date.autoridade + " ");
                author = true;
            }
            if (!the_Processinfo_Date.operador.Equals(string.Empty))
                xw.WriteString((author ? "; " : "") + "Operador: " + the_Processinfo_Date.operador);

            if (!the_Processinfo_Date.data_descricao.Equals(string.Empty)) {
                // <date>
                xw.WriteStartElement("date");
                xw.WriteAttributeString("type", "descrição");
                xw.WriteString(the_Processinfo_Date.data_descricao);
                // </date>:
                xw.WriteEndElement();
            }

            if (!the_Processinfo_Date.data_registo.Equals(string.Empty)) {
                // <date>
                xw.WriteStartElement("date");
                xw.WriteAttributeString("type", "registo");
                xw.WriteString(the_Processinfo_Date.data_registo);
                // </date>:
                xw.WriteEndElement();
            }

            // </p>
            xw.WriteEndElement();
        }

        #endregion

        #region <note> (dentro do <archdesc> e <c>)
        private void gen_note(long IDNivel) {
            string note = EADGeneratorRule.Current.get_NotaGeral(IDNivel, this.conn);
            if (!note.Equals(string.Empty)) {
                xw.WriteStartElement("note");
                xw.WriteStartElement("p");
                xw.WriteString(note);
                xw.WriteEndElement();
                // </note>
                xw.WriteEndElement();
            }


        }

        #endregion

        #region <dao>

        private void gen_dao(long IDNivel) {
            string href = EADGeneratorRule.Current.get_dao_href(IDNivel, this.conn);
            if (!href.Equals(string.Empty)) {
                // <dao>
                xw.WriteStartElement("dao");
                xw.WriteAttributeString("linktype", "simple");
                xw.WriteAttributeString("href", href);
                // </dao>
                xw.WriteEndElement();
            }
        }

        #endregion

        #region UTILS

        /*
         * Escreve cada string de list_of_strings via o dado xmlTextWriter entre o par <p> e </p>.
         * Ignora strings vazias.
         */
        private void generate_P_elems(string[] list_of_strings, XmlTextWriter xmlTextWriter) {
            foreach (string _p in list_of_strings) {
                if (!_p.Equals(string.Empty)) {
                    xmlTextWriter.WriteStartElement("p");
                    xmlTextWriter.WriteString(_p);
                    xmlTextWriter.WriteEndElement();
                }
            }
        }

        /*
         * Escreve em this.xw:
         * <p>
         * o header seguido de ':' 
         * lista de strings, com cada uma separada por ';'
         * </p>
         */
        private void gen_P_header_and_list(string header, List<string> list) {
            // <P>
            xw.WriteStartElement("p");
            xw.WriteString(header + " : ");
            int i = 0;
            foreach (string str in list)
                xw.WriteString(i++ == 0 ? str : "; " + str);
            // </P>
            xw.WriteEndElement();
        }

        /* 
         * Idem
         */
        private void gen_P_header_and_list(string header, string one_string) {
            this.gen_P_header_and_list(header, new List<String> { one_string });
        }

        #endregion

    }   // class EADGenerator
}
