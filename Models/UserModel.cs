#nullable enable

namespace TodoServices.Models
{
    public class UserModel: BaseModel
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public ICollection<ChecklistModel> Checklists { get; set; } = new List<ChecklistModel>();

    }
}
