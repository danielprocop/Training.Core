using System;
using System.Collections.Generic;

namespace Training.Core
{
    public class ImportResult
    {
        public ImportResult(IList<Reading> readings, IList<string> errors)
        {
            Readings = readings ?? throw new ArgumentNullException(nameof(readings));
            Errors = errors ?? throw new ArgumentNullException(nameof(errors));
            Success = errors.Count == 0;
        }
        public IList<Reading> Readings { get; }
        public IList<string> Errors { get; }
        public bool Success { get; }
    }

}
