using System;
using System.ComponentModel;
using Iesi.Collections;
using Iesi.Collections.Generic;
using GISAServer.Hibernate.Utils;
using GISAServer.Hibernate.Exceptions;

namespace GISAServer.Hibernate.Objects
{    
	/// <summary>
	/// An object representation of the ControloAut table
	/// </summary>
	[Serializable]
	public partial class ControloAutEntity
	{
		private System.Int64 _Id;

		private System.Boolean _Autorizado;
		private System.String _ChaveColectividade;
		private System.String _ChaveRegisto;
		private System.Boolean _Completo;
		private string _DescContextoGeral;
		private string _DescEnquadramentoLegal;
		private string _DescEstatutoLegal;
		private string _DescEstruturaInterna;
		private string _DescHistoria;
		private string _DescOcupacoesActividades;
		private string _DescOutraInformacaoRelevante;
		private string _DescZonaGeografica;
		private readonly ISet<NivelControloAutEntity> _FKNivelControloAutControloAut = new HashedSet<NivelControloAutEntity>();
		private System.Boolean _IsDeleted;
		private Iso15924Entity _Iso15924;
		private Iso639Entity _Iso639p2;
		private string _NotaExplicativa;
		private string _Observacoes;
		private string _RegrasConvencoes;
		private TipoNoticiaAutEntity _TipoNoticiaAut;
		private System.Byte[] _Versao;

		public virtual System.Boolean Autorizado
		{
			get
			{
				return _Autorizado;
			}
			set
			{
				_Autorizado = value;
			}
		}

		public virtual System.String ChaveColectividade
		{
			get
			{
				return _ChaveColectividade;
			}
			set
			{
				_ChaveColectividade = value;
			}
		}

		public virtual System.String ChaveRegisto
		{
			get
			{
				return _ChaveRegisto;
			}
			set
			{
				_ChaveRegisto = value;
			}
		}

		public virtual System.Boolean Completo
		{
			get
			{
				return _Completo;
			}
			set
			{
				_Completo = value;
			}
		}

		public virtual string DescContextoGeral
		{
			get
			{
				return _DescContextoGeral;
			}
			set
			{
				_DescContextoGeral = value;
			}
		}

		public virtual string DescEnquadramentoLegal
		{
			get
			{
				return _DescEnquadramentoLegal;
			}
			set
			{
				_DescEnquadramentoLegal = value;
			}
		}

		public virtual string DescEstatutoLegal
		{
			get
			{
				return _DescEstatutoLegal;
			}
			set
			{
				_DescEstatutoLegal = value;
			}
		}

		public virtual string DescEstruturaInterna
		{
			get
			{
				return _DescEstruturaInterna;
			}
			set
			{
				_DescEstruturaInterna = value;
			}
		}

		public virtual string DescHistoria
		{
			get
			{
				return _DescHistoria;
			}
			set
			{
				_DescHistoria = value;
			}
		}

		public virtual string DescOcupacoesActividades
		{
			get
			{
				return _DescOcupacoesActividades;
			}
			set
			{
				_DescOcupacoesActividades = value;
			}
		}

		public virtual string DescOutraInformacaoRelevante
		{
			get
			{
				return _DescOutraInformacaoRelevante;
			}
			set
			{
				_DescOutraInformacaoRelevante = value;
			}
		}

		public virtual string DescZonaGeografica
		{
			get
			{
				return _DescZonaGeografica;
			}
			set
			{
				_DescZonaGeografica = value;
			}
		}

		public virtual ISet<NivelControloAutEntity> FKNivelControloAutControloAut
		{
			get
			{
				return _FKNivelControloAutControloAut;
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

		public virtual Iso15924Entity Iso15924
		{
			get
			{
				return _Iso15924;
			}
			set
			{
				_Iso15924 = value;
			}
		}
		public virtual Iso639Entity Iso639p2
		{
			get
			{
				return _Iso639p2;
			}
			set
			{
				_Iso639p2 = value;
			}
		}
		public virtual string NotaExplicativa
		{
			get
			{
				return _NotaExplicativa;
			}
			set
			{
				_NotaExplicativa = value;
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

		public virtual string RegrasConvencoes
		{
			get
			{
				return _RegrasConvencoes;
			}
			set
			{
				_RegrasConvencoes = value;
			}
		}

		public virtual TipoNoticiaAutEntity TipoNoticiaAut
		{
			get
			{
				return _TipoNoticiaAut;
			}
			set
			{
				_TipoNoticiaAut = value;
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


	}
}
