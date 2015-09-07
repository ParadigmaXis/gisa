using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;
using GISA.Model;
using GISA.SharedResources;

namespace GISA
{
    public partial class FormPickTítulo : Form
    {
        public FormPickTítulo()
        {
            InitializeComponent();
            GetExtraResources();

            ToolBar1.ButtonClick += ToolBarButtonClickEvent;
            txtFiltroDesignacao.KeyDown += txtFiltroDesignacao_KeyDown;

            lstTitulos.DisplayMember = GisaDataSetHelper.GetInstance().ObjetoDigitalTitulo.TituloColumn.ColumnName;
        }

        private void GetExtraResources()
        {
            ToolBar1.ImageList = SharedResourcesOld.CurrentSharedResources.DMManipulacaoImageList;

            string[] strs = SharedResourcesOld.CurrentSharedResources.ODTituloManipulacaoStrings;
            ToolBarButtonNew.ToolTipText = strs[0];
            ToolBarButtonEdit.ToolTipText = strs[1];
            ToolBarButtonDelete.ToolTipText = strs[2];
        }

        public string SelectedTitulo { get; set; }

        public void LoadData()
        {
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                FedoraRule.Current.LoadTitulos(GisaDataSetHelper.GetInstance(), ho.Connection);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                throw ex;
            }
            finally
            {
                ho.Dispose();
            }

            PopulateList(FilterTitle("",false));
        }

        public void PopulateList(List<GISADataset.ObjetoDigitalTituloRow> titulos)
        {
            lstTitulos.DataSource = titulos;
            lstTitulos.DisplayMember = GisaDataSetHelper.GetInstance().ObjetoDigitalTitulo.TituloColumn.ColumnName;

            UpdateToolbarButtons();
        }

