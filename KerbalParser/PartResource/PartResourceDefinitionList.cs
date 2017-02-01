using System.Collections;
using System.Collections.Generic;

namespace KerbalParser
{
    public class PartResourceDefinitionList : IEnumerable, IEnumerable<PartResourceDefinition>
    {
        private Dictionary<int, PartResourceDefinition> _dict;

        public PartResourceDefinition this[string name]
        {
            get
            {
                return this.Get(name);
            }
        }

        public PartResourceDefinition this[int id]
        {
            get
            {
                return this.Get(id);
            }
        }

        public int Count
        {
            get
            {
                return this._dict.Count;
            }
        }

        public PartResourceDefinitionList()
        {
            this._dict = new Dictionary<int, PartResourceDefinition>();
        }

        public PartResourceDefinition Add(UrlConfig urlConfig)
        {
            ConfigNode node = urlConfig.Config;
            if (!node.HasValue("name"))
            {
                Debug.LogWarning("Config has no name field");
                return null;
            }
            string name = node.GetValue("name");
            if (this.Contains(name))
            {
                Debug.LogWarning("PartResourceList: Already contains resource of name '" + name + "'");
                return null;
            }
            PartResourceDefinition def = new PartResourceDefinition();
            def.Load(urlConfig, node);
            Debug.Log(string.Concat(new object[]
            {
                "Resource definition '",
                def.name,
                "' loaded from '",
                def.Mod,
                "' mod"
            }));
            this._dict.Add(def.id, def);
            return def;
        }

        public void Add(PartResourceDefinition def)
        {
            if (this.Contains(def.name))
            {
                Debug.LogWarning("PartResourceList: Already contains resource of name '" + def.name + "'");
                return;
            }
            this._dict.Add(def.id, def);
        }

        public bool Contains(int id)
        {
            return this._dict.ContainsKey(id);
        }

        public bool Contains(string name)
        {
            return this._dict.ContainsKey(name.GetHashCode());
        }

        public PartResourceDefinition Get(int id)
        {
            PartResourceDefinition result;
            if (this._dict.TryGetValue(id, out result))
            {
                return result;
            }
            return null;
        }

        public PartResourceDefinition Get(string name)
        {
            return this.Get(name.GetHashCode());
        }

        public bool Remove(PartResourceDefinition def)
        {
            return this._dict.Remove(def.id);
        }

        public bool Remove(string name)
        {
            return this._dict.Remove(name.GetHashCode());
        }

        public bool Remove(int id)
        {
            return this._dict.Remove(id);
        }

        public void Clear()
        {
            this._dict.Clear();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._dict.Values.GetEnumerator();
        }

        IEnumerator<PartResourceDefinition> IEnumerable<PartResourceDefinition>.GetEnumerator()
        {
            return this._dict.Values.GetEnumerator();
        }
    }
}
