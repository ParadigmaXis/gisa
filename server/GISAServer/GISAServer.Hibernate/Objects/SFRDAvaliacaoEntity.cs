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
	/// An object representation of the SFRDAvaliacao table
	/// </summary>
	[Serializable]
	public partial class SFRDAvaliacaoEntity
	{
		private System.Int64 _Id;

		private AutoEliminacaoEntity _AutoEliminacao;
		private System.Boolean _AvaliacaoTabela;
		private TipoDensidadeEntity _Densidade;
		private FRDBaseEntity _FRDBase;
		private Nullable<System.Decimal> _Frequencia;
		private System.Boolean _IsDeleted;
		private ModelosAvaliacaoEntity _ModeloAvaliacao;
		private string _Observacoes;
		private TipoPertinenciaEntity _Pertinencia;
		private Nullable<System.Int16> _PrazoConservacao;
		private Nullable<System.Boolean> _Preservar;
		private System.Boolean _Publicar;
		private TipoSubDensidadeEntity _Subdensidade;
		private System.Byte[] _Versao;

		public virtual AutoEliminacaoEntity AutoEliminacao
		{
			get
			{
				return _AutoEliminacao;
			}
			set
			{
				_AutoEliminacao = value;
			}
		}
		public virtual System.Boolean AvaliacaoTabela
		{
			get
			{
				return _AvaliacaoTabela;
			}
			set
			{
				_AvaliacaoTabela = value;
			}
		}

		public virtual TipoDensidadeEntity Densidade
		{
			get
			{
				return _Densidade;
			}
			set
			{
				_Densidade = value;
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
		public virtual Nullable<System.Decimal> Frequencia
		{
			get
			{
				return _Frequencia;
			}
			set
			{
				_Frequencia = value;
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

		public virtual ModelosAvaliacaoEntity ModeloAvaliacao
		{
			get
			{
				return _ModeloAvaliacao;
			}
			set
			{
				_ModeloAvaliacao = value;
			}
		}
		public virtual string Observacoes
		{
			get
			{
				return _Observacoes;
			}
			set
			{
				_Observacoes = value;
			}
		}

		public virtual TipoPertinenciaEntity Pertinencia
		{
			get
			{
				return _Pertinencia;
			}
			set
			{
				_Pertinencia = value;
			}
		}
		public virtual Nullable<System.Int16> PrazoConservacao
		{
			get
			{
				return _PrazoConservacao;
			}
			set
			{
				_PrazoConservacao = value;
			}
		}

		public virtual Nullable<System.Boolean> Preservar
		{
			get
			{
				return _Preservar;
			}
			set
			{
				_Preservar = value;
			}
		}

		public virtual System.Boolean Publicar
		{
			get
			{
				return _Publicar;
			}
			set
			{
				_Publicar = value;
			}
		}

		public virtual TipoSubDensidadeEntity Subdensidade
		{
			get
			{
				return _Subdensidade;
			}
			set
			{
				_Subdensidade = value;
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


		protected bool Equals(SFRDAvaliacaoEntity entity)
		{
			if (entity == null) return false;
			if (!base.Equals(entity)) return false;
			if (!Equals(_Id, entity._Id)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as SFRDAvaliacaoEntity);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
