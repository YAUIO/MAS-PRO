using B2.Data.Models;

namespace B2.Data.Repositories;

public interface IListingRepository
{
    Listing? GetListingById(Guid id);
}