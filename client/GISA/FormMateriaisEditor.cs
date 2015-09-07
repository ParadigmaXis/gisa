using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
	public class FormMateriaisEditor : FormDomainValueEditor
	{

	#region  Windows Form Designer generated code 

		public FormMateriaisEditor() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call

		}

		//Form overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components = null;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			//
			//FormMateriaisEditor
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(466, 319);
			this.Name = "FormMateriaisEditor";
			this.Text = "Edição de materiais";

		}

	#endregion

		// Antes de adicionar um novo elemento é verificado se foi eliminado um com uma designação igual. Se for esse o caso, a linha apagada é 
		// reaproveitada evitando possíveis futuros conflitos aquando da gravação de dados
		protected override DataRow GetNewDataRow(string Value)
		{
			GISADataset.TipoAcondicionamentoRow row = null;
			GISADataset.TipoAcondicionamentoRow[] deletedRows = (GISADataset.TipoAcondicionamentoRow[])(GisaDataSetHelper.GetInstance().TipoAcondicionamento.Select(string.Format("Designacao='{0}'", Value), "", DataViewRowState.Deleted));
			GISADataset.TipoAcondicionamentoRow[] currentRows = (GISADataset.TipoAcondicionamentoRow[])(GisaDataSetHelper.GetInstance().TipoAcondicionamento.Select(string.Format("Designacao='{0}'", Value)));
			if (deletedRows.Length > 0)
			{
				deletedRows[0].RejectChanges();
				row = deletedRows[0];
			}
			else if (currentRows.Length > 0)
			{
				// Já existe uma row com esta designacao. Não criamos nem ressuscitamos nenhuma.
				row = null;
			}
			else
			{
				row = GisaDataSetHelper.GetInstance().TipoAcondicionamento.NewTipoAcondicionamentoRow();
				row.Designacao = Value;
				GisaDataSetHelper.GetInstance().TipoAcondicionamento.AddTipoAcondicionamentoRow(row);
			}
			return row;
		}

		protected override DataRow GetUpdatedDataRow(DataRow row, string NewValue)
		{

			GISADataset.TipoAcondicionamentoRow TQRow = (GISADataset.TipoAcondicionamentoRow)row;
			TQRow.Designacao = NewValue;
			return TQRow;
		}

		protected override DataRow[] GetRowDependencies(DataRow row)
		{
			GISADataset.TipoAcondicionamentoRow taRow = (GISADataset.TipoAcondicionamentoRow)row;

			GISADataset.SFRDUFDescricaoFisicaRow[] relatedRowsUF = (GISADataset.SFRDUFDescricaoFisicaRow[])(GisaDataSetHelper.GetInstance().SFRDUFDescricaoFisica. Select("IDTipoAcondicionamento = " + taRow.ID.ToString()));

			return relatedRowsUF;
		}

		protected override string GetDependenciesReport(DataRow[] rows)
		{
			string report = null;
			foreach (DataRow dependentRow in rows)
			{
				if (report == null)
				{
					report = "";
				}
				report += "  " + TipoNivelRelacionado.GetTipoNivelRelacionadoDaPrimeiraRelacaoEncontrada(((GISADataset.SFRDUFDescricaoFisicaRow)dependentRow).FRDBaseRow.NivelRow).Designacao + ": " + Nivel.GetDesignacao(((GISADataset.SFRDUFDescricaoFisicaRow)dependentRow).FRDBaseRow.NivelRow) + Environment.NewLine;
			}
			if (report != null)
			{
				report = "Remover este material fará com que todas as suas referências passem a \"" + ((GISADataset.TipoSuporteRow)(GisaDataSetHelper.GetInstance().TipoSuporte.Select("ID = 1")[0])).Designacao + "\"" + Environment.NewLine + "Foram encontrados os seguintes níveis associados a " + "este material:" + Environment.NewLine + Environment.NewLine + report;
			}
			return report;
		}
	}

} //end of root namespace