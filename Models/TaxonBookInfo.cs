using Microsoft.EntityFrameworkCore;

namespace Hazo.Models;

public class TaxonBookInfo : IHazoModel
{
    public required string TaxonRef { get; set; }
    public required string BookRef { get; set; }
    public int? Fasc { get; set; }
    public int? Page { get; set; }
    public string? Details { get; set; }

    public Taxon? Taxon { get; set; }
    public Book? Book { get; set; }

    public static void ConfigureModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaxonBookInfo>()
            .ToTable("Taxon_Book_Info")
            .HasKey(info => new { info.TaxonRef, info.BookRef });
        modelBuilder.Entity<TaxonBookInfo>()
            .Property(info => info.TaxonRef)
            .HasColumnName("Taxon_Ref");
        modelBuilder.Entity<TaxonBookInfo>()
            .Property(info => info.BookRef)
            .HasColumnName("Book_Ref");
        modelBuilder.Entity<TaxonBookInfo>()
            .HasOne(info => info.Taxon)
            .WithMany(t => t.BookInfo)
            .HasForeignKey(info => info.TaxonRef)
            .HasPrincipalKey(t => t.DocumentRef);
        modelBuilder.Entity<TaxonBookInfo>()
            .HasOne(info => info.Book)
            .WithMany(book => book.TaxonReferences)
            .HasForeignKey(info => info.BookRef)
            .HasPrincipalKey(book => book.DocumentRef);
    }
}
