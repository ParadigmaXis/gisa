using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Lucene.Net.QueryParsers;

using GISA.Utils;

namespace GISA.Search
{
    public class Helper
    {
        public static string EscapeSpecialCharacters(string fieldValue)
        {
            var str = fieldValue;
            bool isPhrase = fieldValue.StartsWith("\"") && fieldValue.EndsWith("\"");
            if (isPhrase)
                str = fieldValue.Substring(1, fieldValue.Length-2);

            // special characters + - && || ! ( ) { } [ ] ^ " ~ * ? : \
            str.Replace("+", "\\+")
                .Replace("-", "\\-")
                .Replace("&&", "\\&&")
                .Replace("||", "\\||")
                .Replace("!", "\\!")
                .Replace("(", "\\(")
                .Replace(")", "\\)")
                .Replace("{", "\\{")
                .Replace("}", "\\}")
                .Replace("[", "\\[")
                .Replace("]", "\\]")
                .Replace("^", "\\^")
                .Replace("\"", "\\\\")
                .Replace("~", "\\~")
                .Replace("*", "\\*")
                .Replace("?", "\\?")
                .Replace(":", "\\:")
                .Replace("\\", "\\\\");

            if (isPhrase)
                return "\"" + str + "\"";
            else
                return str;
        }

        public static string EscapeSpecialCharactersCotaDocumento(string fieldValue)
        {
            var str = fieldValue;
            // é consedirado frase um valor que esteja entre aspas e tenha pelo menos um caracter espaço
            bool isPhrase = fieldValue.StartsWith("\"") && fieldValue.EndsWith("\"") && fieldValue.IndexOf(' ') != -1;
            if (isPhrase)
            {
                str = fieldValue.Substring(1, fieldValue.Length - 2);

                // special characters + - && || ! ( ) { } [ ] ^ " ~ * ? : \
                str = str.Replace("\\", "\\\\")
                    .Replace("+", "\\+")
                    .Replace("-", "\\-")
                    .Replace("&&", "\\&&")
                    .Replace("||", "\\||")
                    .Replace("!", "\\!")
                    .Replace("(", "\\(")
                    .Replace(")", "\\)")
                    .Replace("{", "\\{")
                    .Replace("}", "\\}")
                    .Replace("[", "\\[")
                    .Replace("]", "\\]")
                    .Replace("^", "\\^")
                    .Replace("\"", "\\\"")
                    .Replace("~", "\\~")
                    .Replace("*", "\\*")
                    .Replace("?", "\\?")
                    .Replace(":", "\\:");

                str = str.Replace(' ', '?');

                return "*" + str + "*";
            }
            else
                return str.Replace("\\", "\\\\")
                    .Replace("+", "\\+")
                    .Replace("-", "\\-")
                    .Replace("&&", "\\&&")
                    .Replace("||", "\\||")
                    .Replace("!", "\\!")
                    .Replace("(", "\\(")
                    .Replace(")", "\\)")
                    .Replace("{", "\\{")
                    .Replace("}", "\\}")
                    .Replace("[", "\\[")
                    .Replace("]", "\\]")
                    .Replace("^", "\\^")
                    .Replace("\"", "\\\"")
                    .Replace("~", "\\~")
                    //.Replace("*", "\\*")
                    //.Replace("?", "\\?")
                    .Replace(":", "\\:");
        }

        public static bool IsServerActive()
        {
            bool ret = false;
            try
            {
                List<string> response = HttpClient.HttpGetresults("/?f=ping", "");
                if (response.Contains("pong"))
                {
                    ret = true;
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
            return ret;
        }

        public static bool IsValidTxtID(QueryParser qp, string val)
        {
            char[] validChars = { '+', '-', '&', '|', '!', '(', ')', '{', '}', '[', ']', '^', '"', '~', '*', '?', ':', '\\', ' ' };

            //if (val.Length == 0 || (IsValidTextField(qp, val) && GISA.Utils.MathHelper.IsInteger(val.Trim(validChars))))
            if (val.Length == 0 || (IsValidTextField(qp, val)))
            {
                string[] integers = val.Split(validChars);      // Exemplo: "10?0?0*" -> { "10", "0", "0", "" }
                foreach (string integer in integers)
                    if (!integer.Equals("") && !GISA.Utils.MathHelper.IsInteger(integer))
                        return false;
                return true;
            }
            else
                return false;
        }

        public static bool IsValidTextField(QueryParser qp, string val)
        {
            bool isValid = true;
            try
            {
                if (val.Length > 0)
                    qp.Parse(val);
            }
            catch (ParseException)
            {
                isValid = false;
            }
            return isValid;
        }

        public static string AddFieldToSearch(QueryParser qp, string fieldName, string val, ref StringBuilder errorMessage)
        {
            if (IsValidTextField(qp, val))
                return val;
            else
            {
                errorMessage.AppendLine(string.Format("{0}: {1}", fieldName, val));
                return string.Empty;
            }
        }
    }
}
