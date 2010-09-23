namespace UsingStructureMap.Example4GenericTypes
{
    public interface IRepository<TKey, TEntity> where TEntity : Entity<TKey>
    {
        TEntity GetById(TKey id);
    }
}