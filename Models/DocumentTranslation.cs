using Microsoft.EntityFrameworkCore;

namespace Hazo.Models;

public class DocumentTranslation : IHazoModel
{
    public required string DocumentRef { get; set; }
    public required string LangRef { get; set; }
    public required string Name { get; set; }
    public string Details { get; set; } = "";

    public Document? Document { get; set; }
    public Language? Lang { get; set; }

    public static void ConfigureModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DocumentTranslation>().ToTable("Document_Translation")
            .HasKey(dtr => new { dtr.DocumentRef, dtr.LangRef });
        modelBuilder.Entity<DocumentTranslation>().Property(dtr => dtr.DocumentRef)
            .HasColumnName("Document_Ref");
        modelBuilder.Entity<DocumentTranslation>().Property(dtr => dtr.LangRef)
            .HasColumnName("Lang_Ref");
        modelBuilder.Entity<DocumentTranslation>()
            .HasOne(dtr => dtr.Document)
            .WithMany(doc => doc.Translations)
            .HasForeignKey(dtr => dtr.DocumentRef)
            .HasPrincipalKey(doc => doc.Ref);
        modelBuilder.Entity<DocumentTranslation>()
            .HasOne(dtr => dtr.Lang)
            .WithMany(lang => lang.Translations)
            .HasForeignKey(dtr => dtr.LangRef)
            .HasPrincipalKey(lang => lang.Ref);
    }
}
