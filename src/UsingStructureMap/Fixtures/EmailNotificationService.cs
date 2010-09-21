using System;

namespace UsingStructureMap.Fixtures
{
    public class EmailNotificationService : INotificationService
    {
        public void Notify(string message)
        {
            Console.WriteLine("Notification from Email Service: {0}", message);
        }
    }
}