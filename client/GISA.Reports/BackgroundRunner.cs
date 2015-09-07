using System;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

using GISA.Controls;

namespace GISA.Reports {
	/// <summary>
	/// Summary description for BackgroundRunner.
	/// </summary>
	public class BackgroundRunner {
		private Form progressDialog;
		private DoubleProgressBar progressBar;
		private Relatorio rel;
        public BackgroundRunner(Control TopLevelControl, Relatorio relatorio, long Ceiling)
        {
            if (relatorio.GetFileName != null)
            {
                progressBar = new DoubleProgressBar();
                progressBar.Dock = DockStyle.Fill;
                progressBar.Current = 0;
                progressBar.Maximum = 0;
                progressBar.Ceiling = Ceiling;
                progressDialog = new Form();
                progressDialog.Size = new Size(320, 82);
                progressDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                progressDialog.ControlBox = false;
                progressDialog.ShowInTaskbar = false;
                progressDialog.Text = "A gerar documento...";
                progressDialog.StartPosition = FormStartPosition.CenterParent;
                progressDialog.Controls.Add(progressBar);
                progressDialog.DockPadding.All = 16;
                this.rel = relatorio;
                Thread th = new Thread(new System.Threading.ThreadStart(this.RunReport));
                th.Start();
                progressDialog.ShowDialog(TopLevelControl);
            }
        }

        private delegate void CloseDialogHandler();
        private void CloseDialog() {
            progressDialog.Close();
        }

        private delegate void DoneEntriesHandler();
		private void RunReport() {
			try { 
				Thread.Sleep(1000);
				rel.AddedEntries += new AddedEntriesEventHandler(this.BackgroundRunner_AddedEntries);
				rel.RemovedEntries += new RemovedEntriesEventHandler(this.BackgroundRunner_RemovedEntries);
				try { 
					progressBar.StartAnimation();
					rel.GenerateRel();
				} finally { 
					progressBar.StopAnimation();
				}

                if (this.progressDialog.InvokeRequired)
                    progressDialog.BeginInvoke(new DoneEntriesHandler(DoneEntries));
                else
                    DoneEntries();

				Thread.Sleep(500);
			} finally {
                if (this.progressDialog.InvokeRequired)
                    progressBar.BeginInvoke(new CloseDialogHandler(CloseDialog));
                else
                    CloseDialog();
			}
		}

        private delegate void ManagePorgressBarEntriesHandler(int n);
        private void AddEntries(int n) {
            progressBar.Maximum += n;
        }

		private void BackgroundRunner_AddedEntries(int n) {
            if (this.progressBar.InvokeRequired)
                progressBar.BeginInvoke(new ManagePorgressBarEntriesHandler(AddEntries), new object[] {n});
            else
                AddEntries(n);
		}

        private void RemoveEntries(int n) {
            progressBar.Current += n;
        }

		private void BackgroundRunner_RemovedEntries(int n) {
            if (this.progressBar.InvokeRequired)
                progressBar.BeginInvoke(new ManagePorgressBarEntriesHandler(RemoveEntries), new object[] {n});
            else
                RemoveEntries(n);
		}

		private void DoneEntries() {
            if (progressBar.Maximum != progressBar.Current)
            {
                progressBar.Ceiling = progressBar.Maximum;
                progressBar.Current = progressBar.Ceiling;
            }
		}
	}
}
