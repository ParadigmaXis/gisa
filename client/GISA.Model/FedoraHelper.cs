using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Net;

using GISA.Fedora.FedoraHandler;
using GISA.Fedora.FedoraHandler.Fedora.APIA;
using GISA.Fedora.FedoraHandler.Fedora.APIM;

namespace GISA.Model
{
    public class FedoraHelper
    {
        private string server = string.Empty;
        private string username = string.Empty;
        private string password = string.Empty;
        private string gisaOperator = string.Empty;
        public const string gisaPrefix = "gisa:";
        public static string typeFedora = GUIHelper.TranslationHelper.FormatTipoAcessoEnumToTipoAcessoText(ResourceAccessType.Fedora);
        private static Quality defaultQuality;

        public FedoraHelper()
        {
            var config = GisaDataSetHelper.GetInstance().GlobalConfig.Cast<GISADataset.GlobalConfigRow>().Single();

            if (config == null || config.IsFedoraServerUrlNull() || config.FedoraServerUrl.Length == 0 ||
                config.IsFedoraUsernameNull() || config.FedoraUsername.Length == 0 ||
                config.IsFedoraPasswordNull() || config.FedoraPassword.Length == 0)
            {
                return;
            }

            server = ((GISADataset.GlobalConfigRow)(GisaDataSetHelper.GetInstance().GlobalConfig.Rows[0])).FedoraServerUrl;
            username = ((GISADataset.GlobalConfigRow)(GisaDataSetHelper.GetInstance().GlobalConfig.Rows[0])).FedoraUsername;
            password = ((GISADataset.GlobalConfigRow)(GisaDataSetHelper.GetInstance().GlobalConfig.Rows[0])).FedoraPassword;
            gisaOperator = SessionHelper.GetGisaPrincipal().TrusteeUserOperator.TrusteeRow.Name;

            defaultQuality = config.IsQualidadeImagemNull() ? Quality.Low : FedoraHelper.TranslateQualityEnum(config.QualidadeImagem);
        }

        public string ServerUrl { get { return this.server; } }
        public string Username { get { return this.username; } }
        public string Password { get { return this.password; } }

        public FedoraConnection fedoraConnect;
        public bool Connect()
        {
            Uri newUri;
            if (Uri.TryCreate(server, UriKind.Absolute, out newUri))
            {
                fedoraConnect = new FedoraConnection(newUri, gisaOperator);
                return fedoraConnect.Connect(username, password);
            }
            else
            {
                return false;
            }
        }

        public string GetRepositoryName() {
            try 
            { 
                return this.fedoraConnect.GetRepositoryInfo().repositoryName; 
            } 
            catch 
            { 
                return ""; 
            }   
        }

        public ObjDigital LoadID(string pid, string timestamp)
        {
            try 
            {
                return fedoraConnect.GetStructureForPid(pid, timestamp, true);
            }
            catch 
            {
                // TODO: apanhar e lançar a excepção provocada por uma falha com a comunicação com o servidor
                Trace.WriteLine(string.Format("O objeto não foi encontrado! ({0})", pid)); return null; 
            }
        }

        public void CheckIntegrity(ObjDigSimples objecto)
        {
            List<string> badDatastreams = new List<string>();

            foreach (Anexo anexo in objecto.fich_associados)
            {
                if(!fedoraConnect.CheckIntegrity(objecto.pid, anexo.dataStreamID, anexo.checksum)) {
                    badDatastreams.Add(anexo.url);
                }
            }

            string result;
            MessageBoxIcon icon;

            if(badDatastreams.Count > 0) 
            {
                if (badDatastreams.Count == 1) result = "Foi detetada uma falha de integridade na imagem:\n\n";
                else if (badDatastreams.Count > 10) result = String.Format("Foram detetadas {0} falhas de integridade neste documento, entre as quais:\n\n", badDatastreams.Count);
                else result = "Foram detetados falhas de integridade nas seguintes imagens:\n\n";

                icon = MessageBoxIcon.Warning;
                for (int i = 0; i < Math.Min(10, badDatastreams.Count); i++)
                {
                    result += "  - " + badDatastreams[i] + "\n";
                }
            } 
            else
            {
                result = "O objeto digital está totalmente integro.";
                icon = MessageBoxIcon.Information;
            }

            MessageBox.Show(result, "Verificação de Integridade", MessageBoxButtons.OK, icon);
        }

        public string GetDatastream(string pid, string datastreamId, out bool success, out string errorMessage)
        {
            success = true;
            errorMessage = null;

            string url = fedoraConnect.GetUrlForDatastream(pid, datastreamId, null);
            return ImageHelper.getAndConvertImageResourceToPng(url, out success, out errorMessage, true);
        }

        /*
         * Quando se trata de pdfs muito grandes o soap request dá uma excepção de OutOfMemoryException. 
         * Este método é uma alternativa e faz um pedido directo por Url (como acontece no gisa internet
        */
        public string GetDisseminatorByUrl(string pid, Quality imageQuality, out bool success, out string errorMessage)
        {
            success = true;
            errorMessage = null;

            var config = ((GISADataset.GlobalConfigRow)(GisaDataSetHelper.GetInstance().GlobalConfig.Rows[0]));
            var url = string.Format("{0}objects/{1}/methods/{2}:getPdfSDef/getPDF?parm1={3}", config.FedoraServerUrl, pid, fedoraConnect.GetRepositoryInfo().repositoryPIDNamespace, imageQuality.ToString());

            try
            {
                Uri uri = new Uri(url);
                WebRequest request = WebRequest.Create(uri);
                var username = ((GISADataset.GlobalConfigRow)(GisaDataSetHelper.GetInstance().GlobalConfig.Rows[0])).FedoraUsername;
                var password = ((GISADataset.GlobalConfigRow)(GisaDataSetHelper.GetInstance().GlobalConfig.Rows[0])).FedoraPassword;
                var credentials = new CredentialCache();

                credentials.Add(uri, "Basic", new NetworkCredential(username, password));
                request.Credentials = credentials;
                request.PreAuthenticate = true;
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(username + ":" + password)));

