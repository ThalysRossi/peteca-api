using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetecaAPIImprovement1
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

            LogPetecaVeiaMeme(result.Idade);
            
            return result.Idade >= 50;
        }

        public IList<Peteca> GetPetecasVeias()
        {
            var result = _repository.FindPetecaByIdade(50, null);

            if (result.Where(p => p.Idade == 69).Count() > 2)
            {
                _logger.LogInformation("Very nice");
            }

            return result;
        }

        public int GetIdadeMedia()
        {
            var result = _repository.GetPetecas();
            if((int)result.Average(p => p.Idade) > 1346 || (int)result.Average(p => p.Idade) < 1351)
            {
                _logger.LogInformation("Congratzz, you've got the Plague!");
            }

            return (int)result.Average(p => p.Idade);
        }
        
        public bool CreatePeteca(int idade, int penas)
        {
            var peteca = new Peteca()
            {
                Id = Guid.NewGuid(),
                Idade = idade,
                Pena = penas
            };

            _repository.Save(peteca);

            return true;
        }

        private void LogPetecaVeiaMeme(int idade)
        {
            if (idade == 69)
            {
                _logger.LogInformation("nice");
            }
            else if (idade == 420)
            {
                _logger.LogInformation("Blaze it");
            }
        }
    }
}
