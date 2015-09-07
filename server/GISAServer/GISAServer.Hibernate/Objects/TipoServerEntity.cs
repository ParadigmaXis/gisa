using System;
using System.ComponentModel;
using Iesi.Collections;
using Iesi.Collections.Generic;
using GISAServer.Hibernate.Utils;
using GISAServer.Hibernate.Exceptions;

namespace GISAServer.Hibernate.Objects
{    
	/// <summary>
	/// An object representation of the TipoServer table
	/// </summary>
	[Serializable]
	public partial class TipoServerEntity
	{
		private System.Int64 _Id;

		private System.String _BuiltInName;
		private readonly ISet<ServerLicenseEntity> _FKServerLicenseTipoServer = new HashedSet<ServerLicenseEntity>();
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

		public virtual ISet<ServerLicenseEntity> FKServerLicenseTipoServer
		{
			get
			{
				return _FKServerLicenseTipoServer;
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


	}
}
