using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TodoServices.Dto;
using TodoServices.Models;
using TodoServices.Repositories.Checklist;
using TodoServices.Repositories.Item;
using static TodoServices.Dto.TodoDtos;

namespace TodoServices.Services.ItemService
{
    public class ItemService(IItemRepository itemRepo, IChecklistRepository checklistRepo) : IItemService
    {
        private static int GetUserId(ClaimsPrincipal user) => int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);

        private async Task<bool> CanAccessChecklist(int checklistId, ClaimsPrincipal user) =>
            await checklistRepo.IsOwnerAsync(checklistId, GetUserId(user));

        public async Task<IResult> GetAllItemsAsync(int checklistId, ClaimsPrincipal user)
        {
            try
            {
                if (!await CanAccessChecklist(checklistId, user))
                    return Results.NotFound(new ApiResponse<object?>(null, StatusCodes.Status404NotFound, "Checklist tidak ditemukan atau Anda tidak memiliki akses."));

                var items = await itemRepo.GetAllByChecklistIdAsync(checklistId);
                var dtos = items.Select(i => new ItemDto(i.ID, i.Name, i.Status));
                return Results.Ok(new ApiResponse<IEnumerable<ItemDto>>(dtos, StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                return Results.Json(new ApiResponse<object?>(null, StatusCodes.Status500InternalServerError, $"Gagal mengambil item: {ex.Message}"));
            }
        }

        public async Task<IResult> CreateItemAsync(int checklistId, CreateItemDto dto, ClaimsPrincipal user)
        {
            try
            {
                if (!await CanAccessChecklist(checklistId, user))
                    return Results.NotFound(new ApiResponse<object?>(null, StatusCodes.Status404NotFound, "Checklist tidak ditemukan atau Anda tidak memiliki akses."));

                var item = new ItemModel { Name = dto.ItemName, ChecklistId = checklistId };
                var created = await itemRepo.AddAsync(item);
                var responseDto = new ItemDto(created.ID, created.Name, created.Status);
                return Results.Created($"/checklist/{checklistId}/item/{created.ID}", new ApiResponse<ItemDto>(responseDto, StatusCodes.Status201Created));
            }
            catch (Exception ex)
            {
                return Results.Json(new ApiResponse<object?>(null, StatusCodes.Status500InternalServerError, $"Gagal membuat item: {ex.Message}"));
            }
        }

        public async Task<IResult> GetItemByIdAsync(int checklistId, int itemId, ClaimsPrincipal user)
        {
            try
            {
                if (!await CanAccessChecklist(checklistId, user))
                    return Results.NotFound(new ApiResponse<object?>(null, StatusCodes.Status404NotFound, "Checklist tidak ditemukan atau Anda tidak memiliki akses."));

                var item = await itemRepo.GetByIdAsync(itemId, checklistId);
                if (item == null)
                    return Results.NotFound(new ApiResponse<object?>(null, StatusCodes.Status404NotFound, "Item tidak ditemukan."));

                return Results.Ok(new ApiResponse<ItemDto>(new ItemDto(item.ID, item.Name, item.Status), StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                return Results.Json(new ApiResponse<object?>(null, StatusCodes.Status500InternalServerError, $"Gagal mengambil item: {ex.Message}"));
            }
        }

        public async Task<IResult> UpdateItemStatusAsync(int checklistId, int itemId, ClaimsPrincipal user)
        {
            try
            {
                if (!await CanAccessChecklist(checklistId, user))
                    return Results.NotFound(new ApiResponse<object?>(null, StatusCodes.Status404NotFound, "Checklist tidak ditemukan atau Anda tidak memiliki akses."));

                var item = await itemRepo.GetByIdAsync(itemId, checklistId);
                if (item == null)
                    return Results.NotFound(new ApiResponse<object?>(null, StatusCodes.Status404NotFound, "Item tidak ditemukan."));

                item.Status = !item.Status;
                await itemRepo.UpdateAsync(item);

                return Results.Ok(new ApiResponse<ItemDto>(new ItemDto(item.ID, item.Name, item.Status), StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                return Results.Json(new ApiResponse<object?>(null, StatusCodes.Status500InternalServerError, $"Gagal memperbarui status item: {ex.Message}"));
            }
        }

        public async Task<IResult> DeleteItemAsync(int checklistId, int itemId, ClaimsPrincipal user)
        {
            try
            {
                if (!await CanAccessChecklist(checklistId, user))
                    return Results.NotFound(new ApiResponse<object?>(null, StatusCodes.Status404NotFound, "Checklist tidak ditemukan atau Anda tidak memiliki akses."));

                if (!await itemRepo.DeleteAsync(itemId, checklistId))
                    return Results.NotFound(new ApiResponse<object?>(null, StatusCodes.Status404NotFound, "Item tidak ditemukan."));

                return Results.Ok(new ApiResponse<object>(new { message = "Item berhasil dihapus." }, StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                return Results.Json(new ApiResponse<object?>(null, StatusCodes.Status500InternalServerError, $"Gagal menghapus item: {ex.Message}"));
            }
        }

        public async Task<IResult> RenameItemAsync(int checklistId, int itemId, RenameItemDto dto, ClaimsPrincipal user)
        {
            try
            {
                if (!await CanAccessChecklist(checklistId, user))
                    return Results.NotFound(new ApiResponse<object?>(null, StatusCodes.Status404NotFound, "Checklist tidak ditemukan atau Anda tidak memiliki akses."));

                var item = await itemRepo.GetByIdAsync(itemId, checklistId);
                if (item == null)
                    return Results.NotFound(new ApiResponse<object?>(null, StatusCodes.Status404NotFound, "Item tidak ditemukan."));

                item.Name = dto.ItemName;
                await itemRepo.UpdateAsync(item);

                return Results.Ok(new ApiResponse<ItemDto>(new ItemDto(item.ID, item.Name, item.Status), StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                return Results.Json(new ApiResponse<object?>(null, StatusCodes.Status500InternalServerError, $"Gagal mengganti nama item: {ex.Message}"));
            }
        }
    }
}
