using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeNodeSystem.Models;

namespace NodeSystemTests
{
    class TestOracle
    {
        public static Dictionary<Node, Tuple<string, string>> TestCases { get; } 
            = new Dictionary<Node, Tuple<string, string>>();

        public static void SetupTestOracle()
        {
            TestCases.Clear();

            Node test1 = new SingleChildNode("root",
                new TwoChildrenNode("child1",
                    new ManyChildrenNode("child11",
                        new NoChildrenNode("leaf1"),
                        null,
                        new SingleChildNode("child112",
                            null)),
                    new SingleChildNode("child2",
                        null)));

            // A.K.A simple describer test
            Node test2 = new SingleChildNode("root",
                new TwoChildrenNode("child1",
                    new NoChildrenNode("leaf1"),
                    new SingleChildNode("child2",
                        new NoChildrenNode("leaf2"))));

            //A.K.A. simple trasnformer test
            Node test3 = new ManyChildrenNode("root",
                new ManyChildrenNode("child1",
                    new ManyChildrenNode("leaf1"),
                    new ManyChildrenNode("child2",
                        new ManyChildrenNode("leaf2"))));

            TestCases.Add(test1,
                new Tuple<string, string>(
                    ExpectedOutputs.test1,
                    ExpectedOutputs.test1_Transformed));

            TestCases.Add(test2,
                new Tuple<string, string>(
                    ExpectedOutputs.test2,
                    ExpectedOutputs.test2_Transformed));

            TestCases.Add(test3,
                new Tuple<string, string>(
                    ExpectedOutputs.test3,
                    ExpectedOutputs.test3_Transformed));
        }

    }
}
