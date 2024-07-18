using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BidingApp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public ApplicationDbContext()
    {
    }
    public DbSet<Listing> Listings {get; set;}
    public DbSet<Bid> Bids {get; set;}
    public DbSet<Comment> Comments {get; set;}

}
