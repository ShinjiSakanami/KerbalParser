using System.Collections;
using System.Collections.Generic;

namespace KerbalParser
{
    public class RDNodeList : IEnumerable, IEnumerable<RDNode>
    {
        private List<RDNode> _nodes;

        public RDNode this[int index]
        {
            get
            {
                return this._nodes[index];
            }
        }

        public int Count
        {
            get
            {
                return this._nodes.Count;
            }
        }

        public RDNodeList()
        {
            this._nodes = new List<RDNode>();
        }

        public void Clear()
        {
            this._nodes.Clear();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._nodes.GetEnumerator();
        }

        IEnumerator<RDNode> IEnumerable<RDNode>.GetEnumerator()
        {
            return this._nodes.GetEnumerator();
        }
    }
}
