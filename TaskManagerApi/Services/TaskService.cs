using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerApi.Models;
using TaskManagerApi.Data;

namespace TaskManagerApi.Services
{
       public class TaskService
    {
        private readonly TaskDbContext _context;

        public TaskService(TaskDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Devuelve todos los elementos de tarea
        public IEnumerable<TaskItem> GetAll()
        {
            return _context.TaskItems.ToList();
        }

        // Obtiene una tarea por ID, devuelve null si no existe
        public TaskItem? GetById(int id)
        {
            return _context.TaskItems.FirstOrDefault(t => t.Id == id);
        }

        // Agrega una nueva tarea
        public void Add(TaskItem taskItem)
        {
            if (taskItem == null)
            {
                throw new ArgumentNullException(nameof(taskItem), "Task item cannot be null.");
            }

            _context.TaskItems.Add(taskItem);
            _context.SaveChanges();
        }

        // Actualiza una tarea existente
        public void Update(TaskItem taskItem)
        {
            if (taskItem == null)
            {
                throw new ArgumentNullException(nameof(taskItem), "Task item cannot be null.");
            }

            var existingTask = GetById(taskItem.Id);
            if (existingTask == null)
            {
                throw new KeyNotFoundException($"Task with ID {taskItem.Id} not found.");
            }

            _context.TaskItems.Update(taskItem);
            _context.SaveChanges();
        }

        // Elimina una tarea por ID
        public void Delete(int id)
        {
            var taskItem = GetById(id);
            if (taskItem != null)
            {
                _context.TaskItems.Remove(taskItem);
                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"Task with ID {id} not found.");
            }
        }
    }
}