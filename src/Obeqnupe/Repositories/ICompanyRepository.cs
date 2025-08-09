using Obeqnupe.Entities;

namespace Obeqnupe.Repositories;

public interface ICompanyRepository : IRepository<Company, Guid>
{
    IQueryable<Company> GetQueryable();
}
