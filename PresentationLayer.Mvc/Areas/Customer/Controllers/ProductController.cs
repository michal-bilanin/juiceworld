using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Mvc.Areas.Customer.Controllers;

[Area(Constants.Areas.Customer)]
public class ProductController(IProductService productService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] ProductFilterDto productFilter)
    {
        var products = await productService.GetProductDetailsFilteredAsync(productFilter);
        return View(products);
    }
}