        public void RefreshList(string selectItem)
        {
            if (selectItem.Length > 0)
            {
                var res = SearchTitle(txtFiltroDesignacao.Text);
                if (res.SingleOrDefault(r => r.Titulo.Equals(selectItem)) == null)
                {
                    MessageBox.Show("O campo de filtragem vai ser limpo " + System.Environment.NewLine + "para ser possível visualizar o novo título.", "Filtro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFiltroDesignacao.Text = "";
                    PopulateList(FilterTitle("", false));
                }
                else
                    PopulateList(res);
            }
            else
                PopulateList(FilterTitle("", false));

        }

        private void ToolBarButtonClickEvent(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button == ToolBarButtonNew)
            {
                var frmNewTitle = new FormObjetoDigitalTitulo();
                frmNewTitle.SetNewTitle();
                var newTitle = "";

                if (frmNewTitle.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    newTitle = frmNewTitle.Titulo;

                var matchNewTitle = FilterTitle(newTitle, false);
                if (matchNewTitle.Count > 0)
                {
                    lstTitulos.SelectedItem = matchNewTitle[0];
                    return;
                }

                matchNewTitle = FilterTitle(newTitle, true);
                if (matchNewTitle.Count > 0)
                {
                    var item = matchNewTitle[0];
                    Debug.Assert(item.RowState == DataRowState.Deleted);
                    item.RejectChanges();
                    lstTitulos.SelectedItem = item;
                    return;
                }

                var newTitleRow = GisaDataSetHelper.GetInstance().ObjetoDigitalTitulo.AddObjetoDigitalTituloRow(newTitle, new byte[] { }, 0);
                RefreshList(newTitleRow.Titulo);
                lstTitulos.SelectedItem = newTitleRow;
            }
            else if (e.Button == ToolBarButtonEdit)
            {
                Debug.Assert(lstTitulos.SelectedItems.Count == 1);

                var selectedTitle = lstTitulos.SelectedItems[0] as GISADataset.ObjetoDigitalTituloRow;

                var frmNewTitle = new FormObjetoDigitalTitulo();
                frmNewTitle.SetEditTitle();
                frmNewTitle.Titulo = selectedTitle.Titulo;

                var newTitle = "";
                if (frmNewTitle.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    newTitle = frmNewTitle.Titulo;

                var matchNewTitle = FilterTitle(newTitle, false);
                if (matchNewTitle.Count > 0)
                {
                    lstTitulos.SelectedItem = matchNewTitle[0];
                    return;
                }

                matchNewTitle = FilterTitle(newTitle, true);
                if (matchNewTitle.Count > 0)
                {
                    var item = matchNewTitle[0];
                    Debug.Assert(item.RowState == DataRowState.Deleted);
                    item.RejectChanges();
                    lstTitulos.Items.Add(item);
                    lstTitulos.SelectedItem = item;
                    return;
                }

                selectedTitle.Titulo = newTitle;
                RefreshList(selectedTitle.Titulo);
                lstTitulos.SelectedItem = selectedTitle;
            }
            else if (e.Button == ToolBarButtonDelete)
            {
                Debug.Assert(lstTitulos.SelectedItems.Count == 1);

                if (MessageBox.Show("Tem a certeza que pretende apagar o título selecionado?", "Apagar título", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No) return;

                var item = lstTitulos.SelectedItems[0] as GISADataset.ObjetoDigitalTituloRow;
                item.Delete();
                RefreshList("");
            }
            else
            {
                Debug.Assert(false, "Unexpected button clicked in ToolBar.");
            }

            UpdateToolbarButtons();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            var filter = txtFiltroDesignacao.Text.Trim();
            var filterResult = SearchTitle(filter);

            PopulateList(filterResult);
        }

        private void txtFiltroDesignacao_KeyDown(object Sender, KeyEventArgs e)
        {
            if (e.KeyValue == Convert.ToInt32(Keys.Enter))
            {
                var filter = txtFiltroDesignacao.Text.Trim();
                var filterResult = SearchTitle(filter);

                PopulateList(filterResult);
            }
        }

        private static List<GISADataset.ObjetoDigitalTituloRow> SearchTitle(string filter)
        {
            var tbl_ObjetoDigitalTitulo = GisaDataSetHelper.GetInstance().ObjetoDigitalTitulo;
            try
            {
                filter = filter.Trim();
                if (filter.Length == 0)
                    return tbl_ObjetoDigitalTitulo.Rows.Cast<GISADataset.ObjetoDigitalTituloRow>().Where(r => r.RowState != DataRowState.Deleted).OrderBy(r => r.Titulo).ToList();
                else
                {
                    /*var ids = new List<long>();
                    GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                    try
                    {
                        filter = DBAbstractDataLayer.DataAccessRules.PesquisaRule.Current.buildLikeStatement("Titulo", "'" + filter + "'");
                        ids = FedoraRule.Current.GetTitulos(GisaDataSetHelper.GetInstance(), filter, ho.Connection);
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex.ToString());
                        throw ex;
                    }
                    finally
                    {
                        ho.Dispose();
                    }

                    return ids.Select(id => tbl_ObjetoDigitalTitulo.Single(r => r.ID == id)).ToList();*/

                    filter = filter.Replace("*", "[*]");
                    var query = string.Empty;
                    if (filter.Contains('%') && !filter.StartsWith("%") && !filter.EndsWith("%"))
                    {
                        var filterParts = filter.Split('%');
                        if (filterParts.Length > 2)
                        {
                            var str = new StringBuilder(tbl_ObjetoDigitalTitulo.TituloColumn.ColumnName + " like '" + filterParts[0] + "%' AND ");
                            for (int i = 0; i < filterParts.Length - 2; i++)
                                str.Append(tbl_ObjetoDigitalTitulo.TituloColumn.ColumnName + " like '%" + filterParts[i] + "%' AND ");
                            str.Append(tbl_ObjetoDigitalTitulo.TituloColumn.ColumnName + " like '%" + filterParts[filterParts.Length - 1] + "'");
                            query = str.ToString();
                        }
                        else
                        {
                            query = tbl_ObjetoDigitalTitulo.TituloColumn.ColumnName + " like '" + filterParts[0] + "%' AND " +
                                tbl_ObjetoDigitalTitulo.TituloColumn.ColumnName + " like '%" + filterParts[1] + "'";
                        }
                    }
                    else
                        query = tbl_ObjetoDigitalTitulo.TituloColumn.ColumnName + " like '" + filter + "'";

                    tbl_ObjetoDigitalTitulo.CaseSensitive = false;
                    return tbl_ObjetoDigitalTitulo.Select(query, "").Cast<GISADataset.ObjetoDigitalTituloRow>().OrderBy(r => r.Titulo).ToList();
                }
            }
            finally
            {
                tbl_ObjetoDigitalTitulo.CaseSensitive = true;
            }
        }

        private static List<GISADataset.ObjetoDigitalTituloRow> FilterTitle(string filter, bool searchOnDeletedRows)
        {
            filter = filter.Trim();
            var tbl_ObjetoDigitalTitulo = GisaDataSetHelper.GetInstance().ObjetoDigitalTitulo;
            if (filter.Length == 0)
                return tbl_ObjetoDigitalTitulo.Rows.Cast<GISADataset.ObjetoDigitalTituloRow>().Where(r => r.RowState != DataRowState.Deleted).OrderBy(r => r.Titulo).ToList();
            else
            {
                var query = tbl_ObjetoDigitalTitulo.TituloColumn.ColumnName + " = '" + filter + "'";
                if (searchOnDeletedRows)
                    return tbl_ObjetoDigitalTitulo.Select(query, "", DataViewRowState.Deleted).Cast<GISADataset.ObjetoDigitalTituloRow>().OrderBy(r => r.Titulo).ToList();
                else
                    return tbl_ObjetoDigitalTitulo.Select(query).Cast<GISADataset.ObjetoDigitalTituloRow>().OrderBy(r => r.Titulo).ToList();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SelectedTitulo = lstTitulos.SelectedItem != null ? ((GISADataset.ObjetoDigitalTituloRow)lstTitulos.SelectedItem).Titulo : "";
        }

        private void lstTitulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateToolbarButtons();
        }

        private void UpdateToolbarButtons()
        {
            ToolBarButtonNew.Enabled = true;
            ToolBarButtonEdit.Enabled = lstTitulos.SelectedItems.Count == 1;
            ToolBarButtonDelete.Enabled = lstTitulos.SelectedItems.Count == 1;
            btnOk.Enabled = lstTitulos.SelectedItems.Count == 1;
        }

        private void FormPickTítulo_FormClosing(object sender, FormClosingEventArgs e)
        {
            PersistencyHelper.SimpleSave(GisaDataSetHelper.GetInstance().ObjetoDigitalTitulo, GisaDataSetHelper.GetInstance().ObjetoDigitalTitulo.Cast<GISADataset.ObjetoDigitalTituloRow>().ToArray());
        }
    }
}
