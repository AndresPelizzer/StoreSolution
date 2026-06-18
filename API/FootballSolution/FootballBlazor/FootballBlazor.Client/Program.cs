using FootballBlazor.Client.Models;
using FootballBlazor.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


var cfg = builder.Configuration;
var cfgSession = cfg.GetSection("Api");
string API_BASEURL = cfgSession.GetValue<string>("BaseUrl")!;


builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(API_BASEURL)
});


builder.Services.AddRadzenComponents();


//builder.Services.AddLocalStorageServices();
//builder.Services.AddSingleton<IStorageService, StorageService>();


//builder.Services.AddCascadingValue(t => new Storage { Token = string.Empty });

//builder.Services.AddSingleton<Storage>();

//builder.Services.AddCascadingValue(sp =>
//    sp.GetRequiredService<Storage>());
builder.Services.AddSingleton<Storage>();
await builder.Build().RunAsync();
