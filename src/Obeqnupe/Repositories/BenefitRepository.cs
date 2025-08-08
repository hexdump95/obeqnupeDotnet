using Microsoft.EntityFrameworkCore;

using Obeqnupe.Data;
using Obeqnupe.Entities;

namespace Obeqnupe.Repositories;

public class BenefitRepository : IBenefitRepository
{
    private readonly ApplicationDbContext _context;

    public BenefitRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Benefit>> FindAllAsync()
    {
        return await _context.Benefits.ToListAsync();
    }

    public async Task<Benefit?> FindByIdAsync(long id)
    {
        return await _context.Benefits.FindAsync(id);
    }
}
