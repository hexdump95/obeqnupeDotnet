using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obeqnupe.Entities;

[Table("company_rating")]
public class CompanyRating
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("score")]
    public float Score { get; set; }

    [Column("rating_id")]
    public long? RatingId { get; set; }

    public virtual Rating? Rating { get; set; }
}
