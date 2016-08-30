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
    public class DynamicIoCTests
    {
        IUnityContainer container = null;

        [TestInitialize]
        public void Setup()
        {
            TestOracle.SetupTestOracle();
            container = new UnityContainer();
            UnitySetup.RegisterTypeMappings(container);
        }

        [TestMethod]
        public void RunAllTestsDynamically()
        {
            INodeTransformer transImplementation = container.Resolve<NodeTransformer>();
            INodeDescriber descImplementation = container.Resolve<NodeDescriber>();
            INodeWriter writerImplementation = container.Resolve<NodeWriter>();

            foreach (var testInput in TestOracle.TestCases)
            {
                //check if original input is described correctly
                var result = descImplementation.Describe(testInput.Key);
                Assert.AreEqual(result, testInput.Value.Item1);

                //check if trasnformed input is described correctly
                var transformedNode = transImplementation.Transform(testInput.Key);
                var trasnformedResult = descImplementation.Describe(transformedNode);
                Assert.AreEqual(trasnformedResult, testInput.Value.Item2);

                //test writer for the given test case
                var filePath = string.Format("tmp_{0}.txt", System.Guid.NewGuid());
                writerImplementation.WriteToFileAsync(testInput.Key, filePath).Wait();
                var fileResult = File.ReadAllText(filePath);
                Assert.AreEqual(fileResult, testInput.Value.Item1);
                //remove temporary file
                File.Delete(filePath);
            }
        }
    }
}
