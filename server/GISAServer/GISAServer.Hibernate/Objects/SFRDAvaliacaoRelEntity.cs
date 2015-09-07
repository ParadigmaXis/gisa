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
	/// An object representation of the SFRDAvaliacaoRel table
	/// </summary>
	[Serializable]
	public partial class SFRDAvaliacaoRelEntity
	{
		private PairIdComponent _Id;

		private TipoDensidadeEntity _Densidade;
		private SFRDAvaliacaoEntity _FRDBase;
		private System.Boolean _IsDeleted;
		private NivelEntity _Nivel;
		private System.Decimal _Ponderacao;
		private TipoSubDensidadeEntity _SubDensidade;
		private System.Byte[] _Versao;

		public virtual TipoDensidadeEntity Densidade
		{
			get
			{
				return _Densidade;
			}
			set
			{
				_Densidade = value;
			}
		}
		public virtual SFRDAvaliacaoEntity FRDBase
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

		public virtual PairIdComponent Id
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

		public virtual System.Decimal Ponderacao
		{
			get
			{
				return _Ponderacao;
			}
			set
			{
				_Ponderacao = value;
			}
		}

		public virtual TipoSubDensidadeEntity SubDensidade
		{
			get
			{
				return _SubDensidade;
			}
			set
			{
				_SubDensidade = value;
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


		protected bool Equals(SFRDAvaliacaoRelEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as SFRDAvaliacaoRelEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
