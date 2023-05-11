namespace WebLibrary.API.Services
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile imageFile);
        Task<bool> DeleteImageAsync(string fileName);
    }
}