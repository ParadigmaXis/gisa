using System;
using System.Collections;
using GISA.Model;
using com.lowagie.text;

namespace GISA.Reports {
	public class CatalogoMultinivel : Multinivel {
		public CatalogoMultinivel(string FileName, GISADataset.RelacaoHierarquicaRow[] rhRow) : base(FileName, rhRow) {
		}
		protected override void GenerateTitle(Document doc) {
			
			Paragraph p = new Paragraph("Catálogo", TitleFont);
			p.setAlignment(ElementConst.ALIGN_CENTER);
			doc.add(p);
		}
		protected override void GenerateInventarioEntry(Document doc, GISADataset.RelacaoHierarquicaRow rhRow, float CurrentIndentCm) {
			DoRemovedEntries(1);
			if (rhRow.TipoNivelRelacionadoRow.IDTipoNivel == TipoNivelRelacionado.D) {
				GenerateInventarioEntryDetails(doc, rhRow, CurrentIndentCm);
			}
			else {
				GenerateInventarioEntryChildren(doc, rhRow, CurrentIndentCm);
			}
		}
		private void GenerateInventarioEntryDetails(Document doc, GISADataset.RelacaoHierarquicaRow rhRow, float CurrentIndentCm) {
			string entry = string.Empty;
			Paragraph p;
			GISADataset.NivelRow n = rhRow.NivelRowByNivelRelacaoHierarquica;
			foreach (GISADataset.FRDBaseRow frd in rhRow.NivelRowByNivelRelacaoHierarquica.GetFRDBaseRows()) {
				if (frd.IDTipoFRDBase == (long)TipoFRDBase.FRDOIPublicacao) {
					entry = Nivel.GetCodigoOfNivel(n);
					p = new Paragraph(CentimeterToPoint(0.5F), entry, this.BodyFont);
					p.setIndentationLeft(CentimeterToPoint(0));
					doc.add(p);
					entry = string.Format("{0}: {1}", rhRow.TipoNivelRelacionadoRow.Codigo, Nivel.GetDesignacao(n));
					p = new Paragraph(CentimeterToPoint(0.5F), entry, this.BodyFont);
					p.setIndentationLeft(CentimeterToPoint(0.5F));
					doc.add(p);
					if (frd.GetSFRDDatasProducaoRows().Length > 0) {
						entry = "";
						if (!frd.GetSFRDDatasProducaoRows()[0].IsInicioTextoNull() && frd.GetSFRDDatasProducaoRows()[0].InicioTexto.Length > 0){
							entry += frd.GetSFRDDatasProducaoRows()[0].InicioTexto + ", ";
						}
						entry += GetInicioData(frd.GetSFRDDatasProducaoRows()[0]) + " - " + GetFimData(frd.GetSFRDDatasProducaoRows()[0]);
						p = new Paragraph(CentimeterToPoint(0.5F), entry, this.BodyFont);
						p.setIndentationLeft(CentimeterToPoint(1F));
						doc.add(p);
					}
					entry = "**Dimensão e suporte**";
					if (entry.Length > 0) {
						p = new Paragraph(entry, this.BodyFont);
						p.setIndentationLeft(CentimeterToPoint(1F));
						doc.add(p);
					}
					entry = GetControloAutFormaAut(frd, new TipoNoticiaAut[] {TipoNoticiaAut.EntidadeProdutora});
					//For Each idx As GISADataset.IndexFRDCARow In frd.GetIndexFRDCARows()
					//    If idx.ControloAutRow.IDTipoNoticiaAut = TipoNoticiaAut.EntidadeProdutora Then
					//        For Each cad As GISADataset.ControloAutDicionarioRow In idx.ControloAutRow.GetControloAutDicionarioRows
					//            If cad.IDTipoControloAutForma = TipoControloAutForma.FormaAutorizada Then
					//                If entry.Length > 0 Then entry += " / "
					//                entry += cad.DicionarioRow.Termo
					//            End If
					//        Next
					//    End If
					//Next
					if (entry.Length > 0) {
						p = new Paragraph(entry, this.BodyFont);
						p.setIndentationLeft(CentimeterToPoint(1));
						doc.add(p);
					}
					if (frd.GetSFRDContextoRows().Length == 1){
						entry = frd.GetSFRDContextoRows()[0].HistoriaAdministrativa;
						if (entry.Length > 0) {
							p = new Paragraph(entry, this.BodyFont);
							p.setIndentationLeft(CentimeterToPoint(1));
							doc.add(p);
						}
						entry = frd.GetSFRDContextoRows()[0].HistoriaCustodial;
						if (entry.Length > 0) {
							p = new Paragraph(entry, this.BodyFont);
							p.setIndentationLeft(CentimeterToPoint(1));
							doc.add(p);
						}
						entry = frd.GetSFRDContextoRows()[0].FonteImediataDeAquisicao;
						if (entry.Length > 0) {
							p = new Paragraph(entry, this.BodyFont);
							p.setIndentationLeft(CentimeterToPoint(1));
							doc.add(p);
						}
					}
					entry = GetControloAutFormaAut(frd, new TipoNoticiaAut[] {TipoNoticiaAut.TipologiaInformacional});
					//For Each idx As GISADataset.IndexFRDCARow In frd.GetIndexFRDCARows()
					//    If idx.ControloAutRow.IDTipoNoticiaAut = TipoNoticiaAut.TipologiaInformacional Then
					//        For Each cad As GISADataset.ControloAutDicionarioRow In idx.ControloAutRow.GetControloAutDicionarioRows
					//            If cad.IDTipoControloAutForma = TipoControloAutForma.FormaAutorizada Then
					//                If entry.Length > 0 Then entry += " / "
					//                entry += cad.DicionarioRow.Termo
					//            End If
					//        Next
					//    End If
					//Next
					if (entry.Length > 0) {
						p = new Paragraph(entry, this.BodyFont);
						p.setIndentationLeft(CentimeterToPoint(1));
						doc.add(p);
					}
					if (frd.GetSFRDConteudoEEstruturaRows().Length == 1) {
						entry = frd.GetSFRDConteudoEEstruturaRows()[0].ConteudoInformacional;
						if (entry.Length > 0) {
							p = new Paragraph(entry, this.BodyFont);
							p.setIndentationLeft(CentimeterToPoint(1));
							doc.add(p);
						}
					}
					if (frd.GetSFRDCondicaoDeAcessoRows().Length == 1) {
						entry = frd.GetSFRDCondicaoDeAcessoRows()[0].CondicaoDeReproducao;
						if (entry.Length > 0) {
							p = new Paragraph(entry, this.BodyFont);
							p.setIndentationLeft(CentimeterToPoint(1));
							doc.add(p);
						}
					}
					entry = GetControloAutFormaAut(frd, new TipoNoticiaAut[] {TipoNoticiaAut.Ideografico, TipoNoticiaAut.Onomastico, TipoNoticiaAut.ToponimicoGeografico});
					//For Each idx As GISADataset.IndexFRDCARow In frd.GetIndexFRDCARows()
					//    If idx.ControloAutRow.IDTipoNoticiaAut = TipoNoticiaAut.Ideografico Or idx.ControloAutRow.IDTipoNoticiaAut = TipoNoticiaAut.Onomastico Or idx.ControloAutRow.IDTipoNoticiaAut = TipoNoticiaAut.ToponimicoGeografico Then
					//        For Each cad As GISADataset.ControloAutDicionarioRow In idx.ControloAutRow.GetControloAutDicionarioRows
					//            If cad.IDTipoControloAutForma = TipoControloAutForma.FormaAutorizada Then
					//                If entry.Length > 0 Then entry += " / "
					//                entry += cad.DicionarioRow.Termo
					//            End If
					//        Next
					//    End If
					//Next
					if (entry.Length > 0) {
						p = new Paragraph(entry, this.BodyFont);
						p.setIndentationLeft(CentimeterToPoint(1));
						doc.add(p);
					}
				}
			}
		}
		private string GetControloAutFormaAut(GISADataset.FRDBaseRow frd, TipoNoticiaAut[] noticiaAut){
			ArrayList Results = new ArrayList();
			foreach (GISADataset.IndexFRDCARow idx in frd.GetIndexFRDCARows()) {
				if (Array.IndexOf(noticiaAut, System.Enum.ToObject(typeof(TipoNoticiaAut), idx.ControloAutRow.IDTipoNoticiaAut)) >= 0) {
					foreach (GISADataset.ControloAutDicionarioRow cad in idx.ControloAutRow.GetControloAutDicionarioRows()) {
						if (cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada) {
							Results.Add(cad.DicionarioRow.Termo);
						}
					}
				}
			}
			Results.Sort();
			
			string Result = "";
			foreach (string s in Results) {
				if (Result.Length > 0)
					Result += " / ";
				Result += s;
			}
			return Result;
		}
	}
}
