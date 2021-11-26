using System;
using System.Collections.Generic;
using System.Text;

namespace PetecaAPIV3
{
    public interface IPetecaService
    {
        public bool IsPetecaVeia(Guid id);
        public IList<Peteca> GetPetecasVeias();
        public int GetIdadeMedia();
        public Peteca CreatePeteca(int idade, int penas);
    }
}
