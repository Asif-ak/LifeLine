using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace LifeLine_WebApi.Models
{
    public class Requestor
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required, MaxLength(50)]
        public string RequestorName { get; set; }
        // a simple regex for getting 12 digit long cell phone number for paki users
        [Required,MaxLength(12),RegularExpression(@"^[0-9]{12}$")]
        public string RequestorCellNumber { get; set; }
        //[Required] make it require afterwards
        public string City { get; set; }
        //[Required] make it require afterwards
        [MaxLength(75)]
        public string DonationAddress { get; set; }
        //[Required] make it require afterwards
        [MaxLength(80)]
        public string Email { get; set; }
        
        public DateTime RequestedOn { get; set; }=DateTime.Now.Date;
        public ICollection<Requests> Requests { get; set; }


        
    }
}