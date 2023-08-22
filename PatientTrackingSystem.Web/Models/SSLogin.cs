using System.ComponentModel.DataAnnotations;

namespace PatientTrackingSystem.Web.Models
{
    public class SSLogin
    {
        [Key]
        [Required]
        public int user_id { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public long identity { get; set; }
        [Required]
        public string role { get; set; }
    }
}
