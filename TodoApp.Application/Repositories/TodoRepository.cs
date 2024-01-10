using Microsoft.EntityFrameworkCore;

namespace TodoApp.Application.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly Infrastructure.AppDbContext _context;

        public TodoRepository(Infrastructure.AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<Domain.TodoItem> GetByIdAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task AddAsync(Domain.TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.TodoItem todoItem)
        {
            _context.Entry(todoItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem != null)
            {
                _context.TodoItems.Remove(todoItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
