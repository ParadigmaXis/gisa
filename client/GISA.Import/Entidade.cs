using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.Import
{
    public abstract class Entidade
    {
        public string identificador = string.Empty;

        public static List<string> SplitListDisctinct(string val, char spliter)
        {
            if (val == null || val.Length == 0) return new List<string>();
            var a = val.Split(spliter).Where(s => s.Trim().Length > 0).Select(v => v.Trim()).Distinct().ToList();
            return a;
        }
    }
}
