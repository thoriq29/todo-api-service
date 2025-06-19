using TodoServices.Models;

namespace TodoServices.Repositories.Item
{
    public interface IItemRepository
    {
        Task<IEnumerable<ItemModel>> GetAllByChecklistIdAsync(int checklistId);
        Task<ItemModel?> GetByIdAsync(int itemId, int checklistId);
        Task<ItemModel> AddAsync(ItemModel item);
        Task UpdateAsync(ItemModel item);
        Task<bool> DeleteAsync(int itemId, int checklistId);
    }
}
