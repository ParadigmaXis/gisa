using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using GISA.Fedora.FedoraHandler;
using GISA.Model;
using System.Reflection;
using GISA.SharedResources;
using GISA.Search;
using GISA.GUIHelper;

namespace GISA
{
	public class frmMain : System.Windows.Forms.Form
	{
		internal frmMain()
		{
			InitializeComponent();
		}

		public class ContextChangedEventArgs
		{
			private bool mContextChangedEventArgs = false;
			public bool Cancel
			{
				get {return mContextChangedEventArgs;}
				set {mContextChangedEventArgs = value;}
			}
		}

		public class Context
		{
			private ContextState mState;
			private ContextState mLastState;

	#region  manutenção do último estado conhecido antes do actual 
			public void saveState()
			{
				mLastState = mState;
			}

			public void restoreState()
			{
				mState = mLastState;
                mLastState = new ContextState();
			}
	#endregion

			private struct ContextState
			{
				public GISADataset.NivelRow mNivelEstrututalDocumental;
				public bool mIsNivelEstrututalDocumentalDeleted;
				public GISADataset.NivelRow mPermissoesNivel;
				public bool mIsPermissoesNivelDeleted;
                public GISADataset.TrusteeRow mPermissoesTrustee;
                public bool mIsPermissoesTrusteeDeleted;
				public GISADataset.NivelRow mNivelUnidadeFisica;
				public bool mIsNivelUnidadeFisicaDeleted;
				public GISADataset.ControloAutDicionarioRow mControloAutDicionario;
				public bool mIsControloAutDicionarioDeleted;
				public GISADataset.TrusteeRow mTrustee;
				public bool mIsTrusteeDeleted;
				public GISADataset.TipoFunctionRow mTipoFunction;
				public bool mIsTipoFunctionDeleted;
				public GISADataset.NivelRow mNivel;
				public bool mIsNivelDeleted;
                public GISADataset.AutoEliminacaoRow mAutoEliminacao;
                public bool mIsAutoEliminacaoDeleted;
                public GISADataset.MovimentoRow mMovimento;
                public bool mIsMovimentoDeleted;
                public GISADataset.DepositoRow mDeposito;
                public bool mIsDepositoDeleted;
                public GISADataset.NivelRow mFedoraNivel;
                public bool mIsFedoraNivelDeleted;
                public ObjDigital mObjetoDigital;
                public ObjDigital mObjetoDigitalUpper;
                public GISADataset.NivelRow mPermissoesNivelObjDigital;
                public bool mIsPermissoesNivelObjDigitalDeleted;
                public GISADataset.TrusteeRow mPermissoesTrusteeObjDigital;
                public bool mIsPermissoesTrusteeObjDigitalDeleted;
                public GISADataset.NivelRow mGrupoArquivo;
                public bool mIsGrupoArquivoDeleted;
			}

			public void clear()
			{
				mState.mNivelEstrututalDocumental = null;
				mState.mIsNivelEstrututalDocumentalDeleted = false;
				mState.mPermissoesNivel = null;
				mState.mIsPermissoesNivelDeleted = false;
				mState.mControloAutDicionario = null;
				mState.mIsControloAutDicionarioDeleted = false;
				mState.mNivelUnidadeFisica = null;
				mState.mIsNivelUnidadeFisicaDeleted = false;
				mState.mTrustee = null;
				mState.mIsTrusteeDeleted = false;
				mState.mTipoFunction = null;
				mState.mIsTipoFunctionDeleted = false;
				mState.mNivel = null;
				mState.mIsNivelDeleted = false;
                mState.mAutoEliminacao = null;
                mState.mIsAutoEliminacaoDeleted = false;
                mState.mMovimento = null;
                mState.mIsMovimentoDeleted = false;
                mState.mDeposito = null;
                mState.mIsDepositoDeleted = false;
                mState.mFedoraNivel = null;
                mState.mIsFedoraNivelDeleted = false;
                mState.mObjetoDigital = null;
                mState.mObjetoDigitalUpper = null;
                mState.mPermissoesNivelObjDigital = null;
                mState.mIsPermissoesNivelObjDigitalDeleted = false;
                mState.mPermissoesTrusteeObjDigital = null;
                mState.mIsPermissoesTrusteeObjDigitalDeleted = false;
                mState.mGrupoArquivo = null;
                mState.mIsGrupoArquivoDeleted = false;
			}

			public delegate void NivelEstrututalDocumentalChangedEventHandler(GISAControl.SaveArgs s);
			public event NivelEstrututalDocumentalChangedEventHandler NivelEstrututalDocumentalChanged; 
			public delegate void PermissoesNivelEstrututalDocumentalChangedEventHandler(GISAControl.SaveArgs s);
			public event PermissoesNivelEstrututalDocumentalChangedEventHandler PermissoesChanged;
			public delegate void ControloAutChangedEventHandler(GISAControl.SaveArgs s);
			public event ControloAutChangedEventHandler ControloAutChanged;
			public delegate void NivelUnidadeFisicaChangedEventHandler(GISAControl.SaveArgs s);
			public event NivelUnidadeFisicaChangedEventHandler NivelUnidadeFisicaChanged; 
			public delegate void TrusteeChangedEventHandler(GISAControl.SaveArgs s);
			public event TrusteeChangedEventHandler TrusteeChanged; 
			public delegate void TipoFunctionChangedEventHandler(GISAControl.SaveArgs s);
			public event TipoFunctionChangedEventHandler TipoFunctionChanged;
			public delegate void NivelChangedEventHandler(GISAControl.SaveArgs s);
			public event NivelChangedEventHandler NivelChanged;
			public event EventHandler<RegisterModificationEventArgs> AddRevisionEvent;
            public delegate void AutoEliminacaoChangedEventHandler(GISAControl.SaveArgs s);
            public event AutoEliminacaoChangedEventHandler AutoEliminacaoChanged;
            public delegate void MovimentoChangedEventHandler(GISAControl.SaveArgs s);
            public event MovimentoChangedEventHandler MovimentoChanged;
            public delegate void DepositoChangedEventHandler(GISAControl.SaveArgs s);
            public event DepositoChangedEventHandler DepositoChanged;
            public delegate void FedoraNivelEstrututalDocumentalChangedEventHandler(GISAControl.SaveArgs s);
            public event FedoraNivelEstrututalDocumentalChangedEventHandler FedoraNivelChanged;
            public delegate void PermissoesObjDigitalChangedEventHandler(GISAControl.SaveArgs s);
            public event PermissoesObjDigitalChangedEventHandler PermissoesObjDigitalChanged;
            public event GrupoArquivoChangedEventHandler GrupoArquivoChanged;
            public delegate void GrupoArquivoChangedEventHandler(GISAControl.SaveArgs s);

			public void RaiseRegisterModificationEvent(DataRow context)
			{
                if (this.AddRevisionEvent != null)
                    AddRevisionEvent(null, new RegisterModificationEventArgs(context));
			}

	#region  Niveis 
			public GISADataset.NivelRow NivelEstrututalDocumental
			{
				get {return mState.mNivelEstrututalDocumental;}
			}

			public bool IsNivelEstrututalDocumentalDeleted
			{
				get {return mState.mIsNivelEstrututalDocumentalDeleted;}
			}


			public bool SetNivelEstrututalDocumental(GISADataset.NivelRow nRow)
			{
				return SetNivelEstrututalDocumental(nRow, false);
			}

			public bool SetNivelEstrututalDocumental(GISADataset.NivelRow nRow, bool nvlDeleted)
			{
				// Se mudarmos para um contexto que não tenha uma FRD
				if (nRow != null && nRow.RowState != DataRowState.Detached && nRow.TipoNivelRow.ID == TipoNivel.LOGICO)
				{
					nRow = null;
					nvlDeleted = false;
				}

				GISADataset.NivelRow oldNivelRow = mState.mNivelEstrututalDocumental;
				bool oldIsNivelDeleted = mState.mIsNivelEstrututalDocumentalDeleted;
				mState.mNivelEstrututalDocumental = nRow;
				mState.mIsNivelEstrututalDocumentalDeleted = nvlDeleted;
				// Se mudarmos entre dois contextos vazios ou se mudarmos entre dois contextos apagados não queremos despoletar o evento
				if (! ((nRow == null && oldNivelRow == null) || ((nRow != null && nRow.RowState == DataRowState.Detached) && (oldNivelRow != null && oldNivelRow.RowState == DataRowState.Detached) || (((nRow != null && ! (nRow.RowState == DataRowState.Detached)) && (oldNivelRow != null && ! (oldNivelRow.RowState == DataRowState.Detached))) && nRow == oldNivelRow))))
				{
                    MultiPanelControl.SaveArgs successfulSave = new MultiPanelControl.SaveArgs(PersistencyHelper.SaveResult.successful);
					try
					{
						if (NivelEstrututalDocumentalChanged != null)
							NivelEstrututalDocumentalChanged(successfulSave);

                        if (successfulSave.save == PersistencyHelper.SaveResult.unsuccessful)
						{
							mState.mNivelEstrututalDocumental = oldNivelRow;
							mState.mIsNivelEstrututalDocumentalDeleted = oldIsNivelDeleted;
						}
						ReportAction("Context.DocumentLevelNivel");
					}                 
					catch (InvalidOperationException)
					{
						Trace.WriteLine("Error. Restoring previous nivel.");
						mState.mNivelEstrututalDocumental = oldNivelRow;
						mState.mIsNivelEstrututalDocumentalDeleted = oldIsNivelDeleted;
						throw;
					}

                    // se unsuccessful retorna false, senão retorna true
                    return successfulSave.save != PersistencyHelper.SaveResult.unsuccessful;
				}
				else
					return true;
			}
	#endregion

	#region  Unidades físicas 
			public GISADataset.NivelRow NivelUnidadeFisica
			{
				get
				{
					return mState.mNivelUnidadeFisica;
				}
			}

			public bool IsNivelUnidadeFisicaDeleted
			{
				get
				{
					return mState.mIsNivelUnidadeFisicaDeleted;
				}
			}


			public bool SetNivelUnidadeFisica(GISADataset.NivelRow nRow, bool nvlDeleted)
			{
				return SetNivelUnidadeFisica(nRow, nvlDeleted, false);
			}

			public bool SetNivelUnidadeFisica(GISADataset.NivelRow nRow)
			{
				return SetNivelUnidadeFisica(nRow, false, false);
			}

