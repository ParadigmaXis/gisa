using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Linq;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

#if DEBUG
using NUnit.Framework;
#endif
using System.Drawing;
using System.Windows.Forms;

namespace GISA.Model
{
	public class TipoNivel
	{
		public static long LOGICO { get {return 1;} }
		public static long ESTRUTURAL { get {return 2;} }
		public static long DOCUMENTAL { get {return 3;} }
		public static long OUTRO { get {return 4;} }

		public static bool isNivelLogico(GISADataset.NivelRow nivelRow)
		{
			if (nivelRow == null)
				return false;

			return nivelRow.TipoNivelRow.BuiltInName.Equals("LOGICO");
		}

		public static bool isNivelEstrutural(GISADataset.NivelRow nivelRow)
		{
			if (nivelRow == null)
				return false;

			return nivelRow.TipoNivelRow.IsStructure;
		}

		public static bool isNivelDocumental(GISADataset.NivelRow nivelRow)
		{
			if (nivelRow == null)
				return false;

			return nivelRow.TipoNivelRow.IsDocument;
		}

		public static bool isNivelOrganico(GISADataset.NivelRow nivelRow)
		{
			if (nivelRow == null)
				return false;

			return "CA".Equals(nivelRow.CatCode.Trim()) && nivelRow.IDTipoNivel == TipoNivel.ESTRUTURAL;
		}

		public static bool isNivelTematicoFuncional(GISADataset.NivelRow nivelRow)
		{
			if (nivelRow == null)
				return false;

			return "NVL".Equals(nivelRow.CatCode.Trim()) && nivelRow.IDTipoNivel == TipoNivel.ESTRUTURAL;
		}
	}

	public class TipoNivelRelacionado
	{
		public static GISADataset.RelacaoHierarquicaRow GetPrimeiraRelacaoEncontrada(GISADataset.NivelRow nivelRow)
		{
			GISADataset.RelacaoHierarquicaRow[] rhRows = null;
			GISADataset.RelacaoHierarquicaRow rhRow = null;
			try
			{
				rhRows = nivelRow.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica();
				if (rhRows.Length > 0)
					return rhRows[0];

				rhRows = Nivel.GetSelf(GisaDataSetHelper.GetInstance(), nivelRow);
				if (rhRows.Length == 0)
					return null;

				rhRow = rhRows[0];
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				throw;
			}
			return rhRow;
		}

		public static GISADataset.TipoNivelRelacionadoRow GetTipoNivelRelacionadoDaPrimeiraRelacaoEncontrada(GISADataset.NivelRow nivelRow)
		{
			GISADataset.RelacaoHierarquicaRow rhRow = null;
			rhRow = GetPrimeiraRelacaoEncontrada(nivelRow);
			return GetTipoNivelRelacionadoFromRelacaoHierarquica(rhRow);
		}

		// devolve o TipoRelacionado da RelacaoHierarquica em causa (rhRow) tendo 
		// em atenção as EDs, para as quais rhRow será null
		public static GISADataset.TipoNivelRelacionadoRow GetTipoNivelRelacionadoFromRelacaoHierarquica(GISADataset.RelacaoHierarquicaRow rhRow)
		{
			if (rhRow != null)
				return rhRow.TipoNivelRelacionadoRow;
			else
				return GetTipoNivelRelacionadoED();
		}

		public static GISADataset.TipoNivelRelacionadoRow GetTipoNivelRelacionadoFromRelacaoHierarquica(GISADataset.NivelRow nRow, GISADataset.NivelRow nUpperRow) {
			GISADataset.RelacaoHierarquicaRow rhRow;
			if (nUpperRow == null && nRow.TipoNivelRow.ID != TipoNivel.LOGICO)
				return null;
			else if (nUpperRow == null)
				rhRow = null;
			else
				rhRow = (GISADataset.RelacaoHierarquicaRow) GisaDataSetHelper.GetInstance().RelacaoHierarquica.Select(string.Format("ID={0} AND IDUpper={1}", nRow.ID, nUpperRow.ID))[0];
			
			return GetTipoNivelRelacionadoFromRelacaoHierarquica(rhRow);
		}

		private static GISADataset.TipoNivelRelacionadoRow GetTipoNivelRelacionadoED()
		{
			return (GISADataset.TipoNivelRelacionadoRow)(TipoNivelRule.Current.SelectTipoNivelRelacionado(GisaDataSetHelper.GetInstance(), TipoNivelRelacionado.ED)[0]);
		}

		public static ImageList GetImageList()
		{
			ImageList imglst = null;
			imglst =  SharedResourcesOld.CurrentSharedResources.NiveisImageList;

			return imglst;
		}

