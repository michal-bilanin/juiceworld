using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private const string ApiBaseName = "Review";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateReview))]
    public async Task<ActionResult<bool>> CreateReview()
    {
        return Problem();
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllReviews))]
    public async Task<ActionResult<bool>> GetAllReviews()
    {
        return Problem();
    }

    [HttpGet("{reviewId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetReview))]
    public async Task<ActionResult<bool>> GetReview(int reviewId)
    {
        return Problem();
    }

    [HttpPut("{reviewId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(UpdateReview))]
    public async Task<ActionResult<bool>> UpdateReview(int reviewId)
    {
        return Problem();
    }

    [HttpDelete("{reviewId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteReview))]
    public async Task<ActionResult<bool>> DeleteReview(int reviewId)
    {
        return Problem();
    }
}