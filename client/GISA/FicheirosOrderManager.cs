using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.SharedResources;

namespace GISA
{
    public partial class FicheirosOrderManager : ListViewOrderManager
    {
        public FicheirosOrderManager()
        {
            InitializeComponent();
            GetExtraResources();
        }

        private void GetExtraResources()
        {
            btnAdicionar.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
            btnRemover.Image = SharedResourcesOld.CurrentSharedResources.Apagar;

            CurrentToolTip.SetToolTip(btnAdicionar, SharedResourcesOld.CurrentSharedResources.AdicionarString);
            CurrentToolTip.SetToolTip(btnRemover, SharedResourcesOld.CurrentSharedResources.ApagarString);
        }

        public event EventHandler<EventArgs> AttachedFileDeleted;
        protected virtual void OnAttachedFileDeleted(EventArgs e)
        {
            if (this.AttachedFileDeleted != null)
                AttachedFileDeleted(this, e);
        }

        public event EventHandler<EventArgs> AttachedFileAdded;
        protected virtual void OnAttachedFileAdded(EventArgs e)
        {
            if (this.AttachedFileAdded != null)
                AttachedFileAdded(this, e);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string question = String.Format("{0} Pretende continuar?", lstVw.SelectedItems.Count > 1 ? "Os " + lstVw.SelectedItems.Count + " ficheiros selecionados serão removidos." : "O ficheiro selecionado será removido.");
            if(MessageBox.Show(question, "Remoção de ficheiro associado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                lstVw.SelectedItems.Cast<ListViewItem>().ToList().ForEach(item => item.Remove());
                this.updateToolBarButtons();
                this.OnAttachedFileDeleted(e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddUrlForm frm = new AddUrlForm();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (Fedora.FedoraHandler.Utility.RemoteFileExists(frm.URL)) { addItem(CreateNewItem(frm.URL)); this.OnAttachedFileAdded(e); }
                else
                {
                    MessageBox.Show("Não foi possível aceder ao URL inserido.", "URL Inacessível", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        public override void updateToolBarButtons()
        {
            base.updateToolBarButtons();
            btnAdicionar.Enabled = !readOnlyMode;
            btnRemover.Enabled = lstVw.SelectedItems.Count > 0 && !readOnlyMode;
        }

        private void lstVw_DragEnter(object sender, DragEventArgs e)
        {
            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
                e.Effect = e.Data.GetDataPresent(DataFormats.Html) && !readOnlyMode ? DragDropEffects.Copy : e.Effect = DragDropEffects.None;
        }

        private void lstVw_DragDrop(object sender, DragEventArgs e)
        {
            if (readOnlyMode) return;

            string s = (string)e.Data.GetData(DataFormats.Html);
            List<string> urls = new List<string>();

            string sourceUrl = GetSourceURL(s);
            if (sourceUrl != null)
            {
                WebBrowser wb = new WebBrowser();
                wb.DocumentText = s;
                do { Application.DoEvents(); } while (wb.ReadyState != WebBrowserReadyState.Complete);
                urls.AddRange(ExtractUrls(wb.Document.Links, "href", sourceUrl));
                urls.AddRange(ExtractUrls(wb.Document.Images, "src", sourceUrl));
                wb.Dispose();
            }

            var items = new List<ListViewItem>();
            items.AddRange(urls.Select(url => CreateNewItem(url)));
            populateItems(items);

            if (urls.Count > 0)
                this.OnAttachedFileDeleted(e);

            updateToolBarButtons();
        }

        private ListViewItem CreateNewItem(string url)
        {
            Fedora.FedoraHandler.Anexo anexoNovo = new Fedora.FedoraHandler.Anexo();
            anexoNovo.url = url;
            bool foundMimeType;
            anexoNovo.mimeType = Fedora.FedoraHandler.Utility.GetMIME(url, out foundMimeType);
            ListViewItem newItem = CreateItem(url, anexoNovo);
            newItem.Font = PermissoesHelper.fontItalic;
            return newItem;
        }

        private List<string> ExtractUrls(HtmlElementCollection elements, string tag, string baseUrl)
        {
            List<string> urls = new List<string>();
            foreach (HtmlElement element in elements)
            {
                try
                {
                    string url = element.GetAttribute(tag);
                    if (!url.Contains(baseUrl)) url = baseUrl + url;
                    url = url.Replace("about:", "");      // BIG HACK - se for IE, diz ao IE que vá dar uma curva... :(
                    Uri temp;
                    if (Uri.TryCreate(url, UriKind.Absolute, out temp) && !urls.Contains(url)) urls.Add(url);
                }
                catch { }
            }
            return urls;
        }

        private string GetSourceURL(string code)
        {
            try
            {
                int startIndex = code.IndexOf("SourceURL:") + 10;
                string originUrl = code.Substring(startIndex);
                int endIndex = originUrl.IndexOf("\r");
                originUrl = originUrl.Substring(0, endIndex);
                return originUrl;
            }
            catch { return null; }
        }

        public override void updateItemCounter()
        {
            if (lstVw.Items.Count > 0) groupBox1.Text = String.Format("{0} ({1})", "Ficheiros", lstVw.Items.Count);
            else groupBox1.Text = "Ficheiros";
        }

        private void lstVw_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateToolBarButtons();
        }
    }
}
