

namespace Application.Interface
{
    public interface IUnitofwork : IDisposable
    {
        IGenericRepository<T> GetRepository<T>() where T : class;

        Task CommitAsync();
    }
}
