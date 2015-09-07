using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
    public class ControloAutAutorDragDrop : ControloAutDragDrop
    {
        public ControloAutAutorDragDrop(ListView ListView, TipoNoticiaAut[] TipoNoticiaAutAllowed, GISADataset.FRDBaseRow FRDBaseRow) : base(ListView, TipoNoticiaAutAllowed, FRDBaseRow) { }

        internal protected override DataRow NewRow(GISADataset.ControloAutRow caRow)
        {
            var newRow = GisaDataSetHelper.GetInstance().SFRDAutor.NewSFRDAutorRow();
            newRow.IDFRDBase = FRDBaseRow.ID;
            newRow.IDControloAut = caRow.ID;
            GisaDataSetHelper.GetInstance().SFRDAutor.AddSFRDAutorRow(newRow);
            return newRow;
        }

        protected internal override DataRow GetRow()
        {
            return GisaDataSetHelper.GetInstance().SFRDAutor.Cast<GISADataset.SFRDAutorRow>().Single(r => r.IDControloAut == ControloAutRow.ID && r.IDFRDBase == FRDBaseRow.ID);
        }

        internal protected override ControloAutDragDrop.NotPresentInIndexResults NotPresentInIndex(GISA.Model.GISADataset.ControloAutRow ControloAutRow)
        {
            if (GisaDataSetHelper.GetInstance().SFRDAutor.Select("IDFRDBase=" + FRDBaseRow.ID.ToString() + " AND IDControloAut=" + ControloAutRow.ID.ToString()).Length == 0)
            {
                DataRow[] deletedRows = GisaDataSetHelper.GetInstance().SFRDAutor.Select("", "", DataViewRowState.Deleted);

                if (deletedRows.Length > 0)
                {
                    foreach (DataRow deletedRow in deletedRows)
                    {
                        if (((long)(deletedRow["IDFRDBase", DataRowVersion.Original])) == FRDBaseRow.ID && ((long)(deletedRow["IDControloAut", DataRowVersion.Original])) == ControloAutRow.ID)
                        {
                            deletedRow.RejectChanges();
                            return NotPresentInIndexResults.PresentInIndexDeleted;
                        }
                    }
                }
                return NotPresentInIndexResults.NotPresentInIndex;
            }
            else
                return NotPresentInIndexResults.PresentInIndex;
        }
    }
}
