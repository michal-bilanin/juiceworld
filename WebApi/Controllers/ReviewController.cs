using AutoMapper;
using Infrastructure.Repositories;
using JuiceWorld.Entities;
using JuiceWorld.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(UserRole.Customer))]
public class ReviewController(IRepository<Review> reviewRepository, IMapper mapper) : ControllerBase
{
    private const string ApiBaseName = "Review";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateReview))]
    public async Task<ActionResult<ReviewDto>> CreateReview(ReviewDto review)
    {
        var result = await reviewRepository.Create(mapper.Map<Review>(review));
        return result == null ? Problem() : Ok(mapper.Map<ReviewDto>(result));
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllReviews))]
    public async Task<ActionResult<List<ReviewDto>>> GetAllReviews()
    {
        var result = await reviewRepository.GetAll();
        return Ok(mapper.Map<ICollection<ReviewDto>>(result).ToList());
    }

    [HttpGet("{reviewId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetReview))]
    public async Task<ActionResult<ReviewDto>> GetReview(int reviewId)
    {
        var result = await reviewRepository.GetById(reviewId);
        return result == null ? NotFound() : Ok(mapper.Map<ReviewDto>(result));
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateReview))]
    public async Task<ActionResult<ReviewDto>> UpdateReview(ReviewDto review)
    {
        var result = await reviewRepository.Update(mapper.Map<Review>(review));
        return result == null ? Problem() : Ok(mapper.Map<ReviewDto>(result));
    }

    [HttpDelete("{reviewId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteReview))]
    public async Task<ActionResult<bool>> DeleteReview(int reviewId)
    {
        var result = await reviewRepository.Delete(reviewId);
        return result ? Ok() : NotFound();
    }
}
