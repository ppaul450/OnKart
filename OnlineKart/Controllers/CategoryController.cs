using Microsoft.AspNetCore.Mvc;
using OnlineKart.Data;
using OnlineKart.Models;

namespace OnlineKart.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _context;
		public CategoryController(ApplicationDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			List<Category> objCategoryList= _context.Categories.ToList();
			return View(objCategoryList);
		}

		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
        public IActionResult Create(Category obj)
        {
			if(ModelState.IsValid)
			{
                _context.Categories.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
			return View();
			
        }
    }
}
