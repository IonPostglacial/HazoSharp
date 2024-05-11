using Microsoft.EntityFrameworkCore;

namespace Hazo.Models;

public class Document : IHazoModel
{
    public const string CALENDAR_ID = "_cal";

    public required string Ref { get; set; }
    public string Path { get; set; } = "";
    public int Order { get; set; }
    public required string Name { get; set; }
    public string Details { get; set; } = "";

    public List<DocumentTranslation>? Translations { get; set; }
    public List<DocumentAttachment>? Attachments { get; set; }
    public List<PeriodicCharacter>? PeriodicCharacters { get; set; }
    
    public List<Document>? DependentDescriptors { get; set; }
    public List<Document>? RequiredDescriptors { get; set; }

    public List<Document>? HideDescriptors { get; set; }
    public List<Document>? IncompatibleDescriptors { get; set; }

    public List<Taxon>? DescribedTaxons { get; set; }

    public static void ConfigureModel(ModelBuilder modelBuilder)
    {
                modelBuilder.Entity<Document>().ToTable("Document")
            .HasKey(doc => doc.Ref);
        modelBuilder.Entity<Document>().Property(doc => doc.Order)
            .HasColumnName("Doc_Order");
        modelBuilder.Entity<Document>()
            .HasMany(doc => doc.DependentDescriptors)
            .WithMany(doc => doc.RequiredDescriptors)
            .UsingEntity<DescriptorVisibilityRequirement>(
                left => left.HasOne(requirement => requirement.Descriptor)
                    .WithMany()
                    .HasForeignKey(requirement => requirement.DescriptorRef),
                right => right.HasOne(requirement => requirement.RequiredDescriptor)
                    .WithMany()
                    .HasForeignKey(requirement => requirement.RequiredDescriptorRef)
            );
        modelBuilder.Entity<Document>()
            .HasMany(doc => doc.HideDescriptors)
            .WithMany(doc => doc.IncompatibleDescriptors)
            .UsingEntity<DescriptorVisibilityInapplicable>(
                left => left.HasOne(requirement => requirement.Descriptor)
                    .WithMany()
                    .HasForeignKey(requirement => requirement.DescriptorRef),
                right => right.HasOne(requirement => requirement.InapplicableDescriptor)
                    .WithMany()
                    .HasForeignKey(requirement => requirement.InapplicableDescriptorRef)
            );

        modelBuilder.Entity<Document>().HasData([
            new Document { Ref = CALENDAR_ID, Path = "", Order = 0, Name = "Calendar", Details = "Gregorian calendar" },
            new Document { Ref = GeographicalPlace.ROOT_PLACE_ID, Path = "", Order = 1, Name = "Geographical Places", Details = "All geographical places" },
            new Document { Ref = GeographicalPlace.MADA_PLACE_ID, Path = GeographicalPlace.ROOT_PLACE_ID, Order = 2, Name = "Madagascar", Details = "The island of Madagascar" },
        ]);
    }
}
