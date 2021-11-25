
using System;
using System.Collections.Generic;

namespace PetecaAPI
{
    public class PetecaRepository
    {
        

        public PetecaRepository()
        {

        }
        public bool Save(int idade)
        {
            throw new NotImplementedException();
        }
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
    }
}