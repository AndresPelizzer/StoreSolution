using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StoreBlazor;
using StoreBlazor.Services;
using StoreShared.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7293/")
});



builder.Services.AddScoped<IAreeService, AreeService>();
builder.Services.AddScoped<IDipendentiService, DipendentiService>();
builder.Services.AddScoped<IClientiService,ClientiService>();
builder.Services.AddScoped<IRichiesteService, RichiesteService>();
builder.Services.AddSingleton<AuthState>();
builder.Services.AddScoped<IAuthService, AuthService>();



await builder.Build().RunAsync();






