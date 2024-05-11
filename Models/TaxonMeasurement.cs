using Microsoft.EntityFrameworkCore;

namespace Hazo.Models;

public class TaxonMeasurement : IHazoModel
{
    public required string TaxonRef { get; set; }
    public required string CharacterRef { get; set; }
    public double? Minimum { get; set; }
    public double? Maximum { get; set; }

    public Taxon? Taxon { get; set; }
    public MeasurementCharacter? Character { get; set; }

    public static void ConfigureModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaxonMeasurement>()
            .ToTable("Taxon_Measurement")
            .HasKey(mesurement => new { mesurement.TaxonRef, mesurement.CharacterRef });
        modelBuilder.Entity<TaxonMeasurement>()
            .Property(m => m.TaxonRef).HasColumnName("Taxon_Ref");
        modelBuilder.Entity<TaxonMeasurement>()
            .Property(m => m.CharacterRef).HasColumnName("Character_Ref");
        modelBuilder.Entity<TaxonMeasurement>()
            .HasOne(m => m.Taxon)
            .WithMany(taxon => taxon.Mesurements)
            .HasForeignKey(m => m.TaxonRef)
            .HasPrincipalKey(taxon => taxon.DocumentRef);
        modelBuilder.Entity<TaxonMeasurement>()
            .HasOne(m => m.Character)
            .WithMany(ch => ch.TaxonMesurements)
            .HasForeignKey(m => m.CharacterRef)
            .HasPrincipalKey(ch => ch.DocumentRef);
    }
}
