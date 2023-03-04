using DeployTool.Abstraction;
using DeployTool.Services;
using Microsoft.Extensions.Hosting;

namespace DeployToolTest.Extensions;
public class HostApplicationBuilderExtensionsTest
{
    [Fact]
    public void AddDeployTool_should_setup_di_fro_deploy_tool()
    {
        var builder = Host.CreateApplicationBuilder()
            .AddDeployTool();

        var services = builder.Services;
        Assert.Contains(services, s => s.ServiceType == typeof(IImageService));
        Assert.Contains(services, s => s.ServiceType == typeof(IGitHubFileService));
        Assert.Contains(services, s => s.ServiceType == typeof(AssetService));
    }
}
