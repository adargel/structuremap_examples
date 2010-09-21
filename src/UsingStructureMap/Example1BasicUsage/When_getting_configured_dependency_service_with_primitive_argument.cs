using System.Configuration;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using StructureMap;

namespace UsingStructureMap.Example1BasicUsage
{
    [TestFixture]
    public class When_getting_configured_dependency_service_with_primitive_argument
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            ObjectFactory.Initialize(x =>
            {
                x.For<IOrderRepository>().Use<SqlOrderRepository>().Ctor<string>().Is("connectionString");
                x.For<IInventoryRepository>().Use<SqlInventoryRepository>()
                    .Ctor<string>("user").Is("TheUser")
                    .Ctor<string>("connection").Is("connectionString");
                x.For<IProductRepository>().Use<SqlProductRepository>()
                    .Ctor<string>("connection").EqualToAppSetting("ProductRepositoryConnectionString");
            });
        }

        [Test]
        public void It_returns_the_dependency_with_the_argument_set()
        {
            var repository = ObjectFactory.GetInstance<IOrderRepository>();
            repository.Connection.ShouldEqual("connectionString");
        }

        [Test]
        public void It_returns_the_dependency_with_all_the_arguments_set()
        {
            var repository = ObjectFactory.GetInstance<IInventoryRepository>();
            repository.Connection.ShouldEqual("connectionString");
            repository.User.ShouldEqual("TheUser");
        }

        [Test]
        public void It_returns_the_dependency_with_argument_set_from_config_file()
        {
            var productRepo = ObjectFactory.GetInstance<IProductRepository>();
            productRepo.Connection.ShouldEqual(ConfigurationManager.AppSettings["ProductRepositoryConnectionString"]);
        }
    }

    #region ProductRepository
    public class SqlProductRepository : IProductRepository
    {
        public SqlProductRepository(string connection)
        {
            Connection = connection;
        }

        public string Connection { get; private set; }
    }


    public interface IProductRepository
    {
        string Connection { get; }
    }
    #endregion

    #region InventoryRepository
    public class SqlInventoryRepository : IInventoryRepository
    {
        public SqlInventoryRepository(string connection, string user)
        {
            Connection = connection;
            User = user;
        }

        public string Connection { get; private set; }
        public string User { get; private set; }
    }

    public interface IInventoryRepository
    {
        string Connection { get; }
        string User { get; }
    }

    #endregion

    #region OrderRepository
    public interface IOrderRepository
    {
        string Connection { get; }
    }

    public class SqlOrderRepository : IOrderRepository
    {
        readonly string _connection;

        public SqlOrderRepository(string connection)
        {
            _connection = connection;
        }

        public string Connection
        {
            get { return _connection; }
        }
    }
    #endregion
}