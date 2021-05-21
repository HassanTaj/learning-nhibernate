using FluentHibernateTodos.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Todos.Entities;
using Todos.HIbernate;

namespace FluentHibernateTodos.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly INHibernateDbSession _db;

        public HomeController(ILogger<HomeController> logger, INHibernateDbSession db) {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> Index() {
            var todos = _db.Get<Todo>().ToList();
            try {
                _db.BeginTransaction();
                await _db.Save(new Todo {
                    TaskDescirption = "Description",
                    DueDate = DateTime.Now.AddDays(3),
                    Title = "Task"
                });
                await _db.Commit();

                todos = _db.Get<Todo>().ToList();
                ViewBag.Todos = todos;
            }
            catch (Exception ex) {
                await _db.Rollback();
                _db.CloseTransaction();

                _logger.Log(LogLevel.Debug,ex.Message);
            }
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
