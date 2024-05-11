using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hazo.Models;

public class HazoContext : DbContext
{
    public DbSet<Document> Documents { get; set; }
    public DbSet<DescriptorVisibilityInapplicable> VisibilityInapplicables { get; set; }
    public DbSet<DescriptorVisibilityRequirement> VisibilityRequirements { get; set; }
    public DbSet<DocumentTranslation> Translations { get; set; }
    public DbSet<DocumentAttachment> Attachments { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Taxon> Taxons { get; set; }
    public DbSet<TaxonBookInfo> BookInfo { get; set; }
    public DbSet<TaxonMeasurement> Measurements { get; set; }
    public DbSet<TaxonDescription> TaxonDescriptions { get; set; }
    public DbSet<TaxonSpecimenLocation> SpecimenLocations { get; set; }
    public DbSet<MeasurementCharacter> MeasurementCharacters { get; set; }
    public DbSet<CategoricalCharacter> CategoricalCharacters { get; set; }
    public DbSet<PeriodicCharacter> PeriodicCharacters { get; set; }
    public DbSet<GeographicalCharacter> GeographicalCharacters { get; set; }
    public DbSet<GeographicalPlace> Places { get; set; }
    public DbSet<GeographicalMap> Maps { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<State> States { get; set; }

    public string DbPath { get; }

    public HazoContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, $"dataset.db");
    }

    public static readonly LoggerFactory _myLoggerFactory = new ([ 
        new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
    ]);

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath}").UseLoggerFactory(_myLoggerFactory);
    }

    public static void ConfigureModel<T>(ModelBuilder modelBuilder)
        where T : IHazoModel
    {
        T.ConfigureModel(modelBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureModel<Document>(modelBuilder);
        ConfigureModel<Language>(modelBuilder);
        ConfigureModel<DocumentTranslation>(modelBuilder);
        ConfigureModel<CategoricalCharacter>(modelBuilder);
        ConfigureModel<State>(modelBuilder);
        ConfigureModel<PeriodicCharacter>(modelBuilder);
        ConfigureModel<GeographicalPlace>(modelBuilder);
        ConfigureModel<GeographicalMap>(modelBuilder);
        ConfigureModel<GeographicalCharacter>(modelBuilder);
        ConfigureModel<Unit>(modelBuilder);
        ConfigureModel<MeasurementCharacter>(modelBuilder);
        ConfigureModel<DocumentAttachment>(modelBuilder);
        ConfigureModel<Book>(modelBuilder);
        ConfigureModel<DescriptorVisibilityRequirement>(modelBuilder);
        ConfigureModel<DescriptorVisibilityInapplicable>(modelBuilder);
        ConfigureModel<Taxon>(modelBuilder);
        ConfigureModel<TaxonMeasurement>(modelBuilder);
        ConfigureModel<TaxonDescription>(modelBuilder);
        ConfigureModel<TaxonBookInfo>(modelBuilder);
        ConfigureModel<TaxonSpecimenLocation>(modelBuilder);
    }
}