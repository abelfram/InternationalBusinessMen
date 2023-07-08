using Domain.DomainEntity;

namespace Domain.RepositoryContracts
{
    public interface ITransactionRepository
    {
        List<TransactionDomainEntity> GetTransactions();
    }
}
