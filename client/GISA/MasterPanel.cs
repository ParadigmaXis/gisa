// Classe base de todos os paineis a apresentar na área superior da 
// aplicação. Contém a informação de quais as permissões definidas 
// para o painel em causa. Tais permissões são geralmente dependentes 
// do painel inferior (Slavepanel) que estiver em uso
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
	public class MasterPanel : GISA.SinglePanel
	{

		internal MasterPanel()
		{
			InitializeComponent();

            removeDeletedItems.Tick += removeDeletedItems_Tick;
		}
        public virtual bool AllowCreate { get; set; }
        public virtual bool AllowDelete { get; set; }
        public virtual bool AllowEdit { get; set; }

		public delegate void StackChangedEventHandler(frmMain.StackOperation stackOperation, bool isSupport);
		public event StackChangedEventHandler StackChanged;
		public void DoStackChanged(frmMain.StackOperation stackOperation, bool isSupport)
		{
			if (StackChanged != null)
				StackChanged(stackOperation, isSupport);
		}

		public virtual void UpdateToolBarButtons()
		{
			UpdateToolBarButtons(null);
		}

		public virtual void UpdateToolBarButtons(ListViewItem item)
		{
		}

		public virtual void UpdatePermissions()
		{
            AllowCreate = ((frmMain)TopLevelControl).CheckPermission(GisaPrincipalPermission.CREATE);
            AllowEdit = ((frmMain)TopLevelControl).CheckPermission(GisaPrincipalPermission.WRITE);
            AllowDelete = ((frmMain)TopLevelControl).CheckPermission(GisaPrincipalPermission.DELETE);
		}

        public virtual void UpdateSupoortPanelPermissions(string classFullName)
        {
            // verificar se se pode criar no painel de suporte (as outras operações não são permitidas por definição)
            AllowCreate = false;
            AllowEdit = false;
            AllowDelete = false;
            try
            {
                new GisaPrincipalPermission(SessionHelper.GetGisaPrincipal(), classFullName, GisaPrincipalPermission.CREATE).Demand();
                AllowCreate = true;
            }
            catch (System.Security.SecurityException) { }
        }

		// Coloca numa ICollection para eliminação todos os elementos da lista de contextos 
		// com as respectivas rows associadas com o estado "detached" indicativo que foram 
		// eliminadas por outro utilizador
		protected void DelayedRemoveDeletedItems(ICollection lvItems)
		{
			removeDeletedItems.Stop();
			DataRow row = null;
			foreach (ListViewItem item in lvItems) {
                row = (DataRow)item.Tag;
                if (row.RowState == DataRowState.Detached)
                    mDeletedItems.Add(item);
			}

			if (mDeletedItems.Count > 0) {
				removeDeletedItems.Interval = 1000;
				removeDeletedItems.Start();
			}
		}

		private ArrayList mDeletedItems = new ArrayList();
		private Timer removeDeletedItems = new Timer();
		// Método responsável por eliminar todos os elementos da lista de contexto indicados na ICollection passada como argumento
		// excepto aquele que estiver selecionado
		private void removeDeletedItems_Tick(object sender, System.EventArgs e)
		{
			removeDeletedItems.Stop();
			// as rows ja foram eliminadas no modelo de dados. eliminam-se agora da interface
			ListViewItem item = null;
			for (int i = mDeletedItems.Count - 1; i >= 0; i--)
			{
				item = (ListViewItem)(mDeletedItems[i]);
				if (! item.Selected)
				{
					item.Remove();
					mDeletedItems.RemoveAt(i);
				}
			}
		}

        public static bool isContextPanel(Control MasterPanel)
        {
            frmMain mainForm = (frmMain)MasterPanel.TopLevelControl;
            // mainForm.MasterPanel é o masterpanel de topo

            if (mainForm.MasterPanelCount <= 1)
                return true;

            if (mainForm.MasterPanelCount > 1 && !(MasterPanel == mainForm.MasterPanel))
                return true;

            return false;
        }

		private void InitializeComponent()
		{
			this.pnlToolbarPadding.SuspendLayout();
			this.SuspendLayout();
			//
			//lblFuncao
			//
			this.lblFuncao.Name = "lblFuncao";
			//
			//pnlToolbarPadding
			//
			this.pnlToolbarPadding.DockPadding.Left = 5;
			this.pnlToolbarPadding.DockPadding.Right = 5;
			this.pnlToolbarPadding.Name = "pnlToolbarPadding";
			//
			//ToolBar
			//
			this.ToolBar.Name = "ToolBar";
			this.ToolBar.Size = new System.Drawing.Size(586, 24);
			//
			//MasterPanel
			//
			this.Name = "MasterPanel";
			this.pnlToolbarPadding.ResumeLayout(false);
			this.ResumeLayout(false);			

		}
	}

} //end of root namespace