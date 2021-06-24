using ConsoleWithDb.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.DataTests.Builders
{
    public class SystemManagerBuilder
    {
        private  IInputFileService _inputFileService;
        private  IReadingService _readingService;
        private  IConsolidatedReadingService _consolidatedReadingService;
        private  IOutputFileService _outputFileService;

        public SystemManager Build()
        {
            return new SystemManager(_inputFileService, _readingService, _consolidatedReadingService, _outputFileService);
        }

        public SystemManagerBuilder WithInputFileService(IInputFileService inputFileService)
        {
            _inputFileService = inputFileService;
            return this;
        }
        public SystemManagerBuilder WithReadingService(IReadingService readingService)
        {
            _readingService = readingService;
            return this;
        }
        public SystemManagerBuilder WithConsolidatedReadingService(IConsolidatedReadingService consolidatedReadingService)
        {
            _consolidatedReadingService = consolidatedReadingService;
            return this;
        }
        public SystemManagerBuilder WithOutputFileService(IOutputFileService outputFileService)
        {
            _outputFileService = outputFileService;
            return this;
        }

        public SystemManagerBuilder WithRealDependencies(ServiceProvider serviceProvider)
        {
            _inputFileService = serviceProvider.GetRequiredService<IInputFileService>(); 
            _readingService = serviceProvider.GetRequiredService<IReadingService>();
            _consolidatedReadingService= serviceProvider.GetRequiredService<IConsolidatedReadingService>();
            _outputFileService = serviceProvider.GetRequiredService<IOutputFileService>();
            return this;
        }
    }
   
}
