using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuHoc.Models;

public class NewsPost
{
    [Key]
    public int News_Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public DateTime TimePosted { get; set; }

    [Required]
    public string UserPosted { get; set; }

    [Required]
    public string Content { get; set; }

    public string Images { get; set; }

    public ICollection<ParentComment> Comments { get; set; }

}
