using DeployTool.Abstraction;
using DeployTool.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DeployToolTest.Extensions;
public class HostApplicationBuilderExtensionsTest
{
    [Fact]
    public void AddDeployTool_should_setup_di_fro_deploy_tool()
    {
        var provider = Host.CreateApplicationBuilder()
            .AddDeployTool()
            .Build()
            .Services;

        Assert.NotNull(provider.GetService<IGitHubFileService>());
        Assert.NotNull(provider.GetService<IImageService>());
        Assert.NotNull(provider.GetService<AssetService>());
    }
}
