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
	/// An object representation of the ControloAutDataDeDescricao table
	/// </summary>
	[Serializable]
	public partial class ControloAutDataDeDescricaoEntity
	{
		private TripleIdComponent _Id;

		private ControloAutEntity _ControloAut;
		private System.DateTime _DataAutoria;
		private System.DateTime _DataEdicao;
		private System.Boolean _IsDeleted;
		private TrusteeUserEntity _TrusteeAuthority;
		private TrusteeUserEntity _TrusteeOperator;
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

		public virtual System.DateTime DataAutoria
		{
			get
			{
				return _DataAutoria;
			}
			set
			{
				_DataAutoria = value;
			}
		}

		public virtual System.DateTime DataEdicao
		{
			get
			{
				return _DataEdicao;
			}
			set
			{
				_DataEdicao = value;
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

		public virtual TrusteeUserEntity TrusteeAuthority
		{
			get
			{
				return _TrusteeAuthority;
			}
			set
			{
				_TrusteeAuthority = value;
			}
		}
		public virtual TrusteeUserEntity TrusteeOperator
		{
			get
			{
				return _TrusteeOperator;
			}
			set
			{
				_TrusteeOperator = value;
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


		protected bool Equals(ControloAutDataDeDescricaoEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as ControloAutDataDeDescricaoEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
