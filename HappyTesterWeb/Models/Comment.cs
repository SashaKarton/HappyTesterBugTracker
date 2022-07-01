using System.ComponentModel.DataAnnotations;

namespace HappyTesterWeb.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public User Commenter { get; set; }
        public string Message { get; set; }
        public DateTime CommentDateTime { get; set; } = DateTime.Now;
    }
}
