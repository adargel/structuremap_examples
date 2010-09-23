using System;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace UsingStructureMap.Example5CustomConventions
{
    [TestFixture]
    public class When_auto_registering_types_with_custom_convention
    {
        [TestFixtureSetUp]
        public void ConfigureUsingTypeScanningWithCustomConvention()
        {
            ObjectFactory.Initialize(x => x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.IncludeNamespaceContainingType<EntityFilteredModelBinderConvention>();
                    scan.Convention<EntityFilteredModelBinderConvention>();
                }));
        }

        [Test]
        public void It_returns_model_binders_for_each_class_that_inherits_from_entity()
        {
            var binders = ObjectFactory.GetAllInstances<IFilteredModelBinder>();
            
            binders.Count
                .ShouldEqual(2);

            binders
                .ShouldContainInstanceOfType<IdToEntityModelBinder<Customer>>();

            binders
                .ShouldContainInstanceOfType<IdToEntityModelBinder<Product>>();
        }
    }

    public class EntityFilteredModelBinderConvention : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if (type.IsSubclassOf(typeof(Entity)))
            {
                var openModelBinderType = typeof (IdToEntityModelBinder<>);
                var closedModelBinderType = openModelBinderType.MakeGenericType(type);

                registry.For(typeof(IFilteredModelBinder)).Use(closedModelBinderType);
            }
        }
    }

    public interface IFilteredModelBinder
    {
    }

    public class IdToEntityModelBinder<T> : IFilteredModelBinder
    {
    }

    public class Product : Entity
    {
        
    }

    public class Customer : Entity
    {
        
    }

    public class Entity
    {
        
    }

    
}