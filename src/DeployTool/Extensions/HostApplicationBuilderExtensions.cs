using DeployTool.Abstraction;
using DeployTool.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace Microsoft.Extensions.Hosting;
public static class HostApplicationBuilderExtensions
{
    public static HostApplicationBuilder AddDeployTool(this HostApplicationBuilder builder)
    {
        var configuration = builder.Configuration.AddUserSecrets<Program>().Build();

        builder.Services.AddScoped(sp =>
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("TheBeRealProject.DeployTool", "1.0.0"));

            var apiToken = configuration.GetValue<string>("ApiToken");
            if (apiToken is not null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
            }
            return client;
        })
        .AddTransient<IGitHubFileService>(p => new GitHubFileService(p.GetRequiredService<HttpClient>(), configuration.GetValue<string>("ApiUrl")))
        .AddTransient<IImageService>(p => new ImageService(configuration.GetValue<string>("PublishDir")))
        .AddTransient<AssetService>();

        return builder;
    }
}
