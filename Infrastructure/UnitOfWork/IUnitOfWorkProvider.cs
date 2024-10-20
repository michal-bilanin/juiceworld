namespace Infrastructure.UnitOfWork;

public interface IUnitOfWorkProvider<out TUnitOfWork> where TUnitOfWork : IUnitOfWork
{
    public TUnitOfWork Create();
}
