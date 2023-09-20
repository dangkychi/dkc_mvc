using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuHoc.Models;

public class Profile
{
    [Key]
    public int user_id { get; set; }
    public string full_name { get; set; }
    [DataType(DataType.Date)]
    public DateTime birthdate { get; set; }
    public string phone_number { get; set; }
    public string email { get; set; }
    public string address { get; set; }

    [ForeignKey("user_id")]
    public User User { get; set; }
}