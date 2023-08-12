using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public TaskPriority Priority { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DateOnly DueDate { get; set; }


        public TaskItem()
        {
        }
    }

    public enum TaskPriority
    {
        Low,
        Normal,
        High,
        Urgent
    }
}
