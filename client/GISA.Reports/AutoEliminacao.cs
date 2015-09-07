using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using GISA.Model;
using iTextSharp.text;

namespace GISA.Reports {
	public class AutoEliminacao : Relatorio {

		private GISADataset.AutoEliminacaoRow mAutoEliminacaoRow;
		public AutoEliminacao(string FileName, GISADataset.AutoEliminacaoRow aeRow, long IDTrustee) : base(FileName, IDTrustee) {
			this.mAutoEliminacaoRow = aeRow;
		}

		private enum AutoEliminacaoSeriesColumns {
			ID = 0,
			Designacao = 1,
			ProducaoInicioAno = 2,
			ProducaoInicioMes = 3,
			ProducaoInicioDia = 4,
			ProducaoFimAno = 5,
			ProducaoFimMes = 6,
			ProducaoFimDia = 7,
			Preservar = 8,
			
		}
		private enum AutoEliminacaoUnidadesFisicasColumns {
			ID = 0,
			Designacao = 1,
			Cota = 2,
			TipoAcondicionamento = 3,
			MedidaLargura = 4,
			ProducaoInicioAno = 5,
			ProducaoInicioMes = 6,
			ProducaoInicioDia = 7,
			ProducaoFimAno = 8,
			ProducaoFimMes = 9,
			ProducaoFimDia = 10
		}
		private enum AutoEliminacaoDocumentosColumns {
			ID = 0,
			IDSerie = 1,
			Designacao = 2,
			ProducaoInicioAno = 3,
			ProducaoInicioMes = 4,
			ProducaoInicioDia = 5,
			ProducaoFimAno = 6,
			ProducaoFimMes = 7,
			ProducaoFimDia = 8,
			Preservar = 9,
			
		}
		private Table conteudosTable = null;
		private Hashtable series = new Hashtable();
		private Hashtable unidadesFisicas = new Hashtable();
		private Hashtable documentos = new Hashtable();

