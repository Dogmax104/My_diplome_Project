using Arctech_Manufaction_Menedgment.Data;
using Arctech_Manufaction_Menedgment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;

namespace Arctech_Manufaction_Menedgment.Controllers
{
    public class TaskPageController : Controller
    {
        // Создадим контекст который нельзя будет перезаписать;
        private readonly ApplicationDbContext _db;

        // Создадим конструктор с помощью которого можно будет записать DbContext;
        public TaskPageController(ApplicationDbContext db)
        {
            _db = db;
        }


        // Метод с помощью которого будет создаваться экзэмпляр модели;
        [HttpPost]
        [RequestSizeLimit (524128800)]
        public async Task <IActionResult> CreateProjectModel(ProgectModel model, List<IFormFile> file, string PageadminFlag)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View("_NewTaskPage");
            //}
            // Создать модель;
            ProgectModel modelDataBase= new ProgectModel();
            modelDataBase.NameProjectModel = model.NameProjectModel; // Присвоенно имя модели;
            modelDataBase.ClientNameProjectModel=model.ClientNameProjectModel; // заполнение данных клиента;
            //для модели присвоить значение даты, нельзя проверить, потому, что поле для обязательного заполнения;
            modelDataBase.BeginTime = model.BeginTime;
            modelDataBase.CoordinationTime=model.CoordinationTime; // Заполнение даты согласования заказа;
            modelDataBase.NotesProjectModel = model.NotesProjectModel; // Заполнение коментарий для заказа;
            modelDataBase.StatusOrder=model.StatusOrder; // Заполнение данных о статусе;
            modelDataBase.OrderInManufaction = model.OrderInManufaction; // Данные о запуске в производство либо нет;

            // нужно создать коллекцию с modelDataBase;
            modelDataBase.ClientFileProjectModel = new List<ModelFileClient>();
            // Добовлениия файл(а)ов в базу данных;
            foreach (var item in file)
            {
                if(file!=null && file.Count > 0)
                {
                    using(var memorystream=new MemoryStream())
                    {
                        await item.CopyToAsync(memorystream); // запись файлов в бинарный код
                        var mod = new ModelFileClient();    // Создаем объект нового файла
                        mod.NameModelFile=memorystream.ToArray(); // запись файла в бинарном коде
                        mod.NameModelFileClient=item.FileName; // запись имени файла
                        mod.DateModelFile = DateTime.Now; // запись даты создания файла в базе данных

                        // устонавливавем связь в базе данных один со многим;
                        mod.IdProjectModel104 = modelDataBase.IdProjectModel;
                        mod.ProgectModel1 = model;
                        modelDataBase.ClientFileProjectModel.Add(mod);
                    }
                }
            }
            _db.ProgectModels.Add(modelDataBase); // Добовление в базу данных;
            await _db.SaveChangesAsync(); // Сохранение в базе данных;
            // Список для того, чтоб можно было передать List
            var projectList = _db.ProgectModels.Include(b=>b.ClientFileProjectModel).ToList();
            if (PageadminFlag != null)
            {
                return RedirectToAction("OpenAdminPanel", "Admin_panell");
            }
            return RedirectToAction("MetodBack");
        }




        public IActionResult NewTaskPage()
        {
            ProgectModel model = new ProgectModel();
            return View("_NewTaskPage");
        }

        
        //Метод который возвратит заново таблицу;
        public IActionResult MetodBack()
        {
            // Получение и отправки Cookies
            string? cookie = Request.Cookies["Role"];
            ViewBag.Role = cookie;
            var projectList = _db.ProgectModels.Include(b => b.ClientFileProjectModel).ToList();
            return View("_TableProject", projectList);
        }
    }
}
