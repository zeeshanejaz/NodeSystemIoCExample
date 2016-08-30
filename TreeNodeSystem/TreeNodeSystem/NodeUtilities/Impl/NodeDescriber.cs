using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeNodeSystem.Models;
using TreeNodeSystem.System;

namespace TreeNodeSystem.NodeUtilities.Impl
{
    public class NodeDescriber: INodeDescriber
    {
        private IndentedWriter writer = null;

        public NodeDescriber(IndentedWriter writer)
        {
            this.writer = writer;
        }

        /// <summary>
        /// Describes the given node in a particular indented format
        /// </summary>
        /// <param name="node">Node to describe</param>
        /// <returns>String respresentation of the given node</returns>
        public string Describe(Node node)
        {
            recursiveDescribe(node, new HashSet<Node>());
            string result = writer.ToString();
            writer.Clear();
            return result;
        }

        /// <summary>
        /// implements a depth first traversal using recurssion
        /// </summary>
        /// <param name="node">node to process</param>
        /// <param name="processed">processed nodes</param>
        public void recursiveDescribe(Node node, HashSet<Node> processed)
        {
            //null node cannot be processed
            if (null == node)
            {
                //should we throw exceptions?
                // throw new ArgumentOutOfRangeException("Null valued nodes are not supported");
                writer.AppendIndentedString("null");
                return;
            }

            //keep track of processed nodes to avoid loops/cycles
            if (processed.Contains(node))
                throw new InvalidOperationException("Loop detected in a tree");

            processed.Add(node);

            //use reflection to load type name
            string typeName = node.GetType().Name;
            string output = string.Format("new {0}(\"{1}\"", typeName, node.Name?? "Unknown");
            writer.AppendIndentedString(output);

            //next level of indentation
            writer.PushIndent();

            if (node is SingleChildNode)
            {
                // print separator between name and the next argument
                writer.AppendRawString(",\r\n");

                var acceptedNode = node as SingleChildNode;
                recursiveDescribe(acceptedNode.Child, processed);
            }
            else if (node is TwoChildrenNode)
            {
                // print separator between name and the next argument
                writer.AppendRawString(",\r\n");

                var acceptedNode = node as TwoChildrenNode;
                recursiveDescribe(acceptedNode.FirstChild, processed);

                // print separator between two arguments
                writer.AppendRawString(",\r\n");

                recursiveDescribe(acceptedNode.SecondChild, processed);
            }
            else if (node is ManyChildrenNode)
            {
                var acceptedNode = node as ManyChildrenNode;
                if ((acceptedNode.Children != null) && (acceptedNode.Children.Count() > 0))
                {
                    // print separator between name and the next argument
                    writer.AppendRawString(",\r\n");

                    //loop over all children
                    foreach (var childNode in acceptedNode.Children)
                    {
                        recursiveDescribe(childNode, processed);

                        // print separator between this and the next argument
                        if (childNode != acceptedNode.Children.Last())
                            writer.AppendRawString(",\r\n");
                    }
                }
            }

            //print the closing of the method call
            writer.AppendRawString(")");

            //return back to previous level of indentation
            writer.PopIndent();
        }
    }
}
