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
	/// An object representation of the TipoFunctionGroup table
	/// </summary>
	[Serializable]
	public partial class TipoFunctionGroupEntity
	{
		private System.Byte _Id;

		private System.Byte _GUIOrder;
		private System.Boolean _IsDeleted;
		private System.String _Name;
		private System.Byte[] _Versao;

		public virtual System.Byte GUIOrder
		{
			get
			{
				return _GUIOrder;
			}
			set
			{
				_GUIOrder = value;
			}
		}

		public virtual System.Byte Id
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

		public virtual System.String Name
		{
			get
			{
				return _Name;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException("Name must not be null.");
				}
				_Name = value;
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


		protected bool Equals(TipoFunctionGroupEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as TipoFunctionGroupEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
