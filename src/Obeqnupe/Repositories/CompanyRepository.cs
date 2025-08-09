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

    public async Task<Company?> FindByIdAsync(Guid id)
    {
        return await _context.Companies
            .Include(c => c.Location)
            .Include(c => c.CompanyType)
            .Include(c => c.Benefits)
            .Include(c => c.Skills)
            .Where(c => c.Id == id).FirstOrDefaultAsync();
    }

    public IQueryable<Company> GetQueryable()
    {
        return _context.Companies
            .Include(c => c.CompanyType)
            .Include(c => c.Location)
            .AsQueryable();
    }
}
