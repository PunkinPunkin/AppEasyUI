using AppEasyUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppEasyUI.Controllers
{
    public class DemoController : BasicEzUiController
    {
        public DemoController(ILogger<DemoController> logger) : base(logger) { }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DataGrid()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DemoData(string type = "",
                                      string keyword = "",
                                      DateTime? startDate = null,
                                      DateTime? endDate = null)
        {
            var users = new User().GetTestUsers(type, keyword, startDate, endDate);
            return Ok(users.ToPagination(GetPageOptionInfo(), GetSortInfo()));
        }

        [HttpGet]
        public IActionResult Form()
        {
            return View();
        }

        [HttpGet]
        public IActionResult FindUser(int id)
        {
            var user = new User().GetTestUsers().FirstOrDefault(o => o.Id == id);
            if (user != null)
                return Ok(user);
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                var original = new User().GetTestUsers().Where(o => o.Id == user.Id).FirstOrDefault();
                if (original == null)
                    return NotFound(new { isOk = false });
                return Ok(new { isOk = true, data = user });
            }
            var error = ModelState.Select(x => x.Value?.Errors)
                .Where(y => y?.Count > 0)
                .ToList();
            return BadRequest(new { isOk = false, error });
        }

        [HttpGet]
        public IActionResult Dialog()
        {
            return View();
        }
    }
}
