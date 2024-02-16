using Microsoft.EntityFrameworkCore;

namespace TodoList.Models
{
    public class Todo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public string Attributes { get; set; }
        public string Priority { get; set; }
        public bool IsCompleted { get; set; }
    }
}
