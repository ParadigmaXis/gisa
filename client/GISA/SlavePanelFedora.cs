using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;
using GISA.Controls;
using GISA.Fedora.FedoraHandler;
using GISA.Model;
using GISA.SharedResources;

namespace GISA
{
    public partial class SlavePanelFedora : GISA.SinglePanel
    {
        private ObjetoDigitalFedoraHelper odHelper;

        public SlavePanelFedora()
        {
            InitializeComponent();
            odHelper = new ObjetoDigitalFedoraHelper();
            this.controlObjetoDigital1.ViewMode = ObjetoDigitalFedoraHelper.Contexto.nenhum;
        }

        public static Bitmap FunctionImage
        {
            get { return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "Fedora_enabled_32x32.png"); }
        }

        public override void LoadData()
        {
            if (CurrentContext.FedoraNivel == null)
            {
                odHelper.currentNivel = null;
                return;
            }

            odHelper.currentNivel = CurrentContext.FedoraNivel;
            odHelper.LoadData();

            if (odHelper.currentODComp != null)
                CurrentContext.ObjetoDigital = odHelper.currentODComp;
            else
                CurrentContext.ObjetoDigital = odHelper.currentODSimples.Count == 1 ? odHelper.currentODSimples[0] : null;
        }

        public override void ModelToView()
        {
            this.lblFuncao.Text = "";

            controlObjetoDigital1.ViewMode = odHelper.mContexto;
            controlObjetoDigital1.CurrentODSimples = odHelper.currentODSimples;
            controlObjetoDigital1.CurrentODComp = odHelper.currentODComp;
            controlObjetoDigital1.docSimplesSemOD = odHelper.docSimplesSemOD;

            switch (odHelper.mContexto)
            {
                case ObjetoDigitalFedoraHelper.Contexto.objetosDigitais:
                    this.lblFuncao.Text = "Objetos Digitais";
                    controlObjetoDigital1.Titulo = controlObjetoDigital1.CurrentODComp != null ? controlObjetoDigital1.CurrentODComp.titulo : odHelper.currentNivel.GetNivelDesignadoRows().Single().Designacao;
                    break;
                case ObjetoDigitalFedoraHelper.Contexto.imagens:
                    this.lblFuncao.Text = "Objeto Digital Simples";
                    controlObjetoDigital1.Titulo = odHelper.currentODSimples.Count > 0 ? odHelper.currentODSimples[0].titulo : odHelper.currentNivel.GetNivelDesignadoRows().Single().Designacao;
                    break;
                default: // igual a ObjetoDigitalFedoraHelper.Contexto.objetosDigitais.nenhum
                    this.lblFuncao.Text = "Objeto(s) Digital(ais) associado(s) não encontrado(s)";
                    break;
            }

            controlObjetoDigital1.ModelToView();
        }

        public override bool ViewToModel()
        {
            controlObjetoDigital1.ViewToModel();

            odHelper.currentODSimples = controlObjetoDigital1.CurrentODSimples;
            odHelper.currentODComp = controlObjetoDigital1.CurrentODComp;
            odHelper.docSimplesSemOD = controlObjetoDigital1.docSimplesSemOD;

            odHelper.ViewToModel(controlObjetoDigital1.ViewMode, controlObjetoDigital1.disableSave);

            return true;
        }

        private void Save()
        {
            Save(false);
        }

