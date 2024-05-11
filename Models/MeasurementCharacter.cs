using Microsoft.EntityFrameworkCore;

namespace Hazo.Models;

public class MeasurementCharacter : IDocumentTable
{
    public required string DocumentRef { get; set; }
    public string? UnitRef { get; set; }
    public string? Color { get; set; }

    public Document? Document { get; set; }
    public Unit? Unit { get; set; }

    public List<TaxonMeasurement>? TaxonMesurements { get; set; }

    public static void ConfigureModel(ModelBuilder modelBuilder)
    {
        IDocumentTable.Configure<MeasurementCharacter>(modelBuilder, "Measurement_Character");

        modelBuilder.Entity<MeasurementCharacter>()
            .HasOne(ch => ch.Unit)
            .WithMany(unit => unit.MeasurementCharacters)
            .HasForeignKey(ch => ch.UnitRef)
            .HasPrincipalKey(unit => unit.Ref);
    }
}
