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
	/// An object representation of the TrusteeNivelPrivilege table
	/// </summary>
	[Serializable]
	public partial class TrusteeNivelPrivilegeEntity
	{
		private TripleIdComponent _Id;

		private System.Boolean _IsDeleted;
		private System.Boolean _IsGrant;
		private NivelEntity _Nivel;
		private NivelTipoOperationEntity _NivelTipoOperation;
		private TrusteeEntity _Trustee;
		private System.Byte[] _Versao;

		public virtual TripleIdComponent Id
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

		public virtual NivelEntity Nivel
		{
			get
			{
				return _Nivel;
			}
			set
			{
				_Nivel = value;
			}
		}

		public virtual NivelTipoOperationEntity NivelTipoOperation
		{
			get
			{
				return _NivelTipoOperation;
			}
			set
			{
				_NivelTipoOperation = value;
			}
		}

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


		protected bool Equals(TrusteeNivelPrivilegeEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as TrusteeNivelPrivilegeEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
