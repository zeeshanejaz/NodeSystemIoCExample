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
    public class IoCTests
    {
        Node testData = null;
        IUnityContainer container = null;

        [TestInitialize]
        public void Setup()
        {
            testData = new SingleChildNode("root",
                        new TwoChildrenNode("child1",
                            new ManyChildrenNode("child11",
                                new NoChildrenNode("leaf1"),
                                null,
                                new SingleChildNode("child112",
                                    null)),
                            new SingleChildNode("child2",
                                null)));

            container = new UnityContainer();
            UnitySetup.RegisterTypeMappings(container);
        }

        [TestMethod]
        public void NullNodesNoTransformTest()
        {
            INodeDescriber descImplementation = container.Resolve<NodeDescriber>();
            var result = descImplementation.Describe(testData);
            Assert.AreEqual(result, TestOracle.IoCTests_TestData);
        }

        [TestMethod]
        public void NullNodesTransformTest()
        {
            INodeTransformer transImplementation = container.Resolve<NodeTransformer>();
            INodeDescriber descImplementation = container.Resolve<NodeDescriber>();
            var transformedData = transImplementation.Transform(testData);
            var result = descImplementation.Describe(transformedData);
            Assert.AreEqual(result, TestOracle.IoCTests_TestData_NoTransformed);
        }

        [TestMethod]
        public void NullNodesWriteTest()
        {
            INodeWriter writerImplementation = container.Resolve<NodeWriter>();
            var filePath = "output.txt";
            writerImplementation.WriteToFileAsync(testData, filePath).Wait();
            var result = File.ReadAllText(filePath);
            Assert.AreEqual(result, TestOracle.IoCTests_TestData);
        }
    }
}
