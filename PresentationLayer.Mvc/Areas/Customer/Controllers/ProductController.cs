using System.Security.Claims;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Mvc.ActionFilters;

namespace PresentationLayer.Mvc.Areas.Customer.Controllers;

[Area(Constants.Areas.Customer)]
public class ProductController(IProductService productService, ICartItemService cartItemService, IReviewService reviewService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] ProductFilterDto productFilter)
    {
        var products = await productService.GetProductDetailsFilteredAsync(productFilter);
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var product = await productService.GetProductDetailByIdAsync(id);
        if (product is null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost]
    [RedirectIfNotAuthenticatedActionFilter]
    public async Task<IActionResult> AddToCart(AddToCartDto addToCartDto)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId))
        {
            return Unauthorized();
        }

        var success = await cartItemService.AddToCartAsync(addToCartDto, userId);
        var product = await productService.GetProductDetailByIdAsync(addToCartDto.ProductId);
        if (product is null)
        {
            return NotFound();
        }

        if (!success)
        {
            ViewData[Constants.Keys.ErrorMessage] = "Failed to add the product to the cart.";
        }

        return View("Details", product);
    }

    [HttpPost]
    [RedirectIfNotAuthenticatedActionFilter]
    public async Task<IActionResult> AddReview(ReviewDto reviewDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (!int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId))
        {
            return Unauthorized();
        }

        if (reviewDto.UserId != userId)
        {
            return Unauthorized();
        }

        var review = await reviewService.CreateReviewAsync(reviewDto);
        if (review is null)
        {
            return BadRequest();
        }

        return RedirectToAction(nameof(Details), new { id = review.ProductId });
    }

    [HttpPost]
    [RedirectIfNotAuthenticatedActionFilter]
    public async Task<IActionResult> DeleteReview(int id)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId))
        {
            return Unauthorized();
        }

        var review = await reviewService.GetReviewByIdAsync(id);
        if (review is null || (review.UserId != userId && !User.IsInRole(Commons.Enums.UserRole.Admin.ToString())))
        {
            return Unauthorized();
        }

        var success = await reviewService.DeleteReviewByIdAsync(id);
        if (!success)
        {
            return BadRequest();
        }

        return Ok();
    }
}
