using Arctech_Manufaction_Menedgment.Enums;

namespace Arctech_Manufaction_Menedgment.Models
{
    public class General_Admin
    {
        public UserArctech UserGeneral_Admin{ get; set; }
        public List<UserArctech> ListUser { get; set; }
        public List<ProgectModel> ProgectModel {  get; set; }
        public List<(RoleUser Value,string Text)>RoleEnum { get; set; } = new List<(RoleUser, string)>();
    }
}