		public static long ED   { get {return 1;} }
		public static long GA   { get {return 2;} }
		public static long A    { get {return 3;} }
		public static long SA   { get {return 4;} }
		public static long SC   { get {return 5;} }
		public static long SSC  { get {return 6;} }
		public static long SR   { get {return 7;} }
		public static long SSR  { get {return 8;} }
		public static long D    { get {return 9;} }
		public static long SD   { get {return 10;} }
		public static long UF   { get {return 11; } }

		public static bool IsNivelEntidadeDetentora(GISADataset.NivelRow nRow)
		{
			if (Nivel.GetSelf(GisaDataSetHelper.GetInstance(), nRow).Length == 0)
				return true;

			return false;
		}

		public static GISADataset.TipoNivelRelacionadoRow[] GetTipoNivelRelacionadoInicial(GISADataset CurrentDataSet)
		{
			DataRow[] dr = TipoNivelRule.Current.SelectTipoNivel(CurrentDataSet);
			GISADataset.TipoNivelRelacionadoRow[] tnrRows = null;
			// For some reason, Select(String) creates DataRow instead of using existing TipoNivelRow
			tnrRows = (GISADataset.TipoNivelRelacionadoRow[])(System.Array.CreateInstance(typeof(GISADataset.TipoNivelRelacionadoRow), dr.Length));
			System.Array.Copy(dr, tnrRows, dr.Length);
			return tnrRows;
		}

		// Devolve os TipoNivelRalacionados possíveis de ralacionar com o TipoNivelRalacionado especificado
		public static GISADataset.TipoNivelRelacionadoRow[] GetSubTipoNivelRelacionado(GISADataset CurrentDataSet, GISADataset.TipoNivelRelacionadoRow CurrentRow)
		{

			GISADataset.RelacaoTipoNivelRelacionadoRow[] rtnrRow = CurrentRow.GetRelacaoTipoNivelRelacionadoRowsByTipoNivelRelacionadoRelacaoTipoNivelRelacionadoUpper();
			GISADataset.TipoNivelRelacionadoRow[] Result = null;

			Result = new GISADataset.TipoNivelRelacionadoRow[rtnrRow.Length + 1];
			int i = 0;
			Result[i] = CurrentRow;
			i = i + 1;

			foreach (GISADataset.RelacaoTipoNivelRelacionadoRow r in rtnrRow)
			{
				Result[i] = r.TipoNivelRelacionadoRowByTipoNivelRelacionadoRelacaoTipoNivelRelacionado;
				i = i + 1;
			}
			return Result;
		}

        public static void ConfigureMenu(GISADataset CurrentDataSet, GISADataset.TipoNivelRelacionadoRow CurrentRow, ref ToolBarButton Button, EventHandler CurrentHandler)
        {
            ImageList ImgList = GetImageList();
            Menu.MenuItemCollection CurrentMenu = Button.DropDownMenu.MenuItems;
            GISADataset.TipoNivelRelacionadoRow subtnrRow;

            if (CurrentRow.ID == TipoNivelRelacionado.D)
                subtnrRow = CurrentDataSet.TipoNivelRelacionado.Cast<GISADataset.TipoNivelRelacionadoRow>().Single(r => r.ID == TipoNivelRelacionado.SD);
            else
                subtnrRow = CurrentDataSet.TipoNivelRelacionado.Cast<GISADataset.TipoNivelRelacionadoRow>().Single(r => r.ID == TipoNivelRelacionado.D);

            AddMenuOption(CurrentMenu, CurrentRow, subtnrRow, CurrentHandler, ImgList);
        }

		public static void ConfigureMenu(GISADataset CurrentDataSet, GISADataset.TipoNivelRelacionadoRow CurrentRow, ref ToolBarButton Button, EventHandler CurrentHandler, bool DocumentView)
		{
			ImageList ImgList = GetImageList();
			Menu.MenuItemCollection CurrentMenu = Button.DropDownMenu.MenuItems;

			foreach (GISADataset.TipoNivelRelacionadoRow subtnrRow in GetSubTipoNivelRelacionado(CurrentDataSet, CurrentRow))
			{
				// An option to create a certain type of nivel is shown if:
				// we are in the Strutcture view and we are providing options to create new structure items 
				// *OR* 
				// we are in Documents view and we are providing options to create new document items 
				if ((! DocumentView && ! (subtnrRow.TipoNivelRow.IsDocument ^ CurrentRow.TipoNivelRow.IsDocument)) || (DocumentView & CurrentRow.TipoNivelRow.IsDocument))
				{
					// Adicionar opção de criação apenas para os tipos de nivelRelacionado que não sejam níveis orgânicos
					if (! (GisaDataSetHelper.GetInstance().GlobalConfig[0].NiveisOrganicos && subtnrRow.IDTipoNivel == TipoNivel.ESTRUTURAL && CurrentRow.IDTipoNivel == TipoNivel.ESTRUTURAL))
						AddMenuOption(CurrentMenu, CurrentRow, subtnrRow, CurrentHandler, ImgList);
				}
				else if (DocumentView)
				{
					//Dim pxmi As PXMenuItem = New TipoNivelMenuItem("Criar" + subtnRow.Designacao, subtnRow, ImgList)
					//Dim mi As MenuItem = DirectCast(pxmi, MenuItem)
					if (subtnrRow.TipoNivelRow.IsDocument)
					{
						//If Not CurrentHandler Is Nothing Then AddHandler mi.Click, CurrentHandler
						AddMenuOption(CurrentMenu, CurrentRow, subtnrRow, CurrentHandler, ImgList);
					}
				}
			}
		}

