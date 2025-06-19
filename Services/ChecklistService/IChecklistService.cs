using System.Security.Claims;
using static TodoServices.Dto.TodoDtos;

namespace TodoServices.Services.ChecklistService
{
    public interface IChecklistService
    {
        Task<IResult> GetAllChecklistsAsync(ClaimsPrincipal user);
        Task<IResult> CreateChecklistAsync(CreateChecklistDto dto, ClaimsPrincipal user);
        Task<IResult> DeleteChecklistAsync(int checklistId, ClaimsPrincipal user);
    }
}
