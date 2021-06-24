using FileHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Training.Core
{
    public class ReadingsImportService : IReadingsImportService
    {
        public const string ErrorHeader = "The header is not correct";
        public ImportResult Import(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            var engine = new FileHelperEngine<ReadingDto>();
            engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            IList<ReadingDto> records = engine.ReadStream(new StreamReader(stream));

            if (HeaderNoMatch(engine))
            {
                throw new ArgumentOutOfRangeException("header", engine.HeaderText, ErrorHeader);
            }

            IList<string> errors = GetErrorsFromEngine(engine);
            List<Reading> readings=records.Select(x=>new Reading(
                    x.IdSensore
                    , x.NomeTipoSensore
                    , x.UnitaMisura
                    , x.Idstazione
                    , x.NomeStazione
                    , x.Quota
                    , x.Provincia
                    , x.Comune
                    , x.Storico
                    , x.DataStart
                    , x.DataStop
                    , x.Utm_Nord
                    , x.UTM_Est
                    , x.lat
                    , x.lng)).ToList();

            return new ImportResult(readings, errors);


        }

        private bool HeaderNoMatch(FileHelperEngine<ReadingDto> engine)
        {
            return engine.HeaderText.Trim() != engine.GetFileHeader();
        }


        private IList<string> GetErrorsFromEngine(FileHelperEngine<ReadingDto> engine)
        {
            IList<string> errors = new List<string>();
            if (engine.ErrorManager.HasErrors)
            {
                foreach (var item in engine.ErrorManager.Errors)
                {
                    if(item.ExceptionInfo is ConvertException)
                    {
                        errors.Add($"LineNumber: {item.LineNumber}, FieldName: {((FileHelpers.ConvertException)item.ExceptionInfo).FieldName}, {item.ExceptionInfo.Message}");
                    }
                    else
                    {
                        errors.Add($"{item.ExceptionInfo.Message}");
                    }
                  
                }

            }

            return errors;
        }
    }
}
