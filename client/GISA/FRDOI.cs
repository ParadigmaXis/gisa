using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using GISA.SharedResources;

namespace GISA
{
	public class FRDOrganizacaoInformacao : GISA.MultiPanelControl
	{

		// default value. shadowed in derived classes
		public static Bitmap FunctionImage
		{
			get
			{
				return SharedResourceSets.CurrentSharedResourceSets.getImageResource(typeof(SharedDomainIcons), "Documento_enabled_32x32.png");
			}
		}

		protected GISADataset.FRDBaseRow CurrentFRDBase;

		protected override bool isInnerContextValid()
		{
			return CurrentFRDBase != null && ! (CurrentFRDBase.RowState == DataRowState.Detached) && CurrentFRDBase.isDeleted == 0;
		}

		protected override bool isOuterContextValid()
		{
			return CurrentContext.NivelEstrututalDocumental != null;
		}

		protected override bool isOuterContextDeleted()
		{
			Debug.Assert(CurrentContext.NivelEstrututalDocumental != null, "CurrentContext.NivelEstrututalDocumental Is Nothing");
			return CurrentContext.NivelEstrututalDocumental.RowState == DataRowState.Detached;
		}

		protected override void addContextChangeHandlers()
		{
			CurrentContext.NivelEstrututalDocumentalChanged += this.Recontextualize;
		}

		protected override void removeContextChangeHandlers()
		{
			CurrentContext.NivelEstrututalDocumentalChanged -= this.Recontextualize;
		}
	}

} //end of root namespace