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
	/// An object representation of the SearchCacheWeb table
	/// </summary>
	[Serializable]
	public partial class SearchCacheWebEntity
	{
		private PairIdComponent _Id;

		private System.String _ClientGUID;
		private System.Int64 _IDFRDBase;
		private System.Int32 _OrderNumber;

		public virtual System.String ClientGUID
		{
			get
			{
				return _ClientGUID;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException("ClientGUID must not be null.");
				}
				_ClientGUID = value;
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

		public virtual System.Int64 IDFRDBase
		{
			get
			{
				return _IDFRDBase;
			}
			set
			{
				_IDFRDBase = value;
			}
		}

		public virtual System.Int32 OrderNumber
		{
			get
			{
				return _OrderNumber;
			}
			set
			{
				_OrderNumber = value;
			}
		}


		protected bool Equals(SearchCacheWebEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as SearchCacheWebEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
