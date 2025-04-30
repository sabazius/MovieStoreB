namespace MovieStoreB.Models.DTO
{
    public record Movie : CacheItem
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public List<string> ActorIds { get; set; }
    }
}
