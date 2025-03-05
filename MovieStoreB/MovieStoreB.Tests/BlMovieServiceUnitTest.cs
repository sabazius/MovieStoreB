using Moq;
using MovieStoreB.BL.Interfaces;
using MovieStoreB.BL.Services;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.Tests
{
    public class BlMovieServiceUnitTest
    {
        private readonly Mock<IMovieService> _movieServiceMock;
        private readonly Mock<IActorRepository> _actorRepositoryMock;

        private List<Movie> _movies = new List<Movie>()
        {
            new Movie()
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Movie 1",
                Year = 2021,
                Actors = [
                    "157af604-7a4b-4538-b6a9-fed41a41cf3a",
                    "baac2b19-bbd2-468d-bd3b-5bd18aba98d7"]
            },
            new Movie()
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Movie 2",
                Year = 2022,
                Actors = [
                    "157af604-7a4b-4538-b6a9-fed41a41cf3a",
                    "5c93ba13-e803-49c1-b465-d471607e97b3"
                ]
            }
        };

        private List<Actor> _actors = new List<Actor>
        {
            new Actor()
            {
                Id = "157af604-7a4b-4538-b6a9-fed41a41cf3a",
                Name = "Actor 1"
            },
            new Actor()
            {
                Id = "baac2b19-bbd2-468d-bd3b-5bd18aba98d7",
                Name = "Actor 2"
            },
            new Actor()
            {
                Id = "5c93ba13-e803-49c1-b465-d471607e97b3",
                Name = "Actor 3"
            },
        };

        public BlMovieServiceUnitTest()
        {
            _movieServiceMock = new Mock<IMovieService>();
            _actorRepositoryMock = new Mock<IActorRepository>();
        }

        [Fact]
        public async Task GetAllMovieDetails_ReturnsData()
        {
            //setup
            var expectedCount = 2;

            _movieServiceMock
                .Setup(x => x.GetMovies())
                .ReturnsAsync(_movies);

            _actorRepositoryMock
                .Setup(repo =>
                    repo.GetById(It.IsAny<string>()))
                    .Returns((string id) =>
                        _actors.FirstOrDefault(x => x.Id == id));

            //inject
            var blMovieService = new BlMovieService(
                _movieServiceMock.Object,
                _actorRepositoryMock.Object);

            //act
            var result =
                await blMovieService.GetAllMovieDetails();

            //assert
            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.Count);
        }
    }
}