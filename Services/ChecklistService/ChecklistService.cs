using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TodoServices.Dto;
using TodoServices.Models;
using TodoServices.Repositories.Checklist;
using static TodoServices.Dto.TodoDtos;

namespace TodoServices.Services.ChecklistService
{
    public class ChecklistService(IChecklistRepository checklistRepo) : IChecklistService
    {
        private static int GetUserId(ClaimsPrincipal user) => int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<IResult> GetAllChecklistsAsync(ClaimsPrincipal user)
        {
            try
            {
                var checklists = await checklistRepo.GetAllByUserIdAsync(GetUserId(user));
                var dtos = checklists.Select(c => new ChecklistDto(c.ID, c.Name));
                return Results.Ok(new ApiResponse<IEnumerable<ChecklistDto>>(dtos, StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                return Results.Json(
                    new ApiResponse<object?>(null, StatusCodes.Status500InternalServerError, $"Terjadi kesalahan saat mengambil daftar checklist: {ex.Message}"),
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<IResult> CreateChecklistAsync(CreateChecklistDto dto, ClaimsPrincipal user)
        {
            try
            {
                var checklist = new ChecklistModel
                {
                    Name = dto.Name,
                    UserId = GetUserId(user)
                };

                var created = await checklistRepo.AddAsync(checklist);
                var responseDto = new ChecklistDto(created.ID, created.Name);

                return Results.Created(
                    $"/checklist/{created.ID}",
                    new ApiResponse<ChecklistDto>(responseDto, StatusCodes.Status201Created));
            }
            catch (Exception ex)
            {
                return Results.Json(
                    new ApiResponse<object?>(null, StatusCodes.Status500InternalServerError, $"Terjadi kesalahan saat membuat checklist: {ex.Message}"),
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<IResult> DeleteChecklistAsync(int checklistId, ClaimsPrincipal user)
        {
            try
            {
                if (!await checklistRepo.DeleteAsync(checklistId, GetUserId(user)))
                {
                    return Results.NotFound(new ApiResponse<object?>(null, StatusCodes.Status404NotFound, "Checklist tidak ditemukan."));
                }

                return Results.Ok(new ApiResponse<object>(new { message = "Checklist berhasil dihapus." }, StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                return Results.Json(
                    new ApiResponse<object?>(null, StatusCodes.Status500InternalServerError, $"Terjadi kesalahan saat menghapus checklist: {ex.Message}"),
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
