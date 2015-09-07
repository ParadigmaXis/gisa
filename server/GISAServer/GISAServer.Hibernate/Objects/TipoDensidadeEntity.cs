using System;
using System.ComponentModel;
using Iesi.Collections;
using Iesi.Collections.Generic;
using GISAServer.Hibernate.Utils;
using GISAServer.Hibernate.Exceptions;

namespace GISAServer.Hibernate.Objects
{    
	/// <summary>
	/// An object representation of the TipoDensidade table
	/// </summary>
	[Serializable]
	public partial class TipoDensidadeEntity
	{
		private System.Int64 _Id;

		private System.String _Designacao;
		private readonly ISet<SFRDAvaliacaoRelEntity> _FKSFRDAvaliacaoRelTipoDensidade = new HashedSet<SFRDAvaliacaoRelEntity>();
		private readonly ISet<SFRDAvaliacaoEntity> _FKSFRDAvaliacaoTipoDensidade = new HashedSet<SFRDAvaliacaoEntity>();
		private readonly ISet<TipoSubDensidadeEntity> _FKTipoSubDensidadeTipoDensidade = new HashedSet<TipoSubDensidadeEntity>();
		private System.Boolean _IsDeleted;
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

		public virtual ISet<SFRDAvaliacaoRelEntity> FKSFRDAvaliacaoRelTipoDensidade
		{
			get
			{
				return _FKSFRDAvaliacaoRelTipoDensidade;
			}
		}

		public virtual ISet<SFRDAvaliacaoEntity> FKSFRDAvaliacaoTipoDensidade
		{
			get
			{
				return _FKSFRDAvaliacaoTipoDensidade;
			}
		}

		public virtual ISet<TipoSubDensidadeEntity> FKTipoSubDensidadeTipoDensidade
		{
			get
			{
				return _FKTipoSubDensidadeTipoDensidade;
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
