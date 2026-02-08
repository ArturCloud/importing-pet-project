using System.Data;
using System.Text;
using DataImportProj.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySqlConnector;

namespace DataImportProj.Services.ImportServices
{
    public class ImportProductsService(DbAppContext _db, ILogger<ImportProductsService> _logger) : IImportProductsService
    {
        public async Task<List<Product>> GetProductsOld(MySqlConnection conn)
        {
            List<Product> list = new List<Product>();
            using var comm = new MySqlCommand($"SELECT * FROM products", conn);
            using var reader = await comm.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var newProduct = new Product()
                {
                    Country = reader.GetString("country"),
                    CreatedAt = reader.GetDateTime("created_at"),
                    Price = reader.GetDecimal("price"),
                    BestBefore = !reader.IsDBNull("best_before") ? reader.GetDateOnly("best_before") : DateOnly.MinValue,
                    Deleted = reader.GetBoolean("deleted"),
                    OldId = reader.GetInt32("id")
                };

                list.Add(newProduct);
                _logger.LogDebug($"A record was received from the old database – old ID: {newProduct.OldId}");
            }
            return list;
        }

        public async Task SaveProducts(List<Product> productsOld, StringBuilder sbEmailLogs)
        {
            var list = new List<Product>();
            var products = await _db.Products.AsNoTracking().ToListAsync();

            foreach (var productOld in productsOld)
            {
                var isCProductExists = products.Any(x => x.OldId == productOld.OldId && x.OldId != null);
                if (!isCProductExists)
                {
                    _logger.LogDebug($"A record with OldId {productOld.OldId} does not exist and will be added to the database");
                    list.Add(productOld);
                }
            }

            if (list.Count > 0)
            {
                await _db.Products.AddRangeAsync(list);
            }
            await _db.SaveChangesAsync();
            _logger.LogInformation($"A total of {list.Count} records were added to the Products table");

            sbEmailLogs.AppendLine($"<p>Was added {list.Count} Product items</p>");
        }
    }
}
