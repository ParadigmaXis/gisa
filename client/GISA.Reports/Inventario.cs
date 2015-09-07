using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using GISA.Model;
using iTextSharp.text;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA.Reports 
{
	public class Inventario : Relatorio
	{
        public Inventario(string FileName, bool isTopDown, long idTrustee) : base(FileName, idTrustee) {
			this.isTopDown = isTopDown;
		}

        public Inventario(string FileName, ArrayList parameters, long idTrustee) : base(FileName, parameters, idTrustee) { }

		public Inventario(string FileName, ArrayList parameters, bool isTopDown, long idTrustee) : base(FileName, parameters, idTrustee) {
			this.isTopDown = isTopDown;
		}

        public Inventario(string FileName, ArrayList parameters, List<ReportParameter> fields, bool isTopDown, long idTrustee)
            : base(FileName, parameters, fields, idTrustee)
        {
            this.isTopDown = isTopDown;
        }

		protected override void InitializeReport(IDbConnection connection){
			if (mParameters != null && mParameters.Count == 0)
				return;

			RelatorioRule.Current.InitializeInventario((long)mParameters[0], connection);
		}

		protected override void FinalizeReport(IDbConnection connection){
			RelatorioRule.Current.FinalizeInventario(connection);
		}

		private bool isTopDown = false; // é bottomUp por omissão
        
        // lista de todos os níveis
        protected Dictionary<long, Nivel> niveis = new Dictionary<long, Nivel>();
        // nivel que foi selecionado para a geração do relatório (inventário ou catálogo)
        protected Nivel nvlContexto = null;
        // níveis que servem como ponto de partida
        protected List<Nivel> topNiveis = new List<Nivel>();
        // produtores dos níveis documentais
        protected Dictionary<long, Nivel> prodHT = new Dictionary<long, Nivel>();
        // códigos completos das séries e documentos soltos
        protected Hashtable codCompletos = new Hashtable();

        protected override void LoadContents(IDbConnection connection, ref IDataReader reader)
        {
            // carregar informação para memória
            LoadData(connection, ref reader);            

            // ler entidades produtoras
            ArrayList produtores = new ArrayList();
            produtores = DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.GetProdutores();

            Nivel nvl = null;
            long IDNivel;
            foreach (List<string> produtor in produtores)
            {
                IDNivel = System.Convert.ToInt64(produtor[0]);
                if (!prodHT.ContainsKey(IDNivel))
                {
                    nvl = CreateNivelIfNonExistent(IDNivel);
                    nvl.IDNivel = IDNivel;
                    nvl.Codigo = produtor[1];
                    nvl.TipoNivelRelacionado = produtor[2];
                    nvl.Designacao = produtor[3];
                    prodHT.Add(IDNivel, nvl);
                }
            }

            // ler os codigos de referência dos níveis documentais a apresentar no inventário (só serão apresentados
            // os códigos completos de séries e documentos soltos)            
            codCompletos = DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.GetCodCompletos();
            
            // ler níveis documentais a serem apresentados no relatório
            // Nota: o método é chamado duas vezes porque na primeira carrega os nós de contexto (o nível de detalhe é mínimo) e na 
            //       na segunda são carregados os níveis com maior detalhe
            LoadTopNivel(ref reader);
            LoadBasicContents(true, ref reader);
            LoadBasicContents(false, ref reader);
        }

        protected virtual void LoadData(IDbConnection connection, ref IDataReader reader)
        {
            try
            {
                reader = DBAbstractDataLayer.DataAccessRules.RelatorioRule.Current.ReportInventario((long)this.mParameters[0], IDTrustee(), IsCatalogo(), IsDetalhado(), IsTopDownExpansion(), this.mFields, connection);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        // carregar nível que serviu de ponto de partida para o inventário/catálogo
        protected virtual void LoadTopNivel(ref IDataReader reader)
        {
            Nivel nvl = null;
            long IDNivel;
            long IDUpperNivel = 0;
            while (reader.Read())
            {
                IDNivel = System.Convert.ToInt64(reader.GetValue(0));
                IDUpperNivel = System.Convert.ToInt64(reader.GetValue(1));

                nvl = CreateNivelIfNonExistent(IDNivel, true);
                // a atribuição do valor a este membro tem que se feita antes do DefineTopNiveis
                nvl.isContext = true;

                nvlContexto = nvl;

                if (codCompletos.Contains(IDNivel))
                {
                    nvl.CodigosCompletos = new ArrayList();
                    foreach (string cod in ((List<string>)codCompletos[IDNivel]))
                        nvl.CodigosCompletos.Add(cod);
                }

                nvl.Codigo = reader.GetValue(2).ToString();
                nvl.IDTipoNivel = System.Convert.ToInt64(reader.GetValue(3));
                nvl.TipoNivelRelacionado = this.mParameters[1].ToString();
                nvl.Designacao = reader.GetValue(5).ToString();
                nvl.InicioTexto = reader.GetValue(6).ToString();
                nvl.InicioAno = reader.GetValue(7).ToString();
                nvl.InicioMes = reader.GetValue(8).ToString();
                nvl.InicioDia = reader.GetValue(9).ToString();
                if (reader.GetValue(10) != DBNull.Value)
                    nvl.InicioAtribuida = GisaDataSetHelper.GetDBNullableBoolean(ref reader, 10);
                nvl.FimAno = reader.GetValue(11).ToString();
                nvl.FimMes = reader.GetValue(12).ToString();
                nvl.FimDia = reader.GetValue(13).ToString();
                if (reader.GetValue(14) != DBNull.Value)
                    nvl.FimAtribuida = GisaDataSetHelper.GetDBNullableBoolean(ref reader, 14);
                if (IsDetalhado())
                {
                    nvl.InfoAdicional = new Dictionary<ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet, ArrayList>();
                    ArrayList info;
                    int i = 15;
                    foreach (ReportParameterRelInvCatPesqDet rp in this.mFields)
                    {
                        if (rp.RetType == ReportParameter.ReturnType.TextOnly)
                        {
                            info = new ArrayList();
                            info.Add(new List<string>() { reader.GetValue(i).ToString() });
                            nvl.InfoAdicional.Add(rp.Campo, info);
                            i++;
                        }
                    }
                }
            }
            reader.NextResult();
        }

        protected void LoadBasicContents(bool isContext, ref IDataReader reader)
        {
            Nivel nvl = null;
            long IDNivel;
            long IDUpperNivel = 0;
            while (reader.Read())
            {
                IDNivel = System.Convert.ToInt64(reader.GetValue(0));
                if (reader.GetValue(1) != DBNull.Value)
                    IDUpperNivel = System.Convert.ToInt64(reader.GetValue(1));

                nvl = CreateNivelIfNonExistent(IDNivel, !isContext);
                // a atribuição do valor a este membro tem que se feita antes do DefineTopNiveis
                nvl.isContext = isContext;

                DefineTopNiveis(nvl, IDUpperNivel);
                nvl.AddUpper(CreateNivelIfNonExistent(IDUpperNivel));

                if (codCompletos.Contains(IDNivel))
                {
                    nvl.CodigosCompletos = new ArrayList();
                    foreach (string cod in ((List<string>)codCompletos[IDNivel]))
                        nvl.CodigosCompletos.Add(cod);
                }
                
                nvl.Codigo = reader.GetValue(2).ToString();
                nvl.IDTipoNivel = System.Convert.ToInt64(reader.GetValue(3));
                nvl.TipoNivelRelacionado = reader.GetValue(4).ToString();
                nvl.Designacao = reader.GetValue(5).ToString();
                nvl.InicioTexto = reader.GetValue(6).ToString();
                nvl.InicioAno = reader.GetValue(7).ToString();
                nvl.InicioMes = reader.GetValue(8).ToString();
                nvl.InicioDia = reader.GetValue(9).ToString();
                nvl.InicioAtribuida = GisaDataSetHelper.GetDBNullableBoolean(ref reader, 10);
                nvl.FimAno = reader.GetValue(11).ToString();
                nvl.FimMes = reader.GetValue(12).ToString();
                nvl.FimDia = reader.GetValue(13).ToString();
                nvl.FimAtribuida = GisaDataSetHelper.GetDBNullableBoolean(ref reader, 14);
                if (IsDetalhado())
                {
                    nvl.InfoAdicional = new Dictionary<ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet, ArrayList>();
                    ArrayList info;
                    int i = 15;
                    foreach (ReportParameterRelInvCatPesqDet rp in this.mFields)
                    {
                        if (rp.RetType == ReportParameter.ReturnType.TextOnly)
                        {
                            info = new ArrayList();
                            info.Add(new List<string>() { reader.GetValue(i).ToString() });
                            nvl.InfoAdicional.Add(rp.Campo, info);
                            i++;
                        }
                    }
                }
            }
            reader.NextResult();
        }

        protected virtual void DefineTopNiveis(Nivel nvl, long IDUpperNivel)
        {
            // séries e documentos soltos são usados como ponto de partida;
            // no caso de o nivel actual ter um produtor associado acima, então trata-se de uma série ou documento
            // solto
            if (prodHT.ContainsKey(IDUpperNivel))
            {
                if (!topNiveis.Contains(nvl))
                    topNiveis.Add(nvl);

                nvl.AddProdutor((Nivel)prodHT[IDUpperNivel]);
            }
            else
                CreateNivelIfNonExistent(IDUpperNivel);

            nvl.IDNivelUpper = IDUpperNivel;
        }

		protected override void FillContents()
        {
            // nvlContexto é null quando, nos catálogos, o nível ponto de partida é uma série ou documento
            // solto
            if (nvlContexto != null)
                AddNivelContexto(base.mDoc, nvlContexto, 0.0F, this.BodyFont);

            foreach (Nivel topNivel in topNiveis)
                AddNivelEstrutural(base.mDoc, topNivel, 0.5F, this.BodyFont);
		}

        protected Nivel CreateNivelIfNonExistent(long IDNivel)
        {
            return CreateNivelIfNonExistent(IDNivel, false);
        }

		protected Nivel CreateNivelIfNonExistent(long IDNivel, bool addEntry) {
			Nivel nvl;
			nvl = GetExistentNivel(IDNivel);
            if (nvl == null)
            {
                nvl = new Nivel();
                nvl.IDNivel = IDNivel;
                niveis.Add(IDNivel, nvl);
                if (addEntry)
                    DoAddedEntries(1);
            }

			return nvl;
		}

		protected Nivel GetExistentNivel(long IDNivel) {
			if (niveis.ContainsKey(IDNivel))
				return (Nivel)niveis[IDNivel];
			else
				return null;
		}

		// o valor de retorno é usado para determinar se deve ser adicionado um separador entre o nivel actual e o seguinte.
		// devolve verdadeiro se produziu algum nível documental ou se se tratar do ultimo nivel estrutural dentro de um determinado nivel
		protected void AddNivelEstrutural(Document doc, Nivel nvl, float indentation) {
			AddNivelEstrutural(doc, nvl, indentation, this.BodyFont, null, false);
		}

        protected void AddNivelEstrutural(Document doc, Nivel nvl, float indentation, List<Nivel> niveisContexto)
        {
            AddNivelEstrutural(doc, nvl, indentation, this.BodyFont, niveisContexto, false);
        }

        private void AddNivelEstrutural(Document doc, Nivel nvl, float indentation, iTextSharp.text.Font font)
        {
            AddNivelEstrutural(doc, nvl, indentation, font, null, false);
        }

        private void AddNivelEstrutural(Document doc, Nivel nvl, float indentation, iTextSharp.text.Font font, bool isContext)
        {
            AddNivelEstrutural(doc, nvl, indentation, font, null, isContext);
        }

        private void AddNivelContexto(Document doc, Nivel nvl, float indentation, iTextSharp.text.Font font)
        {
            Table detailsTable;

            string nivelStr = string.Empty;
            nivelStr = string.Format("{0}: {1}", nvl.TipoNivelRelacionado, nvl.Designacao);

            Paragraph p;
            p = new Paragraph(CentimeterToPoint(0.5f), nivelStr, font);
            p.IndentationLeft = CentimeterToPoint(0 + indentation);
            p.SpacingBefore = 5f;
            doc.Add(p);

            string datasProducao = GISA.Utils.GUIHelper.FormatDateInterval(
                GISA.Utils.GUIHelper.FormatDate(nvl.InicioAno, nvl.InicioMes, nvl.InicioDia, nvl.InicioAtribuida),
                GISA.Utils.GUIHelper.FormatDate(nvl.FimAno, nvl.FimMes, nvl.FimDia, nvl.FimAtribuida));            
            
            if (IsDetalhado())
                datasProducao = nvl.InicioTexto + " " + datasProducao;

            detailsTable = CreateTable(indentation);
            AddNewCell(detailsTable, "");
            AddNewCell(detailsTable, "Datas de produção:", this.HeaderFont);
            AddNewCell(detailsTable, datasProducao, this.ContentFont);
            AddTable(doc, detailsTable);

            detailsTable = CreateTable(indentation);
            AddNewCell(detailsTable, "");
            if (nvl.CodigosCompletos.Count > 0)
            {
                List<string> codigos = new List<string>();
                foreach (string codigoCompleto in nvl.CodigosCompletos)
                    codigos.Add(codigoCompleto);

                AddNewCell(detailsTable, "Código(s) de referência:", this.HeaderFont);
                AddNewCell(detailsTable, codigos, this.ContentFont);
            }
            else
            {
                AddNewCell(detailsTable, "Código parcial:", this.HeaderFont);
                AddNewCell(detailsTable, nvl.Codigo, this.ContentFont);
            }
            AddTable(doc, detailsTable);
            
            AddExtraDetails(nvl, doc, indentation);
            
            DoRemovedEntries(1);
        }

        private void AddNivelEstrutural(Document doc, Nivel nvl, float indentation, iTextSharp.text.Font font, List<Nivel> niveisContexto, bool isContext)
        {
            Table detailsTable;

            string nivelStr = string.Empty;
            nivelStr = string.Format("{0}: {1}", nvl.TipoNivelRelacionado, nvl.Designacao);

            Paragraph p;
            p = new Paragraph(CentimeterToPoint(0.5f), nivelStr, font);
            p.IndentationLeft = CentimeterToPoint(0 + indentation);
            p.SpacingBefore = 5f;
            doc.Add(p);

            if (niveisContexto != null && niveisContexto.Count > 0)
            {
                detailsTable = CreateTable(indentation);
                AddNewCell(detailsTable, "");
                AddNewCell(detailsTable, "Contexto:", this.HeaderFont);
                AddNewCell(detailsTable, "");
                AddTable(doc, detailsTable);

                float innerIndentation = indentation + 1.0f;
                
                foreach (Nivel nContexto in niveisContexto)
                {
                    innerIndentation = (float)(innerIndentation + 0.5f);
                    AddNivelEstrutural(doc, nContexto, innerIndentation, ContentFont, true);
                }
            }

            iTextSharp.text.Font hFont = null;
            if (isContext)
                hFont = this.ContentFont;
            else
                hFont = this.HeaderFont;

            // imprimir os produtores do nível (só no caso de este ser uma série ou um documento solto)
            if (nvl.Produtores.Count > 0)
            {
                string entProdutoras = string.Empty;
                List<Chunk> chunks = new List<Chunk>();
                foreach (Nivel prod in nvl.Produtores)
                {
                    if (chunks.Count > 0)
                        chunks.Add(new Chunk(Environment.NewLine, ContentFont));

                    // só imprime a designação e o TipoNivelRelacionado do nivel "produtor" caso este seja controlado
                    if (prod.Designacao != null && prod.Designacao.Length > 0)
                    {
                        if (prod.TipoNivelRelacionado.Length == 0)
                            chunks.Add(new Chunk("Desconhecido", ContentItalicFont));
                        else
                            chunks.Add(new Chunk(prod.TipoNivelRelacionado, ContentFont));

                        chunks.Add(new Chunk(string.Format(" — {0}", prod.Designacao), ContentFont));
                    }
                }

                detailsTable = CreateTable(indentation);
                AddNewCell(detailsTable, "");
                AddNewCell(detailsTable, "Entidades Produtoras:", hFont);
                AddNewCell(detailsTable, chunks);
                AddTable(doc, detailsTable);
            }

            string datasProducao = GISA.Utils.GUIHelper.FormatDateInterval(
                GISA.Utils.GUIHelper.FormatDate(nvl.InicioAno, nvl.InicioMes, nvl.InicioDia, nvl.InicioAtribuida),
                GISA.Utils.GUIHelper.FormatDate(nvl.FimAno, nvl.FimMes, nvl.FimDia, nvl.FimAtribuida));            
            
            if (IsDetalhado())
                datasProducao = nvl.InicioTexto + " " + datasProducao;

            detailsTable = CreateTable(indentation);
            AddNewCell(detailsTable, "");
            AddNewCell(detailsTable, "Datas de produção:", hFont);
            AddNewCell(detailsTable, datasProducao, this.ContentFont);
            AddTable(doc, detailsTable);

            detailsTable = CreateTable(indentation);
            AddNewCell(detailsTable, "");
            if (nvl.CodigosCompletos.Count > 0)
            {
                List<string> codigos = new List<string>();
                foreach (string codigoCompleto in nvl.CodigosCompletos)
                    codigos.Add(codigoCompleto);

                AddNewCell(detailsTable, "Código(s) de referência:", hFont);
                AddNewCell(detailsTable, codigos, this.ContentFont);
            }
            else
            {
                AddNewCell(detailsTable, "Código parcial:", hFont);
                AddNewCell(detailsTable, nvl.Codigo, this.ContentFont);
            }
            AddTable(doc, detailsTable);

            if (!isContext)
                AddExtraDetails(nvl, doc, indentation);            

            ImprimeSubNiveis(nvl, doc, indentation);

            if (!isContext)
                DoRemovedEntries(1);
        }

        protected virtual void ImprimeSubNiveis(Nivel nvl, Document doc, float indentation)
        {
            foreach (Nivel nvlLower in nvl.Lowers)
            {
                if (nvlLower.IDTipoNivel == TipoNivel.DOCUMENTAL)
                    AddNivelEstrutural(doc, nvlLower, (float)(indentation + 0.5), this.SmallerBodyFont);
            }
        }

		// reimplementar para relatorios detalhados
        protected virtual void AddExtraDetails(Nivel nvl, Document doc, float indentation)
        {
		}
		
		#region Métodos reimplementaveis
		// reimplementar em classes descendentes que definam a impressão de catálogos
		protected virtual bool IsCatalogo(){
			return false;
		}

		protected virtual bool IsDetalhado(){
			return false;
		}

		protected virtual bool IsTopDownExpansion(){
			return isTopDown;
		}

        protected virtual long IDTrustee()
        {
            return mIDTrustee;
        }

        protected virtual List<ReportParameter> Fields()
        {
            return new List<ReportParameter>();
        }

		protected override string GetTitle(){
			return "Inventário";
		}
		#endregion

		protected class Nivel {
			public ArrayList Uppers = new ArrayList();
			// ArrayList de Nivel
			public ArrayList Lowers = new ArrayList();
			// ArrayList de Nivel
            public List<Nivel> Produtores = new List<Nivel>();
            public long IDNivel;
            public long IDNivelUpper;
            public bool isContext;
			public string Codigo;
			public long IDTipoNivel;
			public string TipoNivelRelacionado;
			public string Designacao;
			public string InicioTexto;
            public string InicioAno;
			public string InicioMes;
			public string InicioDia;
            public bool InicioAtribuida;
			public string FimAno;
			public string FimMes;
			public string FimDia;
            public bool FimAtribuida;
            public string Agrupador;
            public ArrayList Autores = new ArrayList();
			public ArrayList CodigosCompletos = new ArrayList();
			public ArrayList Tipologias = new ArrayList();
			public ArrayList Conteudos = new ArrayList();
            public ArrayList Diplomas = new ArrayList();
            public ArrayList Modelos = new ArrayList();
            public ArrayList DiplomaLegal = new ArrayList();
            public ArrayList UnidadesFisicas = new ArrayList();
            public ArrayList CotaDocumento = new ArrayList();
            public string UnidadesFisicasNotas = string.Empty;

            public ArrayList LO_RequerentesIniciais = new ArrayList();
            public ArrayList LO_RequerentesAverbamentos = new ArrayList();
            public ArrayList LO_DesignacaoNumPoliciaAct = new ArrayList();
            public ArrayList LO_DesignacaoNumPoliciaAntigo = new ArrayList();
            public ArrayList LO_TecnicoObra = new ArrayList();
            public ArrayList LO_AtestHabit = new ArrayList();
            public ArrayList LO_DataLicConst = new ArrayList();

            public ArrayList TradicaoDocumental = new ArrayList();
            public ArrayList Ordenacao = new ArrayList();
            public ArrayList ObjectosDigitais = new ArrayList();
            public ArrayList ObjectosDigitaisFedora = new ArrayList();
            public ArrayList Lingua = new ArrayList();
            public ArrayList Alfabeto = new ArrayList();
            public ArrayList FormaSuporteAcond = new ArrayList();
            public ArrayList MaterialSuporte = new ArrayList();
            public ArrayList TecnicaRegisto = new ArrayList();
            public OrderedDictionary HistAdministrativaBiografica = new OrderedDictionary();
            public Dictionary<ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet, ArrayList> InfoAdicional = new Dictionary<ReportParameterRelInvCatPesqDet.CamposRelInvCatPesqDet, ArrayList>();
			// ArrayList de String
			public void AddUpper(Nivel nvl) {
				Uppers.Add(nvl);
				if (!nvl.Lowers.Contains(this)) {
					nvl.Lowers.Add(this);
				}
			}
			public void AddLower(Nivel nvl) {
				Lowers.Add(nvl);
				if (!nvl.Uppers.Contains(this)) {
					nvl.Uppers.Add(this);
				}
			}
            public void AddProdutor(Nivel nvl) {
				if (!this.Produtores.Contains(nvl)) {
                    this.Produtores.Add(nvl);
				}
			}
		}
	}
}
