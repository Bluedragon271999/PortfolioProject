using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPGEnemies.Models;
using PagedList;

namespace RPGEnemies.Controllers
{
    public class StatBlocksController : Controller
    {
        private readonly RPGEnemiesContext _context;

        public StatBlocksController(RPGEnemiesContext context)
        {
            _context = context;
        }

        // GET: StatBlocks
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? pageIndex)
        {
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var block = from s in _context.StatBlocks select s;
            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                block = block.Where(s => s.CharacterName.Contains(searchString));
            }
            int pageSize = 10;
            return View(await PaginatedList<StatBlocks>.CreateAsync(block.AsNoTracking(), pageIndex ?? 1, pageSize));
        }

        // GET: StatBlocks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statBlocks = await _context.StatBlocks
                .FirstOrDefaultAsync(m => m.CharacterName == id);
            if (statBlocks == null)
            {
                return NotFound();
            }

            return View(statBlocks);
        }

        // GET: StatBlocks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatBlocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CharacterName,Brawn,Agility,Intellect,Cunning,Willpower,Presence,Description,Talents,Abilities,Weapons,Skills")] StatBlocks statBlocks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statBlocks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statBlocks);
        }

        // GET: StatBlocks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statBlocks = await _context.StatBlocks.FindAsync(id);
            if (statBlocks == null)
            {
                return NotFound();
            }
            return View(statBlocks);
        }

        // POST: StatBlocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CharacterName,Brawn,Agility,Intellect,Cunning,Willpower,Presence,Description,Talents,Abilities,Weapons,Skills")] StatBlocks statBlocks)
        {
            if (id != statBlocks.CharacterName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statBlocks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatBlocksExists(statBlocks.CharacterName))
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
            return View(statBlocks);
        }

        // GET: StatBlocks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statBlocks = await _context.StatBlocks
                .FirstOrDefaultAsync(m => m.CharacterName == id);
            if (statBlocks == null)
            {
                return NotFound();
            }

            return View(statBlocks);
        }

        // POST: StatBlocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var statBlocks = await _context.StatBlocks.FindAsync(id);
            _context.StatBlocks.Remove(statBlocks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatBlocksExists(string id)
        {
            return _context.StatBlocks.Any(e => e.CharacterName == id);
        }
    }
}
