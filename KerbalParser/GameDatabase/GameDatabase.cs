using System.Collections.Generic;

namespace KerbalParser
{
    public class GameDatabase
    {
        private static string _KSPRootPath;

        private UrlDir _root;

        private List<ConfigDirectory> _configDirectories;

        private PartList _parts;

        private PartResourceDefinitionList _resourceDefinitions;

        private static GameDatabase _instance;

        public static string KSPRootPath
        {
            get
            {
                return GameDatabase._KSPRootPath;
            }
        }

        public UrlDir Root
        {
            get
            {
                return this._root;
            }
        }

        public static GameDatabase Instance
        {
            get
            {
                if (GameDatabase._instance == null)
                {
                    GameDatabase._instance = new GameDatabase();
                }
                return GameDatabase._instance;
            }
        }

        public PartList Parts
        {
            get
            {
                return this._parts;
            }
        }

        public PartResourceDefinitionList ResourceDefinitions
        {
            get
            {
                return this._resourceDefinitions;
            }
        }

        private GameDatabase()
        {
        }

        public void LoadGameDatabase(string kspPath)
        {
            if (!UrlDir.IsValidKSPPath(kspPath))
            {
                Debug.LogError("Invalid KSP root path! Abording game database load.");
                return;
            }
            GameDatabase._KSPRootPath = kspPath;
            this.Reset();
            this._root = new UrlDir(this._configDirectories.ToArray(), new ConfigFileType[0]);
            UrlDir data = this._root.GetDirectory("data");
            foreach (UrlDir dir in data.GetDirectories(false))
            {
                Debug.Log(string.Concat(new object[]
                {
                    "Mod '",
                    dir.Name,
                    "' found with ",
                    dir.GetConfigFiles().Length,
                    " config files"
                }));
            }
        }

        private void Reset()
        {
            this._parts = new PartList();
            this._resourceDefinitions = new PartResourceDefinitionList();
            this._configDirectories = new List<ConfigDirectory>();
            this._configDirectories.Add(new ConfigDirectory("parts", "parts", UrlDir.DirectoryType.Parts));
            this._configDirectories.Add(new ConfigDirectory("internals", "internals", UrlDir.DirectoryType.Internals));
            this._configDirectories.Add(new ConfigDirectory("data", "gamedata", UrlDir.DirectoryType.GameData));
        }

        public bool ExistsConfigNode(string url)
        {
            if (this._root.GetConfig(url) == null)
            {
                return false;
            }
            return true;
        }

        public ConfigNode GetConfigNode(string url)
        {
            UrlConfig config = this._root.GetConfig(url);
            if (config == null)
            {
                return null;
            }
            return config.Config;
        }

        public ConfigNode[] GetConfigNodes(string typeName)
        {
            List<ConfigNode> list = new List<ConfigNode>();
            foreach (UrlConfig urlConfig in this._root.GetConfigs(typeName, true))
            {
                list.Add(urlConfig.Config);
            }
            return list.ToArray();
        }

        public ConfigNode[] GetConfigNodes(string baseUrl, string typeName)
        {
            List<ConfigNode> list = new List<ConfigNode>();
            UrlDir directory = this._root.GetDirectory(baseUrl);
            if (directory == null)
            {
                return list.ToArray();
            }
            foreach (UrlConfig urlConfig in directory.GetConfigs(typeName, true))
            {
                list.Add(urlConfig.Config);
            }
            return list.ToArray();
        }

        public UrlConfig[] GetConfigs(string typeName)
        {
            List<UrlConfig> list = new List<UrlConfig>(this._root.GetConfigs(typeName, true));
            UrlConfig[] array = new UrlConfig[list.Count];
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                array[i] = list[i];
            }
            return array;
        }

        public void LoadResourceDefinitions()
        {
            this._resourceDefinitions.Clear();
            UrlConfig[] urlConfigs = this.GetConfigs("RESOURCE_DEFINITION");
            int num = urlConfigs.Length;
            for (int i = 0; i < num; i++)
            {
                PartResourceDefinition def = this._resourceDefinitions.Add(urlConfigs[i]);
            }
        }

        public PartResourceDefinition GetResourceDefinition(string name)
        {
            return this._resourceDefinitions[name];
        }

        public PartResourceDefinition GetResourceDefinition(int id)
        {
            return this._resourceDefinitions[id];
        }

        public void LoadParts()
        {
            this._parts.Clear();
            UrlConfig[] urlConfigs = this.GetConfigs("PART");
            int num = urlConfigs.Length;
            for (int i = 0; i < num; i++)
            {
                Part part = this._parts.Add(urlConfigs[i]);
            }
        }
    }
}
