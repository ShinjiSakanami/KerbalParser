namespace KerbalParser
{
    public class PartResource
    {
        public enum FlowMode
        {
            None = 0,
            Out,
            In,
            Both
        }

        public string name;

        public double amount;

        public double maxAmount;

        public bool flowState;

        public bool isTweakable;

        public bool hideFlow;

        public bool isVisible;

        public PartResource.FlowMode flowMode;

        private PartResourceDefinition _info;

        private Part _part;

        public PartResourceDefinition Info
        {
            get
            {
                return this._info;
            }
        }

        public Part Part
        {
            get
            {
                return this._part;
            }
        }

        public PartResource(Part p)
        {
            this._part = p;
            this.Init();
        }

        private void Init()
        {
            this.flowState = true;
            this.isVisible = true;
            this.flowMode = PartResource.FlowMode.Both;
        }

        public void Load(ConfigNode node)
        {
            ConfigNode.LoadObjectFromConfig(this, node, false);
        }

        public void Save(ConfigNode node)
        {
            node.AddValue("name", this._info.name);
            node.AddValue("amount", this.amount);
            node.AddValue("maxAmount", this.maxAmount);
            node.AddValue("flowState", this.flowState);
            node.AddValue("isTweakable", this.isTweakable);
            node.AddValue("hideFlow", this.hideFlow);
            node.AddValue("isVisible", this.isVisible);
            node.AddValue("flowMode", this.flowMode);
        }

        public void Copy(PartResource res)
        {
            this._info = res.Info;
            this.name = res.name;
            this.amount = res.amount;
            this.maxAmount = res.maxAmount;
            this.flowState = res.flowState;
            this.isTweakable = res.isTweakable;
            this.isVisible = res.isVisible;
            this.hideFlow = res.hideFlow;
            this.flowMode = res.flowMode;
        }

        public void SetInfo(PartResourceDefinition info)
        {
            this._info = info;
            this.name = info.name;
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
