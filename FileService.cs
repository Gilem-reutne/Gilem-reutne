using System.Text.Json;

namespace LibraryApp;

public static class FileService
    {
        private const string JsonFileName = "library_data.json";
        private const string NdJsonFileName = "library_data.ndjson";

        public static void SaveData(List<Book> books)
        {
            var json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(JsonFileName, json);

            using var writer = new StreamWriter(NdJsonFileName, false);
            foreach (var b in books)
                writer.WriteLine(JsonSerializer.Serialize(b));
        }

        public static List<Book> LoadData()
        {
            if (!File.Exists(JsonFileName))
                return new List<Book>();

            var raw = File.ReadAllText(JsonFileName);
            return JsonSerializer.Deserialize<List<Book>>(raw) ?? new List<Book>();
        }

        public static void AppendBook(Book book)
        {
            using var writer = new StreamWriter(NdJsonFileName, true);
            writer.WriteLine(JsonSerializer.Serialize(book));
        }

        public static List<Book> LoadChunk(int skip, int take)
        {
            var result = new List<Book>();
            if (!File.Exists(NdJsonFileName)) return result;

            using var reader = new StreamReader(NdJsonFileName);
            int index = 0;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null) break;
                if (index >= skip && result.Count < take)
                {
                    try
                    {
                        var book = JsonSerializer.Deserialize<Book>(line);
                        if (book != null) result.Add(book);
                    }
                    catch {}
                }
                if (result.Count == take) break;
                index++;
            }
            return result;
        }
    }
