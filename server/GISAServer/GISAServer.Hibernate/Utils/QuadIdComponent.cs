using System;

namespace GISAServer.Hibernate.Utils
{
    [Serializable]
    public class QuadIdComponent : TripleIdComponent, IEquatable<QuadIdComponent>
    {
        public const string Key4Property = "Key4";
        protected object _Key4;

        public QuadIdComponent()
        {
        }

        public object Key4
        {
            get { return _Key4; }
            set { _Key4 = value; }
        }

        public bool Equals(QuadIdComponent quadIdComponent)
        {
            if (quadIdComponent == null) return false;
            if (!Equals(_Key1, quadIdComponent._Key1)) return false;
            if (!Equals(_Key2, quadIdComponent._Key2)) return false;
            if (!Equals(_Key3, quadIdComponent._Key3)) return false;
            if (!Equals(_Key4, quadIdComponent._Key4)) return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            return Equals(obj as QuadIdComponent);
        }

        public override int GetHashCode()
        {
            int result = _Key1.GetHashCode();
            result = 29 * result + _Key2.GetHashCode();
            result = 29 * result + _Key3.GetHashCode();
            result = 29 * result + _Key4.GetHashCode();
            return result;
        }
    }
}