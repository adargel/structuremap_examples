using System;

namespace UsingStructureMap.Example4GenericTypes
{
    public class SqlRepository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : Entity<TKey>
    {
        public TEntity GetById(TKey id)
        {
            throw new NotImplementedException();
        }
    }
}