using Microsoft.EntityFrameworkCore;

namespace Hazo.Models;

public class TaxonSpecimenLocation : IHazoModel
{
    public required string TaxonRef { get; set; }
    public required int Index { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public Taxon? Taxon { get; set; }

    public static void ConfigureModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaxonSpecimenLocation>()
            .ToTable("Taxon_Specimen_Location")
            .HasKey(location => new { location.TaxonRef, location.Index });
        modelBuilder.Entity<TaxonSpecimenLocation>()
            .Property(location => location.TaxonRef)
            .HasColumnName("Taxon_Ref");
        modelBuilder.Entity<TaxonSpecimenLocation>()
            .Property(location => location.Index)
            .HasColumnName("Specimen_Index");
        modelBuilder.Entity<TaxonSpecimenLocation>()
            .HasOne(location => location.Taxon)
            .WithMany(taxon => taxon.SpecimenLocations)
            .HasForeignKey(location => location.TaxonRef)
            .HasPrincipalKey(taxon => taxon.DocumentRef);
    }
}