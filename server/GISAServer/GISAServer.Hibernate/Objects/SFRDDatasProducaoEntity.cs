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
	/// An object representation of the SFRDDatasProducao table
	/// </summary>
	[Serializable]
	public partial class SFRDDatasProducaoEntity
	{
		private System.Int64 _Id;

		private System.String _FimAno;
		private System.Boolean _FimAtribuida;
		private System.String _FimDia;
		private System.String _FimMes;
		private System.String _FimTexto;
		private FRDBaseEntity _FRDBase;
		private System.String _InicioAno;
		private System.Boolean _InicioAtribuida;
		private System.String _InicioDia;
		private System.String _InicioMes;
		private System.String _InicioTexto;
		private System.Boolean _IsDeleted;
		private System.Byte[] _Versao;

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

		public virtual System.Boolean FimAtribuida
		{
			get
			{
				return _FimAtribuida;
			}
			set
			{
				_FimAtribuida = value;
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

		public virtual System.String FimTexto
		{
			get
			{
				return _FimTexto;
			}
			set
			{
				_FimTexto = value;
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

		public virtual System.Boolean InicioAtribuida
		{
			get
			{
				return _InicioAtribuida;
			}
			set
			{
				_InicioAtribuida = value;
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

		public virtual System.String InicioTexto
		{
			get
			{
				return _InicioTexto;
			}
			set
			{
				_InicioTexto = value;
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


		protected bool Equals(SFRDDatasProducaoEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as SFRDDatasProducaoEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
