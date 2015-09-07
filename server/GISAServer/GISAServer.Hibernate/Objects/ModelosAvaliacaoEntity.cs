using System;
using System.ComponentModel;
using Iesi.Collections;
using Iesi.Collections.Generic;
using GISAServer.Hibernate.Utils;
using GISAServer.Hibernate.Exceptions;

namespace GISAServer.Hibernate.Objects
{    
	/// <summary>
	/// An object representation of the ModelosAvaliacao table
	/// </summary>
	[Serializable]
	public partial class ModelosAvaliacaoEntity
	{
		private System.Int64 _Id;

		private System.String _Designacao;
		private readonly ISet<SFRDAvaliacaoEntity> _FKSFRDAvaliacaoModelosAvaliacao = new HashedSet<SFRDAvaliacaoEntity>();
		private System.Boolean _IsDeleted;
		private ListaModelosAvaliacaoEntity _ListaModelosAvaliacao;
		private Nullable<System.Int16> _PrazoConservacao;
		private System.Boolean _Preservar;
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

		public virtual ISet<SFRDAvaliacaoEntity> FKSFRDAvaliacaoModelosAvaliacao
		{
			get
			{
				return _FKSFRDAvaliacaoModelosAvaliacao;
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

		public virtual ListaModelosAvaliacaoEntity ListaModelosAvaliacao
		{
			get
			{
				return _ListaModelosAvaliacao;
			}
			set
			{
				_ListaModelosAvaliacao = value;
			}
		}
		public virtual Nullable<System.Int16> PrazoConservacao
		{
			get
			{
				return _PrazoConservacao;
			}
			set
			{
				_PrazoConservacao = value;
			}
		}

		public virtual System.Boolean Preservar
		{
			get
			{
				return _Preservar;
			}
			set
			{
				_Preservar = value;
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
