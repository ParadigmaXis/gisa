using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Collections.Generic;

using GISA.Model;
using GISA.SharedResources;
using DBAbstractDataLayer.DataAccessRules;

using GISA.Controls;

namespace GISA
{
	public class PesquisaList
#if DEBUG
 : MiddleClass
#else  
 : PaginatedListView 
#endif
	{
        private PaginatedLVGetItems returnedInfo;

	#region  Windows Form Designer generated code 

		public PesquisaList() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            this.GrpResultadosLabel = "Séries e Documentos";

            if (!SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsReqEnable())
                this.lstVwPaginated.Columns.Remove(this.chRequisitado);

            if (!SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsLicObrEnable())
            {
                this.lstVwPaginated.Columns.Remove(this.chRequerentesIniciais);
                this.lstVwPaginated.Columns.Remove(this.chLocObraNumPoliciaAct);
                this.lstVwPaginated.Columns.Remove(this.chLocObraDesignacaoAct);
                this.lstVwPaginated.Columns.Remove(this.chTipoObra);
            }

            lstVwPaginated.CustomizedSorting = true;

            GetExtraResources();
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
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
        private ColumnHeader colCodReferencia;
        private ColumnHeader colNivelDesc;
        private ColumnHeader colDesignacao;
        private ColumnHeader colDatasProducao;
        private ColumnHeader chCodigo;
        private ColumnHeader chTipoNivel;
        private ColumnHeader chDesignacao;
        private ColumnHeader chDataInicio;
		private ColumnHeader chID;
        private ColumnHeader chRequisitado;
        private ColumnHeader chDataFim;
        private ColumnHeader chAgrupador;
        private ColumnHeader chRequerentesIniciais;
        private ColumnHeader chLocObraNumPoliciaAct;
        private ColumnHeader chLocObraDesignacaoAct;
        private ColumnHeader chTipoObra;
    
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.chID = new System.Windows.Forms.ColumnHeader();
            this.chCodigo = new System.Windows.Forms.ColumnHeader();
            this.chTipoNivel = new System.Windows.Forms.ColumnHeader();
            this.chDesignacao = new System.Windows.Forms.ColumnHeader();
            this.chDataInicio = new System.Windows.Forms.ColumnHeader();
            this.chDataFim = new System.Windows.Forms.ColumnHeader();
            this.chRequisitado = new System.Windows.Forms.ColumnHeader();
            this.chAgrupador = new System.Windows.Forms.ColumnHeader();
            this.chRequerentesIniciais = new System.Windows.Forms.ColumnHeader();
            this.chLocObraDesignacaoAct = new System.Windows.Forms.ColumnHeader();
            this.chLocObraNumPoliciaAct = new System.Windows.Forms.ColumnHeader();
            this.chTipoObra = new System.Windows.Forms.ColumnHeader();
            this.colCodReferencia = new System.Windows.Forms.ColumnHeader();
            this.colNivelDesc = new System.Windows.Forms.ColumnHeader();
            this.colDesignacao = new System.Windows.Forms.ColumnHeader();
            this.colDatasProducao = new System.Windows.Forms.ColumnHeader();
            this.grpResultados.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // lstVwPesquisa
            // 
            this.lstVwPaginated.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chID,
            this.chCodigo,
            this.chTipoNivel,
            this.chDesignacao,
            this.chDataInicio,
            this.chDataFim,
            this.chRequisitado,
            this.chAgrupador,
            this.chRequerentesIniciais,
            this.chLocObraDesignacaoAct,
            this.chLocObraNumPoliciaAct,
            this.chTipoObra});
            // 
            // columnHeaderID
            // 
            this.chID.Text = "Identificador";
            this.chID.Width = 80;
            // 
            // columnHeaderCodigo
            // 
            this.chCodigo.Text = "Código referência";
            this.chCodigo.Width = 163;
            // 
            // columnHeaderTipoNivel
            // 
            this.chTipoNivel.Text = "Nível de descrição";
            this.chTipoNivel.Width = 104;
            // 
            // columnHeaderDesignacao
            // 
            this.chDesignacao.Text = "Título";
            this.chDesignacao.Width = 251;
            // 
            // columnHeaderDataInicio
            // 
            this.chDataInicio.Text = "Data de produção início";
            this.chDataInicio.Width = 137;
            // 
            // columnHeaderDataFim
            // 
            this.chDataFim.Text = "Data de produção fim";
            this.chDataFim.Width = 137;
            // 
            // chRequisitado
            // 
            this.chRequisitado.Text = "Requisitado";
            this.chRequisitado.Width = 70;
            // 
            // chAgrupador
            // 
            this.chAgrupador.Text = "Agrupador";
            this.chAgrupador.Width = 300;
            // 
            // chRequerentesIniciais
            // 
            this.chRequerentesIniciais.Text = "Requerentes iniciais";
            this.chRequerentesIniciais.Width = 100;
            // 
            // chLocObraDesignacaoAct
            // 
            this.chLocObraDesignacaoAct.Text = "Localização da obra (atual)";
            this.chLocObraDesignacaoAct.Width = 100;
            // 
            // chLocObraNumPoliciaAct
            // 
            this.chLocObraNumPoliciaAct.Text = "Num. polícia (atual)";
            this.chLocObraNumPoliciaAct.Width = 100;
            // 
            // chTipoObra
            // 
            this.chTipoObra.Text = "Tipo de obra";
            this.chTipoObra.Width = 100;
            // 
            // colCodReferencia
            // 
            this.colCodReferencia.Text = "Cod. referencia";
            this.colCodReferencia.Width = 163;
            // 
            // colNivelDesc
            // 
            this.colNivelDesc.Text = "Nivel de descrição";
            this.colNivelDesc.Width = 104;
            // 
            // colDesignacao
            // 
            this.colDesignacao.Text = "Designação";
            this.colDesignacao.Width = 251;
            // 
            // colDatasProducao
            // 
            this.colDatasProducao.Text = "Datas de produção";
            this.colDatasProducao.Width = 137;
            // 
            // PesquisaList
            // 
            this.Name = "PesquisaList";
            this.Size = new System.Drawing.Size(620, 268);
            this.grpResultados.ResumeLayout(false);
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
            PesquisaRule.Current.CalculateOrderedItems(ordenacao, SearchServerIDs, IDNivelEstrutura, UserID, SoDocExpirados, NewSearch, out nrResults, connection);
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
            returnedInfo = new PaginatedLVGetItemsPesq(PesquisaRule.Current.GetItems(GisaDataSetHelper.GetInstance(), pageNr, itemsPerPage, connection));
		}

        protected override void DeleteTemporaryResults(IDbConnection connection)
		{
            PesquisaRule.Current.DeleteTemporaryResults(connection);
		}

        protected override void AddItemsToList()
		{
            List<ListViewItem> itemsToBeAdded = new List<ListViewItem>();
            if (returnedInfo.rowsInfo != null)
            {
                foreach (PesquisaRule.NivelDocumental pItem in returnedInfo.rowsInfo)
                {
                    GISADataset.TipoNivelRelacionadoRow tnrRow = (GISADataset.TipoNivelRelacionadoRow)(GisaDataSetHelper.GetInstance().TipoNivelRelacionado.Select("ID=" + pItem.IDTipoNivelRelacionado.ToString())[0]);
                    ListViewItem item = new ListViewItem();
                    item.Tag = GisaDataSetHelper.GetInstance().FRDBase.Select("ID=" + pItem.IDFRDBase.ToString())[0];
                    item.ImageIndex = SharedResourcesOld.CurrentSharedResources.NivelImageBase(System.Convert.ToInt32(tnrRow.GUIOrder));
                    item.StateImageIndex = 0;
                    item.SubItems.AddRange(new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                            string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 
                            string.Empty, string.Empty, string.Empty, string.Empty, string.Empty});
                    item.SubItems[this.chID.Index].Text = pItem.IDNivel.ToString();
                    item.SubItems[this.chCodigo.Index].Text = pItem.CodigoCompleto.ToString();
                    item.SubItems[this.chTipoNivel.Index].Text = tnrRow.Designacao;
                    item.SubItems[this.chDesignacao.Index].Text = pItem.Designacao;
                    item.SubItems[this.chDataInicio.Index].Text = GISA.Utils.GUIHelper.FormatDate(pItem.InicioAno, pItem.InicioMes, pItem.InicioDia, pItem.InicioAtribuida);
                    item.SubItems[this.chDataFim.Index].Text = GISA.Utils.GUIHelper.FormatDate(pItem.FimAno, pItem.FimMes, pItem.FimDia, pItem.FimAtribuida);

                    // requisições
                    if (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsReqEnable())
                    {
                        if (pItem.Requisitado)
                            item.SubItems[this.chRequisitado.Index].Text = "Sim";
                        else
                            item.SubItems[this.chRequisitado.Index].Text = "Não";
                    }
                    
                    // agrupador
                    item.SubItems[this.chAgrupador.Index].Text = pItem.Agrupador;

                    // licença de obra
                    if (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().IsLicObrEnable())
                    {
                        item.SubItems[this.chRequerentesIniciais.Index].Text = pItem.RequerentesIniciais;
                        item.SubItems[this.chLocObraNumPoliciaAct.Index].Text = pItem.LocObraNumPoliciaAct;
                        item.SubItems[this.chLocObraDesignacaoAct.Index].Text = pItem.LocObraDesignacaoAct;
                        item.SubItems[this.chTipoObra.Index].Text = pItem.TipoObra;
                    }

                    itemsToBeAdded.Add(item);
                }

                if (itemsToBeAdded.Count > 0)
                {
                    this.lstVwPaginated.BeginUpdate();
                    this.Items.AddRange(itemsToBeAdded.ToArray());
                    this.lstVwPaginated.EndUpdate();
                }
            }
		}

        public List<string> SearchServerIDs { get; set; }
        public long UserID { get; set; }
        public bool SoDocExpirados { get; set; }
        public bool NewSearch { get; set; }
        public long NrResults { get; set; }
        public Int64? IDNivelEstrutura { get; set; }

		public void ClearSearchResults()
		{
			this.ResetList();
			btnAnterior.Enabled = false;
			btnProximo.Enabled = false;
			txtNroPagina.Text = "";
			lstVwPaginated.ClearSort();
		}
	}
}