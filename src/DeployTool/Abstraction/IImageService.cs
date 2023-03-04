using TheBeRealProject.Models;

namespace DeployTool.Abstraction;
public interface IImageService
{
    string PublishDir { get; }

    void SaveGlimpse(MemoryStream stream, AssetItem asset);
    void SaveMini(MemoryStream stream, AssetItem asset);
}