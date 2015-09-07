using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.Import
{
    public class UnidadeInformacional : Entidade
    {
        public string nivel = string.Empty;
        public string codigoRef = string.Empty;
        public string titulo = string.Empty;
        public string idNivelSuperior = string.Empty;
        public List<string> entidadesProdutoras = new List<string>();
        public List<string> autores = new List<string>();
        public string dataIncerta = string.Empty;
        public string anoInicio = string.Empty;
        public string mesInicio = string.Empty;
        public string diaInicio = string.Empty;
        public string atribuidaInicio = string.Empty;
        public string anoFim = string.Empty;
        public string mesFim = string.Empty;
        public string diaFim = string.Empty;
        public string atribuidaFim = string.Empty;
        public string dimensaoUnidadeInformacional = string.Empty;
        public List<string> unidadesFisicas = new List<string>();
        public string cotaDoc = string.Empty;
        public string historiaAdministrativa = string.Empty;
        public string historiaArquivistica = string.Empty;
        public string fonteAquisicaoOuTransferencia = string.Empty;
        public string tipoInformacional = string.Empty;
        public List<string> diplomaLegal = new List<string>();
        public List<string> modelo = new List<string>();
        public string conteudoInformacional = string.Empty;
        public string destinoFinal = string.Empty;
        public string publicacao = string.Empty;
        public string incorporacoes = string.Empty;
        public List<string> tradicaoDocumental = new List<string>();
        public List<string> ordenacao = new List<string>();
        public string condicoesAcesso = string.Empty;
        public string condicoesReproducao = string.Empty;
        public List<string> lingua = new List<string>();
        public List<string> alfabeto = new List<string>();
        public List<string> formaSuporte = new List<string>();
        public List<string> materialSuporte = new List<string>();
        public List<string> tecnicaRegisto = new List<string>();
        public string estadoConservacao = string.Empty;
        public string instrumentosPesquisa = string.Empty;
        public string existenciaOriginais = string.Empty;
        public string existenciaCopias = string.Empty;
        public string unidadesDescricaoRelacionadas = string.Empty;
        public string notaPublicacao = string.Empty;
        public string notas = string.Empty;
        public string notaArquivista = string.Empty;
        public string regras = string.Empty;
        public string autorDescricao = string.Empty;
        public string dataAutoria = string.Empty;
        public List<string> onomasticos = new List<string>();
        public List<string> ideograficos = new List<string>();
        public List<string> geograficos = new List<string>();

        public UnidadeInformacional() { }
    }
}
