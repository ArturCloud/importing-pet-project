using System.Data;
using System.Text;
using DataImportProj.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySqlConnector;

namespace DataImportProj.Services.ImportServices
{
    public class ImportProductCustomersService(DbAppContext _db, ILogger<ImportProductCustomersService> _logger) : IImportProductCustomersService
    {
        public async Task<List<ProductCustomer>> GetProductCustomersOld(MySqlConnection conn)
        {
            List<ProductCustomer> list = new List<ProductCustomer>();
            using var comm = new MySqlCommand($"SELECT * FROM product_customers", conn);
            using var reader = await comm.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var newProductCustomer = new ProductCustomer()
                {
                    ProductId = reader.GetInt32("product_id"),
                    CustomerId = reader.GetInt32("customer_id"),
                    CreatedAt = reader.GetDateTime("created_at")
                };

                list.Add(newProductCustomer);
                _logger.LogDebug($"A record was received from the old database – ProductId: {newProductCustomer.ProductId} and CustomerId: {newProductCustomer.CustomerId}");
            }
            return list;
        }

        public async Task SaveProductCustomers(List<ProductCustomer> productCustomersOld, StringBuilder sbEmailLogs)
        {
            var list = new List<ProductCustomer>();
            var productCustomers = await _db.ProductCustomers.AsNoTracking().ToListAsync();

            foreach (var productCustomerOld in productCustomersOld)
            {
                var isPCExists = productCustomers.Any(x => x.ProductId == productCustomerOld.ProductId && x.CustomerId == productCustomerOld.CustomerId);
                if (!isPCExists)
                {
                    _logger.LogDebug($"A record with ProductId {productCustomerOld.ProductId} and CustomerId {productCustomerOld.CustomerId} does not exist and will be added to the database");
                    list.Add(productCustomerOld);
                }
            }

            if (list.Count > 0)
            {
                await _db.ProductCustomers.AddRangeAsync(list);
            }
            await _db.SaveChangesAsync();
            _logger.LogInformation($"A total of {list.Count} records were added to the ProductCustomers table");

            sbEmailLogs.AppendLine($"<p>Was added {list.Count} ProductCustomer items</p>");
        }
    }
}
