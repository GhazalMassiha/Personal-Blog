namespace Personal_Blog.EndPoint.MVC.Services
{
    public interface IImageService
    {
        (bool Success, string? RelativePath, string? ErrorMessage) SaveImage(
            IFormFile imageFile,
            string uploadsFolderRelative,
            long maxFileSizeBytes,
            string[] allowedExtensions);

        bool DeleteImage(string? relativePath, string webRootPath);
    }
}