			public bool SetNivelUnidadeFisica(GISADataset.NivelRow nRow, bool nvlDeleted, bool multiselection)
			{
				if (multiselection)
				{
					return true;
				}
				if (mState.mNivelUnidadeFisica == nRow && ! nvlDeleted ^ this.mState.mIsNivelUnidadeFisicaDeleted)
				{
					if (NivelUnidadeFisicaChanged != null)
						NivelUnidadeFisicaChanged(null);
					return false;
				}
				GISADataset.NivelRow oldNivelUnidadeFisicaRow = mState.mNivelUnidadeFisica;
				bool oldIsNivelUnidadeFisicaDeleted = mState.mIsNivelUnidadeFisicaDeleted;

				mState.mNivelUnidadeFisica = nRow;
				mState.mIsNivelUnidadeFisicaDeleted = nvlDeleted;
				try
				{
					MultiPanelControl.SaveArgs successfulSave = new MultiPanelControl.SaveArgs(PersistencyHelper.SaveResult.successful);
					if (NivelUnidadeFisicaChanged != null)
						NivelUnidadeFisicaChanged(successfulSave);
                    if (successfulSave.save == PersistencyHelper.SaveResult.unsuccessful)
					{
						mState.mNivelUnidadeFisica = oldNivelUnidadeFisicaRow;
						mState.mIsNivelUnidadeFisicaDeleted = oldIsNivelUnidadeFisicaDeleted;
					}
					ReportAction("Context.NivelUnidadeFisica");
                    return successfulSave.save != PersistencyHelper.SaveResult.unsuccessful;
				}
				catch (InvalidOperationException)
				{
					Trace.WriteLine("Error. Restoring previous UF.");
					mState.mNivelUnidadeFisica = oldNivelUnidadeFisicaRow;
					mState.mIsNivelUnidadeFisicaDeleted = oldIsNivelUnidadeFisicaDeleted;
					throw;
				}
			}
	#endregion

	#region  Controlo de autoridade 
			public GISADataset.ControloAutDicionarioRow ControloAutDicionario
			{
				get
				{
					return mState.mControloAutDicionario;
				}
			}

			public GISADataset.ControloAutRow ControloAut
			{
				get
				{
					if (mState.mControloAutDicionario == null)
					{
						return null;
					}
					else
					{
						return mState.mControloAutDicionario.ControloAutRow;
					}
				}
			}

			private bool IsControloAutDicionarioDeleted
			{
				get
				{
					return mState.mIsControloAutDicionarioDeleted;
				}
			}


			public bool SetControloAutDicionario(GISADataset.ControloAutDicionarioRow cadRow)
			{
				return SetControloAutDicionario(cadRow, false);
			}

			public bool SetControloAutDicionario(GISADataset.ControloAutDicionarioRow cadRow, bool cadDeleted)
			{
				GISADataset.ControloAutRow caRow = null;
				GISADataset.ControloAutDicionarioRow lastCadRowSelected = mState.mControloAutDicionario;
				if (cadRow == null)
					caRow = null;
				else
					caRow = cadRow.ControloAutRow;
				
                if (cadRow == null && ControloAutDicionario == null)
				{
				}
				else if ((cadRow != null && cadRow.RowState == DataRowState.Detached) && (ControloAutDicionario != null && ControloAutDicionario.RowState == DataRowState.Detached) || (((cadRow != null && ! (cadRow.RowState == DataRowState.Detached)) && (ControloAutDicionario != null && ! (ControloAutDicionario.RowState == DataRowState.Detached))) && cadRow.ControloAutRow == ControloAutDicionario.ControloAutRow))
				{

					// verificar se apesar de não ter existido mudança do controlo de autoridade selecionado
					// foi alterada a selecção sobre uma das suas formas
					if (! (cadRow == ControloAutDicionario))
					{
						mState.mControloAutDicionario = cadRow;
						return true;
					}
					else if (cadRow == ControloAutDicionario)
					{
						return true;
					}
					else
					{
						if (ControloAutChanged != null)
							ControloAutChanged(null);
						return false;
					}

				}
				mState.mControloAutDicionario = cadRow;
				mState.mIsControloAutDicionarioDeleted = cadDeleted;
                MultiPanelControl.SaveArgs successfulSave = new MultiPanelControl.SaveArgs(PersistencyHelper.SaveResult.successful);
				if (ControloAutChanged != null)
					ControloAutChanged(successfulSave);
				ReportAction("Context.ControloAut");
                if (successfulSave.save == PersistencyHelper.SaveResult.unsuccessful)
				{
					mState.mControloAutDicionario = lastCadRowSelected;
				}
                return successfulSave.save != PersistencyHelper.SaveResult.unsuccessful;
			}
	#endregion

	#region  Trustees 
			public GISADataset.TrusteeRow Trustee
			{
				get
				{
					return mState.mTrustee;
				}
			}

			public bool IsTrusteeDeleted
			{
				get
				{
					return mState.mIsTrusteeDeleted;
				}
			}


			public bool SetTrustee(GISADataset.TrusteeRow tRow)
			{
				return SetTrustee(tRow, false);
			}

//INSTANT C# NOTE: C# does not support optional parameters. Overloaded method(s) are created above.
//ORIGINAL LINE: Public Function SetTrustee(ByVal tRow As GISADataset.TrusteeRow, Optional ByVal tDeleted As Boolean = false) As Boolean
			public bool SetTrustee(GISADataset.TrusteeRow tRow, bool tDeleted)
			{
				if (mState.mTrustee == tRow && ! tDeleted ^ mState.mIsTrusteeDeleted)
				{
					return false;
				}
				GISADataset.TrusteeRow lastTrusteeSelected = mState.mTrustee;
				mState.mTrustee = tRow;
				mState.mIsTrusteeDeleted = tDeleted;

                List<long> niveisIDs = new List<long>();
                foreach (GISADataset.TrusteeNivelPrivilegeRow tnpRow in GisaDataSetHelper.GetInstance().TrusteeNivelPrivilege.Select("", "", DataViewRowState.Added | DataViewRowState.ModifiedOriginal))
                    niveisIDs.Add(tnpRow.IDNivel);

                MultiPanelControl.SaveArgs successfulSave = new MultiPanelControl.SaveArgs(PersistencyHelper.SaveResult.successful);                
                
				if (TrusteeChanged != null)
					TrusteeChanged(successfulSave);
				ReportAction("Context.Trustee");

                if (successfulSave.save == PersistencyHelper.SaveResult.unsuccessful)
				{
					mState.mTrustee = lastTrusteeSelected;
				}
                return successfulSave.save != PersistencyHelper.SaveResult.unsuccessful;
			}
	#endregion

	#region  Permissoes 
			public GISADataset.TipoFunctionRow TipoFunction
			{
				get
				{
					return mState.mTipoFunction;
				}
			}

			public bool IsTipoFunctionDeleted
			{
				get
				{
					return mState.mIsTipoFunctionDeleted;
				}
			}


			public bool SetTipoFunction(GISADataset.TipoFunctionRow tfRow)
			{
				return SetTipoFunction(tfRow, false);
			}

//INSTANT C# NOTE: C# does not support optional parameters. Overloaded method(s) are created above.
//ORIGINAL LINE: Public Function SetTipoFunction(ByVal tfRow As GISADataset.TipoFunctionRow, Optional ByVal tfDeleted As Boolean = false) As Boolean
			public bool SetTipoFunction(GISADataset.TipoFunctionRow tfRow, bool tfDeleted)
			{
				if (mState.mTipoFunction == tfRow && ! tfDeleted ^ mState.mIsTipoFunctionDeleted)
				{
					return false;
				}
				GISADataset.TipoFunctionRow lastTipoFunctionSelected = mState.mTipoFunction;
				mState.mTipoFunction = tfRow;
				mState.mIsTipoFunctionDeleted = tfDeleted;
                MultiPanelControl.SaveArgs successfulSave = new MultiPanelControl.SaveArgs(PersistencyHelper.SaveResult.successful);
				if (TipoFunctionChanged != null)
					TipoFunctionChanged(successfulSave);
				ReportAction("Context.TipoFunction");

                if (successfulSave.save == PersistencyHelper.SaveResult.unsuccessful)
				{
					mState.mTipoFunction = lastTipoFunctionSelected;
				}
                return successfulSave.save != PersistencyHelper.SaveResult.unsuccessful;
			}


			public GISADataset.NivelRow Nivel
			{
				get
				{
					return mState.mNivel;
				}
			}


			public bool IsNivelDeleted
			{
				get
				{
					return mState.mIsNivelDeleted;
				}
			}


			public bool SetNivel(GISADataset.NivelRow nRow)
			{
				return SetNivel(nRow, false);
			}

//INSTANT C# NOTE: C# does not support optional parameters. Overloaded method(s) are created above.
//ORIGINAL LINE: Public Function SetNivel(ByVal nRow As GISADataset.NivelRow, Optional ByVal nvlDeleted As Boolean = false) As Boolean
			public bool SetNivel(GISADataset.NivelRow nRow, bool nvlDeleted)
			{

				// Se mudarmos para um contexto que não tenha uma FRD
				if (mState.mNivel == nRow && ! nvlDeleted ^ mState.mIsNivelDeleted)
				{
					return false;
				}

				GISADataset.NivelRow lastNivelSelected = mState.mNivel;
				mState.mNivel = nRow;
				mState.mIsNivelDeleted = nvlDeleted;
                MultiPanelControl.SaveArgs successfulSave = new MultiPanelControl.SaveArgs(PersistencyHelper.SaveResult.successful);
				if (NivelChanged != null)
					NivelChanged(successfulSave);
				ReportAction("Context.Nivel");

                if (successfulSave.save == PersistencyHelper.SaveResult.unsuccessful)
				{
					mState.mNivel = lastNivelSelected;
				}
                return successfulSave.save != PersistencyHelper.SaveResult.unsuccessful;
			}



	#endregion

	#region  Permissões Níveis 
			public GISADataset.NivelRow PermissoesNivel
			{
				get {return mState.mPermissoesNivel;}
			}

			public bool IsPermissoesNivelDeleted
			{
				get {return mState.mIsPermissoesNivelDeleted;}
			}

            public GISADataset.TrusteeRow PermissoesTrustee
            {
                get { return mState.mPermissoesTrustee; }
            }

            public bool IsPermissoesTrusteeDeleted
            {
                get { return mState.mIsPermissoesTrusteeDeleted; }
            }

			public bool SetPermissoes(GISADataset.NivelRow nRow, GISADataset.TrusteeRow tRow)
			{
				return SetPermissoes(nRow, tRow, false, false);
			}

