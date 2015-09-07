using System;

namespace GISAServer.Hibernate.Utils
{
    /// <summary>
    /// Serves as a base for entities based on tables having Id composed of two objects.
    /// </summary>
    [Serializable]
    public class PairIdComponent
    {
        public const string Key1Property = "Key1";
        public const string Key2Property = "Key2";
        protected object _Key1;
        protected object _Key2;

        public PairIdComponent()
        {
        }

        public PairIdComponent(object key1, object key2)
        {
            _Key1 = key1;
            _Key2 = key2;
        }

        /// <summary>
        /// Gets or sets first Id.
        /// </summary>
        public virtual object Key1
        {
            get { return _Key1; }
            set { _Key1 = value; }
        }

        /// <summary>
        /// Gets or sets second Id.
        /// </summary>
        public virtual object Key2
        {
            get { return _Key2; }
            set { _Key2 = value; }
        }

        public override int GetHashCode()
        {
            return _Key1.GetHashCode() ^ _Key2.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            PairIdComponent pairIdComponent = obj as PairIdComponent;
            if (pairIdComponent == null) return false;
            if (!Equals(_Key1, pairIdComponent._Key1)) return false;
            if (!Equals(_Key2, pairIdComponent._Key2)) return false;
            return true;
        }

        public override string ToString()
        {
            return _Key2.ToString();
        }
    }
}