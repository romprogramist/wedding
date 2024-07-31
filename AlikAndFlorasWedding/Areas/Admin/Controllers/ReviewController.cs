using Microsoft.AspNetCore.Mvc;

namespace AlikAndFlorasWedding.Areas.Admin.Controllers;

public class ReviewController : BaseAdminController
{
    public IActionResult Index()
    {
        return View();
    }
}