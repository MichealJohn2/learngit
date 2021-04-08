using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi1.Models;

namespace TodoApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItem1Controller : ControllerBase
    {
        private readonly TodoContext1 _context;

        public TodoItem1Controller(TodoContext1 context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public  List<TodoItem1> Get()
        {
            using(TodoContext1 t1=new TodoContext1())
            {
                var todo = t1.TodoItems.ToList();
                return  todo;
            }
            
                
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem1DTO>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItem1DTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public ActionResult<IEnumerable<string>> PostWeatherForecast(TodoItem1 todoitem1)
        {
            var database = new TodoContext1();
            database.TodoItems.Add(new TodoItem1 { Id = todoitem1.Id, Name = todoitem1.Name, IsComplete = todoitem1.IsComplete });
            database.SaveChanges();
            return new string[] { "value1", "value2" };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id) =>
             _context.TodoItems.Any(e => e.Id == id);

        private static TodoItem1DTO ItemToDTO(TodoItem1 todoItem) =>
            new TodoItem1DTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}
    