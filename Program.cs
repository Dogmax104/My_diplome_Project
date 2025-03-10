using Arctech_Manufaction_Menedgment.Data;
using Arctech_Manufaction_Menedgment.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Настройка для базы данных, "потребовала ide для установки";
//SQLitePCL.Batteries.Init();

// Сервис для представления.
builder.Services.AddControllersWithViews();
// Также добавим технологию Razor;
builder.Services.AddRazorPages();



// строка подключения для базы данных c файла json;
var connectionString = builder.Configuration.GetConnectionString("SqLiteConnection")
    ?? throw new InvalidOperationException("Connection string 'SqLiteConnection' not found.");

// подключение к базе данных;
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlite(connectionString));

//настройка identity;
//builder.Services.AddDefaultIdentity<UserArctech>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

app.UseMiddleware<AuthMiddleware>(); // Для проверки сохранненных cookie;