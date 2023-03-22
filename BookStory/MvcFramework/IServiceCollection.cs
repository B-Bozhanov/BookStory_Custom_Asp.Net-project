namespace MvcFramework
{
    public interface IServiceCollection
    {
        void AddService<TSource, TDestination>();
        void CreateInstance(Type type);
    }
}