            public bool SetPermissoes(GISADataset.NivelRow nRow, GISADataset.TrusteeRow tRow, bool nvlDeleted, bool tDeleted)
			{
				GISADataset.NivelRow oldPermissoesNivelRow = mState.mPermissoesNivel;
                GISADataset.TrusteeRow oldPermissoesTrusteeRow = mState.mPermissoesTrustee;
				bool oldPermissoesIsNivelDeleted = mState.mIsPermissoesNivelDeleted;
                bool oldPermissoesIsTrusteeDeleted = mState.mIsPermissoesTrusteeDeleted;
				mState.mPermissoesNivel = nRow;
                mState.mPermissoesTrustee = tRow;
				mState.mIsPermissoesNivelDeleted = nvlDeleted;
                mState.mIsPermissoesTrusteeDeleted = tDeleted;

				// Se mudarmos entre dois contextos vazios ou se mudarmos entre dois contextos apagados não queremos despoletar o evento
                if (!((nRow == null && oldPermissoesNivelRow == null && tRow == null && oldPermissoesTrusteeRow == null) ||
                        ((nRow != null && nRow.RowState == DataRowState.Detached && tRow != null && tRow.RowState == DataRowState.Detached)
                            && (oldPermissoesNivelRow != null && oldPermissoesNivelRow.RowState == DataRowState.Detached && oldPermissoesTrusteeRow != null && oldPermissoesTrusteeRow.RowState == DataRowState.Detached) 
                            || (((nRow != null && !(nRow.RowState == DataRowState.Detached)) && (oldPermissoesNivelRow != null && ! (oldPermissoesNivelRow.RowState == DataRowState.Detached))) && nRow == oldPermissoesNivelRow &&
                                ((tRow != null && !(tRow.RowState == DataRowState.Detached)) && (oldPermissoesTrusteeRow != null && !(oldPermissoesTrusteeRow.RowState == DataRowState.Detached))) && tRow == oldPermissoesTrusteeRow))))
				{

                    List<long> niveisIDs = new List<long>();
                    foreach (GISADataset.TrusteeNivelPrivilegeRow tnpRow in GisaDataSetHelper.GetInstance().TrusteeNivelPrivilege.Select("", "", DataViewRowState.Added | DataViewRowState.ModifiedOriginal))
                        niveisIDs.Add(tnpRow.IDNivel);

                    MultiPanelControl.SaveArgs successfulSave = new MultiPanelControl.SaveArgs(PersistencyHelper.SaveResult.successful);
                    
					try
					{
						if (PermissoesChanged != null)
							PermissoesChanged(successfulSave);
                        
                        if (successfulSave.save == PersistencyHelper.SaveResult.unsuccessful)
						{
							mState.mPermissoesNivel = oldPermissoesNivelRow;
                            mState.mPermissoesTrustee = oldPermissoesTrusteeRow;
							mState.mIsPermissoesNivelDeleted = oldPermissoesIsNivelDeleted;
                            mState.mIsPermissoesTrusteeDeleted = oldPermissoesIsTrusteeDeleted;
						}
						ReportAction("Context.DocumentLevelPermissoesNivel");
					}
					catch (InvalidOperationException)
					{
						Trace.WriteLine("Error. Restoring previous nivel.");
						mState.mPermissoesNivel = oldPermissoesNivelRow;
                        mState.mPermissoesTrustee = oldPermissoesTrusteeRow;
						mState.mIsPermissoesNivelDeleted = oldPermissoesIsNivelDeleted;
                        mState.mIsPermissoesTrusteeDeleted = oldPermissoesIsTrusteeDeleted;
						throw;
					}
                    return successfulSave.save != PersistencyHelper.SaveResult.unsuccessful;
				}
				else
				{
					return true;
				}
			}
	#endregion

    #region Depósito
            public GISADataset.DepositoRow Deposito
            {
                get { return mState.mDeposito; }
            }

            public bool IsDepositoDeleted
            {
                get { return mState.mIsDepositoDeleted; }
            }

            public bool SetDeposito(GISADataset.DepositoRow depRow)
            {
                return SetDeposito(depRow, false);
            }

            public bool SetDeposito(GISADataset.DepositoRow depRow, bool depDeleted)
            {
                if (mState.mDeposito == depRow && !depDeleted ^ this.mState.mIsDepositoDeleted)
                {
                    if (DepositoChanged != null)
                        DepositoChanged(null);
                    return false;
                }
                GISADataset.DepositoRow oldDepositoRow = mState.mDeposito;
                bool oldIsDepositoDeleted = mState.mIsDepositoDeleted;

                mState.mDeposito = depRow;
                mState.mIsDepositoDeleted = depDeleted;
                try
                {
                    MultiPanelControl.SaveArgs successfulSave = new MultiPanelControl.SaveArgs(PersistencyHelper.SaveResult.successful);
                    if (DepositoChanged != null)
                        DepositoChanged(successfulSave);
                    if (successfulSave.save == PersistencyHelper.SaveResult.unsuccessful)
                    {
                        mState.mDeposito = oldDepositoRow;
                        mState.mIsDepositoDeleted = oldIsDepositoDeleted;
                    }
                    ReportAction("Context.Deposito");
                    return successfulSave.save != PersistencyHelper.SaveResult.unsuccessful;
                }
                catch (InvalidOperationException)
                {
                    Trace.WriteLine("Error. Restoring previous AE.");
                    mState.mDeposito = oldDepositoRow;
                    mState.mIsDepositoDeleted = oldIsDepositoDeleted;
                    throw;
                }
            }
            #endregion

    #region Fedora
            public GISADataset.NivelRow FedoraNivel { get { return mState.mFedoraNivel; } }
            public bool IsFedoraNivelDeleted { get { return mState.mIsFedoraNivelDeleted; } }
            public ObjDigital ObjetoDigital { get { return mState.mObjetoDigital; } set { mState.mObjetoDigital = value; } }

            public bool SetFedoraNivel(GISADataset.NivelRow fedNivelRow)
            {
                return SetFedoraNivel(fedNivelRow, false);
            }

            public bool SetFedoraNivel(GISADataset.NivelRow fedNivelRow, bool fedNivelDeleted)
            {
                GISADataset.NivelRow oldFedoraNivelRow = mState.mFedoraNivel;
                bool oldIsFedoraNivelDeleted = mState.mIsFedoraNivelDeleted;
                mState.mFedoraNivel= fedNivelRow;
                mState.mIsFedoraNivelDeleted = fedNivelDeleted;

                // Se mudarmos entre dois contextos vazios ou se mudarmos entre dois contextos apagados não queremos despoletar o evento
                if (!((fedNivelRow == null && oldFedoraNivelRow == null) || ((fedNivelRow != null && fedNivelRow.RowState == DataRowState.Detached) && (oldFedoraNivelRow != null && oldFedoraNivelRow.RowState == DataRowState.Detached) || (((fedNivelRow != null && !(fedNivelRow.RowState == DataRowState.Detached)) && (oldFedoraNivelRow != null && !(oldFedoraNivelRow.RowState == DataRowState.Detached))) && fedNivelRow == oldFedoraNivelRow))))
                {
                    MultiPanelControl.SaveArgs successfulSave = new MultiPanelControl.SaveArgs(PersistencyHelper.SaveResult.successful);
                    try
                    {
                        if (FedoraNivelChanged != null)
                            FedoraNivelChanged(successfulSave);

                        if (successfulSave.save == PersistencyHelper.SaveResult.unsuccessful)
                        {
                            mState.mFedoraNivel = oldFedoraNivelRow;
                            mState.mIsFedoraNivelDeleted = oldIsFedoraNivelDeleted;
                        }
                        ReportAction("Context.DocumentLevelFedoraNivel");
                    }
                    catch (InvalidOperationException)
                    {
                        Trace.WriteLine("Error. Restoring previous fedora nivel.");
                        mState.mFedoraNivel = oldFedoraNivelRow;
                        mState.mIsFedoraNivelDeleted = oldIsFedoraNivelDeleted;
                        throw;
                    }

                    // se unsuccessful retorna false, senão retorna true
                    return successfulSave.save != PersistencyHelper.SaveResult.unsuccessful;
                }
                else
                    return true;
            }
            #endregion

    #region Permissões Objectos Digitais
            public GISADataset.NivelRow PermissoesNivelObjDigital
            {
                get { return mState.mPermissoesNivelObjDigital; }
            }

            public bool IsPermissoesNivelObjDigitalDeleted
            {
                get { return mState.mIsPermissoesNivelObjDigitalDeleted; }
            }

            public GISADataset.TrusteeRow PermissoesTrusteeObjDigital
            {
                get { return mState.mPermissoesTrusteeObjDigital; }
            }

            public bool IsPermissoesTrusteeObjDigitalDeleted
            {
                get { return mState.mIsPermissoesTrusteeObjDigitalDeleted; }
            }

            public bool SetPermissoesObjDigital(GISADataset.NivelRow nRow, GISADataset.TrusteeRow tRow)
            {
                return SetPermissoesObjDigital(nRow, tRow, false, false);
            }

