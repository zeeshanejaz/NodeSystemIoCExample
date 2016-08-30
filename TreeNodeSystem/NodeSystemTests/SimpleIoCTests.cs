using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TreeNodeSystem.Models;
using TreeNodeSystem.NodeUtilities;
using TreeNodeSystem.NodeUtilities.Impl;
using TreeNodeSystem.System;
using Microsoft.Practices.Unity;

namespace NodeSystemTests
{
    [TestClass]
    public class SimpleIoCTests
    {
        IUnityContainer container = null;

        [TestInitialize]
        public void Setup()
        {
            container = new UnityContainer();
            UnitySetup.RegisterTypeMappings(container);
        }

        [TestMethod]
        public void SimpleDescriberTest()
        {
            INodeDescriber implementation = container.Resolve<NodeDescriber>();
            var testData = new SingleChildNode("root",
                new TwoChildrenNode("child1",
                    new NoChildrenNode("leaf1"),
                    new SingleChildNode("child2",
                        new NoChildrenNode("leaf2"))));
            var result = implementation.Describe(testData);
        }

        [TestMethod]
        public void SimpleTransformerTest()
        {
            INodeTransformer implementation = container.Resolve<NodeTransformer>();
            var testData = new ManyChildrenNode("root",
                new ManyChildrenNode("child1",
                    new ManyChildrenNode("leaf1"),
                    new ManyChildrenNode("child2",
                        new ManyChildrenNode("leaf2"))));
            var result = implementation.Transform(testData);
        }

        [TestMethod]
        public void SimpleWriterTest()
        {
            INodeWriter implementation = container.Resolve<NodeWriter>();
            var filePath = "output.txt";
            var testData = new NoChildrenNode("root");
            implementation.WriteToFileAsync(testData, filePath).Wait();
            var result = File.ReadAllText(filePath);
        }
    }
}
