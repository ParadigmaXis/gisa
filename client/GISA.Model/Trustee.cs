using System;
using System.Collections;
using System.Data;
using System.Diagnostics;

using System.Security.Principal;
using DBAbstractDataLayer.DataAccessRules;

#region FIXME Move this region to GISA.Security.dll

namespace GISA.Model
{
	public enum TipoFunctionGroup: int
	{
		FolhaDeRecolhaDeDados = 1,
		ControloDeAutoridade = 2
	}

	public enum TipoFunction: int
	{
		Recolha = 1,
		AgregacaoESeleccao = 2,
		Publicacao = 3,
		DiplomasEModelos = 4, //?

		EntidadeProdutora = 1,
		Conteúdo = 2,
		TipologiaInformacional = 3
	}

	public enum TipoOperation: byte
	{
		CREATE = 1,
		READ = 2,
		WRITE = 3,
		DELETE = 4,
		EXPAND = 7
	}



	#endregion

	public class Trustee
	{
		public static TrusteeRule.IndexErrorMessages validateUser(string username, string password)
		{
			TrusteeRule.IndexErrorMessages messageCode = 0;
			IDbConnection conn = GisaDataSetHelper.GetConnection();

			try
			{
				conn.Open();

				//validar utilizador
				messageCode = TrusteeRule.Current.validateUser(username, password, conn);
			}
			catch (Exception ex)
			{
				Trace.WriteLine("Register Client Application: " + ex.ToString());
			}
			finally
			{
				conn.Close();
			}

			return messageCode;
		}


		public static ArrayList GetAvailableAuthors()
		{
			return GetAvailableAuthors(false);
		}

		public static ArrayList GetAvailableAuthors(bool useTRowInsteadOfTuRow)
		{
			ArrayList result = new ArrayList();
			foreach (GISADataset.TrusteeUserRow usrRow in GisaDataSetHelper.GetInstance().TrusteeUser.Select("IsAuthority=1"))
			{
				if (!usrRow.TrusteeRow.BuiltInTrustee)
				{
					if (useTRowInsteadOfTuRow)
					{
						result.Add(usrRow.TrusteeRow);
					}
					else
					{
						result.Add(usrRow);
					}
				}
			}
			return result;
		}

	}

} //end of root namespace