using Microsoft.AspNetCore.Mvc;
using OnlineKart.Data;
using OnlineKart.Models;
using System.Linq;

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


		//For create category
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
        public IActionResult Create(Category obj)
        {
			Category? categoryName = _context.Categories.FirstOrDefault(c => c.Name == obj.Name);
			Category? categoryDisplayOrder = _context.Categories.FirstOrDefault(c => c.DisplayOrder == obj.DisplayOrder);

			if (categoryName != null)
			{
                ModelState.AddModelError("Name", "Same category name is already there!!" );
			}
			if (categoryDisplayOrder != null)
			{
				ModelState.AddModelError("DisplayOrder", "Same Display Order is already there!!");
			}

            if (ModelState.IsValid)
            {

                _context.Categories.Add(obj);
                _context.SaveChanges();
                TempData["success"] = "Category created sucessfully";
                return RedirectToAction("Index");

            }
			return View();
			
        }


        //To edit category
        public IActionResult Edit(int? id)
        {

            if(id==null || id==0)
            { 
                return NotFound();
            }
            //There are three ways to get the category with id
            //Category? category= _context.Categories.Where(x=>x.Id==id).FirstOrDefault();
            //Category? category = _context.Categories.FirstOrDefault(c => c.Id == id);
            Category? category = _context.Categories.Find(id);
            if (category==null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
			Category? categoryName = _context.Categories.FirstOrDefault(c => c.Name == obj.Name);
			Category? categoryDisplayOrder = _context.Categories.FirstOrDefault(c => c.DisplayOrder == obj.DisplayOrder);

			if (categoryName != null)
			{
				ModelState.AddModelError("Name", "Name is exactly same as previous!!");
			}
			if (categoryDisplayOrder != null)
			{
				ModelState.AddModelError("DisplayOrder", "Display Order is same as previous!!");
			}
			if (ModelState.IsValid)
            {
                _context.Categories.Update(obj);
                _context.SaveChanges();
                TempData["success"] = "Category updated sucessfully";
                return RedirectToAction("Index");

            }
            return View();

        }
        //public IActionResult Delete(int? id)
        //{

        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    //There are three ways to get the category with id
        //    //Category? category= _context.Categories.Where(x=>x.Id==id).FirstOrDefault();
        //    //Category? category = _context.Categories.FirstOrDefault(c => c.Id == id);
        //    Category? category = _context.Categories.Find(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(category);
        //}
        public IActionResult Delete(int? id)
        {
            
                Category? category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
                _context.Categories.Remove(category);
                _context.SaveChanges();
            TempData["delete"] = "Category deleted sucessfully";
            return RedirectToAction("Index");

           

        }
    }
}
