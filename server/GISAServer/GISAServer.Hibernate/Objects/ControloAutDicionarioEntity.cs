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
	/// An object representation of the ControloAutDicionario table
	/// </summary>
	[Serializable]
	public partial class ControloAutDicionarioEntity
	{
		private TripleIdComponent _Id;

		private ControloAutEntity _ControloAut;
		private DicionarioEntity _Dicionario;
		private System.Boolean _IsDeleted;
		private TipoControloAutFormaEntity _TipoControloAutForma;
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

		public virtual DicionarioEntity Dicionario
		{
			get
			{
				return _Dicionario;
			}
			set
			{
				_Dicionario = value;
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

		public virtual TipoControloAutFormaEntity TipoControloAutForma
		{
			get
			{
				return _TipoControloAutForma;
			}
			set
			{
				_TipoControloAutForma = value;
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


		protected bool Equals(ControloAutDicionarioEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as ControloAutDicionarioEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
