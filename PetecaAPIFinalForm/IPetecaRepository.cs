using Shared;
using System;
using System.Collections.Generic;

namespace PetecaAPIV3
{
    public interface IPetecaRepository
    {
        public Peteca FindPetecaById(Guid id);
        public IList<Peteca> FindPetecaByAge(int min, int? max);
        public IList<Peteca> GetPetecas();
        public bool Save(Peteca peteca);
    }
}