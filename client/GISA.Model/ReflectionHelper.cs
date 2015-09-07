using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GISA.Model
{
	public class ReflectionHelper
	{

		private static Attribute GetAssemblyAttribute(Type target)
		{
			return (Attribute)(System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(target, false)[0]);
		}

		public static string ProductName
		{
			get
			{
                System.Reflection.AssemblyProductAttribute pa = null;
                pa = (System.Reflection.AssemblyProductAttribute)(GetAssemblyAttribute(typeof(System.Reflection.AssemblyProductAttribute)));
                return pa.Product;
			}
		}

		public static string CompanyName
		{
			get
			{
				System.Reflection.AssemblyCompanyAttribute cn = null;
				cn = (System.Reflection.AssemblyCompanyAttribute)(GetAssemblyAttribute(typeof(System.Reflection.AssemblyCompanyAttribute)));
				return cn.Company;
			}
		}

	}

} //end of root namespace