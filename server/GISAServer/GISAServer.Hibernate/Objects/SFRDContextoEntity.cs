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
	/// An object representation of the SFRDContexto table
	/// </summary>
	[Serializable]
	public partial class SFRDContextoEntity
	{
		private System.Int64 _Id;

		private string _FonteImediataDeAquisicao;
		private FRDBaseEntity _FRDBase;
		private string _HistoriaAdministrativa;
		private string _HistoriaCustodial;
		private System.Boolean _IsDeleted;
		private System.Boolean _SerieAberta;
		private System.Byte[] _Versao;

		public virtual string FonteImediataDeAquisicao
		{
			get
			{
				return _FonteImediataDeAquisicao;
			}
			set
			{
				_FonteImediataDeAquisicao = value;
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
		public virtual string HistoriaAdministrativa
		{
			get
			{
				return _HistoriaAdministrativa;
			}
			set
			{
				_HistoriaAdministrativa = value;
			}
		}

		public virtual string HistoriaCustodial
		{
			get
			{
				return _HistoriaCustodial;
			}
			set
			{
				_HistoriaCustodial = value;
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

		public virtual System.Boolean SerieAberta
		{
			get
			{
				return _SerieAberta;
			}
			set
			{
				_SerieAberta = value;
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


		protected bool Equals(SFRDContextoEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as SFRDContextoEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
