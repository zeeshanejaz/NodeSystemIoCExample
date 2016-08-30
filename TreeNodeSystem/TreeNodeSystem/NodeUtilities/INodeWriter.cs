using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeNodeSystem.Models;

namespace TreeNodeSystem.NodeUtilities
{
    public interface INodeWriter
    {
        Task WriteToFileAsync(Node node, string filePath);
    }
}