        public override PersistencyHelper.SaveResult Save(bool activateOpcaoCancelar)
        {
            var preTransactionAction = new PreTransactionAction();
            var args = new PersistencyHelper.FedoraIngestPreTransactionArguments();
            preTransactionAction.args = args;
            bool ingestSuccess = true;

            if (controlObjetoDigital1.disableSave) return PersistencyHelper.SaveResult.nothingToSave;

            preTransactionAction.preTransactionDelegate = delegate(PersistencyHelper.PreTransactionArguments preTransactionArgs)
            {
                string msg = null;

                if (odHelper.currentODComp != null)
                    ingestSuccess = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.Ingest(odHelper.currentODComp, out msg);
                else
                    odHelper.currentODSimples.ForEach(odSimples => ingestSuccess &= SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.Ingest(odSimples, out msg));

                if (ingestSuccess)
                    odHelper.newObjects.Keys.ToList().ForEach(k => { k.pid = odHelper.newObjects[k].pid; });

                preTransactionArgs.cancelAction = !ingestSuccess;
                preTransactionArgs.message = msg;
            };

            PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save(preTransactionAction, activateOpcaoCancelar);

            if (successfulSave != PersistencyHelper.SaveResult.successful && !ingestSuccess)
            {
                MessageBox.Show("Ocorreu um erro na ingestão do objeto digital.", "Ingestão", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                odHelper.newObjects.Clear();
                odHelper.mContexto = ObjetoDigitalFedoraHelper.Contexto.nenhum;
                odHelper.currentODComp = null;
                odHelper.currentODSimples = null;
            }
            else if (successfulSave == PersistencyHelper.SaveResult.successful)
            {
                GISA.Search.Updater.updateNivelDocumental(odHelper.currentNivel.ID);
            }

            return successfulSave;
        }

        public override void Deactivate()
        {
            this.lblFuncao.Text = "";
            this.PanelMensagem1.Visible = false;
            this.PanelMensagem1.SendToBack();

            odHelper.Deactivate();
            controlObjetoDigital1.Deactivate();
        }

        protected override bool isConnected()
        {
            return SessionHelper.AppConfiguration.GetCurrentAppconfiguration().FedoraHelperSingleton.Connect();
        }

        protected override bool isInnerContextValid()
        {
            return odHelper.currentNivel != null && odHelper.currentNivel.RowState != DataRowState.Detached && odHelper.currentNivel.isDeleted == 0 && odHelper.mContexto != ObjetoDigitalFedoraHelper.Contexto.nenhum;
        }

        protected override bool isOuterContextValid()
        {
            return CurrentContext.FedoraNivel != null 
                && CurrentContext.FedoraNivel.IDTipoNivel == TipoNivel.DOCUMENTAL;
        }

        protected override bool isOuterContextDeleted()
        {
            Debug.Assert(CurrentContext.FedoraNivel != null, "CurrentContext.FedoraNivel Is Nothing");
            return CurrentContext.FedoraNivel.RowState == DataRowState.Detached;
        }

        protected override bool hasReadPermission()
        {
            return PermissoesHelper.AllowRead &&
                (CurrentContext.FedoraNivel.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First().IDTipoNivelRelacionado < TipoNivelRelacionado.SD ||
                    (CurrentContext.FedoraNivel.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica().First().IDTipoNivelRelacionado == TipoNivelRelacionado.SD && PermissoesHelper.ObjDigAllowRead));
        }

        protected override void addContextChangeHandlers()
        {
            CurrentContext.FedoraNivelChanged += this.Recontextualize;
        }

        protected override void removeContextChangeHandlers()
        {
            CurrentContext.FedoraNivelChanged -= this.Recontextualize;
        }

        protected override PanelMensagem GetNoConnectionMessage()
        {
            PanelMensagem1.LblMensagem.Text = "Não foi possível contactar o repositório de objetos " + Environment.NewLine +
                    "digitais. A funcionalidade de associação e visualização" + Environment.NewLine +
                    "de objetos poderá encontrar-se condicionada.";
            return PanelMensagem1;
        }

        protected override PanelMensagem GetDeletedContextMessage()
        {
            PanelMensagem1.LblMensagem.Text = "Esta relação hierárquica foi eliminada não sendo, por isso, possível apresentar a unidade de informação selecionada.";
            return PanelMensagem1;
        }

        protected override PanelMensagem GetNoContextMessage()
        {
            PanelMensagem1.LblMensagem.Text = "Para visualizar os detalhes deverá selecionar uma unidade informacional do tipo documental no painel superior.";
            return PanelMensagem1;
        }

        protected override PanelMensagem GetNoReadPermissionMessage()
        {   
            PanelMensagem1.LblMensagem.Text = "Não tem permissão para visualizar os detalhes da unidade informacional selecionada no painel superior.";
            if (!PermissoesHelper.ObjDigAllowRead)
                PanelMensagem1.LblMensagem.Text = "Não tem permissão para visualizar o objeto digital associado à unidade informacional selecionada no painel superior.";
            return PanelMensagem1;
        }
    }
}
