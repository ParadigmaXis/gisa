using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GISA.GUIHelper;

namespace GISA.Import
{
    public static class ExceptionHelper
    {
        internal const string TAB_DOCUMENTOS = "Documentos";
        internal const string TAB_UNIDADES_FISICAS = "Unidades físicas";
        internal const string CAPTION = "Erro na importação";
        internal const string ERR_VALOR_INVALIDO = "Valor inválido";
        internal const string ERR_VALOR_NAO_DEFINIDO = "Valor não definido";
        internal const string ERR_VALOR_REPETIDO = "Valor repetido";
        internal const string ERR_ID_NAO_LISTADO = "Identificador não existente na lista";
        internal static Dictionary<string, string> errorHelpersUF = new Dictionary<string, string>() { 
            {ImportExcel.UF_IDENTIFICADOR, "A coluna Identificador deve ser obrigatoriamente preenchida com um valor alfanumérico e único na lista."},
            {ImportExcel.UF_TITULO, "A coluna Titulo deve ser obrigatoriamente preenchida."},
            {ImportExcel.UF_TIPOENTREGA, "A coluna TipoEntrega pode ser preenchida com um dos seguintes valores: 'Incorporação', 'Transferência', 'Depósito', 'Doação' ou 'Compra'."},
            {ImportExcel.UF_GUIA, "A coluna Guia pode ser preenchida com um alfanumérico representando o número da guia (por exemplo: '23/1999')."},
            {ImportExcel.UF_COTA, "A coluna Cota pode ser preenchido com um alfanumérico."},
            {ImportExcel.UF_ALTURA, "A coluna Altura pode ser preenchida com um número decimal."},
            {ImportExcel.UF_LARGURA, "A coluna Largura pode ser preenchida com um número decimal."},
            {ImportExcel.UF_PROFUNDIDADE, "A coluna Profundidade pode ser preenchida com um número decimal."},
            {ImportExcel.UF_TIPO, "A coluna Tipo pode ser preenchida com um tipo de acondicionamento (Exemplo: 'Livro', 'Maço', etc..) que já deverá existir no GISA."},
            {ImportExcel.UF_CODIGOBARRAS, "A coluna CodigoBarras, quando preenchida, deverá ter um número inteiro. Ter em conta que por vezes o Excel transforma os números em números com expoente, os quais não serão aceites pelo importador."},
            {ImportExcel.UF_CONTEUDOINFORMACIONAL, "A coluna ConteudoInformacional poderá conter um texto."},
            {ImportExcel.UF_ENTIDADEDETENTORA, "A coluna EntidadeDetentora deverá conter a designação de uma Entidade detentora já existente no GISA."},
            {ImportExcel.UF_ANOINICIO, "A coluna AnoInicio admite 4 dígitos, que deverão ser números (por exemplo, '2003',…) ou '?' em substituição para indicar que não se conhece o dígito. (por exemplo sec XX será '19??')."},
            {ImportExcel.UF_MESINICIO, "A coluna MesInicio admite 2 dígitos correspondendo a um mês válido (por exemplo, '04',…) ou '?' em substituição para indicar que não se conhece o dígito."},
            {ImportExcel.UF_DIAINICIO, "A coluna DiaInicio admite 2 dígitos correspondendo a um dia válido (por exemplo, '23',…) ou '?' em substituição para indicar que não se conhece o dígito."},
            {ImportExcel.UF_ATRIBUIDAINICIO, "A coluna AtribuidaInicio admite '0' quando a data está na unidade física ou '1' quando a data não se encontra expressamente na unidade física."},
            {ImportExcel.UF_ANOFIM, "A coluna AnoFim admite 4 dígitos, que deverão ser números (por exemplo, '2003',…) ou '?' em substituição para indicar que não se conhece o dígito. (por exemplo sec XX será '19??')."},
            {ImportExcel.UF_MESFIM, "A coluna MesFim admite 2 dígitos correspondendo a um mês válido (por exemplo, '04',…) ou '?' em substituição para indicar que não se conhece o dígito."},
            {ImportExcel.UF_DIAFIM, "A coluna DiaFim admite 2 dígitos correspondendo a um dia válido (por exemplo, '23',…) ou '?' em substituição para indicar que não se conhece o dígito."},
            {ImportExcel.UF_ATRIBUIDAFIM, "A coluna AtribuidaFim admite '0' quando a data está na unidade física ou '1' quando a data não se encontra expressamente na unidade física."},
            {ImportExcel.UF_LOCALCONSULTA, "A coluna LocalConsulta deverá conter um local de consulta já definido no GISA."}
        };

