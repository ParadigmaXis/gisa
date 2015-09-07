using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

using System.Data.Common;
using System.Windows.Forms;
using DBAbstractDataLayer.DataAccessRules;
using GISA.Fedora.FedoraHandler;

namespace GISA.Model
{
	public class PersistencyHelper
	{
        // resultados possíveis para o retorno do save
        public enum SaveResult
        {
            successful = 0,
            unsuccessful = 1,
            nothingToSave = 2,
            cancel = 3
        }

		//dataset com as linhas correspondentes aquelas alteradas pelo utilizador
		private static DataSet originalRowsDB1 = null;
		private static DataSet originalRowsDB2 = null;
        
		public static TableDepthOrdered tdo = new TableDepthOrdered();
		//arraylist que contem as tabelas ordenadas por profundidade ascendente (começa por aquelas que não tem parent relations)
		//esta ordenação reflecte a ordem necessaria para serem executados inserts e updates de forma a manter a integridade referencial da base de dados
		private static ArrayList mDataSetTablesOrderedA;
        private static ArrayList DataSetTablesOrderedA
        {
            get {
                if (mDataSetTablesOrderedA == null)
                    mDataSetTablesOrderedA = tdo.getTabelasOrdenadas();
                return mDataSetTablesOrderedA;
            }
        }

		private static FormConcurrencyMessage frm = new FormConcurrencyMessage();

        private static Dictionary<List<TableDepthOrdered.TableCloudType>, List<TableDepthOrdered.tableDepth>> cacheNuvens = new Dictionary<List<TableDepthOrdered.TableCloudType>, List<TableDepthOrdered.tableDepth>>();

        #region Pre-transaction arguments
        public delegate void PreTransactionDelegate(PreTransactionArguments args);
        public interface PreTransactionArguments
        {
            bool cancelAction { get; set; }
            string message { get; set; }
        }

        public class FedoraIngestPreTransactionArguments : PersistencyHelper.PreTransactionArguments
        {
            private bool mCancelAction = false;
            private string mMessage = "";
            public bool cancelAction
            {
                get { return mCancelAction; }
                set { mCancelAction = value; }
            }

            public string message
            {
                get { return mMessage; }
                set { mMessage  = value; }
            }
        }
        #endregion

        #region  Pre-concurrency arguments
        public delegate void preConcDelegate(PreConcArguments pca);
		public interface PreConcArguments
		{
			IDbTransaction tran {get; set;}
			DataSet gisaBackup {get; set;}
			// property utilizada somente na eliminação de relações hierárquicas no contexto da organização de informação
			bool continueSave {get; set;}
		}
        public class LstPasteRhXPreConcArguments : PersistencyHelper.PreConcArguments
        {
            private IDbTransaction mTran;
            public System.Data.IDbTransaction tran
            {
                get { return mTran; }
                set { mTran = value; }
            }

            private DataSet mGisaBackup;
            public DataSet gisaBackup
            {
                get { return mGisaBackup; }
                set { mGisaBackup = value; }
            }

            private bool mContinueSave;
            public bool continueSave
            {
                get { return mContinueSave; }
                set { mContinueSave = value; }
            }

            public long IDTipoNivelRelacionado;
            public List<ObjDigital> ODsToIngest;
            public List<PasteRhXPreConcArguments> pasteRhXPreConcArguments;
        }

		public class PasteRhXPreConcArguments : PersistencyHelper.PreConcArguments
		{
			private IDbTransaction mTran;
			public System.Data.IDbTransaction tran
			{
				get {return mTran;}
				set {mTran = value;}
			}

			private DataSet mGisaBackup;
			public DataSet gisaBackup
			{
				get {return mGisaBackup;}
				set {mGisaBackup = value;}
			}

			private bool mContinueSave;
			public bool continueSave
			{
				get {return mContinueSave;}
				set {mContinueSave = value;}
			}

			public enum PasteErrors: int
			{
				NoError = 0,
				RHDeleted = 1,
				NDeleted = 2,
				Avaliacao = 3,
				NotUniqueCodigo = 4,
                ObjDigital = 5
			}

            public long rhRowOldID;
			public long rhRowOldIDUpper;
			public GISADataset.RelacaoHierarquicaRow rhRowNew;
			public PasteErrors PasteError = PasteErrors.NoError;
			public string message = string.Empty;
			//Nível sobre o qual vai ser executado o "paste"
			public GISADataset.NivelRow nivel;

			public EnsureUniqueCodigoNivelPreConcArguments ensureUniqueCodigoArgs;
            public ManageDocsPermissionsPreConcArguments manageDocsPermissionsArgs;
            public SetNivelOrderPreConcArguments setNivelOrderPreConcArguments;
            public UpdatePermissionsPostSaveArguments updatePermissionsPostSaveArgs;
		}

        public class ManageDocsPermissionsPreConcArguments : PersistencyHelper.PreConcArguments
        {
            private IDbTransaction mTran;
            public System.Data.IDbTransaction tran
            {
                get { return mTran; }
                set { mTran = value; }
            }

            private DataSet mGisaBackup;
            public DataSet gisaBackup
            {
                get { return mGisaBackup; }
                set { mGisaBackup = value; }
            }

            private bool mContinueSave;
            public bool continueSave
            {
                get { return mContinueSave; }
                set { mContinueSave = value; }
            }

            public bool changePermissions = false;
            public GISADataset.NivelRow newParentRow;
            public GISADataset.NivelRow oldParentRow;
            public GISADataset.NivelRow nRow;
        }

        public class SetNivelOrderPreConcArguments : PersistencyHelper.PreConcArguments
        {
            private IDbTransaction mTran;
            public System.Data.IDbTransaction tran
            {
                get { return mTran; }
                set { mTran = value; }
            }

            private DataSet mGisaBackup;
            public DataSet gisaBackup
            {
                get { return mGisaBackup; }
                set { mGisaBackup = value; }
            }

            private bool mContinueSave;
            public bool continueSave
            {
                get { return mContinueSave; }
                set { mContinueSave = value; }
            }

            public long nRowID;
            public long nRowIDUpper;
        }

		public class CreateTrusteePreConcArguments : PersistencyHelper.PreConcArguments
		{
            private IDbTransaction mTran;
            public System.Data.IDbTransaction tran
            {
                get { return mTran; }
                set { mTran = value; }
            }

            private DataSet mGisaBackup;
            public DataSet gisaBackup
            {
                get { return mGisaBackup; }
                set { mGisaBackup = value; }
            }

            private bool mContinueSave;
            public bool continueSave
            {
                get { return mContinueSave; }
                set { mContinueSave = value; }
            }

			public long truRowID;
			public long grpRowID;
			public long usrRowID;
            public UpdatePermissionsPostSaveArguments UpdatePermissionsPostSaveArguments;
			public bool successful = false;
		}

