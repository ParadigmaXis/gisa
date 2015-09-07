using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Diagnostics;

using DBAbstractDataLayer.DataAccessRules;
using GISA.Model;
using GISA.IntGestDoc.Model;
using GISA.IntGestDoc.Model.EntidadesExternas;
using GISA.Webservices.DocInPorto;
using GISA.Webservices.ProdDocInPortoWebService;
using GISA.Webservices.ToponimiaWS;

namespace GISA.IntGestDoc.Controllers
{
    public class DocInPortoWS : IIntGestDocService
    {
        private string serviceEndPoint;

        public DocInPortoWS()
        {
            this.serviceEndPoint = string.Empty;
        }

        #region IIntGestDocService Members
        public List<DocumentoExterno> GetDocumentos(DateTime timeStamp, int maxDocs)
        {
            var diprecords = GetDocumentosEnviadosParaArquivoGeral(timeStamp, maxDocs);
            var mrecords = GetMoradas(diprecords.Select(dipr => dipr.CODMORADA).Distinct());

            // se ocorrer alguma excepção no serviço de moradas, aborta-se o processo
            if (mrecords == null)
                return new List<DocumentoExterno>();

            //DIPRecordsExportCsv(diprecords);

            var des = this.ToDocumentosExternos(diprecords, mrecords);
            return des;
        }

        private void DIPRecordsExportCsv(List<DocumentoInfoArquivoGeral> diprecords)
        {
            using (StreamWriter sw = new StreamWriter("d://a.csv", false, UTF8Encoding.UTF8))
            {
                diprecords.ForEach(diprecord =>
                {
                    sw.Write(string.Join(";", new string[] { diprecord.NUD, diprecord.NUP, diprecord.NUD_CAPA, diprecord.DATAREGISTO, diprecord.ID_DOCUMENTO }));
                    sw.Write("\r\n");
                });
                sw.Flush();
                sw.Close();
            }
        }
        #endregion

        public List<CorrespondenciaDocs> FilterPreviousIncorporations(List<CorrespondenciaDocs> corresp)
        {
            var desDictionary = corresp.ToDictionary(c => new IntGestDocRule.DocInPortoRecord() { IDExterno = c.EntidadeExterna.IDExterno, DataArquivo = c.EntidadeExterna.Timestamp, IDSistema = (int)c.EntidadeExterna.Sistema, IDTipoEntidade = (int)c.EntidadeExterna.Tipo }, c => c.EntidadeExterna);
            var correspDictionary = corresp.ToDictionary(c => c.EntidadeExterna, c => c);

            List<IntGestDocRule.DocInPortoRecord> diprecordsFiltered = null;
            var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                diprecordsFiltered = IntGestDocRule.Current.FilterPreviousIncorporations(desDictionary.Keys.ToList(), ho.Connection);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
            finally
            {
                ho.Dispose();
            }
            
            var desFiltered = diprecordsFiltered.Where(rec => desDictionary.ContainsKey(rec)).Select(r => desDictionary[r]).ToList();
            return desFiltered.Where(de => correspDictionary.ContainsKey(de)).Select(d => correspDictionary[d]).ToList();
        }

