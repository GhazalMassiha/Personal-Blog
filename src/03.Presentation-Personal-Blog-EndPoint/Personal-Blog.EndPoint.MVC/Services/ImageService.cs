namespace Personal_Blog.EndPoint.MVC.Services
{
    public class ImageService : IImageService
    {
        public (bool Success, string? RelativePath, string? ErrorMessage) SaveImage(
       IFormFile imageFile,
       string uploadsFolderRelative,
       long maxFileSizeBytes,
       string[] allowedExtensions)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return (false, null, "هیچ فایلی انتخاب نشده است.");
            }

            var ext = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(ext))
            {
                return (false, null, "فرمت تصویر معتبر نیست.");
            }

            if (imageFile.Length > maxFileSizeBytes)
            {
                return (false, null, $"حجم تصویر نباید بیشتر از {maxFileSizeBytes} بایت باشد.");
            }

            var uploadsAbsolute = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", uploadsFolderRelative);
            if (!Directory.Exists(uploadsAbsolute))
                Directory.CreateDirectory(uploadsAbsolute);

            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(uploadsAbsolute, fileName);

            try
            {
                using var fs = new FileStream(filePath, FileMode.Create);
                imageFile.CopyTo(fs);
            }
            catch (Exception)
            {
                return (false, null, "خطا در ذخیره‌سازی تصویر.");
            }

            var relative = "/" + uploadsFolderRelative.Trim('/') + "/" + fileName;
            return (true, relative, null);
        }

        public bool DeleteImage(string? relativePath, string webRootPath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return true;

            try
            {
                var trimmed = relativePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
                var absolute = Path.Combine(webRootPath, trimmed);
                if (File.Exists(absolute))
                    File.Delete(absolute);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
