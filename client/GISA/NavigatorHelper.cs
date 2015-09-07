using System;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GISA.Controls.Nivel;
using GISA.Model;
using GISA.Controls.Localizacao;

namespace GISA
{
    public interface INivelNavigatorProvider
    {
        NivelNavigator NivelNavigator { get; }
    }

    public interface IControloNivelListProvider
    {
        ControloNivelList ControloNivelList { get; }
    }

    public class NavigatorHelper
    {
        public static void ForceRefresh(GISADataset.RelacaoHierarquicaRowChangeEvent e, MasterPanel mp, frmMain topLevelControl)
        {
            try
            {
                if (!(e.Action == DataRowAction.Add || (e.Action == DataRowAction.Change) || e.Action == DataRowAction.Delete))
                    return;

                // verificar se a row foi realmente editada
                if (e.Action == DataRowAction.Change && !Concorrencia.isModifiedRow(e.Row))
                    return;

                // descartar modificações intermédias de rows (por exemplo, a atribuição de um ID uma nivelRow quando gravada para a BD)
                if (e.Row.NivelRowByNivelRelacaoHierarquica == null || e.Row.NivelRowByNivelRelacaoHierarquicaUpper == null)
                    return;

                // garantir que a alteração occoreu entre duas entidades produtoras
                if (!(e.Row.NivelRowByNivelRelacaoHierarquica.IDTipoNivel == TipoNivel.ESTRUTURAL && (e.Row.NivelRowByNivelRelacaoHierarquicaUpper.IDTipoNivel == TipoNivel.LOGICO || e.Row.NivelRowByNivelRelacaoHierarquicaUpper.IDTipoNivel == TipoNivel.ESTRUTURAL)))
                    return;

                if (!topLevelControl.IsFirstMasterPanelInStack(mp) && topLevelControl.MasterPanelCount == 1)
                {
                    if (mp is INivelNavigatorProvider)
                    {
                        mp.lblFuncao.Text = "Estrutura orgânica";
                        var nav = mp as INivelNavigatorProvider;
                        nav.NivelNavigator.PanelToggleState = NivelNavigator.ToggleState.Estrutural;
                        nav.NivelNavigator.CollapseAllNodes();
                        nav.NivelNavigator.ToggleView(false);
                    }
                    else if (mp is IControloNivelListProvider)
                    {
                        var cnl = mp as IControloNivelListProvider;
                        cnl.ControloNivelList.CollapseAllNodes();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.ToString());
                Trace.WriteLine(ex);
                throw ex;
            }
        }
    }
}
