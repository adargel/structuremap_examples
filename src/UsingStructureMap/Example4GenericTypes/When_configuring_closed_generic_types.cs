using NBehave.Spec.NUnit;
using NUnit.Framework;
using StructureMap;

namespace UsingStructureMap.Example4GenericTypes
{
    [TestFixture]
    public class When_configuring_closed_generic_types
    {
        [TestFixtureSetUp]
        public void ConfigureWithClosedGenericTypes()
        {
            ObjectFactory.Initialize(x =>
                {
                    x.For<IRepository<int, Customer>>().Use < SqlRepository<int, Customer>>();
                    x.For<IRepository<string, Product>>().Use<SqlRepository<string, Product>>();
                });
        }

        [Test]
        public void It_returns_closed_repository_with_entity_type_given()
        {
            ObjectFactory.GetInstance<IRepository<int, Customer>>()
                .ShouldBeInstanceOfType<SqlRepository<int, Customer>>();

            ObjectFactory.GetInstance<IRepository<string, Product>>()
                .ShouldBeInstanceOfType<SqlRepository<string, Product>>();
        }
    }
}