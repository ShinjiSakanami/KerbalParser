namespace KerbalParser
{
    public class RDParentAnchor
    {
        public RDNode node;

        public RDNode.Anchor anchor;

        public RDParentAnchor(RDNode node, RDNode.Anchor anchor)
        {
            this.node = node;
            this.anchor = anchor;
        }
    }
}
