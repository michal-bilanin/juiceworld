using JuiceWorld.Data;

namespace JuiceWorld.UnitOfWork;

public class UnitOfWorkProvider(Func<JuiceWorldDbContext> dbContextFactory)
{
    public UnitOfWork Create()
    {
        return new UnitOfWork(dbContextFactory());
    }
}
