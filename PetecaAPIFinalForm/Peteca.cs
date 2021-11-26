using System;

namespace PetecaAPIV3
{
    public class Peteca
    {
        public static readonly IPetecaFactory Factory = new PetecaFactory();
        public Guid Id { get; set; }
        public int Age { get; set; }
        public int Feathers { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)} {Id} {Environment.NewLine}" +
                $"{nameof(Age)} {Age} {Environment.NewLine}" +
                $"{nameof(Feathers)} {Feathers} {Environment.NewLine}";
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
            public Peteca Create(int age, int feather)
            {
                if(feather < 3 || feather > 12)
                {
                    return null;
                };

                return new Peteca()
                {
                    Id = Guid.NewGuid(),
                    Age = age,
                    Feathers = feather
                };
            }
        }
    }
}