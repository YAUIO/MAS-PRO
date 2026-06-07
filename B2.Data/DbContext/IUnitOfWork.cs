namespace B2.Data.DbContext;

public interface IUnitOfWork
{
    void SaveChanges();
    Task SaveChangesAsync();
}