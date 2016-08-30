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
            Assert.AreEqual(result, ExpectedOutputs.test2);
        }

        [TestMethod]
        public void SimpleTransformerTest()
        {
            INodeDescriber descImplementation = container.Resolve<NodeDescriber>();
            INodeTransformer implementation = container.Resolve<NodeTransformer>();
            var testData = new ManyChildrenNode("root",
                new ManyChildrenNode("child1",
                    new ManyChildrenNode("leaf1"),
                    new ManyChildrenNode("child2",
                        new ManyChildrenNode("leaf2"))));
            var trasnformed = implementation.Transform(testData);
            var result = descImplementation.Describe(trasnformed);
            Assert.AreEqual(result, ExpectedOutputs.test3_Transformed);
        }

        [TestMethod]
        public void SimpleWriterTest()
        {
            INodeWriter implementation = container.Resolve<NodeWriter>();
            var filePath = string.Format("tmp_{0}.txt", System.Guid.NewGuid());
            var testData = new NoChildrenNode("root");
            implementation.WriteToFileAsync(testData, filePath).Wait();
            var result = File.ReadAllText(filePath);
            Assert.AreEqual(result, "new NoChildrenNode(\"root\")");
            File.Delete(filePath);
        }
    }
}
