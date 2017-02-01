using System;
using System.Collections.Generic;
using System.IO;

namespace KerbalParser
{
    public class UrlDir
    {
        public enum DirectoryType
        {
            Parts = 1,
            Internals = 2,
            GameData = 3
        }

        private string _name;

        private UrlDir _root;

        private UrlDir _parent;

        private List<UrlDir> _children;

        private List<UrlFile> _files;

        private string _fullPath;

        private UrlDir.DirectoryType _type;

        public string Name
        {
            get
            {
                return this._name;
            }
        }

        public UrlDir Root
        {
            get
            {
                return this._root;
            }
        }

        public UrlDir Parent
        {
            get
            {
                return this._parent;
            }
        }

        public List<UrlDir> Children
        {
            get
            {
                return this._children;
            }
        }

        public List<UrlFile> Files
        {
            get
            {
                return this._files;
            }
        }

        public string FullPath
        {
            get
            {
                return this._fullPath;
            }
        }

        public UrlDir.DirectoryType Type
        {
            get
            {
                return this._type;
            }
        }

        public string Url
        {
            get
            {
                if (this._parent != null)
                {
                    if (this._parent != this._root)
                    {
                        if (this._parent.Name != string.Empty)
                        {
                            return this._parent.Url + "/" + this._name;
                        }
                    }
                }
                return this._name;
            }
        }

        public UrlDir(ConfigDirectory[] dirConfig, ConfigFileType[] fileConfig)
        {
            this._files = new List<UrlFile>();
            this._children = new List<UrlDir>();
            this._parent = null;
            this._root = this;
            this._name = "root";
            int num = dirConfig.Length;
            for (int i = 0; i < num; i++)
            {
                this._children.Add(new UrlDir(this, dirConfig[i]));
            }
            foreach (UrlFile file in this.GetFiles())
            {
                file.ConfigureFile(fileConfig);
            }
        }

        private UrlDir(UrlDir root, ConfigDirectory rootInfo)
        {
            string path = UrlDir.CreateKSPPath(rootInfo.directory);
            DirectoryInfo info = Directory.CreateDirectory(path);
            this._name = rootInfo.urlRoot;
            this._type = rootInfo.type;
            this.Create(root, info);
        }

        private UrlDir(UrlDir parent, DirectoryInfo info)
        {
            this._name = info.Name;
            this._type = parent.Type;
            this.Create(parent, info);
        }

        private void Create(UrlDir parent, DirectoryInfo info)
        {
            this._fullPath = info.FullName;
            this._parent = parent;
            this._root = parent.Root;
            this._files = new List<UrlFile>();
            this._children = new List<UrlDir>();
            FileInfo[] files = info.GetFiles();
            int num = files.Length;
            for (int i = 0; i < num; i++)
            {
                this._files.Add(new UrlFile(this, files[i]));
            }
            DirectoryInfo[] directories = info.GetDirectories();
            int num2 = directories.Length;
            for (int j = 0; j < num2; j++)
            {
                DirectoryInfo directoryInfo = directories[j];
                if (directoryInfo.Name != "PluginData")
                {
                    this._children.Add(new UrlDir(this, directoryInfo));
                }
            }
        }

        public UrlConfig GetConfig(string url)
        {
            UrlIdentifier urlIdentifier = new UrlIdentifier(url);
            if (urlIdentifier.UrlDepth == -1)
            {
                Debug.LogError("Invalid url: '" + url + "'");
                return null;
            }
            return this.GetConfig(urlIdentifier, 0);
        }

