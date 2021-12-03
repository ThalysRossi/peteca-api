using System;

namespace Shared
{
    public class PetecaServiceException : Exception
    {
        public PetecaServiceException(Exception ex)
            : base("fuuuuuu", ex)
        {
        }
    }
}