using System.Collections.Generic;

namespace KerbalParser
{
    public class UrlConfig
    {
        private string _name;

        private string _type;

        private ConfigNode _config;

        private UrlFile _parent;

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }
        }

        public ConfigNode Config
        {
            get
            {
                return _config;
            }
        }

        public UrlFile Parent
        {
            get
            {
                return _parent;
            }
        }

        public string Url
        {
            get
            {
                return this._parent.Url + "/" + this._name;
            }
        }

        public UrlConfig(UrlFile parent, ConfigNode node)
        {
            this._type = node.name;
            this._parent = parent;
            if (node.name == string.Empty)
            {
                node.name = parent.Name;
            }
            if (node.HasValue("name"))
            {
                this._name = node.GetValue("name");
            }
            else
            {
                this._name = node.name;
            }
            this._config = node;
        }

        public static List<UrlConfig> CreateNodeList(UrlDir parentDir, UrlFile parent)
        {
            ConfigNode configNode = ConfigNode.Load(parent.FullPath);
            if (configNode == null)
            {
                Debug.LogWarning("Cannot create config from file '" + parent.FullPath + "'.");
                return new List<UrlConfig>();
            }
            if (!configNode.HasData())
            {
                Debug.LogWarning("Config in file '" + parent.FullPath + "' contains no data.");
                return new List<UrlConfig>();
            }
            List<UrlConfig> list = new List<UrlConfig>();
            if (configNode.HasValue())
            {
                if (parentDir.Type == UrlDir.DirectoryType.Parts)
                {
                    configNode.name = "PART";
                    list.Add(new UrlConfig(parent, configNode));
                    return list;
                }
            }
            int count = configNode.Nodes.Count;
            for (int i = 0; i < count; i++)
            {
                ConfigNode configNode2 = configNode.Nodes[i];
                if (configNode2.name == string.Empty)
                {
                    if (parentDir.Type == UrlDir.DirectoryType.Parts)
                    {
                        configNode2.name = "PARTS";
                    }
                    else
                    {
                        Debug.LogWarning("Config in file '" + parent.FullPath + "' contains an unnamed node. Skipping.");
                        continue;
                    }
                }
                list.Add(new UrlConfig(parent, configNode2));
            }
            return list;
        }

        public override string ToString()
        {
            return this._name;
        }
    }
}