        private UrlConfig GetConfig(UrlIdentifier id, int index)
        {
            if (index == id.UrlDepth)
            {
                int count = this._files.Count;
                for (int i = 0; i < count; i++)
                {
                    UrlFile urlFile = this._files[i];
                    if (urlFile.Type == UrlFile.FileType.Config)
                    {
                        if (urlFile.ContainsConfig(id[index]))
                        {
                            return urlFile.GetConfig(id[index]);
                        }
                    }
                }
            }
            else
            {
                int count2 = this._children.Count;
                for (int j = 0; j < count2; j++)
                {
                    UrlDir urlDir = this._children[j];
                    if (urlDir.Name != string.Empty)
                    {
                        if (id[index] == urlDir.Name)
                        {
                            return urlDir.GetConfig(id, index + 1);
                        }
                    }
                    else
                    {
                        UrlConfig config = urlDir.GetConfig(id, index);
                        if (config != null)
                        {
                            return config;
                        }
                    }
                }
                int count3 = this._files.Count;
                for (int k = 0; k < count3; k++)
                {
                    UrlFile urlFile2 = this._files[k];
                    if (urlFile2.Type == UrlFile.FileType.Config)
                    {
                        if (id[index] == urlFile2.Name)
                        {
                            return urlFile2.GetConfig(id[index + 1]);
                        }
                    }
                }
            }
            return null;
        }

        public bool ConfigExists(string url)
        {
            return this.GetConfig(url) != null;
        }

        public UrlDir CreateDirectory(string urlDir)
        {
            UrlIdentifier urlIdentifier = new UrlIdentifier(this.Url);
            if (urlIdentifier.UrlDepth == -1)
            {
                return null;
            }
            return this.CreateDirectory(urlIdentifier, 0);
        }

        private UrlDir CreateDirectory(UrlIdentifier id, int index)
        {
            if (index == id.UrlDepth)
            {
                int count = this._children.Count;
                for (int i = 0; i < count; i++)
                {
                    if (id[index] == this._children[i].Name)
                    {
                        return this._children[i];
                    }
                }
                DirectoryInfo info = Directory.CreateDirectory(Path.Combine(this._fullPath, id[index]));
                UrlDir urlDir = new UrlDir(this, info);
                this._children.Add(urlDir);
                return urlDir;
            }
            int count2 = this._children.Count;
            for (int j = 0; j < count2; j++)
            {
                if (id[index] == this._children[j].Name)
                {
                    return this._children[j].CreateDirectory(id, index + 1);
                }
            }
            return null;
        }

        public UrlDir GetDirectory(string url)
        {
            UrlIdentifier urlIdentifier = new UrlIdentifier(url);
            if (urlIdentifier.UrlDepth == -1)
            {
                return null;
            }
            return this.GetDirectory(new UrlIdentifier(url), 0);
        }

        private UrlDir GetDirectory(UrlIdentifier id, int index)
        {
            if (index == id.UrlDepth)
            {
                int count = this._children.Count;
                for (int i = 0; i < count; i++)
                {
                    if (id[index] == this._children[i].Name)
                    {
                        return this._children[i];
                    }
                }
            }
            else
            {
                int count2 = this._children.Count;
                for (int j = 0; j < count2; j++)
                {
                    if (id[index] == this._children[j].Name)
                    {
                        return this._children[j].GetDirectory(id, index + 1);
                    }
                }
            }
            return null;
        }

        public bool FileExists(string url)
        {
            return this.GetFile(url) != null;
        }

        public UrlFile GetFile(string url)
        {
            UrlIdentifier urlIdentifier = new UrlIdentifier(url);
            if (urlIdentifier.UrlDepth == -1)
            {
                return null;
            }
            return this.GetFile(new UrlIdentifier(url), 0);
        }

        private UrlFile GetFile(UrlIdentifier id, int index)
        {
            if (index == id.UrlDepth)
            {
                int count = this._files.Count;
                for (int i = 0; i < count; i++)
                {
                    if (id[index] == this._children[i].Name)
                    {
                        return this._files[i];
                    }
                }
            }
            else
            {
                int count2 = this._children.Count;
                for (int j = 0; j < count2; j++)
                {
                    if (id[index] == this._children[j].Name)
                    {
                        return this._children[j].GetFile(id, index + 1);
                    }
                }
            }
            return null;
        }

        public bool DirectoryExists(string url)
        {
            return this.GetDirectory(url) != null;
        }

