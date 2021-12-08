using Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetecaAPIV1
{
    public class PetecaService
    {
        public Peteca CreatePeteca(int age, int feathers)
        {
            PetecaRepository repo = new PetecaRepository();

            var peteca = new Peteca()
            {
                Age = age,
                Feathers = feathers
            };
            repo.Save(peteca);

            return peteca;
        }

        public bool IsPetecaVeia(Guid id)
        {
            PetecaRepository age = new PetecaRepository();
            var result = age.FindPetecaById(id);

            return result.Age > 50;
        }

        public IList<Peteca> GetPetecasVeias()
        {
            PetecaRepository age = new PetecaRepository();
            var result = age.FindPetecaByAge(50, null);

            if(result.Where(p => p.Age == 69).Count() > 2)
            {
                Console.WriteLine("Very nice");
            }
            return result;
        }

        public int GetAverageAge()
        {
            PetecaRepository age = new PetecaRepository();
            
            var result = age.GetPetecas();

            return (int)result.Average(p => p.Age);
        }
    }
}
