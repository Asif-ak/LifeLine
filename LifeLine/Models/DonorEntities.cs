using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifeLine_WebApi.Models
{
    public class Donor
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DonorID { get; set; }
        [Required, MaxLength(50)]
        public string DonorName { get; set; }
        [Required]
        public BloodType DonorBloodtype { get; set; }

        // a simple regex for getting 12 digit long cell phone number for paki users
        [Required,MaxLength(12),RegularExpression(@"^[0-9]{12}$")]
        public string DonorCellNumber { get; set; }
        //[Required]
        [MaxLength(80)]
        public string Email { get; set; }
        [Required]
        public string City { get; set; }
    }
    
}