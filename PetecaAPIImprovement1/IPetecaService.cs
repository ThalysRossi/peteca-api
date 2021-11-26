using Shared;
using System;
using System.Collections.Generic;

namespace PetecaAPIV2
{
    public interface IPetecaService
    {
        public bool IsPetecaVeia(Guid id);
        public IList<Peteca> GetPetecasVeias();
        public int GetAverageAge();
        public bool CreatePeteca(int age, int feathers);
    }
}
