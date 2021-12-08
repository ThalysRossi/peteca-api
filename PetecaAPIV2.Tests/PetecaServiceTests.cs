using NUnit.Framework;
using Microsoft.Extensions.Logging;
using Moq;
using PetecaAPIV2;
using System;
using System.Collections.Generic;
using Shared;

namespace PetecaAPITests
{
    public class PetecaServiceTests
    {
        private ILogger<PetecaService> _logger;
        private Mock<IPetecaRepository> _repoMock;
        
        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IPetecaRepository>();
            _logger = TestLogger.Create<PetecaService>();
        }

        [Test]
        public void GivenAgeAndFeathersWhenValidThenCreatePetecaSuccessfully()
        {
            //Arrange
            _repoMock
                .Setup(r => r.Save(It.IsAny<Peteca>()))
                .Returns(true)
                .Verifiable();

            var sut = new PetecaService(_logger, _repoMock.Object);
            var age = 69;
            var feathers = 4;

            //Act
            var result = sut.CreatePeteca(age, feathers);

            var expected = new Peteca()
            {
                Id = result.Id,
                Age = age,
                Feathers = feathers
            };

            //Assert
            _repoMock.Verify(r => r.Save(It.Is<Peteca>((arg) => Object.ReferenceEquals(arg, result))));
            Assert.AreEqual(result, expected);
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

            var sut = new PetecaService(_logger, _repoMock.Object);
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

            var sut = new PetecaService(_logger, _repoMock.Object);
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

            var sut = new PetecaService(_logger, _repoMock.Object);
            
            //Act
            var result = sut.IsPetecaVeia(id);

            //Assert
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
            var sut = new PetecaService(_logger, _repoMock.Object);
            
            //Act
            var result = sut.IsPetecaVeia(id);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void GivenPetecasVeiasWhenValidAgeThenReturnList()
        {
            //Arrange
            _repoMock
                .Setup(r => r.FindPetecaByAge(50, null))
                .Returns(new List<Peteca>()
                {
                    new Peteca()
                    {
                        Id = Guid.NewGuid(),
                        Age = 420,
                        Feathers = 5
                    },
                    new Peteca()
                    {
                        Id = Guid.NewGuid(),
                        Age = 69,
                        Feathers = 5
                    },
                    new Peteca()
                    {
                        Id = Guid.NewGuid(),
                        Age = 69,
                        Feathers = 7
                    },
                    new Peteca()
                    {
                        Id = Guid.NewGuid(),
                        Age = 69,
                        Feathers = 10
                    },
                    new Peteca()
                    {
                        Id = Guid.NewGuid(),
                        Age = 72,
                        Feathers = 5
                    },
                })
                .Verifiable();

            var sut = new PetecaService(_logger, _repoMock.Object);

            var expected = new List<Peteca>()
            {
                new Peteca()
                {
                    Id = Guid.NewGuid(),
                    Age = 420,
                    Feathers = 5
                },
                new Peteca()
                {
                    Id = Guid.NewGuid(),
                    Age = 69,
                    Feathers = 5
                },
                new Peteca()
                {
                    Id = Guid.NewGuid(),
                    Age = 69,
                    Feathers = 7
                },
                new Peteca()
                {
                    Id = Guid.NewGuid(),
                    Age = 69,
                    Feathers = 10
                },
                new Peteca()
                {
                    Id = Guid.NewGuid(),
                    Age = 72,
                    Feathers = 5
                }
            };

            //Act
            var result = sut.GetPetecasVeias();

            //Assert
            CollectionAssert.AreEqual(result, expected);
        }

        [Test]
        public void GivenNoPetecasVeiasReturnEmptyList()
        {
            //Arrange
            _repoMock
                .Setup(r => r.FindPetecaByAge(50, null))
                .Returns(new List<Peteca>())
                .Verifiable();
            var sut = new PetecaService(_logger, _repoMock.Object);

            var expected = new List<Peteca>();

            //Act
            var result = sut.GetPetecasVeias();

            //Assert
            CollectionAssert.AreEqual(result, expected);
        }

        [Test]
        public void GivenPetecasWhenValidAgeThenReturnAverageAge()
        {
            //Arrange
            _repoMock
                .Setup(r => r.GetPetecas())
                .Returns(new List<Peteca>()
                {
                    new Peteca()
                    {
                        Id = Guid.NewGuid(),
                        Age = 450,
                        Feathers = 5
                    },
                    new Peteca()
                    {
                        Id = Guid.NewGuid(),
                        Age = 450,
                        Feathers = 5
                    },
                    new Peteca()
                    {
                        Id = Guid.NewGuid(),
                        Age = 450,
                        Feathers = 7
                    }
                })
                .Verifiable();
            var sut = new PetecaService(_logger, _repoMock.Object);
            var expected = 450;

            //Act
            var result = sut.GetAverageAge();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GivenAgeAndFeathersWhenInvalidThenThrow()
        {
            _repoMock
                .Setup(r => r.Save(It.IsAny<Peteca>()))
                .Throws(new NullReferenceException())
                .Verifiable();

            var sut = new PetecaService(_logger, _repoMock.Object);
            var age = 69;
            var feathers = 2;

            Assert.Throws<NullReferenceException>(() => sut.CreatePeteca(age, feathers));
        }
    }
}