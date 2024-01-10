using Microsoft.AspNetCore.Mvc;

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

    }
}
