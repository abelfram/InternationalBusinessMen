using Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Agregates
{
    public class FilterBySkuAgregate
    {
        public List<TransactionDomainEntity> ElementsBySKU { get; set; }
        public List<TransactionDomainEntity> GetElementsBySku(List<TransactionDomainEntity> transactions, string sku)
        {
            List<TransactionDomainEntity> result = transactions.Where(transaction => transaction.sku == sku).ToList();

            return result;
        }
    }
}

