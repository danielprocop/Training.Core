using ConsoleWithDb.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SystemWrapper.Implemetation;
using SystemWrapper.Interface;
using Training.Core;
using Training.Data;

namespace Training.DataTests.Builders
{
    public class InputFileServiceBuilder
    {
        public IOptions<Paths> _settings;
        public IContextFactory _contextFactory;
        public IInputFileRepository _inputFileRepository;
        public IDirectoryInfo _directoryInfo;
        public IFile _file;
    
        public InputFileService Build()
        {
            return new InputFileService(_settings, _contextFactory, _inputFileRepository, _directoryInfo, _file);
        }

        public InputFileServiceBuilder()
        {
            _settings = Options.Create(new Paths()
            {
                InputPath = @"",
                OutputFile = @"",
                BACKUP = @""
            });
            _contextFactory = new Mock<IContextFactory>().Object;
            _inputFileRepository = new Mock<IInputFileRepository>().Object;
       
            _directoryInfo = new Mock<IDirectoryInfo>().Object;
            _file = new Mock<IFile>().Object;
        }

        public InputFileServiceBuilder WithOptions(IOptions<Paths> settings)
        {
            _settings = settings;
            return this;
        }
        public InputFileServiceBuilder WithContextFactory(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            return this;
        }
        public InputFileServiceBuilder WithInputFileRepository(IInputFileRepository inputFileRepository)
        {
            _inputFileRepository = inputFileRepository;
            return this;
        }

        public InputFileServiceBuilder WithDirectoryInfo(IDirectoryInfo directoryInfo)
        {
            _directoryInfo = directoryInfo;
            return this;
        }

        public InputFileServiceBuilder WithFile(IFile file)
        {
            _file = file;
            return this;
        }

        public InputFileServiceBuilder WithRealDependency()
        {
            _settings= Options.Create(new Paths()
            {
                InputPath= @"C:\Users\Daniel Procop\source\repos\Training.Core\ConsoleWithDb\In\",
                OutputFile=@"C:\Users\Daniel Procop\source\repos\Training.Core\ConsoleWithDb\Output\AverageProvinceData.csv",
                BACKUP= @"C:\Users\Daniel Procop\source\repos\Training.Core\ConsoleWithDb\BACKUP\"
            });

            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(System.IO.Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .Build();

            var connectionString = configuration.GetConnectionString("Default");
            _contextFactory = new DapperContextFactory(connectionString);
            _inputFileRepository = new InputFileRepository();
             _directoryInfo = new DirectoryInfoWrap();
            _file = new FileWrap();
            return this;
        }
    }
}
