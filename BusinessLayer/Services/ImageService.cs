using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Services;

public class ImageService(ILogger<ImageService> logger) : IImageService
{
    private const string ImgFolderPath = "Images";

    private static readonly Dictionary<string, string> MimeTypes = new()
    {
        // enough to determine the image type
        // source https://stackoverflow.com/questions/57976898/how-to-get-mime-type-from-base-64-string
        { "/9j/", ".jpg" },
        { "iVBORw0KGgo", ".png" },
        { "R0lGODlh", ".gif" },
        { "R0lGODdh", ".gif" }
    };

    public string GetImageExtension(string base64Image)
    {
        foreach (var mimeType in MimeTypes)
        {
            if (base64Image.StartsWith(mimeType.Key))
            {
                return mimeType.Value;
            }
        }
        return string.Empty;
    }


    public async Task<bool> SaveImageAsync(string base64Image, string imageName)
    {
        Directory.CreateDirectory(ImgFolderPath);
        var imageBytes = Convert.FromBase64String(base64Image);
        var filePath = Path.Combine(ImgFolderPath, imageName);
        try
        {
            await File.WriteAllBytesAsync(filePath, imageBytes);
        }
        catch (Exception e)
        {
            logger.LogError($"ERROR - unable to write to file {filePath} \n ERROR-MESSAGE: {e.Message}");
            return false;
        }
        return true;
    }

    public async Task<string?> GetImageAsync(string imagePath)
    {
        var filePath = Path.Combine(ImgFolderPath, imagePath);
        if (!File.Exists(filePath))
        {
            return null;
        }
        return Convert.ToBase64String(await File.ReadAllBytesAsync(filePath));
    }

    public bool DeleteImageAsync(string imageName)
    {
        try
        {
            File.Delete(Path.Combine(ImgFolderPath, imageName));
        }
        catch (Exception e)
        {
            logger.LogError($"ERROR - unable to delete file {imageName} \n ERROR-MESSAGE: {e.Message}");
            return false;
        }
        return true;
    }

    public async Task<bool> UpdateImageAsync(string base64Image, string imageName, string newImageName)
    {
        var oldImagePath = Path.Combine(ImgFolderPath, imageName);
        if (File.Exists(oldImagePath))
        {
            File.Delete(oldImagePath);
        }
        return await SaveImageAsync(base64Image, newImageName);
    }
}