        internal static Dictionary<string, string> errorHelpersUI = new Dictionary<string, string>() { 
            {ImportExcel.UI_IDENTIFICADOR, @"A coluna Identificador deve ser obrigatoriamente preenchida com um valor alfanumérico e único na lista."},
            {ImportExcel.UI_NIVEL, @"A coluna Nível é de preenchimento obrigatório e admite os valores: 'Documento/Processo' (quando  nível superior for uma Entidade produtora ou um nível do tipo Série ou Subsérie) ou 'Documento subordinado/Ato informacional' (quando  nível superior for um nível do tipo Documento/Processo"},
            {ImportExcel.UI_CODIGOREF, "A coluna CodigoRef é de preenchimento obrigatório com o código de referência parcial relativo ao nível de descrição em causa, sendo um alfanumérico sem caracteres espaço."},
            {ImportExcel.UI_TITULO, "A coluna Titulo deve ser obrigatoriamente preenchida."},
            {ImportExcel.UI_IDNIVELSUPERIOR, "A coluna IDNivelSuperior quando preenchida, pode ter um valor da coluna Identificador da mesma tabela ou então um identificador GISA com o seguinte formato 'gisa:12345'."},
            {ImportExcel.UI_IDNIVELSUPERIOR2, "A coluna IDNivelSuperior quando preenchida com um identificador GISA deverá ser com um que seja referente a uma série, subsérie ou documento."},
            {ImportExcel.UI_ENTIDADESPRODUTORAS, "A coluna EntidadesProdutoras poderá ser preenchida usando o ou os termos autorizados de Entidades produtoras existentes no GISA. Separar as várias entidades produtoras com ';'."},
            {ImportExcel.UI_IDNIVELSUPERIOR + ", " + ImportExcel.UI_ENTIDADESPRODUTORAS, "É obrigatório o preenchimento de uma e uma só das duas seguintes colunas: IDNivelSuperior ou EntidadesProdutoras."},
            {ImportExcel.UI_AUTORES, "A coluna Autores poderá ser preenchida usando o ou os termos autorizados de Entidades produtoras existentes no GISA. Separar os vários autores com ';'."},
            {ImportExcel.UI_DATAINCERTA, "A coluna DataIncerta admite os seguintes valores: 'Antes de', 'Depois de' ou 'Cerca de'"},
            {ImportExcel.UI_ANOINICIO, "A coluna AnoInicio admite 4 dígitos, que deverão ser números (por exemplo, '2003',…) ou '?' em substituição para indicar que não se conhece o dígito. (por exemplo sec XX será '19??')."},
            {ImportExcel.UI_MESINICIO, "A coluna MesInicio admite 2 dígitos correspondendo a um mês válido (por exemplo, '04',…) ou '?' em substituição para indicar que não se conhece o dígito."},
            {ImportExcel.UI_DIAINICIO, "A coluna DiaInicio admite 2 dígitos correspondendo a um dia válido (por exemplo, '23',…) ou '?' em substituição para indicar que não se conhece o dígito."},
            {ImportExcel.UI_ATRIBUIDAINICIO, "A coluna AtribuidaInicio admite '0' quando a data está no documento ou '1' quando a data não se encontra expressamente no documento."},
            {ImportExcel.UI_ANOFIM, "A coluna AnoFim admite 4 dígitos, que deverão ser números (por exemplo, '2003',…) ou '?' em substituição para indicar que não se conhece o dígito. (por exemplo sec XX será '19??')."},
            {ImportExcel.UI_MESFIM, "A coluna MesFim admite 2 dígitos correspondendo a um mês válido (por exemplo, '04',…) ou '?' em substituição para indicar que não se conhece o dígito."},
            {ImportExcel.UI_DIAFIM, "A coluna DiaFim admite 2 dígitos correspondendo a um dia válido (por exemplo, '23',…) ou '?' em substituição para indicar que não se conhece o dígito."},
            {ImportExcel.UI_ATRIBUIDAFIM, "A coluna AtribuidaFim admite '0' quando a data está no documento ou '1' quando a data não se encontra expressamente no documento."},
            {ImportExcel.UI_DIMENSAOUNIDADEINFORMACIONAL, "Campo de texto DimensaoUnidadeInformacional não obrigatório."},
            {ImportExcel.UI_UNIDADESFISICAS, "A coluna UnidadesFisicas quando preenchida, pode ter um ou mais valores existentes na coluna Identificador da tabela UnidadesFisicas e/ou um ou vários identificadores GISA (código da unidade física) com o seguinte formato 'gisa_uf:PT/UF2011-1'. No caso de ser mais do que uma Unidade Física, os valores separam-se por ';'."},
            {ImportExcel.UI_COTADOC, "CotaDoc"},
            {ImportExcel.UI_HISTORIAADMINISTRATIVA, "A coluna HistoriaAdministrativa deve ser preenchida com texto."},
            {ImportExcel.UI_HISTORIAARQUIVISTICA, "A coluna HistoriaArquivistica deve ser preenchida com texto."},
            {ImportExcel.UI_FONTEAQUISICAOOUTRANSFERENCIA, "A coluna FonteAquisicaoOuTransferencia deve ser preenchida com texto."},
            {ImportExcel.UI_TIPOINFORMACIONAL, "A coluna TipoInformacional quando preenchida só admite um termo autorizado de uma Tipologia informacional já existente no GISA."},
            {ImportExcel.UI_DIPLOMALEGAL, "A coluna Diploma legal quando preenchida só admite diplomas já existentes no GISA. Cada diploma legal indicado deverá existir como diploma no GISA. Para o caso de ser mais do que um diploma, estes separam-se por ';'."},
            {ImportExcel.UI_MODELO, "A coluna Modelo quando preenchida só admite modelos já existentes no GISA. Cada modelo indicado deverá existir como modelo no GISA. Para o caso de ser mais do que um modelo, estes separam-se por ';'."},
            {ImportExcel.UI_CONTEUDOINFORMACIONAL, "A colun a ConteudoInformacional deve ser preenchida com texto."},
            {ImportExcel.UI_DESTINOFINAL, "A coluna DestinoFinal quando preenchida admite os valores 'Conservação' ou 'Eliminação'."},
            {ImportExcel.UI_PUBLICACAO, "A coluna Publicacao quando preenchida admite os valores '0' ou '1'."},
            {ImportExcel.UI_INCORPORACOES, "A coluna Incorporaçoes deve ser preenchida com texto."},
            {ImportExcel.UI_TRADICAODOCUMENTAL, "A coluna TradicaoDocumental quando preenchida admite os seguintes valores:'Cópia', 'Minuta', 'Original' ou 'Resumo'. Quando existe mais que um valor separar por ';'."},
            {ImportExcel.UI_ORDENACAO, "A coluna Ordenaçao quando preenchida admite os seguintes valores: 'Aleatória', 'Alfabética', 'Cronológica', 'Numérica' ou 'Sistemática'. Quando existe mais que um valor separar por ';'."},
            {ImportExcel.UI_CONDICOESACESSO, "A coluna CondicoesAcesso deve ser preenchida com texto."},
            {ImportExcel.UI_CONDICOESREPRODUCAO, "A coluna CondicoesReproducao deve ser preenchida com texto."},
            {ImportExcel.UI_LINGUA, "A coluna Lingua quando preenchida admite a designações da ISO 639. Quando existe mais que um valor separar por ';'."},
            {ImportExcel.UI_ALFABETO, "A coluna Alfabeto quando preenchida admite a designações da ISO 15924. Quando existe mais que um valor separar por ';'."},
            {ImportExcel.UI_FORMASUPORTE, "A coluna FormaSuporte quando preenchida admite os seguintes valores: 'Folhas', 'Caderneta', 'Caderno', 'Livro', 'Fichas', 'Caixa', 'Maço', 'Pasta', 'Envelope', 'Rolo', 'Bobina', 'Cassete', 'Disco', 'Disquete' ou 'Outra'. Quando existe mais que um valor separar por ';'."},
            {ImportExcel.UI_MATERIALSUPORTE, "A coluna MaterialSuporte quando preenchida admite os seguintes valores: 'Papel', 'Pergaminho', 'Película', 'Vidro', 'Metal', 'Tecido', 'Vinil', 'PVC', 'Tela', 'Pele' ou 'Outro'. Quando existe mais que um valor separar por ';'."},
            {ImportExcel.UI_TECNICAREGISTO, "A coluna TecnicaRegisto quando preenchida admite os seguintes valores: 'Manuscrito', 'Impresso', 'Gravura', 'Fotografia', 'Microfilme', 'Filme', 'Áudio', 'Audiovisual', 'Magnético', 'Óptico', 'Multimédia' ou 'Outra'. Quando existe mais que um valor separar por ';'."},
            {ImportExcel.UI_ESTADOCONSERVACAO, "A coluna EstadoConservacao quando preenchida admite um dos seguintes valores: 'Bom', 'Mau' ou 'Razoável'."},
            {ImportExcel.UI_INSTRUMENTOSPESQUISA, "A coluna InstrumentosPesquisa deve ser preenchida com texto."},
            {ImportExcel.UI_EXISTENCIAORIGINAIS, "A coluna ExistenciaOriginais deve ser preenchida com texto."},
            {ImportExcel.UI_EXISTENCIACOPIAS, "A coluna ExistenciaCopias deve ser preenchida com texto."},
            {ImportExcel.UI_UNIDADESDESCRICAORELACIONADAS, "A coluna UnidadesDescricaoRelacionadas deve ser preenchida com texto."},
            {ImportExcel.UI_NOTAPUBLICACAO, "A coluna NotaPublicacao deve ser preenchida com texto."},
            {ImportExcel.UI_NOTAS, "A coluna Notas deve ser preenchida com texto."},
            {ImportExcel.UI_NOTAARQUIVISTA, "A coluna NotaArquivista deve ser preenchida com texto."},
            {ImportExcel.UI_REGRAS, "A coluna Regras deve ser preenchida com texto."},
            {ImportExcel.UI_AUTORDESCRICAO, "A coluna AutorDescricao deve ser preenchida com o nome de um utilizador já criado no Gisa. Esse utilizador deve estar marcado como autor."},
            {ImportExcel.UI_DATAAUTORIA, "A coluna Data autoria quando preenchida deve ter o formato AAAAMMDD, em que AAAA é o ano, MM o mês e DD o dia."},
            {ImportExcel.UI_ONOMASTICOS, "A coluna Onomasticos quando preenchida deve ter um termo (ou uma lista de termos separados por ';') autorizado já existente no GISA como Onomástico."},
            {ImportExcel.UI_IDEOGRAFICOS, "A coluna Ideograficos quando preenchida deve ter um termo (ou uma lista de termos separados por ';') autorizado já existente no GISA como Ideográfico."},
            {ImportExcel.UI_GEOGRAFICOS, "A coluna Geograficos quando preenchida deve ter um termo (ou uma lista de termos separados por ';') autorizado já existente no GISA como 'Nome geográfico/Topónimo citadino'."}
        };

