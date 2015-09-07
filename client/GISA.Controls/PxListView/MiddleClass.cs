using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GISA.Controls
{
    /*
     * Esta classe serve somente para permitir utilizar o designer para editar os controlos gráficos. 
     * http://stackoverflow.com/questions/1620847/how-can-i-get-visual-studio-2008-windows-forms-designer-to-render-a-form-that-im/2406058#2406058
     */
    public partial class MiddleClass : PaginatedListView
    {
        public MiddleClass()
        {
            InitializeComponent();
        }
    }
}
