using Domain.DomainEntity;
using Domain.RepositoryContracts;
using Infrastructure.Data.DataEntity;
using System.Globalization;
using System.Text.Json;
using static Infrastructure.Data.DataEntity.RateDataEntity;

namespace Infrastructure.Data.RepositoryImplementation
{

    public class RatesRepository : IRatesRepository
    {
        private const string JsonPath = ".\\JSon\\Currency.Json";
        
        public List<RateDomainEntity> GetAll()
        {
            string textFromJson = File.ReadAllText(JsonPath);

            textFromJson = textFromJson.Replace(".", ",");

            List<RateDataEntity> deserializedInfoFromJson = JsonSerializer.Deserialize<List<RateDataEntity>>(textFromJson);

            List<RateDomainEntity> rateDomainEntities = new List<RateDomainEntity>();

            foreach (var transaction in deserializedInfoFromJson)
            {
                RateDomainEntity entity = new RateDomainEntity();
                entity.From = transaction.From;
                entity.To = transaction.To;
                entity.Rate = decimal.Parse(transaction.Rate);
                rateDomainEntities.Add(entity);
            }

            return rateDomainEntities;
            
        }
    }
}
