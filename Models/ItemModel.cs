namespace TodoServices.Models
{
    public class ItemModel: BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public bool Status { get; set; } = false;
        public long ChecklistId { get; set; } // Foreign Key
        public ChecklistModel? Checklist { get; set; }
    }
}
