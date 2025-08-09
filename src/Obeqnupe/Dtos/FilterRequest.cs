namespace Obeqnupe.Dtos;

public class FilterRequest
{
    public long? LocationId { get; set; }
    public long? CompanyTypeId { get; set; }
    public string? BenefitIds { get; set; }
    public string? ExcludedBenefitIds { get; set; }
    public string? SkillIds { get; set; }
    public string? ExcludedSkillIds { get; set; }
    public string? Query { get; set; }
}
