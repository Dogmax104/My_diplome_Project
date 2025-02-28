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

        //�������� ��� Dbcontext;
        private readonly ApplicationDbContext _db;

        //� ����������� �������� db context ��� ����������� � ���� ������;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        // ����� ��� ���������� � ���� ������ ������ ������������;
        // ����� ���������� ����� ��� ����, ���� ����� ���� ����� � ��������������;
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
