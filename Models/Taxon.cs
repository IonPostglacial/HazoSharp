using Microsoft.EntityFrameworkCore;

namespace Hazo.Models;

public class Taxon : IDocumentTable
{
    public required string DocumentRef { get; set; }
    public string? Author { get; set; }
    public string? Website { get; set; }
    public string? Meaning { get; set; }
    public string? HerbariumNumber { get; set; }
    public string? HerbariumPicture { get; set; }
    public int? Fasc { get; set; }
    public int? Page { get; set; }

    public Document? Document { get; set; }
    public List<TaxonMeasurement>? Mesurements { get; set; }
    public List<Document>? Descriptions { get; set; }
    public List<TaxonBookInfo>? BookInfo { get; set; }
    public List<TaxonSpecimenLocation>? SpecimenLocations { get; set; }

    public static void ConfigureModel(ModelBuilder modelBuilder)
    {
        IDocumentTable.Configure<Taxon>(modelBuilder, "Taxon");

        modelBuilder.Entity<Taxon>().Property(t => t.HerbariumNumber).HasColumnName("Herbarium_No");
        modelBuilder.Entity<Taxon>().Property(t => t.HerbariumPicture).HasColumnName("Herbarium_Picture");
        modelBuilder.Entity<Taxon>()
            .HasMany(taxon => taxon.Descriptions)
            .WithMany(desc => desc.DescribedTaxons)
            .UsingEntity<TaxonDescription>(
                left => left.HasOne(desc => desc.Description)
                    .WithMany()
                    .HasForeignKey(desc => desc.DescriptionRef),
                right => right.HasOne(desc => desc.Taxon)
                    .WithMany()
                    .HasForeignKey(desc => desc.TaxonRef)
            );
    }
}
