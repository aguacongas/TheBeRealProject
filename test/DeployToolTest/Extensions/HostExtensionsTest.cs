using DeployTool.Abstraction;
using DeployTool.Models;
using DeployTool.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TheBeRealProject.Models;

namespace DeployToolTest.Extensions;
public class HostExtensionsTest
{
    [Fact]
    public async Task UseDeployToolAsync_should_use_asset_service()
    {
        var gitHubFileServiceMock = new Mock<IGitHubFileService>();
        gitHubFileServiceMock.Setup(m => m.GetItemsAsync()).ReturnsAsync(Array.Empty<GitHubItem>());
        var imageServiceMock = new Mock<IImageService>();
        var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        imageServiceMock.SetupGet(m => m.PublishDir).Returns(path);

        var builder = Host.CreateApplicationBuilder();
        builder.Services.AddTransient(p => gitHubFileServiceMock.Object)
            .AddTransient(p => imageServiceMock.Object)
            .AddTransient<AssetService>();

        await builder.Build().UseDeployToolAsync()

        var assets = JsonSerializer.Deserialize<AssetItem[]>(File.ReadAllText(Path.Combine(path, "assets.json")));

        Assert.NotNull(assets);
        Assert.Empty(assets);
    }
}
