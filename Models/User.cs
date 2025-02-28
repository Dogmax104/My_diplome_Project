using Microsoft.AspNetCore.Identity;

namespace Arctech_Manufaction_Menedgment.Models
{
    public class UserArctech
    {
        public int Id { get; set; }
        public string NameUser { get; set; }
        public string PasswordUser { get; set; }
        public string RoleUser { get; set; }
    }
}
