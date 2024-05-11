using Microsoft.EntityFrameworkCore;

namespace Hazo.Models;

public class Unit : IHazoModel
{
    public required string Ref { get; set; }
    public string? BaseUnitRef { get; set; }
    public double ToBaseUnitFactor { get; set; }

    public Unit? BaseUnit { get; set; }
    public List<Unit>? DerivedUnits { get; set; }
    public List<MeasurementCharacter>? MeasurementCharacters { get; set; }

    public static void ConfigureModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Unit>().ToTable("Unit").HasKey("Ref");
        modelBuilder.Entity<Unit>()
            .Property(unit => unit.BaseUnitRef).HasColumnName("Base_Unit_Ref");
        modelBuilder.Entity<Unit>()
            .Property(unit => unit.ToBaseUnitFactor).HasColumnName("To_Base_Unit_Factor");
        modelBuilder.Entity<Unit>()
                .HasOne(unit => unit.BaseUnit)
                .WithMany(unit => unit.DerivedUnits).IsRequired(false)
                .HasForeignKey(unit => unit.BaseUnitRef);
        modelBuilder.Entity<Unit>().HasData([
            new Unit { Ref = "kg" },
            new Unit { Ref = "g", BaseUnitRef = "kg", ToBaseUnitFactor = 1000 },
            new Unit { Ref = "m" },
            new Unit { Ref = "mm", BaseUnitRef = "m", ToBaseUnitFactor = 1000 },
            new Unit { Ref = "cm", BaseUnitRef = "m", ToBaseUnitFactor = 100 },
            new Unit { Ref = "km", BaseUnitRef = "m", ToBaseUnitFactor = 0.001 },
            new Unit { Ref = "nbr" },
        ]);
    }
}
