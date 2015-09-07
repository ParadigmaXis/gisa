using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using iTextSharp.text;
using iTextSharp.text.rtf;
using iTextSharp.text.rtf.headerfooter;
using iTextSharp.text.pdf;

namespace GISA.Reports {
	public class OperationAbortedException : Exception {
		public OperationAbortedException(string message) : base(message) {
		}
	}

	public delegate void AddedEntriesEventHandler(int Count);
	public delegate void RemovedEntriesEventHandler(int Count);

	public abstract class Relatorio {
		public event AddedEntriesEventHandler AddedEntries;
		public event RemovedEntriesEventHandler RemovedEntries;
        private static string CommonDialogFilter = "Portable Document Format (*.pdf)|*.pdf|Rich Text Format (*.rtf)|*.rtf";
        private static string XLSXDialogFilter = "Excel Microsoft Office Open XML Format Spreadsheet (*.xlsx)|*.xlsx";
		private static SaveFileDialog mSaveDialog;
		static Relatorio() {
			mSaveDialog = new SaveFileDialog();
			mSaveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			mSaveDialog.AddExtension = true;
			mSaveDialog.DefaultExt = "pdf";
            mSaveDialog.Filter = CommonDialogFilter;
			mSaveDialog.OverwritePrompt = true;
			mSaveDialog.ValidateNames = true;
		}

        private enum FileTypes {
            pdf = 1,
            rtf = 2,
            xlsx = 3
        }

		protected string mFileName;
		protected ArrayList mParameters;
		protected long mIDTrustee;
        protected List<ReportParameter> mFields;
        protected Document mDoc;

        public string GetFileName
        {
            get { return mFileName; }
        }
            

        private Dictionary<string, string> mCriteriosDePesquisa;
        public Dictionary<string, string> CriteriosDePesquisa
        {
            get { return this.mCriteriosDePesquisa; }
            set { this.mCriteriosDePesquisa = value; }
        }

		/// <summary>
		/// Construtor para relatórios integrais.
		/// </summary>
		protected Relatorio(string FileName, long idTrustee) {
            // hack...
            if (this.GetType() == typeof(UnidadesFisicasResumido) || this.GetType() == typeof(ResultadosPesquisa))
                mSaveDialog.Filter = CommonDialogFilter + "|" + XLSXDialogFilter;
            else
                mSaveDialog.Filter = CommonDialogFilter;

			mFileName = FileNameSelection(FileName);
            if (mFileName != null && !mFileName.Equals(""))
            {
                this.mParameters = new ArrayList();
                this.mFields = new List<ReportParameter>();
                this.mIDTrustee = idTrustee;
                this.mCriteriosDePesquisa = null;
                this.CreateDocument();
            }
		}

		/// <summary>
		/// Construtor para relatórios parciais.
		/// </summary>
        protected Relatorio(string FileName, ArrayList parameters, long idTrustee)
            : this(FileName, idTrustee)
        {
			this.mParameters = parameters;            
		}

        protected Relatorio(string FileName, ArrayList parameters, List<ReportParameter> fields, long idTrustee) : this(FileName, parameters, idTrustee)
        {
            this.mFields = fields;            
        }

        private FileTypes mSelectedFileType;
        private FileTypes SelectedFileType
        {
            get { return mSelectedFileType; }
            set { mSelectedFileType = value; }
        }


		protected string FileNameSelection(string FileName) {
            mSaveDialog.FileName = FileName;
			switch (mSaveDialog.ShowDialog()) {
				case DialogResult.OK:
                    mSaveDialog.InitialDirectory = new System.IO.FileInfo(mSaveDialog.FileName).Directory.ToString();
                    SelectedFileType = (FileTypes) mSaveDialog.FilterIndex;
                    return mSaveDialog.FileName;
                case DialogResult.Cancel:
                    return null;
			}
            return null;
		}