                WebResponse response = request.GetResponse();
                Stream downloadedStream = response.GetResponseStream();

                string gisaTempPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\ParadigmaXis\\GISA";
                string fullFilename = gisaTempPath + "\\" + DateTime.Now.ToFileTime().ToString() + ".pdf";

                try
                {
                    FileStream fileStream = new FileStream(fullFilename, FileMode.Create);

                    // Copy downloaded stream to file
                    byte[] buffer = new byte[8 * 1024];
                    int len;
                    while ((len = downloadedStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileStream.Write(buffer, 0, len);
                    }

                    fileStream.Close();
                    downloadedStream.Close();
                    response.Close();

                    return fullFilename;
                }
                catch (Exception ex)
                {
                    throw new IOException("Erro ao escrever o ficheiro obtido de " + url + ":", ex);
                }
            }
            catch (Exception)
            {
                success = false;
                errorMessage = "Ocorreu um erro ao obter o objeto digital. Por favor contacte o administrador de sistema.";
            }

            return "";
        }

        /*
         * Não está a ser usado por ninguém. Foi subsituido por GetDisseminatorByUrl porque dá uma excepção OutOfMemoryException
         * no System.Xml.dll quando se tenta pedir pdfs grandes (o exemplo usado ocupava 150MB
        */
        public string GetDisseminator(string pid, Quality imageQuality, out bool success, out string errorMessage)
        {
            MIMETypedStream stream = this.fedoraConnect.GetDisseminatorForPid(pid, "getPDF", imageQuality);
            success = true;
            errorMessage = null;

            if(stream != null) 
            {
                if (stream.MIMEType == "text/xml")
                {
                    success = false;
                    errorMessage = "O objeto digital tem um erro na sua estrutura. Por favor contacte o administrador de sistema."; 
                    string tempString = Encoding.UTF8.GetString(stream.stream, 0, stream.stream.Length);
                    Trace.WriteLine(errorMessage);
                    Trace.WriteLine(tempString);
                }
                else if (stream.MIMEType == "text/plain")
                {
                    string tempString = Encoding.UTF8.GetString(stream.stream, 0, stream.stream.Length);

                    string[] parts = tempString.Split(new char[] { ' ' }, 2);
                    if (parts.Length == 2)
                    {
                        int code = 0;
                        if (int.TryParse(parts[0], out code) && code == 202)
                        {
                            success = false;
                            errorMessage = "O objeto requisitado está a ser processado. Por favor tente mais tarde.";
                        }
                        else if (int.TryParse(parts[0], out code) && code == 500)
                        {
                            success = false;
                            errorMessage = "De momento não é possivel requisitar este objeto. Por favor tente mais tarde.";
                        }
                        else
                        {
                            success = false;
                            errorMessage = "Ocorreu um erro ao obter o objeto digital. Por favor contacte o administrador de sistema.";
                        }
                    }
                    else
                    {
                        success = false;
                        errorMessage = "Ocorreu um erro ao obter o objeto digital. Por favor contacte o administrador de sistema.";
                    }
                    
                    Trace.WriteLine(tempString + " / " + pid + " / " + imageQuality);
                }
                else
                {
                    string gisaTempPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\ParadigmaXis\\GISA";
                    string fullFilename = gisaTempPath + "\\" + DateTime.Now.ToFileTime().ToString() + ".pdf";
                    try
                    {
                        if (File.Exists(fullFilename)) File.Delete(fullFilename);
                        FileStream fs = new FileStream(fullFilename, FileMode.CreateNew);
                        fs.Write(stream.stream, 0, stream.stream.Length);
                        fs.Close();
                        fs.Dispose();
                        return fullFilename;
                    }
                    catch (Exception ex)
                    {
                        success = false;
                        errorMessage = "Não foi possível manipular o ficheiro PDF nesta máquina. Por favor contacte o administrador de sistema.";
                        Trace.WriteLine(ex.ToString());
                    }
                }
            }
            else
            {
                success = false;
                errorMessage = "De momento, este ficheiro PDF encontra-se inacessível. Por favor volte a tentar mais tarde.";
            }

            return "";
        }

        public bool Ingest(ObjDigital doc, out string message)
        {
            switch(fedoraConnect.Ingest(doc))
            {
                case IngestResult.Success:
                    message = "";
                    return true;
                case IngestResult.Partial:
                    message = "Não foi possível efectuar todas as alterações necessárias nos objetos digitais." + Environment.NewLine + "Por esse motivo, alguns objetos poderão aparecer na sua versão anterior. Pode tentar fazer de novo as alterações mais tarde para corrigir este problema.";
                    return true;
                case IngestResult.Error:
                    message = "Não foi possível contactar o repositório digital de momento. Por favor volte a tentar fazer estas alterações mais tarde.";
                    return false;
                default:
                    message = "";
                    return false;
            }
        }

        private const string QUALITY_HIGH_EN = "High";
        private const string QUALITY_MEDIUM_EN = "Medium";
        private const string QUALITY_LOW_EN = "Low";
        private const string QUALITY_TINY_EN = "Tiny";
        private const string QUALITY_HIGH = "Alta";
        private const string QUALITY_MEDIUM = "Média";
        private const string QUALITY_LOW = "Baixa";
        private const string QUALITY_TINY = "Mínima";
        public static string TranslateQualityEnum(Quality quality)
        {
            switch (quality)
            {
                case Quality.High:
                    return QUALITY_HIGH;
                case Quality.Medium:
                    return QUALITY_MEDIUM;
                case Quality.Low:
                    return QUALITY_LOW;
                case Quality.Tiny:
                    return QUALITY_TINY;
                default:
                    return QUALITY_LOW;
            }
        }

