using DeployTool.Models;
using DeployTool.Services;
using RichardSzalay.MockHttp;

namespace DeployToolTest.Services;
public class GitHubFileServiceTest
{
    [Fact]
    public async Task GetItemsAsync_should_return_git_hub_asset_from_api_url()
    {
        var apiUrl = "http://localhost/contents/";
        var mockHttpHandler = new MockHttpMessageHandler();
        mockHttpHandler.When(apiUrl).RespondJson(new GitHubItem[]
        {
            new GitHubItem()
        });
        var httpClient = mockHttpHandler.ToHttpClient();
        var sut = new GitHubFileService(httpClient, apiUrl);

        var result = await sut.GetItemsAsync()

        mockHttpHandler.Expect(apiUrl);
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetFileStreamAsync_should_return_stream_from_download_url()
    {
        var item = new GitHubItem
        {
            DownloadUrl = $"http://{Guid.NewGuid()}"
        };

        var mockHttpHandler = new MockHttpMessageHandler();
        mockHttpHandler.When(item.DownloadUrl).Respond(req => new HttpResponseMessage
        {
            Content = new StreamContent(new MemoryStream())
        });
        var httpClient = mockHttpHandler.ToHttpClient();
        var sut = new GitHubFileService(httpClient, "http://localhost/contents/");

        var result = await sut.GetFileStreamAsync(item)

        mockHttpHandler.Expect(item.DownloadUrl);
        Assert.NotNull(result);
    }
}
