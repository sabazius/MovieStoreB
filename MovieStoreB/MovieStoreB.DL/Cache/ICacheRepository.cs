namespace MovieStoreB.DL.Cache
{
    public interface ICacheRepository<T> where T : class
    {
        Task<IEnumerable<T?>> FullLoad();

        Task<IEnumerable<T?>> DifLoad(DateTime lastExecuted);
    }
}
