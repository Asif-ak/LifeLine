using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifeLine_WebApi.Models
{
    public class Requests
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestID { get; set; }
        [Required]
        public BloodType RequestedBloodtype { get; set; }
        public bool IsActive { get;set; }
        [Required]
        public int RID { get; set; }
        public virtual Requestor Requestor { get; set; }



    }
}