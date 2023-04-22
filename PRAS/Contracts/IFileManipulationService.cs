namespace PRAS.Contracts
{
    public interface IFileManipulationService
    {
        string SaveImageOnDisk(string newsId, string base64String, string fileName);
        void RemoveOldImage(string fileName);
    }
}
