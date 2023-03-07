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
using Microsoft.CodeAnalysis.CSharp;
using Tweetinvi;
using VaderSharp2;
using Tweetinvi.Core.Models;
using Microsoft.EntityFrameworkCore.Update;

namespace Assignment3maimbriglio.Controllers
{
    public class ActorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Actors
        public async Task<IActionResult> Index()
        {
              return View(await _context.Actor.ToListAsync());
        }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Actor == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor
                .FirstOrDefaultAsync(m => m.id == id);
            if (actor == null)
            {
                return NotFound();
            }

            TweetViewModel tweetViewModel = new TweetViewModel();
            tweetViewModel.actor = actor;
            tweetViewModel.Tweets = new List<TweetWithScore>();

            var userClient = new TwitterClient("AAx9UfdCemph0Pg0t8Moq5c6L", "LbhoERpFGjBESYSNjTHuRvE0R80cGxZBx5lJWanM5lFpO2Hs63", "1455230009153503238-WTxQgoYUAQ3D9PTSsUu8stHkmJvuVe", "2ZVnM9tWbCSNAhyJcyC4WPIgiIbUWZ77MTLSx2Qb8TkW3");
            var searchResponse = await userClient.SearchV2.SearchTweetsAsync(actor.name);
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
                    tweetViewModel.Tweets.Add(tweet);

                }
            }

     
            return View(tweetViewModel);
        }

        // GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,gender,age,IMDB, photo")] Actor actor, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                if(photo != null && photo.Length >0)
                {
                    var memoryStream = new MemoryStream();
                    await photo.CopyToAsync(memoryStream);
                    actor.photo = memoryStream.ToArray();
                }
                _context.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        public async Task<IActionResult> GetActorPhoto(int id)
        {
            var actor = await _context.Actor
                .FirstOrDefaultAsync(m => m.id == id);
            if (actor == null)
            {
                return NotFound();
            }
            var imageData = actor.photo;
            return File(imageData, "image/jpg");
        }



        // GET: Actors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Actor == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }
   
        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,gender,age,IMDB,photo")] Actor actor, IFormFile photo)
        {
            if (id != actor.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (photo != null && photo.Length > 0)
                {
                    var memoryStream = new MemoryStream();
                    await photo.CopyToAsync(memoryStream);
                    actor.photo = memoryStream.ToArray();
                }
                try
                {
                    _context.Update(actor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actor.id))
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
            return View(actor);
        }
        

   

        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Actor == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor
                .FirstOrDefaultAsync(m => m.id == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Actor == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Actor'  is null.");
            }
            var actor = await _context.Actor.FindAsync(id);
            if (actor != null)
            {
                _context.Actor.Remove(actor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
          return _context.Actor.Any(e => e.id == id);
        }
    }
}
