using FluentValidation;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Training.Core;

namespace Training.CoreTests
{
    [TestClass]
    public class ConsistentReadingFactoryTest
    {
        Mock<IValidator<Reading>> validator;
        [TestInitialize]  
        public void Init()
        {
            validator = new Mock<IValidator<Reading>>(MockBehavior.Strict);
        }
        [TestMethod]
        public void CreateConsistentReading_ReadingWithoutErrors_ReturnResultOK()
        {
            Reading reading = new ReadingBuilder()
                .SetDefaults()
                .Build();

            ConsistentReading expectedConsistentReading = new ConsistendReadingBuilder()
                .SetDataFromReading(reading)
                .Build();


            validator
                .Setup(x => x.Validate(It.IsAny<Reading>()))
                .Returns(new ValidationResult());

            ConsistentReadingFactory sut = new ConsistentReadingFactory(validator.Object);

            //Act
            Result<ConsistentReading> result = sut.CreateConsistentReading(reading);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.Errors.Count);
            AssertHelper.AreEqual(result.Value, expectedConsistentReading);

        }

        [TestMethod]
        public void CreateConsistentReading_ReadingWithSensorIdLessThanZero_ReturnResultKO()
        {
            Reading reading = new ReadingBuilder()
                .SetDefaults()
                .Build();

            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("SensorId", "SensorId  cannot be 0 or negative"));

            validator
                .Setup(x => x.Validate(reading))
                .Returns(validationResult);

            ConsistentReadingFactory sut = new ConsistentReadingFactory(validator.Object);

