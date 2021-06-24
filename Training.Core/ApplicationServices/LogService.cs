using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Training.Core.ApplicationServices
{
    public class LogService
    {
        private readonly ILogger<LogService> _logger;

        public LogService(ILogger<LogService> logger )
        {
            this._logger = logger;
        }
        public virtual  void LogErrors(IList<string> errors)
        {
            if (errors == null)
            {
                throw new ArgumentNullException();
            }
            foreach (var error in errors)
            {
                _logger.LogError(error);
            }
           
        }
        public virtual void LogErrors<T>(List<Result<T>> results)
        {
            List<string> errors = results
                .Where(x => x.Success == false)
                .SelectMany(x => x.Errors)
                .ToList();
            LogErrors(errors);

        }
    }
}