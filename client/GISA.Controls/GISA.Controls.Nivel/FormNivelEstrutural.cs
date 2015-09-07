using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using GISA.Controls.ControloAut;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA.Controls.Nivel
{
	public class FormNivelEstrutural : FormAddNivel
	{

	#region  Windows Form Designer generated code 

		public FormNivelEstrutural() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            caList.BeforeNewListSelection += caList_BeforeNewListSelection;
			caList.AllowedNoticiaAut(TipoNoticiaAut.EntidadeProdutora);
            //caList.ListHandler = new ControloAutList.DefaultCAListHandler(caList);
            caList.FilterVisible = true;
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
		public System.Windows.Forms.GroupBox grpControloAut;
        public ControloAutList caList;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.grpControloAut = new System.Windows.Forms.GroupBox();
            this.caList = new ControloAutList();
            this.grpTitulo.SuspendLayout();
            this.grpCodigo.SuspendLayout();
            this.grpControloAut.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(378, 20);
            // 
            // grpTitulo
            // 
            this.grpTitulo.Size = new System.Drawing.Size(618, 43);
            this.grpTitulo.Visible = false;
            // 
            // txtDesignacao
            // 
            this.txtDesignacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDesignacao.ReadOnly = true;
            this.txtDesignacao.Size = new System.Drawing.Size(604, 20);
            // 
            // grpCodigo
            // 
            this.grpCodigo.Size = new System.Drawing.Size(394, 43);
            this.grpCodigo.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(552, 334);
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.Location = new System.Drawing.Point(472, 334);
            // 
            // grpControloAut
            // 
            this.grpControloAut.Controls.Add(this.caList);
            this.grpControloAut.Location = new System.Drawing.Point(5, 5);
            this.grpControloAut.Name = "grpControloAut";
            this.grpControloAut.Size = new System.Drawing.Size(622, 320);
            this.grpControloAut.TabIndex = 3;
            this.grpControloAut.TabStop = false;
            this.grpControloAut.Text = "Notícia de autoridade";
            // 
            // caList
            // 
            this.caList.AllowedNoticiaAutLocked = false;
            this.caList.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.caList.ListHandler = null;
            this.caList.Location = new System.Drawing.Point(3, 16);
            this.caList.Name = "caList";
            //this.caList.originalLabel = "Notícias de autoridade encontradas";
            this.caList.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.caList.Size = new System.Drawing.Size(616, 301);
            this.caList.TabIndex = 13;
            this.caList.BeforeNewListSelection += new ControloAutList.BeforeNewListSelectionEventHandler(this.caList_BeforeNewListSelection);
            // 
            // FormNivelEstrutural
            // 
            this.ClientSize = new System.Drawing.Size(634, 365);
            this.Controls.Add(this.grpControloAut);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNivelEstrutural";
            this.Controls.SetChildIndex(this.grpCodigo, 0);
            this.Controls.SetChildIndex(this.grpTitulo, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnAccept, 0);
            this.Controls.SetChildIndex(this.grpControloAut, 0);
            this.grpTitulo.ResumeLayout(false);
            this.grpTitulo.PerformLayout();
            this.grpCodigo.ResumeLayout(false);
            this.grpCodigo.PerformLayout();
            this.grpControloAut.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                caList.ReloadList();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

		public override void LoadData()
		{
			caList.txtFiltroDesignacao.Text = txtDesignacao.Text;
			caList.ReloadList();
			if (caList.Items.Count > 0)
				caList.Items[0].Selected = true;
			else
				caList.txtFiltroDesignacao.Clear();
		}

		//TODO: FIXME: variavel dummy para manter compatibilidade. o objectivo é que desapareca
		public bool chkControloAut = true;

		private void caList_BeforeNewListSelection(object sender, BeforeNewSelectionEventArgs e)
		{
			if (e.ItemToBeSelected != null && e.ItemToBeSelected.Tag != null)
			{
				GISADataset.ControloAutDicionarioRow cadRow = null;
				cadRow = (GISADataset.ControloAutDicionarioRow)e.ItemToBeSelected.Tag;
				// se o termo encontrado não se tratar de uma forma 
				// autorizada é necessário obter a forma autorizada 
				// da notícia de autoridade respectiva. Se não existir 
				// em memória será necessário obte-la da base de dados.
				if (cadRow.IDTipoControloAutForma != Convert.ToInt64(TipoControloAutForma.FormaAutorizada))
				{
					GISADataset.ControloAutDicionarioRow cadRowAutorizado = null;
					cadRowAutorizado = GetTermoAutorizado(cadRow.ControloAutRow);
					if (cadRowAutorizado == null)
						cadRowAutorizado = GetTermoAutorizadoFromDB(cadRow.ControloAutRow);

					cadRow = cadRowAutorizado;
				}

				GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
				try
				{
					DBAbstractDataLayer.DataAccessRules.NivelRule.Current.LoadNivelByControloAut(cadRow.IDControloAut, GisaDataSetHelper.GetInstance(), ho.Connection);
				}
				finally
				{
					ho.Dispose();
				}

				this.txtDesignacao.Text = cadRow.DicionarioRow.Termo;

                GISADataset.NivelControloAutRow[] ncaRows = null;
                ncaRows = cadRow.ControloAutRow.GetNivelControloAutRows();
                if (ncaRows.Length != 0)
                    this.txtCodigo.Text = ncaRows[0].NivelRow.Codigo;
                else
                {
                    Debug.Assert(false, "Não foi encontrado o código de " + cadRow.DicionarioRow.Termo);
                    this.txtCodigo.Clear();
                }

			}
			else
			{
				this.txtDesignacao.Text = "";
				this.txtCodigo.Text = "";
			}
			this.UpdateButtonState();
		}

		// Obtem a forma autorizada de uma determinada notícia de autoridade,
		// se não for encontrada é devolvido "nothing"
		private GISADataset.ControloAutDicionarioRow GetTermoAutorizado(GISADataset.ControloAutRow caRow)
		{
			DataRow[] cadRowsAutorizados = null;
			cadRowsAutorizados = GisaDataSetHelper.GetInstance().ControloAutDicionario.Select("IDControloAut = " + caRow.ID.ToString() + " AND IDTipoControloAutForma = " + System.Enum.Format(typeof(TipoControloAutForma), TipoControloAutForma.FormaAutorizada, "D"));

			if (cadRowsAutorizados.Length == 1)
				return (GISADataset.ControloAutDicionarioRow)(cadRowsAutorizados[0]);
			else
				return null;
		}

		// Adiciona ao dataset interno a forma autorizada de uma determinada
		// notícia de autoridade devolvendo a row adicionanda
		private GISADataset.ControloAutDicionarioRow GetTermoAutorizadoFromDB(GISADataset.ControloAutRow caRow)
		{
			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try
			{
				ControloAutRule.Current.LoadFormaAutorizada(GisaDataSetHelper.GetInstance(), caRow.ID, System.Enum.Format(typeof(TipoControloAutForma), TipoControloAutForma.FormaAutorizada, "D"), ho.Connection);
			}
			finally
			{
				ho.Dispose();
			}

			return GetTermoAutorizado(caRow);
		}
	}

} //end of root namespace