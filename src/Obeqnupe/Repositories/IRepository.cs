namespace Obeqnupe.Repositories;

public interface IRepository<T, in TK>
{
    Task<IEnumerable<T>> FindAllAsync();
    Task<T?> FindByIdAsync(TK id);
}
