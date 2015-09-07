using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using GISA.Controls;
using GISA.Model;
using GISA.SharedResources;

namespace GISA
{
	public class Relacao : System.Windows.Forms.UserControl
	{

	#region  Windows Form Designer generated code 

		public Relacao() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            cbTipoControloAutRel.SelectedIndexChanged += cbTipoControloAutRel_SelectedIndexChanged;
		}

		//UserControl overrides dispose to clean up the component list.
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
		protected internal System.Windows.Forms.Label lblTipoNivel;
		internal GISA.Controls.PxDateBox dtRelacaoFim;
		internal GISA.Controls.PxDateBox dtRelacaoInicio;
		protected internal System.Windows.Forms.Label lblTipocontroloAutRel;
		protected internal System.Windows.Forms.ComboBox cbTipoControloAutRel;
		internal System.Windows.Forms.GroupBox grpRelacao;
		protected internal GISA.Controls.PxComboBox cbTipoNivel;
		internal System.Windows.Forms.Label lblDataFim;
		internal System.Windows.Forms.TextBox txtDescricao;
		internal System.Windows.Forms.Label lblDataInicio;
		internal System.Windows.Forms.Label lblDescricao;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.grpRelacao = new System.Windows.Forms.GroupBox();
			this.lblTipoNivel = new System.Windows.Forms.Label();
			this.cbTipoNivel = new GISA.Controls.PxComboBox();
			this.dtRelacaoFim = new GISA.Controls.PxDateBox();
			this.lblDataFim = new System.Windows.Forms.Label();
			this.dtRelacaoInicio = new GISA.Controls.PxDateBox();
			this.lblTipocontroloAutRel = new System.Windows.Forms.Label();
			this.txtDescricao = new System.Windows.Forms.TextBox();
			this.cbTipoControloAutRel = new System.Windows.Forms.ComboBox();
			this.lblDataInicio = new System.Windows.Forms.Label();
			this.lblDescricao = new System.Windows.Forms.Label();
			this.grpRelacao.SuspendLayout();
			this.SuspendLayout();
			//
			//grpRelacao
			//
			this.grpRelacao.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.grpRelacao.Controls.Add(this.lblTipoNivel);
			this.grpRelacao.Controls.Add(this.cbTipoNivel);
			this.grpRelacao.Controls.Add(this.dtRelacaoFim);
			this.grpRelacao.Controls.Add(this.lblDataFim);
			this.grpRelacao.Controls.Add(this.dtRelacaoInicio);
			this.grpRelacao.Controls.Add(this.lblTipocontroloAutRel);
			this.grpRelacao.Controls.Add(this.txtDescricao);
			this.grpRelacao.Controls.Add(this.cbTipoControloAutRel);
			this.grpRelacao.Controls.Add(this.lblDataInicio);
			this.grpRelacao.Controls.Add(this.lblDescricao);
			this.grpRelacao.Location = new System.Drawing.Point(2, 2);
			this.grpRelacao.Name = "grpRelacao";
			this.grpRelacao.Size = new System.Drawing.Size(616, 156);
			this.grpRelacao.TabIndex = 12;
			this.grpRelacao.TabStop = false;
			this.grpRelacao.Text = "Caracterização da relação";
			//
			//lblTipoNivel
			//
			this.lblTipoNivel.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.lblTipoNivel.Location = new System.Drawing.Point(190, 17);
			this.lblTipoNivel.Name = "lblTipoNivel";
			this.lblTipoNivel.Size = new System.Drawing.Size(234, 16);
			this.lblTipoNivel.TabIndex = 21;
            this.lblTipoNivel.Text = "Tipo do nível subordinado";
			//
			//cbTipoNivel
			//
			this.cbTipoNivel.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.cbTipoNivel.BackColor = System.Drawing.SystemColors.Window;
			this.cbTipoNivel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbTipoNivel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTipoNivel.Location = new System.Drawing.Point(190, 33);
			this.cbTipoNivel.Name = "cbTipoNivel";
			this.cbTipoNivel.Size = new System.Drawing.Size(234, 21);
			this.cbTipoNivel.TabIndex = 2;
			//
			//dtRelacaoFim
			//
			this.dtRelacaoFim.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
			this.dtRelacaoFim.Location = new System.Drawing.Point(523, 33);
			this.dtRelacaoFim.Name = "dtRelacaoFim";
			this.dtRelacaoFim.Size = new System.Drawing.Size(82, 22);
			this.dtRelacaoFim.TabIndex = 4;
			this.dtRelacaoFim.ValueDay = "";
			this.dtRelacaoFim.ValueMonth = "";
			this.dtRelacaoFim.ValueYear = "";
			//
			//lblDataFim
			//
			this.lblDataFim.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
			this.lblDataFim.Location = new System.Drawing.Point(523, 17);
			this.lblDataFim.Name = "lblDataFim";
			this.lblDataFim.Size = new System.Drawing.Size(80, 16);
			this.lblDataFim.TabIndex = 18;
			this.lblDataFim.Text = "Data de fim";
			//
			//dtRelacaoInicio
			//
			this.dtRelacaoInicio.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
			this.dtRelacaoInicio.Location = new System.Drawing.Point(432, 33);
			this.dtRelacaoInicio.Name = "dtRelacaoInicio";
			this.dtRelacaoInicio.Size = new System.Drawing.Size(82, 22);
			this.dtRelacaoInicio.TabIndex = 3;
			this.dtRelacaoInicio.ValueDay = "";
			this.dtRelacaoInicio.ValueMonth = "";
			this.dtRelacaoInicio.ValueYear = "";
			//
			//lblTipocontroloAutRel
			//
			this.lblTipocontroloAutRel.Location = new System.Drawing.Point(10, 17);
			this.lblTipocontroloAutRel.Name = "lblTipocontroloAutRel";
			this.lblTipocontroloAutRel.Size = new System.Drawing.Size(165, 16);
			this.lblTipocontroloAutRel.TabIndex = 16;
			this.lblTipocontroloAutRel.Text = "Categoria";
			//
			//txtDescricao
			//
			this.txtDescricao.AcceptsReturn = true;
			this.txtDescricao.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.txtDescricao.Location = new System.Drawing.Point(10, 78);
			this.txtDescricao.MaxLength = 4000;
			this.txtDescricao.Multiline = true;
			this.txtDescricao.Name = "txtDescricao";
			this.txtDescricao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtDescricao.Size = new System.Drawing.Size(595, 70);
			this.txtDescricao.TabIndex = 5;
			this.txtDescricao.Text = "";
			//
			//cbTipoControloAutRel
			//
			this.cbTipoControloAutRel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTipoControloAutRel.Location = new System.Drawing.Point(10, 33);
			this.cbTipoControloAutRel.Name = "cbTipoControloAutRel";
			this.cbTipoControloAutRel.Size = new System.Drawing.Size(172, 21);
			this.cbTipoControloAutRel.TabIndex = 1;
			//
			//lblDataInicio
			//
			this.lblDataInicio.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
			this.lblDataInicio.Location = new System.Drawing.Point(432, 17);
			this.lblDataInicio.Name = "lblDataInicio";
			this.lblDataInicio.Size = new System.Drawing.Size(88, 16);
			this.lblDataInicio.TabIndex = 12;
			this.lblDataInicio.Text = "Data de início";
			//
			//lblDescricao
			//
			this.lblDescricao.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.lblDescricao.Location = new System.Drawing.Point(8, 62);
			this.lblDescricao.Name = "lblDescricao";
			this.lblDescricao.Size = new System.Drawing.Size(598, 16);
			this.lblDescricao.TabIndex = 11;
			this.lblDescricao.Text = "Descrição";
			//
			//Relacao
			//
			this.Controls.Add(this.grpRelacao);
			this.Name = "Relacao";
			this.Size = new System.Drawing.Size(620, 160);
			this.grpRelacao.ResumeLayout(false);
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

