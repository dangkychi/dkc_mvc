using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuHoc.Models;

public class Course
{
    [Key]
    public int Course_Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    [Required]
    public DateTime DayPosted { get; set; }

    public string Scholarship { get; set; }

    // Foreign key to represent the related University
    [ForeignKey("University_Id")]
    public int University_Id { get; set; }

    // Navigation property to represent the related University
    public University University { get; set; }
}
