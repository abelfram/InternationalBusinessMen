using Domain.DomainEntity;
using Domain.RepositoryContracts;

namespace Infrastructure.Data.RepositoryImplementation
{
    public class GetElemetnsBySKURepository : IGetElemetnsBySKURepository
    {
        

        public List<TransactionDomainEntity> GetElementsBySku(List<TransactionDomainEntity> transactions, string sku)
        {
            List<TransactionDomainEntity> result = transactions.Where(transaction => transaction.sku == sku).ToList();

            return result;
        }
    }
}
