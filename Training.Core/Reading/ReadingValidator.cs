using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.Core
{
    public class ReadingValidator: AbstractValidator<Reading>
    {
        public ReadingValidator()
        {
            RuleFor(reading => reading.SensorId)
                .GreaterThan(0)
                .WithMessage("SensorId  cannot be 0 or negative");

            RuleFor(reading => reading.SensorTypeName)
                .NotEmpty()
                .WithMessage("SensorTypeName cannot be null o empty");

            RuleFor(reading => reading.Unit)
                .NotEmpty()
                .IsEnumName(typeof(Unit), caseSensitive: false)
                .WithMessage("must be rapresented by the enum Unit");

            RuleFor(reading => reading.StationId)
                .GreaterThan(0)
                .WithMessage("StationId cannot be 0 or negative");


            RuleFor(reading => reading.StationName)
                .NotEmpty()
                .WithMessage("StationName cannot be null o empty");
           
            RuleFor(reading => reading.Value)
             .GreaterThanOrEqualTo(0)
             .WithMessage("Value cannot be negative");

            RuleFor(reading => reading.Province)
              .NotEmpty()
              .WithMessage("Province cannot be null o empty");

            RuleFor(reading => reading.City)
               .NotEmpty()
               .WithMessage("City cannot be null o empty");


            RuleFor(reading => reading.StartDate)
              .GreaterThanOrEqualTo(new DateTime(1968, 1, 1))
              .WithMessage("StartDate cannot be before 1968");

            RuleFor(reading => reading.StopDate)
              .GreaterThan(r=>r.StartDate)
              .When(x=>x.StopDate !=null)
              .WithMessage("StopDate must be after the StartDate");


            RuleFor(reading => reading.UtmNord)
               .GreaterThanOrEqualTo(0)
               .WithMessage("UtmNord cannot be negative");
           
            RuleFor(reading => reading.UtmEst)
              .GreaterThanOrEqualTo(0)
              .WithMessage("UtmEst cannot be negative");


            RuleFor(reading => reading.Latitude)
                .NotEmpty()
                .WithMessage("Latitude cannot be null o empty");


            RuleFor(reading => reading.Longitude)
                .NotEmpty()
                .WithMessage("Longitude cannot be null o empty");
        }
    }
}
