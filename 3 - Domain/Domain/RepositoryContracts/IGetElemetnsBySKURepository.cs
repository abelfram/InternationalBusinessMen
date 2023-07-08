using Domain.DomainEntity;

namespace Domain.RepositoryContracts
{
    public interface IGetElemetnsBySKURepository
    {
        public List<TransactionDomainEntity> GetElementsBySku(List<TransactionDomainEntity> transactions, string sku);
    }
}