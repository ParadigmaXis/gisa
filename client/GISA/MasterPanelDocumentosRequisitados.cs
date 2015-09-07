using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

using GISA.Controls;

namespace GISA
{
    public partial class MasterPanelDocumentosRequisitados : GISA.MasterPanel
    {
        public MasterPanelDocumentosRequisitados()
        {
            InitializeComponent();

            this.tbFiltro.ToolTipText = SharedResourcesOld.CurrentSharedResources.FiltroString;

            NivelDocumentalList1.ItemDrag += NivelDocumentalList1_ItemDrag;
            ToolBar.ButtonClick += Toolbar_ButtonClick;
        }        

        private void Toolbar_ButtonClick(object Sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button == tbFiltro)
                NivelDocumentalList1.FilterVisible = tbFiltro.Pushed;
        }

        private void NivelDocumentalList1_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
        {
            object DragDropObject = null;
            GISADataset.NivelRow nRow = null;
            this.Cursor = Cursors.WaitCursor;

            if (e.Item == null)
                return;

            if (e.Item is ListViewItem)
            {
                if (NivelDocumentalList1.SelectedItems.Count > 1)
                {
                    List<ListViewItem> lst = new List<ListViewItem>();
                    Dictionary<long, ListViewItem> dick = new Dictionary<long, ListViewItem>();
                    List<long> nivelIDs = new List<long>();
                    var perms = new Dictionary<long, Dictionary<string, byte>>();
                    long tmpID;
                    foreach (ListViewItem lvItem in NivelDocumentalList1.SelectedItems)
                    {
                        tmpID = ((GISADataset.NivelRow)lvItem.Tag).ID;
                        nivelIDs.Add(tmpID);
                        dick[tmpID] = lvItem;
                    }

                    GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                    try
                    {
                        perms = PermissoesRule.Current.CalculateEffectivePermissions(nivelIDs, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID, ho.Connection);
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex.ToString());
                        throw;
                    }
                    finally
                    {
                        ho.Dispose();
                    }

                    foreach (long idNivel in perms.Keys)
                    {
                        var nperm = perms[idNivel];
                        if (nperm.ContainsKey("Ler") && nperm["Ler"] == 1)
                            lst.Add(dick[idNivel]);
                    }
                    DragDropObject = lst.ToArray();
                }
                else if (e.Item != null)
                {
                    DragDropObject = (ListViewItem)e.Item;
                    nRow = (GISADataset.NivelRow)(((ListViewItem)DragDropObject).Tag);
                    PermissoesHelper.UpdateNivelPermissions(nRow, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID);
                    if (!PermissoesHelper.AllowRead)
                        return;
                }
            }

            if (DragDropObject == null)
                return;

            this.Cursor = Cursors.Default;
            Trace.WriteLine("Dragging " + DragDropObject.ToString().GetType().FullName);
            DoDragDrop(DragDropObject, DragDropEffects.Link);
        }
    }
}
