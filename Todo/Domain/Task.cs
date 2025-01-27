using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.Domain
{
    public class Task
    {
        [Key]
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool Priority { get; set; }
        public bool IsCompleted { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
