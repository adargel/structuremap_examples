using NBehave.Spec.NUnit;
using NUnit.Framework;
using StructureMap;

namespace UsingStructureMap.Example3AutoRegistrationAndTypeScanning
{
    [TestFixture]
    public class When_auto_registering_types_with_naming_convention
    {
        [TestFixtureSetUp]
        public void SetupUsingConventionBasedNamedInstances()
        {
            ObjectFactory.Initialize(x => x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.IncludeNamespaceContainingType<IWorkflow>();
                    scan.AddAllTypesOf<IWorkflow>().NameBy(type => type.Name.Replace("Workflow", ""));
                }));
        }

        [Test]
        public void It_returns_instances_given_a_name_by_convention()
        {
            ObjectFactory.GetNamedInstance<IWorkflow>("Inventory")
                .ShouldBeInstanceOfType<InventoryWorkflow>();
            
            ObjectFactory.GetNamedInstance<IWorkflow>("Order")
                .ShouldBeInstanceOfType<OrderWorkflow>();

            ObjectFactory.GetNamedInstance<IWorkflow>("Customer")
                .ShouldBeInstanceOfType<CustomerWorkflow>();
        }
    }

    public class InventoryWorkflow : IWorkflow { }
    public class CustomerWorkflow : IWorkflow { }
    public class OrderWorkflow : IWorkflow { }

    public interface IWorkflow
    {
    }
}