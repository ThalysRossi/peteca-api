using System;
using System.Collections.Generic;
using System.Text;

namespace PetecaAPIImprovement1
{
    public interface IPetecaService
    {
        public bool IsPetecaVeia(Guid id);
        public IList<Peteca> GetPetecasVeias();
        public int GetIdadeMedia();
        public bool CreatePeteca(int idade, int penas);
    }
}
