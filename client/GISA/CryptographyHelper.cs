using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using System.IO;

namespace GISA
{
	public class CryptographyHelper
	{
		public static string GetMD5(string str)
		{
			System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
			System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
			System.Text.StringBuilder result = new System.Text.StringBuilder();
			foreach (byte b in md5Hasher.ComputeHash(encoder.GetBytes(string.Format("Gisa{0}", str))))
			{
				// converter cada byte para hexadecimal
				result.Append(b.ToString("x2").ToUpper());
			}
			return result.ToString();
		}

		public static string GetMD5(FileStream stream)
		{
			System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
			System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
			System.Text.StringBuilder result = new System.Text.StringBuilder();
			foreach (byte b in md5Hasher.ComputeHash(stream))
			{
				// converter cada byte para hexadecimal
				result.Append(b.ToString("x2").ToUpper());
			}
			return result.ToString();
		}
	}

} //end of root namespace