		public void GenerateRel() { 
            if (this.mFileName != null) 
            try
            {                
                if (this.mSelectedFileType == FileTypes.xlsx)
                {
                    LoadContents();
                    FillContentsXLSX();
                }
                else
                {
                    FillHeaderAndFooter();
                    AddCriteriosDePesquisa();
                    LoadContents();
                    FillContents();
                }
            }            
            catch (System.IO.FileNotFoundException ex)
            {
                Trace.WriteLine(ex.ToString());
                MessageBox.Show(
                    string.Format("O ficheiro de destino ({0}) não se encontra disponível.", new System.IO.FileInfo(mFileName).Name),
                    GetTitle(),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1);
                return;
            }
            catch (System.IO.IOException ex)
            {
                Trace.WriteLine(ex.ToString());
                MessageBox.Show(
                    string.Format("O ficheiro de destino ({0}) está a ser utilizado por outra aplicação.", new System.IO.FileInfo(mFileName).Name),
                    GetTitle(),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1);
                return;
            }
            catch (OutOfMemoryException ex)
            {
                Trace.WriteLine(ex.ToString());
                MessageBox.Show("Não foi possível gerar o relatório dado o elevado volume de informação." + System.Environment.NewLine + "Por favor contacte o administrador de sistema.", "Erro na geração do relatório", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                throw;
            }
            finally {
                if (this.mDoc!=null)
                    this.mDoc.Close();
			}
		}

		protected virtual void FillHeaderAndFooter() {
			// O rodapé é adicionado no início de forma a aparecer logo na primeira página
            this.mDoc.Footer = getFooter(null);
            this.mDoc.Open();
			
			// título, e subtitulo caso exista
			Paragraph p = new Paragraph(GetTitle(), TitleFont);
			p.Alignment = Element.ALIGN_LEFT ;
			if (GetSubTitle() != null){
				p.SpacingAfter = 5f;
                this.mDoc.Add(p);
				p = new Paragraph(GetSubTitle(), SubTitleFont);
				p.Alignment = Element.ALIGN_LEFT;
			}
			p.SpacingAfter = 30f;
            this.mDoc.Add(p);

			// O cabeçalho é adicionado após a adição do título de forma a já não ser 
			// apresentado na primeira página mas aparecer nas restantes
            this.mDoc.Header = getHeader(GetTitle());
		}

        protected HeaderFooter getHeader(string text)
        {
            HeaderFooter header = null;
            if (text != null && text.Length > 0)
                header = new HeaderFooter(new Phrase(string.Format("{0} – {1}", "Gestão Integrada de Sistemas de Arquivo", text), PageHeaderFont), false);
            else
                header = new HeaderFooter(new Phrase("Gestão Integrada de Sistemas de Arquivo", PageHeaderFont), false);
            header.Alignment = Element.ALIGN_CENTER;
            header.Border = 2;
            header.BorderColor = PageHeaderFont.Color;
            return header;
        }

        protected HeaderFooter getFooter(string text)
        {
            HeaderFooter footer = new HeaderFooter(new Phrase(" ", PageFooterFont), true);
            //Câmara Municipal do Porto - Departamento de Arquivos -
            footer.Alignment = Element.ALIGN_LEFT;
            footer.Border = 1;
            footer.BorderColor = PageFooterFont.Color;
            return footer;
        }

		private void LoadContents() {
			// na chamada inicial é criado um reader vazio. na 1ª reimplementação da 
			// assinatura virtual terá de ser criada uma instancia do datareader e em 
			// reimplementações posteriores será reutilizado o datareader que seja passado 
			// de trás (o 1º passo de cada reimplementação terá de ser a chamada ao pai)
			IDataReader reader = null;
			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try { 
				InitializeReport(ho.Connection);
				LoadContents(ho.Connection, ref reader);
				if (reader != null && !reader.IsClosed){
					reader.Close();
				}
				FinalizeReport(ho.Connection);
            } catch (Exception ex) {
				Trace.WriteLine(ex);
				throw;
			} finally {
				ho.Dispose();
			}
		}

        private void CreateDocument()
        {
            Document doc = new Document(PageSize.A4, CentimeterToPoint(2.5F), CentimeterToPoint(2.5F), CentimeterToPoint(2.5F), CentimeterToPoint(2.5F));

            try {
                switch (SelectedFileType) {
                    case FileTypes.pdf:
                        PdfWriter.GetInstance(doc, new System.IO.FileStream(mFileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite));                        
                        break;
                    case FileTypes.rtf:
                        RtfWriter2.GetInstance(doc, new System.IO.FileStream(mFileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite)).GetDocumentSettings().SetDataCacheStyle(iTextSharp.text.rtf.document.output.RtfDataCache.CACHE_DISK);
                        break;
                    case FileTypes.xlsx:
                        break;
                    default:
                        throw new NotSupportedException();
                }
                this.mDoc = doc;
            }
            catch (IOException) {
                MessageBox.Show(
                    string.Format("O ficheiro de destino ({0}) está a ser utilizado por outra aplicação.", new System.IO.FileInfo(mFileName).Name),
                    GetTitle(),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1);
                this.mFileName = null;
                return;
            }

        }

        protected void AddNewCell(iTextSharp.text.Table table, iTextSharp.text.Cell cell)
        {
            table.AddCell(cell);
        }

        protected void AddNewCell(iTextSharp.text.Table table, string text)
        {
            AddNewCell(table, text, null);
        }

        // adiciona uma célula com texto com uma formatação expecífica
        protected void AddNewCell(iTextSharp.text.Table table, string text, iTextSharp.text.Font font)
        {
            AddNewCell(table, text, font, Element.ALIGN_LEFT);
        }

        protected void AddNewCell(iTextSharp.text.Table table, string text, iTextSharp.text.Font font, int hAlignment)
        {
            Cell cell;
            if (font == null)
                cell = new Cell(new Phrase(ConvertNewLines(text)));
            else
                cell = new Cell(new Phrase(ConvertNewLines(text), font));
            
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.HorizontalAlignment = hAlignment;
            table.AddCell(cell);
        }

        // adiciona uma célula à tabela onde cada porção de texto tem uma formatação própria
        protected void AddNewCell(iTextSharp.text.Table table, List<Chunk> chunks)
        {
            Phrase f = new Phrase();
            f.Leading = 10;
            foreach (Chunk c in chunks)
                f.Add(new iTextSharp.text.Chunk(ConvertNewLines(c.Text), c.Font));

            Cell cell = new Cell(f);
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(cell);
        }

        // adiciona uma célula com o texto organizado em várias linhas diferentes
        protected void AddNewCell(iTextSharp.text.Table table, List<string> paragraphs, iTextSharp.text.Font font)
        {
            Cell cell = new Cell();
            foreach (string paragraph in paragraphs)
            {
                iTextSharp.text.Chunk chunk = new iTextSharp.text.Chunk(ConvertNewLines(paragraph), font);
                Paragraph p = new Paragraph(10, chunk);
                cell.Add(p);
            }
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(cell);
        }

        protected iTextSharp.text.Table CreateTable(float indentation)
        {
            //este offset deve passar novamente para 0 caso se volte a usar Tables em vez de PdfTables
            Table detailsTable;
            float indentPercent = (indentation + 1.5f) * 100f / 21.6f;

            detailsTable = new Table(3, 1);
            detailsTable.Offset = 3;
            detailsTable.Width = 100;
            detailsTable.BorderColor = iTextSharp.text.Color.WHITE;
            detailsTable.DefaultCellBorderColor = iTextSharp.text.Color.WHITE;
            detailsTable.CellsFitPage = true;

            // uma página A4 tem 21.6 cm
            detailsTable.Widths = new float[] { indentPercent, 22, 100 - 22 - indentPercent };

            return detailsTable;
        }

        protected void AddTable(Document doc, iTextSharp.text.Table table)
        {
            switch (SelectedFileType)
            {
                case FileTypes.pdf:
                    AddTablePDF(doc, table);
                    break;
                case FileTypes.rtf:
                    AddTableRTF(doc, table);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        private void AddTablePDF(Document doc, iTextSharp.text.Table table)
        {
            try
            {
                table.Convert2pdfptable = true;
                var tbl = table.CreatePdfPTable();
                doc.Add(tbl);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                throw;
            }        
        }

        private void AddTableRTF(Document doc, iTextSharp.text.Table table)
        {
            doc.Add(table);
        }

        protected struct Chunk
        {
            public string Text;
            public iTextSharp.text.Font Font;

            public Chunk(string t, iTextSharp.text.Font f)
            {
                Text = t;
                Font = f;
            }
        }

        private string ConvertNewLines(string text)
        {
            string result = string.Empty;
            switch (SelectedFileType)
            {
                case FileTypes.pdf:
                    result = text;
                    break;
                case FileTypes.rtf:
                    // nos relatórios rtf o '\r' é mostrado com o caracter '?'
                    result = text.Replace("\r\n", "\n");
                    break;
                default:
                    throw new NotSupportedException();
            }

            return result;
        }

        #region Fonts
        private Font mPageHeaderFont;
		public Font PageHeaderFont {
			get {
				if (mPageHeaderFont == null)
                    mPageHeaderFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, iTextSharp.text.Font.ITALIC, iTextSharp.text.Color.GRAY);
				return mPageHeaderFont;
			}
		}

		private Font mPageFooterFont;
		public Font PageFooterFont {
			get {
				if (mPageFooterFont == null)
                    mPageFooterFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, iTextSharp.text.Font.ITALIC, iTextSharp.text.Color.GRAY);
				return mPageFooterFont;
			}
		}

		private Font mBodyFont;
		public Font BodyFont {
			get {
				if (mBodyFont == null)
                    mBodyFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 10, iTextSharp.text.Font.NORMAL);
				return mBodyFont;
			}
		}

