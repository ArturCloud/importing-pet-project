using System.Text;
using DataImportProj.Database;
using MySqlConnector;

namespace DataImportProj.Services.ImportServices
{
    public interface IImportCustomersService
    {
        /// <summary>
        /// Get customers from old db
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        Task<List<Customer>> GetCustomersOld(MySqlConnection conn);

        /// <summary>
        /// Saving in new db
        /// </summary>
        /// <param name="customersOld"></param>
        /// <returns></returns>
        Task SaveCustomers(List<Customer> customersOld, StringBuilder sb);
    }
}