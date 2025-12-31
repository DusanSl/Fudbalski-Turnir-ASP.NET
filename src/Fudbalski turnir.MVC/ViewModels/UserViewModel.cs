using FudbalskiTurnir.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace FudbalskiTurnir.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public IEnumerable<string>? Roles { get; set; }
        [Display(Name = "Broj telefona")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Potvrđen mail")]
        public bool EmailConfirmed { get; set; }
        [Display(Name = "Potvrđen broj telefona")]
        public bool PhoneNumberConfirmed { get; set; }
        public bool IsActive { get; set; }
        [Display(Name = "Uloga")]
        public string SelectedRole { get; set; } = "User";
        public IEnumerable<string>? AllRoles { get; set; }
    }
}