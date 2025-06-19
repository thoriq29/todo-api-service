using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sentry;
using static TodoServices.Dto.TodoDtos;
using TodoServices.Services.ItemService;

namespace TodoServices.Controllers
{
    [Authorize]
    [ApiController]
    [Route("checklist/{checklistId:int}/item")]
    public class ItemController(IItemService itemService) : ControllerBase
    {
        [HttpGet]
        public async Task<IResult> GetAll(int checklistId) => await itemService.GetAllItemsAsync(checklistId, User);

        [HttpPost]
        public async Task<IResult> Create(int checklistId, CreateItemDto dto) => await itemService.CreateItemAsync(checklistId, dto, User);

        [HttpGet("{checklistItemId:int}")]
        public async Task<IResult> GetById(int checklistId, int checklistItemId) => await itemService.GetItemByIdAsync(checklistId, checklistItemId, User);

        [HttpPut("{checklistItemId:int}")]
        public async Task<IResult> UpdateStatus(int checklistId, int checklistItemId) => await itemService.UpdateItemStatusAsync(checklistId, checklistItemId, User);

        [HttpDelete("{checklistItemId:int}")]
        public async Task<IResult> Delete(int checklistId, int checklistItemId) => await itemService.DeleteItemAsync(checklistId, checklistItemId, User);

        [HttpPut("rename/{checklistItemId:int}")]
        public async Task<IResult> Rename(int checklistId, int checklistItemId, RenameItemDto dto) => await itemService.RenameItemAsync(checklistId, checklistItemId, dto, User);
    }
}
