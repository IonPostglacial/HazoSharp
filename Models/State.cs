using Microsoft.EntityFrameworkCore;

namespace Hazo.Models;

public class State : IDocumentTable
{
    public required string DocumentRef { get; set; }
    public string? Color { get; set; }

    public Document? Document { get; set; }

    public static void ConfigureModel(ModelBuilder modelBuilder)
    {
        IDocumentTable.Configure<State>(modelBuilder, "State");
    }
}
