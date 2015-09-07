using System;
using System.Diagnostics;
using System.Data.SqlClient;

namespace DBAbstractDataLayer.DataAccessRules
{
	/// <summary>
	/// Summary description for ExceptionHelper.
	/// </summary>
	public class ExceptionHelper
	{
		public static bool isDeadlockException(Exception ex){

            if (ex.GetType() == typeof(SqlException) && ((SqlException)ex).Number == 1205) // deadlock
				return true;
						
			return false;
		}

		public static bool isTimeoutException(Exception ex){
			//Um timeout não é um erro do SqlServer, é um erro do 
			//DOTNETLIB. No entanto, qualquer que seja a origem do 
			//erro, o código de erro é sempre passado na propriedade 
			//Number da SqlException, independentemente da sua origem.
			//Para uma lista de erros do DOTNETLIB consultar a assembly 
			//System.Data.SqlClient.TdsParser.ProcessNetLibError usando 
			//o reflector.
			if (ex.GetType() == typeof(SqlException)){
				if(((SqlException)ex).Number == -2){ // timeout (DOTNETLIB, MDAC)
					return true;
				}
			}
			return false;
		}		
	}
}
