
namespace MovieStoreB.Models.DTO
{
    public record Actor(string Id, string Name) : ICacheItem<string>
    {
        public DateTime DateInserted { get; set; }

        public string GetKey()
        {
            return Id;
        }
    }
}
