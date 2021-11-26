using Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetecaAPIV1
{
    public class PetecaService
    {
        public void CreatePeteca(int age, int feathers)
        {
            PetecaRepository repo = new PetecaRepository();

            repo.Save(new Peteca() { Age = age, Feathers = feathers });
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
