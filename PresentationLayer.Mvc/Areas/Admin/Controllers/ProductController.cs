using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Mvc.ActionFilters;

namespace PresentationLayer.Mvc.Areas.Admin.Controllers;

[Area(Constants.Areas.Admin)]
[RedirectIfNotAdminActionFilter]
public class ProductController(IProductService productService) : Controller
{
    public async Task<IActionResult> Index([FromQuery] ProductFilterDto productFilter)
    {
        var products = await productService.GetProductsFilteredAsync(productFilter);
        return View(products);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var product = await productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var product = await productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        await productService.DeleteProductByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
