using System.ComponentModel.DataAnnotations;

namespace Arctech_Manufaction_Menedgment.Models
{
    public class ProgectModel
    {
        [Key]
        public int IdProjectModel { get; set; }
        [Required (ErrorMessage ="Поле для обязательного заполнения")]
        public string NameProjectModel { get; set; }    //Имя проекта или имя изделия;
        [Required (ErrorMessage ="Поле для обязательного заполнения")]
        public string ClientNameProjectModel { get; set; }  // Заказчик, ответственное лицо за заказ, телефон, адрес;
        [Required(ErrorMessage = "Обязательно заполните дату")]
        public DateTime BeginTime { get; set; }     // Дата поступления заказа;
        public DateTime CoordinationTime { get; set; }  // Дата согласования заказа;
        // Коллекция файлов, которые будут прикреплены в таблице;
        public ICollection <ModelFileClient>? ClientFileProjectModel { get; set; }  //Прилогаемые файлы, которые для заказа;
        public byte? CoordinationFileProjectModel { get; set; }     //Файлы, которые будет добовлять конструктор;
        public bool OrderInManufaction {  get; set; } // "флаг" Заказ в производство устанавливает menedger;
        public int? StatusOrder {  get; set; }      // статус заказа, в int для того чтоб можно видеть процент выполнения заказа;
        public string? NotesProjectModel { get; set; }      // Заметки для заказа;

    }
}
