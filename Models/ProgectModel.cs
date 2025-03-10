using System.ComponentModel.DataAnnotations;

namespace Arctech_Manufaction_Menedgment.Models
{
    public class ProgectModel
    {
        [Key]
        public int IdProjectModel { get; set; }
        [Required (ErrorMessage ="Поле для обязательного заполнения")]
        public string NameProjectModel { get; set; }
        [Required (ErrorMessage ="Поле для обязательного заполнения")]
        public string ClientNameProjectModel { get; set; }
        [Required(ErrorMessage = "Обязательно заполните дату")]
        public DateTime BeginTime { get; set; }
        public DateTime CoordinationTime { get; set; }
        // Коллекция файлов, которые будут прикреплены в таблице;
        public ICollection <ModelFileClient>? ClientFileProjectModel { get; set; }
        public byte? CoordinationFileProjectModel { get; set; }
        public bool OrderInManufaction {  get; set; } // Заказ в производство
        public int? StatusOrder {  get; set; } 
        public string? NotesProjectModel { get; set; }

        public string Converting(int idpoject)
        {
            string numer=idpoject.ToString("D5");
            return (numer);
        }
    }
}
