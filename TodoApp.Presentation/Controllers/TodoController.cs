using Microsoft.AspNetCore.Mvc;
using TodoApp.Domain;

namespace TodoApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly Application.Repositories.ITodoRepository _todoRepository;

        public TodoController(Application.Repositories.ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Domain.TodoItem>>> GetTodoItems()
        {
            var todoItems = await _todoRepository.GetAllAsync();
            return Ok(todoItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var todoItem = await _todoRepository.GetByIdAsync(id);
            if (todoItem == null)
                return NotFound();

            return Ok(todoItem);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var newToDoItem = new TodoItem();
            newToDoItem.Title = todoItemDTO.Title;
            await _todoRepository.AddAsync(newToDoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = newToDoItem.Id }, newToDoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(int id, TodoItemDTO todoItemDTO)
        {
            var todoItem = await _todoRepository.GetByIdAsync(id);
            if (todoItem is null)
                return BadRequest();

            todoItem.Title = todoItemDTO.Title;
            todoItem.IsCompleted = todoItemDTO.IsCompleted;

            await _todoRepository.UpdateAsync(todoItem);
            return Ok("Updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            await _todoRepository.DeleteAsync(id);
            return Ok("Deleted");
        }

    }
}