            //Act
            Result<ConsistentReading> result = sut.CreateConsistentReading(reading);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(1, result.Errors.Count);

        }
        [TestMethod]
        public void CreateConsistentReading_ReadingWithSensorTypeNameEmpty_ReturnResultKO()
        {
            Reading reading = new ReadingBuilder()
                .SetDefaults()
                .SetSensorTypeName("")
                .Build();
            //usare Moq

            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("SensorTypeName", "SensorTypeName cannot be null o empty"));

            validator
                .Setup(x => x.Validate(It.IsAny<Reading>()))
                .Returns(validationResult);

            ConsistentReadingFactory sut = new ConsistentReadingFactory(validator.Object);

            //Act
            Result<ConsistentReading> result = sut.CreateConsistentReading(reading);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(1, result.Errors.Count);

        }
        [TestMethod]
        public void CreateConsistentReading_ReadingWithUnitNotValid_ReturnResultKO()
        {
            Reading reading = new ReadingBuilder()
                .SetDefaults()
                .SetUnit("m")
                .Build();

            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("Unit", "must be rapresented by the enum Unit"));

            validator
                .Setup(x => x.Validate(It.IsAny<Reading>()))
                .Returns(validationResult);

            ConsistentReadingFactory sut = new ConsistentReadingFactory(validator.Object);

            //Act
            Result<ConsistentReading> result = sut.CreateConsistentReading(reading);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(1, result.Errors.Count);

        }
        [TestMethod]
        public void CreateConsistentReading_ReadingWithStationIdLessThanZero_ReturnResultKO()
        {
            Reading reading = new ReadingBuilder()
                .SetDefaults()
                .SetStationId(-1)
                .Build();

            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("Unit", "must be rapresented by the enum Unit"));

            validator
                .Setup(x => x.Validate(It.IsAny<Reading>()))
                .Returns(validationResult);

            ConsistentReadingFactory sut = new ConsistentReadingFactory(validator.Object);

            //Act
            Result<ConsistentReading> result = sut.CreateConsistentReading(reading);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(1, result.Errors.Count);

        }

        [TestMethod]
        public void CreateConsistentReading_ReadingWithStationNameEmpty_ReturnResultKO()
        {
            Reading reading = new ReadingBuilder()
                .SetDefaults()
                .SetStationName(string.Empty)
                .Build();

            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("StationName", "StationName cannot be null o empty"));

            validator
                .Setup(x => x.Validate(It.IsAny<Reading>()))
                .Returns(validationResult);

            ConsistentReadingFactory sut = new ConsistentReadingFactory(validator.Object);

            //Act
            Result<ConsistentReading> result = sut.CreateConsistentReading(reading);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(1, result.Errors.Count);

        }
        [TestMethod]
        public void CreateConsistentReading_ReadingWithValueLessThanZero_ReturnResultKO()
        {
            Reading reading = new ReadingBuilder()
                .SetDefaults()
                .SetValue(-1)
                .Build();

            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("Value", "Value cannot be negative"));

            validator
                .Setup(x => x.Validate(It.IsAny<Reading>()))
                .Returns(validationResult);

            ConsistentReadingFactory sut = new ConsistentReadingFactory(validator.Object);

            //Act
            Result<ConsistentReading> result = sut.CreateConsistentReading(reading);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(1, result.Errors.Count);

        }
        [TestMethod]
        public void CreateConsistentReading_ReadingWithProvinceEmpty_ReturnResultKO()
        {
            Reading reading = new ReadingBuilder()
                .SetDefaults()
                .SetProvince(string.Empty)
                .Build();

            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("Province", "Province cannot be null o empty"));

            validator
                .Setup(x => x.Validate(It.IsAny<Reading>()))
                .Returns(validationResult);

            ConsistentReadingFactory sut = new ConsistentReadingFactory(validator.Object);

            //Act
            Result<ConsistentReading> result = sut.CreateConsistentReading(reading);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(1, result.Errors.Count);

        }

        [TestMethod]
        public void CreateConsistentReading_ReadingWithCityEmpty_ReturnResultKO()
        {
            Reading reading = new ReadingBuilder()
                .SetDefaults()
                .SetCity(string.Empty)
                .Build();

            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("City", "City cannot be null o empty"));

            validator
                .Setup(x => x.Validate(It.IsAny<Reading>()))
                .Returns(validationResult);

            ConsistentReadingFactory sut = new ConsistentReadingFactory(validator.Object);

            //Act
            Result<ConsistentReading> result = sut.CreateConsistentReading(reading);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(1, result.Errors.Count);

        }
        [TestMethod]
        public void CreateConsistentReading_ReadingWithStartDateBefore1968_ReturnResultKO()
        {
            Reading reading = new ReadingBuilder()
                .SetDefaults()
                .SetStartDate(new DateTime(1967,1,1))
                .Build();

            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("StartDate", "StartDate cannot be before 1968"));

            validator
                .Setup(x => x.Validate(It.IsAny<Reading>()))
                .Returns(validationResult);

            ConsistentReadingFactory sut = new ConsistentReadingFactory(validator.Object);

            //Act
            Result<ConsistentReading> result = sut.CreateConsistentReading(reading);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(1, result.Errors.Count);

        }
        [TestMethod]
        public void CreateConsistentReading_ReadingWithStopDateBeforeStartDate_ReturnResultKO()
        {
            Reading reading = new ReadingBuilder()
                .SetDefaults()
                .SetStartDate(new DateTime(2000,1,1))
                .SetStopDate(new DateTime(1999, 1, 1))
                .Build();

            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("StopDate", "StopDate must be after the StartDate"));

            validator
                .Setup(x => x.Validate(It.IsAny<Reading>()))
                .Returns(validationResult);

            ConsistentReadingFactory sut = new ConsistentReadingFactory(validator.Object);

            //Act
            Result<ConsistentReading> result = sut.CreateConsistentReading(reading);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(1, result.Errors.Count);

        }
        [TestMethod]
        public void CreateConsistentReading_ReadingWithUtmNordLessThanZero_ReturnResultKO()
        {
            Reading reading = new ReadingBuilder()
                .SetDefaults()
                .SetUtmNord(-1)
                .Build();

            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("UtmNord", "UtmNord cannot be negative"));

            validator
                .Setup(x => x.Validate(It.IsAny<Reading>()))
                .Returns(validationResult);

            ConsistentReadingFactory sut = new ConsistentReadingFactory(validator.Object);

            //Act
            Result<ConsistentReading> result = sut.CreateConsistentReading(reading);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(1, result.Errors.Count);

        }
        [TestMethod]
        public void CreateConsistentReading_ReadingWithUtmEstLessThanZero_ReturnResultKO()
        {
            Reading reading = new ReadingBuilder()
                .SetDefaults()
                .SetUtmEst(-1)
                .Build();

            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("UtmEst", "UtmEst cannot be negative"));

            validator
                .Setup(x => x.Validate(It.IsAny<Reading>()))
                .Returns(validationResult);

            ConsistentReadingFactory sut = new ConsistentReadingFactory(validator.Object);

            //Act
            Result<ConsistentReading> result = sut.CreateConsistentReading(reading);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(1, result.Errors.Count);

        }
        [TestMethod]
        public void CreateConsistentReading_ReadingWithLatitudeEmpty_ReturnResultKO()
        {
            Reading reading = new ReadingBuilder()
                .SetDefaults()
                .SetLatitude(String.Empty)
                .Build();

            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("Latitude", "Latitude cannot be null o empty"));

            validator
                .Setup(x => x.Validate(It.IsAny<Reading>()))
                .Returns(validationResult);

            ConsistentReadingFactory sut = new ConsistentReadingFactory(validator.Object);

            //Act
            Result<ConsistentReading> result = sut.CreateConsistentReading(reading);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(1, result.Errors.Count);

        }
        [TestMethod]
        public void CreateConsistentReading_ReadingWithLongitudeEmpty_ReturnResultKO()
        {
            Reading reading = new ReadingBuilder()
                .SetDefaults()
                .SetLongitude(String.Empty)
                .Build();

            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("Longitude", "Longitude cannot be null o empty"));

            validator
                .Setup(x => x.Validate(It.IsAny<Reading>()))
                .Returns(validationResult);

            ConsistentReadingFactory sut = new ConsistentReadingFactory(validator.Object);

            //Act
            Result<ConsistentReading> result = sut.CreateConsistentReading(reading);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(1, result.Errors.Count);

        }

        [TestMethod]
        public void CreateConsistentReading_ReadingNull_ThrowArgumentNullException()
        {
            Reading reading = null;
            ConsistentReadingFactory sut = new ConsistentReadingFactory(validator.Object);

            try
            {
                Result<ConsistentReading> result = sut.CreateConsistentReading(reading);
            }
            catch (ArgumentNullException)
            {
                return;
            }
            Assert.Fail("No exception was throw");

        }
    }
}
