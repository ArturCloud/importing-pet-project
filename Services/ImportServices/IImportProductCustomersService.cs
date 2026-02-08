using System.Text;
using DataImportProj.Database;
using MySqlConnector;

namespace DataImportProj.Services.ImportServices
{
    public interface IImportProductCustomersService
    {
        /// <summary>
        /// Get productcustomers from old db
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        Task<List<ProductCustomer>> GetProductCustomersOld(MySqlConnection conn);

        /// <summary>
        /// Saving in new db
        /// </summary>
        /// <param name="productCustomersOld"></param>
        /// <returns></returns>
        Task SaveProductCustomers(List<ProductCustomer> productCustomersOld, StringBuilder sb);
    }
}