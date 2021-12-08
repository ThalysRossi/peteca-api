using Microsoft.Extensions.Logging;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetecaAPIV3
{
    public class PetecaService : IPetecaService
    {
        private readonly ILogger<PetecaService> _logger;
        private readonly IPetecaRepository _repository;
        
        public PetecaService(
            ILogger<PetecaService> logger, 
            IPetecaRepository repository
        )
        {
            _logger = logger;
            _repository = repository;
        }

        public bool IsPetecaVeia(Guid id)
        {
            try
            {
                var result = _repository.FindPetecaById(id);

                if (result.Age == 69)
                {
                    _logger.LogInformation("nice");
                }

                else if (result.Age == 420)
                {
                    _logger.LogInformation("Blaze it");
                }
                _logger.LogInformation(result.Age.ToString());
                return result.Age >= 50;
            }
            catch(Exception ex)
            {
                throw new PetecaServiceException(ex);
            }
        }

        public IList<Peteca> GetPetecasVeias()
        {
            try
            {
                var result = _repository.FindPetecaByAge(50, null);

                if (result.Where(p => p.Age == 69).Count() > 2)
                {
                    _logger.LogInformation("Very nice");
                }

                return result;
            }
            catch(Exception ex)
            {
                throw new PetecaServiceException(ex);
            }
        }

        public int GetAverageAge()
        {
            try
            {
                var result = _repository.GetPetecas();

                if (result.Sum(p => p.Age) >= 1346)
                {
                    _logger.LogInformation("Congratzz, you've got the Plague!");
                }

                return (int)result.Average(p => p.Age);
            }
            catch (Exception ex)
            {
                throw new PetecaServiceException(ex);
            }
        }
        
        public Peteca CreatePeteca(int age, int feathers)
        {
            try
            {
                var peteca = new Peteca()
                {
                    Id = Guid.NewGuid(),
                    Age = age,
                    Feathers = feathers
                };

                _repository.Save(peteca);

                return peteca;
            }

            catch (NullReferenceException)
            {
                throw new NullReferenceException();
            }
        }
    }
}
