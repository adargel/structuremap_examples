using NBehave.Spec.NUnit;
using NUnit.Framework;
using StructureMap;
using UsingStructureMap.Example2OrganizingWithRegistries.Registries;
using UsingStructureMap.Fixtures;

namespace UsingStructureMap.Example2OrganizingWithRegistries
{
    [TestFixture]
    public class When_configuring_structure_map_with_registries
    {
        [TestFixtureSetUp]
        public void SetupWithAutoRegistration()
        {
            ObjectFactory.Initialize(x => x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.IncludeNamespaceContainingType<RepositoryRegistry>();
                    scan.LookForRegistries();
                }));
        }

        [Test]
        public void It_registered_customer_repository()
        {
            ObjectFactory.GetInstance<ICustomerRepository>()
                .ShouldBeInstanceOfType<SqlCustomerRepository>();
        }

        [Test]
        public void It_registered_notification_services()
        {
            var notifications = ObjectFactory.GetAllInstances<INotificationService>();
            
            notifications.Count
                .ShouldEqual(2);

            notifications
                .ShouldContainInstanceOfType<EmailNotificationService>();
            
            notifications
                .ShouldContainInstanceOfType<TwitterNotificationService>();
        }
    }
}