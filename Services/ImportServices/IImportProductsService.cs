using System.Text;
using DataImportProj.Database;
using MySqlConnector;

namespace DataImportProj.Services.ImportServices
{
    public interface IImportProductsService
    {
        /// <summary>
        /// Get products from old db
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        Task<List<Product>> GetProductsOld(MySqlConnection conn);

        /// <summary>
        /// Saving in new db
        /// </summary>
        /// <param name="productsOld"></param>
        /// <returns></returns>
        Task SaveProducts(List<Product> productsOld, StringBuilder sb);
    }
}