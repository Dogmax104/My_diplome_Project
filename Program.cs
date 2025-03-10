using Arctech_Manufaction_Menedgment.Data;
using Arctech_Manufaction_Menedgment.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ��������� ��� ���� ������, "����������� ide ��� ���������";
//SQLitePCL.Batteries.Init();

// ������ ��� �������������.
builder.Services.AddControllersWithViews();
// ����� ������� ���������� Razor;
builder.Services.AddRazorPages();



// ������ ����������� ��� ���� ������ c ����� json;
var connectionString = builder.Configuration.GetConnectionString("SqLiteConnection")
    ?? throw new InvalidOperationException("Connection string 'SqLiteConnection' not found.");

// ����������� � ���� ������;
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlite(connectionString));

//��������� identity;
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

app.UseMiddleware<AuthMiddleware>(); // ��� �������� ������������ cookie;