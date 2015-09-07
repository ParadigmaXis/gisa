using System;
using System.Collections;
using System.Data;
using System.Diagnostics;

namespace GISA.Model
{
	public sealed class DomainValuesHelper
	{
		public static string stringifyEnumValue(object enumValue)
		{
			return System.Enum.Format(enumValue.GetType(), enumValue, "D");
		}
	}

// Todos estes enumerados deveriam ser gerados a partir do metamodelo
	public enum TipoNoticiaAut: int
	{
		Ideografico = 1,
		Onomastico = 2,
		ToponimicoGeografico = 3,
		EntidadeProdutora = 4,
		TipologiaInformacional = 5,
		// SubtipologiaInformacional = 6
		Diploma = 7,
		Modelo = 8
	}

	public enum IndexFRDCASelector: int
	{
		SubtipologiaInformacional = 6
	}

	public enum TipoControloAutForma: int
	{
		FormaAutorizada = 1,
        FormaParalela = 2,
        FormaNormalizada = 3,
        OutraForma = 4
	}

	public enum TipoFRDBase: int
	{
		FRDOIRecolha = 1,
		FRDUnidadeFisica = 2
	}

	public enum ModuloPesquisa: int
	{	
		Recolha = 0,
		Publicacao = 1,
        NaoPublicados = 2		
	}

	public enum TipoSuporte: int
	{
		Desconhecido = 1
	}

	public enum TipoAcondicionamento: int
	{
		Pasta = 1
	}

	public enum TipoControloAutRel: int
	{
		TermoGenerico = 1,
		TermoRelacionado = 2,
		Hierarquica = 3,
		Temporal = 4,
		Familiar = 5,
		Associativa = 6,
		Instituicao = 7
	}

	public enum Modulos: int
	{
		Core = 1,
		Requisicoes = 2,
        Depositos = 3
	}

	public enum TipoServer: int
	{
		Monoposto = 1,
		ClienteServidor = 2
	}

    public enum ResourceAccessType : int
    {
        Smb,
        Web,
        Fedora,
        DICAnexo,
        DICConteudo
    }
}
