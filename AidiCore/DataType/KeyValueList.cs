using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidiCore.DataType
{
    public class KeyValueList<K, T>
    {
        private Dictionary<K, T> list = new Dictionary<K, T>();

        public void Add(K key, T value)
        {
            list.Add(key, value);
        }

        public T this[K key]
        {
            get
            {
                T value;
                list.TryGetValue(key, out value);
                return value;
            }
        }
    }
}
