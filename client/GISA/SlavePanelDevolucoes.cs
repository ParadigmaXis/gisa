using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using GISA.SharedResources;

namespace GISA
{
    public partial class SlavePanelDevolucoes : GISA.SlavePanelMovimentos
    {
        public SlavePanelDevolucoes()
        {
            InitializeComponent();
        }

        public static Bitmap FunctionImage
        {
            get
            {
                return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "Devolucoes_32x32.png");
            }
        }

        protected internal override void ToggleNiveisSupportPanel(bool showIt)
        {
            if (showIt)
            {
                // Make sure the button is pushed
                ToolBarButtonAuxList.Pushed = true;

                // Indicação que um painel está a ser usado como suporte
                ((frmMain)this.TopLevelControl).isSuportPanel = true;

                // Show the panel with all documentos requisitados
                ((frmMain)this.TopLevelControl).PushMasterPanel(typeof(MasterPanelDocumentosRequisitados));

                MasterPanelDocumentosRequisitados mpDocReq = (MasterPanelDocumentosRequisitados)(((frmMain)this.TopLevelControl).MasterPanel);

                mpDocReq.NivelDocumentalList1.IDMovimento = CurrentMovimento.ID;
                mpDocReq.NivelDocumentalList1.Visible = true;
                mpDocReq.NivelDocumentalList1.FilterVisible = false;
                mpDocReq.NivelDocumentalList1.MultiSelectListView = true;
                mpDocReq.NivelDocumentalList1.ReloadList();
            }
            else
            {
                // Make sure the button is not pushed            
                ToolBarButtonAuxList.Pushed = false;

                // Remove the panel with all unidades fisicas
                if (this.TopLevelControl != null)
                {
                    if (((frmMain)this.TopLevelControl).MasterPanel is MasterPanelDocumentosRequisitados)
                    {
                        MasterPanelDocumentosRequisitados mpDocReq = (MasterPanelDocumentosRequisitados)(((frmMain)this.TopLevelControl).MasterPanel);
                        
                        // Indicação que nenhum painel está a ser usado como suporte
                        ((frmMain)this.TopLevelControl).isSuportPanel = false;
                        ((frmMain)this.TopLevelControl).PopMasterPanel(typeof(MasterPanelDocumentosRequisitados));
                    }
                }
            }
        }
    }
}
