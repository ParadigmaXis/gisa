using System;
using System.ComponentModel;
using Iesi.Collections;
using Iesi.Collections.Generic;
using GISAServer.Hibernate.Utils;
using GISAServer.Hibernate.Exceptions;

namespace GISAServer.Hibernate.Objects
{    
	/// <summary>
	/// An object representation of the TipoSubDensidade table
	/// </summary>
	[Serializable]
	public partial class TipoSubDensidadeEntity
	{
		private System.Int64 _Id;

		private System.String _Designacao;
		private readonly ISet<SFRDAvaliacaoRelEntity> _FKSFRDAvaliacaoRelTipoSubDensidade = new HashedSet<SFRDAvaliacaoRelEntity>();
		private readonly ISet<SFRDAvaliacaoEntity> _FKSFRDAvaliacaoTipoSubDensidade = new HashedSet<SFRDAvaliacaoEntity>();
		private System.Boolean _IsDeleted;
		private TipoDensidadeEntity _TipoDensidade;
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

		public virtual ISet<SFRDAvaliacaoRelEntity> FKSFRDAvaliacaoRelTipoSubDensidade
		{
			get
			{
				return _FKSFRDAvaliacaoRelTipoSubDensidade;
			}
		}

		public virtual ISet<SFRDAvaliacaoEntity> FKSFRDAvaliacaoTipoSubDensidade
		{
			get
			{
				return _FKSFRDAvaliacaoTipoSubDensidade;
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

		public virtual TipoDensidadeEntity TipoDensidade
		{
			get
			{
				return _TipoDensidade;
			}
			set
			{
				_TipoDensidade = value;
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
