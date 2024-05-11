using Microsoft.EntityFrameworkCore;

namespace Hazo.Models;

public class DescriptorVisibilityInapplicable : IHazoModel
{
    public required string DescriptorRef { get; set; }
    public required string InapplicableDescriptorRef { get; set; }

    public Document? Descriptor { get; set; }
    public Document? InapplicableDescriptor { get; set; }

    public static void ConfigureModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DescriptorVisibilityInapplicable>()
            .ToTable("Descriptor_Visibility_Inapplicable")
            .HasKey(dvr => new { dvr.DescriptorRef, dvr.InapplicableDescriptorRef });
        modelBuilder.Entity<DescriptorVisibilityInapplicable>()
            .Property(dvr => dvr.DescriptorRef).HasColumnName("Descriptor_Ref");
        modelBuilder.Entity<DescriptorVisibilityInapplicable>()
            .Property(dvr => dvr.InapplicableDescriptorRef).HasColumnName("Inapplicable_Descriptor_Ref");
    }
}
