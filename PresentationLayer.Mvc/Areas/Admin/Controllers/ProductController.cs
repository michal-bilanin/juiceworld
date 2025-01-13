using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Mvc.ActionFilters;
using PresentationLayer.Mvc.Models;

namespace PresentationLayer.Mvc.Areas.Admin.Controllers;

[Area(Constants.Areas.Admin)]
[RedirectIfNotAdminActionFilter]
public class ProductController(
    IProductService productService,
    IManufacturerService manufacturerService,
    ITagService tagService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] ProductFilterDto productFilter)
    {
        var products = await productService.GetProductsFilteredAsync(productFilter);
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var manufacturers = await manufacturerService.GetAllManufacturersAsync();
        var tags = await tagService.GetAllTagsAsync();
        var viewModel = new ProductEditViewModel
        {
            Product = new ProductDto(),
            AllManufacturers = manufacturers,
            AllTags = tags
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductEditViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            viewModel.AllManufacturers = await manufacturerService.GetAllManufacturersAsync();
            viewModel.AllTags = await tagService.GetAllTagsAsync();
            return View(viewModel);
        }

        var createdProduct = await productService.CreateProductAsync(viewModel.Product);
        if (createdProduct == null)
        {
            ModelState.AddModelError("Id", "Failed to create product.");
            return View(viewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await productService.GetProductByIdAsync(id);
        if (product == null)
        {
            ModelState.AddModelError("Id", "Product not found.");
            return View();
        }

        var manufacturers = await manufacturerService.GetAllManufacturersAsync();
        var tags = await tagService.GetAllTagsAsync();
        var viewModel = new ProductEditViewModel
        {
            Product = product,
            AllManufacturers = manufacturers,
            AllTags = tags
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductEditViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            viewModel.AllManufacturers = await manufacturerService.GetAllManufacturersAsync();
            viewModel.AllTags = await tagService.GetAllTagsAsync();
            return View(viewModel);
        }

        var updatedProduct = await productService.UpdateProductAsync(viewModel.Product);
        if (updatedProduct == null)
        {
            ModelState.AddModelError("Id", "Failed to update product.");
            return View(viewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await productService.DeleteProductByIdAsync(id);
        return result ? RedirectToAction(nameof(Index)) : NotFound();
    }
}