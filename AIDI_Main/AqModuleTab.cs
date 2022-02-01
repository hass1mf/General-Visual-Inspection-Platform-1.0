using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDI_Main
{
    public class AqModuleTab
    {

        private Dictionary<string, string> list = new Dictionary<string, string>();

        public void Add(string key, string value)
        {
            list.Add(key, value);
        }

        public void Remove(string key)
        {

            if (list.ContainsKey(key))
            {

                list.Remove(key);
            }
                        
        
        }

        public AqModuleTab(string key, string value)
        {
            list.Add(key, value);
        }
        public string this[string key]
        {
            get
            {
                string value;
                list.TryGetValue(key, out value);
               
                return value;
            }
        }


        public List<string> GetTab(string t)
        {
            List<string> _mass = new List<string>();
            foreach (KeyValuePair<string,string> item in list)
            {
                if (item.Value == t)
                {
                    _mass.Add(item.Key);
                }
            }
            return _mass;
        }



        public bool ContainsKey(string Key)
        {
            return list.ContainsKey(Key);
        }

        public void Exchange(string Key1, string Key2)
        {
            if (list.ContainsKey(Key1))
            {
                string temp = list[Key1];
                list.Remove(Key1);
                list.Add(Key2, temp);
            }
        
        }


    }
}
