
using DeployTool.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using TheBeRealProject.Models;

var builder = Host.CreateApplicationBuilder(args);
var configuration = builder.Configuration;
var apiUrl = configuration.GetValue<string>("ApiUrl") ?? throw new InvalidOperationException("Api url is null");
builder.Services.AddScoped(sp => {
    var client = new HttpClient {
        BaseAddress = new Uri(apiUrl),
    };
    client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("TheBeRealProject.DeployTool", "1.0.0"));
    return client;
});

var app = builder.Build();

var publishDir = configuration.GetValue<string>("PublishDir") ?? throw new InvalidOperationException("Publish dir is null");
var assetDir = Path.Combine(publishDir, "assets");
if (!Directory.Exists(assetDir))
{
    Directory.CreateDirectory(assetDir);
}
foreach(var file in Directory.GetFiles(assetDir))
{
    File.Delete(file);
}

var httpClient = app.Services.GetRequiredService<HttpClient>();
var data = await httpClient.GetFromJsonAsync<GitHubItem[]>("contents/assets").ConfigureAwait(false) ?? throw new InvalidOperationException("data is null");

var assetsPath = configuration.GetValue<string>("AssetsPath") ?? throw new InvalidOperationException("Api url is null");
var assets = new Collection<AssetItem>();
for (int i = 0; i < data.Length; i++) 
{
    var item = data[i];
    var dateString = item.Name?.ToUpperInvariant()
            .Replace("BEREAL-", string.Empty)
            .Replace("-1200.JPEG", string.Empty);
    if (!DateOnly.TryParseExact(dateString, "yyyy-MM-dd", out DateOnly date)) {
        continue;
    }

    if (item.Name is null) {
        continue;
    }
    var id = Guid.NewGuid();
    var asset = new AssetItem 
    {
        Id = id,
        Date = date,
        OriginalUrl = item.Url
    };
    var path = Path.Combine(assetsPath, item.Name);
    using var glimpse = ResizeImage(path, 400);
    glimpse.Save(Path.Combine(publishDir, asset.Path));

    using var mini = ResizeImage(path, 20);
    mini.Save(Path.Combine(publishDir, asset.MiniPath));

    assets.Add(asset);
    Console.Write(".");
}

using var stream = File.Create(Path.Combine(publishDir, "assets.json"));
JsonSerializer.Serialize(stream, assets);
static Image ResizeImage(string path, float size)
{ 
    using var stream = File.OpenRead(path);
    using var original = new Bitmap(stream);

    var scaleHeight = size / original.Height;
    var scaleWidth = size / original.Width;

    var scale = Math.Min(scaleHeight, scaleWidth);

    return new Bitmap(original, (int)(original.Width * scale), (int)(original.Height * scale));
}