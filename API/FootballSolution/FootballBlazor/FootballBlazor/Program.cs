using FootballBlazor.Client.Models;
using FootballBlazor.Client.Pages;
using FootballBlazor.Client.Services;
using FootballBlazor.Components;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddHttpClient();


builder.Services.AddRadzenComponents();

var cfg = builder.Configuration;
var cfgSession = cfg.GetSection("Api");
string API_BASEURL = cfgSession.GetValue<string>("BaseUrl")!;



builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(API_BASEURL)
});


//builder.Services.AddSingleton<IStorageService, StorageService>();
//builder.Services.AddCascadingValue(t => new Storage { Token = string.Empty });
//builder.Services.AddSingleton<Storage>();

//builder.Services.AddCascadingValue(sp =>
//    sp.GetRequiredService<Storage>());

builder.Services.AddSingleton<Storage>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(FootballBlazor.Client._Imports).Assembly);

app.Run();
