using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arctech_Manufaction_Menedgment.Models
{
    public class ModelFileClient
    {
        [Key]
        public int IdModelFileClient {  get; set; }
        public string NameModelFileClient { get; set; }
        public byte [] NameModelFile { get; set; }
        public DateTime? DateModelFile { get; set; }


        // созданная связь с внешним ключом
        public int IdProjectModel104 {  get; set; }
        public ProgectModel? ProgectModel1 { get; set; }
    }
}
