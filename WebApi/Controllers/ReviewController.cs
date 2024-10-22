using Infrastructure.UnitOfWork;
using JuiceWorld.Entities;
using JuiceWorld.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "user")]
public class ReviewController(IUnitOfWorkProvider<UnitOfWork> unitOfWorkProvider) : ControllerBase
{
    private const string ApiBaseName = "Review";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateReview))]
    public async Task<ActionResult<Review>> CreateReview(Review review)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.ReviewRepository.Create(review);
        if (result == null)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(result);
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllReviews))]
    public async Task<ActionResult<List<Review>>> GetAllReviews()
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.ReviewRepository.GetAll();
        return Ok(result);
    }

    [HttpGet("{reviewId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetReview))]
    public async Task<ActionResult<Review>> GetReview(int reviewId)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.ReviewRepository.GetById(reviewId);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateReview))]
    public async Task<ActionResult<Review>> UpdateReview(Review review)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        if (!await unitOfWork.ReviewRepository.Update(review))
        {
            return NotFound();
        }

        await unitOfWork.Commit();
        return Ok(review);
    }

    [HttpDelete("{reviewId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteReview))]
    public async Task<ActionResult<bool>> DeleteReview(int reviewId)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.ReviewRepository.Delete(reviewId);
        if (!result)
        {
            return NotFound();
        }

        await unitOfWork.Commit();
        return Ok(result);
    }
}