		private GISADataset.NivelRow mContextNivelRow = null;
		public GISADataset.NivelRow ContextNivelRow
		{
			get
			{
				return mContextNivelRow;
			}
			set
			{
				mContextNivelRow = value;
				PopulateControls();
			}
		}

		private GISADataset.TipoControloAutRelRow lastValidTcarRow = null;
		private ArrayList cachedPossibleSubitems = null;
		protected virtual void PopulateControls()
		{
			if (ContextNivelRow == null)
			{
				cbTipoNivel.DataSource = null;
				cbTipoControloAutRel.DataSource = null;
			}
			else
			{
				if (cachedPossibleSubitems != null)
				{
					cachedPossibleSubitems.Clear();
				}
				cachedPossibleSubitems = GetPossibleSubItems(ContextNivelRow);

				cbTipoNivel.ImageList = SharedResourcesOld.CurrentSharedResources.NiveisImageList;
				cbTipoNivel.ImageIndexes.Clear();
				foreach (TipoNivelRelacionado.PossibleSubNivel item in cachedPossibleSubitems)
				{
					cbTipoNivel.ImageIndexes.Add(SharedResourcesOld.CurrentSharedResources.NivelImageBase(System.Convert.ToInt32(item.SubIDTipoNivelRelacionado)));
				}

				cbTipoControloAutRel.SelectedIndexChanged -= cbTipoControloAutRel_SelectedIndexChanged;
				cbTipoControloAutRel.DataSource = GisaDataSetHelper.GetInstance().TipoControloAutRel.Select(string.Format("Thesaurus=0 AND NOT ID={0}", System.Enum.Format(typeof(TipoControloAutRel), TipoControloAutRel.Instituicao, "D")), "Designacao", DataViewRowState.CurrentRows);

				cbTipoControloAutRel.DisplayMember = "Designacao";
				cbTipoControloAutRel.SelectedIndexChanged += cbTipoControloAutRel_SelectedIndexChanged;
				if (lastValidTcarRow != null)
				{
					cbTipoControloAutRel.SelectedItem = lastValidTcarRow;
				}
			}

			UpdateControlsState();
		}

