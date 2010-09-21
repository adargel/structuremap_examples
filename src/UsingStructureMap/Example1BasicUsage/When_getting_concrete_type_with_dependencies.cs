using NBehave.Spec.NUnit;
using NUnit.Framework;
using StructureMap;
using UsingStructureMap.Fixtures;

namespace UsingStructureMap.Example1BasicUsage
{
    [TestFixture]
    public class When_getting_concrete_type_with_dependencies
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            ObjectFactory.Initialize(x =>
                {
                    x.For<ICustomerRepository>().Use<SqlCustomerRepository>();
                    x.For<INotificationService>().Use<EmailNotificationService>();
                });
        }

        [Test]
        public void It_returns_the_instance_with_dependencies_filled()
        {
            var hasDependencies = ObjectFactory.GetInstance<DependsOnRepositoryAndNotification>();
            
            hasDependencies.Notification
                .ShouldBeInstanceOfType<EmailNotificationService>();

            hasDependencies.Repository
                .ShouldBeInstanceOfType<SqlCustomerRepository>();
        }
    }

    public class DependsOnRepositoryAndNotification
    {
        readonly ICustomerRepository _repository;
        readonly INotificationService _notification;

        public DependsOnRepositoryAndNotification(ICustomerRepository repository, INotificationService notification)
        {
            _repository = repository;
            _notification = notification;
        }

        public ICustomerRepository Repository
        {
            get { return _repository; }
        }

        public INotificationService Notification
        {
            get { return _notification; }
        }
    }
}