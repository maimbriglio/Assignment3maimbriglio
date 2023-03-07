using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment3maimbriglio.Data;
using Assignment3maimbriglio.Models;
using Assignment3maimbriglio.Data.Migrations;
using System.Numerics;
using Tweetinvi.Core.Injectinvi;
using Tweetinvi;
using VaderSharp2;

namespace Assignment3maimbriglio.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
              return View(await _context.Movie.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            MovieViewModel movieViewModel = new MovieViewModel();
            movieViewModel.movie = movie;
            movieViewModel.Tweets = new List<TweetWithScore>();

            var userClient = new TwitterClient("AAx9UfdCemph0Pg0t8Moq5c6L", "LbhoERpFGjBESYSNjTHuRvE0R80cGxZBx5lJWanM5lFpO2Hs63", "1455230009153503238-WTxQgoYUAQ3D9PTSsUu8stHkmJvuVe", "2ZVnM9tWbCSNAhyJcyC4WPIgiIbUWZ77MTLSx2Qb8TkW3");
            var searchResponse = await userClient.SearchV2.SearchTweetsAsync(movie.Title);
            var tweets = searchResponse.Tweets;
            var analyzer = new SentimentIntensityAnalyzer();


            foreach (var tweetText in tweets)
            {
                var tweet = new TweetWithScore();
                tweet.Text = tweetText.Text;
                var results = analyzer.PolarityScores(tweet.Text);
                if (results.Compound != 0)
                {
                    tweet.Score = results.Compound;
                    movieViewModel.Tweets.Add(tweet);

                }
            }


            return View(movieViewModel);

        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,IMDB,Genre,Year,Photo")] Movie movie, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null && photo.Length > 0)
                {
                    var memoryStream = new MemoryStream();
                    await photo.CopyToAsync(memoryStream);
                    movie.Photo = memoryStream.ToArray();
                }
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        public async Task<IActionResult> GetMoviePhoto(int id)
        {
            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            var imageData = movie.Photo;
            return File(imageData, "image/jpg");
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,IMDB,Genre,Year,Photo")] Movie movie, IFormFile photo)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (photo != null && photo.Length > 0)
                {
                    var memoryStream = new MemoryStream();
                    await photo.CopyToAsync(memoryStream);
                    movie.Photo = memoryStream.ToArray();
                }
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movie == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Movie'  is null.");
            }
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
          return _context.Movie.Any(e => e.Id == id);
        }
    }
}
