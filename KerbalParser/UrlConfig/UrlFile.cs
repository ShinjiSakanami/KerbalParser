using System;
using System.Collections.Generic;
using System.IO;

namespace KerbalParser
{
    public class UrlFile
    {
        public enum FileType
        {
            Unknown,
            Config,
            Texture,
            Model,
            Audio,
            Assembly,
            AssetBundle
        }

        private string _name;

        private UrlFile.FileType _type;

        private string _fullPath;

        private string _extension;

        private DateTime _fileTime;

        private UrlDir _root;

        private UrlDir _parent;

        private List<UrlConfig> _configs;

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public FileType Type
        {
            get
            {
                return _type;
            }
        }

        public string FullPath
        {
            get
            {
                return _fullPath;
            }
        }

        public string Extension
        {
            get
            {
                return _extension;
            }
        }

        public DateTime FileTime
        {
            get
            {
                return _fileTime;
            }
        }

        public UrlDir Root
        {
            get
            {
                return _root;
            }
        }

        public UrlDir Parent
        {
            get
            {
                return _parent;
            }
        }

        public List<UrlConfig> Configs
        {
            get
            {
                return _configs;
            }
        }

        public string Url
        {
            get
            {
                return this._parent.Url + "/" + this._name;
            }
        }

        public UrlFile(UrlDir parent, FileInfo info)
        {
            this._name = Path.GetFileNameWithoutExtension(info.Name);
            this._fullPath = info.FullName;
            this._extension = Path.GetExtension(info.Name);
            this._fileTime = info.LastWriteTime;
            if (this._extension.Length > 1)
            {
                this._extension = this._extension.Substring(1);
            }
            this._parent = parent;
            this._root = parent.Root;
            if (this._extension == "cfg")
            {
                this._type = UrlFile.FileType.Config;
                this._configs = UrlConfig.CreateNodeList(parent, this);
            }
            else
            {
                this._type = UrlFile.FileType.Unknown;
                this._configs = new List<UrlConfig>();
            }
        }

        public void AddConfig(ConfigNode newConfig)
        {
            if (this._type != UrlFile.FileType.Config)
            {
                Debug.Log("Cannot add config as file is not of type Config");
                return;
            }
            this._configs.Add(new UrlConfig(this, newConfig));
        }

        public void ConfigureFile(ConfigFileType[] fileConfig)
        {
            if (this._type != UrlFile.FileType.Unknown)
            {
                return;
            }
            int num = fileConfig.Length;
            for (int i = 0; i < num; i++)
            {
                ConfigFileType configFileType = fileConfig[i];
                int count = configFileType.Extensions.Count;
                for (int j = 0; j < count; i++)
                {
                    if (configFileType.Extensions[j] == this._extension)
                    {
                        this._type = configFileType.type;
                        return;
                    }
                }
            }
        }

        public bool ContainsConfig(string name)
        {
            return this.GetConfig(name) != null;
        }

        public bool Exists(string url)
        {
            return this.Exists(new UrlIdentifier(url), 0);
        }

        public bool Exists(UrlIdentifier url, int index)
        {
            if (url.UrlDepth == -1)
            {
                return false;
            }
            if (index != url.UrlDepth)
            {
                return false;
            }
            if (this.GetConfig(url[index]) == null)
            {
                return false;
            }
            return true;
        }

        public UrlConfig GetConfig(string name)
        {
            int count = this._configs.Count;
            for (int i = 0; i < count; i++)
            {
                if (this._configs[i].Name == name)
                {
                    return this._configs[i];
                }
            }
            Debug.LogError("Cannot find config in file : " + name);
            return null;
        }

        public void SaveConfigs()
        {
            if (this._type != UrlFile.FileType.Config)
            {
                Debug.Log("Cannot save as file is not of type Config");
                return;
            }
            ConfigNode configNode = new ConfigNode();
            int count = this._configs.Count;
            for (int i = 0; i < count; i++)
            {
                configNode.AddNode(this._configs[i].Config);
            }
            configNode.Save(this._fullPath);
        }

        public override string ToString()
        {
            return this._name;
        }
    }
}
