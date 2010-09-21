using NBehave.Spec.NUnit;
using NUnit.Framework;
using StructureMap;
using UsingStructureMap.Fixtures;

namespace UsingStructureMap.Example
{
    [TestFixture]
    public class When_getting_single_instance_of_a_configured_service_or_dependency
    {
        [TestFixtureSetUp]
        public void IntializeContainer()
        {
            ObjectFactory.Initialize(x =>
            {
                x.For<ICustomerRepository>().Use<SqlCustomerRepository>();
                x.For<INotificationService>().Use<EmailNotificationService>();
            });
        }

        [Test]
        public void It_returns_sql_repository_for_customer_repository()
        {
            ObjectFactory.GetInstance<ICustomerRepository>()
                .ShouldBeInstanceOfType<SqlCustomerRepository>();
        }

        [Test]
        public void It_returns_email_notification_for_notification_service()
        {
            ObjectFactory.GetInstance<INotificationService>()
                .ShouldBeInstanceOfType<EmailNotificationService>();
        }
    }
}