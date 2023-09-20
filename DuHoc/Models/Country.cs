using System.ComponentModel.DataAnnotations;

namespace DuHoc.Models;

public class Country
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Continent { get; set; }
    public string? Images { get; set; }

    public ICollection<University> University { get; set; }
}