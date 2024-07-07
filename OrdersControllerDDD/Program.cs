using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OrdersControllerDDD;
using Serilog;
using System;
using System.IO;

try
{
    var host = CreateHostBuilder(args).Build();
    
    var app = host.RunAsync();
    await app;


}
catch (Exception ex)
{

	throw;
}

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        })
        .ConfigureAppConfiguration((context, builder) =>
        {
            builder.SetBasePath($"{Directory.GetCurrentDirectory()}\\Config");
            Log.Information($"Application logging configured for environment: '{context.HostingEnvironment.EnvironmentName}'");
        })
        .UseDefaultServiceProvider((x, options) =>
        {
            options.ValidateOnBuild = true;
            options.ValidateScopes = true;
        });
