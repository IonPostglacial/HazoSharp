using Microsoft.EntityFrameworkCore;

namespace Hazo.Models;

public interface IDocumentTable : IHazoModel
{
    public string DocumentRef { get; set; }
    public Document? Document { get; set; }

    public static void Configure<T>(ModelBuilder modelBuilder, string tableName)
        where T : class, IDocumentTable 
    {
        modelBuilder.Entity<T>().ToTable(tableName)
            .HasKey(ch => ch.DocumentRef);
        modelBuilder.Entity<T>().Property(ch => ch.DocumentRef)
            .HasColumnName("Document_Ref");
        modelBuilder.Entity<T>()
            .HasOne(ch => ch.Document)
            .WithOne().IsRequired(false)
            .HasForeignKey<T>(doc => doc.DocumentRef);
    }
}
