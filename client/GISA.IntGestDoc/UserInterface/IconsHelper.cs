using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

using GISA.IntGestDoc.Model;
using GISA.SharedResources;

namespace GISA.IntGestDoc.UserInterface
{
    public class IconsHelper
    {
        private IconsHelper() { }

        public const int SingleIconWidth = 16;
        public const int ComposedIconWidth = 35;

        private static IconsHelper instance;
        public static IconsHelper Instance
        {
            get
            {
                if (instance == null)
                    instance = new IconsHelper();
                return instance;
            }
        }

        private struct EntidadeIconIndexFormat
        {
            public TipoEntidadeInterna tipoEntidadeInterna;
            public TipoEstado iconEstadoEntidade;
            public TipoOpcao iconOpcao;
        }

        private struct PropRelIconIndexFormat
        {
            public Bitmap propRelIcon;
            public TipoEstado iconEstado;
            public TipoOpcao iconOpcao;
        }

        private struct ComposedIconIndexFormat
        {
            public EntidadeIconIndexFormat entidadeIconIndexFormat;
            public PropRelIconIndexFormat propRelIconIndexFormat;
        }

        private Dictionary<EntidadeIconIndexFormat, Bitmap> EntidadeIconsDictionary = new Dictionary<EntidadeIconIndexFormat, Bitmap>();
        private Dictionary<PropRelIconIndexFormat, Bitmap> PropRelIconsDictionary = new Dictionary<PropRelIconIndexFormat, Bitmap>();
        private Dictionary<ComposedIconIndexFormat, Bitmap> ComposedIconsDictionary = new Dictionary<ComposedIconIndexFormat, Bitmap>();
        
        // tipicamente utilizado quando só se pretende o icon da entidade
        public Bitmap GetIcon(TipoEntidadeInterna indexEntidade, TipoEstado indexEstadoEntidade, TipoOpcao indexOpcao)
        {
            var idxIconEntidade = new EntidadeIconIndexFormat() { tipoEntidadeInterna = indexEntidade, iconEstadoEntidade = indexEstadoEntidade, iconOpcao = indexOpcao };

            return GetBitmapEntidade(idxIconEntidade);
        }

        // tipicamente utilizado quando só se pretende o icon da propriedade
        public Bitmap GetIcon(TipoEstado indexEstadoRelacao, TipoOpcao indexOpcao)
        {
            var idxPropRelEntidade = new PropRelIconIndexFormat() { iconEstado = indexEstadoRelacao, iconOpcao = indexOpcao, propRelIcon = SharedResourcesOld.CurrentSharedResources.Property };

            return GetBitmapPropRel(idxPropRelEntidade);
        }

        // utilizado quando se pretende o icon da entidade mais o da relação
        public Bitmap GetIcon(TipoEntidadeInterna indexEntidade, TipoEstado indexEstadoEntidade, TipoOpcao indexOpcao, TipoEstado indexEstadoRelacao)
        {
            var idxIconEntidade = new EntidadeIconIndexFormat() { tipoEntidadeInterna = indexEntidade, iconEstadoEntidade = indexEstadoEntidade, iconOpcao = indexOpcao };
            var idxPropRelEntidade = new PropRelIconIndexFormat() { iconEstado = indexEstadoRelacao, iconOpcao = indexOpcao, propRelIcon = SharedResourcesOld.CurrentSharedResources.Relation };
            var idxIconComposto = new ComposedIconIndexFormat() { entidadeIconIndexFormat = idxIconEntidade, propRelIconIndexFormat = idxPropRelEntidade };

            return GetComposedIcon(idxIconComposto);
        }

        private Bitmap GetComposedIcon(ComposedIconIndexFormat idxIconComposto)
        {
            if (ComposedIconsDictionary.ContainsKey(idxIconComposto)) return ComposedIconsDictionary[idxIconComposto];

            Bitmap bmpEntidade = GetBitmapEntidade(idxIconComposto.entidadeIconIndexFormat);
            Bitmap bmpRelProp = GetBitmapPropRel(idxIconComposto.propRelIconIndexFormat);

            var bmp = ComposeIcon(bmpEntidade, bmpRelProp);
            ComposedIconsDictionary[idxIconComposto] = bmp;

            return bmp;
        }

        private Bitmap GetBitmapPropRel(PropRelIconIndexFormat indexPropRel)
        {
            if (PropRelIconsDictionary.ContainsKey(indexPropRel)) return PropRelIconsDictionary[indexPropRel];

            Bitmap relProp = indexPropRel.propRelIcon;
            if (indexPropRel.iconEstado != TipoEstado.SemAlteracoes)
            {
                Bitmap overlay = SharedResourcesOld.CurrentSharedResources.StateIcons[(int)indexPropRel.iconEstado][(int)indexPropRel.iconOpcao];
                relProp = SharedResources.SharedResourcesOld.MakeOverlay(relProp, overlay);
            }

            PropRelIconsDictionary[indexPropRel] = relProp;

            return relProp;
        }

        private Bitmap GetBitmapEntidade(EntidadeIconIndexFormat idxEntidade)
        {
            if (EntidadeIconsDictionary.ContainsKey(idxEntidade)) return EntidadeIconsDictionary[idxEntidade];

            Bitmap entidade = SharedResourcesOld.CurrentSharedResources.EntidadesImageList[(int)idxEntidade.tipoEntidadeInterna];
            if (idxEntidade.iconEstadoEntidade != TipoEstado.SemAlteracoes)
            {
                Bitmap overlay = SharedResourcesOld.CurrentSharedResources.StateIcons[(int)idxEntidade.iconEstadoEntidade][(int)idxEntidade.iconOpcao];
                entidade = SharedResources.SharedResourcesOld.MakeOverlay(entidade, overlay);
            }

            EntidadeIconsDictionary[idxEntidade] = entidade;

            return entidade;
        }

        private Bitmap ComposeIcon(Bitmap bmpEntidade, Bitmap bmpRelacao)
        {
            Debug.Assert(bmpEntidade != null);
            Debug.Assert(bmpRelacao != null);
            Debug.Assert(bmpEntidade.Width == bmpRelacao.Width & bmpEntidade.Height == bmpRelacao.Height);

            Bitmap Result = new Bitmap(bmpEntidade.Width + 3 + bmpRelacao.Width, bmpEntidade.Height);
            Graphics g = Graphics.FromImage(Result);

            bmpEntidade.MakeTransparent(Color.Fuchsia);
            g.DrawImage(bmpEntidade, 0, 0);

            bmpRelacao.MakeTransparent(Color.Fuchsia);
            g.DrawImage(bmpRelacao, bmpEntidade.Width + 3, 0);

            g.Dispose();

            return Result;
        }
    }
}