            public bool SetPermissoesObjDigital(GISADataset.NivelRow nRow, GISADataset.TrusteeRow tRow, bool nvlDeleted, bool tDeleted)
            {
                GISADataset.NivelRow oldPermissoesNivelRow = mState.mPermissoesNivelObjDigital;
                GISADataset.TrusteeRow oldPermissoesTrusteeRow = mState.mPermissoesTrusteeObjDigital;
                bool oldPermissoesIsNivelDeleted = mState.mIsPermissoesNivelObjDigitalDeleted;
                bool oldPermissoesIsTrusteeDeleted = mState.mIsPermissoesTrusteeObjDigitalDeleted;
                mState.mPermissoesNivelObjDigital = nRow;
                mState.mPermissoesTrusteeObjDigital = tRow;
                mState.mIsPermissoesNivelObjDigitalDeleted = nvlDeleted;
                mState.mIsPermissoesTrusteeObjDigitalDeleted = tDeleted;

                // Se mudarmos entre dois contextos vazios ou se mudarmos entre dois contextos apagados não queremos despoletar o evento
                if (!((nRow == null && oldPermissoesNivelRow == null && tRow == null && oldPermissoesTrusteeRow == null) ||
                        ((nRow != null && nRow.RowState == DataRowState.Detached && tRow != null && tRow.RowState == DataRowState.Detached)
                            && (oldPermissoesNivelRow != null && oldPermissoesNivelRow.RowState == DataRowState.Detached && oldPermissoesTrusteeRow != null && oldPermissoesTrusteeRow.RowState == DataRowState.Detached)
                            || (((nRow != null && !(nRow.RowState == DataRowState.Detached)) && (oldPermissoesNivelRow != null && !(oldPermissoesNivelRow.RowState == DataRowState.Detached))) && nRow == oldPermissoesNivelRow &&
                                ((tRow != null && !(tRow.RowState == DataRowState.Detached)) && (oldPermissoesTrusteeRow != null && !(oldPermissoesTrusteeRow.RowState == DataRowState.Detached))) && tRow == oldPermissoesTrusteeRow))))
                {
                    MultiPanelControl.SaveArgs successfulSave = new MultiPanelControl.SaveArgs(PersistencyHelper.SaveResult.successful);

                    try
                    {
                        if (PermissoesObjDigitalChanged != null)
                            PermissoesObjDigitalChanged(successfulSave);

                        if (successfulSave.save == PersistencyHelper.SaveResult.unsuccessful)
                        {
                            mState.mPermissoesNivelObjDigital = oldPermissoesNivelRow;
                            mState.mPermissoesTrusteeObjDigital = oldPermissoesTrusteeRow;
                            mState.mIsPermissoesNivelObjDigitalDeleted = oldPermissoesIsNivelDeleted;
                            mState.mIsPermissoesTrusteeObjDigitalDeleted = oldPermissoesIsTrusteeDeleted;
                        }
                        ReportAction("Context.DocumentLevelPermissoesObjDigital");
                    }
                    catch (InvalidOperationException)
                    {
                        Trace.WriteLine("Error. Restoring previous nivel.");
                        mState.mPermissoesNivelObjDigital = oldPermissoesNivelRow;
                        mState.mPermissoesTrusteeObjDigital = oldPermissoesTrusteeRow;
                        mState.mIsPermissoesNivelObjDigitalDeleted = oldPermissoesIsNivelDeleted;
                        mState.mIsPermissoesTrusteeObjDigitalDeleted = oldPermissoesIsTrusteeDeleted;
                        throw;
                    }
                    return successfulSave.save != PersistencyHelper.SaveResult.unsuccessful;
                }
                else
                {
                    return true;
                }
            }
    #endregion

    #region Auto de eliminação
            public GISADataset.AutoEliminacaoRow AutoEliminacao
            {
                get {return mState.mAutoEliminacao;}
            }

            public bool IsAutoEliminacaoDeleted
            {
                get {return mState.mIsAutoEliminacaoDeleted;}
            }
            
            public bool SetAutoEliminacao(GISADataset.AutoEliminacaoRow aeRow)
            {
                return SetAutoEliminacao(aeRow, false);
            }

            public bool SetAutoEliminacao(GISADataset.AutoEliminacaoRow aeRow, bool aeDeleted)
            {
                if (mState.mAutoEliminacao == aeRow && !aeDeleted ^ this.mState.mIsAutoEliminacaoDeleted)
                {
                    if (AutoEliminacaoChanged != null)
                        AutoEliminacaoChanged(null);
                    return false;
                }
                GISADataset.AutoEliminacaoRow oldAutoEliminacaoRow = mState.mAutoEliminacao;
                bool oldIsAutoEliminacaoDeleted = mState.mIsAutoEliminacaoDeleted;

                mState.mAutoEliminacao = aeRow;
                mState.mIsAutoEliminacaoDeleted = aeDeleted;
                try
                {
                    MultiPanelControl.SaveArgs successfulSave = new MultiPanelControl.SaveArgs(PersistencyHelper.SaveResult.successful);
                    if (AutoEliminacaoChanged != null)
                        AutoEliminacaoChanged(successfulSave);
                    if (successfulSave.save == PersistencyHelper.SaveResult.unsuccessful)
                    {
                        mState.mAutoEliminacao = oldAutoEliminacaoRow;
                        mState.mIsAutoEliminacaoDeleted = oldIsAutoEliminacaoDeleted;
                    }
                    ReportAction("Context.AutoEliminacao");
                    return successfulSave.save != PersistencyHelper.SaveResult.unsuccessful;
                }
                catch (InvalidOperationException)
                {
                    Trace.WriteLine("Error. Restoring previous AE.");
                    mState.mAutoEliminacao = oldAutoEliminacaoRow;
                    mState.mIsAutoEliminacaoDeleted = oldIsAutoEliminacaoDeleted;
                    throw;
                }
            }
            #endregion
                        
    #region Movimento
            public GISADataset.MovimentoRow Movimento
            {
                get
                {
                    return mState.mMovimento;
                }
            }

            public bool IsMovimentoDeleted
            {
                get
                {
                    return mState.mIsMovimentoDeleted;
                }
            }


            public bool SetMovimento(GISADataset.MovimentoRow movRow)
            {
                return SetMovimento(movRow, false);
            }

            public bool SetMovimento(GISADataset.MovimentoRow movRow, bool reqDeleted)
            {
                GISADataset.MovimentoRow oldMovRow = mState.mMovimento;
                bool oldIsMovDeleted = mState.mIsMovimentoDeleted;
                mState.mMovimento = movRow;
                mState.mIsMovimentoDeleted = reqDeleted;
                // Se mudarmos entre dois contextos vazios ou se mudarmos entre dois contextos apagados não queremos despoletar o evento
                if (!((movRow == null && oldMovRow == null) || ((movRow != null && movRow.RowState == DataRowState.Detached) && (oldMovRow != null && oldMovRow.RowState == DataRowState.Detached) || (((movRow != null && !(movRow.RowState == DataRowState.Detached)) && (oldMovRow != null && !(oldMovRow.RowState == DataRowState.Detached))) && movRow == oldMovRow))))
                {
                    MultiPanelControl.SaveArgs successfulSave = new MultiPanelControl.SaveArgs(PersistencyHelper.SaveResult.successful);
                    try
                    {
                        if (MovimentoChanged != null)
                            MovimentoChanged(successfulSave);

                        if (successfulSave.save == PersistencyHelper.SaveResult.unsuccessful)
                        {
                            mState.mMovimento = oldMovRow;
                            mState.mIsMovimentoDeleted= oldIsMovDeleted;
                        }
                        ReportAction("Context.Movimento");
                    }
                    catch (InvalidOperationException)
                    {
                        Trace.WriteLine("Error. Restoring previous movimento.");
                        mState.mMovimento = oldMovRow;
                        mState.mIsMovimentoDeleted = oldIsMovDeleted;
                        throw;
                    }

                    // se unsuccessful retorna false, senão retorna true
                    return successfulSave.save != PersistencyHelper.SaveResult.unsuccessful;
                }
                else
                {                    
                    return true;
                }
            }
            #endregion

    #region Imagens Ilustracao

            public GISADataset.NivelRow GrupoArquivo
            {
                get { return mState.mGrupoArquivo; }
            }

            public bool IsGrupoArquivoDeleted
            {
                get { return mState.mIsGrupoArquivoDeleted; }
            }

            public bool SetGrupoArquivo(GISADataset.NivelRow gaRow)
            {
                return SetGrupoArquivo(gaRow, false);
            }

            public bool SetGrupoArquivo(GISADataset.NivelRow gaRow, bool gaDeleted)
            {
                if (mState.mGrupoArquivo == gaRow && !gaDeleted ^ this.mState.mIsGrupoArquivoDeleted)
                {
                    if (GrupoArquivoChanged != null)
                        GrupoArquivoChanged(null);
                    return false;
                }
                GISADataset.NivelRow oldGrupoArquivoRow = mState.mGrupoArquivo;
                bool oldIsGrupoArquivoDeleted = mState.mIsGrupoArquivoDeleted;
                mState.mGrupoArquivo = gaRow;
                mState.mIsGrupoArquivoDeleted = gaDeleted;
                if (!((gaRow == null && oldGrupoArquivoRow == null) || ((gaRow != null && gaRow.RowState == DataRowState.Detached) && (oldGrupoArquivoRow != null && oldGrupoArquivoRow.RowState == DataRowState.Detached) || (((gaRow != null && !(gaRow.RowState == DataRowState.Detached)) && (oldGrupoArquivoRow != null && !(oldGrupoArquivoRow.RowState == DataRowState.Detached))) && gaRow == oldGrupoArquivoRow))))
                {
                    try
                    {
                        MultiPanelControl.SaveArgs successfulSave = new MultiPanelControl.SaveArgs(PersistencyHelper.SaveResult.successful);
                        if (GrupoArquivoChanged != null)
                            GrupoArquivoChanged(successfulSave);
                        if (successfulSave.save == PersistencyHelper.SaveResult.unsuccessful)
                        {
                            mState.mGrupoArquivo = oldGrupoArquivoRow;
                            mState.mIsGrupoArquivoDeleted = oldIsGrupoArquivoDeleted;
                        }
                        ReportAction("Context.GrupoArquivo");
                        return successfulSave.save != PersistencyHelper.SaveResult.unsuccessful;
                    }
                    catch (InvalidOperationException)
                    {
                        Trace.WriteLine("Error. Restoring previous GA.");
                        mState.mGrupoArquivo = oldGrupoArquivoRow;
                        mState.mIsGrupoArquivoDeleted = oldIsGrupoArquivoDeleted;
                        throw;
                    }
                }
                else
                {
                    return true;
                }
            }

    #endregion

            private void ReportAction(string label)
			{
				Trace.Write(string.Format("{0} was changed. Location: {1}", label, new StackFrame(2, true).ToString()));
			}
		}

		private Context mContext;
		public Context CurrentContext
		{
			get
			{
				if (mContext == null)
					mContext = new Context();
				return mContext;
			}
		}

		private int mWaitModeCounter = 0;
        private StatusBarPanel StatusBarPanelServer;
		private Cursor mWaitModeOldCursor = null;
		public void EnterWaitMode()
		{
			if (mWaitModeCounter == 0)
			{
				mWaitModeOldCursor = this.Cursor;
				this.Cursor = Cursors.WaitCursor;
			}
			mWaitModeCounter = mWaitModeCounter + 1;
		}

		public void LeaveWaitMode()
		{
			mWaitModeCounter = mWaitModeCounter - 1;
			if (mWaitModeCounter == 0)
			{
				this.Cursor = mWaitModeOldCursor;
			}
		}

	#region  Windows Form Designer generated code 

		public frmMain(string username) : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            //outlookBar.ItemClicked += OutlookBarItemClicked;
            base.Closing += frmMain_Closing;
            StatusBar.PanelClick += StatusBar_PanelClick;
            StatusBar.DoubleClick += StatusBar_DoubleClick;
            MenuItemAbout.Click += MenuItemAbout_Click;
            lblBtnSwitchAuthor.Click += lblBtnSwitchAuthor_Click;
            lblBtnEditAuthors.Click += lblBtnEditAuthors_Click;
            serverPingTimer.Tick += serverPingTimer_Tick;
                        

