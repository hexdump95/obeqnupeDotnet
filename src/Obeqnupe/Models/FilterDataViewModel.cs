using Obeqnupe.Entities;

namespace Obeqnupe.Models;

public class FilterDataViewModel
{
    public List<Benefit> Benefits { get; set; } = [];
    public List<Location> Locations { get; set; } = [];
    public List<Skill> Skills { get; set; } = [];
    public List<CompanyType> CompanyTypes { get; set; } = [];
}
