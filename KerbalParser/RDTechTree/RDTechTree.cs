using System.Collections.Generic;

namespace KerbalParser
{
    public class RDTechTree
    {
        private ConfigNode _config;

        private List<RDNode> _nodes;

        public RDNode[] Nodes
        {
            get
            {
                return this._nodes.ToArray();
            }
        }

        public int Count
        {
            get
            {
                return this._nodes.Count;
            }
        }

        public RDTechTree()
        {
            this._nodes = new List<RDNode>();
        }

        public static RDTechTree Load(string filePath)
        {
            RDTechTree techTree = new RDTechTree();
            string fileFullPath = KPUtil.PathCombine(GameDatabase.KSPRootPath, filePath);
            ConfigNode configNode = ConfigNode.Load(fileFullPath);
            if (configNode != null)
            {
                if (configNode.HasNode("TechTree"))
                {
                    techTree.Load(configNode.GetNode("TechTree"));
                }
            }
            else
            {
                Debug.LogError("[Tech Tree]: file does not exist or has bad nodes.");
            }
            return techTree;
        }

        public void Load(ConfigNode node)
        {
            this._config = node;
            ConfigNode[] nodes = node.GetNodes("RDNode");
            int num = nodes.Length;
            for (int i = 0; i < num; i++)
            {
                ConfigNode node2 = nodes[i];
                RDNode rDNode = new RDNode();
                rDNode.Load(node2);
                rDNode.LoadParts();
                this._nodes.Add(rDNode);
            }
            for (int j = 0; j < num; j++)
            {
                this._nodes[j].LoadLinks(nodes[j], this._nodes);
            }
        }

        public void Clear()
        {
            this._nodes.Clear();
        }
    }
}
