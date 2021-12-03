using NUnit.Framework;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetecaAPIV1.Tests
{
    public class PetecaServiceTests
    {
        [Test]
        public void GivenAgeAndFeathersWhenValidThenCreatePetecaSuccessfully()
        {
            //Arrange
            var sut = new PetecaService();
            var age = 50;
            var feathers = 5;

            //Act
            sut.CreatePeteca(age, feathers);

            //Assert
            Assert.Pass();
        }

        [Test]
        public void GivenIdWhenPetecaVeiaThenReturnTrue()
        {
            //Arrange
            var sut = new PetecaService();

            //Act
            var result = sut.IsPetecaVeia(Guid.NewGuid());

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void GivenIdWhenNotPetecaVeiaThenReturnFalse()
        {
            //Arrange
            var sut = new PetecaService();

            //Act
            var result = sut.IsPetecaVeia(Guid.NewGuid());

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GivenNoPetecasVeiasReturnEmptyList()
        {
            //Arrange
            var sut = new PetecaService();

            //Act
            var result = sut.GetPetecasVeias();

            //Assert
            Assert.IsFalse(result.Any());
        }

        [Test]
        public void GivenPetecasVeiasExistReturnListWithAllExistingPetecasVeias()
        {
            //Arrange
            var sut = new PetecaService();

            //Act
            var result = sut.GetPetecasVeias();
            var expected = new List<Peteca>()
            {
                new Peteca()
                {
                    Id = Guid.NewGuid(),
                    Age = 420
                },
                new Peteca()
                {
                    Id = Guid.NewGuid(),
                    Age = 69
                },
                new Peteca()
                {
                    Id = Guid.NewGuid(),
                    Age = 69
                },
                new Peteca()
                {
                    Id = Guid.NewGuid(),
                    Age = 69
                },
                new Peteca()
                {
                    Id = Guid.NewGuid(),
                    Age = 72
                },
                new Peteca()
                {
                    Id = Guid.NewGuid(),
                    Age = 25
                }
            };
            
            //Assert
            CollectionAssert.AreEqual(result, expected);
        }

        [Test]
        public void GivenPetecasExistReturnsAverageAge()
        {
            //Arrange
            var sut = new PetecaService();

            //Act
            var result = sut.GetAverageAge();
            var expected = 47;

            //Assert
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void GivenUnexpectedErrorThrowsPetecaServiceException()
        {
            //Arrange
            var sut = new PetecaService();

            //Act / Assert
            Assert.Throws<PetecaServiceException>(() => sut.CreatePeteca(101, 5));
        }

        [Test]
        public void GivenAgeAndFeathersWhenInvalidThenThrow()
        {
            //Arrange
            var sut = new PetecaService();
            var age = 69;
            var feathers = 2;

            //Act / Assert
            Assert.Throws<NullReferenceException>(() => sut.CreatePeteca(age, feathers));
        }
    }
}
