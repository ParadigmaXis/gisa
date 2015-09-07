using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using DBAbstractDataLayer.DataAccessRules;

using GISA.Model;

namespace GISA.EADGen {
    class EADGenerator_Cache {
        private List<EADGeneratorRule.NiveisDescendentes> tree_descendentes;

        public EADGenerator_Cache(long IDNivelPai, IDbConnection conn) {
            this.tree_descendentes = EADGeneratorRule.Current.get_All_NiveisDescendentes(IDNivelPai, SessionHelper.GetGisaPrincipal().TrusteeUserOperator.ID, conn);
        }

        public int get_NiveisCount() {
            return this.tree_descendentes.Count();
        }

        public List<EADGeneratorRule.NiveisDescendentes> get_NiveisDescendentes(long IDNivel) {
            EADGeneratorRule.NiveisDescendentes mock = new EADGeneratorRule.NiveisDescendentes();
            mock.IDNivelPai = IDNivel;
            mock.IDNivel = -1;

            List<EADGeneratorRule.NiveisDescendentes> ret = new List<EADGeneratorRule.NiveisDescendentes>();
            // O BinarySearch nunca vai encontrar o mock; deve encontrar o indice de um objecto imediatamente 'superior':
            int idx = this.tree_descendentes.BinarySearch(mock, new NiveisDescendentes_IDNivelPai_Comp());
            if (idx >= 0) {
                // Isto nunca vai ser executado, excepto se existirem IDNivel negativos !
                for (int i = idx; i < this.tree_descendentes.Count && this.tree_descendentes[i].IDNivelPai == IDNivel; i++) {
                    ret.Add(this.tree_descendentes[i]);
                }
            }
            if (~idx < this.tree_descendentes.Count) {
                // Encontrou NiveisDescendentes imediatamente 'superiores':
                for (int i = ~idx; i < this.tree_descendentes.Count && this.tree_descendentes[i].IDNivelPai == IDNivel; i++) {
                    ret.Add(this.tree_descendentes[i]);
                }
            }

            return ret;            
          }

        private class NiveisDescendentes_IDNivelPai_Comp : IComparer<EADGeneratorRule.NiveisDescendentes> {
            public int Compare(EADGeneratorRule.NiveisDescendentes n1, EADGeneratorRule.NiveisDescendentes n2) {

                if (n1.IDNivelPai > n2.IDNivelPai)
                    return 1;
                if (n1.IDNivelPai < n2.IDNivelPai)
                    return -1;
                else {
                    if (n1.IDNivel > n2.IDNivel)
                        return 1;
                    if (n1.IDNivel < n2.IDNivel)
                        return -1;
                    return 0;
                }
            }
        }

   }

}
