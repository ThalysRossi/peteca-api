using System;
using System.Collections.Generic;

namespace Shared
{
    public interface IPetecaRepository
    {
        public Peteca FindPetecaById(Guid id);
        public IList<Peteca> FindPetecaByAge(int min, int? max);
        public IList<Peteca> GetPetecas();
        public void Save(Peteca peteca);
    }
}