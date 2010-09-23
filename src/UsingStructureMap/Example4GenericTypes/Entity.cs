namespace UsingStructureMap.Example4GenericTypes
{
    public class Entity<TKey>
    {
        public TKey Id { get; private set; }
    }
}