		public class EditTrusteePreConcArguments : PersistencyHelper.PreConcArguments
		{
            private IDbTransaction mTran;
            public System.Data.IDbTransaction tran
            {
                get { return mTran; }
                set { mTran = value; }
            }

            private DataSet mGisaBackup;
            public DataSet gisaBackup
            {
                get { return mGisaBackup; }
                set { mGisaBackup = value; }
            }

            private bool mContinueSave;
            public bool continueSave
            {
                get { return mContinueSave; }
                set { mContinueSave = value; }
            }

			public GISADataset.TrusteeRow truRow = null;
			public string username = null;
			public bool successful = false;
		}

		public class ManageFormasAutorizadasPreConcArguments : PersistencyHelper.PreConcArguments
		{
            private IDbTransaction mTran;
            public System.Data.IDbTransaction tran
            {
                get { return mTran; }
                set { mTran = value; }
            }

            private DataSet mGisaBackup;
            public DataSet gisaBackup
            {
                get { return mGisaBackup; }
                set { mGisaBackup = value; }
            }

            private bool mContinueSave;
            public bool continueSave
            {
                get { return mContinueSave; }
                set { mContinueSave = value; }
            }

			public string termo = null;
			public long caRowID;
			public GISADataset.ControloAutDicionarioRow cadRow;
			public string message = string.Empty;
		}

		public class DeleteCAXPreConcArguments : PersistencyHelper.PreConcArguments
		{
            private IDbTransaction mTran;
            public System.Data.IDbTransaction tran
            {
                get { return mTran; }
                set { mTran = value; }
            }

            private DataSet mGisaBackup;
            public DataSet gisaBackup
            {
                get { return mGisaBackup; }
                set { mGisaBackup = value; }
            }

            private bool mContinueSave;
            public bool continueSave
            {
                get { return mContinueSave; }
                set { mContinueSave = value; }
            }

			public ArrayList termos = new ArrayList();
			public long caRowID;
			public string catCode;
		}

		public class EditControloAutPreConcArguments : PersistencyHelper.PreConcArguments
		{
            private IDbTransaction mTran;
            public System.Data.IDbTransaction tran
            {
                get { return mTran; }
                set { mTran = value; }
            }

            private DataSet mGisaBackup;
            public DataSet gisaBackup
            {
                get { return mGisaBackup; }
                set { mGisaBackup = value; }
            }

            private bool mContinueSave;
            public bool continueSave
            {
                get { return mContinueSave; }
                set { mContinueSave = value; }
            }

			public enum EditCAErrors: int
			{
				NoError = 0,
				CADeleted = 1,
				OrigCadDeleted = 2,
				AlreadyUsedTermo = 3,
				AlreadyUsedCodigo = 4
			}

			public long origDRowID;
			public long newDRowID;
			public long origCadRowIDControloAut;
			public long origCadRowIDDicionario;
			public long origCadRowIDTipoControloAutForma;
			public long newCadRowIDControloAut;
			public long newCadRowIDDicionario;
			public long newCadRowIDTipoControloAutForma;
			public long nRowID;
			public long caRowID;
			public EditCAErrors editCAError = EditCAErrors.NoError;
			public string message;
		}

		public class DeleteTermoXPreConcArguments : PersistencyHelper.PreConcArguments
		{
            private IDbTransaction mTran;
            public System.Data.IDbTransaction tran
            {
                get { return mTran; }
                set { mTran = value; }
            }

            private DataSet mGisaBackup;
            public DataSet gisaBackup
            {
                get { return mGisaBackup; }
                set { mGisaBackup = value; }
            }

            private bool mContinueSave;
            public bool continueSave
            {
                get { return mContinueSave; }
                set { mContinueSave = value; }
            }

			public long drowID;
			public long caRowID;
		}

		public class ManageDescFisicasPreConcArguments : PersistencyHelper.PreConcArguments
		{
            private IDbTransaction mTran;
            public System.Data.IDbTransaction tran
            {
                get { return mTran; }
                set { mTran = value; }
            }

            private DataSet mGisaBackup;
            public DataSet gisaBackup
            {
                get { return mGisaBackup; }
                set { mGisaBackup = value; }
            }

            private bool mContinueSave;
            public bool continueSave
            {
                get { return mContinueSave; }
                set { mContinueSave = value; }
            }

			public enum ActionResult: int
			{
				complete = 1,
				descFisAlreadyExists = 2,
				quantidadeUsedByOthers = 3
			}

			public ArrayList quant;
			public long frdID;
			public ActionResult aResult = ActionResult.complete;
		}

		public class EnsureUniqueCodigoNivelPreConcArguments : PersistencyHelper.PreConcArguments
		{
            private IDbTransaction mTran;
            public System.Data.IDbTransaction tran
            {
                get { return mTran; }
                set { mTran = value; }
            }

            private DataSet mGisaBackup;
            public DataSet gisaBackup
            {
                get { return mGisaBackup; }
                set { mGisaBackup = value; }
            }

            private bool mContinueSave;
            public bool continueSave
            {
                get { return mContinueSave; }
                set { mContinueSave = value; }
            }

			public bool testOnlyWithinNivel = false;
			public long nRowID;
			public long ndRowID;
			public long rhRowID;
			public long rhRowIDUpper;
            public long frdBaseID;
			public string message = string.Empty;
			public bool successful;
			public VerifyIfRHNivelUpperExistsPreConcArguments vrhnuePca = null;
		}

		public class AddEditUFPreConcArguments : PersistencyHelper.PreConcArguments
		{
            private IDbTransaction mTran;
            public System.Data.IDbTransaction tran
            {
                get { return mTran; }
                set { mTran = value; }
            }

            private DataSet mGisaBackup;
            public DataSet gisaBackup
            {
                get { return mGisaBackup; }
                set { mGisaBackup = value; }
            }

            private bool mContinueSave;
            public bool continueSave
            {
                get { return mContinueSave; }
                set { mContinueSave = value; }
            }

			public enum OperationErrors: int
			{
				NewUF = 0,
				EditDesignacao = 1,
				EditEDAndDesignacao = 2,
				EditNewEd = 3,
				EditOriginalEd = 4,
				NoError = 5
			}

			public enum Operations: int
			{
				Create = 0,
                CreateLike = 1,
				Edit = 2
			}

			public long nivelUFRowID;
			public long ndufRowID;
			public long rhufRowID;
			public long rhufRowIDUpper;
			public long newRhufRowID;
			public long newRhufRowIDUpper;
			public long nufufRowID;
            // variáveis para a operação CreateLike
            public long frdufRowID;
            public List<long> uaAssociadas;

			public OperationErrors OperationError = OperationErrors.NoError;
			public Operations Operation;
			public IsCodigoUFBeingUsedPreSaveArguments psa;
			public string message = string.Empty;
		}

