using DeployTool.Abstraction;
using DeployTool.Models;
using DeployTool.Services;
using Moq;
using System.Text.Json;
using TheBeRealProject.Models;

namespace DeployToolTest.Services;
public class AssetServiceTest
{
    [Fact]
    public async Task GetAssetsAsync_should_verify_item_date()
    {
        var gitHubFileServiceMock = new Mock<IGitHubFileService>();
        gitHubFileServiceMock.Setup(m => m.GetItemsAsync()).ReturnsAsync(new GitHubItem[]
        {
            new GitHubItem()
        });
        var imageServiceMock = new Mock<IImageService>();
        imageServiceMock.SetupGet(m => m.PublishDir).Returns(Path.GetTempPath());

        var sut = new AssetService(gitHubFileServiceMock.Object, imageServiceMock.Object);

        await sut.GetAssetsAsync().ConfigureAwait(false);

        var assets = JsonSerializer.Deserialize<AssetItem[]>(File.ReadAllText(Path.Combine(Path.GetTempPath(), "assets.json")));

        Assert.NotNull(assets);
        Assert.Empty(assets);
    }

    [Fact]
    public async Task GetAssetsAsync_should_get_date_from_item_name()
    {
        var gitHubFileServiceMock = new Mock<IGitHubFileService>();
        gitHubFileServiceMock.Setup(m => m.GetItemsAsync()).ReturnsAsync(new GitHubItem[]
        {
            new GitHubItem
            {
                Name = "bereal-2022-10-12-1200.JPEG"
            }
        });

        var imageServiceMock = new Mock<IImageService>();
        imageServiceMock.SetupGet(m => m.PublishDir).Returns(Path.GetTempPath());

        var sut = new AssetService(gitHubFileServiceMock.Object, imageServiceMock.Object);

        await sut.GetAssetsAsync().ConfigureAwait(false);

        var assets = JsonSerializer.Deserialize<AssetItem[]>(File.ReadAllText(Path.Combine(Path.GetTempPath(), "assets.json")));

        Assert.NotNull(assets);
        Assert.Single(assets);
    }
}