        public static string CreateKSPPath(string relativePath)
        {
            if (String.IsNullOrEmpty(GameDatabase.KSPRootPath))
            {
                Debug.LogError("KSP root path is not defined!");
                return relativePath;
            }
            return Path.Combine(GameDatabase.KSPRootPath, relativePath);
        }

        public static bool IsValidKSPPath(string kspPath)
        {
            if (!Directory.Exists(kspPath))
            {
                return false;
            }
            if (!Directory.Exists(Path.Combine(kspPath, "gamedata")))
            {
                return false;
            }
            if (!Directory.Exists(Path.Combine(kspPath, "parts")))
            {
                return false;
            }
            if (!Directory.Exists(Path.Combine(kspPath, "internals")))
            {
                return false;
            }
            if (!File.Exists(Path.Combine(kspPath, "KSP.exe")))
            {
                return false;
            }
            if (!File.Exists(Path.Combine(kspPath, "readme.txt")))
            {
                return false;
            }
            return true;
        }

        public UrlDir[] GetDirectories(bool recursive = true)
        {
            List<UrlDir> list = new List<UrlDir>();
            foreach (UrlDir child in this._children)
            {
                list.Add(child);
                if (recursive)
                {
                    list.AddRange(child.GetDirectories());
                }
            }
            return list.ToArray();
        }

        public UrlFile[] GetFiles()
        {
            List<UrlFile> list = new List<UrlFile>();
            list.AddRange(this._files);
            foreach (UrlDir child in this._children)
            {
                list.AddRange(child.GetFiles());
            }
            return list.ToArray();
        }

        public UrlFile[] GetFiles(UrlFile.FileType type)
        {
            List<UrlFile> list = new List<UrlFile>();
            foreach (UrlFile file in this._files)
            {
                if (file.Type == type)
                {
                    list.Add(file);
                }
            }
            foreach (UrlDir child in this._children)
            {
                list.AddRange(child.GetFiles(type));
            }
            return list.ToArray();
        }

        public UrlFile[] GetConfigFiles()
        {
            List<UrlFile> list = new List<UrlFile>();
            foreach (UrlFile file in this._files)
            {
                if (file.Type == UrlFile.FileType.Config)
                {
                    list.Add(file);
                }
            }
            foreach (UrlDir child in this._children)
            {
                list.AddRange(child.GetConfigFiles());
            }
            return list.ToArray();
        }

        public UrlConfig[] GetConfigs()
        {
            List<UrlConfig> list = new List<UrlConfig>();
            foreach (UrlFile file in this._files)
            {
                list.AddRange(file.Configs);
            }
            foreach (UrlDir child in this._children)
            {
                list.AddRange(child.GetConfigs());
            }
            return list.ToArray();
        }

        public UrlConfig[] GetConfigs(string typeName, bool recursive = true)
        {
            List<UrlConfig> list = new List<UrlConfig>();
            foreach (UrlFile file in this._files)
            {
                foreach (UrlConfig config in file.Configs)
                {
                    if (config.Type == typeName)
                    {
                        list.Add(config);
                    }
                }
            }
            if (recursive)
            {
                foreach (UrlDir child in this._children)
                {
                    list.AddRange(child.GetConfigs(typeName, true));
                }
            }
            return list.ToArray();
        }

        public UrlConfig[] GetConfigs(string typeName, string name, bool recursive = true)
        {
            List<UrlConfig> list = new List<UrlConfig>();
            foreach (UrlFile file in this._files)
            {
                foreach (UrlConfig config in file.Configs)
                {
                    if (config.Type == typeName && config.Name == name)
                    {
                        list.Add(config);
                    }
                }
            }
            if (recursive)
            {
                foreach (UrlDir child in this._children)
                {
                    list.AddRange(child.GetConfigs(typeName, true));
                }
            }
            return list.ToArray();
        }

        public override string ToString()
        {
            return this._name;
        }
    }
}
