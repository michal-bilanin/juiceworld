using System.Security.Claims;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Commons.Enums;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Mvc.ActionFilters;
using PresentationLayer.Mvc.Areas.Admin.Models;
using PresentationLayer.Mvc.Areas.Customer.Models;
using PresentationLayer.Mvc.Facades.Interfaces;
using PresentationLayer.Mvc.Models;

namespace PresentationLayer.Mvc.Areas.Customer.Controllers;

[Area(Constants.Areas.Customer)]
public class ProductController(
    ISearchablesFacade searchablesFacade,
    IProductService productService,
    ICartItemService cartItemService,
    IReviewService reviewService,
    IWishListItemService wishListItemService,
    IMapper mapper) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] SearchablesFilterViewModel searchablesFilter)
    {
        var searchables = await searchablesFacade.GetSearchablesFilteredAsync(searchablesFilter);
        return View(searchables);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var product = await productService.GetProductDetailByIdAsync(id);
        if (product is null)
        {
            return NotFound();
        }

        bool isInWishlist = false;
        if (User.Identity is { IsAuthenticated: true })
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId))
            {
                return BadRequest();
            }

            isInWishlist = await wishListItemService.IsProductInWishListAsync(id, userId);
        }

        return View(new ProductDetailViewModel
        {
            ProductDetail = product,
            IsInWishList = isInWishlist
        });
    }

    [HttpPost]
    [RedirectIfNotAuthenticatedActionFilter]
    public async Task<IActionResult> AddToCart(AddToCartViewModel addToCartViewModel)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId))
        {
            return BadRequest();
        }

        var success = await cartItemService.AddToCartAsync(mapper.Map<AddToCartDto>(addToCartViewModel), userId);
        var product = await productService.GetProductDetailByIdAsync(addToCartViewModel.ProductId);
        if (product is null) return NotFound();

        if (!success) ViewData[Constants.Keys.ErrorMessage] = "Failed to add the product to the cart.";

        return RedirectToAction(nameof(Details), new { id = product.Id });
    }

    [HttpPost]
    [RedirectIfNotAdminActionFilter]
    public async Task<IActionResult> AddToWishlist(int productId)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId))
        {
            return BadRequest();
        }

        await wishListItemService.CreateWishListItemAsync(new WishListItemDto
        {
            ProductId = productId,
            UserId = userId
        });

        return RedirectToAction(nameof(Details), new { id = productId });
    }

    [HttpPost]
    [RedirectIfNotAuthenticatedActionFilter]
    public async Task<IActionResult> AddReview(ReviewViewModel reviewViewModel)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Details), new { id = reviewViewModel.ProductId });
        }

        if (!int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId))
        {
            return BadRequest();
        }

        reviewViewModel.UserId = userId;
        var review = await reviewService.CreateReviewAsync(mapper.Map<ReviewDto>(reviewViewModel));
        if (review is null) return BadRequest();

        return RedirectToAction(nameof(Details), new { id = review.ProductId });
    }

    [HttpPost]
    [RedirectIfNotAuthenticatedActionFilter]
    public async Task<IActionResult> DeleteReview(int id)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId))
        {
            return BadRequest();
        }

        var review = await reviewService.GetReviewByIdAsync(id);
        if (review is null || (review.UserId != userId && !User.IsInRole(UserRole.Admin.ToString())))
            return Unauthorized();

        var success = await reviewService.DeleteReviewByIdAsync(id);
        if (!success)
        {
            return BadRequest();
        }

        return RedirectToAction(nameof(Details), new { id = review.ProductId });
    }
}
