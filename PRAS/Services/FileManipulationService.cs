using PRAS.Contracts;

namespace PRAS.Services
{
    public class FileManipulationService : IFileManipulationService
    {
        public string SaveImageOnDisk(string newsId, string base64String, string fileName)
        {
            var imageName = newsId.ToString() + fileName;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", imageName);

            var bytes = Convert.FromBase64String(base64String);

            using var stream = File.Create(path);
            stream.Write(bytes);

            return imageName;
        }

        public void RemoveOldImage(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);

            File.Delete(path);
        }
    }
}
