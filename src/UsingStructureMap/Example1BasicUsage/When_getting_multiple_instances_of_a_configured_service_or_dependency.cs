using NBehave.Spec.NUnit;
using NUnit.Framework;
using StructureMap;
using UsingStructureMap.Fixtures;

namespace UsingStructureMap.Example1BasicUsage
{
    [TestFixture]
    public class When_getting_multiple_instances_of_a_configured_service_or_dependency
    {
        [TestFixtureSetUp]
        public void IntializeContainer()
        {
            ObjectFactory.Initialize(x =>
            {
                x.For<INotificationService>().Use<EmailNotificationService>();
                x.For<INotificationService>().Use<TwitterNotificationService>();
            });
        }

        [Test]
        public void It_returns_the_last_registered_instance_of_notification_service()
        {
            ObjectFactory.GetInstance<INotificationService>()
                .ShouldBeInstanceOfType<TwitterNotificationService>();
        }

        [Test]
        public void It_returns_all_instances_of_notification_services_when_all_instances_are_requested()
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