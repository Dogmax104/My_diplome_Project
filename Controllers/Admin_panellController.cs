using Arctech_Manufaction_Menedgment.Data;
using Arctech_Manufaction_Menedgment.Enums;
using Arctech_Manufaction_Menedgment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arctech_Manufaction_Menedgment.Controllers
{
    public class Admin_panellController : Controller
    {
        // Создадим контекст который нельзя будет перезаписать;
        private readonly ApplicationDbContext _db;

        // Создадим конструктор с помощью которого можно будет записать DbContext;
        public Admin_panellController(ApplicationDbContext db)
        {
            _db = db;
        }
        // Метод для открытия страницы;
        public IActionResult OpenAdminPanel()
        {
            // Общий класс для передачи данных в представление;
            General_Admin general_Admin = new General_Admin();
            // Создаю и передаю перечисление в представление;
            //var element = Enum.GetValues(typeof(RoleUser)).Cast<RoleUser>().Select(r => new { Value = r, Text=r.ToString() }).ToList();
            var element = Enum.GetValues(typeof(RoleUser))
                    .Cast<RoleUser>()
                    .Select(r => (Value: r, Text: r.ToString()))
                    .ToList();
            //ViewBag.ListRole=element; // Передаем список ролей в представление;
            general_Admin.RoleEnum = element;
            // Передача списка для отражения в таблице администратора пользоввателя;
            general_Admin.ListUser = _db.UserArctechs.ToList();
            general_Admin.ProgectModel = _db.ProgectModels.Include(n=>n.ClientFileProjectModel).ToList();
            return View("_Admin_page", general_Admin);
        }

        // Метод для добавления в базу данных пользователя
        public IActionResult AddUserArctech(General_Admin admin)
        {
            General_Admin general_Admin = new General_Admin();
            UserArctech us = new UserArctech();
            //создание пользователя;
            us.Namer_for_project = admin.UserGeneral_Admin.Namer_for_project;  // добовляем табельный номер сотрудника;
            us.FistNameUser = admin.UserGeneral_Admin.FistNameUser; // добавляем Имя пользователя;
            us.SecondNameUser = admin.UserGeneral_Admin.SecondNameUser; // добовляем Фамилию пользователя;
            us.NameUser = admin.UserGeneral_Admin.NameUser; // добавление nickName пользователя;
            // добовляем пользователя автоматически;
            us.PasswordUser = GeneratePassword(8);
            us.RoleUser = admin.UserGeneral_Admin.RoleUser;
            _db.UserArctechs.Add(us);
            _db.SaveChanges();
            // Переход согласно заданной роли
            ViewBag.Role = admin.UserGeneral_Admin.RoleUser;
            // Создаю и передаю перечисление в представление;
            var element = Enum.GetValues(typeof(RoleUser)).Cast<RoleUser>().Select(r => new { Value = r, Text = r.ToString() }).ToList();
            ViewBag.ListRole = element; // Передаем список ролей
            general_Admin.ListUser = _db.UserArctechs.ToList();
            general_Admin.ProgectModel=_db.ProgectModels.Include(d=>d.ClientFileProjectModel).ToList();
            return View("_Admin_page",general_Admin);
        }

        // Метод для удаления из базы данных пользователя;
        [HttpPost]
        public async Task <IActionResult> DeletUser(UserArctech UserGeneral_Admin)
        {
            UserArctech us = new UserArctech();
            us=await _db.UserArctechs.FindAsync(UserGeneral_Admin.Id);
            if (us != null)
            {
                _db.UserArctechs.Remove(us);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("OpenAdminPanel"); 
        }

        // Метод для редактирования в БД пользователей;
        public async Task <IActionResult> RiderUser(General_Admin general)
        {
            UserArctech user=new UserArctech();
            UserArctech usercop = new UserArctech();

            if (general != null)
            {
                usercop = await _db.UserArctechs.FindAsync(general.UserGeneral_Admin.Id);
                user = usercop;
                _db.UserArctechs.Remove(usercop);
                await _db.SaveChangesAsync();
                // табельный номер сотрудника
                if (!string.IsNullOrEmpty(Convert.ToString(general.UserGeneral_Admin.Namer_for_project)))
                    user.Namer_for_project = general.UserGeneral_Admin.Namer_for_project;
                // Имя сотрудника
                if (!string.IsNullOrEmpty(general.UserGeneral_Admin.FistNameUser))
                    user.FistNameUser = general.UserGeneral_Admin.FistNameUser;
                // Фамилия сотрудника
                if (!string.IsNullOrEmpty(general.UserGeneral_Admin.SecondNameUser))
                    user.SecondNameUser = general.UserGeneral_Admin.SecondNameUser;
                // статус сотрудника
                if (!string.IsNullOrEmpty(general.UserGeneral_Admin.RoleUser.ToString()))
                    user.RoleUser = general.UserGeneral_Admin.RoleUser;
                // логин сотрудника
                if (!string.IsNullOrEmpty(general.UserGeneral_Admin.NameUser))
                    user.NameUser = general.UserGeneral_Admin.NameUser;
                // пароль сотрудника
                if (!string.IsNullOrEmpty(general.UserGeneral_Admin.PasswordUser))
                    user.PasswordUser = general.UserGeneral_Admin.PasswordUser;
                await _db.UserArctechs.AddAsync(user);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("OpenAdminPanel");
        }

        // Метод для создания задания через adminpanell;
        public IActionResult CreateAdminTask(ProgectModel model)
        {
            return RedirectToAction("OpenAdminPanel");
        }

        // Метод для удаления из базы данных заказа;
        public async Task<IActionResult> DeleteTask(ProgectModel model)
        {
            ProgectModel modelDatabase=new ProgectModel();
            modelDatabase = await _db.ProgectModels.FindAsync(model.IdProjectModel);
            if (modelDatabase != null)
            {
                _db.ProgectModels.Remove(modelDatabase);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("OpenAdminPanel");
        }

        // Метод для рандомного написания пороля
        public static string GeneratePassword(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890"; // Латинские буквы
            Random random = new Random();

            return new string(Enumerable.Repeat(chars, length)
                                        .Select(s => s[random.Next(s.Length)])
                                        .ToArray());
        }
    }
}
