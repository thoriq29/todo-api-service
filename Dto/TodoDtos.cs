namespace TodoServices.Dto
{
    public class TodoDtos
    {
        public record ChecklistDto(long Id, string Name);
        public record ItemDto(long Id, string Name, bool Status);
        public record ChecklistDetailDto(long Id, string Name, List<ItemDto> Items);
        public record CreateChecklistDto(string Name);
        public record CreateItemDto(string ItemName);
        public record RenameItemDto(string ItemName);
    }
}
