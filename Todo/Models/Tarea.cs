using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Todo.Models;

namespace Todo.Models;

public class Tarea
{
    public int Id { get; set; } 
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool Priority { get; set; }
    public bool IsCompleted { get; set; }

    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
}
