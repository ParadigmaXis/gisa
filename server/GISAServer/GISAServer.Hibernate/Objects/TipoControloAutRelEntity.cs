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
	/// An object representation of the TipoControloAutRel table
	/// </summary>
	[Serializable]
	public partial class TipoControloAutRelEntity
	{
		private System.Int64 _Id;

		private System.String _Designacao;
		private System.String _DesignacaoInversa;
		private System.Boolean _IsDeleted;
		private System.Boolean _Thesaurus;
		private System.Byte[] _Versao;

		public virtual System.String Designacao
		{
			get
			{
				return _Designacao;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException("Designacao must not be null.");
				}
				_Designacao = value;
			}
		}

		public virtual System.String DesignacaoInversa
		{
			get
			{
				return _DesignacaoInversa;
			}
			set
			{
				_DesignacaoInversa = value;
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

		public virtual System.Boolean Thesaurus
		{
			get
			{
				return _Thesaurus;
			}
			set
			{
				_Thesaurus = value;
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
