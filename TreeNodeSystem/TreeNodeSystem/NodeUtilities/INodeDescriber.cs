using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeNodeSystem.Models;

namespace TreeNodeSystem.NodeUtilities
{
    public interface INodeDescriber
    {
        string Describe(Node node);
    }
}
