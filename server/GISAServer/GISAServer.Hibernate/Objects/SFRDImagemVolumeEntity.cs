using System;
using System.ComponentModel;
using Iesi.Collections;
using Iesi.Collections.Generic;
using GISAServer.Hibernate.Utils;
using GISAServer.Hibernate.Exceptions;

namespace GISAServer.Hibernate.Objects
{    
	/// <summary>
	/// An object representation of the SFRDImagemVolume table
	/// </summary>
	[Serializable]
	public partial class SFRDImagemVolumeEntity
	{
		private System.Int64 _Id;

		private readonly ISet<SFRDImagemEntity> _FKSFRDImagemSFRDImagemVolume = new HashedSet<SFRDImagemEntity>();
		private System.Boolean _IsDeleted;
		private string _Mount;
		private System.Byte[] _Versao;

		public virtual ISet<SFRDImagemEntity> FKSFRDImagemSFRDImagemVolume
		{
			get
			{
				return _FKSFRDImagemSFRDImagemVolume;
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

		public virtual string Mount
		{
			get
			{
				return _Mount;
			}
			set
			{
				_Mount = value;
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
