using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetecaAPIFinalForm
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

                LogPetecaVeiaMeme(result.Idade);

                return result.Idade >= 50;
            }
            catch(ArgumentException)
            {
                throw new ArgumentException();
            }
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

            if(result.Average(p => p.Idade) > 1346)
            {
                _logger.LogInformation("Congratzz, you've got the Plague!");
            }

            return (int)result.Average(p => p.Idade);
        }
        
        public Peteca CreatePeteca(int idade, int penas)
        {
            var peteca = Peteca.Factory.Create(idade, penas);

            try
            {
                _repository.Save(peteca);

                return peteca;
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException();
            }
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
