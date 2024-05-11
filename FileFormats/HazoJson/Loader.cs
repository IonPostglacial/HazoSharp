using System.Text.Json;

namespace Hazo.FileFormats.HazoJson;

class Loader
{
    private static readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

    public static EncodedDataset? TryLoadDataset(ReadOnlySpan<byte> content)
    {
        try {
            return JsonSerializer.Deserialize<EncodedDataset>(content, options);
        } catch  {
            return null;
        }
    }
}