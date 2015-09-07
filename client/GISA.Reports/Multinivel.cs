using System;
using System.Data;
using GISA.Model;
using com.lowagie.text;

namespace GISA.Reports {
	public abstract class Multinivel : Relatorio {
		private GISADataset.RelacaoHierarquicaRow[] mRelacaoHierarquicaRows;
		public Multinivel(string FileName, GISADataset.RelacaoHierarquicaRow[] rhRows) : base(FileName) {
			this.mRelacaoHierarquicaRows = rhRows;
		}

		private void DataRowChanged(object sender, DataRowChangeEventArgs e) {
			DoAddedEntries(0);
		}

		public override void GeneratePdf() {
			dataSet = new GISADataset();
			foreach (DataTable t in dataSet.Tables) {
				t.RowChanged += new DataRowChangeEventHandler(this.DataRowChanged);
			}
			GisaDataSetHelper.LoadStaticDataTables(dataSet);
			
			Document doc;
			doc = new Document(PageSize.A4, CentimeterToPoint(2.5F), CentimeterToPoint(2.5F), CentimeterToPoint(2.5F), CentimeterToPoint(2.5F));
			com.lowagie.text.pdf.PdfWriter.getInstance(doc, new java.io.FileOutputStream(mFileName, false));
			Generate(doc, mRelacaoHierarquicaRows);
			doc.close();
			dataSet = null;
		}
		protected void Generate(Document doc, GISADataset.RelacaoHierarquicaRow[] rhRows) {
			string queryN = "";
			string queryRH = "";

			foreach (GISADataset.RelacaoHierarquicaRow r in rhRows) {
				if (queryN.Length > 0)
					queryN += ", ";
				queryN += string.Format("{0}", r.NivelRowByNivelRelacaoHierarquica.ID);
				if (queryRH.Length > 0)
					queryRH += " OR ";
				queryRH += string.Format("(ID={0} AND IDUpper={1})", r.ID, r.IDUpper);
			}

			GisaDataSetHelper.GetNivelDataAdapter(string.Format("WHERE ID IN ({0})", queryN), null, null).Fill(dataSet.Nivel);
			GisaDataSetHelper.GetRelacaoHierarquicaDataAdapter(string.Format("WHERE {0}", queryRH), null, null).Fill(dataSet.RelacaoHierarquica);
			//PersistencyHelper.cleanDeletedRows()
			rhRows = (GISADataset.RelacaoHierarquicaRow[])dataSet.RelacaoHierarquica.Select("");
			
			java.awt.Color hfcolor = new java.awt.Color(128, 128, 128);
			
			Font hffont = new Font(Font.HELVETICA, 6, Font.ITALIC, hfcolor);
			
			HeaderFooter header = new HeaderFooter(new Phrase("Gestão Integrada de Sistemas de Arquivo", hffont), false);
			header.setAlignment(ElementConst.ALIGN_CENTER);
			header.setBorder(2);
			// iTextSharp.text.Rectangle.BOTTOM
			header.setBorderColor(hfcolor);
			doc.setHeader(header);
			
			HeaderFooter footer = new HeaderFooter(new Phrase("Câmara Municipal do Porto - Departamento de Arquivos - ", hffont), true);
			footer.setAlignment(ElementConst.ALIGN_CENTER);
			footer.setBorder(1);
			// iTextSharp.text.Rectangle.TOP
			footer.setBorderColor(hfcolor);
			doc.setFooter(footer);
			// Headers and footers apply to next page...
			doc.open();
			GenerateTitle(doc);
			
			float CurrentIndentCm = 0;
			DoAddedEntries(rhRows.Length);
			Array.Sort(rhRows, new NivelSorter());
			foreach (GISADataset.RelacaoHierarquicaRow rhRow in rhRows) {
				GenerateInventarioEntry(doc, rhRow, CurrentIndentCm);
			}
		}
		protected abstract void GenerateTitle(Document doc);
		protected abstract void GenerateInventarioEntry(Document doc, GISADataset.RelacaoHierarquicaRow rhRow, float CurrentIndentCm);
		protected void GenerateInventarioEntryChildren(Document doc, GISADataset.RelacaoHierarquicaRow rhRow, float CurrentIndentCm) {
			
			GISADataset.RelacaoHierarquicaRow[] childRhRows;
			try { {
				dataSet.EnforceConstraints = false;
				GisaDataSetHelper.GetNivelDataAdapter(string.Format("WHERE ID IN (SELECT ID FROM RelacaoHierarquica WHERE IDUpper={0})", rhRow.ID), null, null).Fill(dataSet.Nivel);
				GisaDataSetHelper.GetRelacaoHierarquicaDataAdapter(string.Format("WHERE IDUpper={0}", rhRow.ID), null, null).Fill(dataSet.RelacaoHierarquica);
				//PersistencyHelper.cleanDeletedRows()
				childRhRows = Nivel.GetChildren(dataSet, rhRow.NivelRowByNivelRelacaoHierarquica);
				if (childRhRows.Length == 0)
					return;// might not be correct. Was : Exit Sub
				DoAddedEntries(childRhRows.Length);
				GisaDataSetHelper.GetNivelDesignadoDataAdapter(string.Format("WHERE ID IN (SELECT ID FROM RelacaoHierarquica WHERE IDUpper={0})", rhRow.ID), null, null).Fill(dataSet.NivelDesignado);
				GisaDataSetHelper.GetControloAutDataAdapter(string.Format("WHERE ID IN (SELECT NivelControloAut.IDControloAut FROM RelacaoHierarquica inner join NivelControloAut ON RelacaoHierarquica.ID=NivelControloAut.ID WHERE IDUpper={0})", rhRow.ID), null, null).Fill(dataSet.ControloAut);
				GisaDataSetHelper.GetNivelControloAutDataAdapter(string.Format("WHERE ID IN (SELECT ID FROM RelacaoHierarquica WHERE IDUpper={0})", rhRow.ID), null, null).Fill(dataSet.NivelControloAut);
				GisaDataSetHelper.GetDicionarioDataAdapter(string.Format("WHERE ID IN (SELECT IDDicionario FROM ControloAutDicionario INNER JOIN NivelControloAut ON ControloAutDicionario.IDControloAut=NivelControloAut.IDControloAut INNER JOIN RelacaoHierarquica ON NivelControloAut.ID=RelacaoHierarquica.ID WHERE IDUpper={0})", rhRow.ID), null, null).Fill(dataSet.Dicionario);
				GisaDataSetHelper.GetControloAutDicionarioDataAdapter(string.Format("WHERE IDControloAut IN (SELECT IDControloAut FROM NivelControloAut INNER JOIN RelacaoHierarquica ON NivelControloAut.ID=RelacaoHierarquica.ID WHERE IDUpper={0})", rhRow.ID), null, null).Fill(dataSet.ControloAutDicionario);
					
				string query = string.Format("WHERE FRDBase.IDTipoFRDBase=3 AND FRDBase.IDNivel IN (SELECT ID FROM RelacaoHierarquica WHERE IDUpper={0})", rhRow.ID);
				GisaDataSetHelper.GetFRDBaseDataAdapter(query, null, null).Fill(dataSet.FRDBase);
				GisaDataSetHelper.GetSFRDDatasProducaoDataAdapter(string.Format("INNER JOIN FRDBase ON SFRDDatasProducao.IDFRDBase=FRDBase.ID {0}", query), null, null).Fill(dataSet.SFRDDatasProducao);
				GisaDataSetHelper.GetSFRDUFCotaDataAdapter(string.Format("INNER JOIN FRDBase ON SFRDUFCota.IDFRDBase=FRDBase.ID {0}", query), null, null).Fill(dataSet.SFRDUFCota);
				GisaDataSetHelper.GetSFRDConteudoEEstruturaDataAdapter(string.Format("INNER JOIN FRDBase ON SFRDConteudoEEstrutura.IDFRDBase=FRDBase.ID {0}", query), null, null).Fill(dataSet.SFRDConteudoEEstrutura);
				GisaDataSetHelper.GetSFRDCondicaoDeAcessoDataAdapter(string.Format("INNER JOIN FRDBase ON SFRDCondicaoDeAcesso.IDFRDBase=FRDBase.ID {0}", query), null, null).Fill(dataSet.SFRDCondicaoDeAcesso);
				GisaDataSetHelper.GetSFRDContextoDataAdapter(string.Format("INNER JOIN FRDBase ON SFRDContexto.IDFRDBase=FRDBase.ID {0}", query), null, null).Fill(dataSet.SFRDContexto);
				GisaDataSetHelper.GetControloAutDataAdapter(string.Format("INNER JOIN IndexFRDCA ON ControloAut.ID=IndexFRDCA.IDControloAut INNER JOIN FRDBase ON IDFRDBase=FRDBase.ID {0}", query), null, null).Fill(dataSet.ControloAut);
				GisaDataSetHelper.GetIndexFRDCADataAdapter(string.Format("INNER JOIN FRDBase ON IDFRDBase=FRDBase.ID {0}", query), null, null).Fill(dataSet.IndexFRDCA);
				GisaDataSetHelper.GetDicionarioDataAdapter(string.Format("WHERE ID IN (SELECT IDDicionario FROM ControloAutDicionario INNER JOIN IndexFRDCA ON ControloAutDicionario.IDControloAut=IndexFRDCA.IDControloAut INNER JOIN FRDBase ON IndexFRDCA.IDFRDBase=FRDBase.ID {0})", query), null, null).Fill(dataSet.Dicionario);
				GisaDataSetHelper.GetControloAutDicionarioDataAdapter(string.Format("WHERE IDControloAut IN (SELECT IDControloAut FROM IndexFRDCA INNER JOIN FRDBase ON IndexFRDCA.IDFRDBase=FRDBase.ID {0})", query), null, null).Fill(dataSet.ControloAutDicionario);
			}
			} 
			finally { {
				//PersistencyHelper.cleanDeletedRows()

				dataSet.EnforceConstraints = true;
			}
			}
			Array.Sort(childRhRows, new NivelSorter());
			foreach (GISADataset.RelacaoHierarquicaRow childn in childRhRows) {
				GenerateInventarioEntry(doc, childn, CurrentIndentCm + 0.5f);
			}
		}
	}
}
