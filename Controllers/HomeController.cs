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

        //свойство дл€ Dbcontext;
        private readonly ApplicationDbContext _db;

        //в конструктор добавлен db context дл€ подключени€ в базу данных;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        // ћетод который создаст новую загрузочную страницу дл€ выбора авторизации и удал€ет Cookies;
        public IActionResult StartAfterPage()
        {
            HttpContext.Response.Cookies.Delete("Role");
            return View("HomeStartPage");
        }

        // ћетод дл€ добовлени€ в базу данных нового пользовател€;
        public async Task<IActionResult> Create_Bd_User(UserArctech userarctech)
        {
            // проверка при входе в систему;
            if (await CheckUser(userarctech.NameUser, userarctech.PasswordUser))
            {
                //ƒобавлю значение –оли в куки;
                userarctech.RoleUser = Enum.Parse<RoleUser>(await (AddRole(userarctech.NameUser, userarctech.PasswordUser)));
                //создание cokies
                HttpContext.Response.Cookies.Append(
                    "Role",
                    userarctech.RoleUser.ToString(),
                    new CookieOptions
                    {
                        Expires = DateTimeOffset.Now.AddDays(7),
                        Secure = true, //  уки передаютс€ только по HTTP;
                        HttpOnly = true // “олько HTTP запросы, не доступны дл€ JawaSkript;
                    });

                // ѕереход согласно заданной роли
                ViewBag.Role = userarctech.RoleUser.ToString();

                //ѕроверить и через роль выложить данные которые только с прогрессом более 50%
                if(ViewBag.Role == "shop_worker")
                {
                    List<ProgectModel>mod = new List<ProgectModel>();
                    foreach(var t in _db.ProgectModels.Include(n=>n.ClientFileProjectModel))
                    {
                        if(t.OrderInManufaction)
                            mod.Add(t);
                    }
                    return View("_TableProject", mod);
                }

                var data = _db.ProgectModels.Include(p => p.ClientFileProjectModel).ToList(); // ѕередача в представление данных из базы данных;
                return View("_TableProject", data);
            }
            else return View("Index");
        }

        public IActionResult Index()
        {
            HttpContext.Response.Cookies.Delete("Role");
            return View();
        }
        
        // ƒополнительный метод который сохран€ет значени€ cookies;
        public IActionResult IndexCookies()
        {
            // получаю и передаю значение роли при переходе со страницы;
            string? roleuser = Request.Cookies["Role"];
            ViewBag.Role = roleuser;
            if (roleuser != null)
            {
                UserArctech us=new UserArctech();
                return View("Index", us);
            }
            return View("Index");
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

        // ћетод дл€ проверки пользовател€;
        private async Task<bool> CheckUser(string username, string password)
        {
            return await _db.UserArctechs.AnyAsync(u=>u.NameUser==username &&  u.PasswordUser==password);
        }

        // ћетод дл€ поиска в Ѕƒ пользовател€ который прошел аутефикацию и добавление его роли в куки;
        private async Task<string> AddRole(string username, string password)
        {
            var user=await _db.UserArctechs.FirstOrDefaultAsync(u => u.NameUser == username && u.PasswordUser == password);
            if (user!=null)
                return user.RoleUser.ToString();
            return null;
        }
    }
}
