using System;
using System.Collections.Generic;

namespace Shared
{
    public class PetecaRepository : IPetecaRepository
    {
        public PetecaRepository()
        {
        }
        public bool Save(Peteca peteca)
        {
            return true;
        }

        public Peteca FindPetecaById(Guid id)
        {
            return new Peteca()
            {
                Id = id,
                Age = 29,
            };            
        }

        public IList<Peteca> FindPetecaByAge(int min, int? max)
        {
            return new List<Peteca>();
        }

        public IList<Peteca> GetPetecas()
        {
            return new List<Peteca>();
        }
    }
}