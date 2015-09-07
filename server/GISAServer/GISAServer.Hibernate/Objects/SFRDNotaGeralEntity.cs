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
	/// An object representation of the SFRDNotaGeral table
	/// </summary>
	[Serializable]
	public partial class SFRDNotaGeralEntity
	{
		private System.Int64 _Id;

		private FRDBaseEntity _FRDBase;
		private System.Boolean _IsDeleted;
		private string _NotaGeral;
		private System.Byte[] _Versao;

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

		public virtual string NotaGeral
		{
			get
			{
				return _NotaGeral;
			}
			set
			{
				_NotaGeral = value;
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


		protected bool Equals(SFRDNotaGeralEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as SFRDNotaGeralEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
