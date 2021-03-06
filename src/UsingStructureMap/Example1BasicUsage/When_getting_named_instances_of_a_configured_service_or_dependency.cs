using NBehave.Spec.NUnit;
using NUnit.Framework;
using StructureMap;
using UsingStructureMap.Fixtures;

namespace UsingStructureMap.Example1BasicUsage
{
    [TestFixture]
    public class When_getting_named_instances_of_a_configured_service_or_dependency
    {
        [TestFixtureSetUp]
        public void IntializeContainer()
        {
            ObjectFactory.Initialize(x =>
            {
                x.For<INotificationService>().Use<EmailNotificationService>().Named("Email");
                x.For<INotificationService>().Use<TwitterNotificationService>().Named("Twitter");
            });
        }

        [Test]
        public void It_returns_the_twitter_instance_of_notification_service_when_retrieved_with_twitter_name()
        {
            ObjectFactory.GetNamedInstance<INotificationService>("Twitter")
                .ShouldBeInstanceOfType<TwitterNotificationService>();
        }

        [Test]
        public void It_returns_the_email_instance_of_notification_service_when_retrieved_with_email_name()
        {
            ObjectFactory.GetNamedInstance<INotificationService>("Email")
                .ShouldBeInstanceOfType<EmailNotificationService>();
        }
    }
}