using Domain.Agregates;
using Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ServiceContracts
{
    public interface IListTransactionsService
    {
        public Task<List<TransactionDomainEntity>> ListTransactions();
        public Task<List<RateDomainEntity>> ListRates();
        public Task<BillAgregate> GetElementsBySKU(string sku);
    }
}
