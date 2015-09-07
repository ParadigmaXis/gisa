using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.Fedora.FedoraHandler;

namespace GISA
{
    public partial class ControlObjectoDigitalVersao : UserControl
    {
        private ObjDigSimples objSimples { get; set; }
        
        public delegate void OnVersionChange(object sender, int newVersion);
        public event OnVersionChange VersionChanged;

        public ControlObjectoDigitalVersao()
        {
            InitializeComponent();
        }

        public void Load(ObjDigSimples objecto)
        {
            this.objSimples = objecto;

            if (objSimples != null)
            {
                versionBar.Minimum = 0;
                versionBar.Maximum = Math.Max(objSimples.historico.Count - 1, 0);
                versionBar.Value = 0;
                UpdateVersionLabels(false);
                VersionChanged(this, versionBar.Value);
                this.Enabled = true;
            }
            else this.Enabled = false;
        }

        public void Reset()
        {
            versionBox.Text = "Versão";
            versionTimestampLabel.Text = String.Empty;
            versionBar.Minimum = 0;
            versionBar.Maximum = 0;
            versionBar.Value = 0;
        }

        private void UpdateVersionLabels(bool showToolTip)
        {
            Historico entry = objSimples.historico[versionBar.Value];
            versionBox.Text = String.Format("Versão #{0}{1}", versionBar.Maximum - versionBar.Value + 1, String.IsNullOrEmpty(entry.ToString()) ? "" : " - " + entry.ToString());
            CurrentToolTip.SetToolTip(versionBox, entry.FullDescription);
            versionTimestampLabel.Text = entry.Timestamp;

            if (showToolTip)
                CurrentToolTip.SetToolTip(versionBar, entry.FullDescription);
        }

        private void versionBar_ValueChanged(object sender, EventArgs e)
        {
            UpdateVersionLabels(true);
        }

        private void versionBar_MouseCaptureChanged(object sender, EventArgs e)
        {
            if (versionBar.Maximum > 0) VersionChanged(this, versionBar.Value);
        }
    }
}
