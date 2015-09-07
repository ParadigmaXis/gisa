using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.Controls
{
    public static class DateHelper
    {
        public static bool IsValidYear(string txt)
        {
            System.Text.RegularExpressions.Regex exp = new System.Text.RegularExpressions.Regex("^[0-9?]?[0-9?]?[0-9?]?[0-9?]?$");
            return exp.IsMatch(txt);
        }

        public static bool IsValidMonth(string txt)
        {
            System.Text.RegularExpressions.Regex exp = new System.Text.RegularExpressions.Regex("^[0?]?[1-9?]$|^[1?][0-2?]?$|^$");
            return exp.IsMatch(txt);
        }

        public static bool IsValidDay(string txt)
        {
            System.Text.RegularExpressions.Regex exp = new System.Text.RegularExpressions.Regex("^[0?]?[1-9?]$|^[1-2?][0-9?]$|^3[01?]$|^$");
            return exp.IsMatch(txt);
        }
    }
}
