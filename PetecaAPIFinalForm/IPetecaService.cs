using System;
using System.Collections.Generic;
using System.Text;

namespace PetecaAPIFinalForm
{
    public interface IPetecaService
    {
        public bool IsPetecaVeia(Guid id);
        public IList<Peteca> GetPetecasVeias();
        public int GetIdadeMedia();
        public Peteca CreatePeteca(int idade, int penas);
    }
}
