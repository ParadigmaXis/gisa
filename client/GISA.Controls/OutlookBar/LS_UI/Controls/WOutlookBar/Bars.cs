using System;
using System.Collections;

namespace LumiSoft.UI.Controls.WOutlookBar
{
	/// <summary>
	/// Summary description for Bars.
	/// </summary>
	public class Bars : ArrayList
	{
		private WOutlookBar m_pOutlookBar = null;

		public Bars(WOutlookBar parent)
		{
			m_pOutlookBar = parent;
		}


		#region function Add

		public Bar Add(string caption)
		{
			Bar bar = new Bar(this);
			bar.Caption = caption;

			base.Add(bar);
			this.WOutlookBar.UpdateAll();

			return bar;
		}

		#endregion

		public new Bar this[int nIndex]
		{
			get{
                if (this.Count == 0)
                    return null;
                else
                    return (Bar)base[nIndex]; 
            }
		}


		#region Properties Implementation

		public WOutlookBar WOutlookBar
		{
			get{ return m_pOutlookBar; }
		}

		#endregion

	}
}
