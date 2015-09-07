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
	/// An object representation of the SFRDUFMateriaisComponente table
	/// </summary>
	[Serializable]
	public partial class SFRDUFMateriaisComponenteEntity
	{
		private PairIdComponent _Id;

		private SFRDUFComponenteEntity _Componente;
		private System.Boolean _IsDeleted;
		private TipoMaterialEntity _Material;
		private System.Byte[] _Versao;

		public virtual SFRDUFComponenteEntity Componente
		{
			get
			{
				return _Componente;
			}
			set
			{
				_Componente = value;
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

		public virtual TipoMaterialEntity Material
		{
			get
			{
				return _Material;
			}
			set
			{
				_Material = value;
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


		protected bool Equals(SFRDUFMateriaisComponenteEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as SFRDUFMateriaisComponenteEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
