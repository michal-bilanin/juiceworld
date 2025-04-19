namespace BusinessLayer.Services.Interfaces;

public interface IImageService
{
    public string GetImageExtension(string base64Image);
    public Task<string?> SaveImageAsync(string base64Image, string imageName);
    public Task<string?> UpdateImageAsync(string base64Image, string? imageName, string newImageName);
    public Task<bool> DeleteImageAsync(string imageName);
}
