using System;
using System.Collections;

namespace GISA.Reports
{
	/// <summary>
	/// Summary description for CatalogoResumido.
	/// </summary>
	public class CatalogoResumido : InventarioResumido
	{
		public CatalogoResumido(string FileName, bool isTopDown, long IDTrustee) : base(FileName, isTopDown, IDTrustee) {}

		public CatalogoResumido(string FileName, ArrayList parameters, long IDTrustee) : base(FileName, parameters, IDTrustee) {}

		public CatalogoResumido(string FileName, ArrayList parameters, bool isTopDown, long IDTrustee) : base(FileName, parameters, isTopDown, IDTrustee) {}

		protected override bool IsCatalogo(){
			return true;
		}

		protected override string GetTitle(){
			return "Catálogo";
		}
	}
}
