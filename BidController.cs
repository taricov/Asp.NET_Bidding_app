using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BidingApp.Data;

namespace BidingApp
{
    public class BidController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BidController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bid
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Bids.Include(b => b.Listing).Include(b => b.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bid/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bid = await _context.Bids
                .Include(b => b.Listing)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bid == null)
            {
                return NotFound();
            }

            return View(bid);
        }

        // GET: Bid/Create
        public IActionResult Create()
        {
            ViewData["ListingId"] = new SelectList(_context.Listings, "Id", "IdentityUserId");
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Bid/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Price,IdentityUserId,ListingId")] Bid bid)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bid);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ListingId"] = new SelectList(_context.Listings, "Id", "IdentityUserId", bid.ListingId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", bid.IdentityUserId);
            return View(bid);
        }

        // GET: Bid/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bid = await _context.Bids.FindAsync(id);
            if (bid == null)
            {
                return NotFound();
            }
            ViewData["ListingId"] = new SelectList(_context.Listings, "Id", "IdentityUserId", bid.ListingId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", bid.IdentityUserId);
            return View(bid);
        }

        // POST: Bid/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Price,IdentityUserId,ListingId")] Bid bid)
        {
            if (id != bid.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BidExists(bid.Id))
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
            ViewData["ListingId"] = new SelectList(_context.Listings, "Id", "IdentityUserId", bid.ListingId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", bid.IdentityUserId);
            return View(bid);
        }

        // GET: Bid/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bid = await _context.Bids
                .Include(b => b.Listing)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bid == null)
            {
                return NotFound();
            }

            return View(bid);
        }

        // POST: Bid/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bid = await _context.Bids.FindAsync(id);
            if (bid != null)
            {
                _context.Bids.Remove(bid);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BidExists(int id)
        {
            return _context.Bids.Any(e => e.Id == id);
        }
    }
}
