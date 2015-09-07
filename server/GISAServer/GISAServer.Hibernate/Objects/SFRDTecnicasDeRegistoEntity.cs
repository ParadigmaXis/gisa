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
	/// An object representation of the SFRDTecnicasDeRegisto table
	/// </summary>
	[Serializable]
	public partial class SFRDTecnicasDeRegistoEntity
	{
		private PairIdComponent _Id;

		private SFRDCondicaoDeAcessoEntity _FRDBase;
		private System.Boolean _IsDeleted;
		private TipoTecnicasDeRegistoEntity _TipoTecnicasDeRegisto;
		private System.Byte[] _Versao;

		public virtual SFRDCondicaoDeAcessoEntity FRDBase
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

		public virtual TipoTecnicasDeRegistoEntity TipoTecnicasDeRegisto
		{
			get
			{
				return _TipoTecnicasDeRegisto;
			}
			set
			{
				_TipoTecnicasDeRegisto = value;
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


		protected bool Equals(SFRDTecnicasDeRegistoEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as SFRDTecnicasDeRegistoEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
