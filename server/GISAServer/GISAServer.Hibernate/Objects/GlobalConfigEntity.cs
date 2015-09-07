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
	/// An object representation of the GlobalConfig table
	/// </summary>
	[Serializable]
	public partial class GlobalConfigEntity
	{
		private System.Int64 _Id;

		private System.Boolean _GestaoIntegrada;
		private System.Boolean _IsDeleted;
		private System.Int32 _MaxNumResultados;
		private System.Boolean _NiveisOrganicos;
		private System.String _URLBase;
		private System.Boolean _URLBaseActivo;
		private System.Byte[] _Versao;

		public virtual System.Boolean GestaoIntegrada
		{
			get
			{
				return _GestaoIntegrada;
			}
			set
			{
				_GestaoIntegrada = value;
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

		public virtual System.Int32 MaxNumResultados
		{
			get
			{
				return _MaxNumResultados;
			}
			set
			{
				_MaxNumResultados = value;
			}
		}

		public virtual System.Boolean NiveisOrganicos
		{
			get
			{
				return _NiveisOrganicos;
			}
			set
			{
				_NiveisOrganicos = value;
			}
		}

		public virtual System.String URLBase
		{
			get
			{
				return _URLBase;
			}
			set
			{
				_URLBase = value;
			}
		}

		public virtual System.Boolean URLBaseActivo
		{
			get
			{
				return _URLBaseActivo;
			}
			set
			{
				_URLBaseActivo = value;
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


		protected bool Equals(GlobalConfigEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as GlobalConfigEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
