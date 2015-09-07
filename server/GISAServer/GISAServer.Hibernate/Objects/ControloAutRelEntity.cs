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
	/// An object representation of the ControloAutRel table
	/// </summary>
	[Serializable]
	public partial class ControloAutRelEntity
	{
		private TripleIdComponent _Id;

		private ControloAutEntity _ControloAut;
		private ControloAutEntity _ControloAutAlias;
		private System.String _Descricao;
		private System.String _FimAno;
		private System.String _FimDia;
		private System.String _FimMes;
		private System.String _InicioAno;
		private System.String _InicioDia;
		private System.String _InicioMes;
		private System.Boolean _IsDeleted;
		private TipoControloAutRelEntity _TipoRel;
		private System.Byte[] _Versao;

		public virtual ControloAutEntity ControloAut
		{
			get
			{
				return _ControloAut;
			}
			set
			{
				_ControloAut = value;
			}
		}

		public virtual ControloAutEntity ControloAutAlias
		{
			get
			{
				return _ControloAutAlias;
			}
			set
			{
				_ControloAutAlias = value;
			}
		}

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

		public virtual TripleIdComponent Id
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

		public virtual TipoControloAutRelEntity TipoRel
		{
			get
			{
				return _TipoRel;
			}
			set
			{
				_TipoRel = value;
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


		protected bool Equals(ControloAutRelEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as ControloAutRelEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
