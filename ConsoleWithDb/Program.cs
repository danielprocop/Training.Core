using App.Servicess;
using ConsoleWithDb.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using SystemWrapper.Implemetation;
using SystemWrapper.Interface;
using Training.Core;
using Training.Core.ApplicationServices;
using Training.Core.ProvinceData;
using Training.Data;

namespace App
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                var host = GetHost();

                using (var serviceScope = host.Services.CreateScope())
                {
                    var services = serviceScope.ServiceProvider;

                    // creo la app
                    var runner = services.GetRequiredService<SystemManager>();
                    var logger = services.GetRequiredService<ILogger<SystemManager>>();
                    try
                    {
                        runner.Run();
                    }
                    catch (Exception ex)
                    {

                        logger.LogError(ex, "");
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }


        private static IHost GetHost()
        {
            var hostBuilder = new HostBuilder()
              .ConfigureServices((hostContext, services) =>
              {
                  // specifico qual'è il file con la configurazione
                  IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();


                  // aggiungo la configurazione di NLog
                  NLog.LogManager.Configuration = new NLogLoggingConfiguration(configuration.GetSection("NLog"));
                  services.AddSingleton<IConfiguration>(configuration);

                  services.Configure<Paths>(configuration.GetSection(nameof(Paths)));
                
                  string dbConnectionString = configuration.GetConnectionString("Default");
                  services.AddTransient<IContextFactory>((sp) => new DapperContextFactory(dbConnectionString));
                  services.AddTransient<IInputFileRepository, InputFileRepository>();
                  services.AddTransient<IReadingRepository, ReadingRepository>();
                  services.AddTransient<IConsolidatedReadingRepository, ConsolidatedReadingRepository>();
                  services.AddTransient<IOutputFileRepository, OutputFileRepository>();
                  services.AddTransient<IDirectoryInfo, DirectoryInfoWrap>();
                  services.AddTransient<IFile, FileWrap>();
                  services.AddTransient<IFile, FileWrap>();
                  services.AddTransient<IReadingsImportService, ReadingsImportService>();
                  services.AddTransient<IValidator<Reading>, ReadingValidator>();
                  services.AddTransient<IConsistentReadingFactory, ConsistentReadingFactory>();
                  services.AddTransient<IReadingService, ReadingService>();
                  services.AddTransient<IConsolidatedReadingService, ConsolidatedReadingService>();
                  services.AddTransient<IOutputFileService, OutputFileService>();
                  services.AddTransient<IInputFileService, InputFileService>();

                  services.AddTransient<SystemManager>();
              })
              .ConfigureLogging(logBuilder =>
              {
                  logBuilder.SetMinimumLevel(LogLevel.Trace);
                  // aggiungo NLog
                  logBuilder.AddNLog();
              }).UseConsoleLifetime();

            return hostBuilder.Build();
        }
    }
}
