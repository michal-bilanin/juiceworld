namespace Infrastructure.UnitOfWork;

public interface IUnitOfWorkProvider<out TUnitOfWork> where TUnitOfWork : IUnitOfWork
{
    TUnitOfWork Create();
}
