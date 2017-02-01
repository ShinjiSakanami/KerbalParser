using System.Collections;
using System.Collections.Generic;

namespace KerbalParser
{
    public class ConfigNodeList : IEnumerable, IEnumerable<ConfigNode>
    {
        private List<ConfigNode> _nodes;

        public ConfigNode this[int index]
        {
            get
            {
                return this._nodes[index];
            }
        }

        public int Count
        {
            get
            {
                return this._nodes.Count;
            }
        }

        public ConfigNodeList()
        {
            this._nodes = new List<ConfigNode>();
        }

        public void Add(ConfigNode n)
        {
            this._nodes.Add(n);
        }

        public ConfigNode GetNode(string name, int index)
        {
            int num = 0;
            int count = this._nodes.Count;
            for (int i = 0; i < count; i++)
            {
                ConfigNode node = this._nodes[i];
                if (node.name == name)
                {
                    if (num == index)
                    {
                        return node;
                    }
                    num++;
                }
            }
            return null;
        }

        public ConfigNode GetNode(string name)
        {
            return this.GetNode(name, 0);
        }

        public ConfigNode GetNode(string name, string valueName, string value)
        {
            int count = this._nodes.Count;
            for (int i = 0; i < count; i++)
            {
                ConfigNode node = this._nodes[i];
                if (node.name == name)
                {
                    if (node.GetValue(valueName) == value)
                    {
                        return node;
                    }
                }
            }
            return null;
        }

        public ConfigNode GetNodeId(string id)
        {
            int count = this._nodes.Count;
            for (int i = 0; i < count; i++)
            {
                ConfigNode node = this._nodes[i];
                if (node.id == id)
                {
                    return node;
                }
            }
            return null;
        }

        public ConfigNode[] GetNodes()
        {
            List<ConfigNode> list = new List<ConfigNode>();
            int count = this._nodes.Count;
            for (int i = 0; i < count; i++)
            {
                ConfigNode node = this._nodes[i];
                list.Add(node);
            }
            return list.ToArray();
        }

        public ConfigNode[] GetNodes(string name)
        {
            List<ConfigNode> list = new List<ConfigNode>();
            int count = this._nodes.Count;
            for (int i = 0; i < count; i++)
            {
                ConfigNode node = this._nodes[i];
                if (node.name == name)
                {
                    list.Add(node);
                }
            }
            return list.ToArray();
        }

        public ConfigNode[] GetNodes(string name, string valueName, string value)
        {
            List<ConfigNode> list = new List<ConfigNode>();
            int count = this._nodes.Count;
            for (int i = 0; i < count; i++)
            {
                ConfigNode node = this._nodes[i];
                if (node.name == name)
                {
                    if (node.GetValue(valueName) == value)
                    {
                        list.Add(node);
                    }
                }
            }
            return list.ToArray();
        }

        public ConfigNode[] GetNodesStartWith(string name)
        {
            List<ConfigNode> list = new List<ConfigNode>();
            int count = this._nodes.Count;
            for (int i = 0; i < count; i++)
            {
                ConfigNode node = this._nodes[i];
                if (node.name.StartsWith(name))
                {
                    list.Add(node);
                }
            }
            return list.ToArray();
        }

        public bool SetNode(string name, ConfigNode newNode, string newComment, int index, bool createIfNotFound = false)
        {
            int num = 0;
            int count = this._nodes.Count;
            for (int i = 0; i < count; i++)
            {
                ConfigNode node = this._nodes[i];
                if (node.name == name)
                {
                    if (num == index)
                    {
                        node.ClearData();
                        newNode.CopyTo(node);
                        if (!string.IsNullOrEmpty(newComment))
                        {
                            node.comment = newComment;
                        }
                        return true;
                    }
                    num++;
                }
            }
            if (createIfNotFound)
            {
                newNode.name = name;
                if (!string.IsNullOrEmpty(newComment))
                {
                    newNode.comment = newComment;
                }
                this._nodes.Add(newNode);
                return true;
            }
            return false;
        }

        public bool SetNode(string name, ConfigNode newNode, string newComment, bool createIfNotFound = false)
        {
            return this.SetNode(name, newNode, newComment, 0, createIfNotFound);
        }

        public bool SetNode(string name, ConfigNode newNode, int index, bool createIfNotFound = false)
        {
            return this.SetNode(name, newNode, null, index, createIfNotFound);
        }

        public bool SetNode(string name, ConfigNode newNode, bool createIfNotFound = false)
        {
            return this.SetNode(name, newNode, null, 0, createIfNotFound);
        }

        public void Remove(ConfigNode node)
        {
            int count = this._nodes.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                if (this._nodes[i] == node)
                {
                    this._nodes.RemoveAt(i);
                    return;
                }
            }
        }

        public void RemoveNode(string name)
        {
            int count = this._nodes.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                if (this._nodes[i].name == name)
                {
                    this._nodes.RemoveAt(i);
                    return;
                }
            }
        }

        public void RemoveNodes(string name)
        {
            int count = this._nodes.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                if (this._nodes[i].name == name)
                {
                    this._nodes.RemoveAt(i);
                }
            }
        }

        public void RemoveNodesStartWith(string name)
        {
            int count = this._nodes.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                if (this._nodes[i].name.StartsWith(name))
                {
                    this._nodes.RemoveAt(i);
                }
            }
        }

        public bool Contains(string name)
        {
            int count = this._nodes.Count;
            for (int i = 0; i < count; i++)
            {
                ConfigNode node = this._nodes[i];
                if (node.name == name)
                {
                    return true;
                }
            }
            return false;
        }

        public string[] DistrinctNames()
        {
            List<string> list = new List<string>();
            int count = this._nodes.Count;
            for (int i = 0; i < count; i++)
            {
                ConfigNode node = this._nodes[i];
                if (!list.Contains(node.name))
                {
                    list.Add(node.name);
                }
            }
            return list.ToArray();
        }

        public int CountByName(string name)
        {
            int num = 0;
            int count = this._nodes.Count;
            for (int i = 0; i < count; i++)
            {
                ConfigNode node = this._nodes[i];
                if (node.name == name)
                {
                    num++;
                }
            }
            return num;
        }

        public void Clear()
        {
            this._nodes.Clear();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._nodes.GetEnumerator();
        }

        IEnumerator<ConfigNode> IEnumerable<ConfigNode>.GetEnumerator()
        {
            return this._nodes.GetEnumerator();
        }
    }
}
