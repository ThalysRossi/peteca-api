using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetecaAPI
{
    public class PetecaService
    {
        public bool Save(int idade)
        {
            return true;
        }
        public bool IsPetecaVeia(Guid id)
        {
            PetecaRepository idade = new PetecaRepository();
            var result = idade.FindPetecaById(id);

            if(result.Idade == 69)
            {
                Console.WriteLine("nice");
            }
            else if(result.Idade == 420)
            {
                Console.WriteLine("Blaze it!");
            }
            return result.Idade > 50;
        }
        public IList<Peteca> GetPetecasVeias()
        {
            PetecaRepository idade = new PetecaRepository();
            var result = idade.FindPetecaByIdade(50, null);

            if(result.Where(p => p.Idade == 69).Count() > 2)
            {
                Console.WriteLine("Very nice");
            }
            return result;
        }
        public int GetIdadeMedia()
        {
            PetecaRepository idade = new PetecaRepository();
            var result = idade.GetPetecas();

            return (int)result.Average(p => p.Idade);
        }
    }
}
