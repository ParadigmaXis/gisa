using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace GISA.Import
{
    public abstract class ImportExcel
    {
        private const int DOCUMENTOS = 0;
        private const int UNIDADES_FISICAS = 1;

        internal const string UF_IDENTIFICADOR = "Identificador";
        internal const string UF_TITULO = "Titulo";
        internal const string UF_TIPOENTREGA = "TipoEntrega";
        internal const string UF_GUIA = "Guia";
        internal const string UF_COTA = "Cota";
        internal const string UF_ALTURA = "Altura";
        internal const string UF_LARGURA = "Largura";
        internal const string UF_PROFUNDIDADE = "Profundidade";
        internal const string UF_TIPO = "Tipo";
        internal const string UF_CODIGOBARRAS = "CodigoBarras";
        internal const string UF_CONTEUDOINFORMACIONAL = "ConteudoInformacional";
        internal const string UF_ENTIDADEDETENTORA = "EntidadeDetentora";
        internal const string UF_ANOINICIO = "AnoInicio";
        internal const string UF_MESINICIO = "MesInicio";
        internal const string UF_DIAINICIO = "DiaInicio";
        internal const string UF_ATRIBUIDAINICIO = "AtribuidaInicio";
        internal const string UF_ANOFIM = "AnoFim";
        internal const string UF_MESFIM = "MesFim";
        internal const string UF_DIAFIM = "DiaFim";
        internal const string UF_ATRIBUIDAFIM = "AtribuidaFim";
        internal const string UF_LOCALCONSULTA = "LocalConsulta";

        internal const string UI_IDENTIFICADOR = "Identificador";
        internal const string UI_NIVEL = "Nível";
        internal const string UI_CODIGOREF = "CodigoRef";
        internal const string UI_TITULO = "Titulo";
        internal const string UI_IDNIVELSUPERIOR = "IDNivelSuperior";
        internal const string UI_IDNIVELSUPERIOR2 = "IDNivelSuperior ";
        internal const string UI_ENTIDADESPRODUTORAS = "EntidadesProdutoras";
        internal const string UI_AUTORES = "Autores";
        internal const string UI_DATAINCERTA = "DataIncerta";
        internal const string UI_ANOINICIO = "AnoInicio";
        internal const string UI_MESINICIO = "MesInicio";
        internal const string UI_DIAINICIO = "DiaInicio";
        internal const string UI_ATRIBUIDAINICIO = "AtribuidaInicio";
        internal const string UI_ANOFIM = "AnoFim";
        internal const string UI_MESFIM = "MesFim";
        internal const string UI_DIAFIM = "DiaFim";
        internal const string UI_ATRIBUIDAFIM = "AtribuidaFim";
        internal const string UI_DIMENSAOUNIDADEINFORMACIONAL = "DimensaoUnidadeInformacional";
        internal const string UI_UNIDADESFISICAS = "UnidadesFisicas";
        internal const string UI_COTADOC = "CotaDoc";
        internal const string UI_HISTORIAADMINISTRATIVA = "HistoriaAdministrativa";
        internal const string UI_HISTORIAARQUIVISTICA = "HistoriaArquivistica";
        internal const string UI_FONTEAQUISICAOOUTRANSFERENCIA = "FonteAquisicaoOuTransferencia";
        internal const string UI_TIPOINFORMACIONAL = "TipoInformacional";
        internal const string UI_DIPLOMALEGAL = "Diploma legal";
        internal const string UI_MODELO = "Modelo";
        internal const string UI_CONTEUDOINFORMACIONAL = "ConteudoInformacional";
        internal const string UI_DESTINOFINAL = "DestinoFinal";
        internal const string UI_PUBLICACAO = "Publicacao";
        internal const string UI_INCORPORACOES = "Incorporaçoes";
        internal const string UI_TRADICAODOCUMENTAL = "TradicaoDocumental";
        internal const string UI_ORDENACAO = "Ordenaçao";
        internal const string UI_CONDICOESACESSO = "CondicoesAcesso";
        internal const string UI_CONDICOESREPRODUCAO = "CondicoesReproducao";
        internal const string UI_LINGUA = "Lingua";
        internal const string UI_ALFABETO = "Alfabeto";
        internal const string UI_FORMASUPORTE = "FormaSuporte";
        internal const string UI_MATERIALSUPORTE = "MaterialSuporte";
        internal const string UI_TECNICAREGISTO = "TecnicaRegisto";
        internal const string UI_ESTADOCONSERVACAO = "EstadoConservacao";
        internal const string UI_INSTRUMENTOSPESQUISA = "InstrumentosPesquisa";
        internal const string UI_EXISTENCIAORIGINAIS = "ExistenciaOriginais";
        internal const string UI_EXISTENCIACOPIAS = "ExistenciaCopias";
        internal const string UI_UNIDADESDESCRICAORELACIONADAS = "UnidadesDescricaoRelacionadas";
        internal const string UI_NOTAPUBLICACAO = "NotaPublicacao";
        internal const string UI_NOTAS = "Notas";
        internal const string UI_NOTAARQUIVISTA = "NotaArquivista";
        internal const string UI_REGRAS = "Regras";
        internal const string UI_AUTORDESCRICAO = "AutorDescricao";
        internal const string UI_DATAAUTORIA = "Data autoria";
        internal const string UI_ONOMASTICOS = "Onomasticos";
        internal const string UI_IDEOGRAFICOS = "Ideograficos";
        internal const string UI_GEOGRAFICOS = "Geograficos";

        private List<UnidadeInformacional> uis;
        private List<UnidadeFisica> ufs;

        protected string fileName;

        public ImportExcel(String fName) { fileName = fName; }

        protected abstract DataSet ReadFile();

        //public List<UnidadeInformacional> GetUnidadesInformacionais { get { return uis.Where(ui => ui.identificador.Length > 0).ToList(); } }
        //public List<UnidadeFisica> GetUnidadesFisicas { get { return ufs.Where(uf => uf.identificador.Length > 0).ToList(); } }
        public List<UnidadeInformacional> GetUnidadesInformacionais { get { return uis; } }
        public List<UnidadeFisica> GetUnidadesFisicas { get { return ufs; } }

        public void Import()
        {
            try
            {
                var dsImport = ReadFile();
                ufs = dsImport.Tables[UNIDADES_FISICAS].Rows.Cast<DataRow>()
                        .Select(r => new UnidadeFisica()
                        {
                            identificador = Read(r, UF_IDENTIFICADOR),
                            titulo = Read(r, UF_TITULO),
                            tipoEntrega = Read(r, UF_TIPOENTREGA),
                            guia = Read(r, UF_GUIA),
                            cota = Read(r, UF_COTA),
                            altura = Read(r, UF_ALTURA),
                            largura = Read(r, UF_LARGURA),
                            profundidade = Read(r, UF_PROFUNDIDADE),
                            tipo = Read(r, UF_TIPO),
                            codigoBarras = Read(r, UF_CODIGOBARRAS),
                            conteudoInformacional = Read(r, UF_CONTEUDOINFORMACIONAL),
                            entidadeDetentora = Read(r, UF_ENTIDADEDETENTORA),
                            anoInicio = Read(r, UF_ANOINICIO),
                            mesInicio = Read(r, UF_MESINICIO),
                            diaInicio = Read(r, UF_DIAINICIO),
                            atribuidaInicio = Read(r, UF_ATRIBUIDAINICIO),
                            anoFim = Read(r, UF_ANOFIM),
                            mesFim = Read(r, UF_MESFIM),
                            diaFim = Read(r, UF_DIAFIM),
                            atribuidaFim = Read(r, UF_ATRIBUIDAFIM),
                            localConsulta = Read(r, UF_LOCALCONSULTA)
                        }).ToList();

                uis = dsImport.Tables[DOCUMENTOS].Rows.Cast<DataRow>()
                        .Select(r => new UnidadeInformacional()
                        {
                            identificador = Read(r, UI_IDENTIFICADOR),
                            nivel = Read(r, UI_NIVEL),
                            codigoRef = Read(r, UI_CODIGOREF),
                            titulo = Read(r, UI_TITULO),
                            idNivelSuperior = Read(r, UI_IDNIVELSUPERIOR),
                            entidadesProdutoras = Entidade.SplitListDisctinct(Read(r, UI_ENTIDADESPRODUTORAS), ';'),
                            autores = Entidade.SplitListDisctinct(Read(r, UI_AUTORES), ';'),
                            dataIncerta = Read(r, UI_DATAINCERTA),
                            anoInicio = Read(r, UI_ANOINICIO),
                            mesInicio = Read(r, UI_MESINICIO),
                            diaInicio = Read(r, UI_DIAINICIO),
                            atribuidaInicio = Read(r, UI_ATRIBUIDAINICIO),
                            anoFim = Read(r, UI_ANOFIM),
                            mesFim = Read(r, UI_MESFIM),
                            diaFim = Read(r, UI_DIAFIM),
                            atribuidaFim = Read(r, UI_ATRIBUIDAFIM),
                            dimensaoUnidadeInformacional = Read(r, UI_DIMENSAOUNIDADEINFORMACIONAL),
                            unidadesFisicas = Entidade.SplitListDisctinct(Read(r, UI_UNIDADESFISICAS), ';'),
                            cotaDoc = Read(r, UI_COTADOC),
                            historiaAdministrativa = Read(r, UI_HISTORIAADMINISTRATIVA),
                            historiaArquivistica = Read(r, UI_HISTORIAARQUIVISTICA),
                            fonteAquisicaoOuTransferencia = Read(r, UI_FONTEAQUISICAOOUTRANSFERENCIA),
                            tipoInformacional = Read(r, UI_TIPOINFORMACIONAL),
                            diplomaLegal = Entidade.SplitListDisctinct(Read(r, UI_DIPLOMALEGAL), ';'),
                            modelo = Entidade.SplitListDisctinct(Read(r, UI_MODELO), ';'),
                            conteudoInformacional = Read(r, UI_CONTEUDOINFORMACIONAL),
                            destinoFinal = Read(r, UI_DESTINOFINAL),
                            publicacao = Read(r, UI_PUBLICACAO),
                            incorporacoes = Read(r, UI_INCORPORACOES),
                            tradicaoDocumental = Entidade.SplitListDisctinct(Read(r, UI_TRADICAODOCUMENTAL), ';'),
                            ordenacao = Entidade.SplitListDisctinct(Read(r, UI_ORDENACAO), ';'),
                            condicoesAcesso = Read(r, UI_CONDICOESACESSO),
                            condicoesReproducao = Read(r, UI_CONDICOESREPRODUCAO),
                            lingua = Entidade.SplitListDisctinct(Read(r, UI_LINGUA), ';'),
                            alfabeto = Entidade.SplitListDisctinct(Read(r, UI_ALFABETO), ';'),
                            formaSuporte = Entidade.SplitListDisctinct(Read(r, UI_FORMASUPORTE), ';'),
                            materialSuporte = Entidade.SplitListDisctinct(Read(r, UI_MATERIALSUPORTE), ';'),
                            tecnicaRegisto = Entidade.SplitListDisctinct(Read(r, UI_TECNICAREGISTO), ';'),
                            estadoConservacao = Read(r, UI_ESTADOCONSERVACAO),
                            instrumentosPesquisa = Read(r, UI_INSTRUMENTOSPESQUISA),
                            existenciaOriginais = Read(r, UI_EXISTENCIAORIGINAIS),
                            existenciaCopias = Read(r, UI_EXISTENCIACOPIAS),
                            unidadesDescricaoRelacionadas = Read(r, UI_UNIDADESDESCRICAORELACIONADAS),
                            notaPublicacao = Read(r, UI_NOTAPUBLICACAO),
                            notas = Read(r, UI_NOTAS),
                            notaArquivista = Read(r, UI_NOTAARQUIVISTA),
                            regras = Read(r, UI_REGRAS),
                            autorDescricao = Read(r, UI_AUTORDESCRICAO),
                            dataAutoria = Read(r, UI_DATAAUTORIA),
                            onomasticos = Entidade.SplitListDisctinct(Read(r, UI_ONOMASTICOS), ';'),
                            ideograficos = Entidade.SplitListDisctinct(Read(r, UI_IDEOGRAFICOS), ';'),
                            geograficos = Entidade.SplitListDisctinct(Read(r, UI_GEOGRAFICOS), ';')
                        }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string Read(DataRow row, string column)
        {
            return row[column].ToString().Trim();
        }
    }
}
