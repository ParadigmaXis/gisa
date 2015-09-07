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
	/// An object representation of the NivelControloAut table
	/// </summary>
	[Serializable]
	public partial class NivelControloAutEntity
	{
		private System.Int64 _Id;

		private ControloAutEntity _ControloAut;
		private NivelEntity _ID;
		private System.Boolean _IsDeleted;
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


		protected bool Equals(NivelControloAutEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as NivelControloAutEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
