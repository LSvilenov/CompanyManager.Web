using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CompanyManager.Services.Contracts
{
    public interface IFileService
    {
        Task<string> SaveFileToFolderAsync(IFormFile file, string folder, string subFolder);
    }
}
