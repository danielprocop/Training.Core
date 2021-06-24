using FileHelpers;
using System;
using Training.Core.FileHelpersConverters;

namespace Training.Core
{
    [DelimitedRecord(",")]
    [IgnoreFirst(1)]
    public class ReadingDto
    {
        private ReadingDto()
        {

        }
      
        public long IdSensore { get; set; }
        public string NomeTipoSensore { get; set; }
        [FieldConverter(typeof(UnitConverter))]
        public string UnitaMisura { get; set; }
        public long Idstazione { get; set; }
        public string NomeStazione { get; set; }
        public int Quota { get; set; }
        public string Provincia { get; set; }
        public string Comune { get; set; }
        [FieldConverter(ConverterKind.Boolean, "S", "N")]
        public bool Storico { get; set; }

        [FieldConverter(ConverterKind.Date, "dd/MM/yyyy")]
        public DateTime DataStart { get; set; }

        [FieldConverter(ConverterKind.Date, "dd/MM/yyyy")]
        public DateTime? DataStop { get; set; }
        public int Utm_Nord { get; set; }
        public int UTM_Est { get; set; }

        public string lat { get; set; }

        public string lng { get; set; }

        [FieldValueDiscarded]
        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.NotAllow)]
        public string location { get; set; }
    }

}
