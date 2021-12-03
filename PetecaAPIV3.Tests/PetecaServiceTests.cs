using AutoFixture;
using AutoFixture.NUnit3;
using NUnit.Framework;
using Microsoft.Extensions.Logging;
using Moq;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetecaAPIV3.Tests
{
    [TestFixture]
    public class PetecaServiceTests
    {
        private Fixture fixture;
        private ILogger<PetecaService> _testLogger;
        private Mock<IPetecaRepository> _repoMock;

        [OneTimeSetUp]
        public void Setup()
        {
            fixture = new Fixture();
            _repoMock = new Mock<IPetecaRepository>();
            _testLogger = TestLogger.Create<PetecaService>();
        }

        [Test]
        public void GivenAgeAndFeathersWhenValidThenCreatePetecaSuccessfully()
        {
            _repoMock
                .Setup(r => r.Save(It.IsAny<Peteca>()))
                .Returns(true)
                .Verifiable();

            var sut = new PetecaService(_testLogger, _repoMock.Object);
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
            Assert.That(expected, Is.EqualTo(result).And.Not.Null);
        }

        [Test]
        public void GivenIdWhenPetecaVeiaThenReturnTrue()
        {
            //Arrange
            var petecaVeiaFixture = new Fixture();
            petecaVeiaFixture.Customize<Peteca>(ob => ob
                .With(p => p.Age, () => new Random().Next(50, 100))
                );
            var peteca = petecaVeiaFixture.Create<Peteca>();

            _repoMock
                .Setup(r => r.FindPetecaById(peteca.Id))
                .Returns(peteca)
                .Verifiable();
            
            var sut = new PetecaService(_testLogger, _repoMock.Object);

            //Act
            var result = sut.IsPetecaVeia(peteca.Id);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenIdWhenNotPetecaVeiaThenReturnFalse()
        {
            //Arrange
            fixture.Customize<Peteca>(ob => ob
                .With(p => p.Age, () => new Random().Next(1, 49))
                );
            var peteca = fixture.Create<Peteca>();
            _repoMock
                .Setup(r => r.FindPetecaById(peteca.Id))
                .Returns(peteca)
                .Verifiable();

            var sut = new PetecaService(_testLogger, _repoMock.Object);
            //Act
            var result = sut.IsPetecaVeia(peteca.Id);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenIdWhenPetecaVeiaAndMemeThenReturnTrueAndLogMeme()
        {
            //Arrange
            fixture.Customize<Peteca>(ob => ob
               .With(p => p.Age, () => 69)
            );
            var peteca = fixture.Create<Peteca>();
            _repoMock
                .Setup(r => r.FindPetecaById(peteca.Id))
                .Returns(peteca)
                .Verifiable();

            var sut = new PetecaService(_testLogger, _repoMock.Object);
            //Act
            var result = sut.IsPetecaVeia(peteca.Id);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenIdWhenPetecaVeiaAnd420ThenReturnTrueAndLogMeme()
        {
            //Arrange
            fixture.Customize<Peteca>(ob => ob
                .With(p => p.Age, () => 420)
            );
            var peteca = fixture.Create<Peteca>();

            _repoMock
                .Setup(r => r.FindPetecaById(peteca.Id))
                .Returns(peteca)
                .Verifiable();

            var sut = new PetecaService(_testLogger, _repoMock.Object);

            //Act
            var result = sut.IsPetecaVeia(peteca.Id);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenPetecasWhenValidAgeThenReturnAverageAge()
        {
            var petecaList = fixture.CreateMany<Peteca>(100).ToList();
            //Arrange
            _repoMock
                .Setup(r => r.GetPetecas())
                .Returns(petecaList)
                .Verifiable();

            var sut = new PetecaService(_testLogger, _repoMock.Object);

            var expected = (int)petecaList.Average(p => p.Age);

            //Act
            var result = sut.GetAverageAge();

            //Assert
            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void GivenPetecasVeiasWhenValidAgeThenReturnList()
        {
            //Arrange
            var petecaList = fixture.CreateMany<Peteca>(50).ToList();

            _repoMock
                .Setup(r => r.FindPetecaByAge(50, null))
                .Returns(petecaList.Where(p => p.Age > 50).ToList())
                .Verifiable();
            var sut = new PetecaService(_testLogger, _repoMock.Object);

            var expected = petecaList.Where(p => p.Age > 50);

            //Act
            var result = sut.GetPetecasVeias();

            //Assert            
            CollectionAssert.AreEqual(result, expected);
        }

        [Test]
        public void GivenLotsOfPetecasWhenValidAgeThenReturnAverageAge()
        {
            //Arrange
            var petecaList = fixture.CreateMany<Peteca>(50).ToList();

            _repoMock
                .Setup(r => r.GetPetecas())
                .Returns(petecaList)
                .Verifiable();

            var sut = new PetecaService(_testLogger, _repoMock.Object);

            var expected = (int)petecaList.Average(p => p.Age);

            //Act
            var result = sut.GetAverageAge();

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        //[Test]
        //public void GivenAgeAndFeathersWhenInvalidThenReturnNull()
        //{
        //    _repoMock
        //        .Setup(r => r.Save(It.IsAny<Peteca>()))
        //        .Returns(false)
        //        .Verifiable();

        //    var sut = new PetecaService(_testLogger, _repoMock.Object);
        //    var age = 69;
        //    var feathers = 2;

        //    var result = sut.CreatePeteca(age, feathers);

        //    _repoMock.Verify(r => r.Save(It.IsAny<Peteca>()), Times.Never);
        //    Assert.IsNull(result);
        //}

        [Test]
        public void GivenAgeAndFeathersWhenInvalidThenThrow()
        {
            _repoMock
                .Setup(r => r.Save(It.IsAny<Peteca>()))
                .Throws(new NullReferenceException())
                .Verifiable();

            var sut = new PetecaService(_testLogger, _repoMock.Object);
            var age = 69;
            var feathers = 2;

            Assert.Throws<NullReferenceException>(() => sut.CreatePeteca(age, feathers));
        }
    }
}