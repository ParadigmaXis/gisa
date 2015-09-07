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
    /// An object representation of the SFRDAutor table
    /// </summary>
    [Serializable]
    public partial class SFRDAutorEntity
    {
        private PairIdComponent _Id;

        private ControloAutEntity _ControloAut;
        private FRDBaseEntity _FRDBase;
        private System.Boolean _IsDeleted;
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

        protected bool Equals(SFRDAutorEntity entity)
        {
            if (entity == null) return false;
            if (!base.Equals(entity)) return false;
            if (!Equals(_Id, entity._Id)) return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as SFRDAutorEntity);
        }

        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 29 * result + _Id.GetHashCode();
            return result;
        }
    }
}
