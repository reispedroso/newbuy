using System.ComponentModel.DataAnnotations.Schema;

namespace newbuy.Domain.Models;

[Table("user_type")]
public class UserType
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}