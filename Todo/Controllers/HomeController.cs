using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain;
using Todo.Models;

namespace Todo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext _db;

        public HomeController(ILogger<HomeController> logger, DatabaseContext db)
        {
            _logger = logger;
            _db = db;
        }


        public IActionResult Index()
        {
            var tasks = _db.Tasks.ToList();
            var res = new List<Models.Tarea>();
            foreach (var task in tasks) {

                res.Add(new Models.Tarea
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    DueDate = task.DueDate,
                    Priority = task.Priority,
                    IsCompleted = task.IsCompleted,
                    Category = new Models.Category() { 
                        Id = task.Category.Id,
                        Name = task.Category.Name
                    }
                });
            }
            return View(res);
        }

        public IActionResult TaskForm()
        {
            var categories = _db.Categories.ToList();
            var result = new List<Models.Category>();
            foreach (var category in categories)
            {
                result.Add(new Models.Category
                {
                    Id = category.Id,
                    Name = category.Name,
                });
            }

            ViewData["Categories"] = result;

            return View(new Models.Tarea());
        }

        public IActionResult DeleteTask() {
            return RedirectToAction("Index");
        }

        public IActionResult EditTask() {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CreateTask(Models.Tarea tarea)
        {
           
            _db.Tasks.Add(new Domain.Task
            {
               Id=tarea.Id,
               Title=tarea.Title,
               Description=tarea.Description,
               IsCompleted=tarea.IsCompleted,
               Priority = tarea.Priority,
               CategoryId=tarea.CategoryId,
               DueDate=tarea.DueDate
  
            });
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult CategoryForm() {
            return View(new Models.Category());
        }
        public IActionResult Category()
        {
            var categories = _db.Categories.ToList();
            var result = new List<Models.Category>();
            foreach (var category in categories)
            {
                result.Add(new Models.Category
                {
                    Id = category.Id,
                    Name = category.Name,   
                });
            }

            return View(result);
        }

        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            var task = _db.Tasks.Where(x => x.CategoryId == id);

            if (task.Any())
            {
                ViewData["Error"] = "No se puede borrar la categoria";
                return RedirectToAction("Category");
            }

            if (category != null)
            {
                _db.Categories.Remove(category);
                _db.SaveChanges();
            }

            return RedirectToAction("Category");
        }

        [HttpPost]
        public IActionResult CreateCategory(Models.Category category) {

            _db.Categories.Add(new Domain.Category {
                Id = category.Id,
                Name = category.Name,
            });

            _db.SaveChanges();

            return RedirectToAction("Category");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
