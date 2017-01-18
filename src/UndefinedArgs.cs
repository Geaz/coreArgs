using System.Dynamic;
using System.Collections.Generic;
using System.Collections;

namespace coreArgs
{
    ///<summary>
    /// This class provides a custom dynamic object for **coreArgs**.
    /// This type is internally used for the RemaingOptionsAttribute.
    /// We can't use the <c>dynamic</c> object, because this one throws exceptions on
    /// access of undefined properties. In case of **coreArgs** we rather want <c>null</c> values,
    /// if a accessed property is not available.
    /// Further more this class implements the <c>IDictionary</c> interface. This way
    /// we are able to set properties through keys like in an <c>ExpandoObject</c>.
    /// This behaviour is needed to dynamically set properties on the object based on 
    /// named command line arguments.
    ///</summary>   
    public class UndefinedArgs : DynamicObject, IDictionary<string, object>
    {
        private Dictionary<string, object> _properties = new Dictionary<string, object>();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            _properties.TryGetValue(binder.Name, out result);
            //We want to get 'null', if the value is not set. No exception!
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _properties[binder.Name] = value;
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<string, object>)_properties).GetEnumerator();
        }

        #region IDictionary (through _properties)      
        public void Add(KeyValuePair<string, object> item) => ((IDictionary<string, object>)_properties).Add(item);
        public void Add(string key, object value) => ((IDictionary<string, object>)_properties).Add(key, value);
        public void Clear() => ((IDictionary<string, object>)_properties).Clear();
        public bool Contains(KeyValuePair<string, object> item) => ((IDictionary<string, object>)_properties).Contains(item);
        public bool ContainsKey(string key) => ((IDictionary<string, object>)_properties).ContainsKey(key);
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex) => ((IDictionary<string, object>)_properties).CopyTo(array, arrayIndex);
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => ((IDictionary<string, object>)_properties).GetEnumerator();
        public bool Remove(KeyValuePair<string, object> item) => ((IDictionary<string, object>)_properties).Remove(item);
        public bool Remove(string key) => ((IDictionary<string, object>)_properties).Remove(key);
        public bool TryGetValue(string key, out object value) => ((IDictionary<string, object>)_properties).TryGetValue(key, out value);

        public object this[string key]
        {
            get { return ((IDictionary<string, object>)_properties)[key]; } 
            set { ((IDictionary<string, object>)_properties)[key] = value; } 
        }

        public int Count { get { return ((IDictionary<string, object>)_properties).Count; } }
        public bool IsReadOnly { get { return ((IDictionary<string, object>)_properties).IsReadOnly; } }
        public ICollection<string> Keys { get { return ((IDictionary<string, object>)_properties).Keys; } }
        public ICollection<object> Values { get { return ((IDictionary<string, object>)_properties).Values; } }

        #endregion
    }
}