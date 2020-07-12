using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using CompanyManager.Services.Contracts;

namespace CompanyManager.Services
{
    public class FileService : IFileService
    {
        public async Task<string> SaveFileToFolderAsync(IFormFile file, string folder, string subFolder)
        {
            string dbPath = string.Empty;
            if (file.Length > 0)
            {
                var folderName = Path.Combine(folder, subFolder);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                dbPath = Path.Combine(folderName, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return dbPath;
        }
    }
}
