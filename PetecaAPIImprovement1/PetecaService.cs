using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace PetecaAPIV2
{
    public class PetecaService : IPetecaService
    {
        private readonly ILogger<PetecaService> _logger;
        private readonly IPetecaRepository _repository;
        
        public PetecaService(ILogger<PetecaService> logger, IPetecaRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public bool IsPetecaVeia(Guid id)
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

            return result.Age >= 50;
        }

        public IList<Peteca> GetPetecasVeias()
        {
            var result = _repository.FindPetecaByAge(50, null);

            if (result.Where(p => p.Age == 69).Count() > 2)
            {
                _logger.LogInformation("Very nice");
            }

            return result;
        }

        public int GetAverageAge()
        {
            var result = _repository.GetPetecas();
            if((int)result.Average(p => p.Age) > 1346 || (int)result.Average(p => p.Age) < 1351)
            {
                _logger.LogInformation("Congratzz, you've got the Plague!");
            }

            return (int)result.Average(p => p.Age);
        }
        
        public bool CreatePeteca(int age, int feathers)
        {
            var peteca = new Peteca()
            {
                Id = Guid.NewGuid(),
                Age = age,
                Feathers = feathers
            };

            _repository.Save(peteca);

            return true;
        }
    }
}
