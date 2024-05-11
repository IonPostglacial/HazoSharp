using Microsoft.EntityFrameworkCore;

namespace Hazo.Models;

public class GeographicalMap : IDocumentTable
{
    public required string DocumentRef { get; set; }
    public required string PlaceRef { get; set; }
    public required string MapFile { get; set; }
    public required string MapFileFeatureName { get; set; }

    public Document? Document { get; set; }
    public GeographicalPlace? Place { get; set; }
    public List<GeographicalCharacter>? Characters { get; set; }

    public static void ConfigureModel(ModelBuilder modelBuilder)
    {
        IDocumentTable.Configure<GeographicalMap>(modelBuilder, "Geographical_Map");
        modelBuilder.Entity<GeographicalMap>()
            .Property(map => map.PlaceRef)
            .HasColumnName("Place_Ref");
        modelBuilder.Entity<GeographicalMap>()
            .Property(map => map.MapFile)
            .HasColumnName("Map_File");
        modelBuilder.Entity<GeographicalMap>()
            .Property(map => map.MapFileFeatureName)
            .HasColumnName("Map_File_Feature_Name");
        modelBuilder.Entity<GeographicalMap>()
            .HasOne(map => map.Place)
            .WithMany(place => place.Maps)
            .HasForeignKey(map => map.PlaceRef)
            .HasPrincipalKey(place => place.DocumentRef);
    }
}
