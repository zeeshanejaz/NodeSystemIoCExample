using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeNodeSystem.NodeUtilities;
using TreeNodeSystem.System;

namespace TreeNodeSystem.Models
{
    public abstract class Node
    {
        public string Name { get; }

        protected Node(string name)
        {
            Name = name;
        }
    }
}
