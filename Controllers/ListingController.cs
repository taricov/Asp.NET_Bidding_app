using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BidingApp.Controllers
{
    public class ListingController: Controller
    {
        private readonly IListingService _listingService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ListingController(IListingService listingServ, IWebHostEnvironment webHostEnv)
        {
            _listingService = listingServ;
            _webHostEnvironment = webHostEnv;
        }

        // GET: Listing
        public async Task<IActionResult> Index()
        {
        var applicationDbContext = _listingService.GetAll();
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Listing/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing = await _listingService.GetById(id);
            if (listing == null)
            {
                return NotFound();
            }

            return View(listing);
        }

        // // GET: Listing/Create
        public IActionResult Create()
        {
            return View();
        }
        
        // POST: Listing/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ListingVM listing)
        {
            if(listing.Image != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                string fileName = listing.Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await listing.Image.CopyToAsync(fileStream);
                }

                var listingObj = new Listing
            {
                Title = listing.Title,
                Description = listing.Description,
                Price = listing.Price,
                ImgPath = filePath,
                IdentityUserId = listing.IdentityUserId
            };
                await _listingService.Add(listingObj);
                return RedirectToAction("index");
            }
            return View(listing);
        }

        // // GET: Listing/Edit/5
        // public async Task<IActionResult> Edit(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var listing = await _context.Listings.FindAsync(id);
        //     if (listing == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(listing);
        // }

        // // POST: Listing/Edit/5
        // // To protect from overposting attacks, enable the specific properties you want to bind to.
        // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Price,ImgPath,IsSold,IdentityUserId")] Listing listing)
        // {
        //     if (id != listing.Id)
        //     {
        //         return NotFound();
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(listing);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!ListingExists(listing.Id))
        //             {
        //                 return NotFound();
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(listing);
        // }

        // // GET: Listing/Delete/5
        // public async Task<IActionResult> Delete(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var listing = await _context.Listings
        //         .FirstOrDefaultAsync(m => m.Id == id);
        //     if (listing == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(listing);
        // }

        // // POST: Listing/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(int id)
        // {
        //     var listing = await _context.Listings.FindAsync(id);
        //     if (listing != null)
        //     {
        //         _context.Listings.Remove(listing);
        //     }

        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }

        // private bool ListingExists(int id)
        // {
        //     return _context.Listings.Any(e => e.Id == id);
        // }
    }
}
