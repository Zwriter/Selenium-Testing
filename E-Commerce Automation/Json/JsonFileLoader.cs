using System.Text.Json;

public static class JsonFileLoader
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static T Load<T>(string fileName)
    {
        string path = Path.Combine(AppContext.BaseDirectory, fileName);

        if (!File.Exists(path))
            throw new FileNotFoundException($"Test data file not found: {path}");

        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<T>(json, Options)
            ?? throw new InvalidOperationException($"Failed to deserialize {fileName}");
    }
}