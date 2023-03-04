using DeployTool.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting;
public static class HostExtensions
{
    public static Task UseDeployToolAsync(this IHost app)
    {
        var assetService = app.Services.GetRequiredService<AssetService>();
        return assetService.GetAssetsAsync();
    }
}
