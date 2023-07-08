using Domain.DomainEntity;
using Domain.RepositoryContracts;
using Infrastructure.Data.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Infrastructure.Data.DataEntity.RateDataEntity;
using static Infrastructure.Data.DataEntity.TransactionDataEntity;

namespace Infrastructure.Data.RepositoryImplementation
{
    public class TransactionRepository : ITransactionRepository
    {
        private const string JsonPath = ".\\JSon\\Transactions.Json";
        public List<TransactionDomainEntity> GetAll()
        {
            List<TransactionDataEntity> deserializedInfoFromJson = DeserializeJson();

            List<TransactionDomainEntity> result = FromDataEntityToDomainEntity(deserializedInfoFromJson);

            return result;
        }

        public List<TransactionDomainEntity> GetElementsBySku(string sku)
        {
            List<TransactionDataEntity> deserializedInfoFromJson = DeserializeJson();
            List<TransactionDataEntity> resultDataEntity = deserializedInfoFromJson.Where(transaction => transaction.sku == sku).ToList();

            List<TransactionDomainEntity> result = FromDataEntityToDomainEntity(resultDataEntity);

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
