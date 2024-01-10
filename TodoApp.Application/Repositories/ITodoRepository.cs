
namespace TodoApp.Application.Repositories
{
    public interface ITodoRepository
    {
        Task<List<Domain.TodoItem>> GetAllAsync();
        Task<Domain.TodoItem> GetByIdAsync(int id);
        Task AddAsync(Domain.TodoItem todoItem);
        Task UpdateAsync(Domain.TodoItem todoItem);
        Task DeleteAsync(int id);
    }
}
