using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obeqnupe.Entities;

[Table("company")]
public class Company
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string? Name { get; set; }

    [Column("permalink")]
    [StringLength(255)]
    public string? Permalink { get; set; }

    [Column("page")]
    [StringLength(1000)]
    public string? Page { get; set; }

    [Column("upvotes")]
    public int Upvotes { get; set; }

    [Column("votes")]
    public int Votes { get; set; }

    [Column("score")]
    public float Score { get; set; }

    [Column("location_id")]
    public long? LocationId { get; set; }

    public virtual Location? Location { get; set; }

    [Column("company_type_id")]
    public long? CompanyTypeId { get; set; }

    public virtual CompanyType? CompanyType { get; set; }

    public virtual ICollection<Benefit> Benefits { get; set; } = [];

    public virtual ICollection<Skill> Skills { get; set; } = [];

    public virtual ICollection<CompanyRating> CompanyRatings { get; set; } = [];
}
