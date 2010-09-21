using NBehave.Spec.NUnit;
using NUnit.Framework;
using StructureMap;
using UsingStructureMap.Fixtures;

namespace UsingStructureMap.Example3AutoRegistrationAndTypeScanning
{
    [TestFixture]
    public class When_scanning_for_all_types_of_a_given_dependency_or_service
    {
        [TestFixtureSetUp]
        public void SetupWithAutoRegistrationForMultipleInstancesOfDependencyOrService()
        {
            ObjectFactory.Initialize(x => x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.IncludeNamespaceContainingType<INotificationService>();
                    scan.AddAllTypesOf<INotificationService>();
                }));    
        }

        [Test]
        public void It_returns_all_instances_of_the_given_dependency_or_service()
        {
            var notifications = ObjectFactory.GetAllInstances<INotificationService>();
            
            notifications.Count
                .ShouldEqual(2);

            notifications
                .ShouldContainInstanceOfType<EmailNotificationService>();

            notifications
                .ShouldContainInstanceOfType<TwitterNotificationService>();
        }

        [Test]
        public void It_injects_all_instances_into_class_that_depends_on_array_of_types()
        {
            var notifier = ObjectFactory.GetInstance<Notifier>();
            
            notifier.Notify("Hello world!!");
        }
    }

    public class Notifier
    {
        readonly INotificationService[] _notifications;

        public Notifier(INotificationService[] notifications)
        {
            _notifications = notifications;
        }

        public void Notify(string message)
        {
            foreach(var notification in _notifications)
            {
                notification.Notify(message);
            }
        }
    }
}