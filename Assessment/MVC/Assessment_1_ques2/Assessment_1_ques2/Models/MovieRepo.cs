using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using System.Web;

namespace Assessment_1_ques2.Models
{
    public class MovieRepo:IMovieRepos
    {
        private MovieDbContext db = new MovieDbContext();

        public IEnumerable<Movie> GetMovies()
        {
            return db.Movies.ToList();
        }

        public Movie GetMovieById(int id)
        {
            return db.Movies.Find(id);
        }

        public void AddMovie(Movie movie)
        {
            db.Movies.Add(movie);
            db.SaveChanges();
        }

        public void UpdateMovie(Movie movie)
        {
            db.Entry(movie).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteMovie(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}