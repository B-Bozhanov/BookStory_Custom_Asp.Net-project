namespace MvcFramework
{
    internal class ServiceCollection : IServiceCollection
    {
        private readonly Dictionary<Type, Type> dependecyContainer = new();

        public void AddService<TSource, TDestination>()
        {
            if (this.dependecyContainer.ContainsKey(typeof(TSource)))
            {
                this.dependecyContainer[typeof(TSource)] = typeof(TDestination);
            }
        }

        public void CreateInstance(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
