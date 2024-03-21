using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lab28_MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                using (loginEditDatabaseContext db = new loginEditDatabaseContext())
                {
                    string tempStr = "";
                    var userInfo = db.UserInfos
                        .Include(e => e.IdGenderNavigation)
                        .Select(e => e)
                        .Where(e => e.Nickname == User.Identity.Name);
                    foreach(var u in userInfo)
                    {
                        tempStr += $"{u.IdUser}. {u.FirstName ?? "-"} {u.SecondName ?? "-"}" +
                            $"\nПол: {u.IdGenderNavigation?.GenderValue ?? "-"}\n" +
                            $"{u.Birthday?.ToString("d") ?? "-"}\n {u.Country ?? "-"} {u.City ?? "-"}";
                    }
                    ViewData["user"] = tempStr;
                    return View();
                }
            }
            ViewData["user"] = "Не аутентифицирован";
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
