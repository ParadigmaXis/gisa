using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA.Reports
{
	/// <summary>
	/// Summary description for CatalogoCompleto.
	/// </summary>
	public class CatalogoDetalhado : InventarioDetalhado
	{
		public CatalogoDetalhado(string FileName, bool isTopDown, long IDTrustee) : base(FileName, isTopDown, IDTrustee) {}

		public CatalogoDetalhado(string FileName, ArrayList parameters, long IDTrustee) : base(FileName, parameters, false, IDTrustee) {}

		public CatalogoDetalhado(string FileName, ArrayList parameters, bool isTopDown, long IDTrustee) : base(FileName, parameters, isTopDown, IDTrustee) {}

        public CatalogoDetalhado(string FileName, ArrayList parameters, List<ReportParameter> fields, bool isTopDown, long IDTrustee) : base(FileName, parameters, fields, isTopDown, IDTrustee) { }

       	protected override bool IsCatalogo(){
			return true;
		}

        protected override List<ReportParameter> Fields()
        {
            return mFields;
        }

		protected override string GetTitle(){
			return "Catálogo";
		}
	}
}
