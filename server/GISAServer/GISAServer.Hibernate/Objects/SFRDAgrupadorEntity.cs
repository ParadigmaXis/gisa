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
    /// An object representation of the SFRDAgrupador table
    /// </summary>
    [Serializable]
    public partial class SFRDAgrupadorEntity
    {
        private System.Int64 _Id;

        private System.String _Agrupador;
        private FRDBaseEntity _FRDBase;
        private System.Boolean _IsDeleted;
        private System.Byte[] _Versao;

        public virtual System.String Agrupador
        {
            get
            {
                return _Agrupador;
            }
            set
            {
                _Agrupador = value;
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


        protected bool Equals(SFRDAgrupadorEntity entity)
        {
            if (entity == null) return false;
            if (!base.Equals(entity)) return false;
            if (!Equals(_Id, entity._Id)) return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as SFRDAgrupadorEntity);
        }

        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 29 * result + _Id.GetHashCode();
            return result;
        }

    }
}