		public class VerifyIfRHNivelUpperExistsPreConcArguments : PersistencyHelper.PreConcArguments
		{
            private IDbTransaction mTran;
            public System.Data.IDbTransaction tran
            {
                get { return mTran; }
                set { mTran = value; }
            }

            private DataSet mGisaBackup;
            public DataSet gisaBackup
            {
                get { return mGisaBackup; }
                set { mGisaBackup = value; }
            }

            private bool mContinueSave;
            public bool continueSave
            {
                get { return mContinueSave; }
                set { mContinueSave = value; }
            }

			public long nRowID;
			public long ndRowID;
			public long rhRowID;
			public long rhRowIDUpper;
            public long frdBaseID;
			public string message = string.Empty;
			public bool RHNivelUpperExists;
		}

		public class canDeleteRHRowPreConcArguments : PersistencyHelper.PreConcArguments
		{
            private IDbTransaction mTran;
            public System.Data.IDbTransaction tran
            {
                get { return mTran; }
                set { mTran = value; }
            }

            private DataSet mGisaBackup;
            public DataSet gisaBackup
            {
                get { return mGisaBackup; }
                set { mGisaBackup = value; }
            }

            private bool mContinueSave;
            public bool continueSave
            {
                get { return mContinueSave; }
                set { mContinueSave = value; }
            }

			public long nRowID;
			public long nUpperRowID;
			public long rhRowID;
			public long rhRowIDUpper;
			public string message = string.Empty;
			public bool deleteSuccessful = true;
		}

		public class newControloAutRelPreConcArguments : PersistencyHelper.PreConcArguments
		{
            private IDbTransaction mTran;
            public System.Data.IDbTransaction tran
            {
                get { return mTran; }
                set { mTran = value; }
            }

            private DataSet mGisaBackup;
            public DataSet gisaBackup
            {
                get { return mGisaBackup; }
                set { mGisaBackup = value; }
            }

            private bool mContinueSave;
            public bool continueSave
            {
                get { return mContinueSave; }
                set { mContinueSave = value; }
            }

			public GISADataset.ControloAutRelRow car = null;
			//Distinguir entre os IDs que se vão relacionar entre o que o controlo de autoridade que
			//define o contexto e o controlo de autoridade que se vai relacionar com o primeiro
			public long currentCAID;
			public long relatedCAID;
			public string message = string.Empty;
			public bool addSuccessful = true;
		}

		public class VerifyRelExistencePreConcArguments : PersistencyHelper.PreConcArguments 
        {
			private IDbTransaction mTran;
			public IDbTransaction tran {
				get { return mTran; }
				set { mTran = value; }
			}

			private DataSet mGisaBackup;
			public DataSet gisaBackup {
				get { return mGisaBackup; }
				set { mGisaBackup = value; }
			}

			private bool mContinueSave;
			public bool continueSave {
				get { return mContinueSave; }
				set { mContinueSave = value; }
			}

			public enum CreateEditRelationErrors: int
			{
				NoError = 0,
				RelationAlreadyExists = 1,
				CyclicRelation = 2,
				CADeleted = 3
			}

			public long ID;
			public long IDUpper;
			public long IDTipoRel;
			// variável que indica se a row que vai ser tratada é uma RelacaoHierarquicaRow ou ControloAutRelRow
			// valor: true -> ControloAutRelRow
			//        false -> RelacaoHierarquicaRow 
			public bool isCARRow;
			public CreateEditRelationErrors CreateEditResult = CreateEditRelationErrors.NoError;
		}

        public class ValidateNivelAddAndAssocNewUFPreConcArguments : PersistencyHelper.PreConcArguments
        {
            private IDbTransaction mTran;
            public System.Data.IDbTransaction tran
            {
                get { return mTran; }
                set { mTran = value; }
            }

            private DataSet mGisaBackup;
            public DataSet gisaBackup
            {
                get { return mGisaBackup; }
                set { mGisaBackup = value; }
            }

            private bool mContinueSave;
            public bool continueSave
            {
                get { return mContinueSave; }
                set { mContinueSave = value; }
            }

            public long IDNivel;
            public long IDTipoNivelRelacionado;
            public long IDFRDBaseNivelDoc;
            public string designacaoUFAssociada;
            public GISATreeNode produtor;
            public PreConcArguments argsNivel;
            public AddEditUFPreConcArguments argsUF;
            public string message = string.Empty;
            public bool addNewUF = false;
        }

        public class ValidaIntegDocExtPreConcArguments : PersistencyHelper.PreConcArguments
        {
            private IDbTransaction mTran;
            public System.Data.IDbTransaction tran
            {
                get { return mTran; }
                set { mTran = value; }
            }

            private DataSet mGisaBackup;
            public DataSet gisaBackup
            {
                get { return mGisaBackup; }
                set { mGisaBackup = value; }
            }

            private bool mContinueSave;
            public bool continueSave
            {
                get { return mContinueSave; }
                set { mContinueSave = value; }
            }

            public List<ValidateNivelAddAndAssocNewUFPreConcArguments> newDocsList = null;
        }

        public class ValidaImportPreConcArguments : PersistencyHelper.PreConcArguments
        {
            private IDbTransaction mTran;
            public System.Data.IDbTransaction tran
            {
                get { return mTran; }
                set { mTran = value; }
            }

            private DataSet mGisaBackup;
            public DataSet gisaBackup
            {
                get { return mGisaBackup; }
                set { mGisaBackup = value; }
            }

            private bool mContinueSave;
            public bool continueSave
            {
                get { return mContinueSave; }
                set { mContinueSave = value; }
            }

            public List<ValidateNivelAddAndAssocNewUFPreConcArguments> newDocsList = null;
            public List<AddEditUFPreConcArguments> newUfsList = null;
            public string errorMessage = "";
        }

        public class ValidateMovimentoDeleteItemPreConcArguments : PersistencyHelper.PreConcArguments
        {
            private IDbTransaction mTran;
            public System.Data.IDbTransaction tran
            {
                get { return mTran; }
                set { mTran = value; }
            }

            private DataSet mGisaBackup;
            public DataSet gisaBackup
            {
                get { return mGisaBackup; }
                set { mGisaBackup = value; }
            }

            private bool mContinueSave;
            public bool continueSave
            {
                get { return mContinueSave; }
                set { mContinueSave = value; }
            }

            public long IDNivel;
            public long IDMovimento;
            public string CatCode;
        }

        public class DeleteMovimentoPreConcArguments : PersistencyHelper.PreConcArguments
        {
            private IDbTransaction mTran;
            public System.Data.IDbTransaction tran
            {
                get { return mTran; }
                set { mTran = value; }
            }

            private DataSet mGisaBackup;
            public DataSet gisaBackup
            {
                get { return mGisaBackup; }
                set { mGisaBackup = value; }
            }

            private bool mContinueSave;
            public bool continueSave
            {
                get { return mContinueSave; }
                set { mContinueSave = value; }
            }

