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
    public partial class FRDDepositos : GISA.MultiPanelControl
    {
        public FRDDepositos()
        {
            InitializeComponent();

            var node1 = DropDownTreeView1.Nodes.Add("Identificação");
            node1.Tag = PanelDepIdentificacao1;

            var node2 = DropDownTreeView1.Nodes.Add("Unidades Físicas Eliminadas");
            node2.Tag = PanelDepUFEliminadas1;

            DropDownTreeView1.SelectedNode = DropDownTreeView1.Nodes[0];
        }

        public static Bitmap FunctionImage
        {
            get
            {
                return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "GestaoDepositos_32x32.png");
            }
        }

        private GISADataset.DepositoRow CurrentDeposito = null;
        private bool isLoaded = false;
        public override void LoadData()
        {
            try
            {
                ((frmMain)TopLevelControl).EnterWaitMode();
                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                try
                {
                    GisaDataSetHelper.ManageDatasetConstraints(false);

                    if (!isLoaded)
                    {
                        if (CurrentContext.Deposito == null) return;

                        // Recarregar a uf actual e guardar um contexto localmente
                        DepositoRule.Current.LoadDepositoData(GisaDataSetHelper.GetInstance(), CurrentContext.Deposito.ID, ho.Connection);
                        CurrentDeposito = GisaDataSetHelper.GetInstance().Deposito.Cast<GISADataset.DepositoRow>().SingleOrDefault(d => d.ID == CurrentContext.Deposito.ID);

                        if (CurrentDeposito == null || CurrentDeposito.RowState == DataRowState.Detached || 
                            CurrentContext.Deposito == null || CurrentContext.Deposito.RowState == DataRowState.Detached) return;
                        
                        isLoaded = true;
                    }
                    GisaDataSetHelper.ManageDatasetConstraints(false);
                    GISAPanel selectedPanel = (GISAPanel)this.DropDownTreeView1.SelectedNode.Tag;
                    if (!selectedPanel.IsLoaded)
                    {
                        long startTicks = 0;
                        startTicks = DateTime.Now.Ticks;
                        selectedPanel.LoadData(CurrentDeposito, ho.Connection);
                        Debug.WriteLine("Time dispend loading " + selectedPanel.ToString() + ": " + new TimeSpan(DateTime.Now.Ticks - startTicks).ToString());
                    }
                    GisaDataSetHelper.ManageDatasetConstraints(true);
                }
                catch (System.Data.ConstraintException Ex)
                {
                    Trace.WriteLine(Ex);
                    GisaDataSetHelper.FixDataSet(GisaDataSetHelper.GetInstance(), ho.Connection);
                }
                finally
                {
                    ho.Dispose();
                }
            }
            finally
            {
                ((frmMain)TopLevelControl).LeaveWaitMode();
            }
        }

        public override void ModelToView()
        {
            try
            {
                ((frmMain)TopLevelControl).EnterWaitMode();

                // se nao existir um contexto definido os slavepanels não devem ser apresentados
                if (CurrentDeposito == null || CurrentDeposito.RowState == DataRowState.Detached)
                {
                    this.Visible = false;
                    return;
                }

                try
                {
                    GisaPrincipalPermission tempWith1 = new GisaPrincipalPermission(SessionHelper.GetGisaPrincipal(), this.GetType().FullName, GisaPrincipalPermission.WRITE);
                    tempWith1.Demand();
                }
                catch (System.Security.SecurityException)
                {
                    GUIHelper.GUIHelper.makeReadOnly(this.GisaPanelScroller);
                }

                this.Visible = true;
                GISAPanel selectedPanel = (GISAPanel)this.DropDownTreeView1.SelectedNode.Tag;
                if (selectedPanel.IsLoaded && !selectedPanel.IsPopulated)
                {
                    long startTicks = 0;
                    startTicks = DateTime.Now.Ticks;
                    selectedPanel.ModelToView();
                    Debug.WriteLine("Time dispend model to view " + selectedPanel.ToString() + ": " + new TimeSpan(DateTime.Now.Ticks - startTicks).ToString());
                }
            }
            finally
            {
                ((frmMain)TopLevelControl).LeaveWaitMode();
            }
        }

        public override bool ViewToModel()
        {
            base.ViewToModel();
            return true;
        }

        public override void Deactivate()
        {
            DeactivatePanels();
            isLoaded = false;
            CurrentDeposito = null;
            existsModifiedData = false;
        }

        protected override bool isInnerContextValid()
        {
            return CurrentDeposito != null && !(CurrentDeposito.RowState == DataRowState.Detached) && CurrentDeposito.isDeleted == 0;
        }

        protected override bool isOuterContextValid()
        {
            return CurrentContext.Deposito != null;
        }

        protected override bool isOuterContextDeleted()
        {
            Debug.Assert(CurrentContext.Deposito != null, "CurrentContext.Deposito Is Nothing");
            return CurrentContext.Deposito.RowState == DataRowState.Detached;
        }

        protected override void addContextChangeHandlers()
        {
            CurrentContext.DepositoChanged += this.Recontextualize;
            //CurrentContext.AddRevisionEvent += RegisterModification;
        }

        protected override void removeContextChangeHandlers()
        {
            CurrentContext.DepositoChanged -= this.Recontextualize;
            //CurrentContext.AddRevisionEvent -= RegisterModification;
        }

        protected override PanelMensagem GetDeletedContextMessage()
        {
            PanelMensagem1.LblMensagem.Text = "Este depósito foi eliminado não sendo por isso possível apresentá-lo.";
            return PanelMensagem1;
        }

        protected override PanelMensagem GetNoContextMessage()
        {
            PanelMensagem1.LblMensagem.Text = "Para visualizar os detalhes deverá selecionar um depósito no painel superior.";
            return PanelMensagem1;
        }
    }
}
