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


await builder.Build().RunAsync();
