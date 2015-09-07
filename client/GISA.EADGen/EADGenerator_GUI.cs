using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using GISA.Controls;
using GISA.Model;

using DBAbstractDataLayer.DataAccessRules;

namespace GISA.EADGen {
    public class EADGenerator_GUI {

        private SaveFileDialog mSaveDialog;
        
        private Form progressDialog;
        private DoubleProgressBar progressBar;

        private EADGenerator the_EADGenerator;
        private long IDNivel_PAI;
        private long IDNivel;

        public EADGenerator_GUI() {
			mSaveDialog = new SaveFileDialog();
			mSaveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			mSaveDialog.AddExtension = true;
			mSaveDialog.DefaultExt = "xml";
            mSaveDialog.Filter = "EXtended Markup Language (*.xml)|*.xml";
			mSaveDialog.OverwritePrompt = true;
			mSaveDialog.ValidateNames = true;
		}

        protected string FileNameSelection(string FileName) {
            mSaveDialog.FileName = FileName;
            switch (mSaveDialog.ShowDialog()) {
                case DialogResult.OK:
                    mSaveDialog.InitialDirectory = new System.IO.FileInfo(mSaveDialog.FileName).Directory.ToString();
                    return mSaveDialog.FileName;
                case DialogResult.Cancel:
                    return null;
            }
            return null;
        }

        public void launch_EAD_generator(IDbConnection connection, long IDNivel_PAI, long IDNivel, string FileName, Control TopLevelControl) {
            string mFileName = FileNameSelection(FileName);
            if (mFileName != null && !mFileName.Equals("")) {

                long ceiling = (long)(EADGeneratorRule.Current.get_Count_All_NiveisDescendentes(IDNivel, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID, connection) / 2);

                progressBar = new DoubleProgressBar();
                progressBar.Dock = DockStyle.Fill;
                progressBar.ShowCurrent = false;
                progressBar.Current = 0;
                progressBar.Maximum = 0;
                progressBar.Ceiling = (ceiling > 0 ? ceiling : 1);

                progressDialog = new Form();
                progressDialog.Size = new Size(320, 82);
                progressDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                progressDialog.ControlBox = false;
                progressDialog.ShowInTaskbar = false;
                progressDialog.Text = "Geração de EAD:" + mFileName + "...";
                progressDialog.StartPosition = FormStartPosition.CenterParent;
                progressDialog.Controls.Add(progressBar);
                progressDialog.DockPadding.All = 16;

                this.IDNivel_PAI = IDNivel_PAI;
                this.IDNivel = IDNivel;
                this.the_EADGenerator = new EADGenerator(mFileName, connection);

                Thread th = new Thread(new System.Threading.ThreadStart(this.EAD_Runner));
                th.Start();
                progressDialog.ShowDialog(TopLevelControl);
            }
        }

        private void EAD_Runner() {
            try {
                Thread.Sleep(1000);
                the_EADGenerator.AddedEntries += new AddedEntriesEventHandler(this.BackgroundRunner_AddedEntries);
                the_EADGenerator.RemovedEntries += new RemovedEntriesEventHandler(this.BackgroundRunner_RemovedEntries);
                try {
                    progressBar.StartAnimation();
                    the_EADGenerator.generate(IDNivel_PAI, IDNivel);
                }
                finally {
                    progressBar.StopAnimation();
                }

                if (this.progressDialog.InvokeRequired)
                    progressDialog.BeginInvoke(new DoneEntriesHandler(DoneEntries));
                else
                    DoneEntries();

                Thread.Sleep(500);
            }
            finally {
                if (this.progressDialog.InvokeRequired)
                    progressBar.BeginInvoke(new CloseDialogHandler(CloseDialog));
                else
                    CloseDialog();
            }
        }

        private delegate void DoneEntriesHandler();
        private delegate void CloseDialogHandler();
        private void CloseDialog() {
            progressDialog.Close();
        }

        private void DoneEntries() {
            if (progressBar.Maximum != progressBar.Current) {
                progressBar.Ceiling = progressBar.Maximum;
                progressBar.Current = progressBar.Ceiling;
            }
        }

        private delegate void ManagePorgressBarEntriesHandler(int n);
        private void AddEntries(int n) {
            progressBar.Maximum += n;
        }
        private void BackgroundRunner_AddedEntries(int n) {
            if (this.progressBar.InvokeRequired)
                progressBar.BeginInvoke(new ManagePorgressBarEntriesHandler(AddEntries), new object[] { n });
            else
                AddEntries(n);
        }

        private void BackgroundRunner_RemovedEntries(int n) {
            if (this.progressBar.InvokeRequired)
                progressBar.BeginInvoke(new ManagePorgressBarEntriesHandler(RemoveEntries), new object[] { n });
            else
                RemoveEntries(n);
        }
        private void RemoveEntries(int n) {
            progressBar.Current -= n;
        }
    }
}
