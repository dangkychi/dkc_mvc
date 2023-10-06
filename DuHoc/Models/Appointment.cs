using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DuHoc.Models
{
    public class Appointment
    {
        [Key]
        public int Appointment_Id { get; set; }
        [Required]
        public int user_id { get;set; }
        [Required]
        public DateTime Appointment_Date { get; set; }
        [Required]
        public string Title { get; set; }
        public string Decription { get; set; }
        public DateTime Create_At { get; set; }
        [Required]
        public int Status { get; set; }

        [ForeignKey("user_id")]
        public User User { get; set; }
    }
}
