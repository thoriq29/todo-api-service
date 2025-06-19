using Microsoft.EntityFrameworkCore;
using TodoServices.Data;
using TodoServices.Models;

namespace TodoServices.Repositories.Checklist
{
    public class ChecklistRepository(TodoDbContext context) : IChecklistRepository
    {
        public async Task<IEnumerable<ChecklistModel>> GetAllByUserIdAsync(int userId) => await context.Checklists.Where(c => c.UserId == userId).ToListAsync();
        public async Task<ChecklistModel?> GetByIdAsync(int checklistId, int userId) => await context.Checklists.Include(c => c.Items).FirstOrDefaultAsync(c => c.ID == checklistId && c.UserId == userId);
        public async Task<ChecklistModel> AddAsync(ChecklistModel checklist)
        {
            context.Checklists.Add(checklist);
            await context.SaveChangesAsync();
            return checklist;
        }
        public async Task<bool> DeleteAsync(int checklistId, int userId)
        {
            var checklist = await context.Checklists.FirstOrDefaultAsync(c => c.ID == checklistId && c.UserId == userId);
            if (checklist == null) return false;
            context.Checklists.Remove(checklist);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> IsOwnerAsync(int checklistId, int userId) => await context.Checklists.AnyAsync(c => c.ID == checklistId && c.UserId == userId);
    }
}
