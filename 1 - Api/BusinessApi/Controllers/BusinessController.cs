using Business.ServiceContracts;
using Domain.Agregates;
using Domain.DomainEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BusinessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly ITransactionsService _listTransactions;
        private readonly ILogger<BusinessController> _logger;

        public BusinessController(ITransactionsService listTransactions, ILogger<BusinessController> logger)
        {
            _listTransactions = listTransactions;
            _logger = logger;
        }


        [HttpGet]
        [Route("GetAllTransactions")]
        public async Task<List<TransactionDomainEntity>> GetAllTransactions()
        {

            try
            {
                var transactions = await _listTransactions.GetAllTransactions();
                return transactions;
            }
            catch (Exception)
            {

                _logger.LogError("Ha ocurrido algun error en la ejecucion del programa");
                throw;
            }
        }

        [HttpGet]
        [Route("GetAllRates")]
        public async Task<List<RateDomainEntity>> GetRates()
        {
            try
            {
                var transactions = await _listTransactions.GetAllRates();
                return transactions;
            }
            catch (Exception)
            {
                _logger.LogError("Ha ocurrido algun error en la ejecucion del programa");
                throw;
            }
        }

        [HttpGet]
        [Route("GetElementsBySKUInEUR")]
        public async Task<IActionResult> GetElementsBySKU([Required] string sku)
        {
            string pattern = @"^[A-Z]\d{4}$";
            if (Regex.IsMatch(sku, pattern))
            {
                try
                {
                    var transactions = await _listTransactions.GetElementsBySKU(sku);
                    _logger.LogInformation("Programa ejecutado correctamente");

                    //billAgregate
                    return Ok(transactions);
                   
                }
                catch (Exception)
                {
                    _logger.LogError("Ha ocurrido algun error en la ejecucion del programa");
                    throw;
                }
            }
            return null;
        }
    }
}
