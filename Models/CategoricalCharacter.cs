using Microsoft.EntityFrameworkCore;

namespace Hazo.Models;

public class CategoricalCharacter : IDocumentTable
{
    public required string DocumentRef { get; set; }
    public string? Color { get; set; }

    public Document? Document { get; set; }

    public static void ConfigureModel(ModelBuilder modelBuilder)
    {
        IDocumentTable.Configure<CategoricalCharacter>(modelBuilder, "Categorical_Character");
    }
}
