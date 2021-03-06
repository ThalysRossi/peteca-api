using System;
using Shared;
using System.Collections.Generic;

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
