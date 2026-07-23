using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using StoreBlazor;
using StoreBlazor.Services;
using StoreShared.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var configuration = builder.Configuration;
var section = configuration.GetSection("Api");


// string BASE_URL = "https://localhost:7293/";
string BASE_URL = section.GetValue<string>("BaseUrl") ?? "";




builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(BASE_URL)
});


builder.Services.AddScoped<IUtentiService, UtentiService>();


builder.Services.AddScoped<IAreeService, AreeService>();
builder.Services.AddScoped<IDipendentiService, DipendentiService>();
builder.Services.AddScoped<IClientiService, ClientiService>();
builder.Services.AddScoped<IRichiesteService, RichiesteService>();
builder.Services.AddSingleton<AuthState>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<AuthResetService>();
builder.Services.AddScoped<NotificheService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<IRichiesteFerieService, RichiestaFerieService>();
builder.Services.AddScoped<IStraordinarieService, StraordinarieService>();

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();



await builder.Build().RunAsync();






