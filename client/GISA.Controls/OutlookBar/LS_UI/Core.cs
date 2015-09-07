using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace LumiSoft.UI
{
	/// <summary>
	/// Summary description for Core.
	/// </summary>
	public class Core
	{
		public Core()
		{			
		}

		
		#region function LoadIcon
		
		public static Icon LoadIcon(string iconName)
		{			
			Stream strm = Type.GetType("LumiSoft.UI.Core").Assembly.GetManifestResourceStream("LumiSoft.UI.res." + iconName);
			
			Icon ic = null;
			if(strm != null){
				ic = new System.Drawing.Icon(strm);
				strm.Close();
			}

			return ic;
		}

		#endregion

		
		#region fucntion ConvertToDeciaml

		/// <summary>
		/// Converts string value to decimal.
		/// If convert fails, returns 0;
		/// </summary>
		/// <param name="val">String value to convert.</param>
		/// <returns></returns>
		public static decimal ConvertToDeciaml(string val)
		{
			decimal retVal = 0;

			try
			{
				retVal = Convert.ToDecimal(val);
			}
			catch
			{
				retVal = 0;
			}

			return retVal;
		}

		#endregion
	}
}
