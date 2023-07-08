using Domain.DomainEntity;

namespace Domain.Agregates
{
    public class BillAgregate
    {
        public List<TransactionDomainEntity> ListOfTransactions { get; set; }

        public decimal TotalAmount { get; set; }

        public void CalculateBillTotal ()
        {
            TotalAmount = 0;

            TotalAmount = ListOfTransactions.Sum(x => x.amount);
            
        }
    }
}