            public long IDMovimento;
            public string CatCode;
        }

        public class DeleteDepositoPreConcArguments : PersistencyHelper.PreConcArguments
        {
            private IDbTransaction mTran;
            public System.Data.IDbTransaction tran
            {
                get { return mTran; }
                set { mTran = value; }
            }

            private DataSet mGisaBackup;
            public DataSet gisaBackup
            {
                get { return mGisaBackup; }
                set { mGisaBackup = value; }
            }

            private bool mContinueSave;
            public bool continueSave
            {
                get { return mContinueSave; }
                set { mContinueSave = value; }
            }

            public long IDDeposito;
        }
	#endregion

	    #region  Pre-save arguments 
		public delegate void preSaveDelegate(PreSaveArguments psa);
		public abstract class PreSaveArguments
		{
			public IDbTransaction tran {get; set;}
		}

        /*public class LstPasteRhXPreSaveArguments : PersistencyHelper.PreSaveArguments
        {
            public List<PasteRhXPreSaveArguments> lstPasteRhXPreSaveArguments;
            public long IDTipoNivelRelacionado;
            public List<ObjDigital> ODsToIngest;
        }*/

        /*public class PasteRhXPreSaveArguments : PersistencyHelper.PreSaveArguments
        {
            public ManageDocsPermissionsPreSaveArguments manageDocsPermissionsArgs;
            public PersistencyHelper.SetNivelOrderPreSaveArguments psArgsNivelDocSimples;
        }*/

		public class DeleteIDXPreSaveArguments : PersistencyHelper.PreSaveArguments
		{
			public long ID;
		}

		public class GetRelPanelCAControloPreSaveArguments : PersistencyHelper.PreSaveArguments
		{
			public string arg1;
			public string arg2;
			public string arg3;
			public bool rez;
		}

		public class NewControloAutPreSaveArguments : PersistencyHelper.PreSaveArguments
		{
			public long caID;
			public long dID;
			public string dTermo;
            public string epCodigo;
			public long cadIDControloAut;
			public long cadIDDicionario;
			public long cadIDTipoControloAutForma;
			public long nID;
			public bool successTermo = false;
			public bool successCodigo = false;
		}

		public class FetchLastCodigoSeriePreSaveArguments : PersistencyHelper.PreSaveArguments
		{
			public long nRowID;
			public VerifyIfRHNivelUpperExistsPreConcArguments pcArgs;
		}

		public class IsCodigoUFBeingUsedPreSaveArguments : PersistencyHelper.PreSaveArguments
		{
			public long rhufRowID;
			public long rhufRowIDUpper;
			public long nivelUFRowID;
			public bool cancelSetNewCodigo = false;
			public string message = string.Empty;
		}

		public class PublishSubDocumentosPreSaveArguments : PersistencyHelper.PreSaveArguments
		{
            public List<DBAbstractDataLayer.DataAccessRules.NivelRule.PublicacaoDocumentos> DocsID;
            public List<string> idsToUpdate;
		}

		public class AvaliaDocumentosTabelaPreSaveArguments : PersistencyHelper.PreSaveArguments
		{
			public long frdID = long.MinValue;
			public long modeloAvaliacaoID;
			public bool	avaliacaoTabela;
			public bool	preservar;
			public short prazoConservacao;
		}

		public class AvaliacaoPublicacaoPreSaveArguments : PersistencyHelper.PreSaveArguments
		{
			public PublishSubDocumentosPreSaveArguments psArgs1;
			public AvaliaDocumentosTabelaPreSaveArguments psArgs2;
		}

        public class SetNewCodigosPreSaveArguments : PersistencyHelper.PreSaveArguments
        {
            public bool createNewNivelCodigo = false;
            public FetchLastCodigoSeriePreSaveArguments argsNivel;
            public bool createNewUFCodigo = false;
            public IsCodigoUFBeingUsedPreSaveArguments argsUF;
            public SetNewNivelOrderPreSaveArguments argsNivelDocSimples;
            public bool setNewCodigo = false;
        }

        public class SetNewNivelOrderPreSaveArguments : PersistencyHelper.PreSaveArguments
        {
            public long nRowID;
            public long nRowIDUpper;
        }

        public class SetGroupPermsToUsersPreSaveArguments : PersistencyHelper.PreSaveArguments
        {
            public long userID;
            public List<long> groupIDs;
            public CreateTrusteePreConcArguments cetPca = null;
        }

        public class ValidaIntegDocExtPreSaveArguments : PersistencyHelper.PreSaveArguments
        {
            public List<SetNewCodigosPreSaveArguments> newDocArgs = null;
            public List<NewControloAutPreSaveArguments> newControloAutArgs = null;
        }

        public class ValidaImportPreSaveArguments : PersistencyHelper.PreSaveArguments
        {
            public List<SetNewCodigosPreSaveArguments> newDocArgs = null;
            public List<IsCodigoUFBeingUsedPreSaveArguments> newUfArgs = null;
            public string errorMessage = "";
        }
	    #endregion

        #region Post-save arguments
        public delegate void PostSaveDelegate(PostSaveArguments args);
        public interface PostSaveArguments
        {
            IDbTransaction tran { get; set; }
            bool cancelAction { get; set; }
        }

        public class GenericPostSaveArguments : PersistencyHelper.PostSaveArguments
        {
            private IDbTransaction mTran;
            public IDbTransaction tran
            {
                get { return mTran; }
                set { mTran = value; }
            }

            private bool mCancelAction = false;
            public bool cancelAction
            {
                get { return mCancelAction; }
                set { mCancelAction = value; }
            }
        }

        public class UpdatePermissionsPostSaveArguments : PersistencyHelper.PostSaveArguments
        {
            private IDbTransaction mTran;
            public IDbTransaction tran
            {
                get { return mTran; }
                set { mTran = value; }
            }

            private bool mCancelAction = false;
            public bool cancelAction
            {
                get { return mCancelAction; }
                set { mCancelAction = value; }
            }

            public List<long> NiveisIDs;
            public long TrusteeID;
        }
    #endregion 

        #region Save() assinatures
        public static SaveResult save(preConcDelegate pcDelegate, PreConcArguments pcArguments, preSaveDelegate psDelegate, PreSaveArguments psArguments, PostSaveAction postSaveAction, bool activateOpcaoCancelar)
        {
            return save(null, pcDelegate, pcArguments, psDelegate, psArguments, postSaveAction, activateOpcaoCancelar);
        }

        public static SaveResult save(preConcDelegate pcDelegate, PreConcArguments pcArguments, preSaveDelegate psDelegate, PreSaveArguments psArguments, PostSaveAction postSaveAction)
        {
            return save(null, pcDelegate, pcArguments, psDelegate, psArguments, postSaveAction, false);
        }

        public static SaveResult save(preConcDelegate pcDelegate, PreConcArguments pcArguments, preSaveDelegate psDelegate, PreSaveArguments psArguments)
		{
            return save(null, pcDelegate, pcArguments, psDelegate, psArguments, null, false);
		}

