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
	/// An object representation of the SFRDConteudoEEstrutura table
	/// </summary>
	[Serializable]
	public partial class SFRDConteudoEEstruturaEntity
	{
		private System.Int64 _Id;

		private string _ConteudoInformacional;
		private FRDBaseEntity _FRDBase;
		private string _Incorporacao;
		private System.Boolean _IsDeleted;
		private System.Byte[] _Versao;

		public virtual string ConteudoInformacional
		{
			get
			{
				return _ConteudoInformacional;
			}
			set
			{
				_ConteudoInformacional = value;
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

		public virtual string Incorporacao
		{
			get
			{
				return _Incorporacao;
			}
			set
			{
				_Incorporacao = value;
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


		protected bool Equals(SFRDConteudoEEstruturaEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as SFRDConteudoEEstruturaEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
