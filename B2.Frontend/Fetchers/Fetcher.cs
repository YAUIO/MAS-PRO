using System.Net.Http.Json;

namespace B2.Frontend.Fetchers;

public class Fetcher(HttpClient http)
{
    public async Task<T> GetAtPath<T>(string path)
    {
        var res = await http.GetAsync(path);
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<T>();
    }

    public async Task Post(string path, object body)
    {
        var res = await http.PostAsJsonAsync(path, body);
        res.EnsureSuccessStatusCode();
    }
}