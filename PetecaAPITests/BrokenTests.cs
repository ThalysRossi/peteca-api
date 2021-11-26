using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using PetecaAPIV1;

namespace PetecaAPITests
{
    public class BrokenTests
    {
        [Test]
        public void GivenIdadeWhenValidThenSaveAndReturnTrue()
        {
            //Arrange
            var sut = new PetecaService();
            var idade = 50;

            //Act
            var result = sut.CreatePeteca(idade);

            //Assert
            Assert.IsTrue(result);
        }
        [Test]
        public void GivenIdWhenValidThenReturnTrue()
        {
            //Arrange
            var sut = new PetecaService();

            //Act
            var result = sut.IsPetecaVeia(Guid.NewGuid());

            //Assert
            Assert.IsTrue(result);
        }
        [Test]
        public void GivenNoParamsReturnListWithAllPetecasVeias()
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
                    Idade = 420
                },
                new Peteca()
                {
                    Id = Guid.NewGuid(),
                    Idade = 69
                },
                new Peteca()
                {
                    Id = Guid.NewGuid(),
                    Idade = 69
                },
                new Peteca()
                {
                    Id = Guid.NewGuid(),
                    Idade = 69
                },
                new Peteca()
                {
                    Id = Guid.NewGuid(),
                    Idade = 72
                },
                new Peteca()
                {
                    Id = Guid.NewGuid(),
                    Idade = 25
                }
            };
            
            //Assert
            CollectionAssert.AreEqual(result, expected);
        }
        [Test]
        public void GivenNoParamsReturnIdadeMedia()
        {
            //Arrange
            var sut = new PetecaService();

            //Act
            var result = sut.GetIdadeMedia();
            var expected = 47;

            //Assert
            Assert.AreEqual(result, expected);
        }
    }
}
