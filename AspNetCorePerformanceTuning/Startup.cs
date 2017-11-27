// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using AspNetCorePerformanceTuning.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetCorePerformanceTuning
{
  public class Startup
  {
    private IConfigurationRoot configuration;

    public Startup(IHostingEnvironment hostingEnvironment)
    {
      IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
        .SetBasePath(hostingEnvironment.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

      this.configuration = configurationBuilder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<Storage>(
        options => options.UseSqlite(this.configuration.GetConnectionString("DefaultConnection"))
      );

      services.AddMvc();
      services.AddMemoryCache();
      services.AddSingleton(typeof(ICache), typeof(DefaultCache));
      // Caching option #1. Uncomment to turn on built-in server-side response caching
      //services.AddResponseCaching();
    }

    public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole();
      loggerFactory.AddDebug();

      if (hostingEnvironment.IsDevelopment())
      {
        applicationBuilder.UseDeveloperExceptionPage();
        applicationBuilder.UseDatabaseErrorPage();
        applicationBuilder.UseBrowserLink();
      }

      applicationBuilder.UseStaticFiles();

      // Caching option #1. Uncomment to turn on built-in server-side response caching
      //applicationBuilder.UseResponseCaching();
      // or
      // Caching option #2.Uncomment to turn on our custom file system response caching (not production-ready!)
      //applicationBuilder.UseFileSystemCaching();
      applicationBuilder.UseMvcWithDefaultRoute();
    }
  }
}