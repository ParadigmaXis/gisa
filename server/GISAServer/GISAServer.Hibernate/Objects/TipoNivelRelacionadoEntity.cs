using System;
using System.ComponentModel;
using Iesi.Collections;
using Iesi.Collections.Generic;
using GISAServer.Hibernate.Utils;
using GISAServer.Hibernate.Exceptions;

namespace GISAServer.Hibernate.Objects
{    
	/// <summary>
	/// An object representation of the TipoNivelRelacionado table
	/// </summary>
	[Serializable]
	public partial class TipoNivelRelacionadoEntity
	{
		private System.Int64 _Id;

		private System.String _Codigo;
		private System.String _Designacao;
		private readonly ISet<RelacaoHierarquicaEntity> _FKRelacaoHierarquicaTipoNivelRelacionado = new HashedSet<RelacaoHierarquicaEntity>();
		private System.Decimal _GUIOrder;
		private System.Boolean _IsDeleted;
		private Nullable<System.Decimal> _Recursivo;
		private TipoNivelEntity _TipoNivel;
		private System.Byte[] _Versao;

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

		public virtual ISet<RelacaoHierarquicaEntity> FKRelacaoHierarquicaTipoNivelRelacionado
		{
			get
			{
				return _FKRelacaoHierarquicaTipoNivelRelacionado;
			}
		}

		public virtual System.Decimal GUIOrder
		{
			get
			{
				return _GUIOrder;
			}
			set
			{
				_GUIOrder = value;
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

		public virtual Nullable<System.Decimal> Recursivo
		{
			get
			{
				return _Recursivo;
			}
			set
			{
				_Recursivo = value;
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
