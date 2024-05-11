using Microsoft.EntityFrameworkCore;

namespace Hazo.Models;

public class GeographicalPlace : IDocumentTable
{
    public const string ROOT_PLACE_ID = "_geo";
    public const string MADA_PLACE_ID = "_geo_mada";

    public required string DocumentRef { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int Scale { get; set; }

    public Document? Document { get; set; }
    public List<GeographicalMap>? Maps { get; set; }

    public static void ConfigureModel(ModelBuilder modelBuilder)
    {
        IDocumentTable.Configure<GeographicalPlace>(modelBuilder, "Geographical_Place");

        modelBuilder.Entity<GeographicalPlace>().HasData([
            new GeographicalPlace { DocumentRef = GeographicalPlace.MADA_PLACE_ID, Longitude = -18.546564, Latitude = 46.518367, Scale = 2000 }
        ]);
    }
}
