using System;
using System.ComponentModel;
using Iesi.Collections;
using Iesi.Collections.Generic;
using GISAServer.Hibernate.Utils;
using GISAServer.Hibernate.Exceptions;

namespace GISAServer.Hibernate.Objects
{    
	/// <summary>
	/// An object representation of the TipoNivel table
	/// </summary>
	[Serializable]
	public partial class TipoNivelEntity
	{
		private System.Int64 _Id;

		private System.String _BuiltInName;
		private readonly ISet<NivelEntity> _FKNivelTipoNivel = new HashedSet<NivelEntity>();
		private readonly ISet<TipoNivelRelacionadoEntity> _FKTipoNivelRelacionadoTipoNivel = new HashedSet<TipoNivelRelacionadoEntity>();
		private System.Boolean _IsDeleted;
		private System.Boolean _IsDocument;
		private System.Boolean _IsStructure;
		private System.Byte[] _Versao;

		public virtual System.String BuiltInName
		{
			get
			{
				return _BuiltInName;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException("BuiltInName must not be null.");
				}
				_BuiltInName = value;
			}
		}

		public virtual ISet<NivelEntity> FKNivelTipoNivel
		{
			get
			{
				return _FKNivelTipoNivel;
			}
		}

		public virtual ISet<TipoNivelRelacionadoEntity> FKTipoNivelRelacionadoTipoNivel
		{
			get
			{
				return _FKTipoNivelRelacionadoTipoNivel;
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

		public virtual System.Boolean IsDocument
		{
			get
			{
				return _IsDocument;
			}
			set
			{
				_IsDocument = value;
			}
		}

		public virtual System.Boolean IsStructure
		{
			get
			{
				return _IsStructure;
			}
			set
			{
				_IsStructure = value;
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
