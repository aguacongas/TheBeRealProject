using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TheBeRealProject;
using TheBeRealProject.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiUrl = builder.Configuration.GetValue<string>("ApiUrl") ?? throw new InvalidOperationException("Api url is null");
builder.Services.AddScoped(sp =>  new HttpClient 
    {
        BaseAddress = new Uri(apiUrl),
    })
    .AddScoped<ThemeService>();

await builder.Build().RunAsync();
