using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using GISA.Model;
using GISA.Fedora.FedoraHandler;
using GISA.SharedResources;

namespace GISA
{
    public partial class FormFullScreenPdf : Form
    {
        private Quality preSelectedQuality;

        public FormFullScreenPdf(List<ListViewItem> itemsToDisplay, int selectedItemIndex, Quality quality)
        {
            InitializeComponent();
            this.preSelectedQuality = quality;

            lstView.Items.AddRange(itemsToDisplay.ToArray());
            if (selectedItemIndex != -1) lstView.Items[selectedItemIndex].Selected = true;

            this.previewControl.SetupOtherMode();
        }

        private void FormFullScreenPdf_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void ProcessSelectionChange()
        {
            this.Cursor = Cursors.WaitCursor;

            var items = lstView.SelectedItems;
            if (items.Count != 1) { previewControl.Clear(); this.Cursor = Cursors.Default; return; }
            else
            {
                if (items[0].Tag.GetType() == typeof(ObjDigSimples))
                {
                    var selectedItem = (ObjDigSimples)items[0].Tag;
                    if (selectedItem == null && FedoraHelper.HasObjDigReadPermission(selectedItem.pid)) { previewControl.Clear(); return; }
                    previewControl.ShowPDF(selectedItem.pid);
                }
                else if (items[0].Tag.GetType() == typeof(Anexo))
                {
                    previewControl.ShowAnexo(items[0].Tag as Anexo);
                }
                else if (items[0].Tag.GetType() == typeof(GISADataset.ObjetoDigitalRow)) 
                {
                    var selectedItem = (GISADataset.ObjetoDigitalRow)items[0].Tag;
                    previewControl.ShowPDF(selectedItem.pid);
                }
            }
            
            this.Cursor = Cursors.Default;
        }

        private void webBrowser_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void FormFullScreenPdf_Load(object sender, EventArgs e)
        {
            this.previewControl.Qualidade = FedoraHelper.TranslateQualityEnum(this.preSelectedQuality);
            lstView.Focus();
            if (lstView.Items.Count > 0)
                lstView.selectItem(lstView.Items[0]);
        }

        private void lstView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProcessSelectionChange();
        }

        private void FormFullScreenPdf_FormClosed(object sender, FormClosedEventArgs e)
        {
            ImageHelper.DeleteFilteredFiles("*.pdf");
            ImageHelper.DeleteFilteredFiles("*.jpg");
            ImageHelper.DeleteFilteredFiles("*.tmp");
        }

        protected override void WndProc(ref Message message)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch(message.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = message.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                    return;
                    break;
            }

            base.WndProc(ref message);
        }
    }
}
