using Domain.DomainEntity;

namespace Domain.RepositoryContracts
{
    public interface ITransactionRepository
    {
        List<TransactionDomainEntity> GetAll();
        List<TransactionDomainEntity> GetElementsBySku(string sku);
    }
}
