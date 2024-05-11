namespace Hazo.FileFormats.HazoJson;

public class EncodedPhoto
{
    public required string Id { get; set; }
    public required string Url { get; set; }
    public string? HubUrl { get; set; }
    public required string Label { get; set; }
}

public class EncodedState
{
    public required string Id { get; set; }
    public string? Name { get; set; }
    public string[]? Path { get; set; }
    public required string NameEN { get; set; }
    public required string NameCN { get; set; }
    public required List<EncodedPhoto> Photos { get; set; }
    public required string Description { get; set; }
    public string? Color { get; set; }
}

public class EncodedBook
{
    public required string Id { get; set; }
    public required string Label { get; set; }
    public required string[]? Path { get; set; }
}

public class EncodedBookInfo
{
    public string? Fasc { get; set; }
    public string? Page { get; set; }
    public required string Detail { get; set; }
}

public class EncodedDescriptions
{
    public required string DescriptorId { get; set; }
    public required List<string> StatesIds { get; set; }
}

public class EncodedLocation
{
    public required double? Lat { get; set; }
    public required double? Lng { get; set; }
}

public class EncodedMesurement
{
    public required string Character { get; set; }
    public double? Min { get; set; }
    public double? Max { get; set; }
}

public class EncodedTaxon
{
    public required string Id { get; set; }
    public required string ParentId { get; set; }
    public string[]? Path { get; set; }
    public required string Name { get; set; }
    public required string NameEN { get; set; }
    public required string NameCN { get; set; }
    public required string VernacularName { get; set; }
    public required string Detail { get; set; }
    public required List<string> Children { get; set; }
    public required List<EncodedPhoto> Photos { get; set; }
    public required List<EncodedDescriptions> Descriptions { get; set; }
    public required List<EncodedMesurement> Measurements { get; set; }
    public required List<EncodedLocation> SpecimenLocations { get; set; }
    public required string Author { get; set; }
    public required string VernacularName2 { get; set; }
    public required string Name2 { get; set; }
    public required string Meaning { get; set; }
    public required string HerbariumPicture { get; set; }
    public required string Website { get; set; }
    public string? NoHerbier { get; set; }
    public required string Fasc { get; set; }
    public required string Page { get; set; }
    public required Dictionary<string, EncodedBookInfo> BookInfoByIds { get; set; }
    public required Dictionary<string, object> Extra { get; set; }
}

public class EncodedCharacter
{
    public required string Id { get; set; }
    public required string ParentId { get; set; }
    public string[]? Path { get; set; }
    public required string Name { get; set; }
    public required string NameEN { get; set; }
    public required string NameCN { get; set; }
    public required string VernacularName { get; set; }
    public required string Detail { get; set; }
    public required List<string> Children { get; set; }
    public required List<EncodedPhoto> Photos { get; set; }
    public string? Color { get; set; }
    public string? Preset { get; set; }
    public required string CharacterType { get; set; }
    public required List<string> States { get; set; }
    public string? InherentStateId { get; set; }
    public required List<string> InapplicableStatesIds { get; set; }
    public required List<string> RequiredStatesIds { get; set; }
    public string? MapFile { get; set; }
    public int? Min { get; set; }
    public int? Max { get; set; }
    public string? Unit { get; set; }
}

public class EncodedDataset
{
    public required string Id { get; set; }
    public required List<EncodedTaxon> Taxons { get; set; }
    public required List<EncodedCharacter> Characters { get; set; }
    public required List<EncodedState> States { get; set; }
    public required List<EncodedBook> Books { get; set; }
}