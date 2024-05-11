using Microsoft.EntityFrameworkCore;

namespace Hazo.Models;

public class TaxonDescription : IHazoModel
{
    public required string TaxonRef { get; set; }
    public required string DescriptionRef { get; set; }

    public Taxon? Taxon { get; set; }
    public Document? Description { get; set; }

    public static void ConfigureModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaxonDescription>()
            .ToTable("Taxon_Description")
            .HasKey(mesurement => new { mesurement.TaxonRef, mesurement.DescriptionRef });
        modelBuilder.Entity<TaxonDescription>()
            .Property(m => m.TaxonRef).HasColumnName("Taxon_Ref");
        modelBuilder.Entity<TaxonDescription>()
            .Property(m => m.DescriptionRef).HasColumnName("Description_Ref");
    }
}
