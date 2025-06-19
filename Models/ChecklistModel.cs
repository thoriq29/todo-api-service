namespace TodoServices.Models
{
    public class ChecklistModel: BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public long UserId { get; set; }

        public UserModel? User { get; set; }
        public ICollection<ItemModel> Items { get; set; } = new List<ItemModel>();
    }
}
