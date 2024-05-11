using Microsoft.EntityFrameworkCore;

namespace Hazo.Models;

public class Language : IHazoModel
{
    public required string Ref { get; set; }
    public required string Name { get; set; }

    public List<DocumentTranslation>? Translations { get; set; }

    public static void ConfigureModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Language>().ToTable("Lang")
            .HasKey(lang => lang.Ref);

        modelBuilder.Entity<Language>().HasData([
            new Language{ Ref = "V", Name = "Vernacular" },
            new Language{ Ref = "CN", Name = "Chinese" },
            new Language{ Ref = "EN", Name = "English" },
            new Language{ Ref = "FR", Name = "French" },
            new Language{ Ref = "S2", Name = "Name 2" },
            new Language{ Ref = "V2", Name = "Vernacular Name 2" },
        ]);
    }
}
