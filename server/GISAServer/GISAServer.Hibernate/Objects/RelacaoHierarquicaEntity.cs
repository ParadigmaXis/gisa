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
	/// An object representation of the RelacaoHierarquica table
	/// </summary>
	[Serializable]
	public partial class RelacaoHierarquicaEntity
	{
		private PairIdComponent _Id;

		private System.String _Descricao;
		private System.String _FimAno;
		private System.String _FimDia;
		private System.String _FimMes;
		private NivelEntity _ID;
		private System.String _InicioAno;
		private System.String _InicioDia;
		private System.String _InicioMes;
		private System.Boolean _IsDeleted;
		private TipoNivelRelacionadoEntity _TipoNivelRelacionado;
		private NivelEntity _Upper;
		private System.Byte[] _Versao;

		public virtual System.String Descricao
		{
			get
			{
				return _Descricao;
			}
			set
			{
				_Descricao = value;
			}
		}

		public virtual System.String FimAno
		{
			get
			{
				return _FimAno;
			}
			set
			{
				_FimAno = value;
			}
		}

		public virtual System.String FimDia
		{
			get
			{
				return _FimDia;
			}
			set
			{
				_FimDia = value;
			}
		}

		public virtual System.String FimMes
		{
			get
			{
				return _FimMes;
			}
			set
			{
				_FimMes = value;
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

		public virtual System.String InicioAno
		{
			get
			{
				return _InicioAno;
			}
			set
			{
				_InicioAno = value;
			}
		}

		public virtual System.String InicioDia
		{
			get
			{
				return _InicioDia;
			}
			set
			{
				_InicioDia = value;
			}
		}

		public virtual System.String InicioMes
		{
			get
			{
				return _InicioMes;
			}
			set
			{
				_InicioMes = value;
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

		public virtual TipoNivelRelacionadoEntity TipoNivelRelacionado
		{
			get
			{
				return _TipoNivelRelacionado;
			}
			set
			{
				_TipoNivelRelacionado = value;
			}
		}
		public virtual NivelEntity Upper
		{
			get
			{
				return _Upper;
			}
			set
			{
				_Upper = value;
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


		protected bool Equals(RelacaoHierarquicaEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as RelacaoHierarquicaEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
