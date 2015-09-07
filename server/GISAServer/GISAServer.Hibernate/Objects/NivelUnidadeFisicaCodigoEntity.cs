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
	/// An object representation of the NivelUnidadeFisicaCodigo table
	/// </summary>
	[Serializable]
	public partial class NivelUnidadeFisicaCodigoEntity
	{
		private PairIdComponent _Id;

		private System.Decimal _Ano;
		private System.Decimal _Contador;
		private NivelEntity _ID;
		private System.Boolean _IsDeleted;
		private System.Byte[] _Versao;

		public virtual System.Decimal Ano
		{
			get
			{
				return _Ano;
			}
			set
			{
				_Ano = value;
			}
		}

		public virtual System.Decimal Contador
		{
			get
			{
				return _Contador;
			}
			set
			{
				_Contador = value;
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

		public virtual NivelEntity ID
		{
			get
			{
				return _ID;
			}
			set
			{
				_ID = value;
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


		protected bool Equals(NivelUnidadeFisicaCodigoEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as NivelUnidadeFisicaCodigoEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
