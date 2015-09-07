using System;
using System.ComponentModel;
using Iesi.Collections;
using Iesi.Collections.Generic;
using GISAServer.Hibernate.Utils;
using GISAServer.Hibernate.Exceptions;

namespace GISAServer.Hibernate.Objects
{    
	/// <summary>
	/// An object representation of the TrusteeUser table
	/// </summary>
	[Serializable]
	public partial class TrusteeUserEntity
	{
		private System.Int64 _Id;

		private readonly ISet<ControloAutDataDeDescricaoEntity> _FKControloAutDataDeDescricaoTrusteeUser = new HashedSet<ControloAutDataDeDescricaoEntity>();
		private readonly ISet<FRDBaseDataDeDescricaoEntity> _FKFRDBaseDataDeDescricaoTrusteeUser = new HashedSet<FRDBaseDataDeDescricaoEntity>();
		private readonly ISet<TrusteeUserEntity> _FKTrusteeUserTrusteeUser = new HashedSet<TrusteeUserEntity>();
		private System.String _FullName;
		private TrusteeEntity _ID;
		private System.Boolean _IsAuthority;
		private System.Boolean _IsDeleted;
		private System.String _Password;
		private TrusteeUserEntity _TrusteeUserDefaultAuthority;
		private System.Byte[] _Versao;

		public virtual ISet<ControloAutDataDeDescricaoEntity> FKControloAutDataDeDescricaoTrusteeUser
		{
			get
			{
				return _FKControloAutDataDeDescricaoTrusteeUser;
			}
		}

		public virtual ISet<FRDBaseDataDeDescricaoEntity> FKFRDBaseDataDeDescricaoTrusteeUser
		{
			get
			{
				return _FKFRDBaseDataDeDescricaoTrusteeUser;
			}
		}

		public virtual ISet<TrusteeUserEntity> FKTrusteeUserTrusteeUser
		{
			get
			{
				return _FKTrusteeUserTrusteeUser;
			}
		}

		public virtual System.String FullName
		{
			get
			{
				return _FullName;
			}
			set
			{
				_FullName = value;
			}
		}

		public virtual System.Int64 Id
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

		public virtual TrusteeEntity ID
		{
			get
			{
				return _ID;
			}
			set
			{
				_ID = value;
			}
		}
		public virtual System.Boolean IsAuthority
		{
			get
			{
				return _IsAuthority;
			}
			set
			{
				_IsAuthority = value;
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

		public virtual System.String Password
		{
			get
			{
				return _Password;
			}
			set
			{
				_Password = value;
			}
		}

		public virtual TrusteeUserEntity TrusteeUserDefaultAuthority
		{
			get
			{
				return _TrusteeUserDefaultAuthority;
			}
			set
			{
				_TrusteeUserDefaultAuthority = value;
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


		protected bool Equals(TrusteeUserEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as TrusteeUserEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
