using MessagePack;

namespace MovieStoreB.Models.DTO
{
    [MessagePackObject]
    public record Movie : ICacheItem<string>
    {
        [Key(0)]
        public string Id { get; set; }

        [Key(1)]
        public string Title { get; set; }

        [Key(2)]
        public int Year { get; set; }

        [Key(3)]
        public List<string> ActorIds { get; set; }

        [Key(4)]
        public DateTime DateInserted { get; set; }

        public string GetKey()
        {
            return Id;
        }
    }
}
