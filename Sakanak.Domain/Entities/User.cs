using Sakanak.Domain.Enums;

namespace Sakanak.Domain.Entities;

public abstract class User
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    public UserStatus Status { get; set; }
}
