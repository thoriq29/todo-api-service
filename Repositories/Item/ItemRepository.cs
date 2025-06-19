using Microsoft.EntityFrameworkCore;
using TodoServices.Data;
using TodoServices.Models;

namespace TodoServices.Repositories.Item
{
    public class ItemRepository(TodoDbContext context) : IItemRepository
    {
        public async Task<IEnumerable<ItemModel>> GetAllByChecklistIdAsync(int checklistId) => await context.Items.Where(i => i.ChecklistId == checklistId).ToListAsync();
        public async Task<ItemModel?> GetByIdAsync(int itemId, int checklistId) => await context.Items.FirstOrDefaultAsync(i => i.ID == itemId && i.ChecklistId == checklistId);
        public async Task<ItemModel> AddAsync(ItemModel item)
        {
            context.Items.Add(item);
            await context.SaveChangesAsync();
            return item;
        }
        public async Task UpdateAsync(ItemModel item)
        {
            context.Items.Update(item);
            await context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(int itemId, int checklistId)
        {
            var item = await GetByIdAsync(itemId, checklistId);
            if (item == null) return false;
            context.Items.Remove(item);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
