using Business.ServiceContracts;
using Domain.Agregates;
using Domain.DomainEntity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BusinessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly IListTransactionsService _listTransactions;

        public BusinessController(IListTransactionsService listTransactions)
        {
            _listTransactions = listTransactions;
        }


        [HttpGet]
        [Route("GetAllTransactions")]
        public async Task<List<TransactionDomainEntity>> GetAllTransactions()
        {
            var transactions = await _listTransactions.ListTransactions();
            return transactions;
        }

        [HttpGet]
        [Route("GetAllRates")]
        public async Task<List<RateDomainEntity>> GetRates()
        {
            var transactions = await _listTransactions.ListRates();
            return transactions;
        }

        [HttpGet]
        [Route("GetElementsBySKUInEUR")]
        public async Task<BillAgregate> GetElementsBySKU([Required] string sku)
        {
            string pattern = @"^[A-Z]\d{4}$";
            if (Regex.IsMatch(sku, pattern))
            {
                var transactions = await _listTransactions.GetElementsBySKU(sku);
                return transactions;
            }
            return null;
        }
    }
}
