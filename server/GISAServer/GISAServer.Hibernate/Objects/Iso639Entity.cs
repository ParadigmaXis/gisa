using System;
using System.ComponentModel;
using Iesi.Collections;
using Iesi.Collections.Generic;
using GISAServer.Hibernate.Utils;
using GISAServer.Hibernate.Exceptions;

namespace GISAServer.Hibernate.Objects
{    
	/// <summary>
	/// An object representation of the Iso639 table
	/// </summary>
	[Serializable]
	public partial class Iso639Entity
	{
		private System.Int64 _Id;

		private System.String _BibliographicCodeAlpha3;
		private readonly ISet<ControloAutEntity> _FKControloAutIso639 = new HashedSet<ControloAutEntity>();
		private System.Boolean _IsDeleted;
		private System.String _LanguageNameEnglish;
		private System.String _TerminologyCodeAlpha3;
		private System.Byte[] _Versao;

		public virtual System.String BibliographicCodeAlpha3
		{
			get
			{
				return _BibliographicCodeAlpha3;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException("BibliographicCodeAlpha3 must not be null.");
				}
				_BibliographicCodeAlpha3 = value;
			}
		}

		public virtual ISet<ControloAutEntity> FKControloAutIso639
		{
			get
			{
				return _FKControloAutIso639;
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

		public virtual System.String LanguageNameEnglish
		{
			get
			{
				return _LanguageNameEnglish;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException("LanguageNameEnglish must not be null.");
				}
				_LanguageNameEnglish = value;
			}
		}

		public virtual System.String TerminologyCodeAlpha3
		{
			get
			{
				return _TerminologyCodeAlpha3;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException("TerminologyCodeAlpha3 must not be null.");
				}
				_TerminologyCodeAlpha3 = value;
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
