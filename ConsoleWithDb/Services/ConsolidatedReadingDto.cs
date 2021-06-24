using FileHelpers;
using System;
using Training.Core.FileHelpersConverters;

namespace Training.Core
{
    [DelimitedRecord(",")]
    public class ConsolidatedReadingDto
    {
      
        public long IdSensore { get; set; }
        public string NomeTipoSensore { get; set; }
        public Unit UnitaMisura { get; set; }
        public int Quota { get; set; }
        public string Provincia { get; set; }
        public string Comune { get; set; }
        public bool Storico { get; set; }
        public int GiorniDiMisura { get; set; }

        public int Utm_Nord { get; set; }
        public int UTM_Est { get; set; }

        public string lat { get; set; }

        public string lng { get; set; }

    }

}
