namespace B2.Data.DbContext;

public class UnitOfWork(B2DbContext ctx) : IUnitOfWork
{
    public void SaveChanges() => ctx.SaveChanges();

    public async Task SaveChangesAsync() => await ctx.SaveChangesAsync();
}