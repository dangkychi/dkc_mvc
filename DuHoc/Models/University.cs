using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuHoc.Models
{
    public class University
    {
        [Key]
        public int University_Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Decription { get; set; }

        [Required]
        public decimal TuitionFee { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Images { get; set; }

        public int Id { get; set; }
        // Navigation property to represent the related Country
        [ForeignKey("Id")]
        public Country Country { get; set; }
        public ICollection<Course> Course { get; set; }
    }
}
