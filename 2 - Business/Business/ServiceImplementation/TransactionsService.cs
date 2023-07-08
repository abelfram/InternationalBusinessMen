using Business.ServiceContracts;
using Domain.Agregates;
using Domain.DomainEntity;
using Domain.RepositoryContracts;

namespace Business.ServiceImplementation
{
    public class TransactionsService : ITransactionsService
    {
        private readonly IRatesRepository _ratesRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IConvertCurrencyToEuro _convertToEUR;
        public TransactionsService(IRatesRepository conversorRepository, ITransactionRepository transactionRepository, IConvertCurrencyToEuro convertToEUR)
        {
            _ratesRepository = conversorRepository;
            _transactionRepository = transactionRepository;
            _convertToEUR = convertToEUR;

        }
        public async Task<List<TransactionDomainEntity>> GetAllTransactions()
        {
            List<TransactionDomainEntity> transactions = _transactionRepository.GetAll();
            return transactions;
        }

        public async Task<List<RateDomainEntity>> GetAllRates()
        {
            List<RateDomainEntity> transactions = _ratesRepository.GetAll();
            return transactions;
        }

        public async Task<BillAgregate> GetElementsBySKU(string sku)
        {

            List<TransactionDomainEntity> transactionsBySKU = _transactionRepository.GetElementsBySku(sku);

            List<TransactionDomainEntity> transactionsBySKUInEUR = _convertToEUR.ConvertRateToEUR(transactionsBySKU);

            BillAgregate bill = new BillAgregate()
            {
                ListOfTransactions = transactionsBySKUInEUR,
            };
            bill.CalculateBillTotal();

            return bill;
        }
    }
}
