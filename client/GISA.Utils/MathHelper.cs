using System;
using System.Text.RegularExpressions;

namespace GISA.Utils
{
	public class MathHelper
	{
		private static Regex IsIntegerRegex = new Regex("[0-9]+", RegexOptions.Compiled);
		private static Regex IsDecimalRegex = new Regex(string.Format("[0-9]+[{0}]?[0-9]*", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator), RegexOptions.Compiled);

		public static bool IsInteger(string txt) {
            Match result = IsIntegerRegex.Match(txt);
			return result.Success && result.Value.Equals(txt);
		}

		public static bool IsDecimal(string txt) {
			Match result = IsDecimalRegex.Match(txt);
			return result.Success && result.Value.Equals(txt);
		}
	}
}
