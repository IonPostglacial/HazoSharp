using Microsoft.EntityFrameworkCore;

namespace Hazo.Models;

public class PeriodicCharacter : IDocumentTable
{
    public required string DocumentRef { get; set; }
    public required string PeriodicCategoryRef { get; set; }
    public string? Color { get; set; }

    public Document? Document { get; set; }
    public Document? PeriodicCategory { get; set; }

    public static void ConfigureModel(ModelBuilder modelBuilder)
    {
        IDocumentTable.Configure<PeriodicCharacter>(modelBuilder, "Periodic_Character");

        modelBuilder.Entity<PeriodicCharacter>().Property(ch => ch.PeriodicCategoryRef)
            .HasColumnName("Periodic_Category_Ref");
        modelBuilder.Entity<PeriodicCharacter>()
            .HasOne(ch => ch.PeriodicCategory)
            .WithMany(cat => cat.PeriodicCharacters)
            .HasForeignKey(ch => ch.PeriodicCategoryRef)
            .HasPrincipalKey(cat => cat.Ref);
    }
}
