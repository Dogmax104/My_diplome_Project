using Arctech_Manufaction_Menedgment.Data;
using Arctech_Manufaction_Menedgment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Arctech_Manufaction_Menedgment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //свойство для Dbcontext;
        private readonly ApplicationDbContext _db;

        //в конструктор добавлен db context для подключения в базу данных;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        // Метод для добовления в базу данных нового пользователя;
        // Нужно переделать метод для того, чтоб можно было зайти и авторизоваться;
        public IActionResult Create_Bd_User(UserArctech userarctech)
        {
            UserArctech us = new UserArctech();
            //us.NameUser = userarctech.NameUser;
            //us.PasswordUser= userarctech.PasswordUser;
            //us.RoleUser = "admin";
            //_db.UserArctechs.Add(us);
            //_db.SaveChanges();
            return View("_TableProject");
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