		protected override void LoadContents(IDbConnection connection, ref IDataReader reader) { 
			if (mAutoEliminacaoRow == null){
				throw new ArgumentNullException("Auto de eliminação não especificado");
			}

			reader = DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.ReportAutoEliminacao(this.mIDTrustee, mAutoEliminacaoRow.ID, connection);
				
			Serie sr;
			UnidadeFisica uf;
			Documento docm;
				
			long ID = -1;
			// carregamento de séries
			while (reader.Read()) {
				ID = System.Convert.ToInt64(reader.GetValue((int)AutoEliminacaoSeriesColumns.ID));
				sr = new Serie(ID);
				series.Add(ID, sr);
				sr.Designacao = reader.GetString((int)AutoEliminacaoSeriesColumns.Designacao);
				sr.DataProducao = GISA.Utils.GUIHelper.FormatDateInterval(
					GISA.Utils.GUIHelper.FormatDate(
						GisaDataSetHelper.GetDBNullableText(ref reader, (int)AutoEliminacaoSeriesColumns.ProducaoInicioAno), 
						GisaDataSetHelper.GetDBNullableText(ref reader, (int)AutoEliminacaoSeriesColumns.ProducaoInicioMes), 
						GisaDataSetHelper.GetDBNullableText(ref reader, (int)AutoEliminacaoSeriesColumns.ProducaoInicioDia)), 
					GISA.Utils.GUIHelper.FormatDate(
						GisaDataSetHelper.GetDBNullableText(ref reader, (int)AutoEliminacaoSeriesColumns.ProducaoFimAno), 
						GisaDataSetHelper.GetDBNullableText(ref reader, (int)AutoEliminacaoSeriesColumns.ProducaoFimMes), 
						GisaDataSetHelper.GetDBNullableText(ref reader, (int)AutoEliminacaoSeriesColumns.ProducaoFimDia)));
                sr.Preservar = GisaDataSetHelper.GetDBNullableBoolean(ref reader, (int)AutoEliminacaoSeriesColumns.Preservar);
                DoAddedEntries(1);
			}

			reader.NextResult();

			// carregamento de unidades físicas
			while (reader.Read()) {
				ID = System.Convert.ToInt64(reader.GetValue((int)AutoEliminacaoUnidadesFisicasColumns.ID));
				uf = new UnidadeFisica(ID);
				unidadesFisicas.Add(ID, uf);
				uf.Designacao = reader.GetValue((int)AutoEliminacaoUnidadesFisicasColumns.Designacao).ToString();
				uf.Cota = GisaDataSetHelper.GetDBNullableText(ref reader, (int)AutoEliminacaoUnidadesFisicasColumns.Cota);
				uf.TipoAcondicionamento = GisaDataSetHelper.GetDBNullableText(ref reader, (int)AutoEliminacaoUnidadesFisicasColumns.TipoAcondicionamento);
				string medidaLargura = GisaDataSetHelper.GetDBNullableText(ref reader, (int)AutoEliminacaoUnidadesFisicasColumns.MedidaLargura);
				if (medidaLargura.Length != 0)
					uf.MedidaLargura = System.Convert.ToDecimal(medidaLargura);
				uf.DataProducao = GISA.Utils.GUIHelper.FormatDateInterval(
					GISA.Utils.GUIHelper.FormatDate(
					GisaDataSetHelper.GetDBNullableText(ref reader, (int)AutoEliminacaoUnidadesFisicasColumns.ProducaoInicioAno), 
					GisaDataSetHelper.GetDBNullableText(ref reader, (int)AutoEliminacaoUnidadesFisicasColumns.ProducaoInicioMes), 
					GisaDataSetHelper.GetDBNullableText(ref reader, (int)AutoEliminacaoUnidadesFisicasColumns.ProducaoInicioDia)), 
					GISA.Utils.GUIHelper.FormatDate(
					GisaDataSetHelper.GetDBNullableText(ref reader, (int)AutoEliminacaoUnidadesFisicasColumns.ProducaoFimAno), 
					GisaDataSetHelper.GetDBNullableText(ref reader, (int)AutoEliminacaoUnidadesFisicasColumns.ProducaoFimMes), 
					GisaDataSetHelper.GetDBNullableText(ref reader, (int)AutoEliminacaoUnidadesFisicasColumns.ProducaoFimDia)));
                DoAddedEntries(1);
			}
			reader.NextResult();
			
			// carregamento de documentos
			while (reader.Read()) {
				ID = System.Convert.ToInt64(reader.GetValue((int)AutoEliminacaoDocumentosColumns.ID));
				docm = new Documento(ID);
				documentos.Add(ID, docm);
				if (!object.ReferenceEquals(reader.GetValue((int)AutoEliminacaoDocumentosColumns.IDSerie), DBNull.Value)) {
					sr = (Serie)series[System.Convert.ToInt64(reader.GetValue((int)AutoEliminacaoDocumentosColumns.IDSerie))];
					if (sr.Documentos == null) {
						sr.Documentos = new ArrayList();
					}
					sr.Documentos.Add(docm);
					docm.Serie = sr;
				}
				docm.Designacao = reader.GetString((int)AutoEliminacaoDocumentosColumns.Designacao);
				docm.DataProducao = GISA.Utils.GUIHelper.FormatDateInterval(
					GISA.Utils.GUIHelper.FormatDate(
					GisaDataSetHelper.GetDBNullableText(ref reader, (int)AutoEliminacaoDocumentosColumns.ProducaoInicioAno), 
					GisaDataSetHelper.GetDBNullableText(ref reader, (int)AutoEliminacaoDocumentosColumns.ProducaoInicioMes), 
					GisaDataSetHelper.GetDBNullableText(ref reader, (int)AutoEliminacaoDocumentosColumns.ProducaoInicioDia)), 
					GISA.Utils.GUIHelper.FormatDate(
					GisaDataSetHelper.GetDBNullableText(ref reader, (int)AutoEliminacaoDocumentosColumns.ProducaoFimAno), 
					GisaDataSetHelper.GetDBNullableText(ref reader, (int)AutoEliminacaoDocumentosColumns.ProducaoFimMes), 
					GisaDataSetHelper.GetDBNullableText(ref reader, (int)AutoEliminacaoDocumentosColumns.ProducaoFimDia)));
				
				docm.Preservar = System.Convert.ToBoolean(reader.GetValue((int)AutoEliminacaoDocumentosColumns.Preservar));
                DoAddedEntries(1);
			}
			reader.NextResult();
				
			// carregamento de entidades produtoras
			string DesignacaoEP = null;
			while (reader.Read()) {
				ID = System.Convert.ToInt64(reader.GetValue(0));
				DesignacaoEP = reader.GetString(1);
				sr = (Serie)series[ID];
				docm = (Documento)documentos[ID];
				if (sr != null) {
					if (sr.EntidadesProdutoras == null) {
						sr.EntidadesProdutoras = new ArrayList();
					}
					sr.EntidadesProdutoras.Add(DesignacaoEP);
				} else if (docm != null) {
					if (docm.EntidadesProdutoras == null) {
						docm.EntidadesProdutoras = new ArrayList();
					}
					docm.EntidadesProdutoras.Add(DesignacaoEP);
				}
                DoAddedEntries(1);
			}
			reader.NextResult();

			// carregamento das associações entre unidades fisicas e unidades de descrição
			long IDDocumental = -1;
			while (reader.Read()) {
				ID = System.Convert.ToInt64(reader.GetValue(0));
				string IDDoc = GisaDataSetHelper.GetDBNullableText(ref reader, 2);
				if (IDDoc.Length != 0)
					IDDocumental = System.Convert.ToInt64(IDDoc);
				uf = (UnidadeFisica)unidadesFisicas[ID];
				sr = (Serie)series[IDDocumental];
				docm = (Documento)documentos[IDDocumental];
				if (sr != null) {
					if (uf.Series == null) {
						uf.Series = new ArrayList();
					}
					uf.Series.Add(sr);
					if (sr.UnidadesFisicas == null) {
						sr.UnidadesFisicas = new ArrayList();
					}
					sr.UnidadesFisicas.Add(uf);
				} else if (docm != null) {
					if (uf.Documentos == null) {
						uf.Documentos = new ArrayList();
					}
					uf.Documentos.Add(docm);
					if (docm.UnidadesFisicas == null) {
						docm.UnidadesFisicas = new ArrayList();
					}
					docm.UnidadesFisicas.Add(uf);

                    if (docm.Serie == null) //documento solto
                    {
                        if (docm.UnidadesFisicas == null)
                            docm.UnidadesFisicas = new ArrayList();

                        if (!docm.UnidadesFisicas.Contains(uf))
                            docm.UnidadesFisicas.Add(uf); // informação inferida de que se a unidade fisica pertence ao documento então também pertencerá certamente à série, mesmo que isso não tenha sido descrito
                    }
                    else //documento que constitui uma série
                    {
                        if (docm.Serie.UnidadesFisicas == null)
                            docm.Serie.UnidadesFisicas = new ArrayList();
                        
                        if (!docm.Serie.UnidadesFisicas.Contains(uf))
                            docm.Serie.UnidadesFisicas.Add(uf); // informação inferida de que se a unidade fisica pertence ao documento então também pertencerá certamente à série, mesmo que isso não tenha sido descrito
                    }
				}
                DoAddedEntries(1);
			}
		}

