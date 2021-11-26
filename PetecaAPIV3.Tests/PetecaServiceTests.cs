using AutoFixture;
using NUnit.Framework;
using Microsoft.Extensions.Logging;
using Moq;
using PetecaAPIV3;
using System;
using System.Collections.Generic;

namespace PetecaAPIV3.Tests
{
    [TestFixture]
    public class PetecaServiceTests
    {
        private Fixture fixture;
        private Mock<ILogger<PetecaService>> _loggerMock;
        private ILogger<PetecaService> _testLogger;
        private Mock<IPetecaRepository> _repoMock;

        [SetUp]
        public void Setup()
        {
            fixture = new Fixture();
            _loggerMock = new Mock<ILogger<PetecaService>>();
            _repoMock = new Mock<IPetecaRepository>();
            _testLogger = TestLogger.Create<PetecaService>();
        }
        [Test]
        public void GivenAgeAndFeathersWhenValidThenSave()
        {
            _repoMock
                .Setup(r => r.Save(It.IsAny<Peteca>()))
                .Returns(true)
                .Verifiable();

            var sut = new PetecaService(_loggerMock.Object, _repoMock.Object);
            var age = 69;
            var feathers = 4;

            var result = sut.CreatePeteca(age, feathers);
            var expected = new Peteca()
            {
                Id = result.Id,
                Age = age,
                Feathers = feathers
            };

            _repoMock.Verify(r => r.Save(It.Is<Peteca>((arg) => Object.ReferenceEquals(arg, result))));
            _repoMock.Verify(r => r.Save(It.IsAny<Peteca>()), Times.Once);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GivenIdWhenPetecaVeiaThenReturnTrue()
        {
            //Arrange
            var id = Guid.NewGuid();

            _repoMock
                .Setup(r => r.FindPetecaById(id))
                .Returns(new Peteca()
                {
                    Id = id,
                    Age = 50,
                    Feathers = 3
                })
                .Verifiable();

            var sut = new PetecaService(_testLogger, _repoMock.Object);
            //Act
            var result = sut.IsPetecaVeia(id);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void GivenIdWhenNotPetecaVeiaThenReturnFalse()
        {
            //Arrange
            var id = Guid.NewGuid();

            _repoMock
                .Setup(r => r.FindPetecaById(id))
                .Returns(new Peteca()
                {
                    Id = id,
                    Age = 25,
                    Feathers = 8
                })
                .Verifiable();

            var sut = new PetecaService(_testLogger, _repoMock.Object);
            //Act
            var result = sut.IsPetecaVeia(id);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GivenIdWhenPetecaVeiaAndMemeThenReturnTrueAndLogMeme()
        {
            //Arrange
            var id = Guid.NewGuid();

            _repoMock
                .Setup(r => r.FindPetecaById(id))
                .Returns(new Peteca()
                {
                    Id = id,
                    Age = 69,
                    Feathers = 6
                })
                .Verifiable();
            _loggerMock
                .Setup(l => l.LogInformation(It.IsAny<string>()))
                .Verifiable();

            var sut = new PetecaService(_loggerMock.Object, _repoMock.Object);
            //Act
            var result = sut.IsPetecaVeia(id);

            //Assert
            _loggerMock
                .Verify(l => l.LogInformation(
                    It.Is<string>(arg => arg == "nice")
                    ));
            Assert.IsTrue(result);
        }

        [Test]
        public void GivenIdWhenPetecaVeiaAnd420ThenReturnTrueAndLogMeme()
        {
            //Arrange
            var id = Guid.NewGuid();
            _repoMock
                .Setup(r => r.FindPetecaById(id))
                .Returns(new Peteca()
                {
                    Id = id,
                    Age = 420,
                    Feathers = 5
                })
                .Verifiable();
            var sut = new PetecaService(_testLogger, _repoMock.Object);
            //Act
            var result = sut.IsPetecaVeia(id);

            //Assert
            Assert.IsTrue(result);
        }

        //[Test]
        //[AutoData]
        //public void GivenPetecasVeiasWhenValidAgeThenReturnList()
        //{
        //    //Arrange
        //    var petecaList = fixture.CreateMany<IList<Peteca>>(50).ToList();
        //    _repoMock
        //        .Setup(r => r.FindPetecaByAge(50, null))
        //        .Returns(petecaList)
        //        .Verifiable();
        //    var sut = new PetecaService(_testLogger, _repoMock.Object);
        //    //Act
        //    var result = sut.GetPetecasVeias();

        //    //Assert
        //    Assert.IsNotNull(result);
        //}
        [Test]
        public void GivenPetecasWhenValidAgeThenReturnAverageAges()
        {
            //Arrange
            var id = Guid.NewGuid();
            _repoMock
                .Setup(r => r.GetPetecas())
                .Returns(new List<Peteca>()
                {
                    new Peteca()
                    {
                        Id = id,
                        Age = 450,
                        Feathers = 5
                    },
                    new Peteca()
                    {
                        Id = id,
                        Age = 450,
                        Feathers = 5
                    },
                    new Peteca()
                    {
                        Id = id,
                        Age = 450,
                        Feathers = 7
                    }
                })
                .Verifiable();
            var sut = new PetecaService(_testLogger, _repoMock.Object);
            var expected = 450;
            //Act
            var result = sut.GetAverageAge();

            //Assert
            Assert.AreEqual(expected, result);
        }
        [Test]
        public void GivenAgeAndFeathersWhenInvalidThenReturnNull()
        {
            _repoMock
                .Setup(r => r.Save(It.IsAny<Peteca>()))
                .Returns(true)
                .Verifiable();

            var sut = new PetecaService(_loggerMock.Object, _repoMock.Object);
            var age = 69;
            var feathers = 2;

            var result = sut.CreatePeteca(age, feathers);

            Assert.IsNull(result);
        }
        [Test]
        public void GivenAgeAndFeathersWhenInvalidThenThrow()
        {
            _repoMock
                .Setup(r => r.Save(It.IsAny<Peteca>()))
                .Throws(new NullReferenceException())
                .Verifiable();

            var sut = new PetecaService(_loggerMock.Object, _repoMock.Object);
            var age = 69;
            var feathers = 2;

            Assert.Throws<NullReferenceException>(() => sut.CreatePeteca(age, feathers));
        }
    }
}