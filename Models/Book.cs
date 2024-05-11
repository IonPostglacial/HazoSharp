using Microsoft.EntityFrameworkCore;

namespace Hazo.Models;

public class Book : IDocumentTable
{
    public required string DocumentRef { get; set; }
    public string? ISBN { get; set; }

    public Document? Document { get; set; }
    public List<TaxonBookInfo>? TaxonReferences { get; set; }

    public static void ConfigureModel(ModelBuilder modelBuilder)
    {
        IDocumentTable.Configure<Book>(modelBuilder, "Book");
    }
}
