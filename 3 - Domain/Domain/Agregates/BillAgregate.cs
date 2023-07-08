using Domain.DomainEntity;

namespace Domain.Agregates
{
    public class BillAgregate
    {
        public List<TransactionDomainEntity> ListOfTransactions { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal GetBillDomainEntity (List<TransactionDomainEntity> TransactionsInEUR)
        { 
            decimal totalAmount = 0;

            foreach (var transaction in TransactionsInEUR)
            {
                totalAmount += transaction.amount;
            }

            return totalAmount;
        }
    }
}
