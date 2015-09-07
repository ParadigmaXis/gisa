using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA.GUIHelper
{
	public sealed class TranslationHelper
	{
		public static string FormatBoolean(bool Value)
		{
			if (Value)
				return "Sim";
			else
				return "Não";
		}

		public static string FormatTipoAcessoEnumToTipoAcessoText(ResourceAccessType Value)
		{
            switch (Value)
            {
                case ResourceAccessType.Smb:
                    return "Directório";
                case ResourceAccessType.Web:
                    return "Web";
                case ResourceAccessType.Fedora:
                    return "Fedora";
                case ResourceAccessType.DICAnexo:
                    return "DocInPorto Anexo";
                case ResourceAccessType.DICConteudo:
                    return "DocInPorto Conteúdo";
            }
			return null;
		}

		public static ResourceAccessType FormatTipoAcessoTextToTipoAcessoEnum(string Value)
		{
            switch (Value)
            {
                case "Directório":
                    return ResourceAccessType.Smb;
                case "Web":
                    return ResourceAccessType.Web;
                case "Fedora":
                    return ResourceAccessType.Fedora;
                case "DocInPorto Anexo":
                    return ResourceAccessType.DICAnexo;
                case "DocInPorto Conteúdo":
                    return ResourceAccessType.DICConteudo;
            }
			
			return ResourceAccessType.Smb;
		}

		public static string FormatModPesquisaIntToText(ModuloPesquisa Value)
		{
			if (Value == ModuloPesquisa.Publicacao)
                return "Pesquisa pública";
			else if (Value == ModuloPesquisa.Recolha)
				return "Pesquisa total";
            else if (Value == ModuloPesquisa.NaoPublicados)
                return "Pesquisa não publicados";

			return null;
		}

		public static ModuloPesquisa FormatModPesquisaTextToInt(string Value)
		{
            if (Value.Equals("Pesquisa pública"))
                return ModuloPesquisa.Publicacao;
            else if (Value.Equals("Pesquisa total"))
                return ModuloPesquisa.Recolha;
            else if (Value.Equals("Pesquisa não publicados"))
                return ModuloPesquisa.NaoPublicados;

			return ModuloPesquisa.Publicacao;
		}
	}
}