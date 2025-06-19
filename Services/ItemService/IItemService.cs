using System.Security.Claims;
using static TodoServices.Dto.TodoDtos;

namespace TodoServices.Services.ItemService
{
    public interface IItemService
    {
        Task<IResult> GetAllItemsAsync(int checklistId, ClaimsPrincipal user);
        Task<IResult> CreateItemAsync(int checklistId, CreateItemDto dto, ClaimsPrincipal user);
        Task<IResult> GetItemByIdAsync(int checklistId, int itemId, ClaimsPrincipal user);
        Task<IResult> UpdateItemStatusAsync(int checklistId, int itemId, ClaimsPrincipal user);
        Task<IResult> DeleteItemAsync(int checklistId, int itemId, ClaimsPrincipal user);
        Task<IResult> RenameItemAsync(int checklistId, int itemId, RenameItemDto dto, ClaimsPrincipal user);
    }
}
