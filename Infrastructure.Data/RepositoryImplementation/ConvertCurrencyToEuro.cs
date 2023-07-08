﻿using Domain.DomainEntity;
using Domain.RepositoryContracts;
using Infrastructure.Data.DataEntity;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using static Infrastructure.Data.DataEntity.RateDataEntity;

namespace Infrastructure.Data.RepositoryImplementation
{
    public class ConvertCurrencyToEuro : IConvertCurrencyToEuro
    {
        private const string ConversionRatesJsonPath = "./Json/Currency.json";

        public List<TransactionDomainEntity> ConvertCurrencyToEUR(List<TransactionDomainEntity> transactionsBySKU)
        {
            string jsonText = File.ReadAllText(ConversionRatesJsonPath);
            var conversionRates = JsonSerializer.Deserialize<List<RateEntity>>(jsonText);

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

        private TransactionDomainEntity ConvertToEUR(TransactionDomainEntity transaction, List<RateEntity> conversionRates)
        {
            if (transaction.currency == "EUR")
            {
                return transaction;
            }

            var conversionPath = FindConversionPath(transaction.currency, conversionRates, new List<RateEntity>());
            if (conversionPath != null)
            {
                decimal convertedAmount = transaction.amount;

                foreach (var rate in conversionPath)
                {
                    convertedAmount *= decimal.Parse(rate.Rate, CultureInfo.InvariantCulture);
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

        private List<RateEntity> FindConversionPath(string sourceCurrency, List<RateEntity> conversionRates, List<RateEntity> currentPath)
        {
            if (sourceCurrency == "EUR")
            {
                return currentPath;
            }

            var validRates = conversionRates.Where(rate => rate.From == sourceCurrency).ToList();

            foreach (var rate in validRates)
            {
                var newPath = new List<RateEntity>(currentPath);
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