        public static Quality TranslateQualityEnum(string quality)
        {
            if (quality.Equals(QUALITY_HIGH))
                return Quality.High;
            else if (quality.Equals(QUALITY_MEDIUM))
                return Quality.Medium;
            else if (quality.Equals(QUALITY_LOW))
                return Quality.Low;
            else if (quality.Equals(QUALITY_TINY))
                return Quality.Tiny;
            else
                return defaultQuality;
        }

        public static string TranslateQualityEnumEn(string quality)
        {
            if (quality.Equals(QUALITY_HIGH_EN))
                return QUALITY_HIGH;
            else if (quality.Equals(QUALITY_MEDIUM_EN))
                return QUALITY_MEDIUM;
            else if (quality.Equals(QUALITY_LOW_EN))
                return QUALITY_LOW;
            else if (quality.Equals(QUALITY_TINY_EN))
                return QUALITY_TINY;
            else
                return TranslateQualityEnum(defaultQuality);
        }

        public static List<ObjDigital> DeleteObjDigital(GISADataset.NivelRow nRow)
        {
            var odsToIngest = new List<ObjDigital>();

            if (ConfigurationManager.AppSettings["GISA.FedoraEnablel"] == "true") return odsToIngest;

            var imgRows = nRow.GetFRDBaseRows().Single().GetSFRDImagemRows().Where(r => r.Tipo.Equals(FedoraHelper.typeFedora));
            if (imgRows.Count() == 0) return odsToIngest;

            var odRows = imgRows.Select(r => r.GetSFRDImagemObjetoDigitalRows().Single().ObjetoDigitalRow).ToList();
            if (odRows.Count > 1)
            {
                odRows.ForEach(odRow => {
                    var od = new ObjDigSimples() { pid = odRow.pid, state = State.deleted };
                    odsToIngest.Add(od);
                    FedoraHelper.DeleteObjDigitalRows(odRow);
                });
            }
            else
            {
                if (nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First().IDTipoNivelRelacionado == TipoNivelRelacionado.SD)
                {
                    // está-se a apagar um subdocumento  tem que se apagar o seu OD e, no caso de existir, o seu composto caso só esse composto passe a ficar só com 1 simples

                    var odrhCompRow = odRows.Single().GetObjetoDigitalRelacaoHierarquicaRowsByObjetoDigitalObjetoDigitalRelacaoHierarquica().SingleOrDefault();
                    if (odrhCompRow != null)
                    {
                        // simples com composto
                        var odCompRow = odrhCompRow.ObjetoDigitalRowByObjetoDigitalObjetoDigitalRelacaoHierarquicaUpper;
                        var odSimplesRows = odCompRow.GetObjetoDigitalRelacaoHierarquicaRowsByObjetoDigitalObjetoDigitalRelacaoHierarquicaUpper().Select(r => r.ObjetoDigitalRowByObjetoDigitalObjetoDigitalRelacaoHierarquica).ToList();
                        if (odSimplesRows.Count == 2)
                        {
                            // caso onde o simples a apagar pertence a um composto só com 2 simples. neste caso deve-se apagar o od composto para além do od simples
                            var odComp = new ObjDigComposto() { pid = odCompRow.pid, state = State.deleted };
                            odsToIngest.Add(odComp);
                            FedoraHelper.DeleteObjDigitalRows(odCompRow);
                            var od = new ObjDigSimples() { pid = odRows.Single().pid, state = State.deleted };
                            odComp.objSimples.Add(od);
                            FedoraHelper.DeleteObjDigitalRows(odRows.Single());
                        }
                        else
                        {
                            // caso onde o od simples a apagar pertence a um composto com mais do que 2 simples. neste caso apaga-se o simples e actualiza-se o composto
                            var odComp = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.LoadID(odCompRow.pid, null) as ObjDigComposto;
                            odComp.original = odComp.Clone();
                            odComp.state = State.modified;
                            odComp.objSimples.Single(od => od.pid.Equals(odRows.Single().pid)).state = State.deleted;
                            odsToIngest.Add(odComp);
                        }
                    }
                    else
                    {
                        // caso de um simples sem composto
                        var od = new ObjDigSimples() { pid = odRows.Single().pid, state = State.deleted };
                        odsToIngest.Add(od);
                        FedoraHelper.DeleteObjDigitalRows(odRows.Single());
                    }
                }
                else
                {
                    var odSimplesRows = odRows.Single().GetObjetoDigitalRelacaoHierarquicaRowsByObjetoDigitalObjetoDigitalRelacaoHierarquicaUpper().Select(r => r.ObjetoDigitalRowByObjetoDigitalObjetoDigitalRelacaoHierarquica).ToList();
                    if (odSimplesRows.Count == 0)
                    {
                        // o nivel a apagar só tem um OD simples (não é composto pq não tem nenhum simples associado)
                        var od = new ObjDigSimples() { pid = odRows.Single().pid, state = State.deleted };
                        odsToIngest.Add(od);
                        FedoraHelper.DeleteObjDigitalRows(odRows.Single());
                    }
                    else
                    {
                        // o nivel a apagar tem um OD composto (para este caso esse composto é apagado bem como todos os seus simples associados que serão simples sem associação a subdocumentos)
                        var odComp = new ObjDigComposto() { pid = odRows.Single().pid, state = State.deleted };
                        odsToIngest.Add(odComp);
                        odSimplesRows.ForEach(odRow =>
                        {
                            var od = new ObjDigSimples() { pid = odRow.pid, state = State.deleted };
                            odComp.objSimples.Add(od);
                            FedoraHelper.DeleteObjDigitalRows(odRow);
                        });
                        FedoraHelper.DeleteObjDigitalRows(odRows.Single());
                    }
                }
            }

            return odsToIngest;
        }

