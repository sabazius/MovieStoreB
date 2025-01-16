﻿using MovieStoreB.BL.Interfaces;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.BL.Services
{
    internal class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorRepository _actorRepository;

        public MovieService(IMovieRepository movieRepository, IActorRepository actorRepository)
        {
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
        }

        public List<Movie> GetMovies()
        {
            return _movieRepository.GetMovies();
        }

        public void AddMovie(Movie movie)
        {
            if (movie == null || movie.Actors == null) return;

            foreach (var actor in movie.Actors)
            {
                if (!Guid.TryParse(actor, out _)) return;
            }

            _movieRepository.AddMovie(movie);
        }

        public void DeleteMovie(string id)
        {
            if (!string.IsNullOrEmpty(id)) return;

            _movieRepository.DeleteMovie(id);
        }

        public Movie? GetMoviesById(string id)
        {
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out var movieId))
            {
                return null;
            }

            return _movieRepository.GetMoviesById(movieId.ToString());
        }

        public void AddActor(string movieId, Actor actor) 
        {
            if (string.IsNullOrEmpty(movieId) || actor == null) return;

            if (!Guid.TryParse(movieId, out _)) return;

            var movie = _movieRepository.GetMoviesById(movieId);

            if (movie == null) return;

            if (movie.Actors == null)
            {
                movie.Actors = new List<string>();
            }

            if (actor.Id == null || string.IsNullOrEmpty(actor.Id) || Guid.TryParse(actor.Id, out _) == false) return;

            var existingActor = _actorRepository.GetById(actor.Id);

            if (existingActor != null) return;

            movie.Actors.Add(actor.Id);
        }
    }
}
