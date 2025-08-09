using Microsoft.EntityFrameworkCore;

using Obeqnupe.Data;
using Obeqnupe.Entities;

namespace Obeqnupe.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationDbContext _context;

    public CompanyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Company>> FindAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Company?> FindByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Company> GetQueryable()
    {
        return _context.Companies
            .Include(c => c.CompanyType)
            .Include(c => c.Location)
            .AsQueryable();
    }
}
