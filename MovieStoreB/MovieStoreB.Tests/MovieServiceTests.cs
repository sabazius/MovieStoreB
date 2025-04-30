using Moq;
using MovieStoreB.BL.Interfaces;
using MovieStoreB.BL.Services;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.Tests
{
    public class MovieServiceTests
    {
        private readonly Mock<IMovieRepository> _movieRepositoryMock;
        private readonly Mock<IActorRepository> _actorRepositoryMock;

        private List<Movie> _movies = new List<Movie>()
        {
            new Movie()
            {
                Id = "c3bd1985-792e-4208-af81-4d154bff15c8",
                Title = "Movie 1",
                Year = 2021,
                ActorIds = [
                    "157af604-7a4b-4538-b6a9-fed41a41cf3a",
                    "baac2b19-bbd2-468d-bd3b-5bd18aba98d7"]
            },
            new Movie()
            {
                Id = "4c304bec-f213-47b5-8ae0-9df4a4eb3b99",
                Title = "Movie 2",
                Year = 2022,
                ActorIds = [
                    "157af604-7a4b-4538-b6a9-fed41a41cf3a",
                    "5c93ba13-e803-49c1-b465-d471607e97b3"
                ]
            }
        };

        private List<Actor> _actors = new List<Actor>
        {
            new Actor(default, default) {
                Id = "157af604-7a4b-4538-b6a9-fed41a41cf3a",
                Name = "Actor 1"
            },
            new Actor(default, default) {
                Id = "baac2b19-bbd2-468d-bd3b-5bd18aba98d7",
                Name = "Actor 2"
            },
            new Actor(default, default) {
                Id = "5c93ba13-e803-49c1-b465-d471607e97b3",
                Name = "Actor 3"
            },
        };

        public MovieServiceTests()
        {
            _actorRepositoryMock = new Mock<IActorRepository>();
            _movieRepositoryMock = new Mock<IMovieRepository>();
        }

        [Fact]
        void GetMoviesById_ReturnsData()
        {
            // Arrange
            var movieId = _movies[0].Id;

            _movieRepositoryMock.Setup(x => x.GetMoviesById(It.IsAny<string>()))
                    .Returns((string id) =>
                        _movies.FirstOrDefault(x => x.Id == id));

            var movieService = new MovieService(_movieRepositoryMock.Object, _actorRepositoryMock.Object);

            // Act
            var result = movieService.GetMoviesById(movieId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(movieId, result.Id);
        }

        [Fact]
        void GetMoviesById_MovieNotExist()
        {
            // Arrange
            var movieId = "c3bd1985-792e-4208-af81-4d154bff15c9";

            _movieRepositoryMock.Setup(x => x.GetMoviesById(It.IsAny<string>()))
                    .Returns((string id) =>
                        _movies.FirstOrDefault(x => x.Id == id));

            var movieService = new MovieService(_movieRepositoryMock.Object, _actorRepositoryMock.Object);

            // Act
            var result = movieService.GetMoviesById(movieId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        void GetMoviesById_MovieWithInvalidGuid()
        {
            // Arrange
            var movieId = "c3bd1985-792e-4208-af81-4d154bff15c9-12";

            _movieRepositoryMock.Setup(x => x.GetMoviesById(It.IsAny<string>()))
                    .Returns((string id) =>
                        _movies.First(x => x.Id == id));

            var movieService = new MovieService(_movieRepositoryMock.Object, _actorRepositoryMock.Object);

            // Act
            var result = movieService.GetMoviesById(movieId);

            // Assert
            Assert.Null(result);
        }
    }
}
