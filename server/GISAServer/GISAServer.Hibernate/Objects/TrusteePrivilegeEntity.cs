using System;
using System.Collections.Generic;
using System.ComponentModel;
using Iesi.Collections;
using Iesi.Collections.Generic;
using GISAServer.Hibernate.Utils;
using GISAServer.Hibernate.Exceptions;

namespace GISAServer.Hibernate.Objects
{    
	/// <summary>
	/// An object representation of the TrusteePrivilege table
	/// </summary>
	[Serializable]
	public partial class TrusteePrivilegeEntity
	{
		private QuadIdComponent _Id;

		//private FunctionOperationEntity _FKTrusteePrivilegeFunctionOperation;
		private System.Boolean _IsDeleted;
		private System.Boolean _IsGrant;
		//private FunctionOperationEntity _TipoFunctionGroup;
		//private FunctionOperationEntity _TipoOperation;
		private TrusteeEntity _Trustee;
		private System.Byte[] _Versao;
		//private FunctionOperationEntity _TipoFunction;

		/*public virtual FunctionOperationEntity FKTrusteePrivilegeFunctionOperation
		{
			get
			{
				return _FKTrusteePrivilegeFunctionOperation;
			}
			set
			{
				_FKTrusteePrivilegeFunctionOperation = value;
			}
		}*/
		public virtual QuadIdComponent Id
		{
			get
			{
				return _Id;
			}
			set
			{
				_Id = value;
			}
		}

		public virtual System.Boolean IsDeleted
		{
			get
			{
				return _IsDeleted;
			}
			set
			{
				_IsDeleted = value;
			}
		}

		public virtual System.Boolean IsGrant
		{
			get
			{
				return _IsGrant;
			}
			set
			{
				_IsGrant = value;
			}
		}

		/*public virtual FunctionOperationEntity TipoFunctionGroup
		{
			get
			{
				return _TipoFunctionGroup;
			}
			set
			{
				_TipoFunctionGroup = value;
			}
		}

		public virtual FunctionOperationEntity TipoOperation
		{
			get
			{
				return _TipoOperation;
			}
			set
			{
				_TipoOperation = value;
			}
		}
        */
		public virtual TrusteeEntity Trustee
		{
			get
			{
				return _Trustee;
			}
			set
			{
				_Trustee = value;
			}
		}

		public virtual System.Byte[] Versao
		{
			get
			{
				return _Versao;
			}
			set
			{
				_Versao = value;
			}
		}
        /*
		public virtual FunctionOperationEntity TipoFunction
		{
			get
			{
				return _TipoFunction;
			}
			set
			{
				_TipoFunction = value;
			}
		}
        */

		protected bool Equals(TrusteePrivilegeEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as TrusteePrivilegeEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
