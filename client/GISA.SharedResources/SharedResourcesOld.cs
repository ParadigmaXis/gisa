using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace GISA.SharedResources
{
	public class SharedResourcesOld
	{
		private System.Resources.ResourceManager ResourceManager;
		public static Color GlobalTransparentColor = Color.Fuchsia;

		#region Singleton pattern 
		private SharedResourcesOld() : base()
		{
			ResourceManager = new System.Resources.ResourceManager(this.GetType());
		}

		private static SharedResourcesOld mCurrentResourceClass;
		public static SharedResourcesOld CurrentSharedResources 
		{
			get 
			{
				if ((mCurrentResourceClass == null)) 
				{
					mCurrentResourceClass = new SharedResourcesOld();
				}
				return mCurrentResourceClass;
			}
		}
		#endregion

		#region Methods used to actually retrieve the resources
		private Bitmap EnsureLoaded(string name, ref Bitmap bitmap)
		{
			if (bitmap == null) 
			{
				bitmap = GetBitmap(name);
				if (bitmap == null) 
				{
					throw new ArgumentException("Resource " + name + " was not found.");
				}
				bitmap.MakeTransparent(GlobalTransparentColor);
			}
			return bitmap;
		}

		private Icon EnsureLoaded(string name, ref Icon icon)
		{
			if (icon == null) 
			{
				icon = GetIcon(name);
				if (icon == null) 
				{
					throw new ArgumentException("Resource " + name + " was not found.");
				}
			}
			return icon;
		}

		private string EnsureLoaded(string name, ref string str)
		{
			if (str == null) 
			{
				str = GetString(name);
				if (str == null) 
				{
					throw new ArgumentException("Resource " + name + " was not found.");
				}
			}
			return str;
		}

		private Bitmap GetBitmap(string name)
		{
			return ((Bitmap)(ResourceManager.GetObject(name, System.Globalization.CultureInfo.CurrentCulture)));
		}

		private Icon GetIcon(string name)
		{
			return ((Icon)(ResourceManager.GetObject(name, System.Globalization.CultureInfo.CurrentCulture)));
		}

		private string GetString(string name)
		{
			return ((string)(ResourceManager.GetObject(name, System.Globalization.CultureInfo.CurrentCulture)));
		}
		#endregion
		
		private Icon mGisaIcon;
		public Icon GisaIcon 
		{
			get 
			{
				if (mGisaIcon == null) 
				{
					mGisaIcon = ((Icon)(ResourceManager.GetObject("GISA.ico", System.Globalization.CultureInfo.CurrentCulture)));
					if (mGisaIcon == null) 
					{
						throw new ArgumentException("Resource GISA.ico was not found.");
					}
				}
				return mGisaIcon;
			}
		}

		private Icon mHelpIcon;
		public Icon HelpIcon 
		{
			get 
			{
				if (mHelpIcon == null) 
				{
					mHelpIcon = ((Icon)(ResourceManager.GetObject("help.ico", System.Globalization.CultureInfo.CurrentCulture)));
					if (mHelpIcon == null) 
					{
						throw new ArgumentException("Resource help.ico was not found.");
					}
				}
				return mHelpIcon;
			}
		}

		#region icons da statusbar
		private Icon mDbAccess;
		public Icon DbAccess 
		{
			get 
			{
				return EnsureLoaded("dbAccess.ico", ref mDbAccess);
			}
		}

		private Icon mDbAccessOut;
		public Icon DbAccessOut 
		{
			get 
			{
				return EnsureLoaded("dbAccess_out.ico", ref mDbAccessOut);
			}
		}

        private Icon mServerActive;
        public Icon ServerActive
        {
            get
            {
                return EnsureLoaded("upload-server.ico", ref mServerActive);
            }
        }

        private Icon mServerInactive;
        public Icon ServerInactive
        {
            get
            {
                return EnsureLoaded("disable-server.ico", ref mServerInactive);
            }
        }
		#endregion

		#region Manipulacao de listas genéricas e listas paginadas
		private Bitmap mAdicionar;
		public Bitmap Adicionar 
		{
			get 
			{
				return EnsureLoaded("adicionar.bmp", ref mAdicionar);
			}
		}

		private string mAdicionarString;
		public string AdicionarString 
		{
			get 
			{
				return EnsureLoaded("adicionar", ref mAdicionarString);
			}
		}

		private Bitmap mEditar;
		public Bitmap Editar 
		{
			get 
			{
				return EnsureLoaded("editar.bmp", ref mEditar);
			}
		}

		private string mEditarString;
		public string EditarString 
		{
			get 
			{
				return EnsureLoaded("editar", ref mEditarString);
			}
		}

		private Bitmap mApagar;
		public Bitmap Apagar 
		{
			get 
			{
				return EnsureLoaded("apagar.bmp", ref mApagar);
			}
		}

		private string mApagarString;
		public string ApagarString 
		{
			get 
			{
				return EnsureLoaded("apagar", ref mApagarString);
			}
		}

        private Bitmap mPrioridadeMax;
        public Bitmap PrioridadeMax
        {
            get
            {
                return EnsureLoaded("PrioridadeMax.bmp", ref mPrioridadeMax);
            }
        }

        private string mPrioridadeMaxString;
        public string PrioridadeMaxString
        {
            get
            {
                return EnsureLoaded("PrioridadeMax", ref mPrioridadeMaxString);
            }
        }

        private Bitmap mPrioridadeMin;
        public Bitmap PrioridadeMin
        {
            get
            {
                return EnsureLoaded("PrioridadeMin.bmp", ref mPrioridadeMin);
            }
        }

        private string mPrioridadeMinString;
        public string PrioridadeMinString
        {
            get
            {
                return EnsureLoaded("PrioridadeMin", ref mPrioridadeMinString);
            }
        }

		private Bitmap mPrioridadeAumentar;
		public Bitmap PrioridadeAumentar 
		{
			get 
			{
				return EnsureLoaded("PrioridadeAumentar.bmp", ref mPrioridadeAumentar);
			}
		}

		private string mPrioridadeAumentarString;
		public string PrioridadeAumentarString 
		{
			get 
			{
				return EnsureLoaded("PrioridadeAumentar", ref mPrioridadeAumentarString);
			}
		}

		private Bitmap mPrioridadeDiminuir;
		public Bitmap PrioridadeDiminuir 
		{
			get 
			{
				return EnsureLoaded("PrioridadeDiminuir.bmp", ref mPrioridadeDiminuir);
			}
		}

		private string mPrioridadeDiminuirString;
		public string PrioridadeDiminuirString 
		{
			get 
			{
				return EnsureLoaded("PrioridadeDiminuir", ref mPrioridadeDiminuirString);
			}
		}

		private Bitmap mPaginaAnterior;
		public Bitmap PaginaAnterior 
		{
			get 
			{
				return EnsureLoaded("PaginaAnterior.bmp", ref mPaginaAnterior);
			}
		}

		private string mPaginaAnteriorString;
		public string PaginaAnteriorString 
		{
			get 
			{
				return EnsureLoaded("PaginaAnterior", ref mPaginaAnteriorString);
			}
		}

		private Bitmap mPaginaSeguinte;
		public Bitmap PaginaSeguinte 
		{
			get 
			{
				return EnsureLoaded("PaginaSeguinte.bmp", ref mPaginaSeguinte);
			}
		}

		private string mPaginaSeguinteString;
		public string PaginaSeguinteString 
		{
			get 
			{
				return EnsureLoaded("PaginaSeguinte", ref mPaginaSeguinteString);
			}
		}

		private Bitmap mFiltro;
		public Bitmap Filtro 
		{
			get 
			{
				return EnsureLoaded("FiltrarDados.bmp", ref mFiltro);
			}
		}

		private string mFiltroString;
		public string FiltroString 
		{
			get 
			{
				return EnsureLoaded("FiltrarDados", ref mFiltroString);
			}
		}
		#endregion		
		
		private Bitmap mChamarPicker;
		public Bitmap ChamarPicker 
		{
			get 
			{
				return EnsureLoaded("ChamarPicker.bmp", ref mChamarPicker);
			}
		}

		private string mChamarPickerString;
		public string ChamarPickerString 
		{
			get 
			{
				return EnsureLoaded("ChamarPicker", ref mChamarPickerString);
			}
		}

		private Bitmap mRelatorio;
		public Bitmap Relatorio 
		{
			get 
			{
				return EnsureLoaded("Relatorio.bmp", ref mRelatorio);
			}
		}
		
		private string mPainelApoioString;
		public string PainelApoioString 
		{
			get 
			{
				return EnsureLoaded("PainelApoio", ref mPainelApoioString);
			}
		}

		private Bitmap mProcurarImagem;
		public Bitmap ProcurarImagem 
		{
			get 
			{
				return EnsureLoaded("ProcurarImagem.bmp", ref mProcurarImagem);
			}
		}

		private Bitmap mProcurarImagemOpen;
		public Bitmap ProcurarImagemOpen 
		{
			get 
			{
				return EnsureLoaded("Val.bmp", ref mProcurarImagemOpen);
			}
		}

		private string mProcurarImagemString;
		public string ProcurarImagemString 
		{
			get 
			{
				return EnsureLoaded("ProcurarImagem", ref mProcurarImagemString);
			}
		}

		private Bitmap mActualizar;
		public Bitmap Actualizar 
		{
			get 
			{
				return EnsureLoaded("Actualizar.bmp", ref mActualizar);
			}
		}

		private string mActualizarString;
		public string ActualizarString 
		{
			get 
			{
				return EnsureLoaded("Actualizar", ref mActualizarString);
			}
		}

		private Bitmap mAuthImageError;
		public Bitmap AuthImageError 
		{
			get 
			{
				return EnsureLoaded("authImageError.bmp", ref mAuthImageError);
			}
		}

        private Bitmap mCopy;
        public Bitmap Copy
        {
            get
            {
                return EnsureLoaded("Copy.bmp", ref mCopy);
            }
        }

		#region Icons da outlookbar
		private Bitmap mFRDOIRecolha;
		public Bitmap OBFRDOIRecolha 
		{
			get 
			{
				return EnsureLoaded("OBFRDOIRecolha.bmp", ref mFRDOIRecolha);
			}
		}

		private Bitmap mFRDOIAgregacaoESeleccao;
		public Bitmap OBFRDOIAgregacaoESeleccao 
		{
			get 
			{
				return EnsureLoaded("OBFRDOIAgregacaoESeleccao.bmp", ref mFRDOIAgregacaoESeleccao);
			}
		}

		private Bitmap mFRDOIPublicacao;
		public Bitmap OBFRDOIPublicacao 
		{
			get 
			{
				return EnsureLoaded("OBFRDOIPublicacao.bmp", ref mFRDOIPublicacao);
			}
		}

		private Bitmap mFRDDiplomasModelos;
		public Bitmap OBFRDDiplomasModelos 
		{
			get 
			{
				return EnsureLoaded("OBFRDDiplomasModelos.bmp", ref mFRDDiplomasModelos);
			}
		}

		private Bitmap mCAEntidadeProdutora;
		public Bitmap OBCAEntidadeProdutora 
		{
			get 
			{
				return EnsureLoaded("OBCAEntidadeProdutora.bmp", ref mCAEntidadeProdutora);
			}
		}

		private Bitmap mCAConteudos;
		public Bitmap OBCAConteudos 
		{
			get 
			{
				return EnsureLoaded("OBCAConteudos.bmp", ref mCAConteudos);
			}
		}

		private Bitmap mCATipologiaInformacional;
		public Bitmap OBCATipologiaInformacional 
		{
			get 
			{
				return EnsureLoaded("OBCATipologiaInformacional.bmp", ref mCATipologiaInformacional);
			}
		}

		private Bitmap mUFRecenseamento;
		public Bitmap OBUFRecenseamento 
		{
			get 
			{
				return EnsureLoaded("OBUFRecenseamento.bmp", ref mUFRecenseamento);
			}
		}

		private Bitmap mPesquisa;
		public Bitmap OBPesquisa 
		{
			get 
			{
				return EnsureLoaded("OBPesquisa.bmp", ref mPesquisa);
			}
		}

		private Bitmap mPesquisaUF;
		public Bitmap OBPesquisaUF 
		{
			get 
			{
				return EnsureLoaded("OBPesquisaUF.bmp", ref mPesquisaUF);
			}
		}

		private Bitmap mADMGruposUtilizadores;
		public Bitmap OBADMGruposUtilizadores 
		{
			get 
			{
				return EnsureLoaded("OBADMGruposUtilizadores.bmp", ref mADMGruposUtilizadores);
			}
		}

		private Bitmap mADMUtilizadores;
		public Bitmap OBADMUtilizadores 
		{
			get 
			{
				return EnsureLoaded("OBADMUtilizadores.bmp", ref mADMUtilizadores);
			}
		}

		private Bitmap mADMConfiguracao;
		public Bitmap OBADMConfiguracao 
		{
			get 
			{
				return EnsureLoaded("OBADMConfiguracao.bmp", ref mADMConfiguracao);
			}
		}

		private Bitmap mADMPermissoesModulos;
		public Bitmap OBADMPermissoesModulos 
		{
			get 
			{
				return EnsureLoaded("OBADMPermissoesModulos.bmp", ref mADMPermissoesModulos);
			}
		}

		private Bitmap mADMPermissoesPlanoDeClassificacao;
		public Bitmap OBADMPermissoesPlanoDeClassificacao 
		{
			get 
			{
				return EnsureLoaded("OBADMPermissoesPlanoDeClassificacao.bmp", ref mADMPermissoesPlanoDeClassificacao);
			}
		}
		#endregion

		#region Icons do visualizador de imagens
		private Bitmap mVINext;
		public Bitmap VINext 
		{
			get 
			{
				return EnsureLoaded("VINext.bmp", ref mVINext);
			}
		}

		private Bitmap mVIPrevious;
		public Bitmap VIPrevious 
		{
			get 
			{
				return EnsureLoaded("VIPrevious.bmp", ref mVIPrevious);
			}
		}

		private Bitmap mVIRotLeft;
		public Bitmap VIRotLeft 
		{
			get 
			{
				return EnsureLoaded("VIRotLeft.bmp", ref mVIRotLeft);
			}
		}

		private Bitmap mVIRotNone;
		public Bitmap VIRotNone 
		{
			get 
			{
				return EnsureLoaded("VIRotNone.bmp", ref mVIRotNone);
			}
		}

		private Bitmap mVIRotRight;
		public Bitmap VIRotRight 
		{
			get 
			{
				return EnsureLoaded("VIRotRight.bmp", ref mVIRotRight);
			}
		}

		private Bitmap mVIZoomAll;
		public Bitmap VIZommAll 
		{
			get 
			{
				return EnsureLoaded("VIZoomAll.bmp", ref mVIZoomAll);
			}
		}

		private Bitmap mVIZoomin;
		public Bitmap VIZoomin 
		{
			get 
			{
				return EnsureLoaded("VIZoomin.bmp", ref mVIZoomin);
			}
		}

		private Bitmap mVIZoomout;
		public Bitmap VIZoomout 
		{
			get 
			{
				return EnsureLoaded("VIZoomout.bmp", ref mVIZoomout);
			}
		}

		private Bitmap mVIZoomReal;
		public Bitmap VIZoomReal 
		{
			get 
			{
				return EnsureLoaded("VIZoomReal.bmp", ref mVIZoomReal);
			}
		}

		ImageList mVisualizadorImagensImageList;
		public ImageList VisualizadorImagensImageList 
		{
			get 
			{
				if (mVisualizadorImagensImageList == null) 
				{
					mVisualizadorImagensImageList = new ImageList();
					mVisualizadorImagensImageList.TransparentColor = GlobalTransparentColor;
					mVisualizadorImagensImageList.Images.Add(VIPrevious);
					mVisualizadorImagensImageList.Images.Add(VINext);
					mVisualizadorImagensImageList.Images.Add(VIRotLeft);
					mVisualizadorImagensImageList.Images.Add(VIRotNone);
					mVisualizadorImagensImageList.Images.Add(VIRotRight);
					mVisualizadorImagensImageList.Images.Add(VIZoomin);
					mVisualizadorImagensImageList.Images.Add(VIZoomout);
					mVisualizadorImagensImageList.Images.Add(VIZommAll);
					mVisualizadorImagensImageList.Images.Add(VIZoomReal);
				}
				return mVisualizadorImagensImageList;
			}
		}
		#endregion
		
		#region Icons e strings para a manipulacao de unidades fisicas
		private Bitmap mUFCriar;
		public Bitmap UFCriar 
		{
			get 
			{
				return EnsureLoaded("UFCriar.bmp", ref mUFCriar);
			}
		}

        private Bitmap mUFDuplicar;
        public Bitmap UFDuplicar
        {
            get
            {
                return EnsureLoaded("UFDuplicar.bmp", ref mUFDuplicar);
            }
        }

		private Bitmap mUFEditar;
		public Bitmap UFEditar 
		{
			get 
			{
				return EnsureLoaded("UFEditar.bmp", ref mUFEditar);
			}
		}

		private Bitmap mUFEliminar;
		public Bitmap UFEliminar 
		{
			get 
			{
				return EnsureLoaded("UFEliminar.bmp", ref mUFEliminar);
			}
		}

		ImageList mUFManipulacaoImageList = null;
		public ImageList UFManipulacaoImageList 
		{
			get 
			{
				if (mUFManipulacaoImageList == null) 
				{
					mUFManipulacaoImageList = new ImageList();
					mUFManipulacaoImageList.TransparentColor = GlobalTransparentColor;
					mUFManipulacaoImageList.Images.Add(UFCriar);
                    mUFManipulacaoImageList.Images.Add(UFDuplicar);
					mUFManipulacaoImageList.Images.Add(UFEditar);
					mUFManipulacaoImageList.Images.Add(UFEliminar);
					mUFManipulacaoImageList.Images.Add(Filtro);
					mUFManipulacaoImageList.Images.Add(Relatorio);
				}
				return mUFManipulacaoImageList;
			}
		}

		string[] mUFManipulacaoStrings = new string[6];
		public string[] UFManipulacaoStrings 
		{
			get 
			{
				if (mUFManipulacaoStrings[0] == null) 
				{
					mUFManipulacaoStrings[0] = GetString("UFCriar");
                    mUFManipulacaoStrings[1] = GetString("UFDuplicar");
					mUFManipulacaoStrings[2] = GetString("UFEditar");
					mUFManipulacaoStrings[3] = GetString("UFEliminar");
					mUFManipulacaoStrings[4] = GetString("FiltrarDados");
					mUFManipulacaoStrings[5] = GetString("Relatorio");
				}
				return mUFManipulacaoStrings;
			}
		}
		#endregion
		
		#region Icons para a manipulacao de níveis
		private Bitmap mNVLAdd;
		public Bitmap NVLAdd 
		{
			get 
			{
				return EnsureLoaded("NVLAdd.bmp", ref mNVLAdd);
			}
		}

		private Bitmap mNVLCut;
		public Bitmap NVLCut 
		{
			get 
			{
				return EnsureLoaded("NVLCut.bmp", ref mNVLCut);
			}
		}

		private Bitmap mNVLPaste;
		public Bitmap NVLPaste 
		{
			get 
			{
				return EnsureLoaded("NVLPaste.bmp", ref mNVLPaste);
			}
		}

		private Bitmap mNVLToggleEstrutural;
		public Bitmap NVLToggleEstrutural 
		{
			get 
			{
				return EnsureLoaded("NVLToggleEstrutural.bmp", ref mNVLToggleEstrutural);
			}
		}

		private Bitmap mNVLToggleDocumental;
		public Bitmap NVLToggleDocumental 
		{
			get 
			{
				return EnsureLoaded("NVLToggleDocumental.bmp", ref mNVLToggleDocumental);
			}
		}

		ImageList mNVLManipulacaoImageList = null;
		public ImageList NVLManipulacaoImageList 
		{
			get 
			{
				if (mNVLManipulacaoImageList == null) 
				{
					mNVLManipulacaoImageList = new ImageList();
					mNVLManipulacaoImageList.TransparentColor = GlobalTransparentColor;
					mNVLManipulacaoImageList.Images.Add(EDCriar);
					mNVLManipulacaoImageList.Images.Add(NVLAdd);
					mNVLManipulacaoImageList.Images.Add(EDEditar);
					mNVLManipulacaoImageList.Images.Add(EDEliminar);
					mNVLManipulacaoImageList.Images.Add(NVLCut);
					mNVLManipulacaoImageList.Images.Add(NVLPaste);
					mNVLManipulacaoImageList.Images.Add(NVLToggleEstrutural);
					mNVLManipulacaoImageList.Images.Add(NVLToggleDocumental);
					mNVLManipulacaoImageList.Images.Add(Filtro);
					mNVLManipulacaoImageList.Images.Add(Relatorio);
                    mNVLManipulacaoImageList.Images.Add(GenEAD);
                    mNVLManipulacaoImageList.Images.Add(Import);
				}
				return mNVLManipulacaoImageList;
			}
		}

		string[] mNVLManipulacaoStrings = new string[12];
		public string[] NVLManipulacaoStrings 
		{
			get 
			{
				if (mNVLManipulacaoStrings[0] == null) 
				{
					mNVLManipulacaoStrings[0] = "Criar entidade detentora";
					mNVLManipulacaoStrings[1] = GetString("NVLAdd");
					mNVLManipulacaoStrings[2] = "Editar nível";
					mNVLManipulacaoStrings[3] = "Eliminar nível";
					mNVLManipulacaoStrings[4] = GetString("NVLCut");
					mNVLManipulacaoStrings[5] = GetString("NVLPaste");
					mNVLManipulacaoStrings[6] = GetString("NVLToggleEstrutural");
					mNVLManipulacaoStrings[7] = GetString("NVLToggleDocumental");
					mNVLManipulacaoStrings[8] = GetString("FiltrarDados");
					mNVLManipulacaoStrings[9] = GetString("Relatorio");
                    mNVLManipulacaoStrings[10] = GetString("GenEADString");
                    mNVLManipulacaoStrings[11] = GetString("import-document");
				}
				return mNVLManipulacaoStrings;
			}
		}
		#endregion

        #region Icons para a manipulacao de notícias de autoridade
        private Bitmap mCACriar;
		public Bitmap CACriar 
		{
			get 
			{
				return EnsureLoaded("CACriar.bmp", ref mCACriar);
			}
		}

		private Bitmap mCAEditar;
		public Bitmap CAEditar 
		{
			get 
			{
				return EnsureLoaded("CAEditar.bmp", ref mCAEditar);
			}
		}

		private Bitmap mCAEliminar;
		public Bitmap CAEliminar 
		{
			get 
			{
				return EnsureLoaded("CAEliminar.bmp", ref mCAEliminar);
			}
		}

		private Bitmap mCARelatorios;
		public Bitmap CARelatorios 
		{
			get 
			{
				return EnsureLoaded("Relatorio.bmp", ref mCARelatorios);
			}
		}

		ImageList mCAManipulacaoImageList = null;
		public ImageList CAManipulacaoImageList 
		{
			get 
			{
				if (mCAManipulacaoImageList == null) 
				{
					mCAManipulacaoImageList = new ImageList();
					mCAManipulacaoImageList.TransparentColor = GlobalTransparentColor;
					mCAManipulacaoImageList.Images.Add(CACriar);
					mCAManipulacaoImageList.Images.Add(CAEditar);
					mCAManipulacaoImageList.Images.Add(CAEliminar);
					mCAManipulacaoImageList.Images.Add(Filtro);
					mCAManipulacaoImageList.Images.Add(Relatorio);
				}
				return mCAManipulacaoImageList;
			}
		}

		string[] mCAManipulacaoStrings = new string[5];
		public string[] CAManipulacaoStrings 
		{
			get 
			{
				if (mCAManipulacaoStrings[0] == null) 
				{
					mCAManipulacaoStrings[0] = GetString("CACriar");
					mCAManipulacaoStrings[1] = GetString("CAEditar");
					mCAManipulacaoStrings[2] = GetString("CAEliminar");
					mCAManipulacaoStrings[3] = GetString("FiltrarDados");
					mCAManipulacaoStrings[4] = GetString("Relatorio");
				}
				return mCAManipulacaoStrings;
			}
		}
		#endregion
		
		#region Icons para a manipulacao de grupos de utilizadores
		private Bitmap mGrUsrCriar;
		public Bitmap GrUsrCriar 
		{
			get 
			{
				return EnsureLoaded("GrUsrCriar.bmp", ref mGrUsrCriar);
			}
		}

		private Bitmap mGrUsrEditar;
		public Bitmap GrUsrEditar 
		{
			get 
			{
				return EnsureLoaded("GrUsrEditar.bmp", ref mGrUsrEditar);
			}
		}

		private Bitmap mGrUsrEliminar;
		public Bitmap GrUsrEliminar 
		{
			get 
			{
				return EnsureLoaded("GrUsrEliminar.bmp", ref mGrUsrEliminar);
			}
		}

		ImageList mGrUsrManipulacaoImageList = null;
		public ImageList GrUsrManipulacaoImageList 
		{
			get 
			{
				if (mGrUsrManipulacaoImageList == null) 
				{
					mGrUsrManipulacaoImageList = new ImageList();
					mGrUsrManipulacaoImageList.TransparentColor = GlobalTransparentColor;
					mGrUsrManipulacaoImageList.Images.Add(GrUsrCriar);
					mGrUsrManipulacaoImageList.Images.Add(GrUsrEditar);
					mGrUsrManipulacaoImageList.Images.Add(GrUsrEliminar);
				}
				return mGrUsrManipulacaoImageList;
			}
		}

		string[] mGrUsrManipulacaoString = new string[3];
		public string[] GrUsrManipulacaoString 
		{
			get 
			{
				if (mGrUsrManipulacaoString[0] == null) 
				{
					mGrUsrManipulacaoString[0] = GetString("GrUsrCriar");
					mGrUsrManipulacaoString[1] = GetString("GrUsrEditar");
					mGrUsrManipulacaoString[2] = GetString("GrUsrEliminar");
				}
				return mGrUsrManipulacaoString;
			}
		}
		#endregion
		
		#region Icons para a manipulacao de utilizadores
		private Bitmap mUsrCriar;
		public Bitmap UsrCriar 
		{
			get 
			{
				return EnsureLoaded("UsrCriar.bmp", ref mUsrCriar);
			}
		}

		private Bitmap mUsrEditar;
		public Bitmap UsrEditar 
		{
			get 
			{
				return EnsureLoaded("UsrEditar.bmp", ref mUsrEditar);
			}
		}

		private Bitmap mUsrEliminar;
		public Bitmap UsrEliminar 
		{
			get 
			{
				return EnsureLoaded("UsrEliminar.bmp", ref mUsrEliminar);
			}
		}

		private Bitmap mUsrAlterarPalavraChave;
		public Bitmap UsrAlterarPalavraChave 
		{
			get 
			{
				return EnsureLoaded("UsrAlterarPalavraChave.bmp", ref mUsrAlterarPalavraChave);
			}
		}

		private Bitmap mUsrSwitch;
		public Bitmap UsrSwitch 
		{
			get 
			{
				return EnsureLoaded("UsrSwitch.bmp", ref mUsrSwitch);
			}
		}

		private string mUsrSwitchString;
		public string UsrSwitchString 
		{
			get 
			{
				return EnsureLoaded("UsrSwitch", ref mUsrSwitchString);
			}
		}

		ImageList mUsrManipulacaoImageList = null;
		public ImageList UsrManipulacaoImageList 
		{
			get 
			{
				if (mUsrManipulacaoImageList == null) 
				{
					mUsrManipulacaoImageList = new ImageList();
					mUsrManipulacaoImageList.TransparentColor = GlobalTransparentColor;
					mUsrManipulacaoImageList.Images.Add(UsrCriar);
					mUsrManipulacaoImageList.Images.Add(UsrEditar);
					mUsrManipulacaoImageList.Images.Add(UsrEliminar);
					mUsrManipulacaoImageList.Images.Add(UsrAlterarPalavraChave);
				}
				return mUsrManipulacaoImageList;
			}
		}

		string[] mUsrManipulacaoString = new string[4];
		public string[] UsrManipulacaoString 
		{
			get 
			{
				if (mUsrManipulacaoString[0] == null) 
				{
					mUsrManipulacaoString[0] = GetString("UsrCriar");
					mUsrManipulacaoString[1] = GetString("UsrEditar");
					mUsrManipulacaoString[2] = GetString("UsrEliminar");
					mUsrManipulacaoString[3] = GetString("UsrAlterarPalavraChave");
				}
				return mUsrManipulacaoString;
			}
		}
		#endregion

		#region Icons para a manipulação de diplomas e modelos
		private Bitmap mDMCriar;
		public Bitmap DMCriar 
		{
			get 
			{
				return EnsureLoaded("DMCriar.bmp", ref mDMCriar);
			}
		}

		private string mDMCriarString;
		public string DMCriarString 
		{
			get 
			{
				return EnsureLoaded("DMCriar", ref mDMCriarString);
			}
		}

		private Bitmap mDMEditar;
		public Bitmap DMEditar 
		{
			get 
			{
				return EnsureLoaded("DMEditar.bmp", ref mDMEditar);
			}
		}

		private string mDMEditarString;
		public string DMEditarString 
		{
			get 
			{
				return EnsureLoaded("DMEditar", ref mDMEditarString);
			}
		}

		private Bitmap mDMEliminar;
		public Bitmap DMEliminar 
		{
			get 
			{
				return EnsureLoaded("DMEliminar.bmp", ref mDMEliminar);
			}
		}

		private string mDMEliminarString;
		public string DMEliminarString 
		{
			get 
			{
				return EnsureLoaded("DMEliminar", ref mDMEliminarString);
			}
		}

        private string sEntCriar;
        public string EntidadeCriar {
            get { return EnsureLoaded("EntCriar", ref sEntCriar); }
        }

        private string sEntModificar;
        public string EntidadeModificar {
            get { return EnsureLoaded("EntModificar", ref sEntModificar); }
        }

        private string sEntApagar;
        public string EntidadeApagar {
            get { return EnsureLoaded("EntApagar", ref sEntApagar); }
        }

        private string sEntFiltro;
        public string EntidadeFiltro {
            get { return EnsureLoaded("EntFiltro", ref sEntFiltro); }
        }

		ImageList mDMManipulacaoImageList = null;
		public ImageList DMManipulacaoImageList 
		{
			get 
			{
				if (mDMManipulacaoImageList == null) 
				{
					mDMManipulacaoImageList = new ImageList();
					mDMManipulacaoImageList.TransparentColor = GlobalTransparentColor;
					mDMManipulacaoImageList.Images.Add(DMCriar);
					mDMManipulacaoImageList.Images.Add(DMEditar);
					mDMManipulacaoImageList.Images.Add(DMEliminar);
					mDMManipulacaoImageList.Images.Add(Filtro);
				}
				return mDMManipulacaoImageList;
			}
		}

		string[] mDMManipulacaoStrings = new string[4];
		public string[] DMManipulacaoStrings 
		{
			get 
			{
				if (mDMManipulacaoStrings[0] == null) 
				{
					mDMManipulacaoStrings[0] = DMCriarString;
					mDMManipulacaoStrings[1] = DMEditarString;
					mDMManipulacaoStrings[2] = DMEliminarString;
					mDMManipulacaoStrings[3] = FiltroString;
				}
				return mDMManipulacaoStrings;
			}
		}

        string[] sEntidadeManipulacaoStrings = new string[4];
        public string[] EntidadeManipulacaoStrings {
            get {
                if (sEntidadeManipulacaoStrings[0] == null) {
                    sEntidadeManipulacaoStrings[0] = EntidadeCriar;
                    sEntidadeManipulacaoStrings[1] = EntidadeModificar;
                    sEntidadeManipulacaoStrings[2] = EntidadeApagar;
                    sEntidadeManipulacaoStrings[3] = EntidadeFiltro;
                }
                return sEntidadeManipulacaoStrings;
            }
        }
		#endregion

        #region Icons para a manipulação de títulos para objetos digitais
        private string sODTituloCriar;
        public string ODTituloCriar
        {
            get { return EnsureLoaded("ODTituloCriar", ref sODTituloCriar); }
        }

        private string sODTituloModificar;
        public string ODTituloModificar
        {
            get { return EnsureLoaded("ODTituloModificar", ref sODTituloModificar); }
        }

        private string sODTituloApagar;
        public string ODTituloApagar
        {
            get { return EnsureLoaded("ODTituloApagar", ref sODTituloApagar); }
        }

        private string sODTituloFiltro;
        public string ODTituloFiltro
        {
            get { return EnsureLoaded("ODTituloFiltro", ref sODTituloFiltro); }
        }

        string[] mODTituloManipulacaoStrings = new string[4];
        public string[] ODTituloManipulacaoStrings
        {
            get
            {
                if (mODTituloManipulacaoStrings[0] == null)
                {
                    mODTituloManipulacaoStrings[0] = ODTituloCriar;
                    mODTituloManipulacaoStrings[1] = ODTituloModificar;
                    mODTituloManipulacaoStrings[2] = ODTituloApagar;
                    mODTituloManipulacaoStrings[3] = ODTituloFiltro;
                }
                return mODTituloManipulacaoStrings;
            }
        }
        #endregion

        #region Icones para manipulação de autos de eliminação
        private Bitmap mAECriar;
        public Bitmap AECriar
        {
            get
            {
                return EnsureLoaded("AutoEliminacao_criar_16x16.png", ref mAECriar);
            }
        }

        private string mAECriarString;
        public string AECriarString
        {
            get
            {
                return EnsureLoaded("CriarAutoEliminacao", ref mAECriarString);
            }
        }

        private Bitmap mAEEditar;
        public Bitmap AEEditar
        {
            get
            {
                return EnsureLoaded("AutoEliminacao_editar_16x16.png", ref mAEEditar);
            }
        }

        private string mAEEditarString;
        public string AEEditarString
        {
            get
            {
                return EnsureLoaded("EditarAutoEliminacao", ref mAEEditarString);
            }
        }

        private Bitmap mAEApagar;
        public Bitmap AEApagar
        {
            get
            {
                return EnsureLoaded("AutoEliminacao_apagar_16x16.png", ref mAEApagar);
            }
        }

        private string mAEApagarString;
        public string AEApagarString
        {
            get
            {
                return EnsureLoaded("ApagarAutoEliminacao", ref mAEApagarString);
            }
        }
        #endregion                  

        #region Icones para ordenação na pesquisa
        private Bitmap mAdicionaUm;
		public Bitmap AdicionaUm
		{
			get 
			{
				return EnsureLoaded("AdicionaUm.bmp", ref mAdicionaUm);
			}
		}

		private Bitmap mAdicionaTodos;
		public Bitmap AdicionaTodos
		{
			get 
			{
				return EnsureLoaded("AdicionaTodos.bmp", ref mAdicionaTodos);
			}
		}

		private Bitmap mRemoveUm;
		public Bitmap RemoveUm
		{
			get 
			{
				return EnsureLoaded("RemoveUm.bmp", ref mRemoveUm);
			}
		}

		private Bitmap mRemoveTodos;
		public Bitmap RemoveTodos
		{
			get 
			{
				return EnsureLoaded("RemoveTodos.bmp", ref mRemoveTodos);
			}
		}
		#endregion

		#region Dimensões
		private Bitmap mDimensaoPropria;
		public Bitmap DimensaoPropria 
		{
			get 
			{
				return EnsureLoaded("dimensaoPropria.bmp", ref mDimensaoPropria);
			}
		}

		private Bitmap mDimensaoHerdada;
		public Bitmap DimensaoHerdada 
		{
			get 
			{
				return EnsureLoaded("dimensaoHerdada.bmp", ref mDimensaoHerdada);
			}
		}

		private Bitmap mDimensaoHerdadaUF;
		public Bitmap DimensaoHerdadaUF 
		{
			get 
			{
				return EnsureLoaded("dimensaoHerdadaUF.bmp", ref mDimensaoHerdadaUF);
			}
		}

		ImageList mDimensoesImageList = null;
		public ImageList DimensoesImageList 
		{
			get 
			{
				if (mDimensoesImageList == null) 
				{
					mDimensoesImageList = new ImageList();
					mDimensoesImageList.TransparentColor = GlobalTransparentColor;
					mDimensoesImageList.Images.Add(DimensaoPropria);
					mDimensoesImageList.Images.Add(DimensaoHerdada);
					mDimensoesImageList.Images.Add(UF);
					mDimensoesImageList.Images.Add(DimensaoHerdadaUF);
				}
				return mDimensoesImageList;
			}
		}
		#endregion

		#region Pesquisa
		private Bitmap mPesquisaResultados;
		public Bitmap PesquisaResultados 
		{
			get 
			{
				return EnsureLoaded("PesquisaResultados.bmp", ref mPesquisaResultados);
			}
		}

        private Bitmap mPesquisaResultadosCA;
        public Bitmap PesquisaResultadosCA
        {
            get
            {
                return EnsureLoaded("PesquisaResultadosCA.bmp", ref mPesquisaResultadosCA);
            }
        }

		private Bitmap mPesquisaResultadosResumo;
		public Bitmap PesquisaResultadosResumo 
		{
			get 
			{
				return EnsureLoaded("PesquisaResumo.bmp", ref mPesquisaResultadosResumo);
			}
		}

		private Bitmap mPesquisaResultadosUFs;
		public Bitmap PesquisaResultadosUFs 
		{
			get 
			{
				return EnsureLoaded("PesquisaUnidadesFisicas.bmp", ref mPesquisaResultadosUFs);
			}
		}

		private Bitmap mPesquisaResultadosImagens;
		public Bitmap PesquisaResultadosImagens 
		{
			get 
			{
				return EnsureLoaded("PesquisaImagens.bmp", ref mPesquisaResultadosImagens);
			}
		}

		ImageList mPesquisaResultadosImageList = null;
		public ImageList PesquisaResultadosImageList 
		{
			get 
			{
				if (mPesquisaResultadosImageList == null) 
				{
					mPesquisaResultadosImageList = new ImageList();
					mPesquisaResultadosImageList.TransparentColor = GlobalTransparentColor;
                    mPesquisaResultadosImageList.Images.Add(PesquisaResultados);
					mPesquisaResultadosImageList.Images.Add(PesquisaResultadosResumo);
					mPesquisaResultadosImageList.Images.Add(PesquisaResultadosImagens);
					mPesquisaResultadosImageList.Images.Add(PesquisaResultadosUFs);
					mPesquisaResultadosImageList.Images.Add(Relatorio);
                    mPesquisaResultadosImageList.Images.Add(PesquisaResultadosCA);
                    mPesquisaResultadosImageList.Images.Add(SD);
 				}
				return mPesquisaResultadosImageList;
			}
		}

		private Bitmap mExecutarPesquisa;
		public Bitmap ExecutarPesquisa 
		{
			get 
			{
				return EnsureLoaded("pesquisa_run.png", ref mExecutarPesquisa);
			}
		}

		private string mExecutarPesquisaString;
		public string ExecutarPesquisaString 
		{
			get 
			{
                return EnsureLoaded("pesquisa_run", ref mExecutarPesquisaString);
			}
		}

		private Bitmap mLimparPesquisa;
		public Bitmap LimparPesquisa 
		{
			get 
			{
				return EnsureLoaded("LimparPesquisa.bmp", ref mLimparPesquisa);
			}
		}

		private string mLimparPesquisaString;
		public string LimparPesquisaString 
		{
			get 
			{
				return EnsureLoaded("LimparPesquisa", ref mLimparPesquisaString);
			}
		}

		private Bitmap mAjuda;
		public Bitmap Ajuda 
		{
			get 
			{
				return EnsureLoaded("Ajuda.bmp", ref mAjuda);
			}
		}

		private string mAjudaString;
		public string AjudaString 
		{
			get 
			{
				return EnsureLoaded("Ajuda", ref mAjudaString);
			}
		}

        private Bitmap mPesquisaSimples;
        public Bitmap PesquisaSimples
        {
            get
            {
                return EnsureLoaded("pesquisa_simples.png", ref mPesquisaSimples);
            }
        }

        private string mPesquisaSimplesString;
        public string PesquisaSimplesString
        {
            get
            {
                return EnsureLoaded("pesquisa_simples", ref mPesquisaSimplesString);
            }
        }

        private Bitmap mPesquisaAvancada;
        public Bitmap PesquisaAvancada
        {
            get
            {
                return EnsureLoaded("pesquisa_avancada.png", ref mPesquisaAvancada);
            }
        }

        private string mPesquisaAvancadaString;
        public string PesquisaAvancadaString
        {
            get
            {
                return EnsureLoaded("pesquisa_avancada", ref mPesquisaAvancadaString);
            }
        }

		ImageList mPesquisarImageList = null;
		public ImageList PesquisarImageList 
		{
			get 
			{
				if (mPesquisarImageList == null) 
				{
					mPesquisarImageList = new ImageList();
					mPesquisarImageList.TransparentColor = GlobalTransparentColor;
					mPesquisarImageList.Images.Add(ExecutarPesquisa);
					mPesquisarImageList.Images.Add(LimparPesquisa);
					mPesquisarImageList.Images.Add(Ajuda);
                    mPesquisarImageList.Images.Add(PesquisaSimples);
                    mPesquisarImageList.Images.Add(PesquisaAvancada);
				}
				return mPesquisarImageList;
			}
		}
		#endregion
		
		#region Pesquisa de unidades físicas
		ImageList mPesquisaUFsImageList = null;
		public ImageList PesquisaUFsImageList 
		{
			get 
			{
				if (mPesquisaUFsImageList == null) 
				{
					mPesquisaUFsImageList = new ImageList();
					mPesquisaUFsImageList.TransparentColor = GlobalTransparentColor;
					mPesquisaUFsImageList.Images.Add(PesquisaResultadosUFs);
					mPesquisaUFsImageList.Images.Add(PesquisaResultadosResumo);
					mPesquisaUFsImageList.Images.Add(PesquisaResultados);
					mPesquisaUFsImageList.Images.Add(Relatorio);
				}
				return mPesquisaUFsImageList;
			}
		}
		#endregion

		#region Model
		private Bitmap mIncognito;
		public Bitmap Incognito
		{
			get
			{
				return EnsureLoaded("NivelIncognito.bmp", ref mIncognito);
			}
		}

		private Bitmap mED;
		public Bitmap ED
		{
			get 
			{
				return EnsureLoaded("ED.bmp", ref mED);
			}
		}

		private Bitmap mGA;
		public Bitmap GA
		{
			get
			{
				return EnsureLoaded("GA.bmp", ref mGA);
			}
		}

		private Bitmap mA;
		public Bitmap A
		{
			get
			{
				return EnsureLoaded("A.bmp", ref mA);
			}
		}

		private Bitmap mSA;
		public Bitmap SA
		{
			get
			{
				return EnsureLoaded("SA.bmp", ref mSA);
			}
		}

		private Bitmap mSC;
		public Bitmap SC
		{
			get
			{
				return EnsureLoaded("SC.bmp", ref mSC);
			}
		}

		private Bitmap mSSC;
		public Bitmap SSC
		{
			get
			{
				return EnsureLoaded("SSC.bmp", ref mSSC);
			}
		}

		private Bitmap mSR;
		public Bitmap SR
		{
			get
			{
				return EnsureLoaded("SR.bmp", ref mSR);
			}
		}

		private Bitmap mSSR;
		public Bitmap SSR
		{
			get
			{
				return EnsureLoaded("SSR.bmp", ref mSSR);
			}
		}

		private Bitmap mD;
		public Bitmap D
		{
			get
			{
				return EnsureLoaded("D.bmp", ref mD);
			}
		}

		private Bitmap mSD;
		public Bitmap SD
		{
			get
			{
				return EnsureLoaded("SD.bmp", ref mSD);
			}
		}

		private Bitmap mUF;
		public Bitmap UF
		{
			get
			{
				return EnsureLoaded("UF.bmp", ref mUF);
			}
		}

		private Bitmap mEDCriar;
		public Bitmap EDCriar
		{
			get
			{
				return EnsureLoaded("EDCriar.bmp", ref mEDCriar);
			}
		}

		private Bitmap mGACriar;
		public Bitmap GACriar
		{
			get
			{
				return EnsureLoaded("GACriar.bmp", ref mGACriar);
			}
		}

		private Bitmap mACriar;
		public Bitmap ACriar
		{
			get
			{
				return EnsureLoaded("ACriar.bmp", ref mACriar);
			}
		}

		private Bitmap mSACriar;
		public Bitmap SACriar
		{
			get
			{
				return EnsureLoaded("SACriar.bmp", ref mSACriar);
			}
		}

		private Bitmap mSCCriar;
		public Bitmap SCCriar
		{
			get
			{
				return EnsureLoaded("SCCriar.bmp", ref mSCCriar);
			}
		}

		private Bitmap mSSCCriar;
		public Bitmap SSCCriar
		{
			get
			{
				return EnsureLoaded("SSCCriar.bmp", ref mSSCCriar);
			}
		}

		private Bitmap mSRCriar;
		public Bitmap SRCriar
		{
			get
			{
				return EnsureLoaded("SRCriar.bmp", ref mSRCriar);
			}
		}

		private Bitmap mSSRCriar;
		public Bitmap SSRCriar
		{
			get
			{
				return EnsureLoaded("SSRCriar.bmp", ref mSSRCriar);
			}
		}

		private Bitmap mDCriar;
		public Bitmap DCriar
		{
			get
			{
				return EnsureLoaded("DCriar.bmp", ref mDCriar);
			}
		}

		private Bitmap mSDCriar;
		public Bitmap SDCriar
		{
			get
			{
				return EnsureLoaded("SDCriar.bmp", ref mSDCriar);
			}
		}

//		private Bitmap mUFCriar;
//		public Bitmap UFCriar
//		{
//			get
//			{
//				return EnsureLoaded("UFCriar.bmp", ref mUFCriar);
//			}
//		}

		private Bitmap mEDEditar;
		public Bitmap EDEditar
		{
			get
			{
				return EnsureLoaded("EDEditar.bmp", ref mEDEditar);
			}
		}

		private Bitmap mGAEditar;
		public Bitmap GAEditar
		{
			get
			{
				return EnsureLoaded("GAEditar.bmp", ref mGAEditar);
			}
		}

		private Bitmap mAEditar;
		public Bitmap AEditar
		{
			get
			{
				return EnsureLoaded("AEditar.bmp", ref mAEditar);
			}
		}

		private Bitmap mSAEditar;
		public Bitmap SAEditar
		{
			get
			{
				return EnsureLoaded("SAEditar.bmp", ref mSAEditar);
			}
		}

		private Bitmap mSCEditar;
		public Bitmap SCEditar
		{
			get
			{
				return EnsureLoaded("SCEditar.bmp", ref mSCEditar);
			}
		}

		private Bitmap mSSCEditar;
		public Bitmap SSCEditar
		{
			get
			{
				return EnsureLoaded("SSCEditar.bmp", ref mSSCEditar);
			}
		}

		private Bitmap mSREditar;
		public Bitmap SREditar
		{
			get
			{
				return EnsureLoaded("SREditar.bmp", ref mSREditar);
			}
		}

		private Bitmap mSSREditar;
		public Bitmap SSREditar
		{
			get
			{
				return EnsureLoaded("SSREditar.bmp", ref mSSREditar);
			}
		}

		private Bitmap mDEditar;
		public Bitmap DEditar
		{
			get
			{
				return EnsureLoaded("DEditar.bmp", ref mDEditar);
			}
		}

		private Bitmap mSDEditar;
		public Bitmap SDEditar
		{
			get
			{
				return EnsureLoaded("SDEditar.bmp", ref mSDEditar);
			}
		}

//		private Bitmap mUFEditar;
//		public Bitmap UFEditar
//		{
//			get
//			{
//				return EnsureLoaded("UFEditar.bmp", ref mUFEditar);
//			}
//		}

		private Bitmap mEDEliminar;
		public Bitmap EDEliminar
		{
			get
			{
				return EnsureLoaded("EDEliminar.bmp", ref mEDEliminar);
			}
		}

		private Bitmap mGAEliminar;
		public Bitmap GAEliminar
		{
			get
			{
				return EnsureLoaded("GAEliminar.bmp", ref mGAEliminar);
			}
		}

		private Bitmap mAEliminar;
		public Bitmap AEliminar
		{
			get
			{
				return EnsureLoaded("AEliminar.bmp", ref mAEliminar);
			}
		}

		private Bitmap mSAEliminar;
		public Bitmap SAEliminar
		{
			get
			{
				return EnsureLoaded("SAEliminar.bmp", ref mSAEliminar);
			}
		}

		private Bitmap mSCEliminar;
		public Bitmap SCEliminar
		{
			get
			{
				return EnsureLoaded("SCEliminar.bmp", ref mSCEliminar);
			}
		}

		private Bitmap mSSCEliminar;
		public Bitmap SSCEliminar
		{
			get
			{
				return EnsureLoaded("SSCEliminar.bmp", ref mSSCEliminar);
			}
		}

		private Bitmap mSREliminar;
		public Bitmap SREliminar
		{
			get
			{
				return EnsureLoaded("SREliminar.bmp", ref mSREliminar);
			}
		}

		private Bitmap mSSREliminar;
		public Bitmap SSREliminar
		{
			get
			{
				return EnsureLoaded("SSREliminar.bmp", ref mSSREliminar);
			}
		}

		private Bitmap mDEliminar;
		public Bitmap DEliminar
		{
			get
			{
				return EnsureLoaded("DEliminar.bmp", ref mDEliminar);
			}
		}

		private Bitmap mSDEliminar;
		public Bitmap SDEliminar
		{
			get
			{
				return EnsureLoaded("SDEliminar.bmp", ref mSDEliminar);
			}
		}

//		private Bitmap mUFEliminar;
//		public Bitmap UFEliminar
//		{
//			get
//			{
//				return EnsureLoaded("UFEliminar.bmp", ref mUFEliminar);
//			}
//		}

		public static Bitmap MakeOverlay(Bitmap Image1, Bitmap image2)
		{
			Debug.Assert(Image1 != null);
			Debug.Assert(image2 != null);
			Debug.Assert(Image1.Width == image2.Width & Image1.Height == image2.Height);

			Bitmap Result = new Bitmap(Image1.Width, Image1.Height);
			Graphics g = Graphics.FromImage(Result);

			Image1.MakeTransparent(Color.Fuchsia);
			g.DrawImage(Image1, 0, 0);

			image2.MakeTransparent(Color.Fuchsia);
			g.DrawImage(image2, 0, 0);

			g.Dispose();

			return Result;
		}

		private Bitmap mControloAutOverlay;
		public Bitmap ControloAutOverlay
		{
			get
			{
				return EnsureLoaded("ControloAutOverlay.bmp", ref mControloAutOverlay);
			}
		}


		//private Bitmap mEDControloAut;
		public Bitmap EDControloAut
		{
			get
			{
				return MakeOverlay(ED, ControloAutOverlay);
			}
		}

		//private Bitmap mGAControloAut;
		public Bitmap GAControloAut
		{
			get
			{
				return MakeOverlay(GA, ControloAutOverlay);
			}
		}

		//private Bitmap mAControloAut;
		public Bitmap AControloAut
		{
			get
			{
				return MakeOverlay(A, ControloAutOverlay);
			}
		}

		//private Bitmap mSAControloAut;
		public Bitmap SAControloAut
		{
			get
			{
				return MakeOverlay(SA, ControloAutOverlay);
			}
		}

		//private Bitmap mSCControloAut;
		public Bitmap SCControloAut
		{
			get
			{
				return MakeOverlay(SC, ControloAutOverlay);
			}
		}

		//private Bitmap mSSCControloAut;
		public Bitmap SSCControloAut
		{
			get
			{
				return MakeOverlay(SSC, ControloAutOverlay);
			}
		}

		//private Bitmap mIncognitoControloAut;
		public Bitmap IncognitoControloAut 
		{
			get 
			{
				return MakeOverlay(Incognito, ControloAutOverlay);
			}
		}

		private ImageList mNiveisImageList;
		public ImageList NiveisImageList
		{
			get
			{
				if (mNiveisImageList == null)
				{
					mNiveisImageList = new ImageList();
					mNiveisImageList.ImageSize = new Size(16, 18);
					mNiveisImageList.TransparentColor = GlobalTransparentColor;
					mNiveisImageList.Images.Add(Incognito);
					mNiveisImageList.Images.Add(IncognitoControloAut);
					mNiveisImageList.Images.Add(ED);
					mNiveisImageList.Images.Add(GA);
					mNiveisImageList.Images.Add(A);
					mNiveisImageList.Images.Add(SA);
					mNiveisImageList.Images.Add(SC);
					mNiveisImageList.Images.Add(SSC);
					mNiveisImageList.Images.Add(SR);
					mNiveisImageList.Images.Add(SSR);
					mNiveisImageList.Images.Add(D);
					mNiveisImageList.Images.Add(SD);
					mNiveisImageList.Images.Add(UF);
					mNiveisImageList.Images.Add(EDCriar);
					mNiveisImageList.Images.Add(GACriar);
					mNiveisImageList.Images.Add(ACriar);
					mNiveisImageList.Images.Add(SACriar);
					mNiveisImageList.Images.Add(SCCriar);
					mNiveisImageList.Images.Add(SSCCriar);
					mNiveisImageList.Images.Add(SRCriar);
					mNiveisImageList.Images.Add(SSRCriar);
					mNiveisImageList.Images.Add(DCriar);
					mNiveisImageList.Images.Add(SDCriar);
					mNiveisImageList.Images.Add(UFCriar);
					mNiveisImageList.Images.Add(EDEditar);
					mNiveisImageList.Images.Add(GAEditar);
					mNiveisImageList.Images.Add(AEditar);
					mNiveisImageList.Images.Add(SAEditar);
					mNiveisImageList.Images.Add(SCEditar);
					mNiveisImageList.Images.Add(SSCEditar);
					mNiveisImageList.Images.Add(SREditar);
					mNiveisImageList.Images.Add(SSREditar);
					mNiveisImageList.Images.Add(DEditar);
					mNiveisImageList.Images.Add(SDEditar);
					mNiveisImageList.Images.Add(UFEditar);
					mNiveisImageList.Images.Add(EDEliminar);
					mNiveisImageList.Images.Add(GAEliminar);
					mNiveisImageList.Images.Add(AEliminar);
					mNiveisImageList.Images.Add(SAEliminar);
					mNiveisImageList.Images.Add(SCEliminar);
					mNiveisImageList.Images.Add(SSCEliminar);
					mNiveisImageList.Images.Add(SREliminar);
					mNiveisImageList.Images.Add(SSREliminar);
					mNiveisImageList.Images.Add(DEliminar);
					mNiveisImageList.Images.Add(SDEliminar);
					mNiveisImageList.Images.Add(UFEliminar);
					mNiveisImageList.Images.Add(EDControloAut);
					mNiveisImageList.Images.Add(GAControloAut);
					mNiveisImageList.Images.Add(AControloAut);
					mNiveisImageList.Images.Add(SAControloAut);
					mNiveisImageList.Images.Add(SCControloAut);
					mNiveisImageList.Images.Add(SSCControloAut);
				}
				return mNiveisImageList;
			}
		}

		private const int NiveisImageListOffset = 2;

		public int NivelImageIncognito() 
		{
			return 0;
		}

		public int NivelImageIncognitoControloAut() 
		{
			return 1;
		}

		public int NivelImageBase(int idx)
		{
			if (idx >= 1 & idx <= 11)
			{
				return NiveisImageListOffset + 0 + idx - 1;
			}
			return -1;
		}
		public int NivelImageCriar(int idx)
		{
			if (idx >= 1 & idx <= 11)
			{
				return NiveisImageListOffset + 11 + idx - 1;
			}
			return -1;
		}
		public int NivelImageEditar(int idx)
		{
			if (idx >= 1 & idx <= 11)
			{
				return NiveisImageListOffset + 22 + idx - 1;
			}
			return -1;
		}
		public int NivelImageEliminar(int idx)
		{
			if (idx >= 1 & idx <= 11)
			{
				return NiveisImageListOffset + 33 + idx - 1;
			}
			return -1;
		}
		public int NivelImageControloAut(int idx)
		{
			if (idx >= 1 & idx <= 6)
			{
				return NiveisImageListOffset + 44 + idx - 1;
			}
			return -1;
		}
		#endregion

        #region Estatísticas
        ImageList mEstatiscas = null;
        public ImageList Estatiscas
        {
            get
            {
                if (mEstatiscas == null)
                {
                    mEstatiscas = new ImageList();
                    mEstatiscas.TransparentColor = GlobalTransparentColor;
                    mEstatiscas.Images.Add(Copy);
                }
                return mEstatiscas;
            }
        }

        string[] mEstatisticasStrings = new string[1];
        public string[] EstatisticasStrings
        {
            get
            {
                if (mEstatisticasStrings[0] == null)
                {
                    mEstatisticasStrings[0] = "Copiar estatísticas";
                }
                return mEstatisticasStrings;
            }
        }
        #endregion

        #region Gestão de depósitos

        ImageList mDEPImageList = null;
        public ImageList DEPImageList
        {
            get
            {
                if (mDEPImageList == null)
                {
                    mDEPImageList = new ImageList();
                    mDEPImageList.TransparentColor = GlobalTransparentColor;
                    mDEPImageList.Images.Add(Filtro);
                    mDEPImageList.Images.Add(Actualizar);
                }
                return mDEPImageList;
            }
        }

        string[] mDEPStrings = new string[2];
        public string[] DEPStrings
        {
            get
            {
                if (mDEPStrings[0] == null)
                {
                    mCAManipulacaoStrings[0] = GetString("FiltrarDados");
                    mCAManipulacaoStrings[1] = GetString("Actualizar");
                }
                return mCAManipulacaoStrings;
            }
        }

        #endregion

        #region Icons para a manipulacao de depósitos
        private Bitmap mDepositosCriar;
        public Bitmap DepositosCriar
        {
            get
            {
                return EnsureLoaded("DepositosCriar.png", ref mDepositosCriar);
            }
        }

        private Bitmap mDepositosEditar;
        public Bitmap DepositosEditar
        {
            get
            {
                return EnsureLoaded("DepositosEditar.png", ref mDepositosEditar);
            }
        }

        private Bitmap mDepositosEliminar;
        public Bitmap DepositosEliminar
        {
            get
            {
                return EnsureLoaded("DepositosEliminar.png", ref mDepositosEliminar);
            }
        }

        ImageList mDepositosManipulacaoImageList = null;
        public ImageList DepositosManipulacaoImageList
        {
            get
            {
                if (mDepositosManipulacaoImageList == null)
                {
                    mDepositosManipulacaoImageList = new ImageList();
                    mDepositosManipulacaoImageList.TransparentColor = GlobalTransparentColor;
                    mDepositosManipulacaoImageList.Images.Add(DepositosCriar);
                    mDepositosManipulacaoImageList.Images.Add(DepositosEditar);
                    mDepositosManipulacaoImageList.Images.Add(DepositosEliminar);
                }
                return mDepositosManipulacaoImageList;
            }
        }

        string[] mDepositosManipulacaoStrings = new string[3];
        public string[] DepositosManipulacaoStrings
        {
            get
            {
                if (mDepositosManipulacaoStrings[0] == null)
                {
                    mDepositosManipulacaoStrings[0] = GetString("DepositosCriar");
                    mDepositosManipulacaoStrings[1] = GetString("DepositosEditar");
                    mDepositosManipulacaoStrings[2] = GetString("DepositosEliminar");
                }
                return mDepositosManipulacaoStrings;
            }
        }
        #endregion

        #region Repositório Digital

        private string mAdicionarUrl;
        public string AdicionarUrl
        {
            get
            {
                return EnsureLoaded("AdicionarUrl", ref mAdicionarUrl);
            }
        }

        private string mAdicionarUrlLabel;
        public string AdicionarUrlLabel
        {
            get
            {
                return EnsureLoaded("AdicionarUrlLabel", ref mAdicionarUrlLabel);
            }
        }

        private Bitmap qualidadeBaixa;
        public Bitmap ImagemQualidadeBaixa {
            get {
                return EnsureLoaded("QualidadeBaixa.png", ref qualidadeBaixa);
            }
        }

        private Bitmap qualidadeMedia;
        public Bitmap ImagemQualidadeMedia {
            get {
                return EnsureLoaded("QualidadeMedia.png", ref qualidadeMedia);
            }
        }

        private Bitmap qualidadeAlta;
        public Bitmap ImagemQualidadeAlta {
            get {
                return EnsureLoaded("QualidadeAlta.png", ref qualidadeAlta);
            }
        }

        private Bitmap imagemPdf;
        public Bitmap ImagemPdf {
            get {
                return EnsureLoaded("Pdf.png", ref imagemPdf);
            }
        }

        private Bitmap repositorio;
        public Bitmap Repositorio {
            get {
                return EnsureLoaded("Repositorio.png", ref repositorio);
            }
        }


        ImageList fedoraImageList = null;
        public ImageList FedoraImageList {
            get {
                if (fedoraImageList == null) {
                    fedoraImageList = new ImageList();
                    fedoraImageList.Images.Add(ImagemPdf);
                    fedoraImageList.Images.Add(ImagemQualidadeAlta);
                    fedoraImageList.Images.Add(ImagemQualidadeMedia);
                    fedoraImageList.Images.Add(ImagemQualidadeBaixa);
                }
                return fedoraImageList;
            }
        }

        #endregion

        #region EAD
        private Bitmap mGenEAD;
        public Bitmap GenEAD {
            get {
                return EnsureLoaded("ead_16_16_0.bmp", ref mGenEAD);
            }
        }

        private string mGenEADString;
        public string GenEADString {
            get {
                return EnsureLoaded("GenEADString", ref mGenEADString);
            }
        }

        #endregion

        #region Importação
        private Bitmap mImport;
        public Bitmap Import
        {
            get
            {
                return EnsureLoaded("import-document.png", ref mImport);
            }
        }

        private string mImportString;
        public string ImportString
        {
            get
            {
                return EnsureLoaded("import-document", ref mImportString);
            }
        }

        #endregion

        #region Exportação
        private Bitmap mExport;
        public Bitmap Export
        {
            get
            {
                return EnsureLoaded("export-document.png", ref mExport);
            }
        }

        private string mExportString;
        public string ExportString
        {
            get
            {
                return EnsureLoaded("export-document", ref mExportString);
            }
        }

        #endregion

        #region Requisições/Devoluções
        private string mRequisicaoNome1String;
        public string RequisicaoNome1String
        {
            get {return EnsureLoaded("RequisicaoNome1String", ref mRequisicaoNome1String);}
        }
        private string mRequisicaoNome2String;
        public string RequisicaoNome2String
        {
            get { return EnsureLoaded("RequisicaoNome2String", ref mRequisicaoNome2String); }
        }
        private string mRequisicaoCatCodeString;
        public string RequisicaoCatCodeString
        {
            get { return EnsureLoaded("RequisicaoCatCodeString", ref mRequisicaoCatCodeString); }
        }

        private string mDevolucaoNome1String;
        public string DevolucaoNome1String
        {
            get { return EnsureLoaded("DevolucaoNome1String", ref mDevolucaoNome1String); }
        }
        private string mDevolucaoNome2String;
        public string DevolucaoNome2String
        {
            get { return EnsureLoaded("DevolucaoNome2String", ref mDevolucaoNome2String); }
        }
        private string mDevolucaoCatCodeString;
        public string DevolucaoCatCodeString
        {
            get { return EnsureLoaded("DevolucaoCatCodeString", ref mDevolucaoCatCodeString); }
        }
        #endregion

        #region Integracao
        private Bitmap mEntidadeProdutora;
        public Bitmap EntidadeProdutora { get { return EnsureLoaded("creator_16.png", ref mEntidadeProdutora); } }

        private Bitmap mGeografico;
        public Bitmap Geografico { get { return EnsureLoaded("geographic_16.png", ref mGeografico); } }

        private Bitmap mIdeografico;
        public Bitmap Ideografico { get { return EnsureLoaded("subject_16.png", ref mIdeografico); } }

        private Bitmap mOnomastico;
        public Bitmap Onomastico { get { return EnsureLoaded("onomastic_16.png", ref mOnomastico); } }

        private Bitmap mTipologia;
        public Bitmap Tipologia { get { return EnsureLoaded("form_16.png", ref mTipologia); } }

        private Bitmap mOverlayAdd;
        public Bitmap OverlayAdd { get { return EnsureLoaded("overlay_add.png", ref mOverlayAdd); } }

        private Bitmap mOverlayEdit;
        public Bitmap OverlayEdit { get { return EnsureLoaded("overlay_edit.png", ref mOverlayEdit); } }

        private Bitmap mOverlayAddGreen;
        public Bitmap OverlayAddGreen { get { return EnsureLoaded("overlay_add_green.png", ref mOverlayAddGreen); } }

        private Bitmap mOverlayAddYellow;
        public Bitmap OverlayAddYellow { get { return EnsureLoaded("overlay_add_yellow.png", ref mOverlayAddYellow); } }

        private Bitmap mOverlayEditGreen;
        public Bitmap OverlayEditGreen { get { return EnsureLoaded("overlay_edit_green.png", ref mOverlayEditGreen); } }

        private Bitmap mOverlayEditYellow;
        public Bitmap OverlayEditYellow { get { return EnsureLoaded("overlay_edit_yellow.png", ref mOverlayEditYellow); } }

        private Bitmap mRelation;
        public Bitmap Relation { get { return EnsureLoaded("relation.png", ref mRelation); } }

        private Bitmap mProperty;
        public Bitmap Property { get { return EnsureLoaded("property.png", ref mProperty); } }

        public Bitmap RelationUserAdd { get { return MakeOverlay(Relation, OverlayAddYellow); } }
        public Bitmap RelationUserChange { get { return MakeOverlay(Relation, OverlayEditYellow); } }
        public Bitmap RelationSuggestionAdd { get { return MakeOverlay(Relation, OverlayAddGreen); } }
        public Bitmap RelationSuggestionChange { get { return MakeOverlay(Relation, OverlayEditGreen); } }

        public Bitmap PropertyUserAdd { get { return MakeOverlay(Property, OverlayAddYellow); } }
        public Bitmap PropertyUserChange { get { return MakeOverlay(Property, OverlayEditYellow); } }
        public Bitmap PropertySuggestionAdd { get { return MakeOverlay(Property, OverlayAddGreen); } }
        public Bitmap PropertySuggestionChange { get { return MakeOverlay(Property, OverlayEditGreen); } }

        // -----------------------------------------
        // |  OverlayAddGreen  | OverlayEditGreen  |
        // |-------------------|-------------------|
        // | OverlayAddYellow  | OverlayEditYellow |
        // -----------------------------------------
        private Bitmap[][] mStateIcons;
        public Bitmap[][] StateIcons
        {
            get
            {
                if (mStateIcons == null)
                {
                    mStateIcons = new Bitmap[][] { new Bitmap[] { OverlayAddGreen, OverlayAddYellow }, new Bitmap[] { OverlayEditGreen, OverlayEditYellow } };
                }
                return mStateIcons;
            }
        }

        private List<Bitmap> mEntidadesImageList;
        public List<Bitmap> EntidadesImageList
        {
            get
            {
                if (mEntidadesImageList == null)
                {
                    mEntidadesImageList = new List<Bitmap>();
                    mEntidadesImageList.Add(SD);
                    mEntidadesImageList.Add(D);
                    mEntidadesImageList.Add(SR);
                    mEntidadesImageList.Add(SSR);
                    mEntidadesImageList.Add(EntidadeProdutora);
                    mEntidadesImageList.Add(Onomastico);
                    mEntidadesImageList.Add(Ideografico);
                    mEntidadesImageList.Add(Geografico);
                    mEntidadesImageList.Add(Tipologia);
                }
                return mEntidadesImageList;
            }
        }

        private List<Bitmap> mEntidadesRelacaoImageList;
        public List<Bitmap> EntidadesRelacaoImageList
        {
            get
            {
                if (mEntidadesRelacaoImageList == null)
                {
                    mEntidadesRelacaoImageList = new List<Bitmap>();
                    mEntidadesRelacaoImageList.Add(Relation); // original
                    mEntidadesRelacaoImageList.Add(RelationSuggestionAdd);
                    mEntidadesRelacaoImageList.Add(RelationSuggestionChange);
                    mEntidadesRelacaoImageList.Add(RelationUserAdd);
                    mEntidadesRelacaoImageList.Add(RelationUserChange);
                }
                return mEntidadesRelacaoImageList;
            }
        }

        private List<Bitmap> mEntidadesPropriedadeOpcapImageList;
        public List<Bitmap> EntidadesPropriedadeOpcapImageList
        {
            get
            {
                if (mEntidadesPropriedadeOpcapImageList == null)
                {
                    mEntidadesPropriedadeOpcapImageList = new List<Bitmap>();
                    mEntidadesPropriedadeOpcapImageList.Add(Property); // original
                    mEntidadesPropriedadeOpcapImageList.Add(PropertyUserAdd);
                    mEntidadesPropriedadeOpcapImageList.Add(PropertyUserChange);
                    mEntidadesPropriedadeOpcapImageList.Add(PropertySuggestionAdd);
                    mEntidadesPropriedadeOpcapImageList.Add(PropertySuggestionChange);
                }
                return mEntidadesPropriedadeOpcapImageList;
            }
        }

        private List<Bitmap> mEntidadesEstadoImageList;
        public List<Bitmap> EntidadesEstadoImageList
        {
            get
            {
                if (mEntidadesEstadoImageList == null)
                {
                    mEntidadesEstadoImageList = new List<Bitmap>();
                    mEntidadesEstadoImageList.Add(OverlayAdd); // novo
                    mEntidadesEstadoImageList.Add(OverlayEdit); // alterado
                }
                return mEntidadesEstadoImageList;
            }
        }
        #endregion

        #region Navegação no thesaurus
        private Bitmap mNavThesaurusIn;
        public Bitmap NavThesaurusIn { get { return EnsureLoaded("navegador_thesaurus_in.png", ref mNavThesaurusIn); } }

        private Bitmap mNavThesaurusOut;
        public Bitmap NavThesaurusOut { get { return EnsureLoaded("navegador_thesaurus_out.png", ref mNavThesaurusOut); } }

        private ImageList mNavThesaurusImageList;
        public ImageList NavThesaurusImageList
        {
            get
            {
                if (mNavThesaurusImageList == null)
                {
                    mNavThesaurusImageList = new ImageList();
                    mNavThesaurusImageList.Images.Add(NavThesaurusIn);
                    mNavThesaurusImageList.Images.Add(NavThesaurusOut);
                }
                return mNavThesaurusImageList;
            }
        }

        string[] mNavThesaurusStrings = new string[2];
        public string[] NavThesaurusStrings
        {
            get
            {
                if (mNavThesaurusStrings[0] == null)
                {
                    mNavThesaurusStrings[0] = GetString("navegador_thesaurus_in");
                    mNavThesaurusStrings[1] = GetString("navegador_thesaurus_out");
                }
                return mNavThesaurusStrings;
            }
        }
        #endregion

        #region Estado ODs PDF
        private Bitmap mLimparGerados;
        public Bitmap LimparGerados
        {
            get
            {
                return EnsureLoaded("LimparProcessados.png", ref mLimparGerados);
            }
        }

        private string mLimparGeradosString;
        public string LimparGeradosString
        {
            get
            {
                return EnsureLoaded("LimparProcessados", ref mLimparGeradosString);
            }
        }

        ImageList mStatusODImageList = null;
        public ImageList StatusODImageList
        {
            get
            {
                if (mStatusODImageList == null)
                {
                    mStatusODImageList = new ImageList();
                    mStatusODImageList.TransparentColor = GlobalTransparentColor;
                    mStatusODImageList.Images.Add(Actualizar);
                    mStatusODImageList.Images.Add(LimparGerados);
                }
                return mStatusODImageList;
            }
        }

        string[] mStatusODImageListStrings = new string[2];
        public string[] StatusODImageListStrings
        {
            get
            {
                if (mStatusODImageListStrings[0] == null)
                {
                    mStatusODImageListStrings[0] = "Actualizar lista";
                    mStatusODImageListStrings[1] = LimparGeradosString;
                }
                return mStatusODImageListStrings;
            }
        }
        #endregion
    }
}
