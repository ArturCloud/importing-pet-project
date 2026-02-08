using System.Data;
using System.Text;
using DataImportProj.Database;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace DataImportProj.Services.ImportServices
{
    public class ImportProductTranslationsService(DbAppContext _db, ILogger<ImportProductTranslationsService> _logger) : IImportProductTranslationsService
    {
        public async Task<List<ProductTranslation>> GetProductTranslationsOld(MySqlConnection conn)
        {
            List<ProductTranslation> list = new List<ProductTranslation>();
            using var comm = new MySqlCommand($"SELECT * FROM product_translations", conn);
            using var reader = await comm.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var newProductT = new ProductTranslation()
                {
                    ProductId = reader.GetInt32("product_id"),
                    LanguageCode = reader.GetString("language_code"),
                    Name = !reader.IsDBNull("name") ? reader.GetString("name") : string.Empty,
                    CreatedAt = reader.GetDateTime("created_at")
                };

                list.Add(newProductT);
                _logger.LogDebug($"A record was received from the old database – ProductId: {newProductT.ProductId} and LanguageCode: {newProductT.LanguageCode}");
            }
            return list;
        }

        public async Task SaveProductTranslations(List<ProductTranslation> productTranslationsOld, StringBuilder sbEmailLogs)
        {
            var list = new List<ProductTranslation>();
            var productTranslations = await _db.ProductTranslations.ToListAsync();

            foreach (var productTOld in productTranslationsOld)
            {
                var isProductTExists = productTranslations.Any(x => x.ProductId == productTOld.ProductId && x.LanguageCode == productTOld.LanguageCode);
                if (!isProductTExists)
                {
                    _logger.LogDebug($"A record with ProductId {productTOld.ProductId} and LanguageCode {productTOld.LanguageCode} does not exist and will be added to the database");
                    list.Add(productTOld);
                }
            }

            if (list.Count > 0)
            {
                await _db.ProductTranslations.AddRangeAsync(list);
            }
            await _db.SaveChangesAsync();
            _logger.LogInformation($"A total of {list.Count} records were added to the ProductTranslations table");

            sbEmailLogs.AppendLine($"<p>Was added {list.Count} ProductCustomer items</p>");
        }
    }
}
