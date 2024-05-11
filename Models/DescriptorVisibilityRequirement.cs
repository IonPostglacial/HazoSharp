using Microsoft.EntityFrameworkCore;

namespace Hazo.Models;

public class DescriptorVisibilityRequirement : IHazoModel
{
    public required string DescriptorRef { get; set; }
    public required string RequiredDescriptorRef { get; set; }

    public Document? Descriptor { get; set; }
    public Document? RequiredDescriptor { get; set; }

    public static void ConfigureModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DescriptorVisibilityRequirement>()
            .ToTable("Descriptor_Visibility_Requirement")
            .HasKey(dvr => new { dvr.DescriptorRef, dvr.RequiredDescriptorRef });
        modelBuilder.Entity<DescriptorVisibilityRequirement>()
            .Property(dvr => dvr.DescriptorRef).HasColumnName("Descriptor_Ref");
        modelBuilder.Entity<DescriptorVisibilityRequirement>()
            .Property(dvr => dvr.RequiredDescriptorRef).HasColumnName("Required_Descriptor_Ref");
    }
}
