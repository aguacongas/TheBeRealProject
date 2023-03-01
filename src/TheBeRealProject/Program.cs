using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Headers;
using TheBeRealProject;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiUrl = builder.Configuration.GetValue<string>("ApiUrl") ?? throw new InvalidOperationException("Api url is null");
builder.Services.AddScoped(sp =>  new HttpClient 
    {
        BaseAddress = new Uri(apiUrl),
    });

await builder.Build().RunAsync();