		public static ArrayList GetPossibleSubItems(GISADataset.NivelRow nRow) //PossibleSubNivel()
		{
			//ToDo()
			// Obter os TipoNivelRelacionados das RelacaoHierarquicas das EPs superiores. Para cada TiponivelRelacionado devolver também o intervalo da relação associada.
			// espandir os TipoNivelRelacionados permitidos como subníveis para cada um dos TipoNivelRelacionados encontrados anteriormente. Para cada um dos tipos de subnivel guardar o intervalo de data em que ele faz sentido

			ArrayList subNiveis = new ArrayList();
			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try
			{
				Trace.WriteLine("<getPossibleSubTypesOf>");

				IDataReader dataReader = null;
				dataReader = TipoNivelRule.Current.GetPossibleSubItems(nRow.ID, ho.Connection);

				Trace.WriteLine("<getPossibleSubTypesOf/>");

				while (dataReader.Read())
				{
					PossibleSubNivel subNivel = new PossibleSubNivel();
					subNivel.SubIDTipoNivelRelacionado = System.Convert.ToInt64(dataReader.GetValue(0));
					subNivel.Designacao = GisaDataSetHelper.GetDBNullableText(ref dataReader, 1);
					subNivel.InicioAno = GisaDataSetHelper.GetDBNullableText(ref dataReader, 2);
					subNivel.InicioMes = GisaDataSetHelper.GetDBNullableText(ref dataReader, 3);
					subNivel.InicioDia = GisaDataSetHelper.GetDBNullableText(ref dataReader, 4);
					subNivel.FimAno = GisaDataSetHelper.GetDBNullableText(ref dataReader, 5);
					subNivel.FimMes = GisaDataSetHelper.GetDBNullableText(ref dataReader, 6);
					subNivel.FimDia = GisaDataSetHelper.GetDBNullableText(ref dataReader, 7);
					subNiveis.Add(subNivel);
				}
				dataReader.Close();
			}
			catch (Exception ex)
			{				
				Trace.WriteLine(ex);
				throw ex;				
			}
			finally
			{
				ho.Dispose();
			}

			// não são encontrados subniveis organicos possiveis se não existirem 
			if (subNiveis.Count == 0)
			{
			}

			return subNiveis;
		}

		public class PossibleSubNivel
		{
			public long SubIDTipoNivelRelacionado = 0;
			public string Designacao = null;
			public string InicioAno = null;
			public string InicioMes = null;
			public string InicioDia = null;
			public string FimAno = null;
			public string FimMes = null;
			public string FimDia = null;
			public bool Invalido = false;
			public string DesignacaoComposta
			{
				get
				{
					string inicio = FormatDate(InicioAno, InicioMes, InicioDia);
					string fim = FormatDate(FimAno, FimMes, FimDia);
					if (inicio.Length > 0 || fim.Length > 0)
						return Designacao + "  (" + inicio + " - " + fim + ")";
					else
						return Designacao;
				}
			}

			private string FormatDate(string ano, string mes, string dia)
			{
				if (ano == null && mes == null && dia == null)
					return string.Empty;

				if (ano == null || ano.Length == 0)
					ano = "    ";

				if (mes == null || mes.Length == 0)
					mes = "  ";

				if (dia == null || dia.Length == 0)
					dia = "  ";

				return string.Format("{0}/{1}/{2}", ano, mes, dia);
			}

			public PossibleSubNivel()
			{
			}

			public PossibleSubNivel(long SubIDTipoNivelRelacionado, string Designacao, string InicioAno, string InicioMes, string InicioDia, string FimAno, string FimMes, string FimDia): this(SubIDTipoNivelRelacionado, Designacao, InicioAno, InicioMes, InicioDia, FimAno, FimMes, FimDia, false)
			{
			}

