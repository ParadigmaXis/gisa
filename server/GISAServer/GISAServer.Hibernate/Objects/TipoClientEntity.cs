using System;
using System.ComponentModel;
using Iesi.Collections;
using Iesi.Collections.Generic;
using GISAServer.Hibernate.Utils;
using GISAServer.Hibernate.Exceptions;

namespace GISAServer.Hibernate.Objects
{    
	/// <summary>
	/// An object representation of the TipoClient table
	/// </summary>
	[Serializable]
	public partial class TipoClientEntity
	{
		private System.Int64 _Id;

		private System.String _BuiltInName;
		private readonly ISet<ClientLicenseEntity> _FKClientLicenseTipoClient1 = new HashedSet<ClientLicenseEntity>();
		private System.Boolean _IsDeleted;
		private System.String _Name;
		private System.Byte[] _Versao;

		public virtual System.String BuiltInName
		{
			get
			{
				return _BuiltInName;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException("BuiltInName must not be null.");
				}
				_BuiltInName = value;
			}
		}

		public virtual ISet<ClientLicenseEntity> FKClientLicenseTipoClient1
		{
			get
			{
				return _FKClientLicenseTipoClient1;
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

		public virtual System.String Name
		{
			get
			{
				return _Name;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException("Name must not be null.");
				}
				_Name = value;
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


		protected bool Equals(TipoClientEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as TipoClientEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
