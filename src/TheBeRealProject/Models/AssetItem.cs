using System.Text.Json.Serialization;

namespace TheBeRealProject.Models;
public class AssetItem
{
    public Guid Id { get; set; }

    [JsonIgnore]
    public string Path => $"assets/{Id}.jpeg";

    [JsonIgnore]
    public string MiniPath => $"assets/{Id}.mini.jpeg";

    public string? OriginalUrl { get; set; }

    public DateOnly Date { get; set; }
}
