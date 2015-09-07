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
	/// An object representation of the NivelUnidadeFisica table
	/// </summary>
	[Serializable]
	public partial class NivelUnidadeFisicaEntity
	{
		private System.Int64 _Id;

		private System.String _GuiaIncorporacao;
		private NivelDesignadoEntity _ID;
		private System.Boolean _IsDeleted;
		private System.String _CodigoBarras;
		private System.Byte[] _Versao;
        private System.Boolean _Eliminado;

		public virtual System.String GuiaIncorporacao
		{
			get
			{
				return _GuiaIncorporacao;
			}
			set
			{
				_GuiaIncorporacao = value;
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

		public virtual NivelDesignadoEntity ID
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

		public virtual System.String CodigoBarras
		{
			get
			{
				return _CodigoBarras;
			}
			set
			{
				_CodigoBarras = value;
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

        public virtual System.Boolean Eliminado
        {
            get
            {
                return _Eliminado;
            }
            set
            {
                _Eliminado = value;
            }
        }

		protected bool Equals(NivelUnidadeFisicaEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as NivelUnidadeFisicaEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
