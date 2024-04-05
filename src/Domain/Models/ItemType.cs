using System.ComponentModel.DataAnnotations.Schema;

namespace newbuy.Domain.Models;

[Table("item_type")]
public class ItemType
{
    public Guid Id { get; set; }
    public string? TypeName { get; set; }
}