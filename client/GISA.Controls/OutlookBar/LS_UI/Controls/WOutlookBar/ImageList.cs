using System;
using System.Collections;

namespace LumiSoft.UI.Controls.WOutlookBar
{
	/// <summary>
	/// Summary description for ImageList.
	/// </summary>
	public class ImageList
	{
		public ImageList()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private ArrayList imageCollection;
		public ArrayList Images
		{
			get
			{
				if (this.imageCollection == null)
				{
					this.imageCollection = new ArrayList();
				}
				return this.imageCollection;
			}
		}
	}
}
