using System;

namespace GISA.Utils {
	/// <summary>
	/// Summary description for GUIHelper.
	/// </summary>
	public class GUIHelper {

		public static string FormatDateInterval(string startDate, string endDate) {
			return string.Format("{0} - {1}", startDate, endDate);
		}

		public static string FormatDateInterval(string year1, string month1, string day1, string year2, string month2, string day2) {
			return string.Format("{0} - {1}", FormatDate(year1, month1, day1), FormatDate(year2, month2, day2));
		}

        public static string FormatDateInterval(string year1, string month1, string day1, bool isAtribuida1, string year2, string month2, string day2, bool isAtribuida2)
        {
            return string.Format("{0} - {1}", FormatDate(year1, month1, day1, isAtribuida1), FormatDate(year2, month2, day2, isAtribuida2));
        }

		public static string FormatDate(string year, string month, string day) {
			return FormatDate(year, month, day, false);
		}

		public static string FormatDate(string ano, string mes, string dia, bool isAtribuida) {
			if (ano.Length == 0)
				ano = "    ";
			if (mes.Length == 0)
				mes = "  ";
			if (dia.Length == 0)
				dia = "  ";
			if (isAtribuida) {
				return string.Format("[{0}/{1}/{2}]", ano, mes, dia);
			}
			else {
				return string.Format("{0}/{1}/{2}", ano, mes, dia);
			}
		}

		public static string ReadYear(string str) {
			// ToDo: passar a usar expressoes regulares, supondo que será mais eficiente
			if (str.Equals("???") || str.Equals("??") || str.Equals("?")) {
                while (str.Length < 4)
                    str = "0" + str;
			}
			
			return str;
			
		}

		public static string ReadMonth(string str) {
			// ToDo: passar a usar expressoes regulares, supondo que será mais eficiente
			if ( str.Equals("?")) {
				return "0" + str;
			}
			else {
				return str;
			}
		}

		public static string ReadDay(string str) {
			// ToDo: passar a usar expressoes regulares, supondo que será mais eficiente
            if (str.Equals("?"))
            {
                return "0" + str;
            }
			else {
				return str;
			}
		}

		public static System.DateTime getTruncatedCurrentDate() {
			System.DateTime currentDate = DateTime.Now;
			return new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, currentDate.Hour, currentDate.Minute, currentDate.Second);
		}

		public static string CapitalizeFirstLetter(string text)
        {
            string ret = string.Empty;

            if (text.Length == 0) return ret;

            try
            {
                ret = text.Substring(0, 1).ToUpper() + text.Substring(1, text.Length - 1);
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e.ToString());
                throw;
            }
            return ret;
		}

		public static string getStringifiedDecimal(object value){
			if(value == DBNull.Value){
				return string.Empty;
			}else{
				return value.ToString();
			}
		}
	}
}
