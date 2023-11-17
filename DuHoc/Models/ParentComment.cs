using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuHoc.Models
{
    public class ParentComment
    {
        public int ParentComment_Id { get; set; }
        public string user_name { get; set; }
        public string Text { get; set; }
        public DateTime Comment_Date { get; set; }

        public int user_id { get; set; }
        public User User { get; set; }
        public ICollection<ChildComment> ChildComments { get; set; }
    }
}
