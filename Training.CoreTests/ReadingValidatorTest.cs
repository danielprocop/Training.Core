using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Training.Core;

namespace Training.CoreTests
{
    [TestClass]
    public class ReadingValidatorTest
    {
        [DataTestMethod]
        [DataRow(100)]
        public void ValidateReading_SensorIdGratterThanZero_ShouldNotHaveValidationError(long sensorId)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetSensorId(sensorId)
                .Build();

            var result = validator.TestValidate(reading);
            result.ShouldNotHaveValidationErrorFor(reading => reading.SensorId);
        }

        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void ValidateReading_SensorIdLessOrEqualToZero_ShouldHaveValidationError(long sensorId)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetSensorId(sensorId)
                .Build();

            var result = validator.TestValidate(reading);
            
            result.ShouldHaveValidationErrorFor(reading => reading.SensorId);
        }

        [DataTestMethod]
        [DataRow("nome sensore")]
        public void ValidateReading_SensorTypeWithValue_ShouldNotHaveValidationError(string sensorTypeName)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetSensorTypeName(sensorTypeName)
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldNotHaveValidationErrorFor(reading => reading.SensorTypeName);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow(null)]
        public void ValidateReading_SensorTypeNameNullOrEmpty_ShouldHaveValidationError(string sensorTypeName)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetSensorTypeName(sensorTypeName)
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldHaveValidationErrorFor(reading => reading.SensorTypeName);
        }

        [DataTestMethod]
        [DataRow("ng_m3")]
        [DataRow("mg_m3")]
        [DataRow("µg_m3")]
        public void ValidateReading_UnitWithValidValue_ShouldNotHaveValidationError(string unit)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetUnit(unit)
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldNotHaveValidationErrorFor(reading => reading.Unit);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("unita")]
        public void ValidateReading_UnitWithInvalidValue_ShouldHaveValidationError(string unit)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetUnit(unit)
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldHaveValidationErrorFor(reading => reading.Unit);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(55545)]
        public void ValidateReading_StationIdWithValueGratterThanZero_ShouldNotHaveValidationError(long stationId)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetStationId(stationId)
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldNotHaveValidationErrorFor(reading => reading.StationId);
        }

        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void ValidateReading_StationIdWithValueLessOrEqualToZero_ShouldHaveValidationError(long stationId)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetStationId(stationId)
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldHaveValidationErrorFor(reading => reading.StationId);
        }

        [DataTestMethod]
        [DataRow("nome stazione")]
        public void ValidateReading_StationNameWithValue_ShouldNotHaveValidationError(string stationName)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetStationName(stationName)
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldNotHaveValidationErrorFor(reading => reading.StationName);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow(null)]
        public void ValidateReading_StationNameNullOrEmpty_ShouldHaveValidationError(string stationName)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetStationName(stationName)
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldHaveValidationErrorFor(reading => reading.StationName);
        }

        [DataTestMethod]
        [DataRow(100)]
        [DataRow(0)]
        public void ValidateReading_ValueEqualToZeroOrGratter_ShouldNotHaveValidationError(int value)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetValue(value)
                .Build();

            var result = validator.TestValidate(reading);
            result.ShouldNotHaveValidationErrorFor(reading => reading.Value);
        }

        [DataTestMethod]
        [DataRow(-1)]
        public void ValidateReading_NegativeValue_ShouldHaveValidationError(int value)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetValue(value)
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldHaveValidationErrorFor(reading => reading.Value);
        }

        [DataTestMethod]
        [DataRow("nome provincia")]
        public void ValidateReading_ProvinceWithValue_ShouldNotHaveValidationError(string province)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetProvince(province)
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldNotHaveValidationErrorFor(reading => reading.Province);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow(null)]
        public void ValidateReading_ProvinceNullOrEmpty_ShouldHaveValidationError(string province)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetProvince(province)
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldHaveValidationErrorFor(reading => reading.Province);
        }

        [DataTestMethod]
        [DataRow("nome provincia")]
        public void ValidateReading_CityWithValue_ShouldNotHaveValidationError(string city)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetCity(city)
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldNotHaveValidationErrorFor(reading => reading.City);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow(null)]
        public void ValidateReading_CityNullOrEmpty_ShouldHaveValidationError(string city)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetCity(city)
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldHaveValidationErrorFor(reading => reading.City);
        }

        [DataTestMethod]
        [DataRow(1968)]
        [DataRow(1969)]
        public void ValidateReading_StartDateWithValueAfter1968_ShouldNotHaveValidationError(int year)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetStartDate(new DateTime(year, 1, 1))
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldNotHaveValidationErrorFor(reading => reading.StartDate);
        }

        [DataTestMethod]
        [DataRow(1967)]
        [DataRow(1900)]
        public void ValidateReading_StartDateWithValueBefore1968_ShouldHaveValidationError(int year)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetStartDate(new DateTime(year, 1, 1))
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldHaveValidationErrorFor(reading => reading.StartDate);
        }

        [TestMethod]
        public void ValidateReading_StopDateWithValueAfterStartDate_ShouldNotHaveValidationError()
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetStartDate(new DateTime(2000, 1, 1))
                .SetStopDate(new DateTime(2000, 1, 2))
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldNotHaveValidationErrorFor(reading => reading.StopDate);
        }
        [TestMethod]
        public void ValidateReading_StopDateWithNoValue_ShouldNotHaveValidationError()
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetStopDate(null)
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldNotHaveValidationErrorFor(reading => reading.StopDate);
        }


        [TestMethod]
        public void ValidateReading_StopDateWithValueBeforeStartDate_ShouldNotHaveValidationError()
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetStartDate(new DateTime(2000, 1, 1))
                .SetStopDate(new DateTime(1999, 1, 2))
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldHaveValidationErrorFor(reading => reading.StopDate);
        }

        [DataTestMethod]
        [DataRow(100)]
        [DataRow(0)]
        public void ValidateReading_UtmNordEqualToZeroOrGratter_ShouldNotHaveValidationError(int utmNord)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetUtmNord(utmNord)
                .Build();

            var result = validator.TestValidate(reading);
            result.ShouldNotHaveValidationErrorFor(reading => reading.UtmNord);
        }

        [DataTestMethod]
        [DataRow(-1)]
        public void ValidateReading_NegativeUtmNord_ShouldHaveValidationError(int utmNord)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetUtmNord(utmNord)
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldHaveValidationErrorFor(reading => reading.UtmNord);
        }

        [DataTestMethod]
        [DataRow(100)]
        [DataRow(0)]
        public void ValidateReading_UtmEstEqualToZeroOrGratter_ShouldNotHaveValidationError(int utmEst)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetUtmEst(utmEst)
                .Build();

            var result = validator.TestValidate(reading);
            result.ShouldNotHaveValidationErrorFor(reading => reading.UtmEst);
        }

        [DataTestMethod]
        [DataRow(-1)]
        public void ValidateReading_NegativeUtmEst_ShouldHaveValidationError(int utmEst)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetUtmEst(utmEst)
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldHaveValidationErrorFor(reading => reading.UtmEst);
        }

        [DataTestMethod]
        [DataRow("224.5")]
        public void ValidateReading_LatitudeWithValue_ShouldNotHaveValidationError(string latitude)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetLatitude(latitude)
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldNotHaveValidationErrorFor(reading => reading.Latitude);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow(null)]
        public void ValidateReading_LatitudeNullOrEmpty_ShouldHaveValidationError(string latitude)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetLatitude(latitude)
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldHaveValidationErrorFor(reading => reading.Latitude);
        }


        [DataTestMethod]
        [DataRow("24.5")]
        public void ValidateReading_LongitudeWithValue_ShouldNotHaveValidationError(string longitude)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetLongitude(longitude)
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldNotHaveValidationErrorFor(reading => reading.Longitude);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow(null)]
        public void ValidateReading_LongitudeNullOrEmpty_ShouldHaveValidationError(string longitude)
        {
            ReadingValidator validator = new ReadingValidator();
            var reading = new ReadingBuilder()
                .SetLongitude(longitude)
                .Build();

            var result = validator.TestValidate(reading);

            result.ShouldHaveValidationErrorFor(reading => reading.Longitude);
        }
    }
}
