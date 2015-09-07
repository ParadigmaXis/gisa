using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using System.Text;
using GISA.SharedResources;

using GISA.Controls;

namespace GISA
{
	public class PesquisaUFList
#if DEBUG
 : MiddleClass
#else  
 : PaginatedListView 
#endif
	{
        PaginatedLVGetItems returnedInfo;

	#region  Windows Form Designer generated code 

		public PesquisaUFList() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
			lstVwPaginated.CustomizedSorting = true;
            this.grpFiltro.Visible = false;
            this.GrpResultadosLabel = "Unidades físicas";

			GetExtraResources();

            if (!SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsDepEnable())
                this.lstVwPaginated.Columns.Remove(this.chUFEliminada);
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
		private ColumnHeader chUFDPInicio;
        private ColumnHeader chUFCodigo;
        private ColumnHeader chUFDesignacao;
        private ColumnHeader chUFDPFim;
        private ColumnHeader chUFCota;
        private ColumnHeader chUFEliminada;
        private ColumnHeader chUFCodBarras;
        private ColumnHeader chUFGuiaIncorporacao;

		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.chUFDPInicio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUFCodigo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUFDesignacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUFDPFim = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUFCota = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUFGuiaIncorporacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUFEliminada = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUFCodBarras = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpResultados.SuspendLayout();
            this.grpFiltro.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpResultados
            // 
            this.grpResultados.Size = new System.Drawing.Size(608, 197);
            // 
            // txtNroPagina
            // 
            this.txtNroPagina.Location = new System.Drawing.Point(576, 64);
            // 
            // lstVwPaginated
            // 
            this.lstVwPaginated.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chUFCodigo,
            this.chUFDesignacao,
            this.chUFDPInicio,
            this.chUFDPFim,
            this.chUFCota,
            this.chUFGuiaIncorporacao,
            this.chUFEliminada,
            this.chUFCodBarras});
            this.lstVwPaginated.Size = new System.Drawing.Size(560, 173);
            // 
            // btnProximo
            // 
            this.btnProximo.Location = new System.Drawing.Point(576, 92);
            this.ToolTip.SetToolTip(this.btnProximo, "Página seguinte");
            // 
            // btnAnterior
            // 
            this.btnAnterior.Location = new System.Drawing.Point(576, 32);
            this.ToolTip.SetToolTip(this.btnAnterior, "Página anterior");
            // 
            // grpFiltro
            // 
            this.grpFiltro.Size = new System.Drawing.Size(608, 59);
            // 
            // btnAplicar
            // 
            this.btnAplicar.Location = new System.Drawing.Point(536, 21);
            // 
            // chUFDPInicio
            // 
            this.chUFDPInicio.Text = "Datas de Produção Início";
            this.chUFDPInicio.Width = 137;
            // 
            // chUFCodigo
            // 
            this.chUFCodigo.Text = "Código";
            this.chUFCodigo.Width = 200;
            // 
            // chUFDesignacao
            // 
            this.chUFDesignacao.Text = "Título";
            this.chUFDesignacao.Width = 375;
            // 
            // chUFDPFim
            // 
            this.chUFDPFim.Text = "Datas de Produção Fim";
            this.chUFDPFim.Width = 137;
            // 
            // chUFCota
            // 
            this.chUFCota.Text = "Cota";
            this.chUFCota.Width = 100;
            // 
            // chUFGuiaIncorporacao
            // 
            this.chUFGuiaIncorporacao.Text = "Guia de incorporação";
            this.chUFGuiaIncorporacao.Width = 300;
            // 
            // chUFEliminada
            // 
            this.chUFEliminada.Text = "Eliminada";
            this.chUFEliminada.Width = 80;
            // 
            // chUFCodBarras
            // 
            this.chUFCodBarras.Text = "Código de barras";
            this.chUFCodBarras.Width = 100;
            // 
            // PesquisaUFList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.FilterVisible = true;
            this.Name = "PesquisaUFList";
            this.Size = new System.Drawing.Size(620, 268);
            this.grpResultados.ResumeLayout(false);
            this.grpFiltro.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

		protected override void GetExtraResources()
		{
            base.GetExtraResources();
            lstVwPaginated.SmallImageList = TipoNivelRelacionado.GetImageList();
		}

        protected override void CalculateOrderedItems(IDbConnection connection)
		{
            ArrayList ordenacao = this.GetListSortDef();
            long nrResults = 0;
            PesquisaRule.Current.CalculateOrderedItemsUF(ordenacao, SearchServerIDs, operador, anoEdicaoInicio, mesEdicaoInicio, diaEdicaoInicio, anoEdicaoFim, mesEdicaoFim, diaEdicaoFim, IDNivel, assoc, NewSearch, out nrResults, connection);
            NrResults = nrResults;
		}

        protected override int CountPages(int itemsCountLimit, out int totalElementosCount, IDbConnection connection)
        {
            totalElementosCount = 0;
            return PesquisaRule.Current.CountPages(itemsCountLimit, connection);
        }

        protected override int GetPageForItemTag(object itemTag, int pageNr, IDbConnection connection)
        {
            return PesquisaRule.Current.GetPageForID(((GISADataset.FRDBaseRow)itemTag).ID, pageNr, connection);
        }

        protected override void GetItems(int pageNr, int itemsPerPage, IDbConnection connection)
		{
            returnedInfo = new PaginatedLVGetItemsPesq(PesquisaRule.Current.GetItemsUF(GisaDataSetHelper.GetInstance(), pageNr, itemsPerPage, TipoNivel.OUTRO, connection));
		}

        protected override void DeleteTemporaryResults(IDbConnection connection)
		{
            PesquisaRule.Current.DeleteTemporaryResultsUF(connection);
		}

        protected override void AddItemsToList()
		{
            ArrayList itemsToBeAdded = new ArrayList();
            if (returnedInfo.rowsInfo != null)
            {
                foreach (ArrayList rowInfo in returnedInfo.rowsInfo)
                {
                    GISADataset.TipoNivelRelacionadoRow tnrRow = (GISADataset.TipoNivelRelacionadoRow)(GisaDataSetHelper.GetInstance().TipoNivelRelacionado.Select("ID=" + TipoNivelRelacionado.UF.ToString())[0]);
                    ListViewItem item = new ListViewItem();
                    item.Tag = GisaDataSetHelper.GetInstance().Nivel.Select("ID=" + rowInfo[0].ToString())[0];
                    item.ImageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageBase(System.Convert.ToInt32(tnrRow.GUIOrder));
                    item.StateImageIndex = 0;
                    item.SubItems.AddRange(new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty });
                    item.SubItems[this.chUFCodigo.Index].Text = rowInfo[1].ToString();
                    item.SubItems[this.chUFDesignacao.Index].Text = rowInfo[2].ToString();
                    item.SubItems[this.chUFDPInicio.Index].Text = GISA.Utils.GUIHelper.FormatDate(rowInfo[3].ToString(), rowInfo[4].ToString(), rowInfo[5].ToString());
                    item.SubItems[this.chUFDPFim.Index].Text = GISA.Utils.GUIHelper.FormatDate(rowInfo[6].ToString(), rowInfo[7].ToString(), rowInfo[8].ToString());
                    item.SubItems[this.chUFCota.Index].Text = rowInfo[9].ToString();
                    item.SubItems[this.chUFGuiaIncorporacao.Index].Text = rowInfo[10].ToString();

                    // Em deposito:
                    if (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsDepEnable())
                    {
                        if ((bool)rowInfo[11])  // Eliminado ?
                        {
                            item.SubItems[this.chUFEliminada.Index].Text = rowInfo[13].ToString();
                            item.Font = new Font(item.Font, FontStyle.Strikeout);
                        }
                        else
                            item.SubItems[this.chUFEliminada.Index].Text = "Não";

                    }

                    // CodigoBarras:
                    item.SubItems[this.chUFCodBarras.Index].Text = rowInfo[12].ToString();
                    itemsToBeAdded.Add(item);

                }
                if (itemsToBeAdded.Count > 0)
                {
                    this.lstVwPaginated.BeginUpdate();
                    this.Items.AddRange((ListViewItem[])(itemsToBeAdded.ToArray(typeof(ListViewItem))));
                    this.lstVwPaginated.EndUpdate();
                }
            }
		}

        public List<string> SearchServerIDs { get; set; }
        public string operador { get; set; }
        public int anoEdicaoInicio { get; set; }
        public int mesEdicaoInicio { get; set; }
        public int diaEdicaoInicio { get; set; }
        public int anoEdicaoFim { get; set; }
        public int mesEdicaoFim { get; set; }
        public int diaEdicaoFim { get; set; }
        public long IDNivel { get; set; }
        public int assoc { get; set; }
        public bool NewSearch { get; set; }
        public long NrResults { get; set; }

		public void ClearSearchResults()
		{
			this.ResetList();
			btnAnterior.Enabled = false;
			btnProximo.Enabled = false;
			txtNroPagina.Text = "";
			this.lstVwPaginated.ClearSort();
		}
	}
}