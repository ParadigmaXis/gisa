using System;
using System.ComponentModel;
using Iesi.Collections;
using Iesi.Collections.Generic;
using GISAServer.Hibernate.Utils;
using GISAServer.Hibernate.Exceptions;

namespace GISAServer.Hibernate.Objects
{    
	/// <summary>
	/// An object representation of the TipoFunction table
	/// </summary>
	[Serializable]
	public partial class TipoFunctionEntity
	{
		private PairIdComponent _Id;

		private System.String _ClassName;
		private readonly ISet<TipoFunctionEntity> _FKTipoFunctionTipoFunction = new HashedSet<TipoFunctionEntity>();
		private System.Byte _GUIOrder;
		private System.Byte _Idx;
		private System.Boolean _IsDeleted;
		private System.String _ModuleName;
		private System.String _Name;
		private TipoFunctionGroupEntity _TipoFunctionGroup;
		private System.Byte[] _Versao;

		public virtual System.String ClassName
		{
			get
			{
				return _ClassName;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException("ClassName must not be null.");
				}
				_ClassName = value;
			}
		}

		public virtual ISet<TipoFunctionEntity> FKTipoFunctionTipoFunction
		{
			get
			{
				return _FKTipoFunctionTipoFunction;
			}
		}

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

		public virtual System.Byte Idx
		{
			get
			{
				return _Idx;
			}
			set
			{
				_Idx = value;
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

		public virtual System.String ModuleName
		{
			get
			{
				return _ModuleName;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException("ModuleName must not be null.");
				}
				_ModuleName = value;
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

		public virtual TipoFunctionGroupEntity TipoFunctionGroup
		{
			get
			{
				return _TipoFunctionGroup;
			}
			set
			{
				_TipoFunctionGroup = value;
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


		protected bool Equals(TipoFunctionEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as TipoFunctionEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
