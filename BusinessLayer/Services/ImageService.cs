using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Services;

public class ImageService : IImageService
{
    private readonly BlobContainerClient _blobContainerClient;
    private readonly ILogger<ImageService> _logger;

    public ImageService(ILogger<ImageService> logger, BlobServiceClient blobServiceClient, string containerName)
    {
        _logger = logger;
        _blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
        _blobContainerClient.CreateIfNotExists(PublicAccessType.Blob);
    }

    private static readonly Dictionary<string, string> MimeTypes = new()
    {
        { "/9j/", ".jpg" },
        { "iVBORw0KGgo", ".png" },
        { "R0lGODlh", ".gif" },
        { "R0lGODdh", ".gif" }
    };

    public string GetImageExtension(string base64Image)
    {
        foreach (var mimeType in MimeTypes)
            if (base64Image.StartsWith(mimeType.Key))
                return mimeType.Value;

        return string.Empty;
    }

    public async Task<string?> SaveImageAsync(string base64Image, string imageName)
    {
        try
        {
            var imageBytes = Convert.FromBase64String(base64Image);
            var blobClient = _blobContainerClient.GetBlobClient(imageName);

            using var stream = new MemoryStream(imageBytes);
            await blobClient.UploadAsync(stream, overwrite: true);
            return blobClient.Uri.ToString();
        }
        catch (Exception e)
        {
            _logger.LogError($"ERROR - unable to upload image {imageName} to Azure Blob Storage \n ERROR-MESSAGE: {e.Message}");
            return null;
        }
    }

    public async Task<bool> DeleteImageAsync(string imageName)
    {
        try
        {
            var blobClient = _blobContainerClient.GetBlobClient(imageName);
            await blobClient.DeleteIfExistsAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError($"ERROR - unable to delete image {imageName} from Azure Blob Storage \n ERROR-MESSAGE: {e.Message}");
            return false;
        }
    }

    public async Task<string?> UpdateImageAsync(string base64Image, string? imageName, string newImageName)
    {
        if (imageName != null)
        {
            await DeleteImageAsync(imageName);
        }

        return await SaveImageAsync(base64Image, newImageName);
    }
}
