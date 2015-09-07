using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
	public class NivelUnidadeFisicaSorter : IComparer
	{

		public int Compare(object x, object y)
		{
			GISADataset.RelacaoHierarquicaRow rh1Row = (GISADataset.RelacaoHierarquicaRow)x;
			GISADataset.RelacaoHierarquicaRow rh2Row = (GISADataset.RelacaoHierarquicaRow)y;

			if (rh1Row.IDTipoNivelRelacionado != TipoNivelRelacionado.UF)
			{
				throw new ArgumentException("x.IDTipoNivel must be TipoNivel.UF");
			}
			if (rh2Row.IDTipoNivelRelacionado != TipoNivelRelacionado.UF)
			{
				throw new ArgumentException("y.IDTipoNivel must be TipoNivel.UF");
			}

			decimal xxx = decimal.Parse(rh1Row.NivelRowByNivelRelacaoHierarquica.Codigo.Substring(rh1Row.NivelRowByNivelRelacaoHierarquica.Codigo.LastIndexOf("-") + 1));
			decimal yyy = decimal.Parse(rh2Row.NivelRowByNivelRelacaoHierarquica.Codigo.Substring(rh2Row.NivelRowByNivelRelacaoHierarquica.Codigo.LastIndexOf("-") + 1));

			string xxxx = rh1Row.NivelRowByNivelRelacaoHierarquica.Codigo.Substring(1, rh1Row.NivelRowByNivelRelacaoHierarquica.Codigo.LastIndexOf("-") - 1);
			string yyyy = rh2Row.NivelRowByNivelRelacaoHierarquica.Codigo.Substring(1, rh2Row.NivelRowByNivelRelacaoHierarquica.Codigo.LastIndexOf("-") - 1);

			if (xxxx.CompareTo(yyyy) < 0)
			{
				return -1;
			}
			if (xxxx.CompareTo(yyyy) > 0)
			{
				return 1;
			}

			if (xxx < yyy)
			{
				return -1;
			}
			if (xxx > yyy)
			{
				return 1;
			}
			return 0;
		}
	}

} //end of root namespace