			public PossibleSubNivel(long SubIDTipoNivelRelacionado, string Designacao, string InicioAno, string InicioMes, string InicioDia, string FimAno, string FimMes, string FimDia, bool Invalido)
			{
				this.SubIDTipoNivelRelacionado = SubIDTipoNivelRelacionado;
				this.Designacao = Designacao;
				this.InicioAno = InicioAno;
				this.InicioMes = InicioMes;
				this.InicioDia = InicioDia;
				this.FimAno = FimAno;
				this.FimMes = FimMes;
				this.FimDia = FimDia;
				this.Invalido = Invalido;
			}
		}

		private static void AddMenuOption(Menu.MenuItemCollection CurrentMenu, GISADataset.TipoNivelRelacionadoRow existingRow, GISADataset.TipoNivelRelacionadoRow newRow, EventHandler CurrentHandler, ImageList ImgList)
		{
			// If this nivel allows infinite recursivity *OR* 
			// allows at least one more recursivity level for this type of nivel
			if (! existingRow.IsRecursivoNull() && existingRow.Recursivo == 0 && existingRow.ID == newRow.ID)
				return;

			PXMenuItem pxmi = new TipoNivelMenuItem("Criar " + newRow.Designacao, newRow, ImgList);
			MenuItem mi = (MenuItem)pxmi;
			if (CurrentHandler != null)
				mi.Click += CurrentHandler;

			CurrentMenu.Add(mi);
		}
	}

	public class TipoNivelMenuItem : PXMenuItem
	{
		private GISADataset.TipoNivelRelacionadoRow mRow;
		public GISADataset.TipoNivelRelacionadoRow Row
		{
			get
			{
				return mRow;
			}
		}
		public TipoNivelMenuItem(string Text, GISADataset.TipoNivelRelacionadoRow CurrentRow, ImageList ImgList) : base(Text)
		{
			base.Icon = getIconCriarTipoNivel(CurrentRow, ImgList);
			mRow = CurrentRow;
		}
		private Bitmap getIconCriarTipoNivel(GISADataset.TipoNivelRelacionadoRow row, ImageList ImgList)
		{
			return (Bitmap)(ImgList.Images[SharedResources.SharedResourcesOld.CurrentSharedResources.NivelImageCriar(System.Convert.ToInt32(row.ID))]);
		}
	}

	#if DEBUG
	[TestFixture()]
	public class TestTipoNivel
	{
		private GISADataset ds;
		[SetUp()]
		public void SetUp()
		{
			ds = GisaDataSetHelper.GetInstance();
		}
		[TearDown()]
		public void TearDown()
		{
			ds = null;
		}
		[Test()]
		public void GetTipoNivelInicial()
		{
			GISADataset.TipoNivelRelacionadoRow[] tni = TipoNivelRelacionado.GetTipoNivelRelacionadoInicial(ds);
			Assert.AreEqual(1, tni.Length);
		}
		[Test()]
		public void GetSubTipoNivel()
		{
			GISADataset.TipoNivelRelacionadoRow[] stn = TipoNivelRelacionado.GetSubTipoNivelRelacionado(ds, TipoNivelRelacionado.GetTipoNivelRelacionadoInicial(ds)[0]);
			Assert.IsTrue(stn.Length > 0);
			Assert.AreEqual(2, stn[0].ID);
		}
		[Test()]
		public void GetImageList()
		{
			ImageList Imgs = TipoNivelRelacionado.GetImageList();
			Assert.IsNotNull(Imgs);
			Assert.AreEqual(11, Imgs.Images.Count);
		}
		private class ConfigureMenuCallback
		{
			public bool Clicked;
			public ConfigureMenuCallback()
			{
				Clicked = false;
			}
			public void MenuItemClick(object Sender, EventArgs e)
			{
				Clicked = true;
			}
		}
		[Test()]
		public void ConfigureMenu()
		{
			ToolBarButton button = new ToolBarButton();
			ContextMenu mnu = new ContextMenu();
			button.DropDownMenu = mnu;
			ConfigureMenuCallback cmc = new ConfigureMenuCallback();
			Assert.IsTrue(TipoNivelRelacionado.GetTipoNivelRelacionadoInicial(ds).Length > 0);
			TipoNivelRelacionado.ConfigureMenu(ds, TipoNivelRelacionado.GetTipoNivelRelacionadoInicial(ds)[0], ref button, new System.EventHandler(cmc.MenuItemClick), false);
			Assert.IsTrue(mnu.MenuItems.Count > 0);
			mnu.MenuItems[0].PerformClick();
			Assert.IsTrue(cmc.Clicked);
		}
	}
	#endif

}