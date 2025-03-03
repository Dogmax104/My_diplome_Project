using Arctech_Manufaction_Menedgment.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

            // создание составного ключа
            modelBuilder.Entity<UserArctech>()
                .HasKey(u => new { u.NameUser, u.PasswordUser });

            // вот сам метод который определяет один ко многим;
            modelBuilder.Entity<ModelFileClient>()
                .HasOne(p => p.ProgectModel1) // связь одного
                .WithMany(t => t.ClientFileProjectModel) // может быть много по этому ссылается на созданную коллекцию;
                .HasForeignKey(d => d.IdProjectModel104); // созданный внешний ключ;
        }
    }
}
