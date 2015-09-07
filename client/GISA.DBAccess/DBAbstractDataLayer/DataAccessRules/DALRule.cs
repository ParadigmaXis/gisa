using System;
using System.Collections;

namespace DBAbstractDataLayer.DataAccessRules
{
	using DBAbstractDataLayer.ConnectionBuilders;
	public abstract class DALRule
	{
		public static Builder ConnectionBuilder = null;
		public static IMetaModelProvider MetaModel = null;

		#region Rules Configuration
		private static Hashtable connections = new Hashtable();
		private static void AddConnection(Type type) 
		{
			connections.Add(type, new Hashtable());
		}
		private static Hashtable GetConnection(Type type) 
		{
			return (Hashtable) connections[type];
		}
		static DALRule() 
		{
			AddConnection(typeof(SqlClientBuilder));
            GetConnection(typeof(SqlClientBuilder)).Add(typeof(AutoEliminacaoRule), typeof(SqlClient.SqlClientAutoEliminacaoRule));
			GetConnection(typeof(SqlClientBuilder)).Add(typeof(ConcorrenciaRule), typeof(SqlClient.SqlClientConcorrenciaRule));
			GetConnection(typeof(SqlClientBuilder)).Add(typeof(ControloAutRule), typeof(SqlClient.SqlClientControloAutRule));
			GetConnection(typeof(SqlClientBuilder)).Add(typeof(DiplomaModeloRule), typeof(SqlClient.SqlClientDiplomaModeloRule));
            GetConnection(typeof(SqlClientBuilder)).Add(typeof(EstatisticasRule), typeof(SqlClient.SqlClientEstatisticasRule));
			GetConnection(typeof(SqlClientBuilder)).Add(typeof(FRDRule), typeof(SqlClient.SqlClientFRDRule));
			GetConnection(typeof(SqlClientBuilder)).Add(typeof(GisaDataSetHelperRule), typeof(SqlClient.SqlClientGisaDataSetHelperRule));
			GetConnection(typeof(SqlClientBuilder)).Add(typeof(GisaInstallerRule), typeof(SqlClient.SqlClientGisaInstallerRule));
			GetConnection(typeof(SqlClientBuilder)).Add(typeof(GISATreeNodeRule), typeof(SqlClient.SqlClientGISATreeNodeRule));
            GetConnection(typeof(SqlClientBuilder)).Add(typeof(IntGestDocRule), typeof(SqlClient.SqlClientIntGestDocRule));
			GetConnection(typeof(SqlClientBuilder)).Add(typeof(NivelRule), typeof(SqlClient.SqlClientNivelRule));
			GetConnection(typeof(SqlClientBuilder)).Add(typeof(PermissoesRule), typeof(SqlClient.SqlClientPermissoesRule));
			GetConnection(typeof(SqlClientBuilder)).Add(typeof(PersistencyHelperRule), typeof(SqlClient.SqlClientPersistencyHelperRule));
			GetConnection(typeof(SqlClientBuilder)).Add(typeof(PesquisaRule), typeof(SqlClient.SqlClientPesquisaRule));
			GetConnection(typeof(SqlClientBuilder)).Add(typeof(RelatorioRule), typeof(SqlClient.SqlClientRelatorioRule));
            GetConnection(typeof(SqlClientBuilder)).Add(typeof(MovimentoRule), typeof(SqlClient.SqlClientMovimentoRule));
			GetConnection(typeof(SqlClientBuilder)).Add(typeof(TipoNivelRule), typeof(SqlClient.SqlClientTipoNivelRule));
			GetConnection(typeof(SqlClientBuilder)).Add(typeof(TrusteeRule), typeof(SqlClient.SqlClientTrusteeRule));
			GetConnection(typeof(SqlClientBuilder)).Add(typeof(UFRule), typeof(SqlClient.SqlClientUFRule));
            GetConnection(typeof(SqlClientBuilder)).Add(typeof(EADGeneratorRule), typeof(SqlClient.SqlClientEADGeneratorRule));
            GetConnection(typeof(SqlClientBuilder)).Add(typeof(DepositoRule), typeof(SqlClient.SqlClientDepositoRule));
            GetConnection(typeof(SqlClientBuilder)).Add(typeof(ImportRule), typeof(SqlClient.SqlClientImportRule));
            GetConnection(typeof(SqlClientBuilder)).Add(typeof(FedoraRule), typeof(SqlClient.SqlClientFedoraRule));
            GetConnection(typeof(SqlClientBuilder)).Add(typeof(PaginatedListRule), typeof(SqlClient.SqlClientPaginatedListRule));
            GetConnection(typeof(SqlClientBuilder)).Add(typeof(ObjectoDigitalStatusRule), typeof(SqlClient.SqlClientObjectoDigitalStatusRule));
		}
		#endregion

		protected static DALRule Create(Type dalType)
		{
			if (Object.ReferenceEquals(DALRule.ConnectionBuilder, null))
			{
				throw new InvalidOperationException("The ConnectionBuilder is not set.");
			}
			Type runtimeType = (Type) GetConnection(ConnectionBuilder.GetType())[dalType];
			if (Object.ReferenceEquals(runtimeType, null) || !dalType.IsAssignableFrom(runtimeType)) 
			{
				throw new TypeLoadException(string.Format("Can't find implementation for type {0}.", dalType.FullName));
			}
			return (DALRule) Activator.CreateInstance(runtimeType);
		}

	}
}
