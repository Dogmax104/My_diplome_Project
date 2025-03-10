using Arctech_Manufaction_Menedgment.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Arctech_Manufaction_Menedgment.Models
{
    public class UserArctech
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите табельный номер для прользователя")]
        public int Namer_for_project { get; set; }
        [Required(ErrorMessage = "Введите имя сотрудника")]
        public string FistNameUser { get; set; }
        [Required(ErrorMessage = "Введите фамилию сотрудника")]
        public string SecondNameUser { get; set; }
        [Required(ErrorMessage = "Создайте логин для сотрудника")]
        public string NameUser { get; set; }
        public string PasswordUser { get; set; }
        public DateTime BeginUser { get; set; } = DateTime.Now;
        public RoleUser RoleUser{ get; set; }  // Создан тип данных Enum для перечисления;
    }
}
