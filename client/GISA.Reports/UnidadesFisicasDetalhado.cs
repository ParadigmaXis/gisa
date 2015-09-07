using System;
using System.Collections;
using System.Collections.Generic;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA.Reports
{
	/// <summary>
	/// Summary description for UnidadesFisicasDetalhado.
	/// </summary>
	public class UnidadesFisicasDetalhado : UnidadesFisicas
	{
		public UnidadesFisicasDetalhado(string FileName, long IDTrustee) : base(FileName, IDTrustee) {}

        public UnidadesFisicasDetalhado(string FileName, ArrayList parameters, List<ReportParameter> fields, long IDTrustee) : base(FileName, parameters, fields, IDTrustee) { }

		protected override bool IsDetalhado() {
			return true;
		}
	}
}
