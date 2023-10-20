using DuHoc.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuHoc.Models
{
    public class User
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string user_role { get; set; }

        public Profile Profile { get; set; }
        public ICollection<Appointment> Appointment { get; set; }
        public ICollection<ParentComment> ParentComments { get; set; }
        public ICollection<ChildComment> ChildComments { get; set; }
    }
}

