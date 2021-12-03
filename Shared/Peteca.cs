using System;

namespace Shared
{
    public class Peteca
    {
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
                Age == peteca.Age &&
                Feathers == peteca.Feathers;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Age);
        }
    }
}