using System;

namespace PetecaAPIV3
{
    public class Peteca
    {
        public static readonly IPetecaFactory Factory = new PetecaFactory();
        public Guid Id { get; set; }
        public int Idade { get; set; }
        public int Pena { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)} {Id} {Environment.NewLine}" +
                $"{nameof(Idade)} {Idade} {Environment.NewLine}" +
                $"{nameof(Pena)} {Pena} {Environment.NewLine}";
        }

        public override bool Equals(object obj)
        {
            return obj is Peteca peteca &&
                Id == peteca.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        private class PetecaFactory : IPetecaFactory
        {
            public Peteca Create(int idade, int pena)
            {
                if(pena < 3 || pena > 12)
                {
                    return null;
                };

                return new Peteca()
                {
                    Id = Guid.NewGuid(),
                    Idade = idade,
                    Pena = pena
                };
            }
        }
    }
}