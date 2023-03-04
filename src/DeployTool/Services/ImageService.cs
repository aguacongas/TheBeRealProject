using DeployTool.Abstraction;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.InteropServices;
using TheBeRealProject.Models;

namespace DeployTool.Services;
public class ImageService : IImageService
{
    public string PublishDir { get; }

    public ImageService(string? publishDir)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            throw new PlatformNotSupportedException();
        }
        PublishDir = publishDir ?? throw new ArgumentNullException(nameof(publishDir));
    }

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Already checked")]
    public void SaveGlimpse(MemoryStream stream, AssetItem asset)
    {
        stream.Position = 0;
        using var glimpse = ResizeImage(stream, 400, true);
        glimpse.Save(Path.Combine(PublishDir, asset.Path));
    }

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Already checked")]
    public void SaveMini(MemoryStream stream, AssetItem asset)
    {
        stream.Position = 0;
        using var mini = ResizeImage(stream, 20, false);
        mini.Save(Path.Combine(PublishDir, asset.MiniPath));
    }

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Already checked")]
    static Image ResizeImage(Stream stream, float size, bool useHeight)
    {
        using var original = new Bitmap(stream);

        var scale = useHeight ? size / original.Height : size / original.Width;

        return new Bitmap(original, (int)(original.Width * scale), (int)(original.Height * scale));
    }
}
