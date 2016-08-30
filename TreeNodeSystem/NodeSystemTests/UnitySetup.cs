using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using TreeNodeSystem.System;
using TreeNodeSystem.NodeUtilities;
using TreeNodeSystem.NodeUtilities.Impl;

namespace NodeSystemTests
{
    class UnitySetup
    {
        public static void RegisterTypeMappings(IUnityContainer container)
        {
            container.RegisterType<INodeDescriber, NodeDescriber>(new InjectionConstructor(typeof(IndentedWriter)));
            container.RegisterType<INodeTransformer, NodeTransformer>();
            container.RegisterType<INodeWriter, NodeWriter>();
        }
    }
}
