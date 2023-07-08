using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.DataEntity
{

    public class TransactionDataEntity
    {
        public string sku { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
    }
    
}
