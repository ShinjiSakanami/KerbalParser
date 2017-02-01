using System.Collections;
using System.Collections.Generic;

namespace KerbalParser
{
    public class PartList : IEnumerable, IEnumerable<Part>
    {
        private Dictionary<string, Part> _dict;

        public Part this[string name]
        {
            get
            {
                return this.Get(name);
            }
        }

        public int Count
        {
            get
            {
                return this._dict.Count;
            }
        }

        public PartList()
        {
            this._dict = new Dictionary<string, Part>();
        }

        public Part Add(UrlConfig urlConfig)
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
                Debug.LogWarning("PartList: Already contains part of name '" + name + "'");
                return null;
            }
            Part part = new Part();
            part.Load(urlConfig, node);
            Debug.Log(string.Concat(new object[]
            {
                "Part '",
                part.title,
                " (",
                part.name,
                ")' loaded from '",
                part.Mod,
                "' mod"
            }));
            this._dict.Add(part.name, part);
            return part;
        }

        public void Add(Part part)
        {
            if (this.Contains(part.name))
            {
                Debug.LogWarning("PartList: Already contains part of name '" + part.name + "'");
                return;
            }
            this._dict.Add(part.name, part);
        }

        public bool Contains(string name)
        {
            return this._dict.ContainsKey(name);
        }

        public Part Get(string name)
        {
            Part result;
            if (this._dict.TryGetValue(name, out result))
            {
                return result;
            }
            return null;
        }

        public bool Remove(Part part)
        {
            return this._dict.Remove(part.name);
        }

        public bool Remove(string name)
        {
            return this._dict.Remove(name);
        }

        public void Clear()
        {
            this._dict.Clear();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._dict.Values.GetEnumerator();
        }

        IEnumerator<Part> IEnumerable<Part>.GetEnumerator()
        {
            return this._dict.Values.GetEnumerator();
        }
    }
}
