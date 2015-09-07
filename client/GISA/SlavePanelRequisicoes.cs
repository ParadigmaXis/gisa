using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using GISA.SharedResources;
using GISA.Controls.Nivel;

namespace GISA
{
    public partial class SlavePanelRequisicoes : SlavePanelMovimentos
    {
        public SlavePanelRequisicoes()
        {
            InitializeComponent();
        }

        public static Bitmap FunctionImage
        {
            get
            {
                return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "Requisicoes_enabled_32x32.png");
            }
        }

        private void ApplyNDocListFilter(NivelNavigator nNav)
        {
            nNav.ExcluirRequisitados = true;
        }

        private void ClearNDocListFilter(NivelNavigator nNav)
        {
            nNav.ExcluirRequisitados = false;
            nNav.MovimentoID = null;
        }

        protected internal override void ToggleNiveisSupportPanel(bool showIt)
        {
            if (showIt)
            {
                // Make sure the button is pushed
                ToolBarButtonAuxList.Pushed = true;

                // Indicação que um painel está a ser usado como suporte
                ((frmMain)this.TopLevelControl).isSuportPanel = true;

                // Show the panel with all unidades fisicas
                ((frmMain)this.TopLevelControl).PushMasterPanel(typeof(MasterPanelPermissoesPlanoClassificacao));

                MasterPanelPermissoesPlanoClassificacao mpPPC = (MasterPanelPermissoesPlanoClassificacao)(((frmMain)this.TopLevelControl).MasterPanel);

                // é necessário actualizar o estado dos botões neste ponto uma vez que
                // nenhuma unidade física é definida como contexto automaticamente (a primeira a
                // ser apresentada na lista)
                mpPPC.lblFuncao.Text = "Estrutura orgânica";
                mpPPC.coluna_Requisitado_Visible(false);
                mpPPC.UpdateToolBarButtons();
                mpPPC.nivelNavigator1.MultiSelect = true;
                ApplyNDocListFilter(mpPPC.nivelNavigator1);
                mpPPC.nivelNavigator1.MovimentoID = CurrentMovimento.ID;
                if (mpPPC.nivelNavigator1.PanelToggleState == NivelNavigator.ToggleState.Documental)
                    mpPPC.nivelNavigator1.ReloadList();
            }
            else
            {
                // Make sure the button is not pushed            
                ToolBarButtonAuxList.Pushed = false;

                // Remove the panel with all unidades fisicas
                if (this.TopLevelControl != null)
                {
                    if (((frmMain)this.TopLevelControl).MasterPanel is MasterPanelPermissoesPlanoClassificacao)
                    {
                        MasterPanelPermissoesPlanoClassificacao mpPPC = (MasterPanelPermissoesPlanoClassificacao)(((frmMain)this.TopLevelControl).MasterPanel);
                        ClearNDocListFilter(mpPPC.nivelNavigator1);

                        // Indicação que nenhum painel está a ser usado como suporte
                        ((frmMain)this.TopLevelControl).isSuportPanel = false;
                        ((frmMain)this.TopLevelControl).PopMasterPanel(typeof(MasterPanelPermissoesPlanoClassificacao));
                    }
                }
            }
        }
    }
}
