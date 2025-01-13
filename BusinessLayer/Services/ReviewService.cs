using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Infrastructure.Repositories;
using JuiceWorld.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace BusinessLayer.Services;

public class ReviewService(IRepository<Review> reviewRepository,
    IMemoryCache memoryCache,
    IMapper mapper) : IReviewService
{
    private string _cacheKeyPrefix = nameof(ReviewService);
    public async Task<ReviewDto?> CreateReviewAsync(ReviewDto reviewDto)
    {
        var newReview = await reviewRepository.CreateAsync(mapper.Map<Review>(reviewDto));
        return newReview is null ? null : mapper.Map<ReviewDto>(newReview);
    }

    public async Task<IEnumerable<ReviewDto>> GetAllReviewsAsync()
    {
        var reviews = await reviewRepository.GetAllAsync();
        return mapper.Map<List<ReviewDto>>(reviews);
    }

    public async Task<ReviewDto?> GetReviewByIdAsync(int id)
    {
        string cacheKey = $"{_cacheKeyPrefix}-Review{id}";
        if (!memoryCache.TryGetValue(cacheKey, out Review? value))
        {
            value = await reviewRepository.GetByIdAsync(id);
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
            memoryCache.Set(cacheKey, value, cacheEntryOptions);
        }

        return value is null ? null : mapper.Map<ReviewDto>(value);
    }

    public async Task<ReviewDto?> UpdateReviewAsync(ReviewDto reviewDto)
    {
        string cacheKey = $"{_cacheKeyPrefix}-Review{reviewDto.Id}";
        memoryCache.Remove(cacheKey);
        var updatedReview = await reviewRepository.UpdateAsync(mapper.Map<Review>(reviewDto));
        return updatedReview is null ? null : mapper.Map<ReviewDto>(updatedReview);
    }

    public async Task<bool> DeleteReviewByIdAsync(int id)
    {
        string cacheKey = $"{_cacheKeyPrefix}-Review{id}";
        memoryCache.Remove(cacheKey);
        return await reviewRepository.DeleteAsync(id);
    }
}
