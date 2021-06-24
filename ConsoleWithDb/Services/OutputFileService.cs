using FileHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Training.Core;
using Training.Data;
using Training.Data.Reading;

namespace ConsoleWithDb.Services
{
    public class OutputFileService : IOutputFileService
    {
        private readonly IOutputFileRepository _outputFileRepository;
        private readonly IContextFactory _contextFactory;
        private readonly IConsolidatedReadingRepository _consolidatedReadingRepository;

        public OutputFileService(
            IOutputFileRepository outputFileRepository
            , IContextFactory contextFactory
            , IConsolidatedReadingRepository consolidatedReadingRepository)
        {
            if (outputFileRepository is null)
            {
                throw new ArgumentNullException(nameof(outputFileRepository));
            }

            if (contextFactory is null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            if (consolidatedReadingRepository is null)
            {
                throw new ArgumentNullException(nameof(consolidatedReadingRepository));
            }

            this._outputFileRepository = outputFileRepository;
            this._contextFactory = contextFactory;
            this._consolidatedReadingRepository = consolidatedReadingRepository;
        }

        public void CreateOutputFileFromConsistentReadings(List<ConsolidatedReadingGetDto> consolidatedReadings)
        {
            var dtos = consolidatedReadings.Select(x => new ConsolidatedReadingDto()
            {
                IdSensore = x.SensorId,
                NomeTipoSensore = x.SensorTypeName,
                UnitaMisura = x.Unit,
                Quota = x.Value,
                Provincia = x.Province,
                Comune = x.City,
                Storico = x.IsHystoric,
                GiorniDiMisura = x.DaysOfMeasure,
                Utm_Nord = x.UtmNord,
                UTM_Est = x.UtmEst,
                lat = x.Latitude,
                lng = x.Longitude

            });
            var engine = new FileHelperEngine<ConsolidatedReadingDto>();
            var content = engine.WriteString(dtos);
            OutputFileInsertDto outputFileInsertDto = new OutputFileInsertDto(Encoding.UTF8.GetBytes(content));

            using (IContext context = _contextFactory.Create())
            {
                var outputFileId = _outputFileRepository.Insert(outputFileInsertDto, context);
                List<ConsolidatedReadingUpdateDto> dtoupdates = consolidatedReadings.Select(x => new ConsolidatedReadingUpdateDto(x.Id, outputFileId, Status.Success)).ToList();
                dtoupdates.ForEach(dto =>
                     _consolidatedReadingRepository.Update(dto, context));

                context.Commit();
            }
        }
    }
}
