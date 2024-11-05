using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerApi.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;       // Valor predeterminado para evitar valores null
        public string Description { get; set; } = string.Empty; // Valor predeterminado para evitar valores null
        public bool IsCompleted { get; set; }
    }
}