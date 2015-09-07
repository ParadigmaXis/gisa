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
	/// An object representation of the SFRDImagem table
	/// </summary>
	[Serializable]
	public partial class SFRDImagemEntity
	{
		private System.Int64 _Id;

		private string _Descricao;
		private FRDBaseEntity _FRDBase;
		private System.Int64 _GUIOrder;
		private string _Identificador;
		private System.Boolean _IsDeleted;
		private SFRDImagemVolumeEntity _SFDImagemVolume;
		private System.String _Tipo;
		private System.Byte[] _Versao;

		public virtual string Descricao
		{
			get
			{
				return _Descricao;
			}
			set
			{
				_Descricao = value;
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

		public virtual System.Int64 GUIOrder
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

		public virtual string Identificador
		{
			get
			{
				return _Identificador;
			}
			set
			{
				_Identificador = value;
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

		public virtual SFRDImagemVolumeEntity SFDImagemVolume
		{
			get
			{
				return _SFDImagemVolume;
			}
			set
			{
				_SFDImagemVolume = value;
			}
		}
		public virtual System.String Tipo
		{
			get
			{
				return _Tipo;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException("Tipo must not be null.");
				}
				_Tipo = value;
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
