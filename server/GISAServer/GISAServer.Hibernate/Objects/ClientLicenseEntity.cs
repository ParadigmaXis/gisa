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
	/// An object representation of the ClientLicense table
	/// </summary>
	[Serializable]
	public partial class ClientLicenseEntity
	{
		private PairIdComponent _Id;

		private System.String _AssemblyVersion;
		private System.Int16 _FloatingLicensesCount;
		private System.DateTime _GrantDate;
		private System.String _GranterName;
		private System.Boolean _IsDeleted;
		private System.Int32 _SequenceNumber;
		private System.String _SerialNumber;
		private TipoClientEntity _TipoClient;
		private System.Byte[] _Versao;

		public virtual System.String AssemblyVersion
		{
			get
			{
				return _AssemblyVersion;
			}
			set
			{
				_AssemblyVersion = value;
			}
		}

		public virtual System.Int16 FloatingLicensesCount
		{
			get
			{
				return _FloatingLicensesCount;
			}
			set
			{
				_FloatingLicensesCount = value;
			}
		}

		public virtual System.DateTime GrantDate
		{
			get
			{
				return _GrantDate;
			}
			set
			{
				_GrantDate = value;
			}
		}

		public virtual System.String GranterName
		{
			get
			{
				return _GranterName;
			}
			set
			{
				_GranterName = value;
			}
		}

		public virtual PairIdComponent Id
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

		public virtual System.Int32 SequenceNumber
		{
			get
			{
				return _SequenceNumber;
			}
			set
			{
				_SequenceNumber = value;
			}
		}

		public virtual System.String SerialNumber
		{
			get
			{
				return _SerialNumber;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException("SerialNumber must not be null.");
				}
				_SerialNumber = value;
			}
		}

		public virtual TipoClientEntity TipoClient
		{
			get
			{
				return _TipoClient;
			}
			set
			{
				_TipoClient = value;
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


		protected bool Equals(ClientLicenseEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as ClientLicenseEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
