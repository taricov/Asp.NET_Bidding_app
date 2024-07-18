namespace BidingApp;

public interface IListingService
{
    IQueryable<Listing> GetAll();

}
