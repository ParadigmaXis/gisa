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
	/// An object representation of the SFRDUFComponente table
	/// </summary>
	[Serializable]
	public partial class SFRDUFComponenteEntity
	{
		private System.Int64 _Id;

		private SFRDUFDescricaoFisicaEntity _FRDBase;
		private Nullable<System.Int64> _IDNivelUA;
		private System.Boolean _IsDeleted;
		private Nullable<System.Decimal> _MedidaAltura;
		private Nullable<System.Decimal> _MedidaLargura;
		private Nullable<System.Decimal> _MedidaProfundidade;
		private System.Decimal _Quantidade;
		private TipoEstadoConservacaoEntity _TipoEstadoConservacao;
		private TipoMedidaEntity _TipoMedida;
		private TipoSuporteEntity _TipoSuporte;
		private System.Byte[] _Versao;

		public virtual SFRDUFDescricaoFisicaEntity FRDBase
		{
			get
			{
				return _FRDBase;
			}
			set
			{
				_FRDBase = value;
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

		public virtual Nullable<System.Int64> IDNivelUA
		{
			get
			{
				return _IDNivelUA;
			}
			set
			{
				_IDNivelUA = value;
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

		public virtual Nullable<System.Decimal> MedidaAltura
		{
			get
			{
				return _MedidaAltura;
			}
			set
			{
				_MedidaAltura = value;
			}
		}

		public virtual Nullable<System.Decimal> MedidaLargura
		{
			get
			{
				return _MedidaLargura;
			}
			set
			{
				_MedidaLargura = value;
			}
		}

		public virtual Nullable<System.Decimal> MedidaProfundidade
		{
			get
			{
				return _MedidaProfundidade;
			}
			set
			{
				_MedidaProfundidade = value;
			}
		}

		public virtual System.Decimal Quantidade
		{
			get
			{
				return _Quantidade;
			}
			set
			{
				_Quantidade = value;
			}
		}

		public virtual TipoEstadoConservacaoEntity TipoEstadoConservacao
		{
			get
			{
				return _TipoEstadoConservacao;
			}
			set
			{
				_TipoEstadoConservacao = value;
			}
		}
		public virtual TipoMedidaEntity TipoMedida
		{
			get
			{
				return _TipoMedida;
			}
			set
			{
				_TipoMedida = value;
			}
		}
		public virtual TipoSuporteEntity TipoSuporte
		{
			get
			{
				return _TipoSuporte;
			}
			set
			{
				_TipoSuporte = value;
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
