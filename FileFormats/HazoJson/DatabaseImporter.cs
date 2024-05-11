using System.Globalization;
using Hazo.Models;

namespace Hazo.FileFormats.HazoJson;

public static class DatabaseImporter
{
    private class DescriptorVisibilityConstraints
    {
        public List<DescriptorVisibilityInapplicable> VisibilityInapplicables = [];
        public List<DescriptorVisibilityRequirement> VisibilityRequirements = [];
    }

    private static async Task ImportBooks(EncodedDataset dataset, HazoContext db)
    {
        int order = 0;
        foreach (var book in dataset.Books)
        {
            order++;
            db.Documents.Add(new Document { Ref = book.Id, Path = string.Join(".", book.Path ?? []), Order = order, Name = book.Label });
            db.Books.Add(new Book { DocumentRef = book.Id });
        }
        await db.SaveChangesAsync();
    }

    private static async Task<DescriptorVisibilityConstraints> ImportCharacters(EncodedDataset dataset, HazoContext db)
    {
        int order = 0;
        Dictionary<string, string> mapsRefByFileName = db.Maps.ToDictionary(m => m.MapFile, m => m.DocumentRef);
        DescriptorVisibilityConstraints constaints = new();
        foreach (var character in dataset.Characters)
        {
            order++;
            db.Documents.Add(new Document { Ref = character.Id, Path = string.Join(".", character.Path ?? []), Order = order, Name = character.Name, Details = character.Detail });
            if (character.NameCN != null)
            {
                db.Translations.Add(new DocumentTranslation { DocumentRef = character.Id, LangRef = "CN", Name = character.NameCN, Details = "" });
            }
            if (character.NameEN != null)
            {
                db.Translations.Add(new DocumentTranslation { DocumentRef = character.Id, LangRef = "EN", Name = character.NameEN, Details = "" });
            }
            int index = 0;
            foreach (var pic in character.Photos)
            {
                order++;
                db.Attachments.Add(new DocumentAttachment { DocumentRef = character.Id, Index = index, Source = pic.Url, Path = pic.HubUrl });
                index++;
            }
            if (character.CharacterType == "discrete")
            {
                switch (character.Preset)
                {
                    case "flowering":
                        db.PeriodicCharacters.Add(new PeriodicCharacter { DocumentRef = character.Id, PeriodicCategoryRef = Document.CALENDAR_ID, Color = character.Color });
                        break;
                    case "map":
                        if (character.MapFile != null)
                        {
                            if (mapsRefByFileName.TryGetValue(character.MapFile, out string? mapRef))
                            {
                                db.GeographicalCharacters.Add(new GeographicalCharacter { DocumentRef = character.Id, MapRef = mapRef, Color = character.Color });
                            }
                        }
                        break;
                    default:
                        db.CategoricalCharacters.Add(new CategoricalCharacter { DocumentRef = character.Id, Color = character.Color });
                        break;
                }
            }
            if (character.CharacterType == "range")
            {
                db.MeasurementCharacters.Add(new MeasurementCharacter { DocumentRef = character.Id, Color = character.Color, UnitRef = character.Unit });
            }
            foreach (string stateId in character.InapplicableStatesIds)
            {
                constaints.VisibilityInapplicables.Add(new DescriptorVisibilityInapplicable
                {
                    DescriptorRef = character.Id,
                    InapplicableDescriptorRef = stateId
                });
            }
            foreach (string stateId in character.RequiredStatesIds)
            {
                constaints.VisibilityRequirements.Add(new DescriptorVisibilityRequirement
                {
                    DescriptorRef = character.Id,
                    RequiredDescriptorRef = stateId
                });
            }
        }
        await db.SaveChangesAsync();
        return constaints;
    }

    private static async Task ImportStates(EncodedDataset dataset, HazoContext db)
    {
        int order = 0;
        foreach (var state in dataset.States)
        {
            order++;
            db.Documents.Add(new Document { Ref = state.Id, Path = string.Join(".", state.Path ?? []), Order = order, Name = state.Name ?? "", Details = state.Description });
            if (state.NameCN != null)
            {
                db.Translations.Add(new DocumentTranslation { DocumentRef = state.Id, LangRef = "CN", Name = state.NameCN });
            }
            if (state.NameEN != null)
            {
                db.Translations.Add(new DocumentTranslation { DocumentRef = state.Id, LangRef = "EN", Name = state.NameEN });
            }
            db.States.Add(new State { DocumentRef = state.Id, Color = state.Color });
            int index = 0;
            foreach (var pic in state.Photos)
            {
                order++;
                db.Attachments.Add(new DocumentAttachment { DocumentRef = state.Id, Index = index, Source = pic.Url, Path = pic.HubUrl });
                index++;
            }
        }
        await db.SaveChangesAsync();
    }

