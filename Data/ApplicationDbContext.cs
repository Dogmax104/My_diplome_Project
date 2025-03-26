using Arctech_Manufaction_Menedgment.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Emit;

namespace Arctech_Manufaction_Menedgment.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<UserArctech> UserArctechs { get; set; }

        // Метод для составного первичного ключа:
        //создание производственной модели в базе данных;
        public DbSet<ProgectModel> ProgectModels { get; set; }
        public DbSet<ModelFileClient> ModelFiles { get; set; }

        // Метод который будет определять отношения один ко многим;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Вызов базовой реализации

            // создание id чтоб автоматически было заполнение id номера
            modelBuilder.Entity<UserArctech>().HasKey(x => x.Id); // создание первичного ключа
            modelBuilder.Entity<UserArctech>().Property(u => u.Id).ValueGeneratedOnAdd();

            // создание составного ключа
            modelBuilder.Entity<UserArctech>()
                .HasAlternateKey(u => new { u.NameUser, u.PasswordUser });


            // вот сам метод который определяет один ко многим;
            modelBuilder.Entity<ModelFileClient>()
                .HasOne(p => p.ProgectModel1) // связь одного
                .WithMany(t => t.ClientFileProjectModel) // может быть много по этому ссылается на созданную коллекцию;
                .HasForeignKey(d => d.IdProjectModel104) // созданный внешний ключ;
                .OnDelete(DeleteBehavior.Cascade);  // Для автоматического удаления из базы данных привязаного файла;
        }
    }
}
