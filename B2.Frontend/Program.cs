using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using B2.Frontend;
using B2.Frontend.Fetchers;
using B2.Frontend.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var apiUrl = builder.Configuration["apiUrl"] ?? throw new InvalidOperationException("ApiUrl is not configured.");
builder.Services.AddHttpClient<Fetcher>("api", client => client.BaseAddress = new Uri(apiUrl));
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ILocationFetcher, LocationFetcher>();
builder.Services.AddScoped<IOrderFetcher, OrderFetcher>();
builder.Services.AddScoped<IProductFetcher, ProductFetcher>();
builder.Services.AddScoped<Fetcher>();
builder.Services.AddScoped<StatusService>();

await builder.Build().RunAsync();