        private static void ShowDialog(string tabela, string identificador, string coluna, string valor, string erro, string ajuda)
        {
            var form = new FormImportErrorReport();

            form.SetTitle(CAPTION);
            form.SetTabela(tabela);
            form.SetIdentificador(identificador);
            form.SetColuna(coluna);
            form.SetValor(valor);
            form.SetErro(erro);
            form.SetAjuda(ajuda);
            
            form.ShowDialog();
        }

        internal static void ThrowException(string tabela, string identificador, string coluna, string valor, string erro)
        {
            var ajuda = string.Empty;

            if (tabela.Equals(ExceptionHelper.TAB_DOCUMENTOS))
                ajuda = errorHelpersUI[coluna];
            else if (tabela.Equals(ExceptionHelper.TAB_UNIDADES_FISICAS))
                ajuda = errorHelpersUF[coluna];

            ShowDialog(tabela, identificador, coluna, valor, erro, ajuda);
            ComposeAndThrowException(tabela, string.Empty, coluna, valor, erro);
        }

        private static void ComposeAndThrowException(string tabela, string identificador, string coluna, string valor, string erro)
        {
            var exceptionMsg = new StringBuilder(CAPTION);
            exceptionMsg.AppendLine(string.Format("{0}: {1}", "Tabela", tabela));
            exceptionMsg.AppendLine(string.Format("{0}: {1}", "Identificador", identificador));
            exceptionMsg.AppendLine(string.Format("{0}: {1}", "Coluna", coluna));
            exceptionMsg.AppendLine(string.Format("{0}: {1}", "Valor", valor));
            exceptionMsg.AppendLine(string.Format("{0}: {1}", "Erro", erro));
            throw new Exception(exceptionMsg.ToString());
        }
    }
}
