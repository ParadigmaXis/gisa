using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

using DBAbstractDataLayer.DataAccessRules;
using GISA.Model;

namespace GISA
{
	public class NivelOrganicoDragDrop : ControloAutDragDrop
	{
        Control parent = null;
        public NivelOrganicoDragDrop(ListView ListView, GISADataset.FRDBaseRow FRDBaseRow, Control control) : base(ListView, new TipoNoticiaAut[] { TipoNoticiaAut.EntidadeProdutora }, FRDBaseRow) { this.parent = control; }
		public NivelOrganicoDragDrop(ListView ListView, GISADataset.ControloAutRow ControloAutRow) : base(ListView, new TipoNoticiaAut[] {TipoNoticiaAut.EntidadeProdutora}, ControloAutRow) { }

		protected override void AcceptContents(object Value)
		{
			GISADataset.ControloAutRow ControloAutRow = (GISADataset.ControloAutRow)Value;

			if (FRDBase != null)
			{
				// Carregar nivel do CA largado

				IDbConnection conn = GisaDataSetHelper.GetConnection();
				try
				{
					conn.Open();
					DBAbstractDataLayer.DataAccessRules.NivelRule.Current.LoadNivelByControloAut(ControloAutRow.ID, GisaDataSetHelper.GetInstance(), conn);
				}
				finally
				{
					conn.Close();
				}

				if (IsValidRelacaoHierarquica(ControloAutRow))
				{
					// Apresentar form que permita escolher a data da relação
					FormRelacaoHierarquica frmRh = new FormRelacaoHierarquica();
					// Pode-se obter a primeira relação encontrada para efeitos de determinação 
					// do tipo de nível uma vez que o tipo de nível dos níveis documentais nunca se alterará
					frmRh.relacaoNvl.TipoNivelRelacionadoRow = TipoNivelRelacionado.GetPrimeiraRelacaoEncontrada(this.FRDBase.NivelRow).TipoNivelRelacionadoRow;
					frmRh.relacaoNvl.ContextNivelRow = ControloAutRow.GetNivelControloAutRows()[0].NivelRow;
					if (frmRh.ShowDialog() == DialogResult.Cancel)
					{
						return;
					}

					if (GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", this.FRDBase.NivelRow.ID, ControloAutRow.GetNivelControloAutRows()[0].NivelRow.ID), "", DataViewRowState.Deleted).Length > 0)
					{

						GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", this.FRDBase.NivelRow.ID, ControloAutRow.GetNivelControloAutRows()[0].NivelRow.ID), "", DataViewRowState.Deleted)[0].AcceptChanges();
					}

					GISADataset.RelacaoHierarquicaRow rhRow = null;
					rhRow = GisaDataSetHelper.GetInstance().RelacaoHierarquica.NewRelacaoHierarquicaRow();
					TempRelacaoHierarquica = rhRow;

					rhRow.ID = this.FRDBase.NivelRow.ID;
					rhRow.IDUpper = ControloAutRow.GetNivelControloAutRows()[0].NivelRow.ID;
					rhRow.IDTipoNivelRelacionado = TipoNivelRelacionado.GetTipoNivelRelacionadoDaPrimeiraRelacaoEncontrada(this.FRDBase.NivelRow).ID;
					rhRow.Descricao = frmRh.relacaoNvl.txtDescricao.Text;
					rhRow.InicioAno = frmRh.relacaoNvl.dtRelacaoInicio.ValueYear;
					rhRow.InicioMes = frmRh.relacaoNvl.dtRelacaoInicio.ValueMonth;
					rhRow.InicioDia = frmRh.relacaoNvl.dtRelacaoInicio.ValueDay;
					rhRow.FimAno = frmRh.relacaoNvl.dtRelacaoFim.ValueYear;
					rhRow.FimMes = frmRh.relacaoNvl.dtRelacaoFim.ValueMonth;
					rhRow.FimDia = frmRh.relacaoNvl.dtRelacaoFim.ValueDay;
					GisaDataSetHelper.GetInstance().RelacaoHierarquica.AddRelacaoHierarquicaRow(rhRow);

                    // no caso de se tratar de uma entidade produtora é necessário validar o código de referência do nivel documental do contexto
                    if (ControloAutRow.IDTipoNoticiaAut == (long)TipoNoticiaAut.EntidadeProdutora)
                    {
                        var argsPC = new PersistencyHelper.EnsureUniqueCodigoNivelPreConcArguments();
                        argsPC.rhRowID = rhRow.ID;
                        argsPC.rhRowIDUpper = rhRow.IDUpper;

                        var postSaveAction = new PostSaveAction();
                        PersistencyHelper.UpdatePermissionsPostSaveArguments args = new PersistencyHelper.UpdatePermissionsPostSaveArguments();
                        postSaveAction.args = args;

                        postSaveAction.postSaveDelegate = delegate(PersistencyHelper.PostSaveArguments postSaveArgs)
                        {
                            if (argsPC.successful)
                            {
                                if (this.parent == null)
                                    Debug.Assert(false, "CONTEXTO PARENT DEVE ESTAR DEFINIDO.");

                                if (this.parent.GetType() == typeof(MasterPanelSeries))
                                {
                                    ((MasterPanelSeries)this.parent).CurrentContext.RaiseRegisterModificationEvent(this.FRDBase);
                                    PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao,
                                        GisaDataSetHelper.GetInstance().FRDBaseDataDeDescricao.Cast<GISADataset.FRDBaseDataDeDescricaoRow>().Where(r => r.RowState == DataRowState.Added).ToArray(), postSaveArgs.tran);
                                }
                            }
                        };

                        var result = PersistencyHelper.save(DelegatesHelper.verificaCodigosRepetidos, argsPC, postSaveAction, true);

                        if (result == PersistencyHelper.SaveResult.successful)
                        {
                            PersistencyHelper.cleanDeletedData();
                            GisaDataSetHelper.VisitControloAutDicionario(ControloAutRow, DisplayFormaAutorizada);
                            GISA.Search.Updater.updateNivelDocumental(this.FRDBase.NivelRow.ID);
                        }
                        else
                        {
                            if (argsPC.message.Length > 0)
                                MessageBox.Show(argsPC.message, "Adição", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

					
				}
				else
				{
					MessageBox.Show("Não é possível a existência de items repetidos.", "Adição", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			else if (this.ControloAut != null)
			{
				Debug.Assert(false, "ORGANIC RELATIONS BETWEEN CAS NOT IMPLEMENTED. SHOULDN'T BE NEEDED");
			}
		}

		private object TempRelacaoHierarquica;
		protected override void DisplayFormaAutorizada(GISADataset.ControloAutDicionarioRow ControloAutDicionario)
		{
			Debug.Assert(TempRelacaoHierarquica != null);

			if (ControloAutDicionario.IDTipoControloAutForma == Convert.ToInt64(TipoControloAutForma.FormaAutorizada))
			{

				ListViewItem li = ListView.Items.Add(ControloAutDicionario.DicionarioRow.Termo);
				li.Tag = TempRelacaoHierarquica;
				RaiseEventAddControloAut(li);
			}
		}

		private bool IsValidRelacaoHierarquica(GISADataset.ControloAutRow ControloAutRow)
		{

			if (GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1} OR ID={1} AND IDUpper={0}", ControloAutRow.GetNivelControloAutRows()[0].NivelRow.ID, this.FRDBase.NivelRow.ID)).Length > 0)
			{
				return false;
			}
			return true;
		}

	}

} //end of root namespace