		protected override void FillContents() {
            Dictionary<UnidadeFisica, UnidadeFisica> ufsAdicionadas = new Dictionary<UnidadeFisica, UnidadeFisica>();
			// para cada série/documento solto
			foreach (Serie sr in series.Values) {
                AddSerie(base.mDoc, 
					sr.Designacao, 
					sr.DataProducao, 					
					sr.EntidadesProdutoras);

				DoRemovedEntries(1);

				conteudosTable = null;
				conteudosTable = createUnidadesFisicasTable(true); //firstTime);

				ArrayList documentosComUnidadeFisica = new ArrayList();

				if (!object.ReferenceEquals(sr.UnidadesFisicas, null) && sr.UnidadesFisicas.Count > 0) {
					foreach (UnidadeFisica uf in sr.UnidadesFisicas) {
                        AddUnidadeFisica(base.mDoc, uf.Cota, uf.Designacao, uf.TipoAcondicionamento, uf.MedidaLargura, uf.DataProducao, true);//!firstTime);
                        if (!ufsAdicionadas.ContainsKey(uf)) ufsAdicionadas.Add(uf, uf);
						DoRemovedEntries(1);
						if (!object.ReferenceEquals(uf.Documentos, null) && uf.Documentos.Count > 0) {
							// adicionar os documentos desta unidade fisica que pertençam à série em causa
							foreach (Documento docm in uf.Documentos) {
								if (sr.Documentos.Contains(docm)){
									documentosComUnidadeFisica.Add(docm);
                                    AddDocumento(base.mDoc, docm.Designacao, docm.DataProducao);
									DoRemovedEntries(1);
								}
							}
						}
					}
				}

				if (sr.Documentos != null)
				{
					foreach (Documento docm in sr.Documentos) 
					{
						// adicionar no final da listagem os documentos que não tenham 
						// unidade física associada
						if (!documentosComUnidadeFisica.Contains(docm)) 
						{
                            AddDocumento(base.mDoc, docm.Designacao, docm.DataProducao, true);
							DoRemovedEntries(1);
						}
					}
				}
                base.mDoc.Add(conteudosTable);
				conteudosTable = null;
			}

            foreach (Documento ds in documentos.Values)
            {
                if (ds.Serie == null)
                {
                    AddSerie(base.mDoc, ds.Designacao, ds.DataProducao, ds.EntidadesProdutoras);
                    DoRemovedEntries(1);

                    conteudosTable = null;
                    conteudosTable = createUnidadesFisicasTable(true); //firstTime);

                    ArrayList documentosComUnidadeFisica = new ArrayList();

                    if (!object.ReferenceEquals(ds.UnidadesFisicas, null) && ds.UnidadesFisicas.Count > 0)
                    {
                        foreach (UnidadeFisica uf in ds.UnidadesFisicas)
                        {
                            AddUnidadeFisica(base.mDoc, uf.Cota, uf.Designacao, uf.TipoAcondicionamento, uf.MedidaLargura, uf.DataProducao, true);//!firstTime);
                            DoRemovedEntries(1);
                        }
                    }

                    base.mDoc.Add(conteudosTable);
                    conteudosTable = null;
                }
            }

            if (unidadesFisicas.Count > 0 && unidadesFisicas.Count > ufsAdicionadas.Count)
            {
                AddParagraph(base.mDoc, "Unidades físicas sem níveis documentais associados");

                conteudosTable = null;
                conteudosTable = createUnidadesFisicasTable(true); //firstTime);

                foreach (UnidadeFisica uf in unidadesFisicas.Values)
                {
                    if (!ufsAdicionadas.ContainsKey(uf))
                        AddUnidadeFisica(base.mDoc, uf.Cota, uf.Designacao, uf.TipoAcondicionamento, uf.MedidaLargura, uf.DataProducao, true);//!firstTime);
                }

                base.mDoc.Add(conteudosTable);
                conteudosTable = null;
            }

		}

