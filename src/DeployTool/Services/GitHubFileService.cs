using DeployTool.Abstraction;
using DeployTool.Models;
using System.Net.Http.Json;

namespace DeployTool.Services;
public class GitHubFileService : IGitHubFileService
{
    private readonly HttpClient _client;
    private readonly string _fileUrl;

    public GitHubFileService(HttpClient client, string? fileUrl)
    {
        _client = client;
        _fileUrl = fileUrl ?? throw new ArgumentNullException(nameof(fileUrl));
    }

    public Task<GitHubItem[]?> GetItemsAsync()
    => _client.GetFromJsonAsync<GitHubItem[]>(_fileUrl);

    public async Task<MemoryStream> GetFileStreamAsync(GitHubItem item)
    {
        using var gs = await _client.GetStreamAsync(item.DownloadUrl).ConfigureAwait(false);
        var ms = new MemoryStream();
        await gs.CopyToAsync(ms).ConfigureAwait(false);

        return ms;
    }
}
