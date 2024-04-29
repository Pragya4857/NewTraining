using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Assessment_1_ques2.Models;

namespace Assessment_1_ques2.Controllers
{
    public class MoviesController : Controller
    {
        private IMovieRepos movieRepository;

        public MoviesController()
        {
            movieRepository = new MovieRepo();
        }

        // GET: Movies
        public ActionResult Index()
        {
            var movies = movieRepository.GetMovies();
            return View(movies);
        }

        // GET: Movies/Details/5
        public ActionResult Details(int id)
        {
            var movie = movieRepository.GetMovieById(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Moviename,DateofRelease")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                movieRepository.AddMovie(movie);
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int id)
        {
            var movie = movieRepository.GetMovieById(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Mid,Moviename,DateofRelease")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                movieRepository.UpdateMovie(movie);
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int id)
        {
            var movie = movieRepository.GetMovieById(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            movieRepository.DeleteMovie(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                movieRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
