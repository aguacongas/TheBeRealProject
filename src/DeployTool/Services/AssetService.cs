using DeployTool.Abstraction;
using DeployTool.Models;
using System.Collections.ObjectModel;
using System.Text.Json;
using TheBeRealProject.Models;

namespace DeployTool.Services;
public class AssetService
{
    private readonly IGitHubFileService _gitHubFileServices;
    private readonly IImageService _imageService;

    public AssetService(IGitHubFileService gitHubFileServices, IImageService imageService)
    {
        _gitHubFileServices = gitHubFileServices;
        _imageService = imageService;
    }

    public async Task GetAssetsAsync()
    {
        CleanUpDestinationDirectory();

        var data = await _gitHubFileServices.GetItemsAsync().ConfigureAwait(false) ?? throw new InvalidOperationException("data is null");

        var assets = new Collection<AssetItem>();
        for (int i = 0; i < data.Length; i++)
        {
            var item = data[i];
            var dateString = item.Name?.ToUpperInvariant()
                    .Replace("BEREAL-", string.Empty)
                    .Replace("-1200.JPEG", string.Empty);
            if (!DateOnly.TryParseExact(dateString, "yyyy-MM-dd", out DateOnly date))
            {
                continue;
            }

            await AddAssetAsync(assets, item, date).ConfigureAwait(false);
            Console.Write(".");
        }

        using var stream = File.Create(Path.Combine(_imageService.PublishDir, "assets.json"));
        JsonSerializer.Serialize(stream, assets);
    }

    private async Task AddAssetAsync(Collection<AssetItem> assets, GitHubItem item, DateOnly date)
    {
        var id = Guid.NewGuid();
        var asset = new AssetItem
        {
            Id = id,
            Date = date,
            OriginalUrl = item.DownloadUrl
        };

        using var ms = await _gitHubFileServices.GetFileStreamAsync(item).ConfigureAwait(false);
        _imageService.SaveGlimpse(ms, asset);
        _imageService.SaveMini(ms, asset);
        assets.Add(asset);
    }

    private void CleanUpDestinationDirectory()
    {
        var assetDir = Path.Combine(_imageService.PublishDir, "assets");
        if (!Directory.Exists(assetDir))
        {
            Directory.CreateDirectory(assetDir);
        }
        foreach (var file in Directory.GetFiles(assetDir))
        {
            File.Delete(file);
        }
    }
}
