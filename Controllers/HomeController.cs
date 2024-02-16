using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net;
using TodoList.DBContext;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly TodoContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(TodoContext db, ILogger<HomeController> logger)
        {
            _db = db;
            _logger = logger;
        }
        #region View
        public ActionResult Index()
        {
            return View(_db.Todo.ToList());
        }
        #endregion
        #region Create
        public ActionResult Create()
        {
            return View();
        }
        #endregion
        #region Create POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Todo todo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Todo.Add(todo); 
                    _db.SaveChanges();
                    return RedirectToAction("Index"); 
                }
            }
            catch (Exception ex)
            {                
                ModelState.AddModelError("", "An error occurred while saving the todo: " + ex.Message);
            }
            return View(todo);
        }
        #endregion
        #region Edit
        public ActionResult Edit(int? id)
        {
            Todo todo = _db.Todo.Find(id);

            return View(todo);
        }
        #endregion
        #region Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Todo todo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Entry(todo).State = EntityState.Modified; 
                    _db.SaveChanges();
                    return RedirectToAction("Index"); 
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while saving the todo: " + ex.Message);
            }
            return View(todo);
        }
        #endregion
        #region Delete
        public ActionResult Delete(int? id)
        {
            Todo todo = _db.Todo.Find(id);
            _db.Todo.Remove(todo);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        #region UpdateStatus Ajax
        [HttpPost]
        public ActionResult UpdateStatus(int taskId, bool isChecked)
        {
            try
            {    
                Todo detail = _db.Todo.Find(taskId);
                if (detail != null)
                {
                    
                    detail.IsCompleted = isChecked;

                    
                    _db.Entry(detail).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("", "Todo not found");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while saving the todo: " + ex.Message);
            }            

            return Json(new { success = true, message = "Task status updated successfully" });

        }

        #endregion


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
