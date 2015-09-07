using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;
using GISA.Model;
using GISA.SharedResources;
using GISA.Utils;

namespace GISA
{
    public partial class PanelDepUFEliminadas : GISA.GISAPanel
    {
        private GISADataset.DepositoRow CurrentDeposito;
        private GISADataset.AutoEliminacaoRow CurrentAutoEliminacao;
        private const string grpUFsAssociadasText = "Unidades físicas associadas ({0}); Largura total: {1:0.000}m";
        decimal larguraTotal = 0;

        public PanelDepUFEliminadas()
        {
            InitializeComponent();
        }

        private void PanelDepUFEliminadas_ParentChanged(object sender, System.EventArgs e)
        {
            base.ParentChanged -= PanelDepUFEliminadas_ParentChanged;
        }

        public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
        {
            IsLoaded = false;
            CurrentDeposito = (GISADataset.DepositoRow)CurrentDataRow;

            try
            {
                DepositoRule.Current.LoadAutosEliminacao(GisaDataSetHelper.GetInstance(), conn);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw ex;
            }

            IsLoaded = true;
        }

        public override void ModelToView()
        {
            IsPopulated = false;

            var items = new List<ListViewItem>();
            GisaDataSetHelper.GetInstance().AutoEliminacao.Cast<GISADataset.AutoEliminacaoRow>().ToList().ForEach(
                aeRow => items.Add(new ListViewItem(aeRow.Designacao) { Tag = aeRow }));

            lstVwAutosEliminacao.BeginUpdate();
            lstVwAutosEliminacao.Items.AddRange(items.ToArray());
            lstVwAutosEliminacao.EndUpdate();

            IsPopulated = true;
        }

        public override void ViewToModel()
        {
            if (CurrentAutoEliminacao == null) return;

            // Elementos que tem a checkBox 'checked':
            long IDNivel;
            foreach (ListViewItem item in this.lstVwUnidadesFisicas.Items)
            {
                IDNivel = (long)item.Tag;
                GISADataset.NivelUnidadeFisicaRow[] nuf = (GISADataset.NivelUnidadeFisicaRow[])GisaDataSetHelper.GetInstance().NivelUnidadeFisica.Select("ID = " + IDNivel.ToString());
                if (nuf.Length == 0)
                {
                    if (item.Checked)
                    {
                        GISADataset.NivelUnidadeFisicaRow row = GisaDataSetHelper.GetInstance().NivelUnidadeFisica.NewNivelUnidadeFisicaRow(); // Linha nova
                        row.Eliminado = item.Checked;
                        row.ID = IDNivel;
                        GisaDataSetHelper.GetInstance().NivelUnidadeFisica.AddNivelUnidadeFisicaRow(row);
                    }
                }
                else if (item.Tag != null)
                    nuf[0].Eliminado = item.Checked;
            }
            // Notas de eliminacao:
            CurrentAutoEliminacao.NotasEliminacao = txt_NotasEliminacao.Text;
        }

        public override void Deactivate()
        {
            GUIHelper.GUIHelper.clearField(lstVwAutosEliminacao);
            GUIHelper.GUIHelper.clearField(lstVwUnidadesFisicas);
            GUIHelper.GUIHelper.clearField(txt_NotasEliminacao);
            CurrentDeposito = null;
            CurrentAutoEliminacao = null;
        }

        private void lstVwAutosEliminacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstVwAutosEliminacao.SelectedItems.Count == 1)
            {
                CurrentAutoEliminacao = lstVwAutosEliminacao.SelectedItems[0].Tag as GISADataset.AutoEliminacaoRow;
                if (CurrentAutoEliminacao == null) throw new Exception("Auto eliminação inválido!");

                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                try {
                    // Carregar os detalhes deste auto de eliminacao:
                    List<AutoEliminacaoRule.AutoEliminacao_UFsEliminadas> ufs_auto_eliminacao = AutoEliminacaoRule.Current.LoadUnidadesFisicasAvaliadas(GisaDataSetHelper.GetInstance(), CurrentAutoEliminacao.ID, ho.Connection);
                    List<ListViewItem> items = new List<ListViewItem>();
                    foreach (AutoEliminacaoRule.AutoEliminacao_UFsEliminadas uf in ufs_auto_eliminacao) {
                        ListViewItem item = new ListViewItem("");
                        item.Checked = uf.paraEliminar;
                        item.SubItems.Add(uf.codigo);
                        item.SubItems.Add(uf.designacao);
                        item.SubItems.Add(GUIHelper.GUIHelper.FormatDimensoes(uf.altura, uf.largura, uf.profundidade, uf.tipoMedida));
                        item.Tag = uf.IDNivel;
                        items.Add(item);

                        if (uf.largura.HasValue)
                            larguraTotal += uf.largura.Value;
                    }
                    this.lstVwUnidadesFisicas.Items.AddRange(items.ToArray());

                    grpUnidadesFisicasAvaliadas.Text = string.Format(grpUFsAssociadasText, this.lstVwUnidadesFisicas.Items.Count, larguraTotal);

                    // Notas de eliminacao:
                    if (CurrentAutoEliminacao.IsNotasEliminacaoNull())
                        txt_NotasEliminacao.Text = "";
                    else
                        txt_NotasEliminacao.Text = CurrentAutoEliminacao.NotasEliminacao;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    throw;
                }
                finally { ho.Dispose(); }
            }
            else
            {
                ViewToModel();

                lstVwUnidadesFisicas.Items.Clear();
            }
        }
    }
}
