using System;

namespace GISAServer.Hibernate.Utils
{
    /// <summary>
    /// Serves as a base for entities based on tables having Id composed of three objects.
    /// </summary>
    [Serializable]
    public class TripleIdComponent : PairIdComponent
    {
        public const string Key3Property = "Key3";
        protected object _Key3;

        public TripleIdComponent()
        {
        }

        /// <summary>
        /// Gets or sets third Id.
        /// </summary>
        public virtual object Key3
        {
            get { return _Key3; }
            set { _Key3 = value; }
        }


        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            TripleIdComponent tripleIdComponent = obj as TripleIdComponent;
            if (tripleIdComponent == null) return false;
            if (!Equals(_Key1, tripleIdComponent._Key1)) return false;
            if (!Equals(_Key2, tripleIdComponent._Key2)) return false;
            if (!Equals(_Key3, tripleIdComponent._Key3)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int result = _Key1.GetHashCode();
            result = 29 * result + _Key2.GetHashCode();
            result = 29 * result + _Key3.GetHashCode();
            return result;
        }

        public override string ToString()
        {
            return _Key1.ToString() + ";" + _Key2.ToString() + ";" + _Key3.ToString();
        }
    }
}