        public List<DocumentoExterno> ToDocumentosExternos(List<DocumentoInfoArquivoGeral> diprecords, List<MoradaRecord> mrecords)
        {
            Dictionary<RegistoAutoridadeExterno, RegistoAutoridadeExterno> raes = new Dictionary<RegistoAutoridadeExterno, RegistoAutoridadeExterno>();
            Dictionary<DocumentoExterno, DocumentoExterno> des = new Dictionary<DocumentoExterno, DocumentoExterno>();
            Dictionary<DocumentoComposto, string> ProcDataFinal = new Dictionary<DocumentoComposto, string>();
            Dictionary<DocumentoComposto, Produtor> ProcProdutorFimLinha = new Dictionary<DocumentoComposto, Produtor>();

            var ticks = DateTime.Now.Ticks;

            int docSimplesOrderNr = 1;
            int docCompostoOrderNr = 1;

            foreach (var diprecord in diprecords)
            {
                DateTime timestamp = DateTime.ParseExact(diprecord.DATA_ARQUIVOGERAL.TrimEnd('0'), "dd-MM-yyyy HH:mm:ss,FFFFFFF", System.Globalization.CultureInfo.InvariantCulture);
                DocumentoSimples docExt = new DocumentoSimples(Sistema.DocInPorto, timestamp, docSimplesOrderNr++);
                docExt.NUD = diprecord.NUD;
                docExt.NUDCapa = diprecord.NUD_CAPA;
                docExt.Processo = (DocumentoComposto)AddEntidade(des, new DocumentoComposto(Sistema.DocInPorto, timestamp, docCompostoOrderNr++) { NUP = diprecord.NUP });
                docExt.NumeroEspecifico = diprecord.NUMEROESP;
                //validar data criação
                try
                {
                    DataIncompleta.ParseDate(diprecord.DATAREGISTO);
                }
                catch (FormatException e)
                {
                    MessageBox.Show(string.Format("O campo DATAREGISTO do registo DocInPorto com o NUD {0} contém uma data inválida." + System.Environment.NewLine + "O processo vai ser abortado.", diprecord.NUD), "Integração", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Trace.WriteLine(string.Format("O campo DATAREGISTO do registo DocInPorto com o NUD {0} contém uma data inválida ({1}): {2}", diprecord.NUD, diprecord.DATAREGISTO, e.ToString()));
                    return new List<DocumentoExterno>();
                }
                catch (Exception e)
                {
                    MessageBox.Show(string.Format("O campo DATAREGISTO do registo DocInPorto com o NUD {0} contém uma data inválida." + System.Environment.NewLine + "O processo vai ser abortado.", diprecord.NUD), "Integração", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Trace.WriteLine(string.Format("O campo DATAREGISTO do registo DocInPorto com o NUD {0} contém uma data inválida ({1}): {2}", diprecord.NUD, diprecord.DATAREGISTO, e.ToString()));
                    return new List<DocumentoExterno>();
                }
                docExt.DataCriacao = diprecord.DATAREGISTO;
                docExt.DataArquivoGeral = diprecord.DATA_ARQUIVOGERAL;
                docExt.Conteudos = new List<DocumentoExterno.Conteudo>();
                docExt.Notas = diprecord.NOTAS;
                docExt.RefPredial = diprecord.REFPREDIAL;
                docExt.Local = diprecord.FRACCAO;
                docExt.NumPolicia = diprecord.NR_POLICIA;

                //System.Diagnostics.Debug.Assert(docExt.Timestamp.Year > 2008);

                var tipologia = new Tipologia(Sistema.DocInPorto, timestamp);
                if (diprecord.TIPOREGISTO.Equals("Entrada"))
                    tipologia.Titulo = diprecord.TIPODOCUMENTOENTRADA;
                else
                {
                    tipologia.Titulo = diprecord.TIPOREGISTO;
                    tipologia.ID = diprecord.ID_TIPOREGISTO;
                }
                docExt.Tipologia = (Tipologia)AddEntidade(raes, tipologia);

                var produtor = new Produtor(Sistema.DocInPorto, timestamp);
                produtor.Codigo = diprecord.UNIDADEORGANICA_NOMEMECANOGRAFICO;

                docExt.Assunto = diprecord.ASSUNTO;

                var onomastico = new Onomastico(Sistema.DocInPorto, timestamp);
                onomastico.Titulo = diprecord.ENTIDADE_NOME;
                onomastico.NIF = diprecord.ENTIDADE_NIF;
                docExt.Onomastico = (Onomastico)AddEntidade(raes, onomastico);

                if (diprecord.CODMORADA != null)
                {
                    var toponimia = new Geografico(Sistema.DocInPorto, timestamp);
                    toponimia.Codigo = diprecord.CODMORADA;
                    try
                    {
                        toponimia.Titulo = mrecords.SingleOrDefault(mrec => mrec.CodigoMorada == diprecord.CODMORADA).Nome;
                    }
                    catch
                    {
                        toponimia.Titulo = diprecord.CODMORADA; // como último recurso
                    }
                    toponimia.NroPolicia = docExt.NumPolicia;
                    docExt.Toponimia = (Geografico)AddEntidade(raes, toponimia);
                }

                if (!(diprecord.TECNICO_NOME == null && diprecord.TECNICO_NIF == null))
                {
                    var tecObra = new Onomastico(Sistema.DocInPorto, timestamp);
                    tecObra.Titulo = diprecord.TECNICO_NOME;
                    tecObra.NIF = diprecord.TECNICO_NIF;
                    docExt.TecnicoDeObra = (Onomastico)AddEntidade(raes, tecObra);
                }

                var proc = docExt.Processo;
                if (diprecord.CONFIDENCIALIDADE != enConfidencialidade.enTipoConfidencialidadePublico)
                    proc.Confidencialidade = GetConfidencialidadeString(diprecord.CONFIDENCIALIDADE);
                proc.Timestamp = docExt.Timestamp;

                if (proc.NUP.Equals(@"P/" + docExt.NUD))
                    proc.DataInicio = docExt.DataCriacao;
                
                if (diprecord.ASSUNTO != null)
                {   
                    if (proc.NUP.Equals(@"P/" + docExt.NUD))
                    {
                        try
                        {
                            proc.Tipologia = (Tipologia)AddEntidade(raes, new Tipologia(Sistema.DocInPorto, timestamp) { Titulo = diprecord.ASSUNTO, Timestamp = docExt.Timestamp });
                        }
                        catch (ArgumentNullException e1)
                        {
                            Trace.WriteLine(string.Format("Erro na criação da tipologia para o processo {0} com o título {1}: {2}.", proc.IDExterno, diprecord.ASSUNTO, e1.ToString()));
                        }
                        catch (NullReferenceException e2)
                        {
                            MessageBox.Show(string.Format("O processo {0} não tem tipologia associada." + System.Environment.NewLine + "O processo vai ser abortado.", proc.IDExterno), "Integração", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Trace.WriteLine(string.Format("Tipologia do processo {0} sem título: {1}.", proc.IDExterno, e2.ToString()));
                        }
                    }

                    if (diprecord.ASSUNTO.Contains("averbamento"))
                        proc.AverbamentosDeRequerenteOuProprietario.Add(docExt.Onomastico.Titulo);
                    else
                        proc.RequerentesOuProprietariosIniciais.Add(docExt.Onomastico.Titulo);
                }
                
                if (docExt.Toponimia != null)
                    proc.LocalizacoesObraDesignacaoActual.Add(new DocumentoComposto.LocalizacaoObraActual() { LocalizacaoObraDesignacaoActual = docExt.Toponimia, NroPolicia = docExt.NumPolicia });

                if (!ProcDataFinal.ContainsKey(proc))
                {
                    proc.DataFim = docExt.DataCriacao;
                    ProcDataFinal[proc] = docExt.DataCriacao;
                }

                var prod = (Produtor)AddEntidade(raes, produtor);
                if (!ProcProdutorFimLinha.ContainsKey(proc))
                {
                    proc.Produtor = prod;
                    ProcProdutorFimLinha[proc] = prod;
                }

                if (DataIncompleta.CompareDates(ProcDataFinal[proc], docExt.DataCriacao) < 0)
                {
                    proc.DataFim = docExt.DataCriacao;
                    proc.Produtor = prod;
                    ProcDataFinal[proc] = docExt.DataCriacao;
                    ProcProdutorFimLinha[proc] = prod;
                }

                if (docExt.TecnicoDeObra != null)
                    proc.TecnicosDeObra.Add(docExt.TecnicoDeObra);

                if (diprecord.ARRAYCONTEUDOS != null)
                {
                    foreach (var conteudo in diprecord.ARRAYCONTEUDOS)
                    {
                        if (conteudo.TIPO.Equals("Anexo") && conteudo.NUMEROANEXO != null)
                        {
                            DocumentoAnexo docExtAnexo = new DocumentoAnexo(Sistema.DocInPorto, timestamp, docSimplesOrderNr++);
                            docExtAnexo.NUD = conteudo.NUMEROANEXO;
                            docExtAnexo.Descricao = conteudo.DESCRICAO;
                            docExtAnexo.TipoDescricao = conteudo.TIPODESCRICAO;
                            docExtAnexo.Assunto = docExt.Assunto;
                            docExtAnexo.DocumentoSimples = docExt.NUD;
                            docExtAnexo.Processo = docExt.Processo;
                            docExtAnexo.Conteudos = new List<DocumentoExterno.Conteudo>() { new DocumentoExterno.Conteudo() { Ficheiro = conteudo.NOMEFICHEIRO, Tipo = conteudo.TIPODESCRICAO, Titulo = conteudo.NUMEROANEXO, TipoDescricao = conteudo.TIPODESCRICAO } };

                            AddEntidade(des, docExtAnexo);
                        }
                        else
                            docExt.Conteudos.Add(new DocumentoExterno.Conteudo() { Ficheiro = conteudo.NOMEFICHEIRO, Tipo = conteudo.TIPO, Titulo = conteudo.NUMEROANEXO });
                    }
                }

                AddEntidade(des, docExt);
            }

            System.Diagnostics.Debug.WriteLine(">> " + new TimeSpan(DateTime.Now.Ticks - ticks).ToString());

            return des.Keys.ToList();
        }

        private X AddEntidade<X>(Dictionary<X, X> lstEntidades, X novaEntidade) where X : EntidadeExterna
        {
            if (novaEntidade == null)
                throw new ArgumentNullException("Nova entidade não definida");

            if (novaEntidade.IDExterno == null)
                throw new NullReferenceException("Nova entidade sem IDExterno definido: " + novaEntidade.ToString());

            if (!lstEntidades.ContainsKey(novaEntidade))
            {
                lstEntidades.Add(novaEntidade, novaEntidade);
                return novaEntidade;
            }
            return lstEntidades[novaEntidade];
        }

        public virtual List<DocumentoInfoArquivoGeral> GetDocumentosEnviadosParaArquivoGeral(DateTime timeStamp, int maxDocs)
        {
            //return GetWSRecordsFromSerializedSource(@"Database\HistoricalData\20100503-dipExamples.bin");

            return DocInPortoHelper.GetListaDocumentosArquivoGeral(timeStamp, maxDocs);
        }

        public static List<DocumentoInfoArquivoGeral> GetWSRecordsFromSerializedSource(String filename)
        {
            List<DocumentoInfoArquivoGeral> ret = new List<DocumentoInfoArquivoGeral>();

            DocumentoInfoArquivoGeral[] docs = null;
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            docs = (DocumentoInfoArquivoGeral[])bFormatter.Deserialize(stream);
            stream.Close();

            if (docs != null)
            {
                ret.AddRange(docs.ToList<DocumentoInfoArquivoGeral>());
            }
            return ret;
        }

        public virtual List<MoradaRecord> GetMoradas(IEnumerable<string> iEnumerable)
        {
            //return GetWSToponimiasFromSerializedSource(@"Database\HistoricalData\20100503-dipMoradasExamples.bin");

            return DocInPortoHelper.GetMoradas(iEnumerable);
        }

        public static List<MoradaRecord> GetWSToponimiasFromSerializedSource(String filename)
        {
            List<MoradaRecord> ret = new List<MoradaRecord>();

            MoradaRecord[] moradas = null;
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            moradas = (MoradaRecord[])bFormatter.Deserialize(stream);
            stream.Close();

            if (moradas != null)
                ret.AddRange(moradas.ToList<MoradaRecord>());

            return ret;
        }

        private string GetConfidencialidadeString(enConfidencialidade conf)
        {
            switch (conf)
            {
                case enConfidencialidade.enTipoConfidencialidadeConfidencial:
                    return "Confidencial";
                case enConfidencialidade.enTipoConfidencialidadePublico:
                    return "Público";
                case enConfidencialidade.enTipoConfidencialidadeRestrito:
                    return "Restrito";
                default:
                    return "";
            }
        }
    }
}
