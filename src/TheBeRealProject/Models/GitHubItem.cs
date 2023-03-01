
using System.Text.Json.Serialization;

namespace TheBeRealProject.Models;

public class GitHubItem 
{
    public string? Name { get; set; }
    public string? Path { get; set; }
    public string? Sha { get; set; }
    public int? Size { get; set; }
    public string? Url { get; set; }

    [JsonPropertyName("html_url")]
    public string? HtmlUrl { get; set; }
    
    [JsonPropertyName("git_url")]
    public string? GitUrl { get; set; }
    
    [JsonPropertyName("download_url")]
    public string? DownloadUrl { get; set; }
    public string? Type { get; set; }
    
    [JsonPropertyName("_links")]
    public GitHubILink? Links { get; set; }
}
