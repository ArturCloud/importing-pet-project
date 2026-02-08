using System.Text;
using DataImportProj.Services.ImportServices;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace DataImportProj.Controllers
{
    public class ImportController(
        IConfiguration _conf, ILogger<ImportController> _logger, IImportCustomersService _importCustomersService, 
        IImportProductsService _importProductsService, IImportProductCustomersService _importProductCustomersService, 
        IImportProductTranslationsService _importProductTranslationsService, IEmailService _emailService) : Controller
    {
        [HttpPost("Import")]
        public async Task<IActionResult> Importing()
        {
            var _connValue = _conf.GetSection("LegacyDBConnectionStrings").Value;
            if (string.IsNullOrWhiteSpace(_connValue))
            {
                _logger.LogError("Configuration data for import was not found, please check config");
                return Content("Configuration data for import was not found, please check config");
            }

            try
            {
                StringBuilder sbEmailLogs = new StringBuilder();

                using var conn = new MySqlConnection(_connValue);
                await conn.OpenAsync();

                _logger.LogInformation("Import is starting...");

                _logger.LogInformation(" * Importing Products table - retrieving data from the legacy database: *");
                var productsOld = await _importProductsService.GetProductsOld(conn);
                _logger.LogInformation(" * Saving to the new database has started * ");
                await _importProductsService.SaveProducts(productsOld, sbEmailLogs);
                _logger.LogInformation(" * Products import has been completed * ");


                _logger.LogInformation(" * Importing Customers table - retrieving data from the legacy database: *");
                var customerssOld = await _importCustomersService.GetCustomersOld(conn);
                _logger.LogInformation(" * Saving to the new database has started * ");
                await _importCustomersService.SaveCustomers(customerssOld, sbEmailLogs);
                _logger.LogInformation(" * Customers import has been completed * ");


                _logger.LogInformation(" * Importing ProductCustomers table - retrieving data from the legacy database: *");
                var productCustomersOld = await _importProductCustomersService.GetProductCustomersOld(conn);
                _logger.LogInformation(" * Saving to the new database has started * ");
                await _importProductCustomersService.SaveProductCustomers(productCustomersOld, sbEmailLogs);
                _logger.LogInformation(" * ProductCustomers import has been completed * ");


                _logger.LogInformation(" * Importing ProductTranslations table - retrieving data from the legacy database: *");
                var productTranslationsOld = await _importProductTranslationsService.GetProductTranslationsOld(conn);
                _logger.LogInformation(" * Saving to the new database has started * ");
                await _importProductTranslationsService.SaveProductTranslations(productTranslationsOld, sbEmailLogs);
                _logger.LogInformation(" * ProductTranslations import has been completed * ");

                _logger.LogInformation("...Import completed");
                await conn.CloseAsync();

                await _emailService.SendEmailAsync(success: true, null, sbEmailLogs.ToString());

                return Content("Import completed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the import");
                await _emailService.SendEmailAsync(success: false, $"{ex.Message} - {ex.InnerException?.Message}");
                return Content("An error occurred during the import");
            }
        }
    }
}
