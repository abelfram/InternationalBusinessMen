using Domain.DomainEntity;
using Domain.RepositoryContracts;
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
        public List<TransactionDomainEntity> GetTransactions()
        {
            string textFromJson = File.ReadAllText(JsonPath);

            textFromJson = textFromJson.Replace(".", ",");

            List<TransactionsEntity> deserializedInfoFromJson = JsonSerializer.Deserialize<List<TransactionsEntity>>(textFromJson);

            List<TransactionDomainEntity> transactionDomainEntities = new List<TransactionDomainEntity>();

            foreach (var transaction in deserializedInfoFromJson)
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
