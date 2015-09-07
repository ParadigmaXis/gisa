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
	/// An object representation of the FRDBase table
	/// </summary>
	[Serializable]
	public partial class FRDBaseEntity
	{
		private System.Int64 _Id;

		private System.Boolean _IsDeleted;
		private NivelEntity _Nivel;
		private string _NotaDoArquivista;
		private string _RegrasOuConvencoes;
		private TipoFRDBaseEntity _TipoFRDBase;
		private System.Byte[] _Versao;

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

		public virtual NivelEntity Nivel
		{
			get
			{
				return _Nivel;
			}
			set
			{
				_Nivel = value;
			}
		}
		public virtual string NotaDoArquivista
		{
			get
			{
				return _NotaDoArquivista;
			}
			set
			{
				_NotaDoArquivista = value;
			}
		}

		public virtual string RegrasOuConvencoes
		{
			get
			{
				return _RegrasOuConvencoes;
			}
			set
			{
				_RegrasOuConvencoes = value;
			}
		}

		public virtual TipoFRDBaseEntity TipoFRDBase
		{
			get
			{
				return _TipoFRDBase;
			}
			set
			{
				_TipoFRDBase = value;
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
