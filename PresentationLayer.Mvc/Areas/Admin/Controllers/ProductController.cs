using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Mvc.Areas.Admin.Controllers;

[Area(Constants.Areas.Admin)]
public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
