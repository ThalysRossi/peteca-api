using System;
using System.Collections.Generic;

namespace PetecaAPIV3
{
    public class PetecaRepository : IPetecaRepository
    {
        
        public Peteca FindPetecaById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<Peteca> FindPetecaByIdade(int min, int? max)
        {
            throw new NotImplementedException();
        }

        public IList<Peteca> GetPetecas()
        {
            throw new NotImplementedException();
        }

        public bool Save(Peteca peteca)
        {
            throw new NotImplementedException();
        }
    }
}