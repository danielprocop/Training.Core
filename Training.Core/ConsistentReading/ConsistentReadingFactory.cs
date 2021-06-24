using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FluentValidation;

namespace Training.Core
{
    public class ConsistentReadingFactory : IConsistentReadingFactory
    {
        private readonly IValidator<Reading> _validator;
        public ConsistentReadingFactory(IValidator<Reading> validator)
        {
            _validator = validator;
        }
        public Result<ConsistentReading> CreateConsistentReading(Reading reading)
        {
            if (reading == null)
            {
                throw new ArgumentNullException(nameof(reading));
            }
            //Moq
          //  ReadingValidator validator = new ReadingValidator();
            var validationResult = _validator.Validate(reading);
            
            Result<ConsistentReading> result;
            if (validationResult.IsValid)
            {
                var consistentReading= new ConsistentReading(
                reading.SensorId
                , reading.SensorTypeName
                , (Unit)Enum.Parse(typeof(Unit), reading.Unit)
                , reading.Value
                , reading.Province
                , reading.City
                , reading.IsHystoric
                , ExtractDayOfMeasure(reading.StartDate, reading.StopDate)
                , reading.UtmNord
                , reading.UtmEst
                , reading.Latitude
                , reading.Longitude);

                result = Result<ConsistentReading>.Ok(consistentReading);
            }
            else
            {
                List<string> errors = validationResult.Errors
                    .Select(x => x.ToString())
                    .ToList();

                result = Result<ConsistentReading>.Ko(errors);
            }

            return result;
        }

        private int ExtractDayOfMeasure(DateTime startDate, DateTime? stopDate)
        {
            int daysOfMeasure;
            if (stopDate != null)
            {
                daysOfMeasure = ((DateTime)stopDate-startDate).Days;
            }
            else
            {
                daysOfMeasure = (DateTime.Now - startDate).Days;
            }

            return daysOfMeasure;
        }


    }
}
