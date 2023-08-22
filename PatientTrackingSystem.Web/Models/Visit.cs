using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientTrackingSystem.Web.Models
{
    public class Visit
    {
        [Key]
        [Required]
        public int Visit_Id { get; set; }

        [Required(ErrorMessage = "You must enter patient name.")]
        public int Patient_Id { get; set; }
        [ForeignKey("Patient_Id")]
        public virtual Patient? Patient { get; set; }

        [Required(ErrorMessage = "You must enter visit date.")]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "timestamp(0) without time zone")]
        public DateTime Visit_Date { get; set; }

       
        [Required(ErrorMessage = "You must enter doctor.")]
        [RegularExpression(@"^[A-Za-zğüşıöçĞÜŞİÖÇ\s]+$", ErrorMessage = "Doctor name is not in the correct format.")]
        public string Doctor { get; set; }


        [Required(ErrorMessage = "You must enter complaint.")]
        [Column(TypeName = "text")]
        public string Complaint { get; set; }

        [Required(ErrorMessage = "You must enter form of treatment.")]
        [Column(TypeName = "text")]
        public string Treatment { get; set; }
    }
}
