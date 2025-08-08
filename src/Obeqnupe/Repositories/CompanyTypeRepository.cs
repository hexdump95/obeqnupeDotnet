using Microsoft.EntityFrameworkCore;

using Obeqnupe.Data;
using Obeqnupe.Entities;

namespace Obeqnupe.Repositories;

public class CompanyTypeRepository : ICompanyTypeRepository
{
    private readonly ApplicationDbContext _context;

    public CompanyTypeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CompanyType>> FindAllAsync()
    {
        return await _context.CompanyTypes.ToListAsync();
    }

    public async Task<CompanyType?> FindByIdAsync(long id)
    {
        return await _context.CompanyTypes.FindAsync(id);
    }
}
