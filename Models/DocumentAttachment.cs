using Microsoft.EntityFrameworkCore;

namespace Hazo.Models;

public class DocumentAttachment : IHazoModel
{
    public required string DocumentRef { get; set; }
    public required int Index { get; set; }
    public required string Source { get; set; }
    public string? Path { get; set; }

    public Document? Document { get; set; }

    public static void ConfigureModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DocumentAttachment>().ToTable("Document_Attachment")
            .HasKey(attachment => new { attachment.DocumentRef, attachment.Index });
        modelBuilder.Entity<DocumentAttachment>()
            .HasOne(attachment => attachment.Document)
            .WithMany(doc => doc.Attachments)
            .HasForeignKey(attachment => attachment.DocumentRef)
            .HasPrincipalKey(doc => doc.Ref);
        modelBuilder.Entity<DocumentAttachment>().Property(att => att.DocumentRef)
            .HasColumnName("Document_Ref");
        modelBuilder.Entity<DocumentAttachment>().Property(att => att.Index)
            .HasColumnName("Attachment_Index");
    }
}
