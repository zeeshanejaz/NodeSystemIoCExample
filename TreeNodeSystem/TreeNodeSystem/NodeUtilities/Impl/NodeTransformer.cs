using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeNodeSystem.Models;

namespace TreeNodeSystem.NodeUtilities.Impl
{
    public class NodeTransformer : INodeTransformer
    {
        public Node Transform(Node node)
        {
            return recurrsiveTransform(node);
        }

        /// <summary>
        /// Recursively transform the given node
        /// </summary>
        /// <param name="node">Node to transform</param>
        /// <returns>Transformed node</returns>
        public Node recurrsiveTransform(Node node)
        {
            if(node is SingleChildNode)
            {
                return transformSingleChildNode(node as SingleChildNode);
            }
            else if (node is TwoChildrenNode)
            {
                return transformTwoChildrenNode(node as TwoChildrenNode);
            }
            else if(node is ManyChildrenNode)
            {
                return transformManyChildrenNode(node as ManyChildrenNode);
            }
            return node;
        }

        private Node transformManyChildrenNode(ManyChildrenNode node)
        {
            if(null == node.Children)
                return new NoChildrenNode(node.Name);

            //load not null children
            var notNullChildren = node.Children.Where(c => (null != c)).ToArray();

            if (notNullChildren.Length == 0)
            {
                //has no children
                return new NoChildrenNode(node.Name);
            }
            else if (notNullChildren.Length == 1)
            {
                //trasnform the only valid child
                return new SingleChildNode(node.Name,
                    recurrsiveTransform(notNullChildren[0]));
            }
            else if (notNullChildren.Length == 2)
            {
                //trasnform the two valid children
                return new TwoChildrenNode(node.Name,
                    recurrsiveTransform(notNullChildren[0]),
                    recurrsiveTransform(notNullChildren[1]));
            }
            else
            {
                //trasnfrom all children nodes recursively and load them
                return new ManyChildrenNode(node.Name,
                    notNullChildren.Select(n => 
                        recurrsiveTransform(n)).ToArray());
            }
        }

        private Node transformTwoChildrenNode(TwoChildrenNode node)
        {
            if ((null == node.FirstChild) && (null == node.SecondChild))
            {
                return new NoChildrenNode(node.Name);
            }
            else if ((null == node.FirstChild) || (null == node.SecondChild))
            {
                //process the valid child in depth first fashion
                var child = node.FirstChild ?? node.SecondChild;
                var newChild = recurrsiveTransform(child);
                return new SingleChildNode(node.Name, newChild);
            }
            else
            {
                //trasnform both children recursively and create a new node
                var firstChild = recurrsiveTransform(node.FirstChild);
                var secondChild = recurrsiveTransform(node.SecondChild);
                return new TwoChildrenNode(node.Name, firstChild, secondChild);
            }
        }

        private Node transformSingleChildNode(SingleChildNode node)
        {
            if (null == node.Child)
            {
                return new NoChildrenNode(node.Name);
            }
            else
            {
                //has only one child, transform and load into new node
                var newChild = recurrsiveTransform(node.Child);
                return new SingleChildNode(node.Name, newChild);
            }
        }
    }
}
