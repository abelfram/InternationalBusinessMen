using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainEntity
{
    public class TransactionDomainEntity
    {

        public string sku { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }
    }
}
