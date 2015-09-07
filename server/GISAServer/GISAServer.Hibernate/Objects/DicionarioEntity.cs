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
	/// An object representation of the Dicionario table
	/// </summary>
	[Serializable]
	public partial class DicionarioEntity
	{
		private System.Int64 _Id;

		private System.String _CatCode;
		private System.Boolean _IsDeleted;
		private System.String _Termo;
		private System.Byte[] _Versao;

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

		public virtual System.String Termo
		{
			get
			{
				return _Termo;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException("Termo must not be null.");
				}
				_Termo = value;
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