        public static ObjDigital UpdateTipAssuntos(GISADataset.NivelRow NivelRow, string newTipologia, List<string> newAssuntos)
        {
            if (NivelRow.IDTipoNivel != TipoNivel.DOCUMENTAL && SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsFedoraEnable()) return null;

            var rhRow = NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First();

            var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                //DBAbstractDataLayer.DataAccessRules.FedoraRule.Current.LoadObjDigitalSimples(GisaDataSetHelper.GetInstance(), NivelRow.ID, rhRow.IDTipoNivelRelacionado, ho.Connection);
                DBAbstractDataLayer.DataAccessRules.FedoraRule.Current.LoadSFRDImagemFedora(GisaDataSetHelper.GetInstance(), NivelRow.ID, rhRow.IDTipoNivelRelacionado, ho.Connection);
            }
            catch (Exception e) { Trace.WriteLine(e.ToString()); throw; }
            finally { ho.Dispose(); }

            var imgRows = NivelRow.GetFRDBaseRows().Single().GetSFRDImagemRows().Where(r => r.Tipo.Equals(FedoraHelper.typeFedora));
            if (imgRows.Count() != 1) return null;

            var odRow = imgRows.Single().GetSFRDImagemObjetoDigitalRows().Single().ObjetoDigitalRow;
            var objDigital = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.LoadID(odRow.pid, null);
            if (objDigital == null) return null;
            objDigital.original = objDigital.Clone();
            objDigital.state = State.modified;

            if (newTipologia != null)
                objDigital.tipologia = newTipologia;

            if (newAssuntos != null)
                objDigital.assuntos = newAssuntos;

