using StructureMap.Configuration.DSL;
using UsingStructureMap.Fixtures;

namespace UsingStructureMap.Example2OrganizingWithRegistries.Registries
{
    public class RepositoryRegistry : Registry
    {
        public RepositoryRegistry()
        {
            For<ICustomerRepository>().Use<SqlCustomerRepository>();
        }
    }
}