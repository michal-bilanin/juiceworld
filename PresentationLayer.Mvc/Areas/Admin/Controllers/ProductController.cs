using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Mvc.ActionFilters;

namespace PresentationLayer.Mvc.Areas.Admin.Controllers;

[Area(Constants.Areas.Admin)]
[RedirectIfNotAdminActionFilter]
public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
