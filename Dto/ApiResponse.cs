namespace TodoServices.Dto
{
    public record ApiResponse<T>(T? Data, int Status, string? Error = null);
}
