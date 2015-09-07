using System;
using System.ComponentModel;
using Iesi.Collections;
using Iesi.Collections.Generic;
using GISAServer.Hibernate.Utils;
using GISAServer.Hibernate.Exceptions;

namespace GISAServer.Hibernate.Objects
{    
	/// <summary>
	/// An object representation of the SFRDUFDescricaoFisica table
	/// </summary>
	[Serializable]
	public partial class SFRDUFDescricaoFisicaEntity
	{
		private System.Int64 _Id;

		private readonly ISet<SFRDUFComponenteEntity> _FKSFRDUFComponenteSFRDUFDescricaoFisica = new HashedSet<SFRDUFComponenteEntity>();
		private FRDBaseEntity _FRDBase;
		private System.Boolean _IsDeleted;
		private Nullable<System.Decimal> _MedidaAltura;
		private Nullable<System.Decimal> _MedidaLargura;
		private Nullable<System.Decimal> _MedidaProfundidade;
		private TipoAcondicionamentoEntity _TipoAcondicionamento;
		private TipoMedidaEntity _TipoMedida;
		private System.Byte[] _Versao;

		public virtual ISet<SFRDUFComponenteEntity> FKSFRDUFComponenteSFRDUFDescricaoFisica
		{
			get
			{
				return _FKSFRDUFComponenteSFRDUFDescricaoFisica;
			}
		}

		public virtual FRDBaseEntity FRDBase
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

		public virtual TipoAcondicionamentoEntity TipoAcondicionamento
		{
			get
			{
				return _TipoAcondicionamento;
			}
			set
			{
				_TipoAcondicionamento = value;
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


		protected bool Equals(SFRDUFDescricaoFisicaEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as SFRDUFDescricaoFisicaEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
