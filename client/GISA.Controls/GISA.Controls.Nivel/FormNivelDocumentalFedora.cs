using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.Controls.ControloAut;
using GISA.Model;
using GISA.SharedResources;

namespace GISA.Controls.Nivel
{
    public partial class FormNivelDocumentalFedora : FormAddNivel
    {
        public FormNivelDocumentalFedora()
        {
            InitializeComponent();
            GetExtraResources();
        }

        private GISADataset.ControloAutDicionarioRow mTipologia;
        public GISADataset.ControloAutDicionarioRow Tipologia
        {
            get { return mTipologia; }
            set { mTipologia = value; txtTipologia.Text = mTipologia.DicionarioRow.Termo; } 
        }

        private GISADataset.FRDBaseRow mFRDBaseRow;
        public GISADataset.FRDBaseRow FRDBaseRow
        {
            get { return mFRDBaseRow; }
            set { mFRDBaseRow = value; }
        }

        private void GetExtraResources()
        {
            ButtonTI.Image = SharedResourcesOld.CurrentSharedResources.ChamarPicker;
            CurrentToolTip.SetToolTip(ButtonTI, SharedResourcesOld.CurrentSharedResources.ChamarPickerString);
        }

        protected override void UpdateButtonState()
        {
            base.UpdateButtonState();
            btnAccept.Enabled = btnAccept.Enabled;
        }

        public override void LoadData()
        {
            var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                DBAbstractDataLayer.DataAccessRules.NivelRule.Current.LoadTipoDocumento(GisaDataSetHelper.GetInstance(), mFRDBaseRow.IDNivel, ho.Connection);
                DBAbstractDataLayer.DataAccessRules.FRDRule.Current.LoadConteudoEEstrutura(GisaDataSetHelper.GetInstance(), mFRDBaseRow.ID, ho.Connection);
                var idxFRDCARow = GisaDataSetHelper.GetInstance().IndexFRDCA.Cast<GISADataset.IndexFRDCARow>().SingleOrDefault(r => r.IDFRDBase == mFRDBaseRow.ID && !r.IsSelectorNull() && r.Selector == -1);
                if (idxFRDCARow != null)
                    this.Tipologia = idxFRDCARow.ControloAutRow.GetControloAutDicionarioRows().Single(r => r.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
            finally
            {
                ho.Dispose();
            }
        }

        private void txtCodigo_TextChanged(object sender, System.EventArgs e)
        {
            UpdateButtonState();
        }

        private void txtDesignacao_TextChanged(object sender, System.EventArgs e)
        {
            UpdateButtonState();
        }

        private void txtTipologia_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonState();
        }

        private void ButtonTI_Click(object sender, EventArgs e)
        {
            FormPickControloAut frmPick = new FormPickControloAut();
            frmPick.Text = "Controlo de autoridade - Pesquisa por tipologia informacional";
            frmPick.caList.AllowedNoticiaAut(TipoNoticiaAut.TipologiaInformacional);
            retrieveSelection(frmPick, txtTipologia);
        }

        private void retrieveSelection(FormPickControloAut frmPick, TextBox txtBox)
        {
            frmPick.caList.txtFiltroDesignacao.Clear();
            frmPick.caList.ReloadList();

            switch (frmPick.ShowDialog())
            {
                case DialogResult.OK:
                    var li = frmPick.caList.SelectedItems.Cast<ListViewItem>().SingleOrDefault();
                    if (li == null) return;
                    
                    Debug.Assert(li.Tag is GISADataset.ControloAutDicionarioRow);

                    var ca = ((GISADataset.ControloAutDicionarioRow)li.Tag).ControloAutRow;

                    mTipologia = GisaDataSetHelper.GetInstance().ControloAutDicionario.Cast<GISADataset.ControloAutDicionarioRow>().Single(r => r.IDControloAut == ca.ID && r.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada);
                    txtBox.Text = mTipologia.DicionarioRow.Termo;
                    
                    break;
                case DialogResult.Cancel:
                    break;
            }
        }
    }
}
