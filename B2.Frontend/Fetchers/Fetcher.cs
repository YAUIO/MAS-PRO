using System.Net.Http.Json;

namespace B2.Frontend.Fetchers;

public class Fetcher(IHttpClientFactory http)
{
    private readonly HttpClient _http = http.CreateClient("api");
    
    public async Task<T> GetAtPath<T>(string path)
    {
        var res = await _http.GetAsync(path);
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<T>();
    }

    public async Task Post(string path, object body)
    {
        var res = await _http.PostAsJsonAsync(path, body);
        res.EnsureSuccessStatusCode();
    }
}