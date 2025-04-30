namespace MovieStoreB.Models.DTO
{
    public record Actor(string Id, string Name) : CacheItem<string>
    {
        public override string GetKey()
        {
            return Id;
        }
    }
}
