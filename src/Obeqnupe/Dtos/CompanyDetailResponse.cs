namespace Obeqnupe.Dtos;

public class CompanyDetailResponse
{
    public string? Name { get; set; }
    public string? Page { get; set; }
    public string? LocationName { get; set; }
    public List<string?> Benefits { get; set; } = [];
    public List<string?> Skills { get; set; } = [];
}
