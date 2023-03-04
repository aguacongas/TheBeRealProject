using DeployTool.Services;
using System.Drawing;
using System.Runtime.InteropServices;
using TheBeRealProject.Models;

namespace DeployToolTest.Services;
public class ImageServiceTest
{
    [Fact]
    public void SaveGlimpse_should_resize_image()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return;
        }

        var dir = Path.GetTempPath();
        if (!Directory.Exists(Path.Combine(dir, "assets")))
        {
            Directory.CreateDirectory(Path.Combine(dir, "assets"));
        }

        var sut = new ImageService(dir);

        using var fs = File.OpenRead("test.jpeg");
        using var ms = new MemoryStream();
        fs.CopyTo(ms);
        ms.Position = 0;
        var asset = new AssetItem
        {
            Id = Guid.NewGuid()
        };
        sut.SaveGlimpse(ms, asset);

        var path = Path.Combine(dir, asset.Path);
        Assert.True(File.Exists(path));
        using var bitmap = new Bitmap(path);
        Assert.Equal(400, bitmap.Height);
    }

    [Fact]
    public void SaveMini_should_resize_image()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return;
        }

        var dir = Path.GetTempPath();
        if (!Directory.Exists(Path.Combine(dir, "assets")))
        {
            Directory.CreateDirectory(Path.Combine(dir, "assets"));
        }

        var sut = new ImageService(dir);

        using var fs = File.OpenRead("test.jpeg");
        using var ms = new MemoryStream();
        fs.CopyTo(ms);
        ms.Position = 0;
        var asset = new AssetItem
        {
            Id = Guid.NewGuid()
        };
        sut.SaveMini(ms, asset);

        var path = Path.Combine(dir, asset.MiniPath);
        Assert.True(File.Exists(path));
        using var bitmap = new Bitmap(path);
        Assert.Equal(20, bitmap.Width);
    }
}
