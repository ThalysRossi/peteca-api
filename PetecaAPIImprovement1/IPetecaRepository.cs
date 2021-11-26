using System;
using System.Collections.Generic;

namespace PetecaAPIV2
{
    public interface IPetecaRepository
    {
        public Peteca FindPetecaById(Guid id);
        public IList<Peteca> FindPetecaByIdade(int min, int? max);
        public IList<Peteca> GetPetecas();
        public bool Save(Peteca peteca);
    }
}