using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ILogger<TodoItemsController> _logger;
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context, ILogger<TodoItemsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItem()
        {
            _logger.LogInformation("Fetching all todo items.");
            return await _context.TodoItem.ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(Guid id)
        {
            _logger.LogInformation("Fetching todo item with ID: {id}", id);
            var todoItem = await _context.TodoItem.FindAsync(id);

            if (todoItem == null)
            {
                _logger.LogWarning("Todo item with ID: {id} not found.", id);
                return NotFound();
            }

            return todoItem;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(Guid id, TodoItem todoItem)
        {
            _logger.LogInformation("Updating todo item with ID: {id}", id);

            if (id != todoItem.Id)
            {
                _logger.LogWarning("ID in the URL does not match ID in the body. URL ID: {urlId}, Body ID: {bodyId}", id, todoItem.Id);
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency error while updating todo item with ID: {id}", id);
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError("An unexpected error occurred while updating todo item with ID: {id}", id);
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItem.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            _logger.LogInformation("Deleting todo item with ID: {id}", id);
            var todoItem = await _context.TodoItem.FindAsync(id);
            if (todoItem == null)
            {
                _logger.LogWarning("Todo item with ID: {id} not found.", id);
                return NotFound();
            }

            _context.TodoItem.Remove(todoItem);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Todo item with ID: {id} deleted.", id);
            return NoContent();
        }

        private bool TodoItemExists(Guid id)
        {
            return _context.TodoItem.Any(e => e.Id == id);
        }
    }
}
