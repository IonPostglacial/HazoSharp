using Microsoft.EntityFrameworkCore;

namespace Hazo.Models;

public class GeographicalCharacter : IDocumentTable
{
    public required string DocumentRef { get; set; }
    public required string MapRef { get; set; }
    public string? Color { get; set; }

    public Document? Document { get; set; }
    public GeographicalMap? Map { get; set; }

    public static void ConfigureModel(ModelBuilder modelBuilder)
    {
        IDocumentTable.Configure<GeographicalCharacter>(modelBuilder, "Geographical_Character");

        modelBuilder.Entity<GeographicalCharacter>()
            .Property(ch => ch.MapRef)
            .HasColumnName("Map_Ref");
        modelBuilder.Entity<GeographicalCharacter>()
            .HasOne(ch => ch.Map)
            .WithMany(map => map.Characters)
            .HasForeignKey(ch => ch.MapRef)
            .HasPrincipalKey(map => map.DocumentRef);
    }
}
