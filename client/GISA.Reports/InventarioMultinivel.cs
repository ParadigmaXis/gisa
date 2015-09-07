using System;
using GISA.Model;
using com.lowagie.text;

namespace GISA.Reports {
	public class InventarioMultinivel : Multinivel {
		public InventarioMultinivel(string FileName, GISADataset.RelacaoHierarquicaRow[] rhRow) : base(FileName, rhRow) {
		}
		protected override void GenerateTitle(Document doc) {
			
			Paragraph p = new Paragraph("Inventário", TitleFont);
			p.setAlignment(ElementConst.ALIGN_CENTER);
			doc.add(p);
			p = new Paragraph("Listagem dos Documentos", SubTitleFont);
			p.setAlignment(ElementConst.ALIGN_CENTER);
			p.setLeading(CentimeterToPoint(1));
			doc.add(p);
			doc.add(new Paragraph(""));
		}
		protected override void GenerateInventarioEntry(Document doc, GISADataset.RelacaoHierarquicaRow rhRow, float CurrentIndentCm) {
			
			GISADataset.NivelRow n = rhRow.NivelRowByNivelRelacaoHierarquica;
			
			string entry = string.Format("{0}: {1} - {2}", rhRow.TipoNivelRelacionadoRow.Codigo, rhRow.NivelRowByNivelRelacaoHierarquica.Codigo, Nivel.GetDesignacao(n));
			
			Paragraph p = new Paragraph(entry, this.BodyFont);
			p.setIndentationLeft(CentimeterToPoint(CurrentIndentCm));
			doc.add(p);
			DoRemovedEntries(1);
			if (!rhRow.TipoNivelRelacionadoRow.TipoNivelRow.IsDocument) {
				GenerateInventarioEntryChildren(doc, rhRow, CurrentIndentCm);
			}
			else {
				GenerateInventarioEntryDetails(doc, rhRow, CurrentIndentCm);
			}
		}
		private void GenerateInventarioEntryDetails(Document doc, GISADataset.RelacaoHierarquicaRow rhRow, float CurrentIndentCm) {
			
			string entry = "";
			
			Paragraph p;
			GisaDataSetHelper.GetFRDBaseDataAdapter(string.Format("WHERE IDNivel={0}", rhRow.ID), null, null).Fill(dataSet.FRDBase);
			GisaDataSetHelper.GetSFRDDatasProducaoDataAdapter(string.Format("INNER JOIN FRDBase ON SFRDDatasProducao.IDFRDBase=FRDBase.ID WHERE IDNivel={0}", rhRow.ID), null, null).Fill(dataSet.SFRDDatasProducao);
			GisaDataSetHelper.GetSFRDUFCotaDataAdapter(string.Format("INNER JOIN FRDBase ON SFRDUFCota.IDFRDBase=FRDBase.ID WHERE IDNivel={0}", rhRow.ID), null, null).Fill(dataSet.SFRDUFCota);
			//PersistencyHelper.cleanDeletedRows()
			foreach (GISADataset.FRDBaseRow frd in rhRow.NivelRowByNivelRelacaoHierarquica.GetFRDBaseRows()) {
				if (frd.IDTipoFRDBase == (long)TipoFRDBase.FRDOIPublicacao) {
					if (frd.GetSFRDDatasProducaoRows().Length > 0) { {
						if (!frd.GetSFRDDatasProducaoRows()[0].IsInicioTextoNull() && frd.GetSFRDDatasProducaoRows()[0].InicioTexto.Length > 0) {
							entry += frd.GetSFRDDatasProducaoRows()[0].InicioTexto + ", ";
						}
						entry += GetInicioData(frd.GetSFRDDatasProducaoRows()[0]) + " - " + GetFimData(frd.GetSFRDDatasProducaoRows()[0]);
						p = new Paragraph(entry, this.BodyFont);
						p.setIndentationLeft(CentimeterToPoint(CurrentIndentCm + 0.5f));
						doc.add(p);
					}
					}
					entry = frd.GetSFRDUFCotaRows()[0].Cota;
					p = new Paragraph(entry, this.BodyFont);
					p.setIndentationLeft(CentimeterToPoint(CurrentIndentCm + 0.5f));
					doc.add(p);
					entry = Nivel.GetCodigoOfNivel(rhRow.NivelRowByNivelRelacaoHierarquica);
					p = new Paragraph(entry, this.BodyFont);
					p.setIndentationLeft(CentimeterToPoint(CurrentIndentCm + 0.5f));
					doc.add(p);
				}
			}
		}
	}
}
