
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientTrackingSystem.Web.Models
{
    public class Patient
    {
        [Key]
        [Required]
        public int id { get; set; }


        [Required(ErrorMessage = "You must enter ID card number.")]
        [Range(10000000000, 99999999999, ErrorMessage = "ID card number is not in the correct format.")]
        public long id_card { get; set; }

        [MaxLength(40)]
        [RegularExpression(@"^[A-Za-zğüşıöçĞÜŞİÖÇ.\s]+$", ErrorMessage = "Name Surname is not in the correct format.")]
        [Required(ErrorMessage = "You must enter Name Surname.")]
        public string name_surname { get; set; }

        [Column(TypeName = "Date")]
        [Required(ErrorMessage = "You must enter birthday.")]
        public DateTime birthday { get; set; }

       
    }
}