        private void AddParagraph(Document doc, string text)
        {
            Paragraph p;
            p = new Paragraph(CentimeterToPoint(0.5F), text, this.BodyFont);
            p.IndentationLeft = CentimeterToPoint(0);
            p.SpacingBefore = 8f;
            p.SpacingAfter = 4f;
            doc.Add(p);
        }

		private void AddSerie(Document doc, string designacao, string datasProducao, ArrayList entidadesProdutoras) {
            AddParagraph(doc, designacao);

            Table serieTable = new Table(2, 5);
            serieTable.BorderColor = iTextSharp.text.Color.WHITE;
            serieTable.DefaultCellBorderColor = iTextSharp.text.Color.WHITE;
			serieTable.Offset = 0f;
			serieTable.Widths = new float[] {28, 72};
            AddNewCell(serieTable, "Datas de produção:", this.HeaderFont);
            AddNewCell(serieTable, datasProducao, this.ContentFont);
            AddNewCell(serieTable, "Entidade(s) produtora(s):", this.HeaderFont);

            System.Text.StringBuilder designacoesEPs = new System.Text.StringBuilder();
			if (entidadesProdutoras != null) {
				foreach (string designacaoEP in entidadesProdutoras) {
					if (designacoesEPs.Length > 0)
						designacoesEPs.Append("; ");

					designacoesEPs.Append(designacaoEP);
				}
			}
			AddNewCell(serieTable, designacoesEPs.ToString(), this.ContentFont);
            AddTable(doc, serieTable);
		}

