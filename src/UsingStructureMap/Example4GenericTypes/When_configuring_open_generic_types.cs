using NBehave.Spec.NUnit;
using NUnit.Framework;
using StructureMap;

namespace UsingStructureMap.Example4GenericTypes
{
    [TestFixture]
    public class When_configuring_open_generic_types
    {
        [SetUp]
        public void SetupAnOpenGenericType()
        {
            ObjectFactory.Initialize(x => x.For(typeof(IRepository<,>)).Use(typeof(SqlRepository<,>)));
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