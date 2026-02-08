using System.Data;
using System.Text;
using DataImportProj.Database;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace DataImportProj.Services.ImportServices
{
    public class ImportCustomersService(DbAppContext _db, ILogger<ImportCustomersService> _logger) : IImportCustomersService
    {
        public async Task<List<Customer>> GetCustomersOld(MySqlConnection conn)
        {
            List<Customer> list = new List<Customer>();
            using var comm = new MySqlCommand($"SELECT * FROM customers", conn);
            using var reader = await comm.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var newCustomer = new Customer()
                {
                    Name = reader.GetString("name"),
                    Surname = reader.GetString("surname"),
                    Email = reader.GetString("email"),
                    Age = !reader.IsDBNull("age") ? reader.GetInt32("age") : null,
                    Phone = reader.GetString("phone"),
                    CreatedAt = reader.GetDateTime("created_at"),
                    Deleted = reader.GetBoolean("deleted"),
                    OldId = reader.GetInt32("id")
                };

                list.Add(newCustomer);
                _logger.LogDebug($"A record was received from the old database – old ID: {newCustomer.OldId}");
            }
            return list;
        }

        public async Task SaveCustomers(List<Customer> customersOld, StringBuilder sbEmailLogs)
        {
            var list = new List<Customer>();
            var customers = await _db.Customers.AsNoTracking().ToListAsync();

            foreach (var customerOld in customersOld)
            {
                var isCustomerExists = customers.Any(x => x.OldId == customerOld.OldId && x.OldId != null);
                if (!isCustomerExists)
                {
                    _logger.LogDebug($"A record with OldId {customerOld.Id} does not exist and will be added to the database");
                    list.Add(customerOld);
                }
            }

            if (list.Count > 0)
            {
                await _db.Customers.AddRangeAsync(list);
            }
            await _db.SaveChangesAsync();
            _logger.LogInformation($"A total of {list.Count} records were added to the Customers table");

            sbEmailLogs.AppendLine($"<p>Was added {list.Count} Customer items</p>");
        }
    }
}
