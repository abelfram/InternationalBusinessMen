using Domain.DomainEntity;
using System.Globalization;

namespace Domain.DomainServices
{
    public class CurrencyConverterTools
    {

        public List<TransactionDomainEntity> ConvertRateToEUR(List<TransactionDomainEntity> transactionsBySKU, List<RateDomainEntity> conversionRates)
        {
           

            var transactionsInEUR = new List<TransactionDomainEntity>();

            foreach (var transaction in transactionsBySKU)
            {
                if (transaction.currency == "EUR")
                {
                    transactionsInEUR.Add(transaction);
                }
                else
                {
                    var convertedTransaction = ConvertToEUR(transaction, conversionRates);
                    if (convertedTransaction != null)
                    {
                        transactionsInEUR.Add(convertedTransaction);
                    }
                }
            }

            return transactionsInEUR;
        }

        private TransactionDomainEntity ConvertToEUR(TransactionDomainEntity transaction, List<RateDomainEntity> conversionRates)
        {
            if (transaction.currency == "EUR")
            {
                return transaction;
            }

            var conversionPath = FindConversionPath(transaction.currency, conversionRates, new List<RateDomainEntity>());
            if (conversionPath != null)
            {
                decimal convertedAmount = transaction.amount;

                foreach (var rate in conversionPath)
                {
                    convertedAmount *= rate.Rate;
                    transaction.currency = rate.To;
                }

                convertedAmount = Math.Round(convertedAmount, 2);

                return new TransactionDomainEntity
                {
                    sku = transaction.sku,
                    amount = convertedAmount,
                    currency = "EUR"
                };
            }
            return null;
        }

        private List<RateDomainEntity> FindConversionPath(string sourceCurrency, List<RateDomainEntity> conversionRates, List<RateDomainEntity> currentPath)
        {
            if (sourceCurrency == "EUR")
            {
                return currentPath;
            }

            var validRates = conversionRates.Where(rate => rate.From == sourceCurrency).ToList();

            foreach (var rate in validRates)
            {
                var newPath = new List<RateDomainEntity>(currentPath);
                newPath.Add(rate);

                var path = FindConversionPath(rate.To, conversionRates, newPath);
                if (path != null)
                {
                    return path;
                }
            }
            return null;
        }
    }
}