        public static SaveResult save(preConcDelegate pcDelegate, PreConcArguments pcArguments)
		{
            return save(null, pcDelegate, pcArguments, null, null, null, false);
		}

        public static SaveResult save(bool activateOpcaoCancelar)
        {
            return save(null, null, null, null, null, null, activateOpcaoCancelar);
		}

        public static SaveResult save()
		{
            return save(null, null, null, null, null, null, false);
		}

        public static SaveResult save(PostSaveAction postSaveAction, bool activateOpcaoCancelar)
        {
            return save(null, null, null, null, null, postSaveAction, activateOpcaoCancelar);
        }

        public static SaveResult save(PostSaveAction postSaveAction)
        {
            return save(null, null, null, null, null, postSaveAction, false);
        }

        public static SaveResult save(PreTransactionAction preTransactionAction, bool activateOpcaoCancelar)
        {
            return save(preTransactionAction, null, null, null, null, null, activateOpcaoCancelar);
        }

        public static SaveResult save(preConcDelegate pcDelegate, PreConcArguments pcArguments, PostSaveAction postSaveAction)
        {
            return save(null, pcDelegate, pcArguments, null, null, postSaveAction, false);
        }

        public static SaveResult save(preConcDelegate pcDelegate, PreConcArguments pcArguments, PreTransactionAction preTransactionAction)
        {
            return save(preTransactionAction, pcDelegate, pcArguments, null, null, null, false);
        }

        public static SaveResult save(preConcDelegate pcDelegate, PreConcArguments pcArguments, PostSaveAction postSaveAction, PreTransactionAction preTransactionAction, bool activateOpcaoCancelar)
        {
            return save(preTransactionAction, pcDelegate, pcArguments, null, null, postSaveAction, activateOpcaoCancelar);
        }

        public static SaveResult save(preSaveDelegate psDelegate, PreSaveArguments psArguments, PostSaveAction postSaveAction, PreTransactionAction preTransactionAction, bool activateOpcaoCancelar)
        {
            return save(preTransactionAction, null, null, psDelegate, psArguments, postSaveAction, activateOpcaoCancelar);
        }

        public static SaveResult save(preConcDelegate pcDelegate, PreConcArguments pcArguments, preSaveDelegate psDelegate, PreSaveArguments psArguments, PreTransactionAction preTransactionAction)
        {
            return save(preTransactionAction, pcDelegate, pcArguments, psDelegate, psArguments, null, false);
        }

        public static SaveResult save(preConcDelegate pcDelegate, PreConcArguments pcArguments, PostSaveAction postSaveAction, bool activateOpcaoCancelar)
        {
            return save(null, pcDelegate, pcArguments, null, null, postSaveAction, activateOpcaoCancelar);
        }

        public static SaveResult save(PreTransactionAction preTransactionAction, preConcDelegate pcDelegate, PreConcArguments pcArguments, PostSaveAction postSaveAction, bool activateOpcaoCancelar)
        {
            return save(preTransactionAction, pcDelegate, pcArguments, null, null, postSaveAction, activateOpcaoCancelar);
        }

        public static SaveResult save(preSaveDelegate psDelegate, PreSaveArguments psArguments, PostSaveAction postSaveAction, bool activateOpcaoCancelar)
        {
            return save(null, null, null, psDelegate, psArguments, postSaveAction, activateOpcaoCancelar);
        }

        public static SaveResult save(preSaveDelegate psDelegate, PreSaveArguments psArguments, PostSaveAction postSaveAction)
        {
            return save(null, null, null, psDelegate, psArguments, postSaveAction, false);
        }

        public static SaveResult save(preSaveDelegate psDelegate, PreSaveArguments psArguments)
        {
            return save(null, null, null, psDelegate, psArguments, null, false);
        }
        #endregion

