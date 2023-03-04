using DeployTool.Models;

namespace DeployTool.Abstraction;
public interface IGitHubFileService
{
    Task<MemoryStream> GetFileStreamAsync(GitHubItem item);
    Task<GitHubItem[]?> GetItemsAsync();
}