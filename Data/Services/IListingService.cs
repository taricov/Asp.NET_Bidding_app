namespace BidingApp;

public interface IListingService
{
    IQueryable<Listing> GetAll();
    Task Add(Listing listing);
}
