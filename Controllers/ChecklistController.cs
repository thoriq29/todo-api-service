using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sentry;
using static TodoServices.Dto.TodoDtos;
using TodoServices.Services.ChecklistService;

namespace TodoServices.Controllers
{
    [Authorize]
    [ApiController]
    [Route("checklist")]
    public class ChecklistController(IChecklistService checklistService) : ControllerBase
    {
        [HttpGet]
        public async Task<IResult> GetAll() => await checklistService.GetAllChecklistsAsync(User);

        [HttpPost]
        public async Task<IResult> Create(CreateChecklistDto dto) => await checklistService.CreateChecklistAsync(dto, User);

        [HttpDelete("{checklistId:int}")]
        public async Task<IResult> Delete(int checklistId) => await checklistService.DeleteChecklistAsync(checklistId, User);
    }
}
