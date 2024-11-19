using Infrastructure.UnitOfWork;
using JuiceWorld.Data;

namespace JuiceWorld.UnitOfWork;

public class OrderUnitOfWorkProvider(Func<JuiceWorldDbContext> contextFactory) : IUnitOfWorkProvider<OrderUnitOfWork>
{
    private readonly Func<JuiceWorldDbContext> _contextFactory = contextFactory;

    public OrderUnitOfWork Create()
    {
        return new OrderUnitOfWork(_contextFactory());
    }
}
