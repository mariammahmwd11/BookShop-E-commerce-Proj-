using BookSAW.business_logic.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

public class ImageServices : IImageServices
{
    public async Task<string> SaveImage(IFormFile photo,string folderName)
    {
        if (photo == null || photo.Length == 0)
            return null;

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(),$"wwwroot/images/{folderName}");

        Directory.CreateDirectory(uploadsFolder);

        var fileName = Path.GetFileName(photo.FileName);
        var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";

        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await photo.CopyToAsync(stream);
        }

        return $"/images/{folderName}/{uniqueFileName}";
    }

    public void DeleteImage(string imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl))
            return;

        var filePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            imageUrl.TrimStart('/')
        );

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}