using System;
using System.ComponentModel;
using Iesi.Collections;
using Iesi.Collections.Generic;
using GISAServer.Hibernate.Utils;
using GISAServer.Hibernate.Exceptions;

namespace GISAServer.Hibernate.Objects
{    
	/// <summary>
	/// An object representation of the TipoNoticiaAut table
	/// </summary>
	[Serializable]
	public partial class TipoNoticiaAutEntity
	{
		private System.Int64 _Id;

		private System.Boolean _Conteudo;
		private System.String _Designacao;
		private readonly ISet<ControloAutEntity> _FKControloAutNoticiaAut = new HashedSet<ControloAutEntity>();
		private System.Boolean _IsDeleted;
		private System.Byte[] _Versao;

		public virtual System.Boolean Conteudo
		{
			get
			{
				return _Conteudo;
			}
			set
			{
				_Conteudo = value;
			}
		}

		public virtual System.String Designacao
		{
			get
			{
				return _Designacao;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException("Designacao must not be null.");
				}
				_Designacao = value;
			}
		}

		public virtual ISet<ControloAutEntity> FKControloAutNoticiaAut
		{
			get
			{
				return _FKControloAutNoticiaAut;
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
