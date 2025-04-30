namespace MovieStoreB.Models.DTO
{
    public abstract record CacheItem<T>
    {
        public DateTime DateInserted { get; set; }

        public abstract T GetKey();
    }
}