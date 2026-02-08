
namespace DataImportProj.Services.ImportServices
{
    public interface IEmailService
    {
        /// <summary>
        /// Email sending
        /// </summary>
        /// <param name="success"></param>
        /// <param name="exMessage"></param>
        /// <param name="impResult"></param>
        /// <returns></returns>
        Task SendEmailAsync(bool success, string? exMessage = null, string? impResult = null);
    }
}