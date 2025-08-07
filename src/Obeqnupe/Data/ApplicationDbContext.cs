using Microsoft.EntityFrameworkCore;
using Obeqnupe.Entities;

namespace Obeqnupe.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Benefit> Benefits { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyRating> CompanyRatings { get; set; }

    public virtual DbSet<CompanyType> CompanyTypes { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            
            entity.HasOne(d => d.CompanyType)
                .WithMany()
                .HasForeignKey(d => d.CompanyTypeId);
            
            entity.HasOne(d => d.Location)
                .WithMany()
                .HasForeignKey(d => d.LocationId);
            
            entity
                .HasMany(e => e.Skills)
                .WithMany()
                .UsingEntity(
                    "company_skill",
                    r => r.HasOne(typeof(Skill)).WithMany().HasForeignKey("skill_id").HasPrincipalKey(nameof(Skill.Id)),
                    l => l.HasOne(typeof(Company)).WithMany().HasForeignKey("company_id").HasPrincipalKey(nameof(Company.Id)),
                    j => j.HasKey("company_id", "skill_id"));
            
            entity
                .HasMany(e => e.Benefits)
                .WithMany()
                .UsingEntity(
                    "company_benefit",
                    r => r.HasOne(typeof(Benefit)).WithMany().HasForeignKey("benefit_id").HasPrincipalKey(nameof(Benefit.Id)),
                    l => l.HasOne(typeof(Company)).WithMany().HasForeignKey("company_id").HasPrincipalKey(nameof(Company.Id)),
                    j => j.HasKey("company_id", "benefit_id"));
            
            entity
                .HasMany(e => e.CompanyRatings)
                .WithMany()
                .UsingEntity(
                    "company_company_rating",
                    r => r.HasOne(typeof(CompanyRating)).WithMany().HasForeignKey("company_rating_id").HasPrincipalKey(nameof(CompanyRating.Id)),
                    l => l.HasOne(typeof(Company)).WithMany().HasForeignKey("company_id").HasPrincipalKey(nameof(Company.Id)),
                    j =>
                    {
                        j.HasKey("company_id", "company_rating_id");
                        j.HasIndex("company_rating_id").IsUnique();
                    });
            
        });
        
        modelBuilder.Entity<CompanyRating>(entity =>
        {
            entity.HasOne(d => d.Rating)
                .WithMany()
                .HasForeignKey(d => d.RatingId);
        });

    }

}
