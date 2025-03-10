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
        public IActionResult OpenAdminPanel()
        {
            // Общий класс для передачи данных в представление;
            General_Admin general_Admin = new General_Admin();
            // Создаю и передаю перечисление в представление;
            var element = Enum.GetValues(typeof(RoleUser)).Cast<RoleUser>().Select(r => new { Value = r, Text=r.ToString() }).ToList();
            ViewBag.ListRole=element; // Передаем список ролей
            // Передача списка для отражения в таблице администратора пользоввателя;
            general_Admin.ListUser = _db.UserArctechs.ToList();
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
            return View("_Admin_page",general_Admin);
        }
        // Метод для рандомного написания пороля
        public static string GeneratePassword(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"; // Латинские буквы
            Random random = new Random();

            return new string(Enumerable.Repeat(chars, length)
                                        .Select(s => s[random.Next(s.Length)])
                                        .ToArray());
        }
    }
}