		protected virtual ArrayList GetPossibleSubItems(GISADataset.NivelRow nRow)
		{
			return TipoNivelRelacionado.GetPossibleSubItems(nRow);
		}

		private void UpdateControlsState()
		{
			if (ContextNivelRow == null)
			{
				cbTipoNivel.Enabled = false;
				return;
			}

			GISADataset.TipoControloAutRelRow tcarRow = null;
			tcarRow = (GISADataset.TipoControloAutRelRow)cbTipoControloAutRel.SelectedItem;

			// as datas de relação não fazem sentido nas relacoes temporais
			if (tcarRow.ID == Convert.ToInt64(TipoControloAutRel.Temporal))
			{
				dtRelacaoInicio.Enabled = false;
				dtRelacaoFim.Enabled = false;
			}
			else
			{
				dtRelacaoInicio.Enabled = true;
				dtRelacaoFim.Enabled = true;
			}

			// Desactivar escolha de tipo de nivel sempre que nao se tratar de uma relação hierarquica
			if (tcarRow.ID != Convert.ToInt64(TipoControloAutRel.Hierarquica))
			{
				cbTipoNivel.DataSource = null;
				cbTipoNivel.Enabled = false;
			}
			else
			{
				cbTipoNivel.DataSource = cachedPossibleSubitems;
				cbTipoNivel.DisplayMember = "DesignacaoComposta";
				if (this is RelacaoNivel)
				{
					cbTipoNivel.Enabled = false;
				}
				else
				{
					cbTipoNivel.Enabled = true;
				}
			}
		}

		private void cbTipoControloAutRel_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Cursor oldCur = this.Cursor;
			this.Cursor = Cursors.WaitCursor;
			try
			{
				if (cbTipoControloAutRel.SelectedItem != null)
				{
					lastValidTcarRow = (GISADataset.TipoControloAutRelRow)cbTipoControloAutRel.SelectedItem;
				}

				UpdateControlsState();
			}
			finally
			{
				this.Cursor = oldCur;
			}
		}
	}

} //end of root namespace