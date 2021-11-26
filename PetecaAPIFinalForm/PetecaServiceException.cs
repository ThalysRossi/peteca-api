using System;

namespace PetecaAPIV3
{
    public class PetecaServiceException : Exception
    {
        public PetecaServiceException(Exception ex)
            : base("fuuuuuu", ex)
        {
        }
    }
}