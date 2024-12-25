using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Mvc.ActionFilters;
using PresentationLayer.Mvc.Models;

namespace PresentationLayer.Mvc.Areas.Admin.Controllers;

[Area(Constants.Areas.Admin)]
[RedirectIfNotAdminActionFilter]
public class ProductController(IProductService productService, IManufacturerService manufacturerService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] ProductFilterDto productFilter)
    {
        var products = await productService.GetProductsFilteredAsync(productFilter);
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        var manufacturers = await manufacturerService.GetAllManufacturersAsync();
        var viewModel = new ProductEditViewModel
        {
            Product = product,
            AllManufacturers = manufacturers
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductEditViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            viewModel.AllManufacturers = await manufacturerService.GetAllManufacturersAsync();
            return View(viewModel);
        }

        var updatedProduct = await productService.UpdateProductAsync(viewModel.Product);
        if (updatedProduct == null)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }
}