		private Font mSmallerBodyFont;
		public Font SmallerBodyFont {
			get {
				if (mSmallerBodyFont == null)
                    mSmallerBodyFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, iTextSharp.text.Font.BOLD);
				return mSmallerBodyFont;
			}
		}

		private Font mHeaderFont;
		public Font HeaderFont {
			get {
				if (mHeaderFont == null)
                    mHeaderFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, iTextSharp.text.Font.NORMAL);
				return mHeaderFont;
			}
		}

        private Font mHeaderItalicFont;
        public Font HeaderItalicFont
        {
            get
            {
                if (mHeaderItalicFont == null)
                    mHeaderItalicFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, iTextSharp.text.Font.ITALIC);
                return mHeaderItalicFont;
            }
        }

		private Font mContentFont;
		public Font ContentFont {
			get {
				if (mContentFont == null)
                    mContentFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 7, iTextSharp.text.Font.NORMAL);
				return mContentFont;
			}
		}

        private Font mContentItalicFont;
        public Font ContentItalicFont
        {
            get
            {
                if (mContentItalicFont == null)
                    mContentItalicFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 7, iTextSharp.text.Font.ITALIC);
                return mContentItalicFont;
            }
        }

        private Font mContentBoldFont;
        public Font ContentBoldFont
        {
            get
            {
                if (mContentBoldFont == null)
                    mContentBoldFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 7, iTextSharp.text.Font.BOLD);
                return mContentBoldFont;
            }
        }

        private Font mContentStrikeThroughFont;
        public Font ContentStrikeThroughFont
        {
            get
            {
                if (mContentStrikeThroughFont == null)
                    mContentStrikeThroughFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 7, iTextSharp.text.Font.STRIKETHRU);
                return mContentStrikeThroughFont;
            }
        }

		private Font mTitleFont;
		public virtual Font TitleFont {
			get {
				if (mTitleFont == null)
                    mTitleFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 20);
				return mTitleFont;
			}
		}

        private Font mSubTitleFont;
        public Font SubTitleFont
        {
            get
            {
                if (mSubTitleFont == null)
                    mSubTitleFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 16);
                return mSubTitleFont;
            }
        }

		private Font mSubSubTitleFont;
		public Font SubSubTitleFont {
			get {
				if (mSubSubTitleFont == null)
                    mSubSubTitleFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 12);
				return mSubSubTitleFont;
			}
        }
        #endregion

        protected void DoAddedEntries(int Count) {
			if (AddedEntries != null) {
				AddedEntries(Count);
			}
		}

		protected void DoRemovedEntries(int Count) {
			if (RemovedEntries != null) {
				RemovedEntries(Count);
			}
		}

		protected float CentimeterToPoint(float cm) {
			return InchToPoint(cm / 2.54f);
		}

		protected float InchToPoint(float inch) {
			return inch * 72f;
		}

		protected string GetInicioData(GISADataset.SFRDDatasProducaoRow sfrd) {
			string result = "";
			if (!sfrd.IsInicioAnoNull() && sfrd.InicioAno.Length > 0)
				result += sfrd.InicioAno;
			else
				result += "????";
			result += ".";
			if (!sfrd.IsInicioMesNull() && sfrd.InicioMes.Length > 0)
				result += sfrd.InicioMes;
			else
				result += "??";
			result += ".";
			if (!sfrd.IsInicioDiaNull() && sfrd.InicioDia.Length > 0)
				result += sfrd.InicioDia;
			else
				result += "??";

			return result;
		}

		protected string GetFimData(GISADataset.SFRDDatasProducaoRow sfrd) {
			string result = "";
			if (!sfrd.IsFimAnoNull() && sfrd.FimAno.Length > 0)
				result += sfrd.FimAno;				
			else
				result += "????";
			result += ".";
			if (!sfrd.IsFimMesNull() && sfrd.FimMes.Length > 0)
				result += sfrd.FimMes;				
			else
				result += "??";
			result += ".";
			if (!sfrd.IsFimDiaNull() && sfrd.FimDia.Length > 0)
				result += sfrd.FimDia;				
			else
				result += "??";

			return result;
		}

        public void AddCriteriosDePesquisa()
        {
            if (this.mCriteriosDePesquisa != null)
            {
                Paragraph pTitle = new Paragraph("Critérios de pesquisa", this.SubSubTitleFont);
                this.mDoc.Add(pTitle);

                foreach (KeyValuePair<string, string> item in this.mCriteriosDePesquisa)
                {
                    string phrase = string.Format("{0}: {1}", item.Key, item.Value);
                    Paragraph p = new Paragraph(phrase, this.ContentFont);
                    this.mDoc.Add(p);
                }
                Paragraph spacing = new Paragraph("", this.ContentFont);
                spacing.SpacingAfter = 30;
                this.mDoc.Add(spacing);
            }            
        }
		#region Métodos reimplementáveis
        protected virtual void LoadContents(IDbConnection connection, ref IDataReader reader)
        {
        }

        protected virtual void FillContents()
        {
        }

        protected virtual void FillContentsXLSX()
        {
        }

		protected virtual void InitializeReport(IDbConnection connection){
			// este método deve ser overridden em classes descendentes de forma 
			// a ser, por exemplo, adicionada uma lista de parâmetros necessários 
			// ao relatório
		}

		protected virtual void FinalizeReport(IDbConnection connection){
		}

		protected virtual string GetTitle(){
			return "Relatório";
		}

		protected virtual string GetSubTitle(){
			return null;
		}
		#endregion

        #region Nomes de campos dos relatórios detalhados por extenso
        private static Dictionary<ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet, string> mNomesExtensoCamposRelInvCat;
        public static Dictionary<ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet, string> NomesExtensoCamposRelInvCat
        {
            get
            {
                if (mNomesExtensoCamposRelInvCat == null)
                {
                    mNomesExtensoCamposRelInvCat = new Dictionary<ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet, string>();
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.IDNivel, "Identificador");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Agrupador, "Agrupador");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Dimensao, "Dimensão");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.CotaDocumento, "Cota do documento");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Autores, "Autores");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.UFsAssociadas, "Unidades físicas associadas");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.HistAdministrativaBiografica, "História administrativa / biográfica");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.FonteImediataAquisicaoTransferencia, "Fonte imediata de aquisição ou transferência");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.HistoriaArquivistica, "História arquivística");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.TipologiaInformacional, "Tipologia Informacional");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Diplomas, "Diplomas");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Modelos, "Modelos");

                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_RequerentesIniciais, "Requerentes/proprietários (iniciais)");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_RequerentesAverbamentos, "Requerentes/proprietários (averbamentos)");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_DesignacaoNumPoliciaAct, "Localização da obra (designação atual)");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_DesignacaoNumPoliciaAntigo, "Localização da obra (designação antiga)");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_TipoObra, "Tipo de obra");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_PropHorizontal, "Propriedade horizontal");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_TecnicoObra, "Técnico de obra");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_AtestHabit, "Atestado de habitabilidade");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.LO_DataLicConst, "Data da licença de construção");

                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.ConteudoInformacional, "Conteúdo Informacional");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.DiplomaLegal, "Diploma Legal");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.RefTab, "Nº de Referência na Tabela");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.DestinoFinal, "Destino Final");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Prazo, "Prazo");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Publicado, "Publicado");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.ObservacoesEnquadramentoLegal, "Observações / Enquadramento legal");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Incorporacoes, "Incorporações");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.TradicaoDocumental, "Tradição documental");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Ordenacao, "Ordenação");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.ObjectosDigitais, "Objetos Digitais");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.CondicoesAcesso, "Condições de acesso");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.CondicoesReproducao, "Condições de reprodução");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Lingua, "Língua");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Alfabeto, "Alfabeto");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.FormaSuporteAcondicionamento, "Forma de suporte e/ou acondicionamento");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.MaterialSuporte, "Material de suporte");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.TecnicaRegisto, "Técnica de registo");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.EstadoConservacao, "Estado de conservação");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.InstrumentosPesquisa, "Instrumentos de pesquisa");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.ExistenciaLocalizacaoOriginais, "Existência e localização de originais");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.ExistenciaLocalizacaoCopias, "Existência e localização das cópias");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.UnidadesDescricaoAssociadas, "Unidades de descrição associadas");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.NotaPublicacao, "Nota de publicação");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Notas, "Notas");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.NotaArquivista, "Nota do arquivista");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.RegrasConvenções, "Regras ou convenções");
                    mNomesExtensoCamposRelInvCat.Add(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet.Indexacao, "Indexação");
                }
                return mNomesExtensoCamposRelInvCat;
            }
        }

        private static Dictionary<ReportParameterRelPesqUF.CamposRelPesqUF, string> mNomesExtensoCamposRelPesqUF;
        public static Dictionary<ReportParameterRelPesqUF.CamposRelPesqUF, string> NomesExtensoCamposRelPesqUF
        {
            get
            {
                if (mNomesExtensoCamposRelPesqUF == null)
                {
                    mNomesExtensoCamposRelPesqUF = new Dictionary<ReportParameterRelPesqUF.CamposRelPesqUF, string>();
                    mNomesExtensoCamposRelPesqUF.Add(ReportParameterRelPesqUF.CamposRelPesqUF.GuiaIncorporacao, "Guia de incorporação");
                    mNomesExtensoCamposRelPesqUF.Add(ReportParameterRelPesqUF.CamposRelPesqUF.CotaCodigoBarras, "Cota e código de barras");
                    mNomesExtensoCamposRelPesqUF.Add(ReportParameterRelPesqUF.CamposRelPesqUF.DatasProducao, "Datas de produção");
                    mNomesExtensoCamposRelPesqUF.Add(ReportParameterRelPesqUF.CamposRelPesqUF.TipoDimensoes, "Tipo e dimensões");
                    mNomesExtensoCamposRelPesqUF.Add(ReportParameterRelPesqUF.CamposRelPesqUF.UltimaAlteracao, "Última alteração");
                    mNomesExtensoCamposRelPesqUF.Add(ReportParameterRelPesqUF.CamposRelPesqUF.ConteudoInformacional, "Conteúdo Informacional");
                    mNomesExtensoCamposRelPesqUF.Add(ReportParameterRelPesqUF.CamposRelPesqUF.UnidadesInformacionaisAssociadas, "Unidades informacionais associadas");
                    mNomesExtensoCamposRelPesqUF.Add(ReportParameterRelPesqUF.CamposRelPesqUF.Eliminada, "Eliminada");
                }
                return mNomesExtensoCamposRelPesqUF;
            }
        }

        private static Dictionary<ReportParameterRelEPs.CamposRelEPs, string> mNomesExtensoCamposRelEPs;
        public static Dictionary<ReportParameterRelEPs.CamposRelEPs, string> NomesExtensoCamposRelEPs
        {
            get
            {
                if (mNomesExtensoCamposRelEPs == null)
                {
                    mNomesExtensoCamposRelEPs = new Dictionary<ReportParameterRelEPs.CamposRelEPs, string>();
                    mNomesExtensoCamposRelEPs.Add(ReportParameterRelEPs.CamposRelEPs.TipoEntidadeProdutora, "Tipo de entidade");
                    mNomesExtensoCamposRelEPs.Add(ReportParameterRelEPs.CamposRelEPs.FormaParalela, "Forma paralela");
                    mNomesExtensoCamposRelEPs.Add(ReportParameterRelEPs.CamposRelEPs.FormaNormalizada, "Forma normalizada segundo outras regras");
                    mNomesExtensoCamposRelEPs.Add(ReportParameterRelEPs.CamposRelEPs.OutrasFormas, "Outras formas");
                    mNomesExtensoCamposRelEPs.Add(ReportParameterRelEPs.CamposRelEPs.IdentificadorUnico, "Identificador Único");
                    mNomesExtensoCamposRelEPs.Add(ReportParameterRelEPs.CamposRelEPs.DatasExistencia, "Datas de existência");
                    mNomesExtensoCamposRelEPs.Add(ReportParameterRelEPs.CamposRelEPs.Historia, "História");
                    mNomesExtensoCamposRelEPs.Add(ReportParameterRelEPs.CamposRelEPs.ZonaGeografica, "Zona geográfica");
                    mNomesExtensoCamposRelEPs.Add(ReportParameterRelEPs.CamposRelEPs.EstatutoLegal, "Estatuto legal");
                    mNomesExtensoCamposRelEPs.Add(ReportParameterRelEPs.CamposRelEPs.FuncoesOcupacoesActividades, "Funções, ocupações e atividades");
                    mNomesExtensoCamposRelEPs.Add(ReportParameterRelEPs.CamposRelEPs.EnquadramentoLegal, "Enquadramento legal");
                    mNomesExtensoCamposRelEPs.Add(ReportParameterRelEPs.CamposRelEPs.EstruturaInterna, "Estrutura interna");
                    mNomesExtensoCamposRelEPs.Add(ReportParameterRelEPs.CamposRelEPs.ContextoGeral, "Contexto geral");
                    mNomesExtensoCamposRelEPs.Add(ReportParameterRelEPs.CamposRelEPs.OutrasInformacoesRelevantes, "Outras informações relevantes");
                    mNomesExtensoCamposRelEPs.Add(ReportParameterRelEPs.CamposRelEPs.Relacoes, "Relações");
                    mNomesExtensoCamposRelEPs.Add(ReportParameterRelEPs.CamposRelEPs.RegrasConvencoes, "Regras e/ou convenções");
                    mNomesExtensoCamposRelEPs.Add(ReportParameterRelEPs.CamposRelEPs.Validado, "Validado");
                    mNomesExtensoCamposRelEPs.Add(ReportParameterRelEPs.CamposRelEPs.Completo, "Completo");
                    mNomesExtensoCamposRelEPs.Add(ReportParameterRelEPs.CamposRelEPs.LinguaAlfabeto, "Língua e alfabeto");
                    mNomesExtensoCamposRelEPs.Add(ReportParameterRelEPs.CamposRelEPs.FontesObservacoes, "Fontes / Observações");                    
                }
                return mNomesExtensoCamposRelEPs;
            }
        }

        public static string GetParameterName(ReportParameter parameter)
        {            
            if (parameter.GetType() == typeof(ReportParameterRelEPs))
                return GetParameterName(((ReportParameterRelEPs)parameter).Campo);
            else if (parameter.GetType() == typeof(ReportParameterRelInvCatPesqDet))
                return GetParameterName(((ReportParameterRelInvCatPesqDet)parameter).Campo);
            else if (parameter.GetType() == typeof(ReportParameterRelPesqUF))
                return GetParameterName(((ReportParameterRelPesqUF)parameter).Campo);
            else
                return string.Empty;
        }

        public static string GetParameterName(ReportParameterRelEPs.CamposRelEPs campo)
        {
            return NomesExtensoCamposRelEPs[campo];
        }

        public static string GetParameterName(ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet campo)
        {
            return NomesExtensoCamposRelInvCat[campo];
        }

        public static string GetParameterName(ReportParameterRelPesqUF.CamposRelPesqUF campo)
        {
            return NomesExtensoCamposRelPesqUF[campo];
        }
        #endregion
    }
}
