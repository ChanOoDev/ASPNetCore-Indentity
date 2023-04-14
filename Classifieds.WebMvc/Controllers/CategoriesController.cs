using Classifieds.Data;
using Classifieds.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Classifieds.WebMvc.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context) {
            _context=context;
        }
        public IActionResult Index()
        {
            List<Category> categories=_context.Categories.ToList();
            return View(categories);
        }
    }
}
