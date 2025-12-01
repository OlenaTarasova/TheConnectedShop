// using System.Text.Json;
 
 
// public class ConfigData
// {
//     public string VALID_SEARCH_QUERY { get; set; }
//     public string INVALID_SEARCH_QUERY { get; set; }
//     public string EMPTY_SEARCH_QUERY { get; set; }
//     public string SPECIAL_CHARS_SEARCH_QUERY { get; set; }
// }
 
// class Program
// {
//     static async Task Main()
//     {
        
//         var json = await File.ReadAllTextAsync("new.json");
 
        
//         var config = JsonSerializer.Deserialize<ConfigData>(json);
 
//         // Console.WriteLine($"URL: {config.Url}");
//         // Console.WriteLine($"User: {config.Username}");
//     }
// }