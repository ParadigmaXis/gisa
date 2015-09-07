using System;
using System.Collections.Generic;
using System.ComponentModel;
using Iesi.Collections;
using Iesi.Collections.Generic;
using GISAServer.Hibernate.Utils;
using GISAServer.Hibernate.Exceptions;

namespace GISAServer.Hibernate.Objects {

    class LicencaObraAtestadoHabitabilidadeEntity {

        private System.Int64 _Id;
        private FRDBaseEntity _FRDBase;
        private string _Codigo;

        private System.Boolean _IsDeleted;
        private System.Byte[] _Versao;

        public virtual System.Int64 Id {
            get {
                return _Id;
            }
            set {
                _Id = value;
            }
        }

        public virtual FRDBaseEntity FRDBase {
            get {
                return _FRDBase;
            }
            set {
                _FRDBase = value;
            }
        }

        public virtual System.Boolean IsDeleted {
            get {
                return _IsDeleted;
            }
            set {
                _IsDeleted = value;
            }
        }

        public virtual System.Byte[] Versao {
            get {
                return _Versao;
            }
            set {
                _Versao = value;
            }
        }

        public virtual string Codigo {
            get {
                return _Codigo;
            }
            set {
                _Codigo = value;
            }
        }

        protected bool Equals(LicencaObraAtestadoHabitabilidadeEntity entity) {
            if (entity == null) return false;
            if (!base.Equals(entity)) return false;
            if (!Equals(_Id, entity._Id)) return false;
            return true;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as LicencaObraAtestadoHabitabilidadeEntity);
        }

        public override int GetHashCode() {
            int result = base.GetHashCode();
            result = 29 * result + _Id.GetHashCode();
            return result;
        }

    }
}
