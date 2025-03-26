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

        //�������� ��� Dbcontext;
        private readonly ApplicationDbContext _db;

        //� ����������� �������� db context ��� ����������� � ���� ������;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        // ����� ������� ������� ����� ����������� �������� ��� ������ ����������� � ������� Cookies;
        public IActionResult StartAfterPage()
        {
            HttpContext.Response.Cookies.Delete("Role");
            return View("HomeStartPage");
        }

        // ����� ��� ���������� � ���� ������ ������ ������������;
        public async Task<IActionResult> Create_Bd_User(UserArctech userarctech)
        {
            // �������� ��� ����� � �������;
            if (await CheckUser(userarctech.NameUser, userarctech.PasswordUser))
            {
                //������� �������� ���� � ����;
                userarctech.RoleUser = Enum.Parse<RoleUser>(await (AddRole(userarctech.NameUser, userarctech.PasswordUser)));
                //�������� cokies
                HttpContext.Response.Cookies.Append(
                    "Role",
                    userarctech.RoleUser.ToString(),
                    new CookieOptions
                    {
                        Expires = DateTimeOffset.Now.AddDays(7),
                        Secure = true, // ���� ���������� ������ �� HTTP;
                        HttpOnly = true // ������ HTTP �������, �� �������� ��� JawaSkript;
                    });

                // ������� �������� �������� ����
                ViewBag.Role = userarctech.RoleUser.ToString();

                //��������� � ����� ���� �������� ������ ������� ������ � ���������� ����� 50%
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

                var data = _db.ProgectModels.Include(p => p.ClientFileProjectModel).ToList(); // �������� � ������������� ������ �� ���� ������;
                return View("_TableProject", data);
            }
            else return View("Index");
        }

        public IActionResult Index()
        {
            HttpContext.Response.Cookies.Delete("Role");
            return View();
        }
        
        // �������������� ����� ������� ��������� �������� cookies;
        public IActionResult IndexCookies()
        {
            // ������� � ������� �������� ���� ��� �������� �� ��������;
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

        // ����� ��� �������� ������������;
        private async Task<bool> CheckUser(string username, string password)
        {
            return await _db.UserArctechs.AnyAsync(u=>u.NameUser==username &&  u.PasswordUser==password);
        }

        // ����� ��� ������ � �� ������������ ������� ������ ����������� � ���������� ��� ���� � ����;
        private async Task<string> AddRole(string username, string password)
        {
            var user=await _db.UserArctechs.FirstOrDefaultAsync(u => u.NameUser == username && u.PasswordUser == password);
            if (user!=null)
                return user.RoleUser.ToString();
            return null;
        }
    }
}
