using System.Text.Json;

namespace LibraryApp;

public static class FileService
{
    private const string JsonFileName = "library.json";

    public static void Save(IEnumerable<Book> books)
    {
        var json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(JsonFileName, json);
    }

    public static List<Book> Load()
    {
        if (!File.Exists(JsonFileName))
            return new List<Book>();

        var raw = File.ReadAllText(JsonFileName);
        return JsonSerializer.Deserialize<List<Book>>(raw) ?? new List<Book>();
    }
}
