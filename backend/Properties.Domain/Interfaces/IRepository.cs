namespace Properties.Domain.Interfaces
{
    public interface IRepository<T, TId>
    {
        Task <IEnumerable<T>> GetAllAsync();
        Task <T?> GetByIdAsync(TId id);
        Task <T> AddAsync(T entity);
    }
}
