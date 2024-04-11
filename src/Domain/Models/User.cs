using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace newbuy.Domain.Models;

[Table("users")]
public class User
{
    public Guid Id { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [StringLength(50)]
    public string? Surname { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public UserType? UserType { get; set; }
    public Guid UserTypeId { get; set; }


}