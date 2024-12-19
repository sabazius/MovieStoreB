namespace MovieStoreB.Models.DTO
{
    public class Movie
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public List<string> Actors { get; set; }
    }
}
