using AutoFixture;
using AutoFixture.NUnit3;
using NUnit.Framework;
using Microsoft.Extensions.Logging;
using Moq;
using PetecaAPIV3;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetecaAPITests
{
    [TestFixture]
    public class FinalPetecaServiceTests
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
        public void GivenIdadeAndPenaWhenValidThenSave()
        {
            _repoMock
                .Setup(r => r.Save(It.IsAny<Peteca>()))
                .Returns(true)
                .Verifiable();

            var sut = new PetecaService(_loggerMock.Object, _repoMock.Object);
            var idade = 69;
            var penas = 4;

            var result = sut.CreatePeteca(idade, penas);
            var expected = new Peteca()
            {
                Id = result.Id,
                Idade = idade,
                Pena = penas
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
                    Idade = 50,
                    Pena = 3
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
                    Idade = 25,
                    Pena = 8
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
                    Idade = 69,
                    Pena = 6
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
                    Idade = 420,
                    Pena = 5
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
        //        .Setup(r => r.FindPetecaByIdade(50, null))
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
                        Idade = 450,
                        Pena = 5
                    },
                    new Peteca()
                    {
                        Id = id,
                        Idade = 450,
                        Pena = 5
                    },
                    new Peteca()
                    {
                        Id = id,
                        Idade = 450,
                        Pena = 7
                    }
                })
                .Verifiable();
            var sut = new PetecaService(_testLogger, _repoMock.Object);
            var expected = 450;
            //Act
            var result = sut.GetIdadeMedia();

            //Assert
            Assert.AreEqual(expected, result);
        }
        [Test]
        public void GivenIdadeAndPenaWhenInvalidThenReturnNull()
        {
            _repoMock
                .Setup(r => r.Save(It.IsAny<Peteca>()))
                .Returns(true)
                .Verifiable();

            var sut = new PetecaService(_loggerMock.Object, _repoMock.Object);
            var idade = 69;
            var penas = 2;

            var result = sut.CreatePeteca(idade, penas);

            Assert.IsNull(result);
        }
        [Test]
        public void GivenIdadeAndPenaWhenInvalidThenThrow()
        {
            _repoMock
                .Setup(r => r.Save(It.IsAny<Peteca>()))
                .Throws(new NullReferenceException())
                .Verifiable();

            var sut = new PetecaService(_loggerMock.Object, _repoMock.Object);
            var idade = 69;
            var penas = 2;

            Assert.Throws<NullReferenceException>(() => sut.CreatePeteca(idade, penas));
        }
    }
}