using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.Fedora.FedoraHandler;
using GISA.Model;
using System.Net;
using System.IO;

namespace GISA
{
    public partial class ControlFedoraPdfViewer : UserControl {
        private object objectoActual;
        private bool isDocumentLoaded;
        private Quality defaultQuality = Quality.Low;
        private enum ViewerMode { Pesquisa, Other }

        public string Qualidade { 
            get { return cmbQuality.SelectedItem != null ? cmbQuality.SelectedItem as string : null; }
            set { cmbQuality.SelectedItem = value; }
        }

        public ControlFedoraPdfViewer()
        {
            InitializeComponent();

            //GISADataset.GlobalConfigRow configRow = null;
            //var defaultQuality = Quality.Low;

            var configRow = GisaDataSetHelper.GetInstance().GlobalConfig.Cast<GISADataset.GlobalConfigRow>().Single();
            defaultQuality = configRow.IsQualidadeImagemNull() ? Quality.Low : FedoraHelper.TranslateQualityEnum(configRow.QualidadeImagem);
            foreach (Quality quality in Enum.GetValues(typeof(Quality)))
            {
                var qualityString = FedoraHelper.TranslateQualityEnum(quality);
                cmbQuality.Items.Add(qualityString);
                if (quality == defaultQuality)
                    cmbQuality.SelectedItem = qualityString;
            }

            objectoActual = null;
            isDocumentLoaded = false;
        }

        public void ShowPDF(string pid)
        {
            this.cmbQuality.Enabled = true;
            this.Cursor = Cursors.WaitCursor;
            this.objectoActual = pid;

            var qualidade = FedoraHelper.TranslateQualityEnum(cmbQuality.SelectedItem as string);
            bool success;
            string errorMessage;

            string fileName = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.GetDisseminatorByUrl(pid, qualidade, out success, out errorMessage);

            if (success)
            {
                errorLabel.Visible = false;

                Uri fileUri;
                if (Uri.TryCreate(@fileName, UriKind.RelativeOrAbsolute, out fileUri)) webBrowser.Navigate(fileUri);
                else { Clear(); }
            }
            else ShowError(errorMessage);

            // obter o nome (do ficheiro) da primeira imagem do objeto digital
            var od = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.LoadID(pid, null) as ObjDigSimples;
            if (od != null)
                lblFicheiro.Text = od.fich_associados.First().url.Split('/').Last();
            else
                lblFicheiro.Text = "n/d";

            this.Cursor = Cursors.Default;
        }

        public void ShowAnexo(Anexo anexo)
        {
            this.Cursor = Cursors.WaitCursor;
            this.objectoActual = anexo;
            this.cmbQuality.Enabled = false;

            string url;
            bool success;
            string errorMessage;
            if (anexo.isIngested) url = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.GetDatastream(anexo.pid, anexo.dataStreamID, out success, out errorMessage);
            else url = ImageHelper.getAndConvertImageResourceToPng(anexo.url, out success, out errorMessage);

            if (success)
            {
                errorLabel.Visible = false;

                Uri uri;
                if (Uri.TryCreate(url, UriKind.Absolute, out uri))
                {
                    string newPage = Fedora.FedoraHandler.Utility.GetResizableImgElement(url);

                    webBrowser.Navigate("");
                    if (webBrowser.ReadyState != WebBrowserReadyState.Uninitialized)
                    {
                        webBrowser.Document.OpenNew(false);
                        webBrowser.Document.Write(newPage);
                        webBrowser.Refresh();
                    }
                    isDocumentLoaded = true;
                }
                else { Clear(); }
            }
            else ShowError(errorMessage);

            this.Cursor = Cursors.Default;            
        }

        public void ShowError(string message)
        {
            errorLabel.Text = message;
            errorLabel.Visible = true;
        }

        public void Clear(bool preserveQualitySelected)
        {
            this.webBrowser.Navigate("");
            this.objectoActual = null;
            this.isDocumentLoaded = false;
            this.cmbQuality.Enabled = false;
            if (!preserveQualitySelected)
                this.cmbQuality.SelectedItem = FedoraHelper.TranslateQualityEnum(defaultQuality);
            errorLabel.Text = "";
            errorLabel.Visible = true;
            lblFicheiro.Text = "";
            this.Cursor = Cursors.Default;
        }

        public void Clear()
        {
            Clear(false);
        }

        private int lastSelectedIndex = -1;
        private void cmbQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lastSelectedIndex == cmbQuality.SelectedIndex) return;

            lastSelectedIndex = cmbQuality.SelectedIndex;
            if (objectoActual != null)
            {
                if (objectoActual.GetType() == typeof(Anexo)) ShowAnexo(objectoActual as Anexo);
                else if (objectoActual.GetType() == typeof(String) && FedoraHelper.HasObjDigReadPermission(objectoActual as string)) ShowPDF(objectoActual as string);
            }
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (objectoActual != null && objectoActual.GetType() == typeof(Anexo) && !isDocumentLoaded)
            {
                if (objectoActual == null) return;
                ShowAnexo(objectoActual as Anexo);
                isDocumentLoaded = true;
            }
        }

        public void SetupPesquisaMode()
        {
            this.groupFicheiro.Visible = true;
            this.splitContainer1.SplitterDistance = 148;
        }

        public void SetupOtherMode()
        {
            this.groupFicheiro.Visible = false;
            this.splitContainer1.SplitterDistance = 0;
        }
    }
}
