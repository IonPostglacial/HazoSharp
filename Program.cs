using Hazo.Models;
using System.CommandLine;
using Hazo.FileFormats.HazoJson;

class Program
{
    public static async Task<int> Main(string[] args)
    {
        using var db = new HazoContext();

        var inputOption = new Option<FileInfo?>(
            name: "--input",
            description: "The file to read and import in the database.");

        var rootCommand = new RootCommand("Sample app for System.CommandLine");
        rootCommand.AddOption(inputOption);

        rootCommand.SetHandler(async (file) => 
            {
                if (file is not null)
                {
                    await ImportFile(file, db); 
                }
            },
            inputOption);

        // Note: This sample requires the database to be created before running.
        Console.WriteLine($"Database path: {db.DbPath}.");
        
        return await rootCommand.InvokeAsync(args);
    }

    static async Task ImportFile(FileInfo file, HazoContext db)
    {
        byte[] content = await File.ReadAllBytesAsync(file.FullName);
        var ds =  Loader.TryLoadDataset(content);

        if (ds is not null)
        {
            await DatabaseImporter.ImportToDatabase(ds, db);
        }
    }
}
