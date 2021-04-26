using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.SUPER.APP.Models
{

    public class KeyValue
    {

        #region Private Variables
        private int _key;
        private string _value;
        private Boolean _ta201_obligalider;
        #endregion

        #region Public Properties
        public int Key
        {
            get { return _key; }
            set { _key = value; }
        }
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public bool ta201_obligalider
        {
            get { return _ta201_obligalider; }
            set { _ta201_obligalider = value; }
        }
        #endregion

        public KeyValue()
        {
        }

        public KeyValue(int key, string value)
        {
            _key = key;
            _value = value;
        }

        public KeyValue(int key, string value, Boolean ta201_obligalider)
        {
            _key = key;
            _value = value;
            _ta201_obligalider = ta201_obligalider;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            KeyValue objAsKeyValue = obj as KeyValue;
            if (objAsKeyValue == null) return false;
            else return Equals(objAsKeyValue);
        }

        public override int GetHashCode()
        {
            return Key;
        }

        public bool Equals(KeyValue other)
        {
            if (other == null) return false;
            return (this.Key.Equals(other.Key));
        }


    }

    public class KeyValue2
    {

        #region Private Variables
        private string _key;
        private string _value;
        #endregion

        #region Public Properties
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        #endregion

        public KeyValue2()
        {
        }

        public KeyValue2(string key, string value)
        {
            _key = key;
            _value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            KeyValue objAsKeyValue = obj as KeyValue;
            if (objAsKeyValue == null) return false;
            else return Equals(objAsKeyValue);
        }

        //public override string GetHashCode()
        //{
        //    return Key;
        //}

        public bool Equals(KeyValue other)
        {
            if (other == null) return false;
            return (this.Key.Equals(other.Key));
        }


    }

    public class KeyValueComparer : IEqualityComparer<KeyValue>
    {
        public bool Equals(KeyValue x, KeyValue y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.Key == y.Key;
        }
        public int GetHashCode(KeyValue keyvalue)
        {
            if (Object.ReferenceEquals(keyvalue, null)) return 0;
            return keyvalue.Value.GetHashCode();
        }
    }
}