        public static SaveResult save(PreTransactionAction preTransactionAction, preConcDelegate pcDelegate, PreConcArguments pcArguments, preSaveDelegate psDelegate, PreSaveArguments psArguments, PostSaveAction postSaveAction, bool activateOpcaoCancelar)
		{
			Concorrencia conc = new Concorrencia();
			IDbTransaction tran = null;
            SaveResult successfulSave = SaveResult.successful;
			GisaDataSetHelper.HoldOpen ho = null;
			long startTicks = 0;
			bool savedWithoutDeadlock = false;
			// Variavel que indica qual a tabela que está a ser gravada (tem o debugging como fim)
			string currentTable = string.Empty;
			DataSet gBackup = null;
			ArrayList changedRowsArrayList = null;
			// Variável que vai manter a informação das linhas "added" cujos Ids são gerados automaticamente
			// antes e depois de serem gravadas (antes de essas linhas serem gravadas os seus IDs são
			// negativos e depois são-lhe atribuidos valores positivos
			Hashtable trackNewIds = new Hashtable();

            try
            {
                if (preTransactionAction != null)
                {
                    preTransactionAction.ExecuteAction();
                    if (preTransactionAction.args.cancelAction)
                    {
                        MessageBox.Show(preTransactionAction.args.message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        GisaDataSetHelper.GetInstance().RejectChanges();
                        return SaveResult.nothingToSave;
                    }
                }
            }
            catch (Exception e) { 
                Trace.WriteLine(e.ToString());
                return SaveResult.unsuccessful; }
            finally { }



			while (! savedWithoutDeadlock)
			{
				try
				{
					if (pcDelegate != null)
					{
						ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
						tran = ho.Connection.BeginTransaction(GisaDataSetHelper.GetTransactionIsolationLevel());
						pcArguments.tran = tran;
						gBackup = new DataSet();
						gBackup.CaseSensitive = true;
						pcArguments.gisaBackup = gBackup;
						pcArguments.continueSave = true;
						pcDelegate(pcArguments);

						//no caso de se pretender apagar uma relacao hierarquica ha a possibilidade de 
						//não ser necessário prosseguir com a gravação dos dados por razoes de logica
						//(ver o delegate verifyIfCanDeleteRH)
						if (! pcArguments.continueSave)
						{
							tran.Commit();
							savedWithoutDeadlock = true;
                            successfulSave = SaveResult.unsuccessful;
                            break;
						}
					}

					//verifica logo à cabeça se houve de facto alguma alteração ao dataset
					if (! (GisaDataSetHelper.GetInstance().HasChanges()))
					{
                        return SaveResult.nothingToSave;
					}

					//obter um arraylist com estruturas que indicam para cada tabela quais as linhas que foram alteradas
					changedRowsArrayList = getCurrentDatasetChanges(conc);
					if (changedRowsArrayList == null)
					{
                        return SaveResult.nothingToSave;
					}

					while (true)
					{
						if (ho == null)
						{
							ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
						}
						if (tran == null)
						{
							tran = ho.Connection.BeginTransaction(GisaDataSetHelper.GetTransactionIsolationLevel());
						}

						//manter 2 datasets com linhas provenientes da bd
						//isto para permitir a detecção de novos e possiveis conflitos qd o utilizador perante 1 problema de concorrencia pretende manter as suas alterações
						//a estratégia passa por verificar se houve alterações no 1º (e principal) dataset com as linhas da bd
						if (originalRowsDB1 == null)
						{
							//guardar as linhas neste dataset

							//FIXME: changedRowsArrayList esta a chegar com tabelas repetidas (!)
							originalRowsDB1 = conc.getOriginalRowsDB(changedRowsArrayList, tran);
						}
						else if (originalRowsDB1 != null && originalRowsDB2 == null)
						{
							//o dataset principal (originalRowsDB1) ja esta preenchido e o utilizador pretende manter as suas alterações

							//o dataset principal (originalRowsDB1) ja esta preenchido e o utilizador pretende manter as suas alterações
							// as linhas da BD vao parar ao 2º dataset
							originalRowsDB2 = conc.getOriginalRowsDB(changedRowsArrayList, tran);



							if (originalRowsDB2 == null)
							{
								originalRowsDB1 = null;
							}
						}
						else if (originalRowsDB1 != null && originalRowsDB2 != null)
						{
							//os 2 datasets estao preenchidos (o que quer dizer que estamos perante a 2ª situação de concorrencia consecutiva (no mínimo) na mesma mudança de contexto)

							//nesta situação o dataset principal passa a ter as alterações contidas no 2º para que neste último passem a constar os novos dados provenientes da BD
							//se não existirem novas linhas, quer dizer que as alterações feitas em memória coincidem com aquelas existentes na BD

							//verificar se existem novas linhas
							if (! (conc.getOriginalRowsDB(changedRowsArrayList, tran) == null))
							{
								originalRowsDB1 = originalRowsDB2.Copy();
								originalRowsDB2 = conc.getOriginalRowsDB(changedRowsArrayList, tran);
							}
							else
							{
								originalRowsDB1 = null;
								originalRowsDB2 = null;
							}
						}

						//se não existirem conflitos de concorrencia
						if (! ((originalRowsDB1 != null && originalRowsDB2 == null && conc.temLinhas(originalRowsDB1)) || (originalRowsDB1 != null && originalRowsDB2 != null && conc.wasModified(originalRowsDB1, originalRowsDB2, changedRowsArrayList))))
						{
							//não há concorrência quando o originalRowsDB1 não tem linhas e originalRowsDB2 está vazio ou quando os dois datasets são diferentes

							break;
						}
						else
						{
							//caso existam conflitos, é apresentado ao utilizador uma mensagem a indicar os pontos de conflito e de que forma os pretende resolver


							//ToDo: verificar a necessidade de fazer rollback
							tran.Rollback();
							ho.Dispose();
							ho = null;
							tran = null;

							frm.DetalhesUser = Concorrencia.StrConcorrenciaUser.ToString();
							frm.DetalhesBD = Concorrencia.StrConcorrenciaBD.ToString();
							frm.btnCancel.Enabled = activateOpcaoCancelar;

							switch (frm.ShowDialog())
							{
								case DialogResult.Yes:
									//mantem-se dentro do ciclo de forma a voltar a verificar se existe concorrencia ou nao
									//ao mesmo tempo que este tratamento de conflitos e executado outro utilizador pode já ter feito novas alterações                           

									//é necessario limpar as variaveis que mantem as mensagens sobre a concorrencia para o caso de neste situação ainda existirem novos situações de conflito
									Concorrencia.StrConcorrenciaBD.Remove(0, Concorrencia.StrConcorrenciaBD.Length);
									Concorrencia.StrConcorrenciaUser.Remove(0, Concorrencia.StrConcorrenciaUser.Length);

									break;
								case DialogResult.No:
									//gravar em memoria as linhas obtidas da base de dados e sai do metodo
									if (originalRowsDB2 != null)
									{
										//se no dataset originalRowsDB2 existirem linhas, logo é este que vai ser gravado
										conc.MergeDatasets(originalRowsDB2, GisaDataSetHelper.GetInstance(), DataSetTablesOrderedA);
									}
									else
									{
                                        conc.MergeDatasets(originalRowsDB1, GisaDataSetHelper.GetInstance(), DataSetTablesOrderedA);
									}
									conc.ClearRowsChangedToModified();
									cleanConcurrencyVariables();

									return successfulSave;
								case DialogResult.Cancel:
									cleanConcurrencyVariables();

                                    successfulSave = SaveResult.cancel;
									return successfulSave;
							}
						}
					}

					startTicks = DateTime.Now.Ticks;

					if (Concorrencia.StrConcorrenciaLinhasNaoGravadas.Length > 0)
					{
						MessageBox.Show("A informação referente aos campos seguintes não pode ser gravada " + Environment.NewLine + "por ter sido, entretanto, eliminada: " + System.Environment.NewLine + Concorrencia.StrConcorrenciaLinhasNaoGravadas.ToString(), "Gravação de dados.");
					}

					// Chamar qualquer tarefa de "pré-gravação" que possa ter sido definida
					if (psDelegate != null)
					{
						psArguments.tran = tran;
						psDelegate(psArguments);
					}

					// forma de manter a informação referente à actualização dos Ids das linhas quando 
					// estas são adicionadas na base de dados, isto é, saber qual o valor (negativo) 
					// do ID antes da linha ser gravada e o valor (positivo) atribuído pela base de dados
					// depois do save
					trackNewIds.Clear();
					conc.startTrackingIdsAddedRows(GisaDataSetHelper.GetInstance(), changedRowsArrayList, ref trackNewIds);

					// garantir que filhos das linhas a eliminar que não estão carregados ficam também eles eliminados
					//ToDo: em getChildRowsFromDB utilizar dataset de trabalho para obter as relações entre tabelas. Passar ao metodo o origRowsDB para que as linhas obtidas lhe sejam directamente adicionadas
					ArrayList rows = new ArrayList();
					ArrayList afectedTables = new ArrayList();
					GisaDataSetHelper.ManageDatasetConstraints(false);
					foreach (Concorrencia.changedRows changedRow in changedRowsArrayList)
					{
						currentTable = changedRow.tab;
						if (changedRow.rowsDel.Count > 0)
						{
							cascadeManageChildDeletedRows(changedRow.tab, changedRow.rowsDel, conc, tran);
						}

						// alterar o estado das Rowstate.Deleted para Rowstate.Unchanged e passa-las a isDeleted=True
						DataRow delRow = null;

						if (changedRow.rowsDel.Count > 0)
						{

							while (changedRow.rowsDel.Count > 0)
							{
                                if (changedRow.tab.Equals("ControloAutDataDeDescricao") || changedRow.tab.Equals("FRDBaseDataDeDescricao"))
                                {
                                    delRow = (DataRow)(changedRow.rowsDel[0]);
                                    delRow.RejectChanges();
                                    changedRow.rowsDel.RemoveAt(0);
                                }
                                else
                                {
                                    delRow = (DataRow)(changedRow.rowsDel[0]);
                                    delRow.RejectChanges();
                                    delRow["isDeleted"] = 1;
                                    changedRow.rowsMod.Add(delRow);
                                    changedRow.rowsDel.RemoveAt(0);
                                }
							}
						}

						rows.Clear();
						rows.AddRange(changedRow.rowsAdd);
						rows.AddRange(changedRow.rowsMod);
						PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().Tables[changedRow.tab], (DataRow[])(rows.ToArray(typeof(DataRow))), tran);
					}

                    if (postSaveAction != null)
                    {
                        postSaveAction.args.tran = tran;
                        postSaveAction.ExecuteAction();
                    }

					conc.ClearRowsChangedToModified();
					GisaDataSetHelper.ManageDatasetConstraints(true);
					tran.Commit();
					savedWithoutDeadlock = true;
					Debug.WriteLine("Save: " + new TimeSpan(DateTime.Now.Ticks - startTicks).ToString());
					Trace.WriteLine("Save completed.");
				}
				catch (Exception ex)
				{
                    Trace.WriteLine(ex);

					//GisaDataSetHelper.GetInstance().RejectChanges()
					if (DBAbstractDataLayer.DataAccessRules.ExceptionHelper.isDeadlockException(ex))
					{
						Trace.WriteLine(">>> Deadlock (save).");
						conc.prepareRollBackDataSet(ref trackNewIds);
						tran.Rollback();
						tran = null;
						if (conc.mGisaBackup != null && gBackup != null)
						{
                            conc.MergeDatasets(gBackup, conc.gisabackup, DataSetTablesOrderedA);
						}
						if (conc.mGisaBackup != null)
						{
                            conc.MergeDatasets(conc.mGisaBackup, GisaDataSetHelper.GetInstance(), DataSetTablesOrderedA, trackNewIds);
						}
						conc.deleteUnusedRows(GisaDataSetHelper.GetInstance(), ref trackNewIds);
						gBackup = null;
					}
					else if (DBAbstractDataLayer.DataAccessRules.ExceptionHelper.isTimeoutException(ex))
					{
						Trace.WriteLine(">>> Timeout (save).");
						conc.prepareRollBackDataSet(ref trackNewIds);
						tran = null;
						if (conc.mGisaBackup != null && gBackup != null)
						{
                            conc.MergeDatasets(gBackup, conc.gisabackup, DataSetTablesOrderedA);
						}
						if (conc.mGisaBackup != null)
						{
                            conc.MergeDatasets(conc.mGisaBackup, GisaDataSetHelper.GetInstance(), DataSetTablesOrderedA, trackNewIds);
						}
						conc.deleteUnusedRows(GisaDataSetHelper.GetInstance(), ref trackNewIds);
						gBackup = null;
					}
					else
					{
	#if DEBUG
						Trace.WriteLine(currentTable);
	#endif
						Trace.WriteLine("Save failed.");
						Trace.WriteLine(ex);
						Debug.Assert(false, "Save failed.");

						if (tran != null)
						{
							tran.Rollback();
						}
						tran = null;

						MessageBox.Show("Ocorreu um erro inesperado durante a gravação " + Environment.NewLine + "da informação e por esse motivo a aplicação irá " + Environment.NewLine + "fechar. Por favor contacte o administrador do sistema", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw;
					}
				}
				finally
				{
					if (tran != null)
					{
						tran.Dispose();
					}

					cleanConcurrencyVariables();

					if (savedWithoutDeadlock)
					{
						conc.mGisaBackup = null;
					}

					if (ho != null)
					{
						ho.Dispose();
						ho = null;
					}
				}
			}


			return successfulSave;
		}

        public static void SimpleSave(DataTable table, DataRow[] rows)
        {
            var ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            var tran = ho.Connection.BeginTransaction(GisaDataSetHelper.GetTransactionIsolationLevel());

            try
            {
                DBAbstractDataLayer.DataAccessRules.PersistencyHelperRule.Current.saveRows(table, rows, tran);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
                tran.Rollback();
            }
            finally
            {
                tran.Commit();
                ho.Dispose();
            }
            
        }

		private static ArrayList getCurrentDatasetChanges(Concorrencia conc)
		{
			//verifica logo à cabeça se houve de facto alguma alteração ao dataset
			if (! (GisaDataSetHelper.GetInstance().HasChanges()))
			{
				return null;
			}

			//obter um arraylist com estruturas que indicam para cada tabela quais as linhas que foram alteradas
            ArrayList changedRowsArrayList = conc.getAlteracoes(GisaDataSetHelper.GetInstance(), DataSetTablesOrderedA);
			if (! (changedRowsArrayList.Count > 0))
			{
				return null;
			}
			else
			{
				return changedRowsArrayList;
			}
		}

		public static bool hasCurrentDatasetChanges()
		{
			ArrayList changes = getCurrentDatasetChanges(new Concorrencia());
			return changes != null;
		}

		private static void cascadeManageChildDeletedRows(string tableName, ArrayList rows, Concorrencia conc, IDbTransaction tran)
		{
			ArrayList rowsToBeSaved = new ArrayList();
			DBAbstractDataLayer.DataAccessRules.ConcorrenciaRule.ChildRelationRows childRelRows = new DBAbstractDataLayer.DataAccessRules.ConcorrenciaRule.ChildRelationRows();
			childRelRows = DBAbstractDataLayer.DataAccessRules.ConcorrenciaRule.Current.getChildRowsFromDB(GisaDataSetHelper.GetInstance(), GisaDataSetHelper.GetInstance().Tables[tableName], rows, tran); // obter as linhas filhas das linhas eliminadas, podem também elas precisar de ser eliminadas
			// iterar sobre todas as tabelas filhas (no DS) da recebida em tableName
//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of childRelRows.tables.Count for every iteration:
			int tempFor1 = childRelRows.tables.Count;
			for (int i = 0; i < tempFor1; i++)
			{
				DataTable childTable = (DataTable)(childRelRows.tables[i]);
				rowsToBeSaved.Clear();
				// iterar sobre todas as linhas das tabelas filhas
				foreach (DataRow row in childTable.Rows)
				{

					if (! (makeNullableColumnsNull(row, (DataColumn[])(childRelRows.relationColumns[i]))))
					{
						cascadeManageChildDeletedRows(childTable.TableName, new ArrayList(new DataRow[] {row}), conc, tran);
						row["isDeleted"] = 1;
					}
					rowsToBeSaved.Add(row);
				}
				PersistencyHelperRule.Current.saveRows(GisaDataSetHelper.GetInstance().Tables[childTable.TableName], (DataRow[])(rowsToBeSaved.ToArray(typeof(DataRow))), tran);
			}
		}

		private static bool makeNullableColumnsNull(DataRow row, DataColumn[] columns)
		{
			bool result = false;
			foreach (DataColumn column in columns)
			{
				if (column.AllowDBNull)
				{
					row[column.ColumnName] = DBNull.Value;
					result = true;
				}
			}
			return result;
		}

		public static TableDepthOrdered.TableCloudType determinaNuvem(string dt)
		{
            foreach (TableDepthOrdered.tableDepth t in DataSetTablesOrderedA)
			{
				if (dt.Equals(t.tab.TableName))
				{
					return t.nuvem;
				}
			}
			return 0;
		}

        public class DeletedData
        {
            public Dictionary<DataTable, byte[]> latestTimestamps = new Dictionary<DataTable, byte[]>();
            private static DeletedData instance;
            private DeletedData() { }
            public static DeletedData Instance
            {
                get
                {
                    if (instance == null)
                        instance = new DeletedData();

                    return instance;
                }
            }
        }

		public static void cleanDeletedData()
		{
            cleanDeletedData(TableDepthOrdered.TableCloudType.All);
        }

        public static void cleanDeletedData(TableDepthOrdered.TableCloudType nuvem)
        {
            
            if (nuvem == TableDepthOrdered.TableCloudType.All)
            {
                long start = DateTime.Now.Ticks;
                GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
                DataTable currTable = null;
                try
                {
                       
                    GISADataset currentData = GisaDataSetHelper.GetInstance();
                    GisaDataSetHelper.ManageDatasetConstraints(false);
                    
                    ArrayList reverseOrderedTables = tdo.getTabelasOrdenadas(true); // obter uma lista de tabelas ordenada de baixo para cima (dos filhos para os pais)
                    foreach (TableDepthOrdered.tableDepth orderedTable in reverseOrderedTables)
                    {
                        currTable = currentData.Tables[orderedTable.tab.TableName];
                        byte[] ts = null;
                        if (DeletedData.Instance.latestTimestamps.ContainsKey(currTable))
                            ts = DeletedData.Instance.latestTimestamps[currTable];
                        var new_max_ts = PersistencyHelperRule.Current.CleanDatasetDeletedData(currentData, currTable, ts, ho.Connection);
                        DeletedData.Instance.latestTimestamps[currTable] = new_max_ts;
                    }
                    GisaDataSetHelper.ManageDatasetConstraints(true);
                }
                catch (ConstraintException ex)
                {
                    Trace.WriteLine("<EnforceContraints>");
                    Debug.WriteLine(">>> " + currTable.TableName);
                    Trace.WriteLine(ex.ToString());
                    GisaDataSetHelper.FixDataSet(GisaDataSetHelper.GetInstance(), ho.Connection);
#if DEBUG
                    throw;
#endif
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Exception in cleanDeletedData: " + ex.ToString());
                    throw;
                }
                finally
                {
                    ho.Dispose();                
                }
                Trace.WriteLine(">>>>  Clean Deleted Data in: " + new TimeSpan(DateTime.Now.Ticks - start).ToString());
            }
            else
            {
                cleanDeletedData(new List<TableDepthOrdered.TableCloudType>(new TableDepthOrdered.TableCloudType[]{ nuvem }));
            }

        }
        
        public static void cleanDeletedData(List<TableDepthOrdered.TableCloudType> nuvens)
        {
            long start = DateTime.Now.Ticks;
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            GISADataset currentData = GisaDataSetHelper.GetInstance();
            DataTable currTable = null;
            List<TableDepthOrdered.tableDepth> orderedTables;

            try
            {
                GisaDataSetHelper.ManageDatasetConstraints(false);

                if (!cacheNuvens.ContainsKey(nuvens))
                {
                    List<TableDepthOrdered.tableDepth> tmp = new List<TableDepthOrdered.tableDepth>();
                    ArrayList reverseOrderedTables = tdo.getTabelasOrdenadas(true); // obter uma lista de tabelas ordenada de baixo para cima (dos filhos para os pais)
                    foreach (TableDepthOrdered.tableDepth orderedTable in reverseOrderedTables)
                    {
                        if (nuvens.Contains(orderedTable.nuvem))
                        {
                            currTable = currentData.Tables[orderedTable.tab.TableName];
                            byte[] ts = null;
                            if (DeletedData.Instance.latestTimestamps.ContainsKey(currTable))
                                ts = DeletedData.Instance.latestTimestamps[currTable];
                            var new_max_ts = PersistencyHelperRule.Current.CleanDatasetDeletedData(currentData, currTable, ts, ho.Connection);
                            DeletedData.Instance.latestTimestamps[currTable] = new_max_ts;
                            tmp.Add(orderedTable);
                        }
                    }
                    cacheNuvens[nuvens] = tmp;
                    return;
                }
                orderedTables = cacheNuvens[nuvens];

                foreach (TableDepthOrdered.tableDepth orderedTable in orderedTables)
                {
                    currTable = currentData.Tables[orderedTable.tab.TableName];
                    byte[] ts = null;
                    if (DeletedData.Instance.latestTimestamps.ContainsKey(currTable))
                        ts = DeletedData.Instance.latestTimestamps[currTable];
                    var new_max_ts = PersistencyHelperRule.Current.CleanDatasetDeletedData(currentData, currTable, ts, ho.Connection);
                    DeletedData.Instance.latestTimestamps[currTable] = new_max_ts;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in cleanDeletedData: " + ex.ToString());
                throw;
            }
            finally
            {
                ho.Dispose();
                GisaDataSetHelper.ManageDatasetConstraints(true);
            }
            Trace.WriteLine("Clean Deleted Data in: " + new TimeSpan(DateTime.Now.Ticks - start).ToString());            
        }

		private static void cleanConcurrencyVariables()
		{
			originalRowsDB1 = null;
			originalRowsDB2 = null;
			Concorrencia.StrConcorrenciaBD.Remove(0, Concorrencia.StrConcorrenciaBD.Length);
			Concorrencia.StrConcorrenciaUser.Remove(0, Concorrencia.StrConcorrenciaUser.Length);
		}

		// método responsável por guardar uma cópia de linhas a serem gravadas de forma a ser possível 
		// fazer rollback ao dataset de trabalho em caso de excepções em situações de concorrência
		public static void BackupRow(ref DataSet gisaBackup, DataRow row)
		{
			if (! (gisaBackup.Tables.Contains(row.Table.TableName)))
			{
				gisaBackup.Tables.Add(row.Table.Clone());
			}
			gisaBackup.Tables[row.Table.TableName].ImportRow(row);
		}

        public static void BackupRows(ref DataSet gisaBackup, List<DataRow> rows)
        {
            foreach (DataRow row in rows)
                BackupRow(ref gisaBackup, row);
        }
	}

} //end of root namespace