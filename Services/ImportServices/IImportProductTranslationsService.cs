using System.Text;
using DataImportProj.Database;
using MySqlConnector;

namespace DataImportProj.Services.ImportServices
{
    public interface IImportProductTranslationsService
    {
        /// <summary>
        /// Get producttranslations from old db
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        Task<List<ProductTranslation>> GetProductTranslationsOld(MySqlConnection conn);

        /// <summary>
        /// Saving in new db
        /// </summary>
        /// <param name="productTranslationsOld"></param>
        /// <returns></returns>
        Task SaveProductTranslations(List<ProductTranslation> productTranslationsOld, StringBuilder sb);
    }
}