		private Table createUnidadesFisicasTable(bool includeHeader) {
			Table tab = new Table(4);
			tab.BorderWidth = 0;
			tab.DefaultCellBorderColor = iTextSharp.text.Color.BLACK;
			tab.Width = 95;
			tab.Widths = new float[] {52, 19, 10, 10};
			tab.Alignment = Element.ALIGN_RIGHT;
			tab.Padding = 3;
			tab.Spacing = 0;
			if (includeHeader) {
				AddNewCell(tab, "Designação", this.HeaderFont);
                AddNewCell(tab, "Produção", this.HeaderFont);
                AddNewCell(tab, "Largura", this.HeaderFont);
                AddNewCell(tab, "Cota", this.HeaderFont);
				tab.EndHeaders();
				// assim o cabeçalho aparece multiplas vezes no caso de a tabela ter de ser dividida por várias páginas
			}
			return tab;
		}

		private void AddUnidadeFisica(Document doc, string cota, string designacao, string tipoAcondicionamento, decimal largura, string datasProducao, bool addDivider) {
			if (addDivider)
				addDividerRow(conteudosTable);
			
			Cell cell;
			System.Drawing.Color veryLightGray;
			veryLightGray = System.Drawing.Color.Gainsboro;
			cell = new Cell(new Phrase(designacao, this.ContentFont));
            cell.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            AddNewCell(conteudosTable, cell);
			cell = new Cell(new Phrase(datasProducao, this.ContentFont));
            cell.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
			cell.HorizontalAlignment = Element.ALIGN_CENTER;
            AddNewCell(conteudosTable, cell);
			cell = new Cell(new Phrase(largura.ToString("0.000"), this.ContentFont));
            cell.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            AddNewCell(conteudosTable, cell);
			cell = new Cell(new Phrase(cota, this.ContentFont));
            cell.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            AddNewCell(conteudosTable, cell);
		}

		private void AddDocumento(Document doc, string designacao, string datasProducao) {
			AddDocumento(doc, designacao, datasProducao, false);
		}

		private void AddDocumento(Document doc, string designacao, string datasProducao, bool addDivider) { 
			if (addDivider)
				addDividerRow(conteudosTable);
			
			Cell cell;
			cell = new Cell(new Phrase(designacao, this.ContentFont));
			AddNewCell(conteudosTable, cell);
			cell = new Cell(new Phrase(datasProducao, this.ContentFont));
			cell.HorizontalAlignment = Element.ALIGN_CENTER;
            AddNewCell(conteudosTable, cell);
			cell = new Cell(new Phrase("", this.ContentFont));
			cell.Colspan = 2;
			cell.BorderWidth = 0;
            AddNewCell(conteudosTable, cell);
		}
		private void addDividerRow(Table table) {
			
			Cell cell;
			cell = new Cell(true);
            cell.Colspan = table.ProportionalWidths.Length;
			cell.BorderWidth = 0;
			cell.Leading = 0;
            AddNewCell(table, cell);
		}

		protected override string GetTitle(){
			return "";
		}

		protected override string GetSubTitle(){
			return "Auto de eliminação nº " + mAutoEliminacaoRow.Designacao;
		}

		private class Serie {
			public Serie(long ID) {
				this.ID = ID;
			}
			public long ID;
			public string Designacao;
			public string DataProducao;
			public bool Preservar;
			public ArrayList EntidadesProdutoras;
			public ArrayList UnidadesFisicas;
			public ArrayList Documentos = new ArrayList();
		}
		private class UnidadeFisica {
			public UnidadeFisica(long ID) {
				this.ID = ID;
			}
			public long ID;
			public string Designacao;
			public string Cota;
			public string TipoAcondicionamento;
			public decimal MedidaLargura;
			public string DataProducao;
			public ArrayList Series;
			public ArrayList Documentos;
		}
		private class Documento {
			public Documento(long ID) {
				this.ID = ID;
			}
			public long ID;
			public Serie Serie;
			public string Designacao;
			public string DataProducao;
			public bool Preservar;
			
			// EntidadesProdutoras é um ArrayList de strings com os nomes 
			// das EPs. só aplicável aos documentos que não constituam série
			public ArrayList EntidadesProdutoras;
			public ArrayList UnidadesFisicas;
		}
	}
}