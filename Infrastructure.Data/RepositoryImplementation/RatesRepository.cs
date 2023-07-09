using Domain.DomainEntity;
using Domain.RepositoryContracts;
using Infrastructure.Data.DataEntity;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;
using System.Text.Json;
using static Infrastructure.Data.DataEntity.RateDataEntity;

namespace Infrastructure.Data.RepositoryImplementation
{

    public class RatesRepository : IRatesRepository
    {
        private const string JsonPath = ".\\JSon\\Currency.Json";
        private readonly IMemoryCache _cache;

        public RatesRepository(IMemoryCache cache)
        {
            _cache = cache;
        }
    
        public List<RateDomainEntity> GetAll()
        {
            if (_cache.TryGetValue("RateCache", out List<RateDomainEntity> AllRates))
            {
                return AllRates;
            }

            List<RateDataEntity> deserializedInfoFromJson = deserializeInfoFromJson(JsonPath);
            List<RateDomainEntity> rateDomainEntities = FromDataEntityToDomainEntity(deserializedInfoFromJson);

            var cacheOptions = new MemoryCacheEntryOptions().
                SetSlidingExpiration(TimeSpan.FromHours(1));

            _cache.Set("RateCache", rateDomainEntities, cacheOptions);
            return rateDomainEntities;
        }

        private List<RateDataEntity> deserializeInfoFromJson(string JsonPath)
        {
            string textFromJson = File.ReadAllText(JsonPath);

            textFromJson = textFromJson.Replace(".", ",");

            List<RateDataEntity> deserializedInfoFromJson = JsonSerializer.Deserialize<List<RateDataEntity>>(textFromJson);

            return deserializedInfoFromJson;
        }

        private List<RateDomainEntity> FromDataEntityToDomainEntity(List<RateDataEntity> deserializedInfoFromJson)
        {
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
