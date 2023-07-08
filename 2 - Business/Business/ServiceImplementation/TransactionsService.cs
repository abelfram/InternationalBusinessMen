using Business.ServiceContracts;
using Domain.Agregates;
using Domain.DomainEntity;
using Domain.DomainServices;
using Domain.RepositoryContracts;

namespace Business.ServiceImplementation
{
    public class TransactionsService : ITransactionsService
    {
        private readonly IRatesRepository _ratesRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionsService(IRatesRepository conversorRepository, ITransactionRepository transactionRepository)
        {
            _ratesRepository = conversorRepository;
            _transactionRepository = transactionRepository;
            

        }
        public async Task<List<TransactionDomainEntity>> GetAllTransactions()
        {
            List<TransactionDomainEntity> transactions = _transactionRepository.GetAll();
            return transactions;
        }

        public async Task<List<RateDomainEntity>> GetAllRates()
        {
            List<RateDomainEntity> rates = _ratesRepository.GetAll();
            return rates;
        }

        public async Task<BillAgregate> GetElementsBySKU(string sku)
        {

            List<TransactionDomainEntity> transactionsBySKU = _transactionRepository.GetElementsBySku(sku);
            List<RateDomainEntity> rates = _ratesRepository.GetAll();
            CurrencyConverterTools currencyConverterTools = new();
            List<TransactionDomainEntity> transactionsBySKUInEUR = currencyConverterTools.ConvertRateToEUR(transactionsBySKU, rates);

            BillAgregate bill = new()
            {
                ListOfTransactions = transactionsBySKUInEUR,
            };
            bill.CalculateBillTotal();

            return bill;
        }
    }
}