            return objDigital;
        }

        // Método responsável por atualizar todos os objetos digitais que tenham associado a tipologia ou assunto passado como argumento
        public void ActualizaObjDigitaisPorNivel(List<string> IDNiveis, string oldTermo, string newTermo, long IDTipoNoticiaAut)
        {
            if (IDTipoNoticiaAut == (long)TipoNoticiaAut.EntidadeProdutora || IDTipoNoticiaAut == (long)TipoNoticiaAut.Modelo || IDTipoNoticiaAut == (long)TipoNoticiaAut.Diploma) return;

            var msg = "";
            var pids = new List<string>();
            var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                var start = DateTime.Now.Ticks;
                DBAbstractDataLayer.DataAccessRules.FedoraRule.Current.GetPidsPorNvl(IDNiveis, ho.Connection, out pids);
                Trace.WriteLine("<<GetPidsPorNvl>>: " + new TimeSpan(DateTime.Now.Ticks - start).ToString());
            }
            catch (Exception ex) { Debug.WriteLine(ex); throw ex; }
            finally { ho.Dispose(); }

            foreach (var pid in pids)
            {
                var objDigital = LoadID(pid, null);
                if (objDigital == null) continue;
                objDigital.original = objDigital.Clone();

                if (IDTipoNoticiaAut == (long)TipoNoticiaAut.TipologiaInformacional)
                    objDigital.tipologia = newTermo;
                else
                {
                    objDigital.assuntos.Remove(oldTermo);
                    if (newTermo != null) objDigital.assuntos.Add(newTermo);
                }

                objDigital.state = State.modified;
                Ingest(objDigital, out msg);
            }
        }

        public static bool HasObjDigReadPermission(string pid)
        {
            return GetObjDigPermission(pid, PermissoesHelper.ObjDigOpREAD);
        }

        public static bool HasObjDigWritePermission(string pid)
        {
            return GetObjDigPermission(pid, PermissoesHelper.ObjDigOpWRITE);
        }

        private static bool GetObjDigPermission(string pid, GISADataset.ObjetoDigitalTipoOperationRow opRow)
        {
            // Calculo de permissões para este OD composto
            var trusteeRow = SessionHelper.GetGisaPrincipal().TrusteeUserOperator.TrusteeRow;
            var perm = PermissoesHelper.PermissionType.ImplicitDeny;
            var odRow = GisaDataSetHelper.GetInstance().ObjetoDigital.Cast<GISADataset.ObjetoDigitalRow>().SingleOrDefault(od => od.pid.Equals(pid));

            if (odRow != null) // se for null é pq o od é novo e ainda não tem a row criada
            {
                GISADataset.NivelRow nivelRow = null;
                if (odRow.GetObjetoDigitalRelacaoHierarquicaRowsByObjetoDigitalObjetoDigitalRelacaoHierarquica().Count() > 0)
                    nivelRow = odRow.GetObjetoDigitalRelacaoHierarquicaRowsByObjetoDigitalObjetoDigitalRelacaoHierarquica().Single()
                        .ObjetoDigitalRowByObjetoDigitalObjetoDigitalRelacaoHierarquicaUpper
                        .GetSFRDImagemObjetoDigitalRows().First().SFRDImagemRowParent.FRDBaseRow.NivelRow;
                else
                    nivelRow = odRow.GetSFRDImagemObjetoDigitalRows().First().SFRDImagemRowParent.FRDBaseRow.NivelRow;
                perm = PermissoesHelper.CalculateEffectivePermissions(odRow, trusteeRow, nivelRow, opRow.TipoOperationRow);
            }
            else
            {
                perm = GisaDataSetHelper.GetInstance().ObjetoDigital.Cast<GISADataset.ObjetoDigitalRow>().Count(row => row.RowState == DataRowState.Added) > 0 ?
                    PermissoesHelper.PermissionType.ExplicitGrant : PermissoesHelper.PermissionType.ExplicitDeny;
            }

            return perm == PermissoesHelper.PermissionType.ExplicitGrant || perm == PermissoesHelper.PermissionType.ImplicitGrant;
        }

        public static GISADataset.NivelRow GetRelatedNivelDoc(string pid)
        {
            var odRow = GisaDataSetHelper.GetInstance().ObjetoDigital.Cast<GISADataset.ObjetoDigitalRow>().SingleOrDefault(od => od.pid.Equals(pid));
            if (odRow == null) return null;

            var imgODRow = odRow.GetSFRDImagemObjetoDigitalRows().FirstOrDefault();
            if (imgODRow == null) return null;

            return imgODRow.SFRDImagemRowParent.FRDBaseRow.NivelRow;
        }

        public static string GetDesignacaoUI(string pid)
        {
            var odRow = GisaDataSetHelper.GetInstance().ObjetoDigital.Cast<GISADataset.ObjetoDigitalRow>().SingleOrDefault(od => od.pid.Equals(pid));
            if (odRow == null) return "";

            var imgODRow = odRow.GetSFRDImagemObjetoDigitalRows().FirstOrDefault();
            if (imgODRow == null) return "";

            var nRow = imgODRow.SFRDImagemRowParent.FRDBaseRow.NivelRow;
            if (nRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First().IDTipoNivelRelacionado == TipoNivelRelacionado.SD)
                return nRow.GetNivelDesignadoRows().Single().Designacao;
            else
                return "";
        }

        public static long GetGisaID(string idgisa)
        {
            return System.Convert.ToInt64(idgisa.Replace(FedoraHelper.gisaPrefix, ""));
        }

        public static ObjDigComposto GetAssociatedODComposto(GISADataset.NivelRow nRow, out GISADataset.ObjetoDigitalRow odRowComp)
        {
            odRowComp = null;
            var objDigital = default(ObjDigComposto);
            var imgRowSource = nRow.GetFRDBaseRows().Single().GetSFRDImagemRows().Where(r => r.Tipo.Equals(FedoraHelper.typeFedora));

            if (imgRowSource.Count() == 1)
            {
                odRowComp = imgRowSource.Single().GetSFRDImagemObjetoDigitalRows().Single().ObjetoDigitalRow;
                var rhODSimplesRows = odRowComp.GetObjetoDigitalRelacaoHierarquicaRowsByObjetoDigitalObjetoDigitalRelacaoHierarquicaUpper().ToList();
                if (rhODSimplesRows.Count() > 0)
                {
                    // obter o objeto digital composto do servidor para se proceder à sua actualizalção
                    objDigital = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.LoadID(odRowComp.pid, null) as ObjDigComposto;
                    objDigital.original = objDigital.Clone();
                }
                else
                    odRowComp = null;
            }

            return objDigital;
        }

        public static ObjDigital CutPasteODSimples(GISADataset.NivelRow sourceRow, GISADataset.ObjetoDigitalRow odCompSourceRow, ObjDigComposto odCompSource, GISADataset.ObjetoDigitalRow odCompTargetRow, ObjDigComposto odCompTarget)
        {
            var imgRow = sourceRow.GetFRDBaseRows().Single().GetSFRDImagemRows().SingleOrDefault(r => r.Tipo.Equals(FedoraHelper.typeFedora));

            if (imgRow == null) return null;

            var odRow = imgRow.GetSFRDImagemObjetoDigitalRows().Single().ObjetoDigitalRow;
            Debug.Assert(odRow != null);

            var odSimples = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.LoadID(odRow.pid, null) as ObjDigSimples;
            odSimples.original = odSimples.Clone();

            if (odCompSourceRow != null)
            {
                Debug.Assert(odCompSourceRow != null);

                // apagar as relações dos objetos simples com o composto de origem
                odRow.GetObjetoDigitalRelacaoHierarquicaRowsByObjetoDigitalObjetoDigitalRelacaoHierarquica().Where(r => r.ObjetoDigitalRowByObjetoDigitalObjetoDigitalRelacaoHierarquicaUpper.pid.Equals(odCompSource.pid)).Single().Delete();
                odCompSource.objSimples.RemoveAt(odCompSource.objSimples.FindIndex(od => od.pid.Equals(odRow.pid)));
                odCompSource.state = State.modified;
            }

            if (odCompTargetRow != null)
            {
                Debug.Assert(odCompTargetRow != null);

                // criar as relações dos objetos simples com o composto de destino
                GisaDataSetHelper.GetInstance().ObjetoDigitalRelacaoHierarquica.AddObjetoDigitalRelacaoHierarquicaRow(odRow, odCompTargetRow, new byte[] { }, 0);
                odCompTarget.objSimples.Add(odSimples);
                odCompTarget.state = State.modified;
            }

            odSimples.parentDocumentTitle = odCompTargetRow != null ? odCompTarget.pid : "";
            odSimples.state = State.modified;

            return odSimples;
        }

        // determina se o um objeto composto ainda faz sentido existir, isto é, se o objeto composto só tiver 1 od simples ou nenhum, então deve ser apagado; caso contrário mantém-se inalterado
        // retorna um ObjDigSimples no caso de o composto ter um simples
        public static ObjDigSimples DeleteODCompostoIfNecessary(GISADataset.NivelRow sourceUpperRow, GISADataset.ObjetoDigitalRow odCompSourceRow, ObjDigComposto odCompSource, ObjDigComposto odCompTarget)
        {
            var odSimples = default(ObjDigSimples);
            if (odCompSource != null && odCompSource.objSimples.Count <= 1)
            {
                if (odCompSource.objSimples.Count == 1)
                {
                    // ainda existe um objeto simples que precisa de ser desassociado do objeto composto
                    odSimples = odCompSource.objSimples[0];
                    odSimples.parentDocumentTitle = odCompTarget != null ? odCompTarget.pid : "";
                    odSimples.state = State.modified;

                    var odrhRow = odCompSourceRow.GetObjetoDigitalRelacaoHierarquicaRowsByObjetoDigitalObjetoDigitalRelacaoHierarquicaUpper().SingleOrDefault(r => r.RowState != DataRowState.Deleted);
                    Debug.Assert(odrhRow != null);

                    var odSimplesRow = odrhRow.ObjetoDigitalRowByObjetoDigitalObjetoDigitalRelacaoHierarquica;

                    if (odSimplesRow.GetSFRDImagemObjetoDigitalRows().Count() == 0)
                    {
                        // se se tratar de um objeto simples solto é preciso ligá-lo directamente ao documento processo
                        var frdRow = sourceUpperRow.GetFRDBaseRows().Single();
                        odSimples.gisa_id = FedoraHelper.gisaPrefix + sourceUpperRow.ID;

                        var imgVolRow = GetRepositoryImagemVolumeRow();

                        var imgRow = GisaDataSetHelper.GetInstance().SFRDImagem.NewSFRDImagemRow();
                        imgRow.Identificador = "";
                        imgRow.FRDBaseRow = frdRow;
                        imgRow.Tipo = FedoraHelper.typeFedora;
                        imgRow.Versao = new byte[] { };
                        imgRow.isDeleted = 0;
                        imgRow.GUIOrder = long.MaxValue;
                        imgRow.Descricao = odSimples.titulo;
                        imgRow.SFRDImagemVolumeRow = imgVolRow;
                        GisaDataSetHelper.GetInstance().SFRDImagem.AddSFRDImagemRow(imgRow);
                        GisaDataSetHelper.GetInstance().SFRDImagemObjetoDigital.AddSFRDImagemObjetoDigitalRow(imgRow.IDFRDBase, imgRow.idx, odSimplesRow, new byte[] { }, 0);
                    }
                }

                // apagar objeto digital composto de origem
                odCompSource.state = State.deleted;

                DeleteObjDigitalRows(odCompSourceRow);
            }

            return odSimples;
        }

        public static void UpdateODSimplesAndSubDocsOrderNr(GISADataset.NivelRow sourceUpperRow, GISADataset.ObjetoDigitalRow odCompSourceRow, ObjDigComposto odCompSource)
        {
            // actualizar ordem dos objetos digitais na origem quer estejam relacionados a um objeto composto quer estejam associados directamente ao nível de origem
            var ndsRows = sourceUpperRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquicaUpper().Where(rh => rh.RowState != DataRowState.Deleted).Select(rh => rh.NivelRowByNivelRelacaoHierarquica).SelectMany(nRow => nRow.GetNivelDesignadoRows().Single().GetNivelDocumentoSimplesRows()).ToDictionary(r => r.GUIOrder, r => r);
            var maxOrderNrSubDocs = ndsRows.Values.Count > 0 ? ndsRows.Values.Max(r => r.GUIOrder) : 0;
            long nextOrderNr = 1;
            var odsSimples = new Dictionary<long, GISADataset.ObjetoDigitalRow>();
            if (odCompSource != null && odCompSource.state != State.deleted)
            {
                odCompSource.objSimples.Where(obj => obj.state != State.deleted).ToList().ForEach(od =>
                {
                    var r = odCompSourceRow.GetObjetoDigitalRelacaoHierarquicaRowsByObjetoDigitalObjetoDigitalRelacaoHierarquicaUpper()
                                .Single(odRow => odRow.ObjetoDigitalRowByObjetoDigitalObjetoDigitalRelacaoHierarquica.pid.Equals(od.pid))
                                .ObjetoDigitalRowByObjetoDigitalObjetoDigitalRelacaoHierarquica;

                    odsSimples.Add(r.GUIOrder, r);
                });
            }
            else
            {
                odsSimples = sourceUpperRow.GetFRDBaseRows().Single().GetSFRDImagemRows()
                        .Where(r => r.Tipo.Equals(FedoraHelper.typeFedora))
                        .Select(r => r.GetSFRDImagemObjetoDigitalRows().Single().ObjetoDigitalRow).ToDictionary(r => r.GUIOrder, r => r);
            }
            var maxOrderNrODsSimples = odsSimples.Count == 0 ? 0 : odsSimples.Values.Max(r => r.GUIOrder);
            var maxOrderNr = maxOrderNrODsSimples >= maxOrderNrSubDocs ? maxOrderNrODsSimples : maxOrderNrSubDocs;

            for (int i = 1; i <= maxOrderNr; i++)
            {
                if (!odsSimples.ContainsKey(i) && !ndsRows.ContainsKey(i))
                    continue;
                else
                {
                    if (odsSimples.ContainsKey(i))
                        odsSimples[i].GUIOrder = nextOrderNr;

                    if (ndsRows.ContainsKey(i))
                        ndsRows[i].GUIOrder = nextOrderNr;

                    nextOrderNr++;
                }
            }
        }

        public static void UpdateODRowGUIOrder(long IDNivel)
        {
            var nRow = GisaDataSetHelper.GetInstance().Nivel.Cast<GISADataset.NivelRow>().Single(r => r.RowState != DataRowState.Deleted && r.ID == IDNivel);
            var ndsRow = nRow.GetNivelDesignadoRows().Single().GetNivelDocumentoSimplesRows().Single();
            var imgRow = nRow.GetFRDBaseRows().Single().GetSFRDImagemRows().SingleOrDefault(r => r.RowState != DataRowState.Deleted && r.Tipo.Equals(FedoraHelper.typeFedora));
            if (imgRow != null)
                imgRow.GetSFRDImagemObjetoDigitalRows().Single().ObjetoDigitalRow.GUIOrder = ndsRow.GUIOrder;
        }

        public static GISADataset.SFRDImagemVolumeRow GetRepositoryImagemVolumeRow()
        {
            var imgVolRow = GisaDataSetHelper.GetInstance().SFRDImagemVolume.Cast<GISADataset.SFRDImagemVolumeRow>().FirstOrDefault(r => r.Mount.Equals(SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.GetRepositoryName()));
            if (imgVolRow == null)
                imgVolRow = GisaDataSetHelper.GetInstance().SFRDImagemVolume.AddSFRDImagemVolumeRow(SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.GetRepositoryName(), new byte[] { }, 0);

            return imgVolRow;
        }

        public static void DeleteObjDigitalRows(GISADataset.ObjetoDigitalRow odRow)
        {
            odRow.GetObjetoDigitalRelacaoHierarquicaRowsByObjetoDigitalObjetoDigitalRelacaoHierarquica().ToList().ForEach(r => r.Delete()); // para o caso dos OD simples (apagar relação com o OD composto)
            odRow.GetObjetoDigitalRelacaoHierarquicaRowsByObjetoDigitalObjetoDigitalRelacaoHierarquicaUpper().ToList().ForEach(r => r.Delete()); // para o caso dos OD compostos (apagar relação com os ODs simples)
            odRow.GetTrusteeObjetoDigitalPrivilegeRows().ToList().ForEach(r => r.Delete());
            odRow.GetSFRDImagemObjetoDigitalRows().ToList().ForEach(r => r.SFRDImagemRowParent.Delete());
            odRow.GetSFRDImagemObjetoDigitalRows().ToList().ForEach(r => r.Delete());
            odRow.Delete();
        }

        public static bool CanDeleteODsAssociated2UI(GISADataset.NivelRow nRow, out ObjDigital objDigital)
        {
            objDigital = default(ObjDigital);
            bool hasPermission = true;

            if (!SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsFedoraEnable()) return hasPermission;

            var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                // stored procedure que vai retornar se tem permissão (pelos objetos digitais associados) para apagar ou não
                hasPermission = DBAbstractDataLayer.DataAccessRules.FedoraRule.Current.CanUserDeleteAnyAssocOD2UI(nRow.ID, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID, ho.Connection);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
            finally
            {
                ho.Dispose();
            }

            if (!hasPermission)
                MessageBox.Show("Não é possivel apagar a unidade informacional selecionada uma vez que não tem permissão para apagar o(s) objeto(s) digital(ais) associado(s).", "Eliminação", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return hasPermission;
        }

        public static string GetAssociatedODsDetailsMsg(long nivelID)
        {
            var res = new System.Text.StringBuilder();
            var ods = new List<string>();

            if (!SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsFedoraEnable()) return "";

            var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                ods = DBAbstractDataLayer.DataAccessRules.FedoraRule.Current.GetAssociatedODs(nivelID, ho.Connection);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
            finally
            {
                ho.Dispose();
            }

            if (ods.Count > 0)
            {
                res.Append("Lista de objetos digitais associados: ");
                ods.ForEach(od => res.Append(System.Environment.NewLine + od));
            }

            return res.ToString();
        }

        public static List<GISADataset.ObjetoDigitalRow> GetObjetosDigitais(GISADataset.FRDBaseRow frdRow)
        {
            var odRows = new List<GISADataset.ObjetoDigitalRow>();

            //  - Obter os objetos digitais directamente associados
            odRows.AddRange(frdRow.GetSFRDImagemRows().Where(r => r.Tipo.Equals(FedoraHelper.typeFedora)).Select(r => r.GetSFRDImagemObjetoDigitalRows().Single().ObjetoDigitalRow));

            /*//  - Obter os objetos digitais dos subdocumentos caso o nivel selecionado seja um documento/processo
            if (frdRow.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First().IDTipoNivelRelacionado == TipoNivelRelacionado.D)
            {
                foreach (var rh in frdRow.NivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquicaUpper())
                {
                    var imgRow = rh.NivelRowByNivelRelacaoHierarquica.GetFRDBaseRows().Single().GetSFRDImagemRows().SingleOrDefault(r => r.Tipo.Equals(FedoraHelper.typeFedora));
                    if (imgRow == null) continue;
                    odRows.Add(imgRow.GetSFRDImagemObjetoDigitalRows().Single().ObjetoDigitalRow);
                };
            }*/
            return odRows;
        }

        public static void FixObjetoDigital(ref ObjDigComposto odComp, List<string> pidsParaApagar, GISADataset.FRDBaseRow currentFRDBase, ref GISADataset.ObjetoDigitalRow odSimplesRow, ref GISADataset.ObjetoDigitalRow odCompRow)
        {
            if (pidsParaApagar.Count == 0) return;

            var currentODComp = odComp;

            Trace.WriteLine(string.Format(">> Fixing OD composto {0}...", currentODComp.pid));

            currentODComp.original = currentODComp.Clone();
            currentODComp.state = State.modified;
            pidsParaApagar.ForEach(pid =>
            {
                var odSimples = currentODComp.objSimples.Single(simples => simples.pid.Equals(pid));
                currentODComp.objSimples.Remove(odSimples);
                Trace.WriteLine(string.Format(">> ODsimples {0} por apagar no composto {1}", pid, currentODComp.pid));
            });

            if (currentODComp.objSimples.Count < 2)
            {
                if (currentODComp.objSimples.Count == 1)
                {
                    var odSimples = currentODComp.objSimples.Single();
                    odSimples.original = odSimples.Clone();
                    odSimples.parentDocumentTitle = "";
                    odSimples.state = State.modified;
                    odSimplesRow = GisaDataSetHelper.GetInstance().ObjetoDigital.Cast<GISADataset.ObjetoDigitalRow>().Single(r => r.pid.Equals(odSimples.pid));
                    if (odSimplesRow.GetSFRDImagemObjetoDigitalRows().Length == 0)
                        FedoraHelper.RelateODtoUI(odSimples, odSimplesRow, currentFRDBase);
                }

                currentODComp.state = State.deleted;
                string msg = "";
                SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.Ingest(currentODComp, out msg);

                odCompRow = GisaDataSetHelper.GetInstance().ObjetoDigital.Cast<GISADataset.ObjetoDigitalRow>().Single(r => r.pid.Equals(currentODComp.pid));
                FedoraHelper.DeleteObjDigitalRows(odCompRow);
                Trace.WriteLine(string.Format(">> ODComposto {0} apagado por número insuficiente de simples.", currentODComp.pid));
                currentODComp = null;
                odCompRow = null;
                PersistencyHelper.save();
                PersistencyHelper.cleanDeletedData();
            }

            odComp = currentODComp;
        }

        public static void RelateODtoUI(ObjDigSimples odSimples, GISADataset.ObjetoDigitalRow odRow, GISADataset.FRDBaseRow frdBase)
        {
            var imgVolRow = FedoraHelper.GetRepositoryImagemVolumeRow();
            var imgRow = GisaDataSetHelper.GetInstance().SFRDImagem.NewSFRDImagemRow();
            imgRow.Identificador = "";
            imgRow.FRDBaseRow = frdBase;
            imgRow.Tipo = FedoraHelper.typeFedora;
            imgRow.Versao = new byte[] { };
            imgRow.isDeleted = 0;
            imgRow.GUIOrder = long.MaxValue;
            imgRow.Descricao = "";
            imgRow.SFRDImagemVolumeRow = imgVolRow;
            GisaDataSetHelper.GetInstance().SFRDImagem.AddSFRDImagemRow(imgRow);
            GisaDataSetHelper.GetInstance().SFRDImagemObjetoDigital.AddSFRDImagemObjetoDigitalRow(imgRow.IDFRDBase, imgRow.idx, odRow, new byte[] { }, 0);
        }

        #region Search
        private const int PAGE_SIZE = 100;

        public enum FedoraField
        {
            pid, format, creator, subject, description, publisher, contributor,
            date, type, title, source, language, relation, coverage, rights, label,
            identifier, state, ownerId, cDate, mDate, dcmDate
        };

        public class FedoraQuery
        {
            private FieldSearchQueryConditions conditions;
            private List<string> fields;

            public string[] Fields { get { return this.fields.ToArray(); } }
            public FieldSearchQuery Query { get; private set; }

            public FedoraQuery()
            {
                conditions = new FieldSearchQueryConditions();
                conditions.condition = new Condition[0];
                fields = new List<string>();
                Query = new FieldSearchQuery();
                Query.Item = conditions;
            }

            public void AddCondition(FedoraField property, ComparisonOperator op, string value)
            {
                Condition cond = new Condition();
                cond.@operator = op;
                cond.property = property.ToString();
                cond.value = value;

                List<Condition> currentCond = conditions.condition.ToList();
                currentCond.Add(cond);
                conditions.condition = currentCond.ToArray();
            }

            public void AddFields(params FedoraField[] newFields)
            {
                this.fields.AddRange(newFields.Where(f => !this.fields.Contains(f.ToString())).Select(f => f.ToString()));
            }
        }

        public class MonthlyResults
        {
            public List<string> created;
            public List<string> edited;

            public MonthlyResults()
            {
                created = new List<string>();
                edited = new List<string>();
            }

            public void Normalize()
            {
                created = created.Distinct().ToList();
                edited = edited.Distinct().ToList();
            }
        }

        public List<ObjectFields> Search(DateTime dataInicio, DateTime dataFim, out Dictionary<string, List<Historico>> histLst, bool includeDeleted)
        {
            var objsFinalList = new List<ObjectFields>();
            histLst = new Dictionary<string,List<Historico>>();

            FedoraQuery q = new FedoraQuery();
            q.AddFields(new FedoraField[] { FedoraField.pid, FedoraField.cDate, FedoraField.mDate, FedoraField.state });
            if (dataInicio > DateTime.MinValue)
                q.AddCondition(FedoraField.cDate, ComparisonOperator.ge, String.Format("{0}-{1}-{2}T00:00:00.000Z", dataInicio.Year, dataInicio.Month.ToString().PadLeft(2, '0'), dataInicio.Day.ToString().PadLeft(2, '0')));
            if (dataFim < DateTime.MaxValue)
                q.AddCondition(FedoraField.cDate, ComparisonOperator.le, String.Format("{0}-{1}-{2}T23:59:59.999Z", dataFim.Year, dataFim.Month.ToString().PadLeft(2, '0'), dataFim.Day.ToString().PadLeft(2, '0')));
            if (!includeDeleted)
                q.AddCondition(FedoraField.state, ComparisonOperator.eq, "A");

            try
            {
                var objs = fedoraConnect.Search(q.Fields, PAGE_SIZE, q.Query);
                Trace.WriteLine(">>>> objetos obtidos: " + objs.Count());
                var repNamespace = fedoraConnect.GetRepositoryInfo().repositoryPIDNamespace;

                long a = 0;
                foreach (ObjectFields obj in objs)
                {
                    if (obj.pid.StartsWith(repNamespace) && long.TryParse(obj.pid.Replace(repNamespace + ":", ""), out a))
                    {
                        histLst.Add(obj.pid, fedoraConnect.GetHistoric(obj.pid));
                        objsFinalList.Add(obj);
                    }
                }
                Trace.WriteLine(">>>> Detalhes obtidos: " + objsFinalList.Count());
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu ao obter os dados do repositório.", "Erro de comunicação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return objsFinalList;
        }
        #endregion
    }

    public class SubDocumento {
        public long id;
        public string designacao;
        public long guiorder;
        public GISADataset.NivelRow nRow;
    }
}
