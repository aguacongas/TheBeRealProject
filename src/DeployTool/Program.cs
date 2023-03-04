using Microsoft.Extensions.Hosting;

await Host.CreateApplicationBuilder(args)
    .AddDeployTool()
    .Build()
    .UseDeployToolAsync()
    .ConfigureAwait(false);