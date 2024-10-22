using Infrastructure.UnitOfWork;
using JuiceWorld.Data;

namespace JuiceWorld.UnitOfWork;

public class UnitOfWorkProvider(Func<JuiceWorldDbContext> dbContextFactory): IUnitOfWorkProvider<UnitOfWork>
{
    public UnitOfWork Create()
    {
        return new UnitOfWork(dbContextFactory());
    }
}
