using Arctech_Manufaction_Menedgment.Data;
using Arctech_Manufaction_Menedgment.Enums;
using Arctech_Manufaction_Menedgment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        public async Task <IActionResult> Create_Bd_User(UserArctech userarctech)
        {
            // проверка при входе в систему;
            if (await CheckUser(userarctech.NameUser, userarctech.PasswordUser))
            {
                //Добавлю значение Роли в куки;
                userarctech.RoleUser = Enum.Parse<RoleUser>(await (AddRole(userarctech.NameUser, userarctech.PasswordUser)));
                //создание cokies
                HttpContext.Response.Cookies.Append(
                    "Role",
                    userarctech.RoleUser.ToString(),
                    new CookieOptions
                    {
                        Expires = DateTimeOffset.Now.AddDays(7),
                        Secure = true, // Куки передаются только по HTTP;
                        HttpOnly = true // Только HTTP запросы, не доступны для JawaSkript;
                    });

                // Переход согласно заданной роли
                ViewBag.Role = userarctech.RoleUser;

                var data=_db.ProgectModels.Include(p=>p.ClientFileProjectModel).ToList(); // Передача в представление данных из базы данных;
                return View("_TableProject",data);
            }
            else return  View("Index");
        }

        public IActionResult Index()
        {
            HttpContext.Response.Cookies.Delete("Role");
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

        // Метод для проверки пользователя;
        private async Task<bool> CheckUser(string username, string password)
        {
            return await _db.UserArctechs.AnyAsync(u=>u.NameUser==username &&  u.PasswordUser==password);
        }

        // Метод для поиска в БД пользователя который прошел аутефикацию и добавление его роли в куки;
        private async Task<string> AddRole(string username, string password)
        {
            var user=await _db.UserArctechs.FirstOrDefaultAsync(u => u.NameUser == username && u.PasswordUser == password);
            if (user!=null)
                return user.RoleUser.ToString();
            return null;
        }
    }
}
