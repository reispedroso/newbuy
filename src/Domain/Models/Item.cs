using System.ComponentModel.DataAnnotations.Schema;

namespace newbuy.Domain.Models;

[Table("items")]
public class Item
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid ItemTypeId { get; set; }
    public ItemType? ItemType { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}