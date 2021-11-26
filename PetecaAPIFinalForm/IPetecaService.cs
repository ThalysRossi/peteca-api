using System;
using System.Collections.Generic;
using System.Text;

namespace PetecaAPIV3
{
    public interface IPetecaService
    {
        public bool IsPetecaVeia(Guid id);
        public IList<Peteca> GetPetecasVeias();
        public int GetAverageAge();
        public Peteca CreatePeteca(int age, int feathers);
    }
}
