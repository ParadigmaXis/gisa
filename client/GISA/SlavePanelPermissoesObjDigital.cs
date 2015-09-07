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
using GISA.Controls;
using GISA.Model;
using GISA.SharedResources;

namespace GISA
{
    public partial class SlavePanelPermissoesObjDigital : SinglePanel
    {
        public SlavePanelPermissoesObjDigital()
        {
            InitializeComponent();

            permissoesObjetoDigitalList1.FilterVisible = false;
        }

        public static Bitmap FunctionImage
        {
            get { return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "PermissoesFedora_32x32.png"); }
        }

        private GISADataset.NivelRow mCurrentNivel;
        private GISADataset.TrusteeRow mCurrentTrustee;

        public override void LoadData()
        {
            if (CurrentContext.PermissoesNivelObjDigital == null || CurrentContext.PermissoesTrusteeObjDigital == null)
            {
                mCurrentNivel = null;
                mCurrentTrustee = null;
                return;
            }

            mCurrentNivel = CurrentContext.PermissoesNivelObjDigital;
            mCurrentTrustee = CurrentContext.PermissoesTrusteeObjDigital;

            permissoesObjetoDigitalList1.CurrentTrusteeRow = mCurrentTrustee;
            permissoesObjetoDigitalList1.CurrentNivelRow = mCurrentNivel;
            permissoesObjetoDigitalList1.ReloadList();
        }

        public override void ModelToView()
        {
        }

        public override bool ViewToModel()
        {
            return false;
        }

        private void Save()
        {
            Save(false);
        }

        public override PersistencyHelper.SaveResult Save(bool activateOpcaoCancelar)
        {
            PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save(activateOpcaoCancelar);

            return successfulSave;
        }

        public override void Deactivate()
        {
        }

        protected override bool isInnerContextValid()
        {
            return mCurrentTrustee != null && mCurrentNivel != null;
        }

        protected override bool isOuterContextValid()
        {
            return CurrentContext.PermissoesNivelObjDigital != null && CurrentContext.PermissoesTrusteeObjDigital != null
                && CurrentContext.PermissoesNivelObjDigital.IDTipoNivel == TipoNivel.DOCUMENTAL
                && CurrentContext.PermissoesNivelObjDigital.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First().IDTipoNivelRelacionado >= (long)TipoNivelRelacionado.SR;
        }

        protected override bool isOuterContextDeleted()
        {
            Debug.Assert(CurrentContext.PermissoesNivelObjDigital != null && CurrentContext.PermissoesTrusteeObjDigital != null, "CurrentContext.PermissoesObjDigital Is Nothing");
            return CurrentContext.PermissoesTrusteeObjDigital.RowState == DataRowState.Detached || CurrentContext.PermissoesNivelObjDigital.RowState == DataRowState.Detached;
        }

        protected override void addContextChangeHandlers()
        {
            CurrentContext.PermissoesObjDigitalChanged += this.Recontextualize;
        }

        protected override void removeContextChangeHandlers()
        {
            CurrentContext.PermissoesObjDigitalChanged -= this.Recontextualize;
        }

        protected override PanelMensagem GetDeletedContextMessage()
        {
            PanelMensagem1.LblMensagem.Text = "Este utilizador e/ou nivel documental foi removido não sendo por isso possível apresentá-lo.";
            return PanelMensagem1;
        }

        protected override PanelMensagem GetNoContextMessage()
        {
            PanelMensagem1.LblMensagem.Text = "Para visualizar os detalhes deverá selecionar um utilizador e um nivel documental no painel superior.";
            return PanelMensagem1;
        }
    }
}