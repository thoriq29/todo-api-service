using TodoServices.Models;

namespace TodoServices.Repositories.Checklist
{
    public interface IChecklistRepository
    {
        Task<IEnumerable<ChecklistModel>> GetAllByUserIdAsync(int userId);
        Task<ChecklistModel?> GetByIdAsync(int checklistId, int userId);
        Task<ChecklistModel> AddAsync(ChecklistModel checklist);
        Task<bool> DeleteAsync(int checklistId, int userId);
        Task<bool> IsOwnerAsync(int checklistId, int userId);
    }
}
