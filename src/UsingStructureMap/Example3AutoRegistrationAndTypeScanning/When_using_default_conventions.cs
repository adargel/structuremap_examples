using NBehave.Spec.NUnit;
using NUnit.Framework;
using StructureMap;

namespace UsingStructureMap.Example3AutoRegistrationAndTypeScanning
{
    [TestFixture]
    public class When_using_default_conventions
    {
        [TestFixtureSetUp]
        public void SetupWithAutoRegistrationUsingDefaultConventions()
        {
            ObjectFactory.Initialize(x => x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.IncludeNamespaceContainingType<IUserRepository>();
                    scan.WithDefaultConventions();
                }));    
        }

        [Test]
        public void It_registers_the_concrete_instances_that_match_the_default_rules()
        {
            ObjectFactory.GetInstance<IUserRepository>()
                .ShouldBeInstanceOfType<UserRepository>();
        }

        [Test]
        [ExpectedException(typeof(StructureMapException))]
        public void It_does_not_register_types_that_do_not_follow_default_convention()
        {
            ObjectFactory.GetInstance<IProductRepository>();
        }
    }

    public class SqlProductRepository : IProductRepository
    {
        
    }

    public interface IProductRepository
    {
            
    }

    public class UserRepository : IUserRepository
    {
    }

    public interface IUserRepository
    {
    }
}