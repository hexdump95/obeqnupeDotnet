using Microsoft.EntityFrameworkCore;

using Obeqnupe.Data;
using Obeqnupe.Entities;

namespace Obeqnupe.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly ApplicationDbContext _context;

    public SkillRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Skill>> FindAllAsync()
    {
        return await _context.Skills.ToListAsync();
    }

    public async Task<Skill?> FindByIdAsync(long id)
    {
        return await _context.Skills.FindAsync(id);
    }
}
