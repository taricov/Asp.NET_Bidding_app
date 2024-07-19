using BidingApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BidingApp;

public class ListingService: IListingService
{
    private readonly ApplicationDbContext _context;
    public ListingService (ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Listing> Add(Listing listing)
    {
        _context.Listings.Add(listing);
        await _context.SaveChangesAsync();
    }
    public IQueryable<Listing> GetAll()
{
      var applicationDbContext = _context.Listings.Include(b => b.User);
      return applicationDbContext;
    }
    public  Add()
    {

    }

}
