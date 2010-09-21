using NBehave.Spec.NUnit;
using NUnit.Framework;
using StructureMap;

namespace UsingStructureMap.Example
{
    [TestFixture]
    public class SimplestPossibleUsageOfStructureMap
    {
        [TestFixtureSetUp]
        public void IntializeContainer()
        {
            ObjectFactory.Initialize(x =>
            {
                x.For<ICustomerRepository>().Use<SqlCustomerRepository>();
                x.For<INotificationService>().Use<EmailNotificationService>();
                x.For<INotificationService>().Use<TwitterNotificationService>();
            });
        }

        [Test]
        public void It_returns_sql_repository_for_customer_repository()
        {
            ObjectFactory.GetInstance<ICustomerRepository>() 
                .ShouldBeInstanceOfType<SqlCustomerRepository>();
        }

        [Test]
        public void It_returns_all_instances_of_notification_services_when_all_instances_are_requested()
        {
            var notifications = ObjectFactory.GetAllInstances<INotificationService>();
            
            notifications.Count.ShouldEqual(2);
            notifications.ShouldContainInstanceOfType<EmailNotificationService>();
            notifications.ShouldContainInstanceOfType<TwitterNotificationService>();
        }

    }

    public class TwitterNotificationService : INotificationService
    {
    }

    public class EmailNotificationService : INotificationService
    {
    }

    public interface INotificationService
    {
    }

    public class SqlCustomerRepository : ICustomerRepository
    {
    }

    public interface ICustomerRepository
    {
    }
}