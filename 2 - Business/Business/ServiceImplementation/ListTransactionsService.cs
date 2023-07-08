using Business.ServiceContracts;
using Domain.Agregates;
using Domain.DomainEntity;
using Domain.RepositoryContracts;

namespace Business.ServiceImplementation
{
    public class ListTransactionsService : IListTransactionsService
    {
        private readonly IRatesRepository _ratesRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IGetElemetnsBySKURepository _getElementsBySKURepository;
        private readonly IConvertCurrencyToEuro _convertToEUR;
        public ListTransactionsService(IRatesRepository conversorRepository, ITransactionRepository transactionRepository, IGetElemetnsBySKURepository getElementsBySKU, IConvertCurrencyToEuro convertToEUR)
        {
            _ratesRepository = conversorRepository;
            _transactionRepository = transactionRepository;
            _getElementsBySKURepository = getElementsBySKU;
            _convertToEUR = convertToEUR;

        }
        public async Task<List<TransactionDomainEntity>> ListTransactions()
        {
            List<TransactionDomainEntity> transactions = _transactionRepository.GetTransactions();
            return transactions;
        }

        public async Task<List<RateDomainEntity>> ListRates()
        {
            List<RateDomainEntity> transactions = _ratesRepository.rateDomainEntity();
            return transactions;
        }

        public async Task<BillAgregate> GetElementsBySKU(string sku)
        {
            List<TransactionDomainEntity> transactions = _transactionRepository.GetTransactions();

            List<TransactionDomainEntity> transactionsBySKU = _getElementsBySKURepository.GetElementsBySku(transactions, sku);

            List<TransactionDomainEntity> transactionsBySKUInEUR = _convertToEUR.ConvertCurrencyToEUR(transactionsBySKU);

            BillAgregate bill = new BillAgregate();
            bill = new BillAgregate()
            {
                ListOfTransactions = transactionsBySKUInEUR,
                TotalAmount = bill.GetBillDomainEntity(transactionsBySKUInEUR)
            };
            return bill;
        }
    }
}
