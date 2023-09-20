using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace DuHoc.Models
{
    public class User
    {
        public int user_id { get; set; } 
        public string user_name { get; set; }
        public string password { get; set; }
        public int user_role { get; set; }

        public Profile Profile { get; set; }
    }
}