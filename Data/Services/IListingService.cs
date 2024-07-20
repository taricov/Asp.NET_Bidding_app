namespace BidingApp;

public interface IListingService
{
    IQueryable<Listing> GetAll();
    Task Add(Listing listing);
    Task<Listing> GetById(int? id);
}
