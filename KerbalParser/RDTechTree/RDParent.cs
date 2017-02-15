using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KerbalParser
{
    public class RDParent
    {
        public RDParentAnchor parent;

        public RDNode.Anchor anchor;

        public RDParent(RDParentAnchor parent, RDNode.Anchor anchor)
        {
            this.parent = parent;
            this.anchor = anchor;
        }

        public RDParent(ConfigNode node, List<RDNode> nodes)
        {
            this.Load(node, nodes);
        }

        public void Save(ConfigNode node)
        {
            node.AddValue("parentID", this.parent.node.id);
            node.AddValue("lineFrom", this.parent.anchor);
            node.AddValue("lineTo", this.anchor);
        }

        public void Load(ConfigNode node, List<RDNode> nodes)
        {
            string text = string.Empty;
            RDNode rDNode = null;
            RDNode.Anchor anchor = RDNode.Anchor.RIGHT;
            RDNode.Anchor anchor2 = RDNode.Anchor.LEFT;
            if (node.HasValue("parentID"))
            {
                text = node.GetValue("parentID");
            }
            if (text != string.Empty)
            {
                rDNode = this.FindNodeByID(text, nodes);
            }
            if (rDNode != null)
            {
                if (node.HasValue("lineFrom"))
                {
                    anchor = (RDNode.Anchor)((int)Enum.Parse(typeof(RDNode.Anchor), node.GetValue("lineFrom")));
                }
                if (node.HasValue("lineTo"))
                {
                    anchor2 = (RDNode.Anchor)((int)Enum.Parse(typeof(RDNode.Anchor), node.GetValue("lineTo")));
                }
                this.parent = new RDParentAnchor(rDNode, anchor);
                this.anchor = anchor2;
            }
            else
            {
                Debug.LogError("No RDNode registered with id " + text + "!");
            }
        }

        public RDNode FindNodeByID(string techID, List<RDNode> nodes)
        {
            int count = nodes.Count;
            for (int i = 0; i < count; i++)
            {
                if (nodes[i].id == techID)
                {
                    return nodes[i];
                }
            }
            return null;
        }
    }
}
