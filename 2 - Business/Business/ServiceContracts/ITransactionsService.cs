using Domain.Agregates;
using Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ServiceContracts
{
    public interface ITransactionsService
    {
        public Task<List<TransactionDomainEntity>> GetAllTransactions();
        public Task<List<RateDomainEntity>> GetAllRates();
        public Task<BillAgregate> GetElementsBySKU(string sku);
    }
}
