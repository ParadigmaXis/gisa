using System;
using System.ComponentModel;
using Iesi.Collections;
using Iesi.Collections.Generic;
using GISAServer.Hibernate.Utils;
using GISAServer.Hibernate.Exceptions;

namespace GISAServer.Hibernate.Objects
{    
	/// <summary>
	/// An object representation of the Iso15924 table
	/// </summary>
	[Serializable]
	public partial class Iso15924Entity
	{
		private System.Int64 _Id;

		private System.String _CodeAlpha2;
		private System.String _CodeAlpha3;
		private System.Decimal _CodeNumeric;
		private readonly ISet<ControloAutEntity> _FKControloAutIso15924 = new HashedSet<ControloAutEntity>();
		private System.Boolean _IsDeleted;
		private System.String _ScriptNameEnglish;
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

		public virtual System.String CodeAlpha3
		{
			get
			{
				return _CodeAlpha3;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException("CodeAlpha3 must not be null.");
				}
				_CodeAlpha3 = value;
			}
		}

		public virtual System.Decimal CodeNumeric
		{
			get
			{
				return _CodeNumeric;
			}
			set
			{
				_CodeNumeric = value;
			}
		}

		public virtual ISet<ControloAutEntity> FKControloAutIso15924
		{
			get
			{
				return _FKControloAutIso15924;
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

		public virtual System.String ScriptNameEnglish
		{
			get
			{
				return _ScriptNameEnglish;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException("ScriptNameEnglish must not be null.");
				}
				_ScriptNameEnglish = value;
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
