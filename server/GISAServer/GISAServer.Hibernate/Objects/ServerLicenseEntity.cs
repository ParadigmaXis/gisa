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
	/// An object representation of the ServerLicense table
	/// </summary>
	[Serializable]
	public partial class ServerLicenseEntity
	{
		private System.String _Id;

		private System.String _DatabaseVersion;
		private System.Boolean _IsDeleted;
		private TipoServerEntity _TipoServer;
		private System.Byte[] _Versao;

		public virtual System.String DatabaseVersion
		{
			get
			{
				return _DatabaseVersion;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException("DatabaseVersion must not be null.");
				}
				_DatabaseVersion = value;
			}
		}

		public virtual System.String Id
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

		public virtual TipoServerEntity TipoServer
		{
			get
			{
				return _TipoServer;
			}
			set
			{
				_TipoServer = value;
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


		protected bool Equals(ServerLicenseEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as ServerLicenseEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
