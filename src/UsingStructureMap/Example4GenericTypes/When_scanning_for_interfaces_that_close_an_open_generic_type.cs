using NBehave.Spec.NUnit;
using NUnit.Framework;
using StructureMap;

namespace UsingStructureMap.Example4GenericTypes
{
    [TestFixture]
    public class When_scanning_for_interfaces_that_close_an_open_generic_type
    {
        [TestFixtureSetUp]
        public void ConfigureAutoScanningForTypesThatCloseOpenGenericTypes()
        {
            ObjectFactory.Initialize(x => x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.IncludeNamespaceContainingType<Customer>();
                    scan.ConnectImplementationsToTypesClosing(typeof(ILogger<>));
                }));
        }

        [Test]
        public void It_returns_the_type_that_closes_the_given_interface()
        {
            ObjectFactory.GetInstance<ILogger<Customer>>()
                .ShouldBeInstanceOfType<CustomerLogger>();

            ObjectFactory.GetInstance<ILogger<Product>>()
                .ShouldBeInstanceOfType<ProductLogger>();
        }
    }

    public interface ILogger<T> { }

    public class ProductLogger : ILogger<Product> { }

    public class CustomerLogger : ILogger<Customer> { }
}