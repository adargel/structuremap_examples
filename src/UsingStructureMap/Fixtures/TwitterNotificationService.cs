using System;

namespace UsingStructureMap.Fixtures
{
    public class TwitterNotificationService : INotificationService
    {
        public void Notify(string message)
        {
            Console.WriteLine("Notification from Twitter Service: {0}", message);
        }
    }
}