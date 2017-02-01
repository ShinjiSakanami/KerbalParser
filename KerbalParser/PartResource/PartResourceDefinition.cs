namespace KerbalParser
{
    public class PartResourceDefinition
    {
        public enum TransferMode
        {
            NONE,
            DIRECT,
            PUMP
        }

        public enum FlowMode
        {
            NO_FLOW,
            ALL_VESSEL,
            STAGE_PRIORITY_FLOW,
            STACK_PRIORITY_SEARCH,
            ALL_VESSEL_BALANCE,
            STAGE_PRIORITY_FLOW_BALANCE,
            STAGE_STACK_FLOW,
            STAGE_STACK_FLOW_BALANCE,
            NULL
        }

        public string name;

        public int id;

        public double density;

        public double volume;

        public string abbreviation;

        public double unitCost;

        public double hsp;

        public Color color;

        public PartResourceDefinition.FlowMode flowMode;

        public PartResourceDefinition.TransferMode transfer;

        public bool isTweakable;

        public bool isVisible;

        private ConfigNode _config;

        private UrlConfig _urlConfig;

        private string _configFileFullName;

        private string _resourceUrl;

        public ConfigNode Config
        {
            get
            {
                return this._config;
            }
        }

        public UrlConfig UrlConfig
        {
            get
            {
                return this._urlConfig;
            }
        }

        public string ConfigFileFullName
        {
            get
            {
                return this._configFileFullName;
            }
        }

        public string ResourceUrl
        {
            get
            {
                return this._resourceUrl;
            }
        }

        public string Mod
        {
            get
            {
                string[] array = this._resourceUrl.Split(new char[]
                {
                    '/'
                });
                if (array.Length >= 2)
                {
                    if (array[0] == "data")
                    {
                        return array[1];
                    }

                }
                if (array.Length >= 1)
                {
                    return array[0];
                }
                return null;
            }
        }

        public PartResourceDefinition()
        {
            this.Init();
        }

        public PartResourceDefinition(string name)
        {
            this.Init();
            this.name = name;
            this.id = name.GetHashCode();
        }

        private void Init()
        {
            this.name = string.Empty;
            this.id = -1;
            this.density = 1.0;
            this.volume = 5.0;
            this.abbreviation = string.Empty;
            this.color = Color.White;
            this.isVisible = true;
        }

        public void Load(UrlConfig urlConfig, ConfigNode node)
        {
            this._resourceUrl = urlConfig.Url;
            this._urlConfig = urlConfig;
            this._configFileFullName = urlConfig.Parent.FullPath;
            this.Load(node);
        }

        private void Load(ConfigNode node)
        {
            this._config = node;
            this.name = node.GetValue("name");
            this.id = this.name.GetHashCode();
            ConfigNode.LoadObjectFromConfig(this, node, false);
        }

        public void Save(ConfigNode node)
        {
            node.AddValue("name", this.name);
            node.AddValue("abbreviation", this.abbreviation);
            node.AddValue("density", this.density);
            node.AddValue("volume", this.volume);
            node.AddValue("unitCost", this.unitCost);
            node.AddValue("hsp", this.hsp);
            node.AddValue("flowMode", this.flowMode);
            node.AddValue("transfer", this.transfer);
            node.AddValue("isTweakable", this.isTweakable);
            if (this.isVisible != true)
            {
                node.AddValue("isVisible", this.isVisible);
            }
            if (!this.color.Equals(Color.White))
            {
                node.AddValue("color", ConfigNode.WriteColor(this.color));
            }
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