			Startup(username);
		}

		//Form overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		internal System.Windows.Forms.Splitter Splitter1;
		internal System.Windows.Forms.Splitter Splitter2;
		internal System.Windows.Forms.StatusBar StatusBar;
		internal System.Windows.Forms.StatusBarPanel StatusBarPanelHint;
		internal System.Windows.Forms.StatusBarPanel StatusBarPanelUser;
		internal System.Windows.Forms.TextBox TextBox1;
		internal System.Windows.Forms.Panel PanelOutlookBar;
		internal System.Windows.Forms.Panel PanelUp;
		internal System.Windows.Forms.Panel PanelDown;
		internal LumiSoft.UI.Controls.WOutlookBar.WOutlookBar outlookBar;
		internal System.Windows.Forms.ImageList ImageList1;
		internal System.Windows.Forms.StatusBarPanel StatusBarPanelActivity;
		internal System.Windows.Forms.StatusBarPanel StatusBarPanelAutor;
		internal System.Windows.Forms.ContextMenu ContextMenuHelp;
		internal System.Windows.Forms.MenuItem MenuItemAbout;
		internal System.Windows.Forms.StatusBarPanel StatusBarPanelHelp;
		internal System.Windows.Forms.Label lblBtnEditAuthors;
		internal System.Windows.Forms.Label lblBtnSwitchAuthor;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.StatusBar = new System.Windows.Forms.StatusBar();
            this.StatusBarPanelHint = new System.Windows.Forms.StatusBarPanel();
            this.StatusBarPanelUser = new System.Windows.Forms.StatusBarPanel();
            this.StatusBarPanelAutor = new System.Windows.Forms.StatusBarPanel();
            this.StatusBarPanelHelp = new System.Windows.Forms.StatusBarPanel();
            this.StatusBarPanelActivity = new System.Windows.Forms.StatusBarPanel();
            this.StatusBarPanelServer = new System.Windows.Forms.StatusBarPanel();
            this.PanelOutlookBar = new System.Windows.Forms.Panel();
            this.Splitter1 = new System.Windows.Forms.Splitter();
            this.PanelUp = new System.Windows.Forms.Panel();
            this.Splitter2 = new System.Windows.Forms.Splitter();
            this.PanelDown = new System.Windows.Forms.Panel();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ContextMenuHelp = new System.Windows.Forms.ContextMenu();
            this.MenuItemAbout = new System.Windows.Forms.MenuItem();
            this.lblBtnEditAuthors = new System.Windows.Forms.Label();
            this.lblBtnSwitchAuthor = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.StatusBarPanelHint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusBarPanelUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusBarPanelAutor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusBarPanelHelp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusBarPanelActivity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusBarPanelServer)).BeginInit();
            this.SuspendLayout();
            // 
            // StatusBar
            // 
            this.StatusBar.Location = new System.Drawing.Point(0, 530);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.StatusBarPanelHint,
            this.StatusBarPanelUser,
            this.StatusBarPanelAutor,
            this.StatusBarPanelHelp,
            this.StatusBarPanelActivity,
            this.StatusBarPanelServer});
            this.StatusBar.ShowPanels = true;
            this.StatusBar.Size = new System.Drawing.Size(792, 23);
            this.StatusBar.SizingGrip = false;
            this.StatusBar.TabIndex = 0;
            // 
            // StatusBarPanelHint
            // 
            this.StatusBarPanelHint.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.StatusBarPanelHint.Name = "StatusBarPanelHint";
            this.StatusBarPanelHint.Width = 448;
            // 
            // StatusBarPanelUser
            // 
            this.StatusBarPanelUser.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.StatusBarPanelUser.MinWidth = 110;
            this.StatusBarPanelUser.Name = "StatusBarPanelUser";
            this.StatusBarPanelUser.Text = "Utilizador:";
            this.StatusBarPanelUser.Width = 110;
            // 
            // StatusBarPanelAutor
            // 
            this.StatusBarPanelAutor.MinWidth = 150;
            this.StatusBarPanelAutor.Name = "StatusBarPanelAutor";
            this.StatusBarPanelAutor.Text = "Autor:";
            this.StatusBarPanelAutor.Width = 150;
            // 
            // StatusBarPanelHelp
            // 
            this.StatusBarPanelHelp.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.StatusBarPanelHelp.MinWidth = 28;
            this.StatusBarPanelHelp.Name = "StatusBarPanelHelp";
            this.StatusBarPanelHelp.Width = 28;
            // 
            // StatusBarPanelActivity
            // 
            this.StatusBarPanelActivity.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.StatusBarPanelActivity.MinWidth = 28;
            this.StatusBarPanelActivity.Name = "StatusBarPanelActivity";
            this.StatusBarPanelActivity.Width = 28;
            // 
            // StatusBarPanelServer
            // 
            this.StatusBarPanelServer.MinWidth = 28;
            this.StatusBarPanelServer.Name = "StatusBarPanelServer";
            this.StatusBarPanelServer.Width = 28;
            // 
            // PanelOutlookBar
            // 
            this.PanelOutlookBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PanelOutlookBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelOutlookBar.Location = new System.Drawing.Point(0, 0);
            this.PanelOutlookBar.Name = "PanelOutlookBar";
            this.PanelOutlookBar.Size = new System.Drawing.Size(134, 530);
            this.PanelOutlookBar.TabIndex = 1;
            // 
            // Splitter1
            // 
            this.Splitter1.Location = new System.Drawing.Point(134, 0);
            this.Splitter1.Name = "Splitter1";
            this.Splitter1.Size = new System.Drawing.Size(3, 530);
            this.Splitter1.TabIndex = 2;
            this.Splitter1.TabStop = false;
            // 
            // PanelUp
            // 
            this.PanelUp.BackColor = System.Drawing.SystemColors.Control;
            this.PanelUp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PanelUp.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelUp.Location = new System.Drawing.Point(137, 0);
            this.PanelUp.Name = "PanelUp";
            this.PanelUp.Size = new System.Drawing.Size(655, 200);
            this.PanelUp.TabIndex = 3;
            // 
            // Splitter2
            // 
            this.Splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.Splitter2.Location = new System.Drawing.Point(137, 200);
            this.Splitter2.Name = "Splitter2";
            this.Splitter2.Size = new System.Drawing.Size(655, 3);
            this.Splitter2.TabIndex = 4;
            this.Splitter2.TabStop = false;
            // 
            // PanelDown
            // 
            this.PanelDown.AutoScroll = true;
            this.PanelDown.BackColor = System.Drawing.SystemColors.Control;
            this.PanelDown.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PanelDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelDown.Location = new System.Drawing.Point(137, 203);
            this.PanelDown.Name = "PanelDown";
            this.PanelDown.Size = new System.Drawing.Size(655, 327);
            this.PanelDown.TabIndex = 5;
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(296, 552);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(48, 20);
            this.TextBox1.TabIndex = 6;
            this.TextBox1.Text = "TextBox1";
            // 
            // ImageList1
            // 
            this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
            this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList1.Images.SetKeyName(0, "");
            // 
            // ContextMenuHelp
            // 
            this.ContextMenuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItemAbout});
            // 
            // MenuItemAbout
            // 
            this.MenuItemAbout.DefaultItem = true;
            this.MenuItemAbout.Index = 0;
            this.MenuItemAbout.Text = "&Acerca de...";
            // 
            // lblBtnEditAuthors
            // 
            this.lblBtnEditAuthors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBtnEditAuthors.Location = new System.Drawing.Point(662, 534);
            this.lblBtnEditAuthors.Name = "lblBtnEditAuthors";
            this.lblBtnEditAuthors.Size = new System.Drawing.Size(19, 16);
            this.lblBtnEditAuthors.TabIndex = 8;
            // 
            // lblBtnSwitchAuthor
            // 
            this.lblBtnSwitchAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBtnSwitchAuthor.Location = new System.Drawing.Point(687, 534);
            this.lblBtnSwitchAuthor.Name = "lblBtnSwitchAuthor";
            this.lblBtnSwitchAuthor.Size = new System.Drawing.Size(19, 16);
            this.lblBtnSwitchAuthor.TabIndex = 9;
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(792, 553);
            this.Controls.Add(this.lblBtnSwitchAuthor);
            this.Controls.Add(this.lblBtnEditAuthors);
            this.Controls.Add(this.PanelDown);
            this.Controls.Add(this.Splitter2);
            this.Controls.Add(this.PanelUp);
            this.Controls.Add(this.Splitter1);
            this.Controls.Add(this.PanelOutlookBar);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.TextBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Gestão Integrada de Sistemas de Arquivo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.StatusBarPanelHint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusBarPanelUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusBarPanelAutor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusBarPanelHelp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusBarPanelActivity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusBarPanelServer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

	#endregion

		private void Startup(string username)
		{
			this.EnterWaitMode();
			try
			{
				this.Icon = SharedResourcesOld.CurrentSharedResources.GisaIcon;
				StatusBarPanelHelp.Icon = SharedResourcesOld.CurrentSharedResources.HelpIcon;
				lblBtnSwitchAuthor.Image = SharedResourcesOld.CurrentSharedResources.UsrSwitch;
				switch ( (TipoServer) SessionHelper.AppConfiguration.GetCurrentAppconfiguration().TipoServer.ID)
				{
					case TipoServer.ClienteServidor:
						lblBtnEditAuthors.Visible = false;
						break;
					case TipoServer.Monoposto:
						lblBtnEditAuthors.Image = SharedResourcesOld.CurrentSharedResources.UsrEditar;
						StatusBar.Panels.Remove(StatusBarPanelUser);
						break;
				}

				GisaDataSetHelper.ConnectionStateChanged += GisaDataSetHelper_ConnectionStateChanged;
				GisaDataSetHelper_ConnectionStateChanged(false);

				// ficar à escuta pelos dois eventos de alteração de utilizador/autor
				SessionHelper.GetGisaPrincipal().TrusteeUserOperatorChanged += GisaPrincipal_TrusteeUserOperatorChanged;
				SessionHelper.GetGisaPrincipal().TrusteeUserAuthorChanged += GisaPrincipal_TrusteeUserAuthorChanged;
				GisaPrincipal_TrusteeUserOperatorChanged();
				GisaPrincipal_TrusteeUserAuthorChanged();

				ConfigureOutlookBar();
			}
			finally
			{
				this.LeaveWaitMode();
			}

            // Localizacao do servidor de pesquisa:
            if (SessionHelper.AppConfiguration.GetCurrentAppconfiguration().SearchServer != null)
                GISA.Search.HttpClient.SearchServer = SessionHelper.AppConfiguration.GetCurrentAppconfiguration().SearchServer;
            else
                GISA.Search.HttpClient.SearchServer = "http://localhost:8888";

            confRefreshServerTimer();
		}

		private delegate void ConnectionStateChangedHandler(bool isOpen);
		private void GisaDataSetHelper_ConnectionStateChanged(bool isOpen)
		{
			if (this.StatusBar.InvokeRequired)
			{
				this.StatusBar.BeginInvoke(new ConnectionStateChangedHandler(ConnectionStateChange), new object[] {isOpen});
			}
			else
			{
				ConnectionStateChange(isOpen);
			}
		}

		private void ConnectionStateChange(bool isOpen)
		{
			if (isOpen)
			{
				this.StatusBarPanelActivity.Icon = SharedResourcesOld.CurrentSharedResources.DbAccess;
			}
			else
			{
				this.StatusBarPanelActivity.Icon = SharedResourcesOld.CurrentSharedResources.DbAccessOut;
			}
		}

        private void ConnectionStateChangeServer(bool isActive)
        {
            if (isActive)
            {
                this.StatusBarPanelServer.ToolTipText = "O servidor de pesquisa está ativo.";
                this.StatusBarPanelServer.Icon = SharedResourcesOld.CurrentSharedResources.ServerActive;
            }
            else
            {
                this.StatusBarPanelServer.ToolTipText = "Erro na conexão com o servidor de pesquisa.";
                this.StatusBarPanelServer.Icon = SharedResourcesOld.CurrentSharedResources.ServerInactive;
            }
        }

		private void GisaPrincipal_TrusteeUserOperatorChanged()
		{
			string userName = null;
			string userFullName = null;
			userName = SessionHelper.GetGisaPrincipal().TrusteeUserOperator.TrusteeRow.Name;
			if (SessionHelper.GetGisaPrincipal().TrusteeUserOperator.IsFullNameNull())
			{
				userFullName = "";
			}
			else
			{
				userFullName = SessionHelper.GetGisaPrincipal().TrusteeUserOperator.FullName;
			}
			this.StatusBarPanelUser.Text = string.Format("Utilizador: {0}", userName);
			this.StatusBarPanelUser.ToolTipText = string.Format("Nome do utilizador: {0}", userFullName);
		}

		private void GisaPrincipal_TrusteeUserAuthorChanged()
		{
			string authorName = null;
			string authorFullName = null;
			if (SessionHelper.GetGisaPrincipal().TrusteeUserAuthor == null)
			{
				authorName = string.Empty;
				authorFullName = string.Empty;
			}
			else
			{
				authorName = SessionHelper.GetGisaPrincipal().TrusteeUserAuthor.TrusteeRow.Name;
				if (SessionHelper.GetGisaPrincipal().TrusteeUserAuthor.IsFullNameNull())
				{
					authorFullName = "";
				}
				else
				{
					authorFullName = SessionHelper.GetGisaPrincipal().TrusteeUserAuthor.FullName;
				}

			}
			this.StatusBarPanelAutor.Text = string.Format("Autor: {0}", authorName);
			this.StatusBarPanelAutor.ToolTipText = string.Format("Nome do autor: {0}", authorFullName);
		}

		private void ConfigureOutlookBar()
		{
			outlookBar = new LumiSoft.UI.Controls.WOutlookBar.WOutlookBar();
			Font font = new Font(new FontFamily("Arial"), 9F, FontStyle.Regular, GraphicsUnit.Point);
			outlookBar.Font = font;
			outlookBar.Dock = DockStyle.Fill;
			outlookBar.ImageList = new LumiSoft.UI.Controls.WOutlookBar.ImageList();
			outlookBar.ImageList.Images.Add(this.ImageList1.Images[0]);
			outlookBar.UseStaticViewStyle = false;
			outlookBar.ViewStyle.BarBorderColor = Color.Transparent;
            outlookBar.ItemClicked += new LumiSoft.UI.Controls.WOutlookBar.ItemClickedEventHandler(OutlookBarItemClicked);
			outlookBar.ViewStyle.BarClientAreaColor1 = Color.FromArgb(109, 109, 109); // castanho menos escuro
			outlookBar.ViewStyle.BarClientAreaColor2 = Color.FromArgb(58, 58, 58); //castanho mais escuro


			outlookBar.ViewStyle.BarColor = SystemColors.Control;
			outlookBar.ViewStyle.BarHotBorderColor = Color.Transparent;
			outlookBar.ViewStyle.BarHotColor = Color.White;
			outlookBar.ViewStyle.BarHotTextColor = Color.Black;
			outlookBar.ViewStyle.BarItemBorderHotColor = Color.Transparent;
			outlookBar.ViewStyle.BarItemHotColor = Color.WhiteSmoke; //Color.FromArgb(30, 87, 114)
			outlookBar.ViewStyle.BarItemHotTextColor = Color.Black;
			outlookBar.ViewStyle.BarItemPressedColor = Color.Gainsboro;
			outlookBar.ViewStyle.BarItemSelectedColor = Color.LightGray;
			outlookBar.ViewStyle.BarItemSelectedTextColor = Color.Black;
			outlookBar.ViewStyle.BarItemTextColor = Color.White;
			outlookBar.ViewStyle.BarPressedColor = Color.WhiteSmoke;
			outlookBar.ViewStyle.BarTextColor = Color.Black;

			foreach (GISADataset.TipoFunctionGroupRow fg in GisaDataSetHelper.GetInstance().TipoFunctionGroup.Select("", "GUIOrder"))
			{
				// must exist in GisaPrincipal.TrusteePrivileges
				if (HasFunctionGroupAccess(fg))
				{
					LumiSoft.UI.Controls.WOutlookBar.Bar bar = outlookBar.Bars.Add(fg.Name);
					bar.ItemsStyle = LumiSoft.UI.Controls.WOutlookBar.ItemsStyle.FullSelect;
					foreach (GISADataset.TipoFunctionRow f in GisaDataSetHelper.GetInstance().TipoFunction.Select("", "GUIOrder"))
					{
						// must exist in GisaPrincipal.TrusteePrivileges
						// must have at least READ privileges
						if (HasFunctionAccess(fg, f) && ! (supressFunction(f)))
						{

							Assembly asm = EnsureLoaded(f.ModuleName);
							try
							{
								Type t = CheckGuiExtension(asm, f.ClassName);
								Image b = null;
								Trace.WriteLine(string.Format("Installing {0}: {1}", fg.Name, f.Name));
								try
								{
									MemberInfo prop = null;

									Trace.WriteLine("Looking for GetFunctionImage");
									prop = t.GetMethod("GetFunctionImage", new Type[] {typeof(long)});
									if (prop != null)
									{
										b = (Bitmap)(((MethodInfo)prop).Invoke(null, new object[] {f.idx}));
									}
									else
									{
										Trace.WriteLine("Looking for FunctionImage");
										prop = t.GetMember("FunctionImage", BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty)[0];
										b = (Bitmap)(((PropertyInfo)prop).GetValue(null, new object[]{}));
									}

								}
								catch (Exception)
								{
									Trace.WriteLine("Using default image");
									b = (Image)(outlookBar.ImageList.Images[0]);
								}

								LumiSoft.UI.Controls.WOutlookBar.Item tempWith1 = bar.Items.Add(f.Name, 0);
								tempWith1.ImageIndex = outlookBar.ImageList.Images.Add((Bitmap)b);
								tempWith1.Tag = f;
							}
							catch (GuiExtensionNotFoundException)
							{
								MessageBox.Show(string.Format("Não foi possível ativar a funcionalidade" + System.Environment.NewLine + "'{0}' - '{1}'" + System.Environment.NewLine + "configurada para este utilizador.", fg.Name, f.Name), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
							}
						}
					}
	#if ! ALLPRODUCTPERMISSIONS
					if (bar.Items.Count == 0)
					{
						outlookBar.Bars.Remove(bar);
						Trace.WriteLine("Removed " + bar.Caption + ". It had no available functions.");
					}
	#endif
				}
			}

			this.PanelOutlookBar.Controls.Add(outlookBar);
		}


		// Devolver True se houver algum privilégio definido para alguma das funções deste grupo
		private bool HasFunctionGroupAccess(GISADataset.TipoFunctionGroupRow tfg)
		{
	#if ALLPRODUCTPERMISSIONS
			return true;
	#else
			// usar a GUIOrder=0 como indicacao que o grupo deve ser escondido
			if (tfg.GUIOrder == 0)
				return false;

            return SessionHelper.GetGisaPrincipal().TrusteePrivileges.Cast<GISADataset.TrusteePrivilegeRow>().Count(r => r.IDTipoFunctionGroup == tfg.ID) > 0;
	#endif
		}

		// Devolver True se houver algum privilegio definido para a função especificada
		private bool HasFunctionAccess(GISADataset.TipoFunctionGroupRow tfg, GISADataset.TipoFunctionRow tf)
		{

			// Usar o GUIOrder=0 como indicacao que a função deve ser escondida
			if (tf.GUIOrder == 0)
				return false;

			foreach (GISADataset.TrusteePrivilegeRow tp in SessionHelper.GetGisaPrincipal().TrusteePrivileges)
			{
				// se pertencer ao grupo certo
				if (tfg.ID == tp.IDTipoFunctionGroup && tp.IDTipoFunctionGroup == tf.IDTipoFunctionGroup)
				{
	#if ALLPRODUCTPERMISSIONS
					return true;
	#else
					// se se tratar da função especificada 
					if (tp.IdxTipoFunction == tf.idx)
					{
						try
						{
							GisaPrincipalPermission tempWith1 = new GisaPrincipalPermission(SessionHelper.GetGisaPrincipal(), tf.ClassName, GisaPrincipalPermission.READ);
							tempWith1.Demand();
							return true;
						}
						catch (System.Security.SecurityException)
						{
							Trace.WriteLine(string.Format("READ privilege not found for {0} {1}", tfg.Name, tf.Name));
						}
					}
	#endif
				}
			}
			return false;
		}



		// Tratamento de casos excepcionais em que determinada função não deverá ser apresentada
		private bool supressFunction(GISADataset.TipoFunctionRow f)
		{
			return ! (((GISADataset.GlobalConfigRow)(GisaDataSetHelper.GetInstance().GlobalConfig.Select()[0])).NiveisOrganicos) && f.ClassName.Equals("GISA.FRDCAEntidadeProdutora");
		}

		private void mnuAjudaAcercaDe_Click(object sender, System.EventArgs e)
		{
			FormGISAAbout tempWith1 = new FormGISAAbout();
			tempWith1.ShowDialog(this);
		}

		private LumiSoft.UI.Controls.WOutlookBar.Bar LastBarClicked;
		private LumiSoft.UI.Controls.WOutlookBar.Item LastItemClicked;

		private void OutlookBarItemClicked(object Sender, LumiSoft.UI.Controls.WOutlookBar.ItemClicked_EventArgs e)
		{

			if (e.Item.Tag == null)
			{
				return;
			}

			this.outlookBar.Enabled = false;
            try
            {
                GISADataset.TipoFunctionRow masterTipoFunction = null;
                GISADataset.TipoFunctionRow slaveTipoFunction = null;
                slaveTipoFunction = (GISADataset.TipoFunctionRow)e.Item.Tag;
                masterTipoFunction = slaveTipoFunction.TipoFunctionRowParent;                

                // Limpeza do contexto actual antes de fazer um recontextualize. É gravado 
                // o estado actual do contexto para que possa ser recuparado caso se pretenda 
                // cancelar a operação

                if (CurrentContext.Trustee != null)
                    CurrentContext.SetTrustee(null, false);

                CurrentContext.saveState();
                CurrentContext.clear();

                PersistencyHelper.SaveResult successfulSave = PersistencyHelper.SaveResult.successful;
                MultiPanelControl.SaveArgs saveArgs = new MultiPanelControl.SaveArgs(successfulSave);

                // recontextualizar a seguir a uma limpeza de contexto para garantidamente gravar e sair do contexto antigo
                if (PanelDown.Controls.Count == 1)
                {
                    ((GISAControl)(PanelDown.Controls[0])).Recontextualize(saveArgs);
                }

                // clear context information before activating a new master panel
                StatusBarPanelHint.Text = "";

                // limpar propriedade que indica se algum painel está a ser usado como suporte 
                // (mas o estado é guardado numa variável a fim de ser possível restituir o 
                // estado anterior caso ocorra algum conflito de concorrência)
                bool isSptPanel = false;

                // As TipoFunctions costumam ser SlavePanels/FRDs tendo por 
                // isso um TipoFunctionRowParent não nothing. No entanto, o 
                // "slaveTipoFunction" será nothing se existir um MasterPanel 
                // associado directamente ao outlookbaritem clicado. 
                // Inspecção das permissoes de leitura. as permissoes estao 
                // sempre associadas ao slavepanel uma vez que pode-se querer 
                // comportamentos diferentes do painel superior consoante o 
                // painel inferior em uso (Nota: o painel inferior é a maior 
                // parte das vezes um multipanel).
                // Esta logica surge depois da persistencia dos dados na BD
                // para que qualquer alteração às permissões do próprio 
                // utilizador consigam ser recarregadas logo na seguinte 
                // mudança de contexto.
                if (!(CheckTipoFunctionPermissions(slaveTipoFunction, GisaPrincipalPermission.READ)))
                {
#if ! ALLPRODUCTPERMISSIONS
                    MessageBox.Show("Não possui permissões de acesso a esta funcionalidade", "Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    RestoreOutlookBarSelection();
                    if (PanelDown.Controls.Count == 1)
                    {
                        ((GISAControl)(PanelDown.Controls[0])).Recontextualize();
                    }
                    return;
#endif
                }
                else if (saveArgs.save == PersistencyHelper.SaveResult.unsuccessful)
                {
                    RestoreOutlookBarSelection();
                    return;
                }

                // limpar propriedade que indica se algum painel está a ser usado como suporte
                isSuportPanel = isSptPanel;

                Assembly masterAssembly = null;
                Assembly slaveAssembly = null;
                Type masterType = null;
                Type slaveType = null;

                if (masterTipoFunction != null)
                {
                    masterAssembly = EnsureLoaded(masterTipoFunction.ModuleName);
                    masterType = CheckGuiExtension(masterAssembly, masterTipoFunction.ClassName);
                    ClearMasterPanelStack(MasterPanelStack, false);
                }

                if (slaveTipoFunction != null)
                {
                    slaveAssembly = EnsureLoaded(slaveTipoFunction.ModuleName);
                    slaveType = CheckGuiExtension(slaveAssembly, slaveTipoFunction.ClassName);
                    ClearSlavePanelStack(SlavePanelStack, true);
                }
                else
                {
                    Debug.Assert(false, "Slave panel inexistente!");
                }

                if (masterTipoFunction != null)
                {
                    PushMasterPanel(masterType);

                    if (SlavePanelStack.Count > 0)
                    {
                        // teoria: esta situação nunca ocorre. para verificar fica aqui este assert
                        Debug.Assert(false, "existe um slavepanel!?");
                        ((GISAControl)(SlavePanelStack.Peek())).Recontextualize();
                        // no caso de se revelar necessario este recontextualize será necessário também
                        // verificar se qualquer eventual gravacao de dados foi bem sucedida.
                    }
                }

                PushSlavePanel(slaveType);

                if (masterTipoFunction != null)
                {
                    this.PanelUp.Visible = true;
                    this.Splitter2.Visible = true;
                }
                else
                {
                    this.PanelUp.Visible = false;
                    this.Splitter2.Visible = false;
                    foreach (GISAControl gc in MasterPanelStack)
                    {
                        gc.Visible = false;
                    }
                }

                // durante a adição de um masterpanel ou na sua mudança de visibilidade pode 
                // dar-se o caso de ser estabelecido um CurrentContext sem no entanto o evento 
                // lançado ser recebido por alguem (uma vez que não existe slavepanel).
                // Por essa razão é necessário limpar qualquer contexto eventualmente existente
                // e tornar a defini-lo mais tarde, para que o evento seja desta vez recebido.
                CurrentContext.clear();
                ((GISAControl)(SlavePanelStack.Peek())).ListeningToContextChanges = true;

                if (masterTipoFunction != null)
                {
                    UpdateMasterPanelContext(masterType);

                    // Actualizar as permissões do masterpanel tendo em conta o painel inferior 
                    // em uso e de forma a activar/desactivar funcionalidades da toolbar
                    ((frmMain)TopLevelControl).ApplyMasterPermissions();
                    // É necessário voltar a actualizar a toolbar para refletir as permissões 
                    // obtidas do método anterior (em alguns casos - MasterPanelNiveis - a toolbar
                    // é atualizada no momento da construção do master panel)
                    ((frmMain)TopLevelControl).UpdateMasterToolBarButtons();
                }

                LastBarClicked = outlookBar.ActiveBar;
                LastItemClicked = e.Item;
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                Trace.WriteLine(ex);
                MessageBox.Show("Ocorreu um erro durante carregamento de componentes da" + Environment.NewLine + "aplicação, tendo esta de ser fechada. Se o problema persistir" + Environment.NewLine + "por favor contacte o administrador de sistema.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            catch (System.IO.IOException ex)
            {
                Trace.WriteLine(ex);
                MessageBox.Show("Ocorreu um erro durante o acesso ao sistema de ficheiros," + Environment.NewLine + "a aplicação terá de ser fechada. Se o problema persistir" + Environment.NewLine + "por favor contacte o administrador de sistema.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            catch (SearchWebException ex)
            {
                Trace.WriteLine(ex);
                MessageBox.Show("Ocorreu um erro de comunicação com o servidor de pesquisa, a" + Environment.NewLine + "aplicação terá de ser fechada. Se o problema persistir" + Environment.NewLine + "por favor contacte o administrador de sistema.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                MessageBox.Show("Ocorreu um erro durante o acesso à base de dados, a" + Environment.NewLine + "aplicação terá de ser fechada. Se o problema persistir" + Environment.NewLine + "por favor contacte o administrador de sistema.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
			finally
			{
				this.outlookBar.Enabled = true;
			}
		}

		private void RestoreOutlookBarSelection()
		{
			outlookBar.ActiveBar = LastBarClicked;
			outlookBar.StuckenItem = LastItemClicked;
			CurrentContext.restoreState();
		}

		private bool CheckTipoFunctionPermissions(GISADataset.TipoFunctionRow tf, string permission)
		{
			try
			{
				GisaPrincipalPermission tempWith1 = new GisaPrincipalPermission(SessionHelper.GetGisaPrincipal(), tf.ClassName, permission);
				tempWith1.Demand();
				return true;
			}
			catch (System.Security.SecurityException)
			{
				return false;
			}
		}

		public void ApplyMasterPermissions()
		{
			MasterPanel master = null;
			if (MasterPanel != null && MasterPanel is MasterPanel)
			{
				master = (MasterPanel)MasterPanel;
				master.UpdatePermissions();
			}
		}

        public void UpdateMasterToolBarButtons()
        {
            MasterPanel master = null;
            if (MasterPanel != null && MasterPanel is MasterPanel)
            {
                master = (MasterPanel)MasterPanel;
                master.UpdateToolBarButtons();
            }
        }

		private Assembly EnsureLoaded(string AssemblyName)
		{
			foreach (Assembly Asm in AppDomain.CurrentDomain.GetAssemblies())
			{
				System.IO.FileInfo f = new System.IO.FileInfo(Asm.Location);
				AssemblyName = AssemblyName.ToLower();
				if ((f.Name).ToLower().Equals(AssemblyName))
				{
					return Asm;
				}
			}
			return AppDomain.CurrentDomain.Load(AssemblyName);
		}

		private class GuiExtensionNotFoundException : Exception
		{
			public GuiExtensionNotFoundException(string ModuleName, string ClassName) : base(string.Format("Type not found: {0} {1}", ModuleName, ClassName))
			{
			}
		}

		private Type CheckGuiExtension(Assembly Asm, string ClassName)
		{
			Type T = Asm.GetType(ClassName);
			if (T == null)
			{
				throw new GuiExtensionNotFoundException(Asm.GetName().FullName, ClassName);
			}
			if (typeof(GISAControl).IsAssignableFrom(T) | typeof(GISAPanel).IsAssignableFrom(T))
			{
				return T;
			}
			throw new ArgumentException("Type " + ClassName + " must inherit from " + typeof(GISAControl).FullName, "ClassName");
		}

		private Stack MasterPanelStack = new Stack();

		public GISAControl MasterPanel
		{
			get
			{
				if (MasterPanelStack.Count > 0)
				{
					return (GISAControl)(MasterPanelStack.Peek());
				}
				else
				{
					return null;
				}
			}
		}

		private bool mIsSuportPanel = false;
		public bool isSuportPanel
		{
			get
			{
				return mIsSuportPanel;
			}
			set
			{
				mIsSuportPanel = value;
			}
		}

		public int MasterPanelCount
		{
			get
			{
				return MasterPanelStack.Count;
			}
		}

		public bool IsFirstMasterPanelInStack(MasterPanel panel)
		{
			return Array.IndexOf(MasterPanelStack.ToArray(), panel) == MasterPanelStack.Count - 1;
		}

		private void UpdateMasterPanelContext(Type ControlType)
		{
			// update context for all master panels (should only be one at a time)
			try
			{
				((SinglePanel)this.MasterPanel).UpdateContext();
			}
			catch (InvalidCastException ex)
			{
				Trace.WriteLine(ex);
				Debug.Assert(false, "A MasterPanel that is not SinglePanel. Needs fixup.");
			}
		}

		public enum StackOperation: int
		{
			Push = 0,
			Pop = 1
		}



		public void PushMasterPanel(Type ControlType)
		{
			PushPanel(PanelUp, MasterPanelStack, ControlType);

			Control target = null;

			// Lookup a control of this type
			foreach (Control ctrl in PanelUp.Controls)
			{
				if (ctrl.GetType() == ControlType)
				{
					target = ctrl;
				}
			}

			((MasterPanel)target).DoStackChanged(StackOperation.Push, MasterPanelStack.Count > 1);
		}

		public void PopMasterPanel(Type ControlType)
		{
			Control target = null;

            // Lookup a control of this type
			foreach (Control ctrl in PanelUp.Controls)
			{
				if (ctrl.GetType() == ControlType)
				{
					target = ctrl;
				}
			}

			((MasterPanel)target).DoStackChanged(StackOperation.Pop, MasterPanelStack.Count > 0);

			PopPanel(PanelUp, MasterPanelStack, ControlType, false);
		}

		private Stack SlavePanelStack = new Stack();

		public GISAControl SlavePanel
		{
			get
			{
				return (GISAControl)(SlavePanelStack.Peek());
			}
		}

		public void PushSlavePanel(Type ControlType)
		{
			PushPanel(PanelDown, SlavePanelStack, ControlType, false);
		}

		public void PopSlavePanel(Type ControlType)
		{
			PopPanel(PanelDown, SlavePanelStack, ControlType, true);
		}

		public void ClearMasterPanelStack(Stack PanelStack, bool RemovePanel)
		{
			while (PanelStack.Count > 0)
			{
				((MasterPanel)(PanelStack.Peek())).DoStackChanged(StackOperation.Pop, MasterPanelStack.Count > 1);
				PopPanel(PanelUp, PanelStack, PanelStack.Peek().GetType(), RemovePanel);
			}
		}

		public void ClearSlavePanelStack(Stack PanelStack, bool RemovePanel)
		{
			while (PanelStack.Count > 0)
			{
				((GISAControl)(SlavePanelStack.Peek())).ListeningToContextChanges = false;
				PopPanel(PanelDown, PanelStack, PanelStack.Peek().GetType(), RemovePanel);
			}
		}


		public void PushPanel(Panel Container, Stack PanelStack, Type ControlType)
		{
			PushPanel(Container, PanelStack, ControlType, true);
		}

		public void PushPanel(Panel Container, Stack PanelStack, Type ControlType, bool MakeItVisible)
		{
			EnterWaitMode();
			try
			{

				Control target = null;

				// Lookup a control of this type
				foreach (Control ctrl in Container.Controls)
				{
					if (ctrl.GetType() == ControlType)
					{
						target = ctrl;
					}
				}

				// Create a control of this type if it doesn't exist
				if (target == null)
				{
					target = (Control)(ControlType.GetConstructor(new Type[]{}).Invoke(new object[]{}));
					target.Dock = DockStyle.Fill;
					target.Visible = MakeItVisible;
					Container.Controls.Add(target);
				}
				else
				{
					target.Visible = MakeItVisible;
				}

				// Prevent two pushes to the same panel
				if (PanelStack.Count > 1 && PanelStack.Peek() == target)
				{
					return;
				}

				// Prevent recursive use of a panel in a context stack
				if (PanelStack.Contains(target))
				{
					throw new ArgumentException(ControlType.FullName + " is already in use.");
				}

				// Hide previous MasterPanel, if exists
				if (PanelStack.Count > 0)
				{
					((Control)(PanelStack.Peek())).Visible = false;
				}
				PanelStack.Push(target);
			}
			finally
			{
				LeaveWaitMode();
			}
		}

		public void PopPanel(Panel Container, Stack PanelStack, Type ControlType, bool RemovePanel)
		{
			if (! (PanelStack.Peek().GetType() == ControlType))
			{
				throw new ArgumentException(ControlType.FullName + " is not the active panel.");
			}

			Control Ctrl = (Control)(PanelStack.Pop());
			// Hide current Panel
			Ctrl.Visible = false;
			// Remove current Panel
			if (RemovePanel)
			{
				Container.Controls.Remove(Ctrl);
			}

			// Show new current Panel, if exists
			if (PanelStack.Count > 0)
			{
				((Control)(PanelStack.Peek())).Visible = true;
			}
		}

		// verifica se existe permissão de execução da operação especificada 
		// no contexto do painel inferior (o qual é geralmente um multipanel)
		public bool CheckPermission(string operation)
		{
			Debug.Assert(PanelDown.Controls.Count == 1);
			return CheckPermission(PanelDown.Controls[0].GetType().FullName, operation);
		}

		// verifica se existe permissão de execução da operação especificada 
		// no contexto do painel especificado
		public bool CheckPermission(string ClassFullName, string operation)
		{
			try
			{
				GisaPrincipalPermission tempWith1 = new GisaPrincipalPermission(SessionHelper.GetGisaPrincipal(), ClassFullName, operation);
				tempWith1.Demand();
			}
			catch (System.Security.SecurityException)
			{
				return false;
			}
			return true;
		}

		private void frmMain_Closing(object Sender, System.ComponentModel.CancelEventArgs e) {
			

			if (PanelDown.Controls.Count > 0)
			{
				GISAControl ctrl = (GISAControl)(SlavePanelStack.Peek());
                PersistencyHelper.SaveResult successfulSave = PersistencyHelper.SaveResult.successful;
				MultiPanelControl.SaveArgs saveArgs = new MultiPanelControl.SaveArgs(successfulSave);
				ctrl.CurrentContext.saveState();
				ctrl.CurrentContext.clear();
				ctrl.Recontextualize(saveArgs);
                if (saveArgs.save == PersistencyHelper.SaveResult.unsuccessful)
				{
					e.Cancel = true;
					ctrl.CurrentContext.restoreState();
					return;
				}
				PopSlavePanel(ctrl.GetType());
				e.Cancel = SlavePanelStack.Count > 0;
			}
			//logout
            Thread t = new Thread(new ThreadStart(CleanupDB));
            t.Start();
            
            //delete temp files
            Thread t2 = new Thread(new ThreadStart(ImageHelper.DeleteTempFiles));
            t2.Start();
		}

        private void CleanupDB()
        {
            IDbConnection conn = GisaDataSetHelper.GetTempConnection();
            try
            {
                conn.Open();
                DBAbstractDataLayer.DataAccessRules.TrusteeRule.Current.deleteDeletedData(conn);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
        }

  
		private StatusBarPanel panelClicked = null;
		private void StatusBar_PanelClick(object sender, System.Windows.Forms.StatusBarPanelClickEventArgs e)
		{
			if (e.StatusBarPanel == StatusBarPanelHelp && e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				ContextMenuHelp.Show(StatusBar, new Point(e.X, e.Y));
			}
			else if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				panelClicked = e.StatusBarPanel;
			}
		}

		private void StatusBar_DoubleClick(object sender, System.EventArgs e)
		{
			if (panelClicked == StatusBarPanelHelp)
			{
				FormGISAAbout tempWith1 = new FormGISAAbout();
				tempWith1.ShowDialog(this);
			}
		}

		private void MenuItemAbout_Click(object sender, System.EventArgs e)
		{
			FormGISAAbout tempWith1 = new FormGISAAbout();
			tempWith1.ShowDialog(this);
		}

		private void lblBtnSwitchAuthor_Click(object sender, System.EventArgs e)
		{
			FormSwitchAuthor form = new FormSwitchAuthor();
			if (SessionHelper.GetGisaPrincipal().TrusteeUserAuthor != null)
			{
				form.ControloAutores1.SelectedAutor = SessionHelper.GetGisaPrincipal().TrusteeUserAuthor.TrusteeRow;
			}
			form.ShowDialog();
		}

		private void lblBtnEditAuthors_Click(object sender, System.EventArgs e)
		{
			FormManageAutoresDescricao form = new FormManageAutoresDescricao();
			form.ShowDialog();
		}

        //timer responsável por pingar, a cada 60s, o servidor (GISAServer) 
        private bool wasServerActive = false;
        private System.Windows.Forms.Timer serverPingTimer = new System.Windows.Forms.Timer();
        
		private void confRefreshServerTimer()
		{
            bool isOn = Search.Helper.IsServerActive();
            wasServerActive = isOn;
            ConnectionStateChangeServer(isOn);                
            serverPingTimer.Interval = 60000;			
            serverPingTimer.Start();            
		}

        private void serverPingTimer_Tick(object sender, System.EventArgs e)
        {
            SetServerStatus();
        }

        public void SetServerStatus()
        {
            if (wasServerActive && !Search.Helper.IsServerActive())
            {
                wasServerActive = false;
                ConnectionStateChangeServer(false);
                MessageBox.Show(this, "Servidor inativo!");

            }
            else if (!wasServerActive && Search.Helper.IsServerActive())
            {
                wasServerActive = true;
                ConnectionStateChangeServer(true);
            }
        }

        //private bool IsServerActive()
        //{
        //    bool ret = false;
        //    try
        //    {
        //        System.Collections.Generic.List<string> response = GISA.Search.HttpClient.HttpGetresults("/?f=ping", "");
        //        if (response.Contains("pong"))
        //        {
        //            ret = true;                    
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Trace.WriteLine(e);
        //    }
        //    return ret;
        //}
	}

    // classe que define os argumentos utilizados para registar mudanças em controlos de autoridade, 
    // unidades físicas e níveis documentais
    public class RegisterModificationEventArgs : EventArgs
    {
        // indica qual o contexto que vai ser registado
        private DataRow m_context;
        public RegisterModificationEventArgs(DataRow context)
        {
            this.m_context = context;
        }
        
        public DataRow Context
        {
            get {return this.m_context;}
            set {this.m_context = value;}
        }
    }
}