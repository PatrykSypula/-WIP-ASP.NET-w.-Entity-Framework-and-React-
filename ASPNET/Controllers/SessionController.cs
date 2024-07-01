using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPNET.Migrations;
using ASPNET.Models;

namespace ASPNET.Controllers
{
    public class SessionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SessionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Session
        public async Task<IActionResult> Index()
        {
			var applicationDbContext = _context.Dictionaries.Include(d => d.User).Include(d => d.Words).Include(d => d.DictionaryLevelValues);
			return View(await applicationDbContext.ToListAsync());
		}

        [HttpGet]
        public async Task<IActionResult> Learn(int? id)
        {
            var words = await _context.Words.Include(w => w.Dictionary).Where(d => d.DictionaryId == id).ToListAsync();
            var randomWord = GetRandomWord(words);

            // Początkowe wartości dla GoodAnswers i AllAnswers
            ViewBag.GoodAnswers = 0;
            ViewBag.AllAnswers = 0;
            ViewBag.RandomWord = randomWord;

            return View(words);
        }

        [HttpPost]
        public async Task<IActionResult> Learn(List<Words> words, string action, int? randomWordId, int goodAnswers, int allAnswers)
        {
            allAnswers++; // Zwiększ AllAnswers za każdym razem, gdy jest przesyłane

            if (action == "RemoveRandomWord" && randomWordId.HasValue)
            {
                var wordToRemove = words.FirstOrDefault(w => w.Id == randomWordId.Value);
                if (wordToRemove != null)
                {
                    words.Remove(wordToRemove);
                    goodAnswers++; // Zwiększ GoodAnswers tylko, gdy słowo jest usunięte

                    if (!words.Any())
                    {
                        // Dodawanie statystyk sesji, gdy lista słów jest pusta
                        var sessionStatistics = new SessionStatistics
                        {
                            SessionDate = DateTime.Now,
                            GoodAnswers = goodAnswers,
                            AllAnswers = allAnswers,
                            Percentage = ((double)goodAnswers / allAnswers).ToString("P"), // Obliczanie procentu
                            DictionaryId = 2, // Przykładowa wartość, dostosuj według potrzeb
                            UserId = 1 // Przykładowa wartość, dostosuj według potrzeb
                        };

                        _context.SessionStatistics.Add(sessionStatistics);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Details", "SessionStatistics", new { id = sessionStatistics.Id });
                    }
                }
            }

            var randomWord = GetRandomWord(words);
            ViewBag.RandomWord = randomWord;
            ViewBag.GoodAnswers = goodAnswers;
            ViewBag.AllAnswers = allAnswers;

            return View(words);
        }

        //[HttpGet]
        //public async Task<IActionResult> SessionStatistics(int id)
        //{
        //    var statistics = await _context.SessionStatistics.Include(s => s.Dictionary).Include(s => s.User).FirstOrDefaultAsync(s => s.Id == id);
        //    if (statistics == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(statistics);
        //}

        private Words GetRandomWord(List<Words> words)
        {
            if (words.Any())
            {
                var random = new Random();
                int index = random.Next(words.Count);
                return words[index];
            }
            return null;
        }

    }
}
