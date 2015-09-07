using System;
using System.ComponentModel;
using Iesi.Collections;
using Iesi.Collections.Generic;
using GISAServer.Hibernate.Utils;
using GISAServer.Hibernate.Exceptions;

namespace GISAServer.Hibernate.Objects
{    
	/// <summary>
	/// An object representation of the Nivel table
	/// </summary>
	[Serializable]
	public partial class NivelEntity
	{
		private System.Int64 _Id;

		private System.String _CatCode;
		private System.String _Codigo;
		private readonly ISet<FRDBaseEntity> _FKFRDBaseNivel = new HashedSet<FRDBaseEntity>();
		private System.Boolean _IsDeleted;
		private TipoNivelEntity _TipoNivel;
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

		public virtual System.String Codigo
		{
			get
			{
				return _Codigo;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException("Codigo must not be null.");
				}
				_Codigo = value;
			}
		}

		public virtual ISet<FRDBaseEntity> FKFRDBaseNivel
		{
			get
			{
				return _FKFRDBaseNivel;
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

		public virtual TipoNivelEntity TipoNivel
		{
			get
			{
				return _TipoNivel;
			}
			set
			{
				_TipoNivel = value;
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
