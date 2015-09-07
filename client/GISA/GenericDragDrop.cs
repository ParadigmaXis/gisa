using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
	public class GenericDragDrop : object
	{
		protected Control Control;
		private Type[] AcceptTypes;

		public GenericDragDrop(Control Control, params Type[] AcceptTypes)
		{
			this.AcceptTypes = AcceptTypes;
			this.Control = Control;

            Control.DragOver += DragOver;
            Control.DragDrop += DragDrop;
        }

		protected virtual void VerifyContents(object Value, ref bool Cancel)
		{
		}

		protected virtual void AcceptContents(object Value)
		{
		}

		private void DragOver(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.None;
			foreach (Type CurrentType in AcceptTypes)
			{
				if (e.Data.GetDataPresent(CurrentType.FullName))
				{
					bool Cancel = false;
					if (CurrentType.IsArray)
					{
						object[] objs = (object[])(e.Data.GetData(CurrentType.FullName));
						foreach (object obj in objs)
						{
							VerifyContents(obj, ref Cancel);
							if (Cancel)
								break;
						}
					}
					else
						VerifyContents(e.Data.GetData(CurrentType.FullName), ref Cancel);

					if (! Cancel)
						e.Effect = DragDropEffects.Link;
				}
			}
		}

		private void DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				foreach (Type CurrentType in AcceptTypes)
				{
					if (e.Data.GetDataPresent(CurrentType.FullName))
					{
						if (CurrentType.IsArray)
						{
							object[] objs = (object[])(e.Data.GetData(CurrentType.FullName));
							foreach (object obj in objs)
								AcceptContents(obj);
						}
						else
							AcceptContents(e.Data.GetData(CurrentType.FullName));

						e.Effect = DragDropEffects.Link;
					}
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				Debug.Assert(false, ex.ToString());
				throw;
			}
		}
	}
} //end of root namespace