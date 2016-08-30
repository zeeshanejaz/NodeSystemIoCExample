using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeNodeSystem.NodeUtilities;

namespace TreeNodeSystem.Models
{
    public class SingleChildNode : Node
    {
        public Node Child { get; }

        public SingleChildNode(string name, Node child) 
            : base(name)
        {
            Child = child;
        }
    }
}
