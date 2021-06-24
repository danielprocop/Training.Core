using App.Servicess;
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
using Training.Core;
using Training.Core.ApplicationServices;
using Training.Core.ProvinceData;

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
                    var runner = services.GetRequiredService<ManagerService>();
                    var logger= services.GetRequiredService<ILogger<ManagerService>>();
                    try
                    {
                        runner.Run();
                    }
                    catch (Exception ex)
                    {

                        logger.LogError(ex,"");
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
                  services.AddTransient<ManagerService>();
                  services.AddTransient<IReadingsImportService, ReadingsImportService>();
                  services.AddTransient<IValidator<Reading>, ReadingValidator>();
                  services.AddTransient<IConsistentReadingFactory, ConsistentReadingFactory>();
                  services.AddTransient<IProvinceDataListFactory, ProvinceDataListFactory>();
                  services.AddTransient<IAverageProvinceDataFactory, AverageProvinceDataFactory>();
                  services.AddTransient<IAverageProvinceDataExportService, AverageProvinceDataExportService>();
                  services.AddTransient<LogService>();
                  services.AddTransient<DataAggregatorService>();
                  services.AddTransient<ReaderFileService>();
               
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
