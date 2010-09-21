using StructureMap.Configuration.DSL;
using UsingStructureMap.Fixtures;

namespace UsingStructureMap.Example2OrganizingWithRegistries.Registries
{
    public class NotificationRegistry : Registry
    {
        public NotificationRegistry()
        {
            For<INotificationService>().Use<EmailNotificationService>().Named("Email");
            For<INotificationService>().Use<TwitterNotificationService>().Named("Twitter");
        }
    }
}