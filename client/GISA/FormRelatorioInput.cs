using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
	public class FormRelatorioInput : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public FormRelatorioInput() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            InitializeDateControls();
		}

		//Form overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components = null;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		internal System.Windows.Forms.Button btnOk;
        internal GISA.Controls.PxCompleteDateBox cdbDataInicio;
        internal Label lblDataProducaoInicio;
        internal Label lblDataProducaoFim;
        internal GISA.Controls.PxCompleteDateBox cdbDataFim;
		internal System.Windows.Forms.Button btnCancel;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cdbDataInicio = new GISA.Controls.PxCompleteDateBox();
            this.lblDataProducaoInicio = new System.Windows.Forms.Label();
            this.lblDataProducaoFim = new System.Windows.Forms.Label();
            this.cdbDataFim = new GISA.Controls.PxCompleteDateBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(64, 99);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Continuar";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(145, 99);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancelar";
            // 
            // cdbDataInicio
            // 
            this.cdbDataInicio.Checked = false;
            this.cdbDataInicio.Day = 1;
            this.cdbDataInicio.Location = new System.Drawing.Point(53, 22);
            this.cdbDataInicio.Month = 1;
            this.cdbDataInicio.Name = "cdbDataInicio";
            this.cdbDataInicio.Size = new System.Drawing.Size(167, 22);
            this.cdbDataInicio.TabIndex = 16;
            this.cdbDataInicio.Year = 1;
            // 
            // lblDataProducaoInicio
            // 
            this.lblDataProducaoInicio.Location = new System.Drawing.Point(12, 20);
            this.lblDataProducaoInicio.Name = "lblDataProducaoInicio";
            this.lblDataProducaoInicio.Size = new System.Drawing.Size(35, 24);
            this.lblDataProducaoInicio.TabIndex = 18;
            this.lblDataProducaoInicio.Text = "entre";
            this.lblDataProducaoInicio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDataProducaoFim
            // 
            this.lblDataProducaoFim.Location = new System.Drawing.Point(28, 44);
            this.lblDataProducaoFim.Name = "lblDataProducaoFim";
            this.lblDataProducaoFim.Size = new System.Drawing.Size(19, 24);
            this.lblDataProducaoFim.TabIndex = 19;
            this.lblDataProducaoFim.Text = "e";
            this.lblDataProducaoFim.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cdbDataFim
            // 
            this.cdbDataFim.Checked = false;
            this.cdbDataFim.Day = 1;
            this.cdbDataFim.Location = new System.Drawing.Point(53, 50);
            this.cdbDataFim.Month = 1;
            this.cdbDataFim.Name = "cdbDataFim";
            this.cdbDataFim.Size = new System.Drawing.Size(167, 22);
            this.cdbDataFim.TabIndex = 17;
            this.cdbDataFim.Year = 1;
            // 
            // FormRelatorioInput
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(232, 133);
            this.ControlBox = false;
            this.Controls.Add(this.cdbDataInicio);
            this.Controls.Add(this.lblDataProducaoInicio);
            this.Controls.Add(this.lblDataProducaoFim);
            this.Controls.Add(this.cdbDataFim);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRelatorioInput";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Aplicar filtro";
            this.ResumeLayout(false);

		}

	#endregion

        private void InitializeDateControls()
        {
            cdbDataInicio.Day = DateTime.Now.Day;
            cdbDataInicio.Month = DateTime.Now.Month;
            cdbDataInicio.Year = DateTime.Now.Year;

            cdbDataFim.Day = DateTime.Now.Day;
            cdbDataFim.Month = DateTime.Now.Month;
            cdbDataFim.Year = DateTime.Now.Year;
        }

        /**
         * Datas do filtro de datas:
         */
        private DateTime Build_dataInicio()
        {
            if (this.cdbDataInicio.Checked)
                return new DateTime(this.cdbDataInicio.Year, this.cdbDataInicio.Month, this.cdbDataInicio.Day);
            return DateTime.MinValue;
        }
        private DateTime Build_dataFim()
        {
            if (this.cdbDataFim.Checked)
                // adicionadas horas, minutos e segundos para que o filtro não exclua os acontecimentos ocorridos em data_fim
                return new DateTime(this.cdbDataFim.Year, this.cdbDataFim.Month, this.cdbDataFim.Day).AddHours(23).AddMinutes(59).AddSeconds(59);
            return DateTime.MinValue;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var report = new Reports.Movimentos.RelatorioTodosMovimentos(
                string.Format("RelatorioTodosMovimentos_{0}", System.DateTime.Now.ToString("yyyyMMdd")),
                new ArrayList() { Build_dataInicio(), Build_dataFim() },
                SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);

            long ceiling = 1;

            object o = new Reports.BackgroundRunner(TopLevelControl, report, ceiling);
        }

	}

} //end of root namespace