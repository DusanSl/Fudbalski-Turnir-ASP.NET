namespace FudbalskiTurnir.BLL.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<string> Roles { get; set; } = new List<string>();
        public string? SelectedRole { get; set; }
    }
}