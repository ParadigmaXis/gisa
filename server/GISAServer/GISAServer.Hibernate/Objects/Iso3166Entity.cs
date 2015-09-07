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
	/// An object representation of the Iso3166 table
	/// </summary>
	[Serializable]
	public partial class Iso3166Entity
	{
		private System.Int64 _Id;

		private System.String _CodeAlpha2;
		private System.String _CountryNameEnglish;
		private System.Boolean _IsDeleted;
		private System.Byte[] _Versao;

		public virtual System.String CodeAlpha2
		{
			get
			{
				return _CodeAlpha2;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException("CodeAlpha2 must not be null.");
				}
				_CodeAlpha2 = value;
			}
		}

		public virtual System.String CountryNameEnglish
		{
			get
			{
				return _CountryNameEnglish;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException("CountryNameEnglish must not be null.");
				}
				_CountryNameEnglish = value;
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
