using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Infrastructure.Repositories;
using JuiceWorld.Entities;

namespace BusinessLayer.Services;

public class ReviewService(IRepository<Review> reviewRepository, IMapper mapper) : IReviewService
{
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
        var review = await reviewRepository.GetByIdAsync(id);
        return review is null ? null : mapper.Map<ReviewDto>(review);
    }

    public async Task<ReviewDto?> UpdateReviewAsync(ReviewDto reviewDto)
    {
        var updatedReview = await reviewRepository.UpdateAsync(mapper.Map<Review>(reviewDto));
        return updatedReview is null ? null : mapper.Map<ReviewDto>(updatedReview);
    }

    public async Task<bool> DeleteReviewByIdAsync(int id)
    {
        return await reviewRepository.DeleteAsync(id);
    }
}
