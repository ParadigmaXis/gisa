using System;
using System.Collections.Generic;
using System.ComponentModel;
using Iesi.Collections;
using Iesi.Collections.Generic;
using GISAServer.Hibernate.Utils;
using GISAServer.Hibernate.Exceptions;

namespace GISAServer.Hibernate.Objects {

    class LicencaObraEntity {
        private System.Int64 _Id;

        private FRDBaseEntity _FRDBase;
        private string _TipoObra;
        private System.Boolean _PropriedadeHorizontal;
        private string _PHTexto;
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

        public virtual string TipoObra {
            get {
                return _TipoObra;
            }
            set {
                _TipoObra = value;
            }
        }

        public virtual string PHTexto {
            get {
                return _PHTexto;
            }
            set {
                _PHTexto = value;
            }
        }

        public virtual System.Boolean PropriedadeHorizontal {
            get {
                return _PropriedadeHorizontal;
            }
            set {
                _PropriedadeHorizontal = value;
            }
        }

        protected bool Equals(LicencaObraEntity entity) {
            if (entity == null) return false;
            if (!base.Equals(entity)) return false;
            if (!Equals(_Id, entity._Id)) return false;
            return true;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as LicencaObraEntity);
        }

        public override int GetHashCode() {
            int result = base.GetHashCode();
            result = 29 * result + _Id.GetHashCode();
            return result;
        }

    }
}
