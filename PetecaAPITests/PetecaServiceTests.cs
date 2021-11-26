using NUnit.Framework;
using Microsoft.Extensions.Logging;
using Moq;
using PetecaAPIV2;
using System;
using System.Collections.Generic;

namespace PetecaAPITests
{
    public class PetecaServiceTests
    {
        private Mock<ILogger<PetecaService>> _logger;
        private Mock<IPetecaRepository> _repoMock;
        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IPetecaRepository>();
            _logger = new Mock<ILogger<PetecaService>>();
            
        }
        [Test]
        public void GivenAgeAndPenaWhenValidThenSave()
        {
            _repoMock
                .Setup(r => r.Save(It.IsAny<Peteca>()))
                .Returns(true)
                .Verifiable();

            var sut = new PetecaService(_logger.Object, _repoMock.Object);
            var age = 69;
            var penas = 4;

            var result = sut.CreatePeteca(age, penas);

            Assert.IsTrue(result);
        }

        [Test]
        public void GivenIdWhenPetecaVeiaThenReturnTrue()
        {
            //Arrange
            ILogger<PetecaService> log = TestLogger.Create<PetecaService>();
            var id = Guid.NewGuid();

            _repoMock
                .Setup(r => r.FindPetecaById(id))
                .Returns(new Peteca()
                {
                    Id = id,
                    Age = 50,
                    Pena = 3
                })
                .Verifiable();

            var sut = new PetecaService(log, _repoMock.Object);
            //Act
            var result = sut.IsPetecaVeia(id);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void GivenIdWhenNotPetecaVeiaThenReturnFalse()
        {
            //Arrange
            ILogger<PetecaService> log = TestLogger.Create<PetecaService>();
            var id = Guid.NewGuid();

            _repoMock
                .Setup(r => r.FindPetecaById(id))
                .Returns(new Peteca()
                {
                    Id = id,
                    Age = 25,
                    Pena = 8
                })
                .Verifiable();

            var sut = new PetecaService(log, _repoMock.Object);
            //Act
            var result = sut.IsPetecaVeia(id);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GivenIdWhenPetecaVeiaAndMemeThenReturnTrueAndLogMeme()
        {
            //Arrange
            ILogger<PetecaService> log = TestLogger.Create<PetecaService>();
            var id = Guid.NewGuid();

            _repoMock
                .Setup(r => r.FindPetecaById(id))
                .Returns(new Peteca()
                {
                    Id = id,
                    Age = 69,
                    Pena = 6
                })
                .Verifiable();

            var sut = new PetecaService(log, _repoMock.Object);
            //Act
            var result = sut.IsPetecaVeia(id);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void GivenIdWhenPetecaVeiaAnd420ThenReturnTrueAndLogMeme()
        {
            //Arrange
            ILogger<PetecaService> log = TestLogger.Create<PetecaService>();
            var id = Guid.NewGuid();
            _repoMock
                .Setup(r => r.FindPetecaById(id))
                .Returns(new Peteca()
                {
                    Id = id,
                    Age = 420,
                    Pena = 5
                })
                .Verifiable();
            var sut = new PetecaService(log, _repoMock.Object);
            //Act
            var result = sut.IsPetecaVeia(id);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void GivenPetecasVeiasWhenValidAgeThenReturnList()
        {
            //Arrange
            ILogger<PetecaService> log = TestLogger.Create<PetecaService>();
            _repoMock
                .Setup(r => r.FindPetecaByAge(50, null))
                .Returns(new List<Peteca>()
                {
                    new Peteca()
                    {
                        Id = Guid.NewGuid(),
                        Age = 420,
                        Pena = 5
                    },
                    new Peteca()
                    {
                        Id = Guid.NewGuid(),
                        Age = 69,
                        Pena = 5
                    },
                    new Peteca()
                    {
                        Id = Guid.NewGuid(),
                        Age = 69,
                        Pena = 7
                    },
                    new Peteca()
                    {
                        Id = Guid.NewGuid(),
                        Age = 69,
                        Pena = 10
                    },
                    new Peteca()
                    {
                        Id = Guid.NewGuid(),
                        Age = 72,
                        Pena = 5
                    },
                    new Peteca()
                    {
                        Id = Guid.NewGuid(),
                        Age = 25,
                        Pena = 5
                    },
                })
                .Verifiable();
            var sut = new PetecaService(log, _repoMock.Object);
            //Act
            var result = sut.GetPetecasVeias();

            //Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public void GivenPetecasWhenValidAgeThenReturnAverageAges()
        {
            //Arrange
            ILogger<PetecaService> log = TestLogger.Create<PetecaService>();
            var id = Guid.NewGuid();
            _repoMock
                .Setup(r => r.GetPetecas())
                .Returns(new List<Peteca>()
                {
                    new Peteca()
                    {
                        Id = id,
                        Age = 450,
                        Pena = 5
                    },
                    new Peteca()
                    {
                        Id = id,
                        Age = 450,
                        Pena = 5
                    },
                    new Peteca()
                    {
                        Id = id,
                        Age = 450,
                        Pena = 7
                    }
                })
                .Verifiable();
            var sut = new PetecaService(log, _repoMock.Object);
            var expected = 450;
            //Act
            var result = sut.GetAverageAge();

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}