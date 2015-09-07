using System;
using System.ComponentModel.Design;

namespace LumiSoft.UI.Controls.WOutlookBar
{
	public class BarCollectionEditor : System.ComponentModel.Design.CollectionEditor
	{
		public BarCollectionEditor(Type type) : base(type) 
		{ 
		}

		protected override object CreateInstance(Type itemType) {
			object obj = null;

			if(itemType.FullName == "LumiSoft.UI.Controls.WOutlookBar.Bar"){
				obj = ((WOutlookBar)Context.Instance).Bars.Add("obj");
			}
			return obj;
		}
	}
}
