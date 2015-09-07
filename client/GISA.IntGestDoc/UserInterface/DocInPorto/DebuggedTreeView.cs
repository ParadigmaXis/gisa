using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GISA.IntGestDoc.UserInterface.DocInPorto
{
    /*
     * Serve para impedir um evento de duplo click na check box de um nó. Quando acontece, a checkbox muda de estado mas o estado checked do nó mantém-se inalterado
    */
    public class DebuggedTreeView : TreeView
    {
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x203) // identified double click
                m.Result = IntPtr.Zero;
            else
                base.WndProc(ref m);
        }
    }
}
