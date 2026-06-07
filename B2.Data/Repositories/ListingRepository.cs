using B2.Data.DbContext;
using B2.Data.Models;

namespace B2.Data.Repositories;

public class ListingRepository(B2DbContext context) : IListingRepository
{
    public Listing? GetListingById(Guid id)
    {
        return context.Listings.Find(id);
    }
}