    private static async Task ImportTaxons(EncodedDataset dataset, HazoContext db)
    {
        int order = 0;
        foreach (var taxon in dataset.Taxons)
        {
            order++;
            db.Documents.Add(new Document { Ref = taxon.Id, Path = string.Join(".", taxon.Path ?? []), Order = order, Name = taxon.Name, Details = taxon.Detail });
            if (taxon.VernacularName != null)
            {
                db.Translations.Add(new DocumentTranslation { DocumentRef = taxon.Id, LangRef = "V", Name = taxon.VernacularName });
            }
            if (taxon.NameCN != null)
            {
                db.Translations.Add(new DocumentTranslation { DocumentRef = taxon.Id, LangRef = "CN", Name = taxon.NameCN });
            }
            if (taxon.NameEN != null)
            {
                db.Translations.Add(new DocumentTranslation { DocumentRef = taxon.Id, LangRef = "EN", Name = taxon.NameEN });
            }
            if (taxon.Name2 != null)
            {
                db.Translations.Add(new DocumentTranslation { DocumentRef = taxon.Id, LangRef = "S2", Name = taxon.Name2 });
            }
            if (taxon.VernacularName2 != null)
            {
                db.Translations.Add(new DocumentTranslation { DocumentRef = taxon.Id, LangRef = "V2", Name = taxon.VernacularName2 });
            }
            db.Taxons.Add(new Taxon
            {
                DocumentRef = taxon.Id,
                Author = taxon.Author,
                Website = taxon.Website,
                Meaning = taxon.Meaning,
                HerbariumNumber = taxon.NoHerbier,
                HerbariumPicture = taxon.HerbariumPicture,
                Fasc = ParseInt(taxon.Fasc),
                Page = ParseInt(taxon.Page)
            });
            for (int index = 0; index < taxon.Photos.Count; index++)
            {
                order++;
                var pic = taxon.Photos[index];
                if (pic.Url != null)
                {
                    db.Attachments.Add(new DocumentAttachment { DocumentRef = taxon.Id, Index = index, Source = pic.Url, Path = pic.HubUrl });
                }
            }
            foreach (var bookRef in taxon.BookInfoByIds?.Keys ?? Enumerable.Empty<string>())
            {
                if (taxon.BookInfoByIds is null)
                {
                    continue;
                }
                taxon.BookInfoByIds.TryGetValue(bookRef, out EncodedBookInfo? bookInfo);
                db.BookInfo.Add(new TaxonBookInfo
                {
                    TaxonRef = taxon.Id,
                    BookRef = bookRef,
                    Fasc = ParseInt(bookInfo?.Fasc),
                    Page = ParseInt(bookInfo?.Page),
                    Details = bookInfo?.Detail
                });
            }
            foreach (var measurement in taxon.Measurements)
            {
                db.Measurements.Add(new TaxonMeasurement
                {
                    TaxonRef = taxon.Id,
                    CharacterRef = measurement.Character,
                    Minimum = measurement.Min,
                    Maximum = measurement.Max
                });
            }
            foreach (var description in taxon.Descriptions)
            {
                foreach (var stateId in description.StatesIds)
                {
                    db.TaxonDescriptions.Add(new TaxonDescription { TaxonRef = taxon.Id, DescriptionRef = stateId });
                }
            }
            if (taxon.SpecimenLocations != null)
            {
                for (int index = 0; index < taxon.SpecimenLocations.Count; index++)
                {
                    var location = taxon.SpecimenLocations[index];
                    db.SpecimenLocations.Add(new TaxonSpecimenLocation
                    {
                        TaxonRef = taxon.Id,
                        Index = index,
                        Latitude = location.Lat,
                        Longitude = location.Lng
                    });
                }
            }
        }
        await db.SaveChangesAsync();
    }

    private static async Task ImportVisibilityConstraints(EncodedDataset dataset, HazoContext db, DescriptorVisibilityConstraints constraints)
    {
        foreach (var constaint in constraints.VisibilityInapplicables)
        {
            db.VisibilityInapplicables.Add(constaint);
        }
        foreach (var constaint in constraints.VisibilityRequirements)
        {
            db.VisibilityRequirements.Add(constaint);
        }
        await db.SaveChangesAsync();
    }

    public static async Task ImportToDatabase(EncodedDataset dataset, HazoContext db)
    {
        await ImportBooks(dataset, db);
        DescriptorVisibilityConstraints constraints = await ImportCharacters(dataset, db);
        await ImportStates(dataset, db);
        await ImportTaxons(dataset, db);
        await ImportVisibilityConstraints(dataset, db, constraints);
    }

    private static int? ParseInt(string? s)
    {
        if (s is null)
        {
            return null;
        }
        try
        {
            return int.Parse(s, NumberStyles.Integer, CultureInfo.InvariantCulture);
        }
        catch
        {
            return null;
        }
    }
}