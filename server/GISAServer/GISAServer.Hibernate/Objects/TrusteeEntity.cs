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
	/// An object representation of the Trustee table
	/// </summary>
	[Serializable]
	public partial class TrusteeEntity
	{
		private System.Int64 _Id;

		private System.Boolean _BuiltInTrustee;
		private System.String _CatCode;
		private System.String _Description;
		private System.Boolean _IsActive;
		private System.Boolean _IsDeleted;
		private System.Boolean _IsVisibleFunction;
		private System.Boolean _IsVisibleObject;
		private System.String _Name;
		private System.Byte[] _Versao;

		public virtual System.Boolean BuiltInTrustee
		{
			get
			{
				return _BuiltInTrustee;
			}
			set
			{
				_BuiltInTrustee = value;
			}
		}

		public virtual System.String CatCode
		{
			get
			{
				return _CatCode;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException("CatCode must not be null.");
				}
				_CatCode = value;
			}
		}

		public virtual System.String Description
		{
			get
			{
				return _Description;
			}
			set
			{
				_Description = value;
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

		public virtual System.Boolean IsActive
		{
			get
			{
				return _IsActive;
			}
			set
			{
				_IsActive = value;
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

		public virtual System.Boolean IsVisibleFunction
		{
			get
			{
				return _IsVisibleFunction;
			}
			set
			{
				_IsVisibleFunction = value;
			}
		}

		public virtual System.Boolean IsVisibleObject
		{
			get
			{
				return _IsVisibleObject;
			}
			set
			{
				_IsVisibleObject = value;
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
