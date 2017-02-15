using System.Collections.Generic;

namespace KerbalParser
{
    public class RDNode
    {
        public enum Anchor
        {
            TOP = 1,
            BOTTOM,
            RIGHT,
            LEFT
        }

        public string id;

        public string name;

        public string title;

        public string description;

        public string iconRef;

        public double scale;

        public int scienceCost;

        public bool anyToUnlock;

        public bool hideEmpty;

        private Vector3 _pos;

        private List<RDNode> _children;

        private List<RDParent> _parents;

        private List<Part> _partsAssigned;

        public double PosX
        {
            get
            {
                return this._pos.x;
            }
            set
            {
                this._pos.x = value;
            }
        }

        public double PosY
        {
            get
            {
                return this._pos.y;
            }
            set
            {
                this._pos.y = value;
            }
        }

        public double PosZ
        {
            get
            {
                return this._pos.z;
            }
            set
            {
                this._pos.z = value;
            }
        }

        public List<RDNode> Children
        {
            get
            {
                return this._children;
            }
        }

        public List<RDParent> Parents
        {
            get
            {
                return this._parents;
            }
        }

        public List<Part> PartsAssigned
        {
            get
            {
                return this._partsAssigned;
            }
        }

        public RDNode()
        {
            this.scale = 1.0;
            this.iconRef = "RDicon_generic";
            this.description = "Nothing is known about this technology.";
            this._children = new List<RDNode>();
            this._partsAssigned = new List<Part>();
        }

        public void Load(ConfigNode node)
        {
            if (node.HasValue("id"))
            {
                this.id = node.GetValue("id");
            }
            if (node.HasValue("title"))
            {
                this.title = node.GetValue("title");
            }
            if (node.HasValue("description"))
            {
                this.description = node.GetValue("description");
            }
            if (node.HasValue("nodeName"))
            {
                this.name = node.GetValue("nodeName");
            }
            if (node.HasValue("cost"))
            {
                this.scienceCost = KPUtil.ParseInt(node.GetValue("cost"));
            }
            if (node.HasValue("hideEmpty"))
            {
                this.hideEmpty = bool.Parse(node.GetValue("hideEmpty"));
            }
            if (node.HasValue("anyToUnlock"))
            {
                this.anyToUnlock = bool.Parse(node.GetValue("anyToUnlock"));
            }
            if (node.HasValue("pos"))
            {
                this._pos = KPUtil.ParseVector3(node.GetValue("pos"));
            }
            if (node.HasValue("icon"))
            {
                this.iconRef = node.GetValue("icon");
            }
            if (node.HasValue("scale"))
            {
                this.scale = KPUtil.ParseDouble(node.GetValue("scale"));
            }
        }

        public void LoadParts()
        {
            PartList list = null;
            if (GameDatabase.Instance != null)
            {
                list = GameDatabase.Instance.Parts;
            }
            this._partsAssigned = new List<Part>();
            if (list == null)
            {
                Debug.LogWarning("RDNode: No loaded part lists available!");
                return;
            }
            int count = list.Count;
            foreach(Part part in list)
            {
                if (part.TechRequired == this.id)
                {
                    this._partsAssigned.Add(part);
                }
            }
        }

        public void LoadLinks(ConfigNode node, List<RDNode> nodes)
        {
            List<RDParent> list = new List<RDParent>();
            ConfigNode[] nodes2 = node.GetNodes("Parent");
            int num = nodes2.Length;
            for (int i = 0; i < num; i++)
            {
                list.Add(new RDParent(nodes2[i], nodes));
            }
            this._parents = list;
            int num2 = list.Count;
            for (int j = 0; j < num2; j++)
            {
                if (!list[j].parent.node._children.Contains(this))
                {
                    list[j].parent.node._children.Add(this);
                }
            }
        }

        public void Save(ConfigNode node)
        {
            node.AddValue("nodeName", this.name);
            node.AddValue("anyToUnlock", this.anyToUnlock);
            node.AddValue("icon", this.iconRef);
            node.AddValue("pos", KPUtil.WriteVector(this._pos));
            node.AddValue("scale", this.scale);
        }

        public override string ToString()
        {
            return this.id;
        }
    }
}
