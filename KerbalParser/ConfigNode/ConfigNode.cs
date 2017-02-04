using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace KerbalParser
{
    public class ConfigNode
    {
        public string name;

        public string id;

        public string comment;

        private ConfigValueList _values;

        private ConfigNodeList _nodes;

        public ConfigValueList Values
        {
            get
            {
                return _values;
            }
        }

        public ConfigNodeList Nodes
        {
            get
            {
                return _nodes;
            }
        }

        public int CountValues
        {
            get
            {
                return this._values.Count;
            }
        }

        public int CountNodes
        {
            get
            {
                return this._nodes.Count;
            }
        }

        public ConfigNode()
        {
            this.name = string.Empty;
            this.id = string.Empty;
            this._values = new ConfigValueList();
            this._nodes = new ConfigNodeList();
        }

        public ConfigNode(string name)
        {
            this.name = ConfigNode.CleanupInput(name);
            this.id = string.Empty;
            string[] array = this.name.Split(new char[]
            {
                '(',
                ' ',
                ')'
            }, StringSplitOptions.RemoveEmptyEntries);
            if (array.Length > 1)
            {
                this.name = array[0];
                this.id = array[1];
            }
            this._values = new ConfigValueList();
            this._nodes = new ConfigNodeList();
        }

        public ConfigNode(string name, string comment)
        {
            this.name = ConfigNode.CleanupInput(name);
            this.id = string.Empty;
            this.comment = comment;
            string[] array = this.name.Split(new char[]
            {
                '(',
                ' ',
                ')'
            }, StringSplitOptions.RemoveEmptyEntries);
            if (array.Length > 1)
            {
                this.name = array[0];
                this.id = array[1];
            }
            this._values = new ConfigValueList();
            this._nodes = new ConfigNodeList();
        }

        public static ConfigNode Load(string fileFullName)
        {
            if (!File.Exists(fileFullName))
            {
                Debug.LogWarning("File '" + fileFullName + "' does not exist");
                return null;
            }
            string[] array = File.ReadAllLines(fileFullName);
            if (array == null)
            {
                return null;
            }
            List<string[]> list = ConfigNode.PreFormatConfig(array);
            if (list != null)
            {
                if (list.Count != 0)
                {
                    return ConfigNode.RecurseFormat(list);
                }
            }
            return null;
        }

        public static bool LoadObjectFromConfig(object obj, ConfigNode node, bool removeAfterUse)
        {
            if (!ConfigNode.ReadObject(obj, node))
            {
                return false;
            }
            if (removeAfterUse)
            {
                node.RemoveValues(string.Empty);
                node.RemoveNodes(string.Empty);
                removeAfterUse = false;
            }
            return true;
        }

        public void Save(string fileFullName)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(ConfigNode node)
        {
            this.CopyToRecursive(node, false);
        }

        public void CopyTo(ConfigNode node, bool overwrite)
        {
            this.CopyToRecursive(node, overwrite);
        }

        public void CopyTo(ConfigNode node, string newName)
        {
            this.CopyToRecursive(node, false);
            node.name = newName;
        }

        public ConfigNode CreateCopy()
        {
            ConfigNode node = new ConfigNode();
            this.CopyToRecursive(node, false);
            return node;
        }

        public bool HasData()
        {
            return this._values.Count > 0 || this._nodes.Count > 0;
        }

        public void AddData(ConfigNode node)
        {
            int count = node.Values.Count;
            for (int i = 0; i < count; i++)
            {
                ConfigValue value = node.Values[i];
                this.AddValue(value.name, value.value, value.comment);
            }
            int count2 = node.Nodes.Count;
            for (int j = 0; j < count2; j++)
            {
                ConfigNode node2 = this.AddNode(node.Nodes[j].name);
                node.Nodes[j].CopyTo(node2);
            }
        }

        public void ClearData()
        {
            this._values.Clear();
            this._nodes.Clear();
        }

        public bool HasValue(string name)
        {
            return this._values.Contains(name);
        }

        public bool HasValue()
        {
            return this._values.Count > 0;
        }

        public void AddValue(string name, object value, string comment)
        {
            ConfigValue v = new ConfigValue(name, ConfigNode.CleanupInput(KPUtil.WriteObject(value)), comment);
            this._values.Add(v);
        }

        public void AddValue(string name, string value, string comment)
        {
            ConfigValue v = new ConfigValue(name, ConfigNode.CleanupInput(value), comment);
            this._values.Add(v);
        }

        public void AddValue(string name, object value)
        {
            ConfigValue v = new ConfigValue(name, ConfigNode.CleanupInput(KPUtil.WriteObject(value)));
            this._values.Add(v);
        }

        public void AddValue(string name, string value)
        {
            ConfigValue v = new ConfigValue(name, ConfigNode.CleanupInput(value));
            this._values.Add(v);
        }

        public string GetValue(string name)
        {
            return this._values.GetValue(name);
        }

        public string GetValue(string name, int index)
        {
            return this._values.GetValue(name, index);
        }

        public string[] GetValues()
        {
            return this._values.GetValues();
        }

        public string[] GetValues(string name)
        {
            return this._values.GetValues(name);
        }

        public string[] GetValuesStartWith(string name)
        {
            return this._values.GetValuesStartWith(name);
        }

        public bool SetValue(string name, string newValue, bool createIfNotFound = false)
        {
            return this._values.SetValue(name, newValue, createIfNotFound);
        }

        public bool SetValue(string name, string newValue, int index, bool createIfNotFound = false)
        {
            return this._values.SetValue(name, newValue, index, createIfNotFound);
        }

        public bool SetValue(string name, string newValue, string comment, bool createIfNotFound = false)
        {
            return this._values.SetValue(name, newValue, comment, createIfNotFound);
        }

        public bool SetValue(string name, string newValue, string comment, int index, bool createIfNotFound = false)
        {
            return this._values.SetValue(name, newValue, comment, index, createIfNotFound);
        }

        public void RemoveValue(string name)
        {
            this._values.RemoveValue(name);
        }

        public void RemoveValues(params string[] names)
        {
            int num = names.Length;
            for (int i = 0; i < num; i++)
            {
                this._values.RemoveValues(names[i]);
            }
        }

        public void RemoveValues(string name)
        {
            this._values.RemoveValues(name);
        }

        public void RemoveValuesStartWith(string name)
        {
            this._values.RemoveValuesStartWith(name);
        }

        public void ClearValues()
        {
            this._values.Clear();
        }

        public bool HasNodeId(string id)
        {
            int count = this._nodes.Count;
            for (int i = 0; i < count; i++)
            {
                ConfigNode node = this._nodes[i];
                if (node.id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasNode(string name)
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

        public bool HasNode()
        {
            return this._nodes.Count > 0;
        }

        public ConfigNode AddNode(string name)
        {
            if (name.Trim().Length == 0)
            {
                return null;
            }
            ConfigNode node = new ConfigNode(name);
            this._nodes.Add(node);
            return node;
        }

        public ConfigNode AddNode(string name, string comment)
        {
            if (name.Trim().Length == 0)
            {
                return null;
            }
            ConfigNode node = new ConfigNode(name, comment);
            this._nodes.Add(node);
            return node;
        }

        public ConfigNode AddNode(ConfigNode node)
        {
            this._nodes.Add(node);
            return node;
        }

        public ConfigNode AddNode(string name, ConfigNode node)
        {
            if (name.Trim().Length == 0)
            {
                return null;
            }
            node.name = name;
            this._nodes.Add(node);
            return node;
        }

        public ConfigNode GetNodeId(string id)
        {
            return this._nodes.GetNodeId(id);
        }

        public ConfigNode GetNode(string name)
        {
            return this._nodes.GetNode(name);
        }

        public ConfigNode GetNode(string name, string valueName, string value)
        {
            return this._nodes.GetNode(name, valueName, value);
        }

        public ConfigNode GetNode(string name, int index)
        {
            return this._nodes.GetNode(name, index);
        }

        public ConfigNode[] GetNodes(string name)
        {
            return this._nodes.GetNodes(name);
        }

        public ConfigNode[] GetNodes(string name, string valueName, string value)
        {
            return this._nodes.GetNodes(name, valueName, value);
        }

        public ConfigNode[] GetNodes()
        {
            return this._nodes.GetNodes();
        }

        public bool SetNode(string name, ConfigNode newNode, bool createIfNotFound = false)
        {
            return this._nodes.SetNode(name, newNode, createIfNotFound);
        }

        public bool SetNode(string name, ConfigNode newNode, int index, bool createIfNotFound = false)
        {
            return this._nodes.SetNode(name, newNode, index, createIfNotFound);
        }

        public void RemoveNode(string name)
        {
            this._nodes.RemoveNode(name);
        }

        public void RemoveNode(ConfigNode node)
        {
            this._nodes.Remove(node);
        }

        public void RemoveNodes(string name)
        {
            this._nodes.RemoveNodes(name);
        }

        public void RemoveNodesStartWith(string name)
        {
            this._nodes.RemoveNodesStartWith(name);
        }

        public void ClearNodes()
        {
            this._nodes.Clear();
        }

        public static ConfigNode Parse(string s)
        {
            string[] cfgData = s.Split(new char[]
            {
                '\n',
                '\r'
            });
            return ConfigNode.RecurseFormat(ConfigNode.PreFormatConfig(cfgData));
        }

        public override string ToString()
        {
            return this.name;
        }

        private static string CleanupInput(string value)
        {
            if (value == null)
            {
                Debug.LogError("Input is null\n" + Environment.StackTrace);
                return string.Empty;
            }
            value = value.Replace("\n", string.Empty);
            value = value.Replace("\r", string.Empty);
            value = value.Replace("\t", " ");
            return value;
        }

        private void CopyToRecursive(ConfigNode node, bool overwrite = false)
        {
            if (node.name == string.Empty)
            {
                node.name = this.name;
            }
            if (node.id == string.Empty)
            {
                node.id = this.id;
            }
            if (!string.IsNullOrEmpty(this.comment))
            {
                node.comment = this.comment;
            }
            int count = this._values.Count;
            for (int i = 0; i < count; i++)
            {
                ConfigValue value = this._values[i];
                if (overwrite)
                {
                    node.SetValue(value.name, value.value, value.comment, true);
                }
                else
                {
                    node.AddValue(value.name, value.value, value.comment);
                }
            }
            int count2 = this._nodes.Count;
            for (int j = 0; j < count2; j++)
            {
                ConfigNode node2 = this._nodes[j];
                if (overwrite)
                {
                    node.RemoveNode(node2.name);
                }
                node2.CopyToRecursive(node.AddNode(node2.name), false);
            }
        }

        private static List<string[]> PreFormatConfig(string[] cfgData)
        {
            if (cfgData != null)
            {
                if (cfgData.Length > 0)
                {
                    List<string> list = new List<string>(cfgData);
                    int count = list.Count;
                    for (int i = count - 1; i >= 0; i--)
                    {
                        list[i] = list[i];
                        int num;
                        if ((num = list[i].IndexOf("//")) != -1)
                        {
                            if (num == 0)
                            {
                                list.RemoveAt(i);
                                continue;
                            }
                            list[i] = list[i].Remove(num);
                        }
                        list[i] = list[i].Trim();
                        if (list[i].Length == 0)
                        {
                            list.RemoveAt(i);
                        }
                        else
                        {
                            if ((num = list[i].IndexOf("}", 0)) != -1)
                            {
                                if (num == 0)
                                {
                                    if (list[i].Length == 1)
                                    {
                                        goto FLAG1;
                                    }
                                }
                                if (num > 0)
                                {
                                    list.Insert(i, list[i].Substring(0, num));
                                    i++;
                                    list[i] = list[i].Substring(num);
                                    num = 0;
                                }
                                if (num < list[i].Length - 1)
                                {
                                    list.Insert(i + 1, list[i].Substring(num + 1));
                                    list[i] = "}";
                                    i += 2;
                                }
                                continue;
                            }
                        FLAG1:
                            if ((num = list[i].IndexOf("{", 0)) != -1)
                            {
                                if (num == 0)
                                {
                                    if (list[i].Length == 1)
                                    {
                                        continue;
                                    }
                                }
                                if (num > 0)
                                {
                                    list.Insert(i, list[i].Substring(0, num));
                                    i++;
                                    list[i] = list[i].Substring(num);
                                    num = 0;
                                }
                                if (num < list[i].Length - 1)
                                {
                                    list.Insert(i + 1, list[i].Substring(num + 1));
                                    list[i] = "{";
                                    i += 2;
                                }
                            }
                        }
                    }
                    List<string[]> list2 = new List<string[]>(list.Count);
                    int count2 = list.Count;
                    for (int j = 0; j < count2; j++)
                    {
                        string text = list[j];
                        string[] array = text.Split(new char[]
                        {
                            '='
                        }, 2, StringSplitOptions.None);
                        if (array != null)
                        {
                            if (array.Length != 0)
                            {
                                int num2 = array.Length;
                                for (int k = 0; k < num2; k++)
                                {
                                    array[k] = array[k].Trim();
                                }
                                list2.Add(array);
                            }
                        }
                    }
                    return list2;
                }
            }
            Debug.LogError("Error: Empty config file");
            return null;
        }

        private static ConfigNode RecurseFormat(List<string[]> cfg)
        {
            int num = 0;
            ConfigNode configNode = new ConfigNode("root");
            ConfigNode.RecurseFormat(cfg, ref num, configNode);
            return configNode;
        }

        private static void RecurseFormat(List<string[]> cfg, ref int index, ConfigNode node)
        {
            while (index < cfg.Count)
            {
                if (cfg[index].Length == 2)
                {
                    node.Values.Add(new ConfigValue(cfg[index][0], cfg[index][1]));
                    index++;
                }
                else if (cfg[index][0] == "{")
                {
                    ConfigNode configNode = new ConfigNode(string.Empty);
                    node.Nodes.Add(configNode);
                    index++;
                    ConfigNode.RecurseFormat(cfg, ref index, configNode);
                }
                else
                {
                    if (cfg[index][0] == "}")
                    {
                        index++;
                        return;
                    }
                    if (ConfigNode.NextLineIsOpenBrace(cfg, index))
                    {
                        ConfigNode configNode2 = new ConfigNode(cfg[index][0]);
                        node.Nodes.Add(configNode2);
                        index += 2;
                        ConfigNode.RecurseFormat(cfg, ref index, configNode2);
                    }
                    else
                    {
                        index++;
                    }
                }
            }
        }

        private static bool NextLineIsOpenBrace(List<string[]> cfg, int index)
        {
            int num = index + 1;
            if (num < cfg.Count)
            {
                if (cfg[num].Length == 1)
                {
                    if (cfg[num][0] == "{")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool NextLineIsCloseBrace(List<string[]> cfg, int index)
        {
            int num = index + 1;
            if (num < cfg.Count)
            {
                if (cfg[num].Length == 1)
                {
                    if (cfg[num][0] == "}")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool ReadObject(object obj, ConfigNode node)
        {
            Type type = obj.GetType();
            int count = node.Values.Count;
            for (int i = 0; i < count; i++)
            {
                ConfigValue value = node.Values[i];
                if (value.name == "name" || value.name == "id")
                {
                    continue;
                }
                if (value.name.StartsWith("node_") || value.name.StartsWith("sound_") || value.name.StartsWith("fx_"))
                {
                    continue;
                }
                FieldInfo property = type.GetField(value.name);
                if (property != null)
                {
                    Type type2 = property.FieldType;
                    if (ConfigNode.IsValue(type2))
                    {
                        object obj2 = ConfigNode.ReadValue(type2, value.value);
                        property.SetValue(obj, obj2);
                    }
                }
            }
            return true;
        }

        private static bool IsValue(Type type)
        {
            if (type.IsValueType)
            {
                if (type == typeof(bool))
                {
                    return true;
                }
                else if (type == typeof(byte))
                {
                    return true;
                }
                else if (type == typeof(sbyte))
                {
                    return true;
                }
                else if (type == typeof(char))
                {
                    return true;
                }
                else if (type == typeof(decimal))
                {
                    return true;
                }
                else if (type == typeof(double))
                {
                    return true;
                }
                else if (type == typeof(float))
                {
                    return true;
                }
                else if (type == typeof(int))
                {
                    return true;
                }
                else if (type == typeof(uint))
                {
                    return true;
                }
                else if (type == typeof(long))
                {
                    return true;
                }
                else if (type == typeof(ulong))
                {
                    return true;
                }
                else if (type == typeof(short))
                {
                    return true;
                }
                else if (type == typeof(ushort))
                {
                    return true;
                }
                else if (type == typeof(Vector2))
                {
                    return true;
                }
                else if (type == typeof(Vector3))
                {
                    return true;
                }
                else if (type == typeof(Vector4))
                {
                    return true;
                }
                else if (type == typeof(Quaternion))
                {
                    return true;
                }
                else if (type == typeof(Color))
                {
                    return true;
                }
                else if (type == typeof(Matrix4x4))
                {
                    return true;
                }
                else if (type.IsEnum)
                {
                    return true;
                }
            }
            else if (type == typeof(string))
            {
                return true;
            }
            return false;
        }

        private static object ReadValue(Type fieldType, string value)
        {
            NumberStyles style = NumberStyles.Number | NumberStyles.AllowDecimalPoint;
            CultureInfo culture = CultureInfo.InvariantCulture;
            if (fieldType.IsValueType)
            {
                if (fieldType == typeof(bool))
                {
                    bool flag = false;
                    if (bool.TryParse(value, out flag))
                    {
                        return flag;
                    }
                }
                else if (fieldType == typeof(byte))
                {
                    byte b = 0;
                    if (byte.TryParse(value, style, culture, out b))
                    {
                        return b;
                    }
                }
                else if (fieldType == typeof(sbyte))
                {
                    sbyte b2 = 0;
                    if (sbyte.TryParse(value, style, culture, out b2))
                    {
                        return b2;
                    }
                }
                else if (fieldType == typeof(char))
                {
                    char c = '\0';
                    if (char.TryParse(value, out c))
                    {
                        return c;
                    }
                }
                else if (fieldType == typeof(decimal))
                {
                    decimal num = 0m;
                    if (decimal.TryParse(value, style, culture, out num))
                    {
                        return num;
                    }
                }
                else if (fieldType == typeof(double))
                {
                    double num2 = 0.0;
                    if (double.TryParse(value, style, culture, out num2))
                    {
                        return num2;
                    }
                }
                else if (fieldType == typeof(float))
                {
                    float num3 = 0f;
                    if (float.TryParse(value, style, culture, out num3))
                    {
                        return num3;
                    }
                }
                else if (fieldType == typeof(int))
                {
                    int num4 = 0;
                    if (int.TryParse(value, style, culture, out num4))
                    {
                        return num4;
                    }
                }
                else if (fieldType == typeof(uint))
                {
                    uint num5 = 0u;
                    if (uint.TryParse(value, style, culture, out num5))
                    {
                        return num5;
                    }
                }
                else if (fieldType == typeof(long))
                {
                    long num6 = 0L;
                    if (long.TryParse(value, style, culture, out num6))
                    {
                        return num6;
                    }
                }
                else if (fieldType == typeof(ulong))
                {
                    ulong num7 = 0uL;
                    if (ulong.TryParse(value, style, culture, out num7))
                    {
                        return num7;
                    }
                }
                else if (fieldType == typeof(short))
                {
                    short num8 = 0;
                    if (short.TryParse(value, style, culture, out num8))
                    {
                        return num8;
                    }
                }
                else if (fieldType == typeof(ushort))
                {
                    ushort num9 = 0;
                    if (ushort.TryParse(value, style, culture, out num9))
                    {
                        return num9;
                    }
                }
                else if (fieldType == typeof(Vector2))
                {
                    return KPUtil.ParseVector2(value);
                }
                else if (fieldType == typeof(Vector3))
                {
                    return KPUtil.ParseVector3(value);
                }
                else if (fieldType == typeof(Vector4))
                {
                    return KPUtil.ParseVector4(value);
                }
                else if (fieldType == typeof(Quaternion))
                {
                    return KPUtil.ParseQuaternion(value);
                }
                else if (fieldType == typeof(Color))
                {
                    return KPUtil.ParseColor(value);
                }
                else if (fieldType == typeof(Matrix4x4))
                {
                    return KPUtil.ParseMatrix4x4(value);
                }
                else if (fieldType.IsEnum)
                {
                    return KPUtil.ParseEnum(fieldType, value);
                }
            }
            else if (fieldType == typeof(string))
            {
                return value;
            }
            return null;
        }
    }
}
