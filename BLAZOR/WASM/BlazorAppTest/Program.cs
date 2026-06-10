using BlazorAppTest;
using BlazorAppTest.Interfaces;
using BlazorAppTest.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });




builder.Services.AddSingleton<CalcoliService>();


builder.Services.AddTransient<CalcolatriceService>();
builder.Services.AddTransient<IMiaCalcolatrice, MiaCalcolatriceService>();



builder.Services.AddScoped<IGiocatori, GiocatoriService>();



await builder.Build().RunAsync();

