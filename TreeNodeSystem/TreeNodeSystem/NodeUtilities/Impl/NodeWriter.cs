using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeNodeSystem.Models;

namespace TreeNodeSystem.NodeUtilities.Impl
{
    public class NodeWriter : INodeWriter
    {
        private INodeDescriber describer = null;

        //for dependency injection
        public NodeWriter(INodeDescriber nodeDescriber)
        {
            this.describer = nodeDescriber;
        }

        /// <summary>
        /// Decribe a node and write to file
        /// </summary>
        /// <param name="node">Node to describe</param>
        /// <param name="filePath">Path to write file</param>
        /// <returns>Asynchronous Task handle</returns>
        public async Task WriteToFileAsync(Node node, string filePath)
        {
            if (null == describer)
                throw new InvalidOperationException("INodeDescriber is not set");

            if (null == node)
                throw new ArgumentNullException("node");

            using (var fileStream = File.OpenWrite(filePath))
            {
                var data = describer.Describe(node);
                var buffer = ASCIIEncoding.ASCII.GetBytes(data);
                await fileStream.WriteAsync(buffer, 0, buffer.Length);
            }
        }
    }
}
