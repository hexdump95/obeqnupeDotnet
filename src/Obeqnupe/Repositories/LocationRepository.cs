using Microsoft.EntityFrameworkCore;

using Obeqnupe.Data;
using Obeqnupe.Entities;

namespace Obeqnupe.Repositories;

public class LocationRepository :  ILocationRepository
{
    private readonly ApplicationDbContext _context;

    public LocationRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Location>> FindAllAsync()
    {
        return await _context.Locations.ToListAsync();
    }

    public async Task<Location?> FindByIdAsync(long id)
    {
        return await  _context.Locations.FindAsync(id);
    }
}
