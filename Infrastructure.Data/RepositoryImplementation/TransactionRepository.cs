using Domain.DomainEntity;
using Domain.RepositoryContracts;
using Infrastructure.Data.DataEntity;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace Infrastructure.Data.RepositoryImplementation
{
    public class TransactionRepository : ITransactionRepository
    {
        private const string JsonPath = ".\\JSon\\Transactions.Json";
        private readonly IMemoryCache _cache;

        public TransactionRepository(IMemoryCache cache)
        {
            _cache = cache;
        }
        public List<TransactionDomainEntity> GetAll()
        {
            if (_cache.TryGetValue("TransactionsCache", out List<TransactionDomainEntity> transactions))
            {
                return transactions;
            }
            List<TransactionDataEntity> deserializedInfoFromJson = DeserializeJson();

            List<TransactionDomainEntity> result = FromDataEntityToDomainEntity(deserializedInfoFromJson);

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(1));
            _cache.Set("TransactionsCache", result, cacheOptions);

            return result;
        }

        public List<TransactionDomainEntity> GetElementsBySku(string sku)
        {
            if (_cache.TryGetValue($"Transaction{sku}Cache", out List<TransactionDomainEntity> transactions))
            {
                return transactions;
            }
            List<TransactionDataEntity> deserializedInfoFromJson = DeserializeJson();
            List<TransactionDataEntity> resultDataEntity = deserializedInfoFromJson.Where(transaction => transaction.sku == sku).ToList();
            List<TransactionDomainEntity> result = FromDataEntityToDomainEntity(resultDataEntity);

            var cacheOptions = new MemoryCacheEntryOptions().
                SetSlidingExpiration(TimeSpan.FromHours(1));

            _cache.Set($"Transaction{sku}Cache", result, cacheOptions);
            return result;


        }

        private List<TransactionDataEntity> DeserializeJson()
        {
            string textFromJson = File.ReadAllText(JsonPath);

            textFromJson = textFromJson.Replace(".", ",");

            List<TransactionDataEntity> deserializedInfoFromJson = JsonSerializer.Deserialize<List<TransactionDataEntity>>(textFromJson);

            return deserializedInfoFromJson;
        }

        private List<TransactionDomainEntity> FromDataEntityToDomainEntity(List<TransactionDataEntity> transactionsDataEntity) 
        {
            List<TransactionDomainEntity> transactionDomainEntities = new();

            foreach (var transaction in transactionsDataEntity)
            {
                TransactionDomainEntity entity = new TransactionDomainEntity();
                entity.sku = transaction.sku;
                entity.amount = decimal.Parse(transaction.amount);
                entity.currency = transaction.currency;
                transactionDomainEntities.Add(entity);
            }

            return transactionDomainEntities;
        }

    }
}
