namespace WebLibrary.API.Services
{
    public class ImageService : IImageService
    {
        private readonly string _imageFolder;

        public ImageService(IConfiguration configuration)
        {
            _imageFolder = configuration["ImageFolder"];
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            var fileName = $"{Guid.NewGuid()}_{imageFile.FileName}";
            var filePath = Path.Combine(_imageFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return fileName;
        }

        public async Task<bool> DeleteImageAsync(string fileName)
        {
            var filePath = Path.Combine(_imageFolder, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }

            return false;
        }
    }
}
